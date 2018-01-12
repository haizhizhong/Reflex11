if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DBA_ConditionText]') and xtype = 'FN')  
 drop Function DBA_ConditionText
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

CREATE function [dbo].[DBA_ConditionText](@Routine varchar(max), @xType varchar(20))returns varchar(max) as
begin
	--declare @Routine varchar(max), @xType varchar(20)
	--select @Routine = '[dbo].[usp_IN_AccountSubCodeGetByAccountNumber]', @xType = 'P'
	
	/*
	Author:		Robg
	Purpose:	Condition the routine to follow standard header formation
	*/
	declare @Subject varchar(20)
	select @Subject = case @xtype	when 'TR' then 'Trigger'
									when 'P' then 'Proc'
									when 'FN' then 'Func'
									when 'V' then 'View' end

	----strip out leading charaters
	--select @Routine = replace(@Routine, substring(@routine, 1, charindex('create', @Routine)-1), '')
	--where charindex('create', @Routine)-1 > 0
	
	
	--strip out spaces & tabs
	declare @x int select @x = 2
	while @x < 50 and substring(@routine, charindex('create', @Routine)-1, 7) + @subject != 'create ' + @subject
	begin
		select @Routine = replace(@Routine, 'create' + replicate(' ', @x) + @subject, 'create ' + @subject)
		select @Routine = replace(@Routine, 'create' + replicate(char(9), @x) + @subject,  'create ' + @subject)
		select @x = @x + 1	
	end
		
	return @Routine
	--select @Routine
end

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DBA_IndexDiscovery]') and xtype = 'P ')  
 drop Procedure DBA_IndexDiscovery
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE proc DBA_IndexDiscovery as

declare @Version int select @Version = case when substring (@@Version, 1, 25) = 'Microsoft SQL Server 2008' then 2008 else 2005 end

declare @Source varchar(100) select @Source = 'Unknown'
SET NOCOUNT ON


if exists (select * from tempdb..sysobjects where id = object_id(N'tempdb..#massaged_tmp_indexes'))
drop table #massaged_tmp_indexes
CREATE TABLE #massaged_tmp_indexes(
	TableID int,
	TableName varchar(max),
	IndexID int,
	IndexName varchar(max),
	IndexDefinition varchar(max),
	CurrentState varchar(50) not null default 'Implemented')

if exists (select * from tempdb..sysobjects where id = object_id(N'tempdb..#tmp_indexes'))
drop table #tmp_indexes
create table #tmp_indexes(
	object_id int,
	tablename varchar(max),
	tableid int,
	indexid int,
	indexname varchar(max),
	isunique bit,
	isclustered bit,
	indexfillfactor int,
	Filter_Definition varchar(max),
	keycolumns VARCHAR(max), 
	includes VARCHAR(max))

if not exists (select * from sys.tables t where t.name = 'DBA_Reflex_Index_Storage')
begin
	select @Source = 'Reflex Setup'
	create table DBA_Reflex_Index_Storage(
		id int not null identity(1,1) primary key,
		TableID int,
		TableName varchar(max),
		IndexID int,
		IndexName varchar(max),
		IndexDefinition varchar(max),
		CurrentState varchar(50) not null default 'Implemented',
		IndexSource varchar(100) not null default 'Unknown',
		DropIndex bit not null default 0,
		ResurectIndex bit not null default 0)
end

insert #tmp_indexes (object_id, tablename, tableid, indexid, indexname, isunique, isclustered, indexfillfactor, Filter_Definition )
SELECT
ixz.object_id,
tablename = QUOTENAME(scmz.name) + '.' + QUOTENAME((OBJECT_NAME(ixz.object_id))),
tableid = ixz.object_id,
indexid = ixz.index_id,
indexname = ixz.name,
isunique = INDEXPROPERTY (ixz.object_id,ixz.name,'isunique'),
isclustered = INDEXPROPERTY (ixz.object_id,ixz.name,'isclustered'),
indexfillfactor = INDEXPROPERTY (ixz.object_id,ixz.name,'indexfillfactor'),
case when @Version = 2008 then
	--SQL2008+ Filtered indexes:
	CASE
	WHEN ixz.filter_definition IS NULL
	THEN ''
	ELSE ' WHERE ' + ixz.filter_definition
	END 
else 
'' 
end 
Filter_Definition
FROM sys.indexes ixz
INNER JOIN sys.objects obz ON ixz.object_id = obz.object_id
INNER JOIN sys.schemas scmz ON obz.schema_id = scmz.schema_id
WHERE ixz.index_id > 0
AND ixz.index_id < 255 ---- 0 = HEAP index, 255 = TEXT columns index
AND INDEXPROPERTY (ixz.object_id,ixz.name,'ISUNIQUE') = 0 -- comment out to include unique and
AND INDEXPROPERTY (ixz.object_id,ixz.name,'ISCLUSTERED') = 0 -- comment out to include PK's


DECLARE @isql_key VARCHAR(max), @isql_incl VARCHAR(max), @tableid INT, @indexid INT

DECLARE index_cursor CURSOR
FOR
SELECT tableid, indexid FROM #tmp_indexes
OPEN index_cursor
FETCH NEXT FROM index_cursor INTO @tableid, @indexid

WHILE @@FETCH_STATUS <> -1
BEGIN
	SELECT @isql_key = '', @isql_incl = ''
	SELECT --ixz.name, colz.colid, colz.name, ixcolz.index_id, ixcolz.object_id, *
	--key column
	@isql_key = 
		CASE ixcolz.is_included_column
		WHEN 0 THEN 
			CASE ixcolz.is_descending_key
			WHEN 1 THEN @isql_key + COALESCE(colz.name,'') + ' DESC, '
			ELSE @isql_key + COALESCE(colz.name,'') + ' ASC, '
			END
		ELSE @isql_key END,

	--include column
	@isql_incl = 
		CASE ixcolz.is_included_column
		WHEN 1 THEN 
			CASE ixcolz.is_descending_key
			WHEN 1 THEN @isql_incl + COALESCE(colz.name,'') + ', '
			ELSE @isql_incl + COALESCE(colz.name,'') + ', ' END
		ELSE @isql_incl END
	FROM sysindexes ixz
	INNER JOIN sys.index_columns AS ixcolz ON (ixcolz.column_id > 0 AND ( ixcolz.key_ordinal > 0 OR ixcolz.partition_ordinal = 0 OR ixcolz.is_included_column != 0)	)
	AND ( ixcolz.index_id=CAST(ixz.indid AS INT) AND ixcolz.object_id=ixz.id )
	INNER JOIN sys.columns AS colz ON colz.object_id = ixcolz.object_id AND colz.column_id = ixcolz.column_id
	WHERE ixz.indid > 0 AND ixz.indid < 255
	AND (ixz.status & 64) = 0
	AND ixz.id = @tableid
	AND ixz.indid = @indexid
	ORDER BY ixz.name, 
		CASE ixcolz.is_included_column
		WHEN 1 THEN ixcolz.index_column_id
		ELSE ixcolz.key_ordinal
		END

	--remove any trailing commas from the cursor results
	IF LEN(@isql_key) > 1 SET @isql_key = LEFT(@isql_key, LEN(@isql_key) -1)
	IF LEN(@isql_incl) > 1 SET @isql_incl = LEFT(@isql_incl, LEN(@isql_incl) -1)
	--put the columns collection into our temp table
	UPDATE #tmp_indexes SET keycolumns = @isql_key, includes = @isql_incl
	WHERE tableid = @tableid AND indexid = @indexid
FETCH NEXT FROM index_cursor INTO @tableid,@indexid
END --WHILE
CLOSE index_cursor
DEALLOCATE index_cursor

--remove invalid indexes, ie ones without key columns
DELETE FROM #tmp_indexes WHERE keycolumns = ''

insert #massaged_tmp_indexes
SELECT ti.tableid, ti.TABLENAME, ti.indexid, ti.INDEXNAME, 
'IF NOT EXISTS(SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N''' + ti.TABLENAME + '''' + ') AND name = N' + '''' + ti.INDEXNAME + '''' + ')' + ' ' +
'CREATE '
+ CASE WHEN ti.ISUNIQUE = 1 THEN 'UNIQUE ' ELSE '' END
+ CASE WHEN ti.ISCLUSTERED = 1 THEN 'CLUSTERED ' ELSE '' END
+ 'INDEX ' + QUOTENAME(ti.INDEXNAME)
+ ' ON ' + (ti.TABLENAME) + ' '
+ '(' + ti.keycolumns + ')'
+ CASE
WHEN ti.INDEXFILLFACTOR = 0 AND ti.ISCLUSTERED = 1 AND INCLUDES = '' THEN ti.Filter_Definition + ' WITH (SORT_IN_TEMPDB = ON) ON [' + fg.name + ']'
WHEN INDEXFILLFACTOR = 0 AND ti.ISCLUSTERED = 0 AND ti.INCLUDES = '' THEN ti.Filter_Definition + ' WITH (ONLINE = ON, SORT_IN_TEMPDB = ON) ON [' + fg.name + ']'
WHEN INDEXFILLFACTOR <> 0 AND ti.ISCLUSTERED = 0 AND ti.INCLUDES = '' THEN ti.Filter_Definition + ' WITH (ONLINE = ON, SORT_IN_TEMPDB = ON, FILLFACTOR = ' + CONVERT(VARCHAR(10),ti.INDEXFILLFACTOR) + ') ON [' + fg.name + ']'
WHEN INDEXFILLFACTOR = 0 AND ti.ISCLUSTERED = 0 AND ti.INCLUDES <> '' THEN ' INCLUDE (' + ti.INCLUDES + ') ' + ti.Filter_Definition + ' WITH (ONLINE = ON, SORT_IN_TEMPDB = ON) ON [' + fg.name + ']'
ELSE ' INCLUDE(' + ti.INCLUDES + ') ' + ti.Filter_Definition + ' WITH (FILLFACTOR = ' + CONVERT(VARCHAR(10),ti.INDEXFILLFACTOR) + ', ONLINE = ON, SORT_IN_TEMPDB = ON) ON [' + fg.name + ']'
END [IndexDefinition], 'Implemented' CurrentState
FROM #tmp_indexes ti
JOIN sys.indexes i ON ti.Object_id = i.object_id and ti.indexname = i.name 
JOIN sys.filegroups fg on i.data_space_id = fg.data_space_id
WHERE LEFT(ti.tablename,3) NOT IN ('sys', 'dt_') --exclude system tables
ORDER BY ti.tablename, ti.indexid, ti.indexname

insert DBA_Reflex_Index_Storage(TableID, TableName, IndexID, IndexName, IndexDefinition, CurrentState, IndexSource)
select TableID, TableName, IndexID, IndexName, IndexDefinition, CurrentState, @Source
from #massaged_tmp_indexes
where IndexDefinition not in (select IndexDefinition from DBA_Reflex_Index_Storage)



--SET NOCOUNT OFF

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_addtosourcecontrol]') and xtype = 'P ')  
 drop Procedure dt_addtosourcecontrol
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
create proc dbo.dt_addtosourcecontrol
    @vchSourceSafeINI varchar(255) = '',
    @vchProjectName   varchar(255) ='',
    @vchComment       varchar(255) ='',
    @vchLoginName     varchar(255) ='',
    @vchPassword      varchar(255) =''

as

set nocount on

declare @iReturn int
declare @iObjectId int
select @iObjectId = 0

declare @iStreamObjectId int
select @iStreamObjectId = 0

declare @VSSGUID varchar(100)
select @VSSGUID = 'SQLVersionControl.VCS_SQL'

declare @vchDatabaseName varchar(255)
select @vchDatabaseName = db_name()

declare @iReturnValue int
select @iReturnValue = 0

declare @iPropertyObjectId int
declare @vchParentId varchar(255)

declare @iObjectCount int
select @iObjectCount = 0

    exec @iReturn = master.dbo.sp_OACreate @VSSGUID, @iObjectId OUT
    if @iReturn <> 0 GOTO E_OAError


    /* Create Project in SS */
    exec @iReturn = master.dbo.sp_OAMethod @iObjectId,
											'AddProjectToSourceSafe',
											NULL,
											@vchSourceSafeINI,
											@vchProjectName output,
											@@SERVERNAME,
											@vchDatabaseName,
											@vchLoginName,
											@vchPassword,
											@vchComment


    if @iReturn <> 0 GOTO E_OAError

    /* Set Database Properties */

    begin tran SetProperties

    /* add high level object */

    exec @iPropertyObjectId = dbo.dt_adduserobject_vcs 'VCSProjectID'

    select @vchParentId = CONVERT(varchar(255),@iPropertyObjectId)

    exec dbo.dt_setpropertybyid @iPropertyObjectId, 'VCSProjectID', @vchParentId , NULL
    exec dbo.dt_setpropertybyid @iPropertyObjectId, 'VCSProject' , @vchProjectName , NULL
    exec dbo.dt_setpropertybyid @iPropertyObjectId, 'VCSSourceSafeINI' , @vchSourceSafeINI , NULL
    exec dbo.dt_setpropertybyid @iPropertyObjectId, 'VCSSQLServer', @@SERVERNAME, NULL
    exec dbo.dt_setpropertybyid @iPropertyObjectId, 'VCSSQLDatabase', @vchDatabaseName, NULL

    if @@error <> 0 GOTO E_General_Error

    commit tran SetProperties
    
    select @iObjectCount = 0;

CleanUp:
    select @vchProjectName
    select @iObjectCount
    return

E_General_Error:
    /* this is an all or nothing.  No specific error messages */
    goto CleanUp

E_OAError:
    exec dbo.dt_displayoaerror @iObjectId, @iReturn
    goto CleanUp




















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_addtosourcecontrol_u]') and xtype = 'P ')  
 drop Procedure dt_addtosourcecontrol_u
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
create proc dbo.dt_addtosourcecontrol_u
    @vchSourceSafeINI nvarchar(255) = '',
    @vchProjectName   nvarchar(255) ='',
    @vchComment       nvarchar(255) ='',
    @vchLoginName     nvarchar(255) ='',
    @vchPassword      nvarchar(255) =''

as
	-- This procedure should no longer be called;  dt_addtosourcecontrol should be called instead.
	-- Calls are forwarded to dt_addtosourcecontrol to maintain backward compatibility
	set nocount on
	exec dbo.dt_addtosourcecontrol 
		@vchSourceSafeINI, 
		@vchProjectName, 
		@vchComment, 
		@vchLoginName, 
		@vchPassword




















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_adduserobject]') and xtype = 'P ')  
 drop Procedure dt_adduserobject
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
/*
**	Add an object to the dtproperties table
*/
create procedure dbo.dt_adduserobject
as
	set nocount on
	/*
	** Create the user object if it does not exist already
	*/
	begin transaction
		insert dbo.dtproperties (property) VALUES ('DtgSchemaOBJECT')
		update dbo.dtproperties set objectid=@@identity 
			where id=@@identity and property='DtgSchemaOBJECT'
	commit
	return @@identity


















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_adduserobject_vcs]') and xtype = 'P ')  
 drop Procedure dt_adduserobject_vcs
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
create procedure dbo.dt_adduserobject_vcs
    @vchProperty varchar(64)

as

set nocount on

declare @iReturn int
    /*
    ** Create the user object if it does not exist already
    */
    begin transaction
        select @iReturn = objectid from dbo.dtproperties where property = @vchProperty
        if @iReturn IS NULL
        begin
            insert dbo.dtproperties (property) VALUES (@vchProperty)
            update dbo.dtproperties set objectid=@@identity
                    where id=@@identity and property=@vchProperty
            select @iReturn = @@identity
        end
    commit
    return @iReturn



















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_checkinobject]') and xtype = 'P ')  
 drop Procedure dt_checkinobject
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
create proc dbo.dt_checkinobject
    @chObjectType  char(4),
    @vchObjectName varchar(255),
    @vchComment    varchar(255)='',
    @vchLoginName  varchar(255),
    @vchPassword   varchar(255)='',
    @iVCSFlags     int = 0,
    @iActionFlag   int = 0,   /* 0 => AddFile, 1 => CheckIn */
    @txStream1     Text = '', /* drop stream   */ /* There is a bug that if items are NULL they do not pass to OLE servers */
    @txStream2     Text = '', /* create stream */
    @txStream3     Text = ''  /* grant stream  */


as

	set nocount on

	declare @iReturn int
	declare @iObjectId int
	select @iObjectId = 0
	declare @iStreamObjectId int

	declare @VSSGUID varchar(100)
	select @VSSGUID = 'SQLVersionControl.VCS_SQL'

	declare @iPropertyObjectId int
	select @iPropertyObjectId  = 0

    select @iPropertyObjectId = (select objectid from dbo.dtproperties where property = 'VCSProjectID')

    declare @vchProjectName   varchar(255)
    declare @vchSourceSafeINI varchar(255)
    declare @vchServerName    varchar(255)
    declare @vchDatabaseName  varchar(255)
    declare @iReturnValue	  int
    declare @pos			  int
    declare @vchProcLinePiece varchar(255)

    
    exec dbo.dt_getpropertiesbyid_vcs @iPropertyObjectId, 'VCSProject',       @vchProjectName   OUT
    exec dbo.dt_getpropertiesbyid_vcs @iPropertyObjectId, 'VCSSourceSafeINI', @vchSourceSafeINI OUT
    exec dbo.dt_getpropertiesbyid_vcs @iPropertyObjectId, 'VCSSQLServer',     @vchServerName    OUT
    exec dbo.dt_getpropertiesbyid_vcs @iPropertyObjectId, 'VCSSQLDatabase',   @vchDatabaseName  OUT

    if @chObjectType = 'PROC'
    begin
        if @iActionFlag = 1
        begin
            /* Procedure Can have up to three streams
            Drop Stream, Create Stream, GRANT stream */

            begin tran compile_all

            /* try to compile the streams */
            exec (@txStream1)
            if @@error <> 0 GOTO E_Compile_Fail

            exec (@txStream2)
            if @@error <> 0 GOTO E_Compile_Fail

            exec (@txStream3)
            if @@error <> 0 GOTO E_Compile_Fail
        end

        exec @iReturn = master.dbo.sp_OACreate @VSSGUID, @iObjectId OUT
        if @iReturn <> 0 GOTO E_OAError

        exec @iReturn = master.dbo.sp_OAGetProperty @iObjectId, 'GetStreamObject', @iStreamObjectId OUT
        if @iReturn <> 0 GOTO E_OAError
        
        if @iActionFlag = 1
        begin
            
            declare @iStreamLength int
			
			select @pos=1
			select @iStreamLength = datalength(@txStream2)
			
			if @iStreamLength > 0
			begin
			
				while @pos < @iStreamLength
				begin
						
					select @vchProcLinePiece = substring(@txStream2, @pos, 255)
					
					exec @iReturn = master.dbo.sp_OAMethod @iStreamObjectId, 'AddStream', @iReturnValue OUT, @vchProcLinePiece
            		if @iReturn <> 0 GOTO E_OAError
            		
					select @pos = @pos + 255
					
				end
            
				exec @iReturn = master.dbo.sp_OAMethod @iObjectId,
														'CheckIn_StoredProcedure',
														NULL,
														@sProjectName = @vchProjectName,
														@sSourceSafeINI = @vchSourceSafeINI,
														@sServerName = @vchServerName,
														@sDatabaseName = @vchDatabaseName,
														@sObjectName = @vchObjectName,
														@sComment = @vchComment,
														@sLoginName = @vchLoginName,
														@sPassword = @vchPassword,
														@iVCSFlags = @iVCSFlags,
														@iActionFlag = @iActionFlag,
														@sStream = ''
                                        
			end
        end
        else
        begin
        
            select colid, text into #ProcLines
            from syscomments
            where id = object_id(@vchObjectName)
            order by colid

            declare @iCurProcLine int
            declare @iProcLines int
            select @iCurProcLine = 1
            select @iProcLines = (select count(*) from #ProcLines)
            while @iCurProcLine <= @iProcLines
            begin
                select @pos = 1
                declare @iCurLineSize int
                select @iCurLineSize = len((select text from #ProcLines where colid = @iCurProcLine))
                while @pos <= @iCurLineSize
                begin                
                    select @vchProcLinePiece = convert(varchar(255),
                        substring((select text from #ProcLines where colid = @iCurProcLine),
                                  @pos, 255 ))
                    exec @iReturn = master.dbo.sp_OAMethod @iStreamObjectId, 'AddStream', @iReturnValue OUT, @vchProcLinePiece
                    if @iReturn <> 0 GOTO E_OAError
                    select @pos = @pos + 255                  
                end
                select @iCurProcLine = @iCurProcLine + 1
            end
            drop table #ProcLines

            exec @iReturn = master.dbo.sp_OAMethod @iObjectId,
													'CheckIn_StoredProcedure',
													NULL,
													@sProjectName = @vchProjectName,
													@sSourceSafeINI = @vchSourceSafeINI,
													@sServerName = @vchServerName,
													@sDatabaseName = @vchDatabaseName,
													@sObjectName = @vchObjectName,
													@sComment = @vchComment,
													@sLoginName = @vchLoginName,
													@sPassword = @vchPassword,
													@iVCSFlags = @iVCSFlags,
													@iActionFlag = @iActionFlag,
													@sStream = ''
        end

        if @iReturn <> 0 GOTO E_OAError

        if @iActionFlag = 1
        begin
            commit tran compile_all
            if @@error <> 0 GOTO E_Compile_Fail
        end

    end

CleanUp:
	return

E_Compile_Fail:
	declare @lerror int
	select @lerror = @@error
	rollback tran compile_all
	RAISERROR (@lerror,16,-1)
	goto CleanUp

E_OAError:
	if @iActionFlag = 1 rollback tran compile_all
	exec dbo.dt_displayoaerror @iObjectId, @iReturn
	goto CleanUp




















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_checkinobject_u]') and xtype = 'P ')  
 drop Procedure dt_checkinobject_u
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
create proc dbo.dt_checkinobject_u
    @chObjectType  char(4),
    @vchObjectName nvarchar(255),
    @vchComment    nvarchar(255)='',
    @vchLoginName  nvarchar(255),
    @vchPassword   nvarchar(255)='',
    @iVCSFlags     int = 0,
    @iActionFlag   int = 0,   /* 0 => AddFile, 1 => CheckIn */
    @txStream1     text = '',  /* drop stream   */ /* There is a bug that if items are NULL they do not pass to OLE servers */
    @txStream2     text = '',  /* create stream */
    @txStream3     text = ''   /* grant stream  */

as	
	-- This procedure should no longer be called;  dt_checkinobject should be called instead.
	-- Calls are forwarded to dt_checkinobject to maintain backward compatibility.
	set nocount on
	exec dbo.dt_checkinobject
		@chObjectType,
		@vchObjectName,
		@vchComment,
		@vchLoginName,
		@vchPassword,
		@iVCSFlags,
		@iActionFlag,   
		@txStream1,		
		@txStream2,		
		@txStream3		




















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_checkoutobject]') and xtype = 'P ')  
 drop Procedure dt_checkoutobject
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
create proc dbo.dt_checkoutobject
    @chObjectType  char(4),
    @vchObjectName varchar(255),
    @vchComment    varchar(255),
    @vchLoginName  varchar(255),
    @vchPassword   varchar(255),
    @iVCSFlags     int = 0,
    @iActionFlag   int = 0/* 0 => Checkout, 1 => GetLatest, 2 => UndoCheckOut */

as

	set nocount on

	declare @iReturn int
	declare @iObjectId int
	select @iObjectId =0

	declare @VSSGUID varchar(100)
	select @VSSGUID = 'SQLVersionControl.VCS_SQL'

	declare @iReturnValue int
	select @iReturnValue = 0

	declare @vchTempText varchar(255)

	/* this is for our strings */
	declare @iStreamObjectId int
	select @iStreamObjectId = 0

    declare @iPropertyObjectId int
    select @iPropertyObjectId = (select objectid from dbo.dtproperties where property = 'VCSProjectID')

    declare @vchProjectName   varchar(255)
    declare @vchSourceSafeINI varchar(255)
    declare @vchServerName    varchar(255)
    declare @vchDatabaseName  varchar(255)
    exec dbo.dt_getpropertiesbyid_vcs @iPropertyObjectId, 'VCSProject',       @vchProjectName   OUT
    exec dbo.dt_getpropertiesbyid_vcs @iPropertyObjectId, 'VCSSourceSafeINI', @vchSourceSafeINI OUT
    exec dbo.dt_getpropertiesbyid_vcs @iPropertyObjectId, 'VCSSQLServer',     @vchServerName    OUT
    exec dbo.dt_getpropertiesbyid_vcs @iPropertyObjectId, 'VCSSQLDatabase',   @vchDatabaseName  OUT

    if @chObjectType = 'PROC'
    begin
        /* Procedure Can have up to three streams
           Drop Stream, Create Stream, GRANT stream */

        exec @iReturn = master.dbo.sp_OACreate @VSSGUID, @iObjectId OUT

        if @iReturn <> 0 GOTO E_OAError

        exec @iReturn = master.dbo.sp_OAMethod @iObjectId,
												'CheckOut_StoredProcedure',
												NULL,
												@sProjectName = @vchProjectName,
												@sSourceSafeINI = @vchSourceSafeINI,
												@sObjectName = @vchObjectName,
												@sServerName = @vchServerName,
												@sDatabaseName = @vchDatabaseName,
												@sComment = @vchComment,
												@sLoginName = @vchLoginName,
												@sPassword = @vchPassword,
												@iVCSFlags = @iVCSFlags,
												@iActionFlag = @iActionFlag

        if @iReturn <> 0 GOTO E_OAError


        exec @iReturn = master.dbo.sp_OAGetProperty @iObjectId, 'GetStreamObject', @iStreamObjectId OUT

        if @iReturn <> 0 GOTO E_OAError

        create table #commenttext (id int identity, sourcecode varchar(255))


        select @vchTempText = 'STUB'
        while @vchTempText is not null
        begin
            exec @iReturn = master.dbo.sp_OAMethod @iStreamObjectId, 'GetStream', @iReturnValue OUT, @vchTempText OUT
            if @iReturn <> 0 GOTO E_OAError
            
            if (@vchTempText = '') set @vchTempText = null
            if (@vchTempText is not null) insert into #commenttext (sourcecode) select @vchTempText
        end

        select 'VCS'=sourcecode from #commenttext order by id
        select 'SQL'=text from syscomments where id = object_id(@vchObjectName) order by colid

    end

CleanUp:
    return

E_OAError:
    exec dbo.dt_displayoaerror @iObjectId, @iReturn
    GOTO CleanUp




















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_checkoutobject_u]') and xtype = 'P ')  
 drop Procedure dt_checkoutobject_u
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
create proc dbo.dt_checkoutobject_u
    @chObjectType  char(4),
    @vchObjectName nvarchar(255),
    @vchComment    nvarchar(255),
    @vchLoginName  nvarchar(255),
    @vchPassword   nvarchar(255),
    @iVCSFlags     int = 0,
    @iActionFlag   int = 0/* 0 => Checkout, 1 => GetLatest, 2 => UndoCheckOut */

as

	-- This procedure should no longer be called;  dt_checkoutobject should be called instead.
	-- Calls are forwarded to dt_checkoutobject to maintain backward compatibility.
	set nocount on
	exec dbo.dt_checkoutobject
		@chObjectType,  
		@vchObjectName, 
		@vchComment,    
		@vchLoginName,  
		@vchPassword,  
		@iVCSFlags,    
		@iActionFlag 




















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_displayoaerror]') and xtype = 'P ')  
 drop Procedure dt_displayoaerror
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.dt_displayoaerror
    @iObject int,
    @iresult int
as

set nocount on

declare @vchOutput      varchar(255)
declare @hr             int
declare @vchSource      varchar(255)
declare @vchDescription varchar(255)

    exec @hr = master.dbo.sp_OAGetErrorInfo @iObject, @vchSource OUT, @vchDescription OUT

    select @vchOutput = @vchSource + ': ' + @vchDescription
    raiserror (@vchOutput,16,-1)

    return



















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_displayoaerror_u]') and xtype = 'P ')  
 drop Procedure dt_displayoaerror_u
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE dbo.dt_displayoaerror_u
    @iObject int,
    @iresult int
as
	-- This procedure should no longer be called;  dt_displayoaerror should be called instead.
	-- Calls are forwarded to dt_displayoaerror to maintain backward compatibility.
	set nocount on
	exec dbo.dt_displayoaerror
		@iObject,
		@iresult




















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_droppropertiesbyid]') and xtype = 'P ')  
 drop Procedure dt_droppropertiesbyid
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
/*
**	Drop one or all the associated properties of an object or an attribute 
**
**	dt_dropproperties objid, null or '' -- drop all properties of the object itself
**	dt_dropproperties objid, property -- drop the property
*/
create procedure dbo.dt_droppropertiesbyid
	@id int,
	@property varchar(64)
as
	set nocount on

	if (@property is null) or (@property = '')
		delete from dbo.dtproperties where objectid=@id
	else
		delete from dbo.dtproperties 
			where objectid=@id and property=@property



















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_dropuserobjectbyid]') and xtype = 'P ')  
 drop Procedure dt_dropuserobjectbyid
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
/*
**	Drop an object from the dbo.dtproperties table
*/
create procedure dbo.dt_dropuserobjectbyid
	@id int
as
	set nocount on
	delete from dbo.dtproperties where objectid=@id


















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_generateansiname]') and xtype = 'P ')  
 drop Procedure dt_generateansiname
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
/* 
**	Generate an ansi name that is unique in the dtproperties.value column 
*/ 
create procedure dbo.dt_generateansiname(@name varchar(255) output) 
as 
	declare @prologue varchar(20) 
	declare @indexstring varchar(20) 
	declare @index integer 
 
	set @prologue = 'MSDT-A-' 
	set @index = 1 
 
	while 1 = 1 
	begin 
		set @indexstring = cast(@index as varchar(20)) 
		set @name = @prologue + @indexstring 
		if not exists (select value from dtproperties where value = @name) 
			break 
		 
		set @index = @index + 1 
 
		if (@index = 10000) 
			goto TooMany 
	end 
 
Leave: 
 
	return 
 
TooMany: 
 
	set @name = 'DIAGRAM' 
	goto Leave 


















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_getobjwithprop]') and xtype = 'P ')  
 drop Procedure dt_getobjwithprop
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
/*
**	Retrieve the owner object(s) of a given property
*/
create procedure dbo.dt_getobjwithprop
	@property varchar(30),
	@value varchar(255)
as
	set nocount on

	if (@property is null) or (@property = '')
	begin
		raiserror('Must specify a property name.',-1,-1)
		return (1)
	end

	if (@value is null)
		select objectid id from dbo.dtproperties
			where property=@property

	else
		select objectid id from dbo.dtproperties
			where property=@property and value=@value


















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_getobjwithprop_u]') and xtype = 'P ')  
 drop Procedure dt_getobjwithprop_u
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
/*
**	Retrieve the owner object(s) of a given property
*/
create procedure dbo.dt_getobjwithprop_u
	@property varchar(30),
	@uvalue nvarchar(255)
as
	set nocount on

	if (@property is null) or (@property = '')
	begin
		raiserror('Must specify a property name.',-1,-1)
		return (1)
	end

	if (@uvalue is null)
		select objectid id from dbo.dtproperties
			where property=@property

	else
		select objectid id from dbo.dtproperties
			where property=@property and uvalue=@uvalue


















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_getpropertiesbyid]') and xtype = 'P ')  
 drop Procedure dt_getpropertiesbyid
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
/*
**	Retrieve properties by id's
**
**	dt_getproperties objid, null or '' -- retrieve all properties of the object itself
**	dt_getproperties objid, property -- retrieve the property specified
*/
create procedure dbo.dt_getpropertiesbyid
	@id int,
	@property varchar(64)
as
	set nocount on

	if (@property is null) or (@property = '')
		select property, version, value, lvalue
			from dbo.dtproperties
			where  @id=objectid
	else
		select property, version, value, lvalue
			from dbo.dtproperties
			where  @id=objectid and @property=property


















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_getpropertiesbyid_u]') and xtype = 'P ')  
 drop Procedure dt_getpropertiesbyid_u
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
/*
**	Retrieve properties by id's
**
**	dt_getproperties objid, null or '' -- retrieve all properties of the object itself
**	dt_getproperties objid, property -- retrieve the property specified
*/
create procedure dbo.dt_getpropertiesbyid_u
	@id int,
	@property varchar(64)
as
	set nocount on

	if (@property is null) or (@property = '')
		select property, version, uvalue, lvalue
			from dbo.dtproperties
			where  @id=objectid
	else
		select property, version, uvalue, lvalue
			from dbo.dtproperties
			where  @id=objectid and @property=property


















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_getpropertiesbyid_vcs]') and xtype = 'P ')  
 drop Procedure dt_getpropertiesbyid_vcs
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
create procedure dbo.dt_getpropertiesbyid_vcs
    @id       int,
    @property varchar(64),
    @value    varchar(255) = NULL OUT

as

    set nocount on

    select @value = (
        select value
                from dbo.dtproperties
                where @id=objectid and @property=property
                )



















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_getpropertiesbyid_vcs_u]') and xtype = 'P ')  
 drop Procedure dt_getpropertiesbyid_vcs_u
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
create procedure dbo.dt_getpropertiesbyid_vcs_u
    @id       int,
    @property varchar(64),
    @value    nvarchar(255) = NULL OUT

as

    -- This procedure should no longer be called;  dt_getpropertiesbyid_vcsshould be called instead.
	-- Calls are forwarded to dt_getpropertiesbyid_vcs to maintain backward compatibility.
	set nocount on
    exec dbo.dt_getpropertiesbyid_vcs
		@id,
		@property,
		@value output



















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_isundersourcecontrol]') and xtype = 'P ')  
 drop Procedure dt_isundersourcecontrol
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
create proc dbo.dt_isundersourcecontrol
    @vchLoginName varchar(255) = '',
    @vchPassword  varchar(255) = '',
    @iWhoToo      int = 0 /* 0 => Just check project; 1 => get list of objs */

as

	set nocount on

	declare @iReturn int
	declare @iObjectId int
	select @iObjectId = 0

	declare @VSSGUID varchar(100)
	select @VSSGUID = 'SQLVersionControl.VCS_SQL'

	declare @iReturnValue int
	select @iReturnValue = 0

	declare @iStreamObjectId int
	select @iStreamObjectId   = 0

	declare @vchTempText varchar(255)

    declare @iPropertyObjectId int
    select @iPropertyObjectId = (select objectid from dbo.dtproperties where property = 'VCSProjectID')

    declare @vchProjectName   varchar(255)
    declare @vchSourceSafeINI varchar(255)
    declare @vchServerName    varchar(255)
    declare @vchDatabaseName  varchar(255)
    exec dbo.dt_getpropertiesbyid_vcs @iPropertyObjectId, 'VCSProject',       @vchProjectName   OUT
    exec dbo.dt_getpropertiesbyid_vcs @iPropertyObjectId, 'VCSSourceSafeINI', @vchSourceSafeINI OUT
    exec dbo.dt_getpropertiesbyid_vcs @iPropertyObjectId, 'VCSSQLServer',     @vchServerName    OUT
    exec dbo.dt_getpropertiesbyid_vcs @iPropertyObjectId, 'VCSSQLDatabase',   @vchDatabaseName  OUT

    if (@vchProjectName = '')	set @vchProjectName		= null
    if (@vchSourceSafeINI = '') set @vchSourceSafeINI	= null
    if (@vchServerName = '')	set @vchServerName		= null
    if (@vchDatabaseName = '')	set @vchDatabaseName	= null
    
    if (@vchProjectName is null) or (@vchSourceSafeINI is null) or (@vchServerName is null) or (@vchDatabaseName is null)
    begin
        RAISERROR('Not Under Source Control',16,-1)
        return
    end

    if @iWhoToo = 1
    begin

        /* Get List of Procs in the project */
        exec @iReturn = master.dbo.sp_OACreate @VSSGUID, @iObjectId OUT
        if @iReturn <> 0 GOTO E_OAError

        exec @iReturn = master.dbo.sp_OAMethod @iObjectId,
												'GetListOfObjects',
												NULL,
												@vchProjectName,
												@vchSourceSafeINI,
												@vchServerName,
												@vchDatabaseName,
												@vchLoginName,
												@vchPassword

        if @iReturn <> 0 GOTO E_OAError

        exec @iReturn = master.dbo.sp_OAGetProperty @iObjectId, 'GetStreamObject', @iStreamObjectId OUT

        if @iReturn <> 0 GOTO E_OAError

        create table #ObjectList (id int identity, vchObjectlist varchar(255))

        select @vchTempText = 'STUB'
        while @vchTempText is not null
        begin
            exec @iReturn = master.dbo.sp_OAMethod @iStreamObjectId, 'GetStream', @iReturnValue OUT, @vchTempText OUT
            if @iReturn <> 0 GOTO E_OAError
            
            if (@vchTempText = '') set @vchTempText = null
            if (@vchTempText is not null) insert into #ObjectList (vchObjectlist ) select @vchTempText
        end

        select vchObjectlist from #ObjectList order by id
    end

CleanUp:
    return

E_OAError:
    exec dbo.dt_displayoaerror @iObjectId, @iReturn
    goto CleanUp




















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_isundersourcecontrol_u]') and xtype = 'P ')  
 drop Procedure dt_isundersourcecontrol_u
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
create proc dbo.dt_isundersourcecontrol_u
    @vchLoginName nvarchar(255) = '',
    @vchPassword  nvarchar(255) = '',
    @iWhoToo      int = 0 /* 0 => Just check project; 1 => get list of objs */

as
	-- This procedure should no longer be called;  dt_isundersourcecontrol should be called instead.
	-- Calls are forwarded to dt_isundersourcecontrol to maintain backward compatibility.
	set nocount on
	exec dbo.dt_isundersourcecontrol
		@vchLoginName,
		@vchPassword,
		@iWhoToo 




















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_removefromsourcecontrol]') and xtype = 'P ')  
 drop Procedure dt_removefromsourcecontrol
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
create procedure dbo.dt_removefromsourcecontrol

as

    set nocount on

    declare @iPropertyObjectId int
    select @iPropertyObjectId = (select objectid from dbo.dtproperties where property = 'VCSProjectID')

    exec dbo.dt_droppropertiesbyid @iPropertyObjectId, null

    /* -1 is returned by dt_droppopertiesbyid */
    if @@error <> 0 and @@error <> -1 return 1

    return 0




















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_setpropertybyid]') and xtype = 'P ')  
 drop Procedure dt_setpropertybyid
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
/*
**	If the property already exists, reset the value; otherwise add property
**		id -- the id in sysobjects of the object
**		property -- the name of the property
**		value -- the text value of the property
**		lvalue -- the binary value of the property (image)
*/
create procedure dbo.dt_setpropertybyid
	@id int,
	@property varchar(64),
	@value varchar(255),
	@lvalue image
as
	set nocount on
	declare @uvalue nvarchar(255) 
	set @uvalue = convert(nvarchar(255), @value) 
	if exists (select * from dbo.dtproperties 
			where objectid=@id and property=@property)
	begin
		--
		-- bump the version count for this row as we update it
		--
		update dbo.dtproperties set value=@value, uvalue=@uvalue, lvalue=@lvalue, version=version+1
			where objectid=@id and property=@property
	end
	else
	begin
		--
		-- version count is auto-set to 0 on initial insert
		--
		insert dbo.dtproperties (property, objectid, value, uvalue, lvalue)
			values (@property, @id, @value, @uvalue, @lvalue)
	end



















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_setpropertybyid_u]') and xtype = 'P ')  
 drop Procedure dt_setpropertybyid_u
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
/*
**	If the property already exists, reset the value; otherwise add property
**		id -- the id in sysobjects of the object
**		property -- the name of the property
**		uvalue -- the text value of the property
**		lvalue -- the binary value of the property (image)
*/
create procedure dbo.dt_setpropertybyid_u
	@id int,
	@property varchar(64),
	@uvalue nvarchar(255),
	@lvalue image
as
	set nocount on
	-- 
	-- If we are writing the name property, find the ansi equivalent. 
	-- If there is no lossless translation, generate an ansi name. 
	-- 
	declare @avalue varchar(255) 
	set @avalue = null 
	if (@uvalue is not null) 
	begin 
		if (convert(nvarchar(255), convert(varchar(255), @uvalue)) = @uvalue) 
		begin 
			set @avalue = convert(varchar(255), @uvalue) 
		end 
		else 
		begin 
			if 'DtgSchemaNAME' = @property 
			begin 
				exec dbo.dt_generateansiname @avalue output 
			end 
		end 
	end 
	if exists (select * from dbo.dtproperties 
			where objectid=@id and property=@property)
	begin
		--
		-- bump the version count for this row as we update it
		--
		update dbo.dtproperties set value=@avalue, uvalue=@uvalue, lvalue=@lvalue, version=version+1
			where objectid=@id and property=@property
	end
	else
	begin
		--
		-- version count is auto-set to 0 on initial insert
		--
		insert dbo.dtproperties (property, objectid, value, uvalue, lvalue)
			values (@property, @id, @avalue, @uvalue, @lvalue)
	end


















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_validateloginparams]') and xtype = 'P ')  
 drop Procedure dt_validateloginparams
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
create proc dbo.dt_validateloginparams
    @vchLoginName  varchar(255),
    @vchPassword   varchar(255)
as

set nocount on

declare @iReturn int
declare @iObjectId int
select @iObjectId =0

declare @VSSGUID varchar(100)
select @VSSGUID = 'SQLVersionControl.VCS_SQL'

    declare @iPropertyObjectId int
    select @iPropertyObjectId = (select objectid from dbo.dtproperties where property = 'VCSProjectID')

    declare @vchSourceSafeINI varchar(255)
    exec dbo.dt_getpropertiesbyid_vcs @iPropertyObjectId, 'VCSSourceSafeINI', @vchSourceSafeINI OUT

    exec @iReturn = master.dbo.sp_OACreate @VSSGUID, @iObjectId OUT
    if @iReturn <> 0 GOTO E_OAError

    exec @iReturn = master.dbo.sp_OAMethod @iObjectId,
											'ValidateLoginParams',
											NULL,
											@sSourceSafeINI = @vchSourceSafeINI,
											@sLoginName = @vchLoginName,
											@sPassword = @vchPassword
    if @iReturn <> 0 GOTO E_OAError

CleanUp:
    return

E_OAError:
    exec dbo.dt_displayoaerror @iObjectId, @iReturn
    GOTO CleanUp




















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_validateloginparams_u]') and xtype = 'P ')  
 drop Procedure dt_validateloginparams_u
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
create proc dbo.dt_validateloginparams_u
    @vchLoginName  nvarchar(255),
    @vchPassword   nvarchar(255)
as

	-- This procedure should no longer be called;  dt_validateloginparams should be called instead.
	-- Calls are forwarded to dt_validateloginparams to maintain backward compatibility.
	set nocount on
	exec dbo.dt_validateloginparams
		@vchLoginName,
		@vchPassword 




















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_vcsenabled]') and xtype = 'P ')  
 drop Procedure dt_vcsenabled
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
create proc dbo.dt_vcsenabled

as

set nocount on

declare @iObjectId int
select @iObjectId = 0

declare @VSSGUID varchar(100)
select @VSSGUID = 'SQLVersionControl.VCS_SQL'

    declare @iReturn int
    exec @iReturn = master.dbo.sp_OACreate @VSSGUID, @iObjectId OUT
    if @iReturn <> 0 raiserror('', 16, -1) /* Can't Load Helper DLLC */




















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_verstamp006]') and xtype = 'P ')  
 drop Procedure dt_verstamp006
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
/*
**	This procedure returns the version number of the stored
**    procedures used by legacy versions of the Microsoft
**	Visual Database Tools.  Version is 7.0.00.
*/
create procedure dbo.dt_verstamp006
as
	select 7000


















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_verstamp007]') and xtype = 'P ')  
 drop Procedure dt_verstamp007
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
/*
**	This procedure returns the version number of the stored
**    procedures used by the the Microsoft Visual Database Tools.
**	Version is 7.0.05.
*/
create procedure dbo.dt_verstamp007
as
	select 7005

















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_whocheckedout]') and xtype = 'P ')  
 drop Procedure dt_whocheckedout
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
create proc dbo.dt_whocheckedout
        @chObjectType  char(4),
        @vchObjectName varchar(255),
        @vchLoginName  varchar(255),
        @vchPassword   varchar(255)

as

set nocount on

declare @iReturn int
declare @iObjectId int
select @iObjectId =0

declare @VSSGUID varchar(100)
select @VSSGUID = 'SQLVersionControl.VCS_SQL'

    declare @iPropertyObjectId int

    select @iPropertyObjectId = (select objectid from dbo.dtproperties where property = 'VCSProjectID')

    declare @vchProjectName   varchar(255)
    declare @vchSourceSafeINI varchar(255)
    declare @vchServerName    varchar(255)
    declare @vchDatabaseName  varchar(255)
    exec dbo.dt_getpropertiesbyid_vcs @iPropertyObjectId, 'VCSProject',       @vchProjectName   OUT
    exec dbo.dt_getpropertiesbyid_vcs @iPropertyObjectId, 'VCSSourceSafeINI', @vchSourceSafeINI OUT
    exec dbo.dt_getpropertiesbyid_vcs @iPropertyObjectId, 'VCSSQLServer',     @vchServerName    OUT
    exec dbo.dt_getpropertiesbyid_vcs @iPropertyObjectId, 'VCSSQLDatabase',   @vchDatabaseName  OUT

    if @chObjectType = 'PROC'
    begin
        exec @iReturn = master.dbo.sp_OACreate @VSSGUID, @iObjectId OUT

        if @iReturn <> 0 GOTO E_OAError

        declare @vchReturnValue varchar(255)
        select @vchReturnValue = ''

        exec @iReturn = master.dbo.sp_OAMethod @iObjectId,
												'WhoCheckedOut',
												@vchReturnValue OUT,
												@sProjectName = @vchProjectName,
												@sSourceSafeINI = @vchSourceSafeINI,
												@sObjectName = @vchObjectName,
												@sServerName = @vchServerName,
												@sDatabaseName = @vchDatabaseName,
												@sLoginName = @vchLoginName,
												@sPassword = @vchPassword

        if @iReturn <> 0 GOTO E_OAError

        select @vchReturnValue

    end

CleanUp:
    return

E_OAError:
    exec dbo.dt_displayoaerror @iObjectId, @iReturn
    GOTO CleanUp




















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dt_whocheckedout_u]') and xtype = 'P ')  
 drop Procedure dt_whocheckedout_u
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
create proc dbo.dt_whocheckedout_u
        @chObjectType  char(4),
        @vchObjectName nvarchar(255),
        @vchLoginName  nvarchar(255),
        @vchPassword   nvarchar(255)

as

	-- This procedure should no longer be called;  dt_whocheckedout should be called instead.
	-- Calls are forwarded to dt_whocheckedout to maintain backward compatibility.
	set nocount on
	exec dbo.dt_whocheckedout
		@chObjectType, 
		@vchObjectName,
		@vchLoginName, 
		@vchPassword  




















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_DocketPrint]') and xtype = 'P ')  
 drop Procedure sp_DocketPrint
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go


 






-- exec sp_DocketPrint 714740
-- exec sp_DocketPrint 97837

CREATE   proc [dbo].[sp_DocketPrint] @OH_ID int
as
SET NOCOUNT ON;

declare @tr_db varchar(50),
        @sql_str varchar(5000),
        @oh_status varchar(1),
        @ReprintMessage varchar(20),
        @RunLengthLW char(1),
        @Min_pi_size_value float,
        @Min_pi_size varchar(12),
        @Max_pi_size_value float,
        @Max_pi_size varchar(12),
        @Act_Dim1 varchar(12),
        @Act_Dim2 varchar(12),
        @D_Width varchar(12),
        @D_Length varchar(12),
        @ACTUAL_SIZE varchar(25),
        @Q_ID int,
        @Q_QType varchar(5),
        @StockDesc varchar(5000),
        @WindDir varchar(40)        

select @tr_db=tr_db from database_setup

SELECT @oh_status = DH.oh_status, @D_Width = QH.Q_Width, @D_Length = QH.Q_Length, @RunLengthLW = DT.RunLengthLW,
  @Q_ID = DH.Q_ID, @Q_QType = Q_QType, 
  @WindDir =
CASE ISNULL(QH.Q_RLAB_WindDirNotRequestedTF, 'F')
WHEN 'F' THEN 
CASE Q_RLAB_WindDir
WHEN 0 THEN 'Not Specified'
WHEN 1 THEN 'Top First'
WHEN 2 THEN 'Bottom First'
WHEN 3 THEN 'Right First'
WHEN 4 THEN 'Left First'
END  
ELSE 'Not Requested by Dealer'
END
FROM D_Head DH
LEFT JOIN Q_Head QH ON QH.Q_ID = DH.Q_ID
LEFT JOIN C_DocketType DT on DT.DktType = DH.oh_type
WHERE DH.oh_id = @OH_ID

if  ((@Q_QType = 'RLAB') OR (@Q_QType = 'DLAB'))
begin
  set @StockDesc = ''
--  select @StockDesc = PS.StockDesc
--  from Q_Parts QP
--  left join CB_PaperSpec PS on PS.PaperSpecID = QP.PaperSpecID
--  WHERE QP.Q_ID=@Q_ID AND QP.QG_NUM=1 
end
else
begin
  set @StockDesc = ''
  set @WindDir = ''
end




select @Min_pi_size_value = MIN(CBF.FractionValue)
from D_Parts DP
left join CB_Fraction CBF ON CBF.FractionString = DP.pi_size
where DP.oh_ID= @OH_ID

select @Min_pi_size = FractionString
from CB_Fraction
where FractionValue = @Min_pi_size_value

select @Max_pi_size_value = MAX(CBF.FractionValue)
from D_Parts DP
left join CB_Fraction CBF ON CBF.FractionString = DP.pi_size
where DP.oh_ID= @OH_ID

select @Max_pi_size = FractionString
from CB_Fraction
where FractionValue = @Max_pi_size_value

if (@RunLengthLW = 'W')
begin
  set @Act_Dim1 = @D_Width

  if  (@Min_pi_size = @Max_pi_size)
    set @Act_Dim2 = @Min_pi_size
  else
    set @Act_Dim2 = 'VAR'
end
else
begin
  if  (@Min_pi_size = @Max_pi_size)
    set @Act_Dim1 = @Min_pi_size
  else
    set @Act_Dim1 = 'VAR'

  set @Act_Dim2 = @D_Length
end

set @ACTUAL_SIZE = @Act_Dim1 + ' x ' + @Act_Dim2

if  (@oh_status = 'Y')
  set @ReprintMessage = ''
else
  set @ReprintMessage = 'Re-Printed'

set @sql_str='
SELECT VC.CUSTOMER_ID, VC.CUSTOMER_CODE, UPPER(VC.NAME) AS NAME, UPPER(VC.BILL_ADDRESS_1) AS BILL_ADDRESS_1, UPPER(VC.BILL_ADDRESS_2) AS BILL_ADDRESS_2,
 UPPER(VC.BILL_ADDRESS_3) AS BILL_ADDRESS_3, UPPER(VC.BILL_CITY) AS BILL_CITY, UPPER(VC.BILL_STATE) AS BILL_STATE, UPPER(VC.BILL_ZIP) AS BILL_ZIP,
 VC.TELEPHONE, VC.FAX, T.TERRITORY_ID, UPPER(T.TERRITORY_CODE) AS TERRITORY_CODE, UPPER(T.TERRITORY_DESC) AS TERRITORY_DESC,
 UPPER(VSM_IN.FIRSTNAME) AS VSM_INSIDE_SMAN_NAME, UPPER(VSM_BY.FIRSTNAME) AS VSM_TAKENBY_SMAN_NAME,
 RTRIM(QH.Q_Whs) + '' '' +  LTRIM(STR(QH.Q_Num)) AS QUOTE_NUMBER,
 qh.*, dh.*, qg.*, dp.*, cb.*, COV_CB.ShortDesc as PrintedCoverPaperDesc, WHS.PHONE2 AS SalesPersonPhone, ''' + @ReprintMessage + ''' as ReprintMessage,
 CAST(''' + @ACTUAL_SIZE + ''' AS varchar(25)) AS ACTUAL_SIZE, ''' +  @WindDir + ''' as WindDirection,
CASE ISNULL(QH.Q_RLAB_WindDirNotRequestedTF, ''F'')
WHEN ''F'' THEN
 CASE QH.Q_RLAB_WindDir
WHEN 1 THEN LS.LBC_WindTopFirstImg
WHEN 2 THEN LS.LBC_WindBottomFirstImg
WHEN 3 THEN LS.LBC_WindRightFirstImg
WHEN 4 THEN LS.LBC_WindLeftFirstImg
ELSE null
END
ELSE null
END
 AS WindDirectionImage,
UPPER(QG.QG_Desc) AS QG_Desc_UC
  FROM D_Head DH
  LEFT OUTER JOIN D_PARTS DP on DH.OH_ID=DP.OH_ID
  LEFT OUTER JOIN CB_PaperSpec COV_CB on DH.ohp_PaperSpecID=COV_CB.PaperSpecID
  LEFT OUTER JOIN CB_PaperSpec CB on DP.PaperSpecID=CB.PaperSpecID
  LEFT OUTER JOIN Q_HEAD QH ON DH.Q_ID = QH.Q_ID 
  LEFT OUTER JOIN Q_GROUP QG ON DH.QG_ID = QG.QG_ID
  LEFT OUTER JOIN '+@tr_db+'..SALESPERSONS VSM_BY ON QH.SLS_ID = VSM_BY.SLS_ID
  LEFT OUTER JOIN '+@tr_db+'..SALESPERSONS VSM_IN ON DH.SLS_ID_Inside  = VSM_IN.SLS_ID
  LEFT OUTER JOIN '+@tr_db+'..customers vc ON QH.Customer_ID = VC.CUSTOMER_ID
  LEFT OUTER JOIN '+@tr_db+'..territory t ON T.TERRITORY_ID=VC.TERRITORY_ID
  LEFT JOIN '+@tr_db+'..WAREHOUSE WHS ON WHS.WHSE_ID=VSM_BY.WHSE_ID
  LEFT JOIN CB_LabelSetup LS ON (LS.LBC_Key = ''ROLL'') 
where dh.oh_id='+cast(@OH_ID as varchar(50))

--select @sql_str
exec(@sql_str)









































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_DocketPrintOptions]') and xtype = 'P ')  
 drop Procedure sp_DocketPrintOptions
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go


 






-- exec sp_DocketPrintOptions 687222
-- exec sp_DocketPrintOptions 398456

CREATE   proc [dbo].[sp_DocketPrintOptions] @OH_ID int
as

declare
  @Q_ID int,
  @QType varchar(5),
  @QWindDir varchar(40)

SET NOCOUNT ON;

SELECT @Q_ID = Q_ID FROM D_Head WHERE oh_ID=@OH_ID

SELECT @QType = Q_QType,
 @QWindDir = 
CASE ISNULL(Q_RLAB_WindDirNotRequestedTF, 'F')
WHEN 'F' THEN
CASE Q_RLAB_WindDir
WHEN 0 THEN 'Not Specified'
WHEN 1 THEN 'Top First'
WHEN 2 THEN 'Bottom First'
WHEN 3 THEN 'Right First'
WHEN 4 THEN 'Left First'
END
ELSE 'Not Requested by Dealer'
END
FROM Q_Head WHERE Q_ID=@Q_ID

if ((@QType = 'RLAB') OR (@QType = 'DLAB'))
begin
    SELECT DH.OH_ID, OG.OG_Desc, OGI.OGI_Desc,
    QO.ValueDesc, OG.QG_SortRpt, OGI.OGI_Sort as QG_SortRpt2, OG.OG_Desc + ' ' + OGI.OGI_Desc + '   ' + QO.ValueDesc as Option_1Line_Desc
    FROM D_Head DH
    LEFT JOIN Q_Head QH ON QH.Q_ID = DH.Q_ID
    LEFT JOIN Q_Options QO ON QO.Q_ID = QH.Q_ID
    LEFT JOIN CQ_OptGrpItm OGI ON OGI.OGI_ID = QO.OGI_ID
    LEFT JOIN CQ_OptGrp OG ON OG.OG_ID = OGI.OG_ID
    WHERE (OG.OG_Column=2 OR OG.OG_Column=3) AND DH.oh_id = @OH_ID
    UNION
    SELECT @OH_ID, 'Wind Direction' as QG_Desc, @QWindDir as OGI_Desc, null as ValueDesc, 99999 as QG_SortRpt, 99999 as QG_SortRpt2, 
     'Wind Direction ' + @QWindDir as Option_1Line_Desc
    ORDER BY OG.QG_SortRpt, OGI.OGI_Sort
end
else
begin
    SELECT DH.OH_ID, OG.OG_Desc, OGI.OGI_Desc,
    QO.ValueDesc, OG.QG_SortRpt, OGI.OGI_Sort as QG_SortRpt2, OG.OG_Desc + ' ' + OGI.OGI_Desc + '   ' + QO.ValueDesc as Option_1Line_Desc
    FROM D_Head DH
    LEFT JOIN Q_Head QH ON QH.Q_ID = DH.Q_ID
    LEFT JOIN Q_Options QO ON QO.Q_ID = QH.Q_ID
    LEFT JOIN CQ_OptGrpItm OGI ON OGI.OGI_ID = QO.OGI_ID
    LEFT JOIN CQ_OptGrp OG ON OG.OG_ID = OGI.OG_ID
    WHERE (OG.OG_Column=2 OR OG.OG_Column=3) AND DH.oh_id = @OH_ID
    ORDER BY OG.QG_SortRpt

end











Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_DsGoldEmp]') and xtype = 'P ')  
 drop Procedure sp_DsGoldEmp
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go



-- =============================================
-- Description:	Generate Dataset for Goldenrod Employee Lookup
--
-- Gets Data from:
-- D_PY_History      -Piece Work released to Payroll
-- D_PY_Integration  -Piece Work Imported from D_BC_Transactions (Download Function)
-- D_BC_Transactions -Scanned but not 'Downloaded' into D_PY_Integration yet 
--
-- Parameter MaxPayPeriodNum is number of Unique Dates for which Records were exported to Payroll
-- (0 =UnExported, 1 =Last Pay Period,  rest are history in D_PY_History)
-- =============================================
-- sp_DsGoldEmp 104, 1, 'P', '2008-06-09 00:00:00.000', '2008-06-09 23:59:59.999'
-- sp_DsGoldEmp 104, 1, 'S', '2008-06-09 00:00:00.000', '2008-06-09 23:59:59.999'
-- sp_DsGoldEmp 206, 1, 'P'
-- sp_DsGoldEmp 825
-- sp_DsGoldEmp
CREATE PROCEDURE [dbo].[sp_DsGoldEmp] 
	@Emp_No int = 206,
	@MaxPayPeriodNum int = 1,
    @ByPeriod_or_Scandate varchar(1) = 'P',
    @ScanFromDate datetime = NULL,
    @ScanToDate datetime = NULL
AS
BEGIN
	SET NOCOUNT ON;

    if  (@ScanFromDate IS NULL)
      set @ScanFromDate = getdate()
    if  (@ScanToDate IS NULL)
      set @ScanToDate = dateadd(day, 1, getdate())


    declare @hr_db varchar(50), @sql varchar(5000), @emp_name varchar(70), @emp_long_name varchar(80),
            @C_PayRate decimal(17,4)

    select @hr_db=isnull(hr_db,'') from database_setup

DECLARE @TMP1 TABLE
(
	Name varchar(70),
    LongName varchar(80)    
)

    set @sql= '
      SELECT emp_first_name + '' '' + emp_last_name,
      emp_first_name + '' '' +
      CASE WHEN (ISNULL(emp_initial,'''') <> '''') THEN emp_initial + '' '' ELSE '''' END +
      emp_last_name
      FROM ' + @hr_db + '..employee
      WHERE emp_no=' + CAST(@Emp_No AS varchar(10))

    INSERT @TMP1
    exec(@sql)

    SELECT @emp_name = Name FROM @TMP1
    SELECT @emp_long_name = LongName FROM @TMP1

    DELETE Working_PayPeriodNum
    WHERE emp_no = @Emp_No

	INSERT Working_PayPeriodNum (emp_no, et_date)
       SELECT @Emp_No, et_date
	           FROM D_PY_History
	           WHERE Emp_No = @Emp_No
               GROUP BY et_date
	           ORDER BY et_date DESC
	
    DECLARE @RNum int
    SET @RNum = 0

    UPDATE Working_PayPeriodNum
	SET @RNum = PayPeriodNum = @RNum +1
	WHERE Emp_No = @Emp_No

--	SELECT * FROM Working_PayPeriodNum

	SELECT @emp_name as Name,  @emp_long_name as LongName, 3 AS OrderSeq, 'Released' AS PayStatus, Pct_Complete, Setup_Hrs, Run_hrs, Total_Hrs, PY_Rate,
    CASE WHEN Is_Press='T' THEN 'Press' ELSE 'Collate' END AS MachineType, PYH.Entry_Date AS ScanDate,
    CASE WHEN PYH.Manual_Entry='T' THEN 'Added' ELSE 'Wanded' END AS Wanded_Added,
	PY_Calc_Amount, DTMP.et_date AS PayDate,
	DH.oh_type, DH.oh_num, DH.oh_fold_YN as Fold,
    QH.Q_ClientName, QH.Q_Width + ' x ' + QH.Q_Length AS SIZE, QH.Q_DblWide AS RunWide, 
    CASE QH.Q_QE_YN  WHEN 'Y' THEN 'QE' WHEN 'N' THEN 'Custom' ELSE '' END AS Pricing,
    QG.QG_Desc, QG.QG_Qty, QG.QG_Parts, QG.QG_NumPlates,
	DTMP.PayPeriodNum, ISNULL(PYH.Adj_Amount, 0.0) AS ADJ_AMOUNT
    FROM D_PY_History PYH LEFT OUTER JOIN
    D_Head DH ON  DH.oh_ID = PYH.Docket_ID LEFT OUTER JOIN
	Q_Head QH ON  QH.Q_ID = DH.Q_ID LEFT OUTER JOIN
	Q_Group QG ON QG.QG_ID = DH.QG_ID LEFT OUTER JOIN
	Working_PayPeriodNum DTMP ON (PYH.et_date = DTMP.et_date) AND (PYH.Emp_No = DTMP.emp_no)
	WHERE PYH.Emp_No = @Emp_No
    AND ( (@ByPeriod_or_Scandate = 'P' AND (DTMP.PayPeriodNum <= @MaxPayPeriodNum))
           OR 
          ((PYH.Entry_Date >= @ScanFromDate) AND (PYH.Entry_Date <= @ScanToDate))
        )
	UNION

	SELECT @emp_name as Name,  @emp_long_name as LongName, 2 AS OrderSeq, 'In Review' AS PayStatus, Pct_Complete, Setup_Hrs, Run_hrs, Total_Hrs, PY_Rate,
    CASE WHEN Is_Press='T' THEN 'Press' ELSE 'Collate' END AS MachineType, PYH.Entry_Date AS ScanDate,
    CASE WHEN PYH.Manual_Entry='T' THEN 'Added' ELSE 'Wanded' END AS Wanded_Added,
	PY_Calc_Amount, NULL AS PayDate,
	DH.oh_type, DH.oh_num, DH.oh_fold_YN as Fold,
    QH.Q_ClientName, QH.Q_Width + ' x ' + QH.Q_Length AS SIZE, QH.Q_DblWide AS RunWide, 
    CASE QH.Q_QE_YN  WHEN 'Y' THEN 'QE' WHEN 'N' THEN 'Custom' ELSE '' END AS Pricing,
    QG.QG_Desc, QG.QG_Qty, QG.QG_Parts, QG.QG_NumPlates,
	0 AS PayPeriodNum, ISNULL(PYH.Adj_Amount, 0.0) AS ADJ_AMOUNT
	FROM D_PY_Integration PYH LEFT OUTER JOIN
    D_Head DH ON  DH.oh_ID = PYH.Docket_ID LEFT OUTER JOIN
	Q_Head QH ON  QH.Q_ID = DH.Q_ID LEFT OUTER JOIN
	Q_Group QG ON QG.QG_ID = DH.QG_ID
	WHERE PYH.Emp_No = @Emp_No
    AND ( (@ByPeriod_or_Scandate = 'P')
           OR 
          ((PYH.Entry_Date >= @ScanFromDate) AND (PYH.Entry_Date <= @ScanToDate))
        )

	UNION

	SELECT @emp_name as Name,  @emp_long_name as LongName, 1 AS OrderSeq, 'Scanned' AS PayStatus, Pct_Complete,
	CASE WHEN STAT.Is_Press='T' THEN QG.QG_GoldPress_SetupHr ELSE QG.QG_GoldCollate_SetupHr END  AS Setup_Hrs,
	CASE WHEN STAT.Is_Press='T' THEN QG.QG_GoldPress_RunHr   ELSE QG.QG_GoldCollate_RunHr   END  AS Run_hrs,
    CASE WHEN STAT.Is_Press='T' THEN round(QG.QG_GoldPress_SetupHr + QG.QG_GoldPress_RunHr, 3) ELSE
                                     round(QG.QG_GoldCollate_SetupHr + QG.QG_GoldCollate_RunHr, 3) END AS Total_Hrs,
	SWIPE.HrPayRate AS PY_Rate,
    CASE WHEN STAT.Is_Press='T' THEN 'Press' ELSE 'Collate' END AS MachineType, TX.Scan_Date AS ScanDate,
    CASE WHEN TX.Terminal_No='MANUAL' THEN 'Added' ELSE 'Wanded' END AS Wanded_Added,

    SWIPE.HrPayRate *
    (CASE WHEN STAT.Is_Press='T' THEN round(QG.QG_GoldPress_SetupHr + QG.QG_GoldPress_RunHr, 3) ELSE
                                     round(QG.QG_GoldCollate_SetupHr + QG.QG_GoldCollate_RunHr, 3) END) *
    Pct_Complete / 100
      As PY_Calc_Amount,
    NULL AS PayDate,
	DH.oh_type, DH.oh_num, DH.oh_fold_YN as Fold,
    QH.Q_ClientName, QH.Q_Width + ' x ' + QH.Q_Length AS SIZE, QH.Q_DblWide AS RunWide, 
    CASE QH.Q_QE_YN  WHEN 'Y' THEN 'QE' WHEN 'N' THEN 'Custom' ELSE '' END AS Pricing,
    QG.QG_Desc, QG.QG_Qty, QG.QG_Parts, QG.QG_NumPlates,
	0 AS PayPeriodNum, 0.00 AS ADJ_AMOUNT
	FROM D_BC_Transactions TX LEFT OUTER JOIN
    D_Head DH ON  DH.oh_ID = TX.Docket_ID LEFT OUTER JOIN
	Q_Head QH ON  QH.Q_ID = DH.Q_ID LEFT OUTER JOIN
	Q_Group QG ON QG.QG_ID = DH.QG_ID LEFT OUTER JOIN
	D_StatusBadgeSwipe_Ctrl SWIPE ON SWIPE.Swipe_Code = TX.Swipe_Code LEFT OUTER JOIN
	C_Status_Ctrl STAT ON STAT.Status_Code = SWIPE.Status_Code
	WHERE TX.Emp_No = @Emp_No
    AND TX.exported_py='F' and TX.status_code in (select status_code from c_status_ctrl where is_press='T' or is_collate='T')
    AND ( (@ByPeriod_or_Scandate = 'P')
           OR 
          ((TX.Scan_Date >= @ScanFromDate) AND (TX.Scan_Date <= @ScanToDate))
        )

	ORDER BY PayPeriodNum, OrderSeq, DH.oh_type, DH.oh_num
 
END























































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_EmpBadgeSwipes]') and xtype = 'P ')  
 drop Procedure sp_EmpBadgeSwipes
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
create procedure sp_EmpBadgeSwipes
as
declare
@hr_db varchar(50)
set @hr_db='hr_factor'

declare
@sql varchar(4000)

set @sql='select e.emp_first_name, e.emp_last_name, e.badge_num, b.swipe_code, b.status_code, s.status_desc
from '+@hr_db+'..employee e 
join d_statusbadgeswipe_ctrl b on b.emp_badge_num=e.badge_num
join c_status_ctrl s on s.status_code=b.status_code'

exec (@sql)
























































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_ExportToPayroll]') and xtype = 'P ')  
 drop Procedure sp_ExportToPayroll
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE procedure [dbo].[sp_ExportToPayroll]
@username varchar(10)
as

declare
@hr_db varchar(50),
@sql varchar(5000),
@result varchar(100)

set @result = 'OK'

select @hr_db=isnull(hr_db,'') from database_setup

UPDATE working_py_integration SET Paid_Hours = PY_Calc_Amount/PY_Rate

begin tran

set @sql = 
  'insert ' + @hr_db + '..EMP_TIME_BARCODE_HEADER
  (      EMP_NO, ENTRY_DATE, TOTAL_TIME, REG_HRS,     OT_HRS, DT_HRS, SHIFT_ID,
  APPROVED, ACTUAL_TIME_IN, TIME_IN, OPERATOR) 
  select Emp_No, Entry_Date, Paid_Hours,  Paid_Hours, 0,      0,      1,
  ''N'',    Entry_Date,     Entry_Date, ''' + @username + '''
  from working_py_integration where (PY_Selected = ''T'') AND (Paid_Hours > 0)'

exec(@sql)

if  @@error <> 0
  set @result = 'Error Inserting'

if @result = 'OK'
begin
  delete D_PY_Integration
  where PK_ID in (select PK_ID from working_py_integration where PY_Selected='T')
  if (@@error <> 0)
    set @result = 'Error Deleting from D_py_integration'
end;

if @result = 'OK'
begin
  delete working_py_integration where (PY_Selected = 'T')

  if (@@error <> 0)
    set @result = 'Error Deleting from working_py_integration'
end

if @result = 'OK'
  commit tran
else
  rollback tran

select @result
























































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_GenerateScripts]') and xtype = 'P ')  
 drop Procedure sp_GenerateScripts
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

CREATE Procedure [dbo].[sp_GenerateScripts] @ScriptBuildType varchar(1) = 'D' AS
/*
@ScriptBuildType = 'D'(rop and create) is fast
@ScriptBuildType = 'A'(lter) is slow
@ScriptBuildType = 'C'(reate only) is fast
*/
--declare @ScriptBuildType varchar(1) select @ScriptBuildType = 'A'
set nocount on

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__Scripts]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table __Scripts
create table __Scripts(id int identity(1,1) not null, SQL varchar(8000) null, xType varchar(10))

if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__ScriptProgress]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)	
create table __ScriptProgress(id int identity(1,1) not null, ScriptObject varchar(8000) null, oType varchar(25), xCount int, tCount int)
truncate table __ScriptProgress

if exists (select * from tempdb..sysobjects where id = object_id(N'tempdb..#SQL'))
drop table #SQL
create table #SQL(SQL varchar(8000), id int not null identity(1,1))

declare @Name varchar(500), @XType varchar(10), @SQL varchar(8000), @NewLine char(2), @xCount int, @tCount int

set @NewLine=char(13)+char(10)

select @tCount = count(*), @xCount = 0
from sysobjects
where (xtype = 'P' or xtype = 'FN' or xtype = 'V' or xtype='TR') and status >= 0 
--and name = 'usp_IN_AccountSubCodeGetByAccountNumber'

insert __ScriptProgress(ScriptObject, oType, xCount, tCount)
select 'Initializing', 'N/A', 0, @tCount

declare PrincCursor cursor
read_only
for
	 select name, xtype
	 from sysobjects
	 where (xtype = 'P' or xtype = 'FN' or xtype = 'V' or xtype='TR') and status >= 0 
--	and name = 'usp_IN_AccountSubCodeGetByAccountNumber'
	 order by xtype, name
	 
open PrincCursor
fetch next from PrincCursor into @name, @xtype
while (@@fetch_status <> -1)
begin
	 if (@@fetch_status <> -2)
	 begin  			
		truncate table #SQL  
		--quick build, does drop / create 
		if @ScriptBuildType = 'D'
		begin
			select @SQL = 'if exists (select * from dbo.sysobjects where id = object_id(N''[dbo].[' + @Name + ']'') and xtype = ''' + @xtype + ''')  ' +  char(13) + char(10) + ' drop '
			if @xtype = 'TR' 
				select @SQL = @SQL + 'Trigger ' + @Name
			if @xtype = 'P' 
				select @SQL = @SQL + 'Procedure ' + @Name
			if @xtype = 'FN' 
				select @SQL = @SQL + 'Function ' + @Name
			if @xtype = 'V' 
				select @SQL = @SQL + 'View ' + @Name 
			insert #SQL select @SQL 
		end

		--find the first line and update to get alter
		declare @Subject varchar(20)
		select @Subject = case @xtype when 'TR' then 'Trigger'
								when 'P' then 'Proc'
								when 'FN' then 'Function'
								when 'V' then 'View' end
		
		update __ScriptProgress set ScriptObject = @name, oType = @Subject, xCount = @xCount
		select @xCount = @xCount + 1
					
		insert #SQL select char(13) + char(10) + 'Go' + char(13) + char(10)
		if @Name in ('fn_GetConcatInvNo_New', 'sp_WMSFull_CreatePackStr')
			insert #SQL select 'SET QUOTED_IDENTIFIER ON' + char(13) + char(10)
		else
			insert #SQL select 'SET QUOTED_IDENTIFIER OFF' + char(13) + char(10)
			
		insert #SQL select 'Go' + char(13) + char(10)
		insert #SQL select 'SET ANSI_NULLS ON ' + char(13) + char(10)
		insert #SQL select 'Go' + char(13) + char(10)
		
		insert #SQL 
		exec('sp_helptext '''+ @Name +'''')
		
		insert #SQL select char(13) + char(10) + 'Go' + char(13) + char(10)
		insert #SQL select 'SET QUOTED_IDENTIFIER OFF' + char(13) + char(10)
		insert #SQL select 'Go' + char(13) + char(10)
		insert #SQL select 'SET ANSI_NULLS ON ' + char(13) + char(10)
		insert #SQL select 'Go' + char(13) + char(10)

		--slow build, does alter script required for published objects in replication (these do not allow drops)
		if @ScriptBuildType = 'A'
		begin
			--now do the alter
			print @name
						
			update #SQL 
			set sql = replace(dbo.DBA_ConditionText(sql, @xtype), 'create ' + @Subject, 'alter ' + @Subject)
			where SQL like '%create%' + @Subject + '%'
		end
		
		--inserts the alter/drop/create script
		insert __Scripts (SQL, xtype)
		select sql, @xtype from #SQL    
		order by id
	 end

fetch next from PrincCursor into @name, @xtype
end 

close PrincCursor
deallocate PrincCursor

update __ScriptProgress set ScriptObject = 'Scripting Complete', oType = 'N/A', xCount = @tCount

select cast(SQL as varchar(8000)) from __Scripts order by id



Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_Get_CDT_CtlInt]') and xtype = 'P ')  
 drop Procedure sp_Get_CDT_CtlInt
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE proc sp_Get_CDT_CtlInt
    @DktType varchar(1), @CDTINT_Type varchar(30)
as
BEGIN TRAN
UPDATE CDT_CtlInt SET CDTINT_Int=CDTINT_Int+1 WHERE oh_type=@DktType AND CDTINT_Type=@CDTINT_Type
IF @@rowcount=0
BEGIN
  INSERT INTO CDT_CtlInt (CDTINT_Type, oh_type, CDTINT_Int)Values (@CDTINT_Type, @DktType, 1)
END
SELECT CDTINT_Int FROM CDT_CtlInt WHERE oh_type=@DktType AND CDTINT_Type=@CDTINT_Type
COMMIT TRAN























































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_Get_CU_CtlIntMaxTx]') and xtype = 'P ')  
 drop Procedure sp_Get_CU_CtlIntMaxTx
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE proc [dbo].[sp_Get_CU_CtlIntMaxTx]
    @user varchar(60), @type varchar(30), @max int
as

-- sp_Get_CU_CtlIntMaxTx 'FRED', 'FREDTEST', 5
--SET TRANSACTION ISOLATION LEVEL REPEATABLE READ

BEGIN TRY
  BEGIN TRAN

  DECLARE @NXT_INT int

  SET @NXT_INT = (SELECT ISNULL(Value, 0) FROM CU_CtlInt WHERE PCUser=@user AND CINT_Type=@type)

  IF @NXT_INT IS NULL
  BEGIN
    INSERT INTO CU_CtlInt (PCUser, CINT_Type, Value)Values (@user, @type, 1)
    SET @NXT_INT = 1
  END

  IF ((@max > 0) AND (@NXT_INT > @max))
  BEGIN
    UPDATE CU_CtlInt SET Value=2 WHERE PCUser=@user AND CINT_Type=@type
    SET @NXT_INT = 1
  END
  ELSE
    UPDATE CU_CtlInt SET Value=Value+1 WHERE PCUser=@user AND CINT_Type=@type

  COMMIT TRAN
  SELECT @NXT_INT
  RETURN(0)

END TRY
BEGIN CATCH
  if  (XACT_STATE() <> 0)  -- If Tx Pending
  BEGIN
    ROLLBACK TRAN
  END

  SELECT -1
  RETURN(-1)

END CATCH


























































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_Get_CW_CtlInt]') and xtype = 'P ')  
 drop Procedure sp_Get_CW_CtlInt
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE proc [dbo].[sp_Get_CW_CtlInt]
    @whs varchar(3), @type varchar(10)
as
BEGIN TRAN
UPDATE CW_CtlInt SET CWI_Int=CWI_Int+1 WHERE CWI_Whs=@whs AND CWI_Type=@type
IF @@rowcount=0
BEGIN
  INSERT INTO CW_CtlInt (CWI_Whs, CWI_Type, CWI_Int)Values (@whs, @type, 1)
END
SELECT CWI_Int FROM CW_CtlInt WHERE CWI_Whs=@whs AND CWI_Type=@type
COMMIT TRAN






















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_Get_CW_CtlIntIncTx]') and xtype = 'P ')  
 drop Procedure sp_Get_CW_CtlIntIncTx
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE proc [dbo].[sp_Get_CW_CtlIntIncTx]
    @whs varchar(60), @type varchar(30), @inc_value int
as

-- sp_Get_CW_CtlIntIncTx 'ALL','DHL_PARCEL',4
-- sp_Get_CW_CtlIntIncTx 'ALL','FREDTEST',5
--SET TRANSACTION ISOLATION LEVEL REPEATABLE READ

BEGIN TRY
  BEGIN TRAN

  DECLARE @NXT_INT int

  SET @NXT_INT = (SELECT ISNULL(CWI_Int, 0) FROM CW_CtlInt WHERE CWI_Whs=@whs AND CWI_Type=@type)

  IF @NXT_INT IS NULL
  BEGIN
    INSERT INTO CW_CtlInt (CWI_Whs, CWI_Type, CWI_Int)Values (@whs, @type, 1)
    SET @NXT_INT = 1
  END

  UPDATE CW_CtlInt SET CWI_Int=CWI_Int+@inc_value WHERE CWI_Whs=@whs AND CWI_Type=@type

  COMMIT TRAN
  SELECT @NXT_INT
  RETURN(0)

END TRY
BEGIN CATCH
  if  (XACT_STATE() <> 0)  -- If Tx Pending
  BEGIN
    ROLLBACK TRAN
  END

  SELECT -1
  RETURN(-1)

END CATCH

























































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_Get_KBI_Fill_Docket_Data]') and xtype = 'P ')  
 drop Procedure sp_Get_KBI_Fill_Docket_Data
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
 

CREATE Procedure [dbo].[sp_Get_KBI_Fill_Docket_Data] (@CS datetime, @CE datetime, @PS datetime,@PE datetime,@RangeScale varchar(2)) AS

--	Declare @CS datetime, @CE datetime, @PS datetime,@PE datetime,@RangeScale varchar(2)
--	Select @CS = '5/1/2008', @CE = '11/30/2008', @PS = '5/1/2007', @PE = '11/30/2007', @RangeScale = 'P'
-- Quote KBI
    declare @sql varchar(8000), @tr varchar(50)
    select @tr=isnull(tr_db,'') from database_setup

	if exists (select * from tempdb..sysobjects where id = object_id(N'tempdb..#DocketDetails'))
	drop table #DocketDetails
	CREATE TABLE #DocketDetails(
	[Docket Type] [varchar](20) NULL,
	[Quote Type] varchar(50),--q_qtype
	[Quote #] int null,
	[Docket#] [int] NULL,
	[Invoice#] varchar(30),
	[Territory] varchar(50),
	[Region] varchar(50),
	[Dealer Name] varchar(50),
	ClientName varchar(100),	
	[Warehouse] [varchar](45) NULL,
	[Inside Sales] varchar(50),
	[Outside Sales] varchar(50),
	[Sales Manager] varchar(50),
	[Quote Date] datetime,
	[Converted Date] datetime,
    [Form Qty] money,
	[ReRun?] bit,
	[ReRun Category] varchar(50),
	[ReRun Reason] varchar(150),
	[ReRun Qty] money,
	[QE?] bit,
	[Invoice Date] dateTime,
	[Invoice Year] int,
	[Invoice Period] int,
	[ReRun Person Responsible] varchar(50),
	[Dealer Net Cost] money,
	[Internal] bit,
    [Repeat Type] varchar(20),
	[Converted?] bit,
	[Grouped?] bit,
	Cancelled bit,
    ComparisonPeriod varchar(20),
	[Period Name] varchar(10),
	customer_code varchar(10),
	customer varchar(40),
	bill_address_1 varchar(40),
	bill_address_2 varchar(40),
	bill_city varchar(20),
	bill_province varchar(2)
)

	

SET @sql ='insert #DocketDetails   
    SELECT (select DktTypeDesc from c_dockettype ddt where ddt.DktType = DH.oh_type),
      (select QTypeShortDesc from C_QuoteType where QType = qh.Q_QType), 
      qh.q_num, DH.oh_num, INVOICENO, ter.Territory_desc, RegionGrp_Desc,
      (select top 1 Convert(varchar(38),ccc.Name) + ''('' + ccc.Customer_Code + '')'' from '+@tr+'..customers ccc where ccc.customer_id = QH.Customer_ID),
      Q_ClientName, (select Description from '+@tr+'..warehouse www where www.WAREHOUSE = DH.oh_Whs),       
      (select isnull(ss.Firstname,'''')+'' ''+isnull(ss.lastname,'''') from '+@tr+'..salespersons ss where ss.sls_id = dh.sls_id_inside), 
      (select isnull(ss.Firstname,'''')+'' ''+isnull(ss.lastname,'''') from '+@tr+'..salespersons ss where ss.sls_id = CUST.sls_id2), 
      (select isnull(ss.Firstname,'''')+'' ''+isnull(ss.lastname,'''') from '+@tr+'..salespersons ss where ss.sls_id = (select sm.sls_manager from '+@tr+'..salespersons sm where sm.sls_id = CUST.sls_id2)), 
      Q_DatePriced, oh_NRCV_Date, QG_Qty, case when ReRun_YN = ''Y'' then 1 else 0 end, ReC_Desc, Rerun_Reason, ReRun_Qty_Affected, 
      case when Q_QE_YN = ''Y'' then 1 else 0 end, INVOICE_DATE, DatePart(yy, ISNull(oh_NRCV_Date,Q_DatePriced)), DatePart(mm, ISNull(oh_NRCV_Date,Q_DatePriced)), 
      (select isnull(ss.Firstname,'''')+'' ''+isnull(ss.lastname,'''') from '+@tr+'..salespersons ss where sls_id = Q_RerunSLS_ID), 
      IsNull(QG_DktNetCost,0), case when Q_Internal_YN = ''Y'' then 1 else 0 end, 
      case when Q_Repeat_NEC = ''N'' then ''New'' when Q_Repeat_NEC = ''E'' then ''Exact Repeat'' else ''Change Repeat'' end,
      case when oh_NRCV_Date is null then 0 else 1 end, case when Q_GroupingYN = ''Y'' then 1 else 0 end,
      case when oh_status = ''Z'' then 1 else 0 end, case when ISNull(oh_NRCV_Date,Q_DatePriced) < '''+cast(@CS as varchar)+''' then ''Previous'' else ''Current'' end,
      Case When DatePart(mm, ISNull(oh_NRCV_Date,Q_DatePriced)) < 10 Then ''0''+ Convert(varchar(5),DatePart(mm, ISNull(oh_NRCV_Date,Q_DatePriced))) Else Convert(varchar(5),DatePart(mm, ISNull(oh_NRCV_Date,Q_DatePriced))) End + ''-'' +
      IsNull((Select Convert(varchar(3),DateName(m,END_DATE)) From ' + @tr + '..AR_PERIODS ar Where ar.AR_Year = DatePart(yy, ISNull(oh_NRCV_Date,Q_DatePriced)) 
      and ar.Period = DatePart(mm, ISNull(oh_NRCV_Date,Q_DatePriced))),''''),
      cust.customer_code, cust.name, sth.billing_address_1, sth.billing_address_2, sth.bill_city, sth.bill_state
      FROM D_Head DH 
    LEFT OUTER JOIN Q_Head QH ON QH.Q_ID = DH.Q_ID
      left outer join Q_Group qg on qg.qg_id = dh.QG_ID
      left outer join dbo.CQ_RerunCategory rc on rc.ReC_ID = qh.ReC_ID 
      left outer join dbo.CQ_RerunReason rr on rr.Rerun_ID = qh.Rerun_ID
    left outer JOIN ' + @tr + '..SO_TRN_HDR STH ON STH.SO_TRN_ID = DH.R2B_SO_TRN_ID
    LEFT OUTER JOIN ' + @tr + '..CUSTOMERS CUST ON CUST.CUSTOMER_ID = qh.Customer_ID
   LEFT OUTER JOIN ' + @tr + '..TERRITORY TER ON TER.TERRITORY_ID = CUST.TERRITORY_ID
      left outer join C_RegionGroupSet crgs on crgs.TERRITORY_ID = TER.TERRITORY_ID
      left outer join dbo.C_RegionGroupHead crgh on crgh.RegionGrp_ID = crgs.RegionGrp_ID
      WHERE (isnull(oh_status,'''')not in (''H'', ''N'',''Z'')) 
      AND ( (ISNull(oh_NRCV_Date,Q_DatePriced) >= ''' + Cast(@CS as varchar) + ''' and ISNull(oh_NRCV_Date,Q_DatePriced) < ''' + Cast(DateAdd(day,1,@CE) as varchar) + ''') OR 
              (ISNull(oh_NRCV_Date,Q_DatePriced) >= ''' + Cast(@PS as varchar) + ''' and ISNull(oh_NRCV_Date,Q_DatePriced) < ''' + Cast(DateAdd(day,1,@PE) as varchar) + ''') )'
--      AND ( (ISNull(oh_NRCV_Date,Q_DatePriced) between ''' + Convert(Varchar(100),@CS) + ''' and ''' + Convert(Varchar(100),@CE) + ''') OR 
--              (ISNull(oh_NRCV_Date,Q_DatePriced) between ''' + Convert(Varchar(100),@PS) + ''' and ''' + Convert(Varchar(100),@PE) + ''') )'

    exec(@sql)

--	SET @sql ='insert #DocketDetails   
--    SELECT (select DktTypeDesc from c_dockettype ddt where ddt.DktType = DH.oh_type),
--	(select QTypeShortDesc from C_QuoteType where QType = qh.Q_QType), 
--	qh.q_num, DH.oh_num, INVOICENO, ter.Territory_desc, RegionGrp_Desc,
--	(select top 1 Name from '+@tr+'..customers ccc where ccc.customer_id = QH.Customer_ID),
--	Q_ClientName, (select Description from '+@tr+'..warehouse www where www.WAREHOUSE = DH.oh_Whs),    	
--	(select Firstname from '+@tr+'..salespersons ss where sls_id = qh.sls_id), 
--	(select Firstname from '+@tr+'..salespersons ss where sls_id = dh.SLS_ID_TakenBy), 
--	(select Firstname from '+@tr+'..salespersons ss where sls_id = (select sls_manager from '+@tr+'..salespersons where sls_id = dh.sls_id_inside)), 
--	Q_DatePriced, oh_NRCV_Date, QG_Qty, case when ReRun_YN = ''Y'' then 1 else 0 end, ReC_Desc, Rerun_Reason, ReRun_Qty_Affected, 
--	case when Q_QE_YN = ''Y'' then 1 else 0 end, INVOICE_DATE, ACCT_YEAR, ACCT_PERIOD, 
--	(select Firstname from '+@tr+'..salespersons ss where sls_id = Q_RerunSLS_ID), 
--	QG_DktNetCost, case when Q_Internal_YN = ''Y'' then 1 else 0 end, 
--	case when Q_Repeat_NEC = ''N'' then ''New'' when Q_Repeat_NEC = ''E'' then ''Exact Repeat'' else ''Change Repeat'' end,
--	case when oh_NRCV_Date is null then 0 else 1 end, case when Q_GroupingYN = ''Y'' then 1 else 0 end,
--	case when oh_status = ''Z'' then 1 else 0 end, ''Previous'',
--	Case When ACCT_PERIOD < 10 Then ''0''+ Convert(varchar(5),ACCT_PERIOD) Else Convert(varchar(5),ACCT_PERIOD) End + ''-'' +
--	IsNull((Select Convert(varchar(3),DateName(m,END_DATE)) From ' + @tr + '..AR_PERIODS ar Where ar.AR_Year = ACCT_YEAR 
--	and ar.Period = ACCT_PERIOD),'''')
--	FROM D_Head DH 
--    LEFT OUTER JOIN Q_Head QH ON QH.Q_ID = DH.Q_ID
--	left outer join Q_Group qg on qg.qg_id = dh.QG_ID
--	left outer join dbo.CQ_RerunCategory rc on rc.ReC_ID = qh.ReC_ID 
--	left outer join dbo.CQ_RerunReason rr on rr.Rerun_ID = qh.Rerun_ID
--    left outer JOIN ' + @tr + '..SO_TRN_HDR STH ON STH.SO_TRN_ID = DH.R2B_SO_TRN_ID
--    LEFT OUTER JOIN ' + @tr + '..CUSTOMERS CUST ON CUST.CUSTOMER_ID = qh.Customer_ID
--    LEFT OUTER JOIN ' + @tr + '..TERRITORY TER ON TER.TERRITORY_ID = CUST.TERRITORY_ID
--	left outer join C_RegionGroupSet crgs on crgs.TERRITORY_ID = TER.TERRITORY_ID
--	left outer join dbo.C_RegionGroupHead crgh on crgh.RegionGrp_ID = crgs.RegionGrp_ID
-- 	WHERE (not isnull(oh_status,'''') in (''H'', ''N'')) 
--	and (ISNull(oh_NRCV_Date,Q_DatePriced) between ''' + Convert(Varchar(100),@PS) + 
--					  ''' and ''' + Convert(Varchar(100),@PE) + ''') '
--    exec(@sql)

	select * from #DocketDetails --where ComparisonPeriod = 'Current'






























































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_Get_KBI_Fill_Quote_Data]') and xtype = 'P ')  
 drop Procedure sp_Get_KBI_Fill_Quote_Data
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
 

CREATE Procedure [dbo].[sp_Get_KBI_Fill_Quote_Data]
(@CS datetime, @CE datetime, @PS datetime,@PE datetime,@RangeScale varchar(2))
AS 

--	Declare @CS datetime, @CE datetime, @PS datetime,@PE datetime,@RangeScale varchar(2)
--	Select @CS = '1/1/2009', @CE = '12/31/2009', @PS = '1/1/2008', @PE = '12/31/2008', @RangeScale = 'P'
-- Quote KBI
    declare @sql varchar(8000), @tr varchar(50)
    select @tr=isnull(tr_db,'') from database_setup

	if exists (select * from tempdb..sysobjects where id = object_id(N'tempdb..#QuoteDetails'))
	drop table #QuoteDetails
	CREATE TABLE #QuoteDetails(
	[Docket Type] [varchar](20) NULL,
	[Quote Type] varchar(50),--q_qtype
	[Quote #] int null,
	[Docket#] [int] NULL,
	[Invoice#] varchar(30),
	[Territory] varchar(50),
	[Region] varchar(50),
	[Dealer Name] varchar(50),
	ClientName varchar(100),	
	[Warehouse] [varchar](45) NULL,
	[Inside Sales] varchar(50),
	[Outside Sales] varchar(50),
	[Sales Manager] varchar(50),
	[Quote Date] datetime,
	[Converted Date] datetime,
    [Form Qty] money,
	[ReRun?] bit,
	[ReRun Category] varchar(50),
	[ReRun Reason] varchar(150),
	[ReRun Qty] money,
	[QE?] bit,
	[Invoice Date] dateTime,
	[Invoice Year] int,
	[Invoice Period] int,
	[ReRun Person Responsible] varchar(50),
	[Quote$] money,
	[Internal] bit,
    [Repeat Type] varchar(20),
	[Converted?] bit,
	[Grouped?] bit,
	Cancelled bit,
    ComparisonPeriod varchar(20),
	[Period Name] varchar(10),
	customer_code varchar(10),
	customer varchar(40),
	bill_address_1 varchar(40),
	bill_address_2 varchar(40),
	bill_city varchar(20),
	bill_province varchar(2)
)

	

    SET @sql ='insert #QuoteDetails   
    SELECT (select DktTypeDesc from c_dockettype ddt where ddt.DktType = DH.oh_type),
	(select QTypeShortDesc from C_QuoteType where QType = qh.Q_QType), 
	qh.q_num, DH.oh_num, INVOICENO, ter.Territory_desc, RegionGrp_Desc,
	(select top 1 Convert(varchar(38),ccc.Name) + ''('' + ccc.Customer_Code + '')'' from '+@tr+'..customers ccc where ccc.customer_id = QH.Customer_ID),
	Q_ClientName, (select Description from '+@tr+'..warehouse www where www.WAREHOUSE = DH.oh_Whs),    	
	(select isnull(ss.Firstname,'''')+'' ''+isnull(ss.lastname,'''') from '+@tr+'..salespersons ss where ss.sls_id = dh.sls_id_inside), 
	(select isnull(ss.Firstname,'''')+'' ''+isnull(ss.lastname,'''') from '+@tr+'..salespersons ss where ss.sls_id = CUST.sls_id2), 
	(select isnull(ss.Firstname,'''')+'' ''+isnull(ss.lastname,'''') from '+@tr+'..salespersons ss where ss.sls_id = (select sm.sls_manager from '+@tr+'..salespersons sm where sm.sls_id = CUST.sls_id2)), 
	Q_DatePriced, oh_NRCV_Date, QG_Qty, case when ReRun_YN = ''Y'' then 1 else 0 end, ReC_Desc, Rerun_Reason, ReRun_Qty_Affected, 
	case when Q_QE_YN = ''Y'' then 1 else 0 end, INVOICE_DATE, ACCT_YEAR, ACCT_PERIOD, 
	(select isnull(ss.Firstname,'''')+'' ''+isnull(ss.lastname,'''') from '+@tr+'..salespersons ss where sls_id = Q_RerunSLS_ID), 
	IsNull(QG_DktNetCost,0), case when Q_Internal_YN = ''Y'' then 1 else 0 end, 
	case when Q_Repeat_NEC = ''N'' then ''New'' when Q_Repeat_NEC = ''E'' then ''Exact Repeat'' else ''Change Repeat'' end,
	case when oh_NRCV_Date is null then 0 else 1 end, case when Q_GroupingYN = ''Y'' then 1 else 0 end,
	case when oh_status = ''Z'' then 1 else 0 end, 
	Case When ISNull(Q_DatePriced,oh_NRCV_Date) < ''' + Convert(Varchar(100),@CS) + ''' then ''Previous'' else ''Current'' End,
	Case When ACCT_PERIOD < 10 Then ''0''+ Convert(varchar(5),ACCT_PERIOD) Else Convert(varchar(5),ACCT_PERIOD) End + ''-'' +
	IsNull((Select Convert(varchar(3),DateName(m,END_DATE)) From ' + @tr + '..AR_PERIODS ar Where ar.AR_Year = ACCT_YEAR 
	and ar.Period = ACCT_PERIOD),''''),
	cust.customer_code, cust.name, sth.billing_address_1, sth.billing_address_2, sth.bill_city, sth.bill_state
	FROM Q_Head QH 
    LEFT OUTER JOIN D_Head DH ON QH.Q_ID = DH.Q_ID
	left outer join Q_Group qg on qg.qg_id = dh.QG_ID
	left outer join dbo.CQ_RerunCategory rc on rc.ReC_ID = qh.ReC_ID 
	left outer join dbo.CQ_RerunReason rr on rr.Rerun_ID = qh.Rerun_ID
    left outer JOIN ' + @tr + '..SO_TRN_HDR STH ON STH.SO_TRN_ID = DH.R2B_SO_TRN_ID
    LEFT OUTER JOIN ' + @tr + '..CUSTOMERS CUST ON CUST.CUSTOMER_ID = qh.Customer_ID
    LEFT OUTER JOIN ' + @tr + '..TERRITORY TER ON TER.TERRITORY_ID = CUST.TERRITORY_ID
	left outer join C_RegionGroupSet crgs on crgs.TERRITORY_ID = TER.TERRITORY_ID
	left outer join dbo.C_RegionGroupHead crgh on crgh.RegionGrp_ID = crgs.RegionGrp_ID
 	WHERE (not isnull(oh_status,'''') in (''H'', ''N'')) AND QH.Q_Status <> 98 
	AND ( (ISNull(Q_DatePriced,oh_NRCV_Date) >= ''' + Convert(Varchar(100),@CS) + ''' and ISNull(Q_DatePriced,oh_NRCV_Date) < ''' + Convert(Varchar(100),dateadd(day,1,@CE)) + ''') OR
		  (ISNull(Q_DatePriced,oh_NRCV_Date) >= ''' + Convert(Varchar(100),@PS) + ''' and ISNull(Q_DatePriced,oh_NRCV_Date) < ''' + Convert(Varchar(100),dateadd(day,1,@PE)) + ''') )' 

    exec(@sql)

--	SET @sql ='insert #QuoteDetails   
--    SELECT (select DktTypeDesc from c_dockettype ddt where ddt.DktType = DH.oh_type),
--	(select QTypeShortDesc from C_QuoteType where QType = qh.Q_QType), 
--	qh.q_num, DH.oh_num, INVOICENO, ter.Territory_desc, RegionGrp_Desc,
--	(select top 1 Name from '+@tr+'..customers ccc where ccc.customer_id = QH.Customer_ID),
--	Q_ClientName, (select Description from '+@tr+'..warehouse www where www.WAREHOUSE = DH.oh_Whs),    	
--	(select Firstname from '+@tr+'..salespersons ss where sls_id = qh.sls_id), 
--	(select Firstname from '+@tr+'..salespersons ss where sls_id = dh.SLS_ID_TakenBy), 
--	(select Firstname from '+@tr+'..salespersons ss where sls_id = (select sls_manager from '+@tr+'..salespersons where sls_id = dh.sls_id_inside)), 
--	Q_DatePriced, oh_NRCV_Date, QG_Qty, case when ReRun_YN = ''Y'' then 1 else 0 end, ReC_Desc, Rerun_Reason, ReRun_Qty_Affected, 
--	case when Q_QE_YN = ''Y'' then 1 else 0 end, INVOICE_DATE, ACCT_YEAR, ACCT_PERIOD, 
--	(select Firstname from '+@tr+'..salespersons ss where sls_id = Q_RerunSLS_ID), 
--	QG_DktNetCost, case when Q_Internal_YN = ''Y'' then 1 else 0 end, 
--	case when Q_Repeat_NEC = ''N'' then ''New'' when Q_Repeat_NEC = ''E'' then ''Exact Repeat'' else ''Change Repeat'' end,
--	case when oh_NRCV_Date is null then 0 else 1 end, case when Q_GroupingYN = ''Y'' then 1 else 0 end,
--	case when oh_status = ''Z'' then 1 else 0 end, ''Previous'',
--	Case When ACCT_PERIOD < 10 Then ''0''+ Convert(varchar(5),ACCT_PERIOD) Else Convert(varchar(5),ACCT_PERIOD) End + ''-'' +
--	IsNull((Select Convert(varchar(3),DateName(m,END_DATE)) From ' + @tr + '..AR_PERIODS ar Where ar.AR_Year = ACCT_YEAR 
--	and ar.Period = ACCT_PERIOD),'''')
--	FROM Q_Head QH 
--    LEFT OUTER JOIN D_Head DH ON QH.Q_ID = DH.Q_ID
--	left outer join Q_Group qg on qg.qg_id = dh.QG_ID
--	left outer join dbo.CQ_RerunCategory rc on rc.ReC_ID = qh.ReC_ID 
--	left outer join dbo.CQ_RerunReason rr on rr.Rerun_ID = qh.Rerun_ID
--    left outer JOIN ' + @tr + '..SO_TRN_HDR STH ON STH.SO_TRN_ID = DH.R2B_SO_TRN_ID
--    LEFT OUTER JOIN ' + @tr + '..CUSTOMERS CUST ON CUST.CUSTOMER_ID = qh.Customer_ID
--    LEFT OUTER JOIN ' + @tr + '..TERRITORY TER ON TER.TERRITORY_ID = CUST.TERRITORY_ID
--	left outer join C_RegionGroupSet crgs on crgs.TERRITORY_ID = TER.TERRITORY_ID
--	left outer join dbo.C_RegionGroupHead crgh on crgh.RegionGrp_ID = crgs.RegionGrp_ID
-- 	WHERE (not isnull(oh_status,'''') in (''H'', ''N'')) 
--	and (ISNull(Q_DatePriced,oh_NRCV_Date) between ''' + Convert(Varchar(100),@PS) + 
--					  ''' and ''' + Convert(Varchar(100),@PE) + ''') '
--    print(@sql)

	select * from #QuoteDetails --where ComparisonPeriod = 'Current'





























































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_Get_KBI_Fill_Sales_Data]') and xtype = 'P ')  
 drop Procedure sp_Get_KBI_Fill_Sales_Data
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
 

-- exec sp_Get_KBI_Fill_Sales_Data '1/1/2017', '2/13/2017', '1/1/2016', '12/31/2016', 'Y'
CREATE Procedure [dbo].[sp_Get_KBI_Fill_Sales_Data] 
(@CS datetime, @CE datetime, @PS datetime,@PE datetime,@RangeScale varchar(2))
AS

--	Declare @CS datetime, @CE datetime, @PS datetime,@PE datetime,@RangeScale varchar(2)
--	Select @CS = '1/1/2009', @CE = '12/31/2009', @PS = '1/1/2008', @PE = '12/31/2008', @RangeScale = 'S'
-- sales kbi
    declare @sql varchar(8000), @tr varchar(50)
    select @tr=isnull(tr_db,'') from database_setup

	if exists (select * from tempdb..sysobjects where id = object_id(N'tempdb..#OrderDetails'))
	drop table #OrderDetails
	CREATE TABLE #OrderDetails(
	[Source] [varchar](20) NULL,
	[Docket Type] [varchar](20) NULL,
	[Docket#] [int] NULL,
	[Invoice#] varchar(30),
	[Docket ID] [int] NULL,
	[Territory] varchar(50),
	[Region] varchar(50),
	[Dealer Name] varchar(50),
	ClientName varchar(100),	
	[Warehouse] [varchar](45) NULL,
	[Quote #] int null,
	[Inside Sales] varchar(50),
	[Outside Sales] varchar(50),
	[Sales Manager] varchar(50),
	Date datetime,
    [Form Qty] money,
	[ReRun?] bit,
	[ReRun Category] varchar(50),
	[ReRun Reason] varchar(150),
	[ReRun Qty] money,
	[QE?] bit,
	[Invoice Date] dateTime,
	[Invoice Year] int,
	[Invoice Period] int,
	[ReRun Person Responsible] varchar(50),
	[Sale$] money,
	[Misc$] money,
	[Sales & Misc$] money,
	[Cost $] money,
	[GM $] money,
	[GM %] money,
	[Internal] bit,
	[Repeat Type] varchar(20),
	[so_trn_id] int,
    ComparisonPeriod varchar(20),
	[Period Name] varchar(10),
	Customer_Code varchar(10),
	Customer varchar(40),
	Bill_Address_1 varchar(40),
	Bill_Address_2 varchar(40),
	Bill_City varchar(20),
	Bill_Province varchar(2)
)

	Declare @myWhere varchar(8000), @myCase varchar(8000)
	if @RangeScale <> 'P'
	begin
		select @myWhere = ' AND ((INVOICE_DATE between ''' + Convert(Varchar(100),@CS) + 
						  ''' and ''' + Convert(Varchar(100),@CE) + ''') OR (INVOICE_DATE between ''' + Convert(Varchar(100),@PS) + 
						  ''' and ''' + Convert(Varchar(100),@PE) + ''')) ' 

		select @myCase = ' Case When (INVOICE_DATE between ''' + Convert(Varchar(100),@CS) + 
						  ''' and ''' + Convert(Varchar(100),@CE) + ''') Then ''Current'' Else ''Previous'' End '
	end
	else
	begin
		select @myWhere = ' AND ( ((Select End_Date from dbo.AR_Periods ' + 
							'Where AR_Year = ACCT_YEAR and Period = ACCT_PERIOD) ' +
							'between ''' + Convert(Varchar(100),@CS) + 
							''' and ''' + Convert(Varchar(100),@CE) + ''') OR ((Select End_Date from dbo.AR_Periods ' + 
							'Where AR_Year = ACCT_YEAR and Period = ACCT_PERIOD) ' +
							'between ''' + Convert(Varchar(100),@PS) + 
							''' and ''' + Convert(Varchar(100),@PE) + ''')) '

		select @myCase = ' Case When ((Select End_Date from dbo.AR_Periods ' + 
							'Where AR_Year = ACCT_YEAR and Period = ACCT_PERIOD) ' +
							'between ''' + Convert(Varchar(100),@CS) + 
							''' and ''' + Convert(Varchar(100),@CE) + ''') Then ''Current'' Else ''Previous'' End '
	end

	Select @myWhere = Replace(@myWhere,'dbo.',@tr+'..')
	Select @myCase = Replace(@myCase,'dbo.',@tr+'..')


    Select @sql ='insert #OrderDetails   
    SELECT ''Invoiced Docket'', (select DktTypeDesc from c_dockettype ddt where ddt.DktType = DH.oh_type), 
	DH.oh_num, INVOICENO, DH.oh_ID, ter.Territory_desc, RegionGrp_Desc,
	(select top 1 Convert(varchar(38),ccc.Name) + ''('' + ccc.Customer_Code + '')'' from '+@tr+'..customers ccc where ccc.customer_id = QH.Customer_ID),
	Q_ClientName, (select Description from '+@tr+'..warehouse www where www.WAREHOUSE = DH.oh_Whs),    
	dh.q_id, 
	(select isnull(ss.Firstname,'''')+'' ''+isnull(ss.lastname,'''') from '+@tr+'..salespersons ss where ss.sls_id = sth.sls_id), 
	(select isnull(ss.Firstname,'''')+'' ''+isnull(ss.lastname,'''') from '+@tr+'..salespersons ss where ss.sls_id = CUST.sls_id2), 
	(select isnull(ss.Firstname,'''')+'' ''+isnull(ss.lastname,'''') from '+@tr+'..salespersons ss where ss.sls_id = (select sm.sls_manager from '+@tr+'..salespersons sm where sm.sls_id = CUST.sls_id2)), 
	oh_Nrcv_date, QG_Qty, case when ReRun_YN = ''Y'' then 1 else 0 end, 
	ReC_Desc, Rerun_Reason, ReRun_Qty_Affected, case when Q_QE_YN = ''Y'' then 1 else 0 end,
	INVOICE_DATE, ACCT_YEAR, ACCT_PERIOD, 
	(select isnull(ss.Firstname,'''')+'' ''+isnull(ss.lastname,'''') from '+@tr+'..salespersons ss where sls_id = Q_RerunSLS_ID), 
	isnull(Total_products,0), isnull(Total_other_Misc,0), 
	isnull(Total_products,0) + isnull(Total_other_Misc,0), 
	0, 0, 0,
	case when Q_Internal_YN = ''Y'' then 1 else 0 end, 
	case when Q_Repeat_NEC = ''N'' then ''New'' when Q_Repeat_NEC = ''E'' then ''Exact Repeat'' else ''Change Repeat'' end,
	sth.so_trn_id, ' + @myCase + ',
	Case When ACCT_PERIOD < 10 Then ''0''+ Convert(varchar(5),ACCT_PERIOD) Else Convert(varchar(5),ACCT_PERIOD) End + ''-'' +
	IsNull((Select Convert(varchar(3),DateName(m,END_DATE)) From ' + @tr + '..AR_PERIODS ar Where ar.AR_Year = ACCT_YEAR 
	and ar.Period = ACCT_PERIOD),''''),
	cust.customer_code, cust.name, sth.billing_address_1, sth.billing_address_2, sth.bill_city, sth.bill_state
	FROM D_Head DH    
    LEFT OUTER JOIN Q_Head QH ON QH.Q_ID = DH.Q_ID
	left outer join Q_Group qg on qg.qg_id = dh.QG_ID
	left outer join dbo.CQ_RerunCategory rc on rc.ReC_ID = qh.ReC_ID 
	left outer join dbo.CQ_RerunReason rr on rr.Rerun_ID = qh.Rerun_ID
    JOIN ' + @tr + '..SO_TRN_HDR STH ON STH.SO_TRN_ID = DH.R2B_SO_TRN_ID
    LEFT OUTER JOIN ' + @tr + '..CUSTOMERS CUST ON CUST.CUSTOMER_ID = sth.Customer_ID
    LEFT OUTER JOIN ' + @tr + '..TERRITORY TER ON TER.TERRITORY_ID = CUST.TERRITORY_ID
	left outer join C_RegionGroupSet crgs on crgs.TERRITORY_ID = TER.TERRITORY_ID
	left outer join dbo.C_RegionGroupHead crgh on crgh.RegionGrp_ID = crgs.RegionGrp_ID
 	WHERE oh_status = ''X''--billed 
	' + @myWhere + '
	union
    SELECT ''Non Docket Invoice'', ''"M"'', -1 AS oh_num, invoiceno,-1 as oh_ID, 
	ter.Territory_desc,  RegionGrp_Desc,
	(select top 1 Convert(varchar(38),ccc.Name) + ''('' + IsNull(ccc.Customer_Code,'''') + '')'' from '+@tr+'..customers ccc where ccc.customer_id = STH.Customer_ID),'''', 
	(select Description from '+@tr+'..warehouse www where www.WHSE_ID = SMH.WHSE_ID), -1, 
	(select isnull(ss.Firstname,'''')+'' ''+isnull(ss.lastname,'''') from '+@tr+'..salespersons ss where ss.sls_id = sth.sls_id), 
	(select isnull(ss.Firstname,'''')+'' ''+isnull(ss.lastname,'''') from '+@tr+'..salespersons ss where ss.sls_id = CUST.sls_id2), 
	(select isnull(ss.Firstname,'''')+'' ''+isnull(ss.lastname,'''') from '+@tr+'..salespersons ss where ss.sls_id = (select sm.sls_manager from '+@tr+'..salespersons sm where sm.sls_id = CUST.sls_id2)), 
	Invoice_Date, 0, 0, '''', '''', 0, 0,
	INVOICE_DATE, ACCT_YEAR, ACCT_PERIOD, '''', isnull(Total_products,0), isnull(Total_other_Misc,0), 
	isnull(Total_products,0) + isnull(Total_other_Misc,0),  
	0, 0, 0, 0, null, sth.so_trn_id, ' + @myCase + ',
	Case When ACCT_PERIOD < 10 Then ''0''+ Convert(varchar(5),ACCT_PERIOD) Else Convert(varchar(5),ACCT_PERIOD) End + ''-'' +
	IsNull((Select Convert(varchar(3),DateName(m,END_DATE)) From ' + @tr + '..AR_PERIODS ar Where ar.AR_Year = ACCT_YEAR 
	and ar.Period = ACCT_PERIOD),''''),
	cust.customer_code, cust.name, sth.billing_address_1, sth.billing_address_2, sth.bill_city, sth.bill_state
	FROM ' + @tr + '..SO_TRN_HDR STH
    JOIN ' + @tr + '..SO_MASTER_HDR SMH ON SMH.SO_ID = STH.SO_ID
    LEFT JOIN ' + @tr + '..WAREHOUSE WHS ON WHS.WHSE_ID = SMH.WHSE_ID
    LEFT OUTER JOIN ' + @tr + '..CUSTOMERS CUST ON CUST.CUSTOMER_ID = sth.Customer_ID
    LEFT OUTER JOIN ' + @tr + '..TERRITORY TER ON TER.TERRITORY_ID = CUST.TERRITORY_ID
	left outer join C_RegionGroupSet crgs on crgs.TERRITORY_ID = TER.TERRITORY_ID
	left outer join dbo.C_RegionGroupHead crgh on crgh.RegionGrp_ID = crgs.RegionGrp_ID
 	WHERE (STH.SO_ID <> 0) ' + @myWhere + ' '
    exec(@sql)

	Select @sql ='insert #OrderDetails   
    SELECT ''Non Docket Invoice'', ''"M"'', -1 AS oh_num, invoiceno,-1 as oh_ID, 
	ter.Territory_desc,  RegionGrp_Desc,
	(select top 1 Convert(varchar(38),ccc.Name) + ''('' + IsNull(ccc.Customer_Code,'''') + '')'' from '+@tr+'..customers ccc where ccc.customer_id = STH.Customer_ID),'''', 
	''N/A'', -1, 
	(select isnull(ss.Firstname,'''')+'' ''+isnull(ss.lastname,'''') from '+@tr+'..salespersons ss where ss.sls_id = sth.sls_id), 
	(select isnull(ss.Firstname,'''')+'' ''+isnull(ss.lastname,'''') from '+@tr+'..salespersons ss where ss.sls_id = CUST.sls_id2), 
	(select isnull(ss.Firstname,'''')+'' ''+isnull(ss.lastname,'''') from '+@tr+'..salespersons ss where ss.sls_id = (select sls_manager from '+@tr+'..salespersons sm where sm.sls_id = CUST.sls_id2)), 
	Invoice_Date, 0, 0, '''', '''', 0, 0,
	INVOICE_DATE, ACCT_YEAR, ACCT_PERIOD, '''', isnull(Total_products,0), isnull(Total_other_Misc,0), 
	isnull(Total_products,0) + isnull(Total_other_Misc,0),  
	0, 0, 0, 0, null, so_trn_id, ' + @myCase + ',
	Case When ACCT_PERIOD < 10 Then ''0''+ Convert(varchar(5),ACCT_PERIOD) Else Convert(varchar(5),ACCT_PERIOD) End + ''-'' +
	IsNull((Select Convert(varchar(3),DateName(m,END_DATE)) From ' + @tr + '..AR_PERIODS ar Where ar.AR_Year = ACCT_YEAR 
	and ar.Period = ACCT_PERIOD),''''),
	cust.customer_code, cust.name, sth.billing_address_1, sth.billing_address_2, sth.bill_city, sth.bill_state
	FROM ' + @tr + '..SO_TRN_HDR STH
    LEFT OUTER JOIN ' + @tr + '..CUSTOMERS CUST ON CUST.CUSTOMER_ID = sth.Customer_ID
    LEFT OUTER JOIN ' + @tr + '..TERRITORY TER ON TER.TERRITORY_ID = CUST.TERRITORY_ID
	left outer join C_RegionGroupSet crgs on crgs.TERRITORY_ID = TER.TERRITORY_ID
	left outer join dbo.C_RegionGroupHead crgh on crgh.RegionGrp_ID = crgs.RegionGrp_ID
 	WHERE [so_trn_id] not in (select [so_trn_id] from #OrderDetails) ' + @myWhere
    exec(@sql)

--Select @sql ='insert #OrderDetails   
--    SELECT ''Invoiced Docket'', (select DktTypeDesc from c_dockettype ddt where ddt.DktType = DH.oh_type), 
--	DH.oh_num, INVOICENO, DH.oh_ID, ter.Territory_desc, RegionGrp_Desc,
--	(select top 1 Name from '+@tr+'..customers ccc where ccc.customer_id = QH.Customer_ID),
--	Q_ClientName, (select Description from '+@tr+'..warehouse www where www.WAREHOUSE = DH.oh_Whs),    
--	dh.q_id, 
--	(select Firstname from '+@tr+'..salespersons ss where sls_id = sth.sls_id), 
--	(select Firstname from '+@tr+'..salespersons ss where sls_id = CUST.sls_id2), 
--	(select Firstname from '+@tr+'..salespersons ss where sls_id = (select sls_manager from '+@tr+'..salespersons where sls_id = CUST.sls_id2)), 
--	oh_Nrcv_date, QG_Qty, case when ReRun_YN = ''Y'' then 1 else 0 end, 
--	ReC_Desc, Rerun_Reason, ReRun_Qty_Affected, case when Q_QE_YN = ''Y'' then 1 else 0 end,
--	INVOICE_DATE, ACCT_YEAR, ACCT_PERIOD, 
--	(select Firstname from '+@tr+'..salespersons ss where sls_id = Q_RerunSLS_ID), 
--	isnull(Total_products,0), isnull(Total_other_Misc,0), 
--	isnull(Total_products,0) + isnull(Total_other_Misc,0), 
--	0, 0, 0,
--	case when Q_Internal_YN = ''Y'' then 1 else 0 end, sth.so_trn_id, ''Previous'',
--	Case When ACCT_PERIOD < 10 Then ''0''+ Convert(varchar(5),ACCT_PERIOD) Else Convert(varchar(5),ACCT_PERIOD) End + ''-'' +
--	IsNull((Select Convert(varchar(3),DateName(m,END_DATE)) From ' + @tr + '..AR_PERIODS ar Where ar.AR_Year = ACCT_YEAR 
--	and ar.Period = ACCT_PERIOD),'''')
--	FROM D_Head DH    
--    LEFT OUTER JOIN Q_Head QH ON QH.Q_ID = DH.Q_ID
--	left outer join Q_Group qg on qg.qg_id = dh.QG_ID
--	left outer join dbo.CQ_RerunCategory rc on rc.ReC_ID = qh.ReC_ID 
--	left outer join dbo.CQ_RerunReason rr on rr.Rerun_ID = qh.Rerun_ID
--    JOIN ' + @tr + '..SO_TRN_HDR STH ON STH.SO_TRN_ID = DH.R2B_SO_TRN_ID
--    LEFT OUTER JOIN ' + @tr + '..CUSTOMERS CUST ON CUST.CUSTOMER_ID = sth.Customer_ID
--    LEFT OUTER JOIN ' + @tr + '..TERRITORY TER ON TER.TERRITORY_ID = CUST.TERRITORY_ID
--	left outer join C_RegionGroupSet crgs on crgs.TERRITORY_ID = TER.TERRITORY_ID
--	left outer join dbo.C_RegionGroupHead crgh on crgh.RegionGrp_ID = crgs.RegionGrp_ID
-- 	WHERE oh_status = ''X''--billed 
--	' + @myWherePrevious + '
--	union
--    SELECT ''Non Docket Invoice'', ''"M"'', -1 AS oh_num, invoiceno,-1 as oh_ID, 
--	ter.Territory_desc,  RegionGrp_Desc,
--	(select top 1 Name from '+@tr+'..customers ccc where ccc.customer_id = STH.Customer_ID),'''', 
--	(select Description from '+@tr+'..warehouse www where www.WHSE_ID = SMH.WHSE_ID), -1, 
--	(select Firstname from '+@tr+'..salespersons ss where sls_id = sth.sls_id), 
--	(select Firstname from '+@tr+'..salespersons ss where sls_id = CUST.sls_id2), 
--	(select Firstname from '+@tr+'..salespersons ss where sls_id = (select sls_manager from '+@tr+'..salespersons where sls_id = CUST.sls_id2)), 
--	Invoice_Date, 0, 0, '''', '''', 0, 0,
--	INVOICE_DATE, ACCT_YEAR, ACCT_PERIOD, '''', isnull(Total_products,0), isnull(Total_other_Misc,0), 
--	isnull(Total_products,0) + isnull(Total_other_Misc,0),  
--	0, 0, 0, 0, sth.so_trn_id, ''Previous'',
--	Case When ACCT_PERIOD < 10 Then ''0''+ Convert(varchar(5),ACCT_PERIOD) Else Convert(varchar(5),ACCT_PERIOD) End + ''-'' +
--	IsNull((Select Convert(varchar(3),DateName(m,END_DATE)) From ' + @tr + '..AR_PERIODS ar Where ar.AR_Year = ACCT_YEAR 
--	and ar.Period = ACCT_PERIOD),'''')
--	FROM ' + @tr + '..SO_TRN_HDR STH
--    JOIN ' + @tr + '..SO_MASTER_HDR SMH ON SMH.SO_ID = STH.SO_ID
--    LEFT JOIN ' + @tr + '..WAREHOUSE WHS ON WHS.WHSE_ID = SMH.WHSE_ID
--    LEFT OUTER JOIN ' + @tr + '..CUSTOMERS CUST ON CUST.CUSTOMER_ID = sth.Customer_ID
--    LEFT OUTER JOIN ' + @tr + '..TERRITORY TER ON TER.TERRITORY_ID = CUST.TERRITORY_ID
--	left outer join C_RegionGroupSet crgs on crgs.TERRITORY_ID = TER.TERRITORY_ID
--	left outer join dbo.C_RegionGroupHead crgh on crgh.RegionGrp_ID = crgs.RegionGrp_ID
-- 	WHERE (STH.SO_ID <> 0) ' + @myWherePrevious + ' '
--    exec(@sql)
--
--	Select @sql ='insert #OrderDetails   
--    SELECT ''Non Docket Invoice'', ''"M"'', -1 AS oh_num, invoiceno,-1 as oh_ID, 
--	ter.Territory_desc,  RegionGrp_Desc,
--	(select top 1 Name from '+@tr+'..customers ccc where ccc.customer_id = STH.Customer_ID),'''', 
--	''N/A'', -1, 
--	(select Firstname from '+@tr+'..salespersons ss where sls_id = sth.sls_id), 
--	(select Firstname from '+@tr+'..salespersons ss where sls_id = CUST.sls_id2), 
--	(select Firstname from '+@tr+'..salespersons ss where sls_id = (select sls_manager from '+@tr+'..salespersons where sls_id = CUST.sls_id2)), 
--	Invoice_Date, 0, 0, '''', '''', 0, 0,
--	INVOICE_DATE, ACCT_YEAR, ACCT_PERIOD, '''', isnull(Total_products,0), isnull(Total_other_Misc,0), 
--	isnull(Total_products,0) + isnull(Total_other_Misc,0),  
--	0, 0, 0, 0, so_trn_id, ''Previous'',
--	Case When ACCT_PERIOD < 10 Then ''0''+ Convert(varchar(5),ACCT_PERIOD) Else Convert(varchar(5),ACCT_PERIOD) End + ''-'' +
--	IsNull((Select Convert(varchar(3),DateName(m,END_DATE)) From ' + @tr + '..AR_PERIODS ar Where ar.AR_Year = ACCT_YEAR 
--	and ar.Period = ACCT_PERIOD),'''')
--	FROM ' + @tr + '..SO_TRN_HDR STH
--    LEFT OUTER JOIN ' + @tr + '..CUSTOMERS CUST ON CUST.CUSTOMER_ID = sth.Customer_ID
--    LEFT OUTER JOIN ' + @tr + '..TERRITORY TER ON TER.TERRITORY_ID = CUST.TERRITORY_ID
--	left outer join C_RegionGroupSet crgs on crgs.TERRITORY_ID = TER.TERRITORY_ID
--	left outer join dbo.C_RegionGroupHead crgh on crgh.RegionGrp_ID = crgs.RegionGrp_ID
-- 	WHERE [so_trn_id] not in (select [so_trn_id] from #OrderDetails) ' + @myWherePrevious
--    exec(@sql)

	alter table #OrderDetails drop column so_trn_id
	select * from #OrderDetails 

  

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_Get_NextDktNum]') and xtype = 'P ')  
 drop Procedure sp_Get_NextDktNum
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go


CREATE proc [dbo].[sp_Get_NextDktNum]
    @DktType varchar(1), @CDTINT_Type varchar(30)
as
UPDATE CDT_CtlInt SET CDTINT_Int=CDTINT_Int+1 WHERE oh_type=@DktType AND CDTINT_Type=@CDTINT_Type
IF @@rowcount=0
BEGIN
  INSERT INTO CDT_CtlInt (CDTINT_Type, oh_type, CDTINT_Int)Values (@CDTINT_Type, @DktType, 1)
END
SELECT CDTINT_Int FROM CDT_CtlInt WHERE oh_type=@DktType AND CDTINT_Type=@CDTINT_Type


Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_Get_NextWaybillNum]') and xtype = 'P ')  
 drop Procedure sp_Get_NextWaybillNum
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE proc [dbo].[sp_Get_NextWaybillNum]
    @COU_ID int
as
BEGIN TRAN
UPDATE COU_Courier SET Cou_Waybill_NextNum=Cou_Waybill_NextNum+1 WHERE COU_ID=@COU_ID
SELECT Cou_Waybill_NextNum FROM COU_Courier WHERE COU_ID=@COU_ID
COMMIT TRAN






















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_GetContactRecs]') and xtype = 'P ')  
 drop Procedure sp_GetContactRecs
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
--exec sp_GetContactRecs TR_Factor, 1809
CREATE  procedure [dbo].[sp_GetContactRecs]
@tr_dbname varchar(50),
@customer_id varchar (20)
as
begin
  declare @SQL varchar(3000)

  set @SQL = '
  SELECT 1 as sortseq, ''Default'' AS Type, LAST_NAME AS LName, FIRST_NAME AS FName,
  EMAIL AS Email, TITLE AS Title,
  IsNull(TELEPHONE, '''') + CASE WHEN LTRIM(ISNULL(EXTENSION,'''')) = '''' THEN '''' ELSE '' Ext '' + ISNULL(EXTENSION,'''') END AS Phone,
  FAX as Fax,  MOBILE_PHONE AS Cell
  FROM ' + @tr_dbname + '..CUST_CONTACT
  WHERE CUSTOMER_ID = ' + @customer_id + '
  AND ISNULL(CONT_DEFAULT,''F'') = ''T''
  UNION ALL
  SELECT 2 as sortseq, ''Over-ride'' AS Type, '''' AS LName, CST.NAME as FName,
  ISNULL(DL.DLR_EMAIL, ISNULL(CC.EMAIL, '''')) AS Email, ''Company'' AS Title,
  CST.TELEPHONE AS Phone,
  CST.FAX as Fax, '''' AS Cell
  FROM ' + @tr_dbname + '..CUSTOMERS CST
  LEFT JOIN ' + @tr_dbname + '..CUST_CONTACT CC ON CC.CUSTOMER_ID = CST.CUSTOMER_ID AND CC.CONT_DEFAULT = ''T''
  LEFT JOIN dlr_lab DL ON DL.dlr_Customer_ID = CST.CUSTOMER_ID
  WHERE CST.CUSTOMER_ID = ' + @customer_id + '
  UNION ALL
  SELECT 3 as sortseq, ''Alternates'' AS Type, LAST_NAME AS LName, FIRST_NAME AS FName,
  EMAIL AS Email, TITLE AS Title,
  IsNull(TELEPHONE, '''') + CASE WHEN LTRIM(ISNULL(EXTENSION,'''')) = '''' THEN '''' ELSE '' Ext '' + ISNULL(EXTENSION,'''') END AS Phone,
  FAX as Fax,  MOBILE_PHONE AS Cell
  FROM ' + @tr_dbname + '..CUST_CONTACT
  WHERE CUSTOMER_ID = ' + @customer_id + '
  AND ISNULL(CONT_DEFAULT,''F'') = ''F''
  ORDER BY sortseq, Email
' 

  

  exec (@SQL)
end






































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_GetContactRecsNew]') and xtype = 'P ')  
 drop Procedure sp_GetContactRecsNew
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go


--exec sp_GetContactRecs TR_Factor_Mar03, 2474
--exec sp_GetContactRecsNew WEB_FACTOR_Mar03, 14, 2474
CREATE  procedure [dbo].[sp_GetContactRecsNew]
@web_dbname varchar(50),
@AutoID varchar (20),
@customer_id varchar (20)
as
begin
  declare @SQL varchar(3000),
          @tr_db varchar(50)
  
  
  SELECT @tr_db=tr_db FROM database_setup

  set @SQL = '
  SELECT
   1 as sortseq, ''Default'' AS Type, C.LastName AS LName, C.FirstName AS FName,
  C.Email1Address AS Email, C.JobTitle AS Title,
  CASE WHEN LEN(LTRIM(RTRIM(ISNULL(C.BusinessTelephoneNumber, ISNULL(C.PrimaryTelephoneNumber, ''''))))) < 4 THEN
    ISNULL(CST.TELEPHONE,'''')
  ELSE
    ISNULL(C.BusinessTelephoneNumber, ISNULL(C.PrimaryTelephoneNumber, ''''))
  END   AS Phone,
  CASE WHEN LEN(LTRIM(RTRIM(ISNULL(C.BusinessFaxNumber, ISNULL(C.HomeFaxNumber, ''''))))) < 4 THEN
    ISNULL(CST.FAX,'''')
  ELSE
    ISNULL(C.BusinessFaxNumber, ISNULL(C.HomeFaxNumber, ''''))
  END  AS Fax,
  ISNULL(C.MobileTelephoneNumber, '''') AS Cell
  FROM ' + @web_dbname + '..Relations R
  LEFT JOIN ' + @web_dbname + '..Contact C on C.id = R.itemid
  LEFT JOIN ' + @web_dbname + '..COMMUNICATION_DEFAULTS DEF ON DEF.CONTACT_ID = C.id
  LEFT JOIN ' + @tr_db + '..CUSTOMERS CST ON CST.CUSTOMER_ID = R.targetid
  WHERE R.Target = ''Customer'' and R.targetid = ' + @customer_id + ' AND R.CompanyID=' + @AutoID + ' AND ISNULL(DEF.COMM_PURPOSE_ID, -1)=7
  UNION ALL
  SELECT 2 as sortseq, ''Over-ride'' AS Type, '''' AS LName, CST.NAME as FName,
  ISNULL(DL.DLR_EMAIL, '''') AS Email, ''Company'' AS Title,
  CST.TELEPHONE AS Phone,
  CST.FAX as Fax, '''' AS Cell
  FROM ' + @tr_db + '..CUSTOMERS CST
  LEFT JOIN dlr_lab DL ON DL.dlr_Customer_ID = CST.CUSTOMER_ID
  WHERE CST.CUSTOMER_ID = ' + @customer_id + ' AND (ISNULL(DL.dlr_email, '''') <> '''')
    UNION ALL
  SELECT 3 as sortseq, ''Alternates'' AS Type, C.LastName AS LName, C.FirstName AS FName,
  C.Email1Address AS Email, C.JobTitle AS Title,
  CASE WHEN LEN(LTRIM(RTRIM(ISNULL(C.BusinessTelephoneNumber, ISNULL(C.PrimaryTelephoneNumber, ''''))))) < 4 THEN
    ISNULL(CST.TELEPHONE,'''')
  ELSE
    ISNULL(C.BusinessTelephoneNumber, ISNULL(C.PrimaryTelephoneNumber, ''''))
  END AS Phone,
  CASE WHEN LEN(LTRIM(RTRIM(ISNULL(C.BusinessFaxNumber, ISNULL(C.HomeFaxNumber, ''''))))) < 4 THEN
    ISNULL(CST.FAX,'''')
  ELSE
    ISNULL(C.BusinessFaxNumber, ISNULL(C.HomeFaxNumber, ''''))
  END  AS Fax,
  ISNULL(C.MobileTelephoneNumber, '''') AS Cell
  FROM ' + @web_dbname + '..relations R
  LEFT JOIN ' + @web_dbname + '..contact C on C.id = R.itemid
  LEFT JOIN ' + @web_dbname + '..communication_defaults DEF ON DEF.CONTACT_ID = C.id
  LEFT JOIN ' + @tr_db + '..CUSTOMERS CST ON CST.CUSTOMER_ID = R.targetid
  WHERE R.Target = ''Customer'' and R.targetid = ' + @customer_id + ' AND R.CompanyID=' + @AutoID + ' AND ISNULL(DEF.COMM_PURPOSE_ID, -1)<>7
  ORDER BY sortseq, Email'
  
--select @SQL
 exec (@SQL)
end



Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_GetRepeatDocketNums]') and xtype = 'P ')  
 drop Procedure sp_GetRepeatDocketNums
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
-- =============================================
-- sp_GetRepeatDocketNums 679901
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetRepeatDocketNums]
    @Q_ID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @C_Q_Repeat_Q_ID int
	declare @OutputResult varchar(200)

	set @OutputResult=''

	select @C_Q_Repeat_Q_ID = ISNULL(Q_Repeat_Q_ID, -1)
	FROM Q_Head where Q_ID=@Q_ID

	if  (@C_Q_Repeat_Q_ID <> -1)
	begin
		declare @cnt int

		select @cnt = count(*) from D_Head
		where Q_ID=
		(SELECT Q_Repeat_Q_ID FROM Q_Head where Q_ID=@Q_ID)

		if  (@cnt = 1)
		begin
			SELECT @OutputResult = oh_type + ' ' + CAST(min(oh_num) as varchar)
			FROM D_Head
			WHERE Q_ID = 
			(SELECT Q_Repeat_Q_ID FROM Q_Head where Q_ID=@Q_ID)
			GROUP BY oh_type
		end

		if  (@cnt > 1)
		begin
			SELECT @OutputResult = oh_type + ' ' + CAST(min(oh_num) as varchar) + ' (' + CAST(count(*) as varchar) + ')'
			FROM D_Head
			WHERE Q_ID = 
			(SELECT Q_Repeat_Q_ID FROM Q_Head where Q_ID=@Q_ID)
			GROUP BY oh_type
		 end
    end

	select @OutputResult
end









Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_HMMISC_FSLIST]') and xtype = 'P ')  
 drop Procedure sp_HMMISC_FSLIST
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

-- sp_HMMISC_FSLIST
CREATE PROCEDURE [dbo].[sp_HMMISC_FSLIST]
 AS

exec sp_HMMISC_GenTableSpecs

SELECT TABLE_NAME, COLUMN_NAME, TYPE_NAME, PRECISION, LENGTH, ISNULL(SCALE, -1) AS SCALE, ISNULL(RADIX, -1) AS RADIX, NULLABLE
FROM TableSpecs
ORDER BY TABLE_NAME, COLUMN_NAME

























































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_HMMISC_GenTableSpecs]') and xtype = 'P ')  
 drop Procedure sp_HMMISC_GenTableSpecs
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE PROCEDURE [dbo].[sp_HMMISC_GenTableSpecs]
 AS

if exists (select * from sysobjects where id = object_id(N'TableSpecs'))
  drop table TableSpecs
    Create Table TableSpecs(  
     TABLE_QUALIFIER varchar(200) null,
     TABLE_OWNER varchar(50) null, 
     TABLE_NAME varchar(50) null, 
     COLUMN_NAME varchar(50) null, 
     DATA_TYPE int null,
     TYPE_NAME varchar(50) null, 
     [PRECISION] int null,  
     LENGTH int null,      
     SCALE int null, 
     RADIX int null, 
     NULLABLE bit null,
     REMARKS varchar(500) null,


     COLUMN_DEF  varchar(500) null,


     SQL_DATA_TYPE int null,
     SQL_DATETIME_SUB varchar(50) null, 
     CHAR_OCTET_LENGTH int null,
     ORDINAL_POSITION int,
     IS_NULLABLE  varchar(50) null, 
     SS_DATA_TYPE int null)


  declare @TableName varchar(500)


  if exists (select * from tempdb..sysobjects where id =
object_id(N'tempdb..#TableFields'))
  drop table #TableFields
  Create Table #TableFields(      
     TABLE_QUALIFIER varchar(200) null,
     TABLE_OWNER varchar(50) null, 
     TABLE_NAME varchar(50) null, 
     COLUMN_NAME varchar(50) null, 
     DATA_TYPE int null,
     TYPE_NAME varchar(50) null, 
     [PRECISION] int null,  
     LENGTH int null,      
     SCALE int null, 
     RADIX int null, 
     NULLABLE bit null,
     REMARKS varchar(500) null,


     COLUMN_DEF  varchar(500) null,


     SQL_DATA_TYPE int null,
     SQL_DATETIME_SUB varchar(50) null, 
     CHAR_OCTET_LENGTH int null,
     ORDINAL_POSITION int,
     IS_NULLABLE  varchar(50) null, 
     SS_DATA_TYPE int null)


    declare TableCursor cursor
    read_only
    for
    select name
    from sysobjects
    where xtype = 'U'
    order by name


  
    open TableCursor 
    fetch next from TableCursor  into @TableName
    while (@@fetch_status <> -1)


    begin
     if (@@fetch_status <> -2)
     begin
      exec('
      insert #TableFields
      exec sp_columns ''' + @TableName + '''')
     end
    fetch next from TableCursor into @TableName
    end


    close TableCursor
    deallocate TableCursor


insert TableSpecs
select *
from #TableFields

























































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_LoadPYIntegration]') and xtype = 'P ')  
 drop Procedure sp_LoadPYIntegration
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go




-- sp_LoadPYIntegration 'marty', 'hr_factor', '10/30/2007'

CREATE   procedure [dbo].[sp_LoadPYIntegration]
-- declare
@username varchar(10),
@hr_db varchar(50),
@CutOffDate datetime


-- set @username='marty'
-- set @hr_db='hr_factor'
as
declare
@sql nvarchar(4000),
@result varchar(100)
/*
Need to grab all the records from d_bc_transactions and put into d_py_integration
where the swipe code matches a status where the py_calc_type indicates that it needs to go to this table
*/
set @result='OK'

begin tran
--update the d_bc_transactions table with the employee number, based on the badge number
set @sql='
update d_bc_transactions set emp_no=e.emp_no
from ' + @hr_db + '..employee e
where e.badge_num=d_bc_transactions.emp_badge_num and isnull(d_bc_transactions.emp_no,0)=0
and D_BC_Transactions.Emp_No > -1'
exec(@sql)if @@error<>0
	set @result='Error updating d_bc_transactions'

--flag the records we're bringing in
update d_bc_transactions set exported_py='X'
where exported_py='F'
and Scan_Date < @CutOffDate
and status_code in (select status_code from c_status_ctrl where is_press='T' or is_collate='T')
if @@error<>0
	set @result='Error updating d_bc_transactions'

--insert to the payroll integration table
set @sql='
insert d_py_integration
(emp_no, docket_id, pct_complete, setup_hrs, run_hrs, py_rate, exported, py_calc_amount, emp_first_name, emp_last_name, 
entry_date, scan_id, is_press, is_collate, docket_type, docket_number, username, comment, status_code)

select e.emp_no, t.docket_id, t.pct_complete, 0, 0, ISNULL(SC.HrPayRate, 0), ''F'', 0, e.emp_first_name, e.emp_last_name,
t.scan_date, t.pk_id, s.is_press, s.is_collate, d.oh_type, d.oh_num, '''+@username+''', ''Imported from scanner'', t.status_code

from d_bc_transactions t
join '+@hr_db+'..employee e on e.emp_no=t.emp_no
join c_status_ctrl s on s.status_code=t.status_code and (s.is_press=''T'' or s.is_collate=''T'')
join d_head d on d.oh_id=t.docket_id
left join D_StatusBadgeSwipe_Ctrl SC ON SC.Swipe_Code = t.Swipe_Code
--left outer join '+@hr_db+'..emp_earn ee on ee.emp_no=e.emp_no and ee.ee_cycle=''z''
where exported_py=''X'''

exec(@sql)

if @@error<>0
	set @result='Error inserting d_py_integration'

--set @sql='update d_py_integration set py_group=ee.eg_code, py_code=ee.ec_code from '+@hr_db+'..emp_earn ee
--where ee.emp_no=d_py_integration.emp_no and ee.ee_cycle=''z'''

set @sql='update d_py_integration set py_group=SW.eg_code, py_code=SW.ec_code
from D_BC_Transactions TX LEFT OUTER JOIN
D_StatusBadgeSwipe_Ctrl SW ON (SW.Swipe_Code = TX.Swipe_Code)
WHERE TX.pk_id = Scan_ID'
exec(@sql)
if @@error<>0
	set @result='Error updating payroll group/code'

--flag the records as being updated
update d_bc_transactions set exported_py='T', exported_py_username=@username where exported_py='X'
if @@error<>0
	set @result='Error updating d_bc_transactions'



--calculate the payroll amount

declare
@pyi_id int,
@rate money,
@amount money,
@press_collate varchar(10),
@docket_type char(1),
@docket_id int,
@q_id int,
@qg_id int,
@setup_hours decimal (18,3),
@run_hours decimal(18,3),
@total_hours decimal(18,3),
@press_const varchar(10),
@collate_const varchar(10)

declare cur cursor for
select pk_id from d_py_integration where recalc='T'
open cur
fetch cur into @pyi_id
while @@fetch_status=0
begin
	set @press_const='CHR'
	set @collate_const='PHR'
	
	select @press_collate=case when p.is_press='T' then @press_const when p.is_collate='T' then @collate_const else 'X' end,
	@docket_type=docket_type, @docket_id=docket_id, @rate=PY_Rate
	from d_py_integration p where pk_id=@pyi_id
	
	
	if @press_collate<>'X'
	begin
	--get the rate
--	   select @rate=isnull(gcvalue,0) from cb_gcontrol where dkttype=@docket_type and gccode=@press_collate
--	   if @rate is null
--		set @rate=0
	
	   select @q_id=q_id, @qg_id=qg_id from d_head where oh_id=@docket_id
	--get the hours
	   if @press_collate=@press_const
		select @setup_hours=isnull(qg_goldpress_setuphr,0), @run_hours=isnull(qg_goldpress_runhr,0) from q_group where qg_id=@qg_id
	   else if @press_collate=@collate_const
		select @setup_hours=isnull(qg_goldcollate_setuphr,0), @run_hours=isnull(qg_goldcollate_runhr,0) from q_group where qg_id=@qg_id
	   else
		select @setup_hours=0, @run_hours=0
	   
	   set @total_hours=@setup_hours+@run_hours
	   set @total_hours=round(@total_hours,3)
	
	--multiply rate*hours
	   select @amount= @total_hours*@rate
	
	--update the record so we have a complete audit trail
	   update d_py_integration set setup_hrs=@setup_hours, run_hrs=@run_hours, total_hrs=@total_hours, py_rate=@rate, py_calc_amount=ROUND(@amount*(pct_complete/100), 2), qg_id=@qg_id, qg_desc=q.qg_desc, earn_ded='E', recalc='F'
	   from q_group q where q.qg_id=@qg_id
	   and d_py_integration.pk_id=@pyi_id
	   if @@error<>0
		set @result='Error updating d_py_integration'
	
	end
	fetch cur into @pyi_id

end
close cur
deallocate cur



if @result='OK'
	commit tran
else
	rollback tran



select * from d_py_integration










































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_LoadWorkingPYIntegration]') and xtype = 'P ')  
 drop Procedure sp_LoadWorkingPYIntegration
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE procedure sp_LoadWorkingPYIntegration
as
delete working_py_integration
insert working_py_integration
(pk_id, emp_no, docket_id, docket_type, docket_number, pct_complete, setup_hrs, run_hrs, total_hrs,
py_rate, py_calc_amount, emp_first_name, emp_last_name, py_group, py_code, entry_date, earn_ded,
manual_entry, username, comment, py_selected, status_code, adj_amount, adj_operator, rejected, rejected_operator,
is_collate, is_press, scan_id)

select
pk_id, emp_no, docket_id, docket_type, docket_number, pct_complete, setup_hrs, run_hrs, total_hrs,
py_rate, py_calc_amount, emp_first_name, emp_last_name, py_group, py_code, entry_date, earn_ded,
manual_entry, username, comment, py_selected, status_code, adj_amount, adj_operator, rejected, rejected_operator,
is_collate, is_press, scan_id

from d_py_integration where exported='F'


























































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_LoadWorkingPYIntegrationRange]') and xtype = 'P ')  
 drop Procedure sp_LoadWorkingPYIntegrationRange
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE procedure [dbo].[sp_LoadWorkingPYIntegrationRange]
@FromDate datetime,
@ToDate datetime
as
delete working_py_integration
insert working_py_integration
(pk_id, emp_no, docket_id, docket_type, docket_number, pct_complete, setup_hrs, run_hrs, total_hrs,
py_rate, py_calc_amount, emp_first_name, emp_last_name, py_group, py_code, entry_date, earn_ded,
manual_entry, username, comment, py_selected, status_code, adj_amount, adj_operator, rejected, rejected_operator,
is_collate, is_press, scan_id)

select
pk_id, emp_no, docket_id, docket_type, docket_number, pct_complete, setup_hrs, run_hrs, total_hrs,
py_rate, py_calc_amount, emp_first_name, emp_last_name, py_group, py_code, entry_date, earn_ded,
manual_entry, username, comment, py_selected, status_code, adj_amount, adj_operator, rejected, rejected_operator,
is_collate, is_press, scan_id

from d_py_integration where exported='F'
and Entry_Date >= @FromDate
and Entry_Date < @ToDate









































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_Presstin]') and xtype = 'P ')  
 drop Procedure sp_Presstin
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go


-- sp_Presstin 'AND Current_Status=''ICOLL'''

CREATE PROCEDURE [dbo].[sp_Presstin]
  @and_clause varchar(2000)
 AS

declare @tr_db varchar(200)
select @tr_db=isnull(tr_db,'') from database_setup

declare @sql varchar(5000)

set @sql=
'
SELECT D.oh_ID, D.oh_type, D.oh_num, D.oh_ship_date, D.oh_proof_yn,
CASE WHEN (LEN(D.oh_bookin) > 0) OR (LEN(D.oh_at) > 0) OR (LEN(D.oh_pts) > 0) OR (LEN(D.oh_BookShrinkIn) > 0) OR (LEN(CAST(D.oh_BookNote AS Varchar(2))) > 1)  THEN ''Y'' ELSE ''N'' END AS BIND_YN,
CASE WHEN (LEN(D.oh_micr_from) > 0) THEN ''Y'' ELSE ''N'' END AS CODA_YN,
CASE WHEN (D.oh_cut_sheet = ''Y'') THEN ''Y'' ELSE ''N'' END AS CUT_YN,
Q.Q_Width, Q.Q_Length, Q.Customer_ID, VC.NAME, Q.Q_ClientName, VC.TERRITORY_ID, T.TERRITORY_DESC,
STAT.Current_Status, STAT.Date_Last_Changed, DATEDIFF(dd, STAT.Date_Last_Changed, GETDATE()) AS Status_Days, CSC.Status_Desc AS Current_Status_Desc,
STAT.Current_Dept, STAT.Date_Last_Dept_Chg, DATEDIFF(dd, STAT.Date_Last_Dept_Chg, GETDATE()) AS Dept_Days, DEPT.Dept_Description AS Current_Dept_Desc,
QG.QG_Parts, QG.QG_Qty, QG.QG_DealerTotCost, QG_GoldPress_SetupHr + QG_GoldPress_RunHr AS HOURS_PRESS, QG_GoldCollate_SetupHr + QG_GoldCollate_RunHr AS HOURS_COLLATE,
QG.QG_Desc,
RTRIM(Q.Q_Ink1) +
 CASE WHEN (LEN(RTRIM(Q.Q_Ink2)) = 0) THEN '''' ELSE '','' + RTRIM(Q.Q_Ink2) END +
 CASE WHEN (LEN(RTRIM(Q.Q_Ink3)) = 0) THEN '''' ELSE '','' + RTRIM(Q.Q_Ink3) END +
 CASE WHEN (LEN(RTRIM(Q.Q_Ink4)) = 0) THEN '''' ELSE '','' + RTRIM(Q.Q_Ink4) END +
 CASE WHEN (LEN(RTRIM(Q.Q_Ink5)) = 0) THEN '''' ELSE '','' + RTRIM(Q.Q_Ink5) END AS Inks
FROM D_Head D
LEFT OUTER JOIN Q_Head Q ON Q.Q_ID = D.Q_ID
LEFT OUTER JOIN Q_Group QG ON D.QG_ID = QG.QG_ID
LEFT OUTER JOIN ' + @tr_db + '..CUSTOMERS VC ON Q.Customer_ID= VC.CUSTOMER_ID
LEFT OUTER JOIN ' + @tr_db + '..TERRITORY T ON T.TERRITORY_ID = VC.TERRITORY_ID
LEFT OUTER JOIN D_Status STAT ON STAT.Docket_ID = D.oh_ID
LEFT OUTER JOIN C_Status_Ctrl CSC ON CSC.Status_Code = STAT.Current_Status
LEFT OUTER JOIN C_Dept_Ctrl DEPT ON DEPT.Dept_Code = STAT.Current_Dept
WHERE D.oh_status != ''N''
' +
@and_clause

exec(@sql)










































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_QuickEQuery]') and xtype = 'P ')  
 drop Procedure sp_QuickEQuery
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go



CREATE proc [dbo].[sp_QuickEQuery] @UserName varchar(50), @Customer_ID varchar(20) as
--declare @UserName varchar(50), @Customer_ID varchar(20) select @username = 'HM', @Customer_ID = '%'
BEGIN

    declare @sql varchar(8000), @tr varchar(50)
    select @tr=isnull(tr_db,'') from database_setup

	if exists (select * from tempdb..sysobjects where id = object_id(N'tempdb..#OrderDetails'))
	drop table #OrderDetails
	CREATE TABLE #OrderDetails(
	[Source] [varchar](2) NULL,
	[oh_type] [varchar](1) NULL,
	[oh_num] [int] NULL,
	invoiceNumber varchar(30),
	[oh_ID] [int] NULL,
	[Customer_ID] [int] NULL,
	[oh_whs] [varchar](3) NULL,
	[Dollars_Entered] [money] NULL,
	[Dollars_Invoiced] [money] NULL,
	[One_If_Entered] [smallint] NULL,
	[One_If_Invoiced] [smallint] NULL,
	Q_ID int null,
	QG_ID int,
	Sls_id int,
	Date datetime
)
----------------------------------------------------

    SET @sql ='insert #OrderDetails   
    SELECT ''DE'', DH.oh_type, DH.oh_num, -1, DH.oh_ID, QH.Customer_ID, DH.oh_Whs, DH.oh_net_cost, 0, 1, 0, dh.q_id, QG_ID, DH.sls_id_inside, oh_Nrcv_date
	FROM D_Head DH
    LEFT OUTER JOIN Q_Head QH ON QH.Q_ID = DH.Q_ID
 	WHERE (not oh_status in (''H'', ''N'', ''Z'')) and QH.Customer_ID '+
 	(case when @Customer_ID = '%' then ' like ' else ' = ' end) + ''''+@Customer_ID+''' 
	union
    SELECT ''DI'', DH.oh_type, DH.oh_num, -1, DH.oh_ID, QH.Customer_ID, DH.oh_Whs,
    0 AS Dollars_Entered, ISNULL(STH.Total_Products,0) + ISNULL(STH.TOTAL_OTHER_MISC,0) AS Dollars_Invoiced, 0 AS One_If_Entered, 1 AS One_If_Invoiced, 
	dh.q_id, QG_ID, DH.sls_id_inside, oh_Nrcv_date
	FROM D_Head DH
    LEFT OUTER JOIN Q_Head QH ON QH.Q_ID = DH.Q_ID
    JOIN ' + @tr + '..SO_TRN_HDR STH ON STH.SO_TRN_ID = DH.R2B_SO_TRN_ID
 	WHERE oh_status = ''X'' and QH.Customer_ID '+
 	(case when @Customer_ID = '%' then ' like ' else ' = ' end) + ''''+@Customer_ID+'''
	union
    SELECT ''MI'', ''M'', -1 AS oh_num, invoiceno,-1 as oh_ID, STH.Customer_ID, WHS.WAREHOUSE,
    0 AS Dollars_Entered, ISNULL(STH.Total_Products,0) + ISNULL(STH.TOTAL_OTHER_MISC,0) AS Dollars_Invoiced, 0 AS One_If_Entered, 1 AS One_If_Invoiced, 
	-1, -1, sth.sls_id, Invoice_Date
	FROM ' + @tr + '..SO_TRN_HDR STH
    JOIN ' + @tr + '..SO_MASTER_HDR SMH ON SMH.SO_ID = STH.SO_ID
    LEFT JOIN ' + @tr + '..WAREHOUSE WHS ON WHS.WHSE_ID = SMH.WHSE_ID
 	WHERE (STH.SO_ID <> 0)  and STH.Customer_ID '+
 	(case when @Customer_ID = '%' then ' like ' else ' = ' end) + ''''+@Customer_ID+'''
'
    exec(@sql)

--select * from #OrderDetails where oh_id = -1
--select  ReC_Desc [RerunCategory], Rerun_Reason [RerunReason], Q_Num [QuoteNumber], Q_Whs [Warehouse], ReRun_Qty_Affected [RerunQty], 
--ReRun_Notes [RerunNotes], oh_num [DocketNumber], *
--from #OrderDetails dh
--join dbo.Q_Head h
--on dh.q_id = h.q_id
--left outer join dbo.CQ_RerunCategory rc
--on h.ReC_ID = rc.ReC_ID
--left outer join dbo.CQ_RerunReason rr
--on rr.Rerun_ID = h.Rerun_ID
print 'hey'
    SET @sql ='delete working_QuickE where username = ''' + @UserName + ''' 
		insert working_QuickE
		SELECT [Source], D.oh_type, CUST.TERRITORY_ID TerritoryCode, TER.TERRITORY_DESC Territory, D.oh_Whs AS WarehouseCode, WHS.DESCRIPTION AS Warehouse,
		d.SLS_ID AS Salesman_ID, ISNULL(SLS.FIRSTNAME,'''') + '' '' + ISNULL(SLS.LASTNAME,'''') AS SalesmanName, 
		cust.name Dealer, Customer_code DealerCode, Q_ClientName ClientName,
		oh_num [DocketNumber], InvoiceNumber, Q_Num [QuoteNumber], 
		QG_Qty Qty, QG_Desc StockName, QG_Parts Parts,
		D.Dollars_Entered AS Dollars_Entered,
        D.Dollars_Invoiced as Dollars_Invoiced,
		ReC_Desc [RerunCategory], Rerun_Reason [RerunReason], ReRun_Qty_Affected [RerunQty], 
		ReRun_Notes [RerunNotes], 
		''' + @UserName + ''' Username, d.Date
	FROM #OrderDetails D
    LEFT OUTER JOIN ' + @tr + '..CUSTOMERS CUST ON CUST.CUSTOMER_ID = D.Customer_ID
    LEFT OUTER JOIN ' + @tr + '..SALESPERSONS SLS ON SLS.SLS_ID = d.SLS_ID
    LEFT OUTER JOIN ' + @tr + '..TERRITORY TER ON TER.TERRITORY_ID = CUST.TERRITORY_ID
    LEFT OUTER JOIN ' + @tr + '..WAREHOUSE WHS ON WHS.WAREHOUSE = D.oh_Whs
left outer join dbo.Q_Head h
on d.q_id = h.q_id
left outer join dbo.CQ_RerunCategory rc
on h.ReC_ID = rc.ReC_ID
left outer join dbo.CQ_RerunReason rr
on rr.Rerun_ID = h.Rerun_ID
left outer join Q_Group qg
on qg.qg_id = d.QG_ID
--where invoicenumber <> -1

'
--   SELECT @sql
    exec(@sql)

END


Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_QuoteGroups]') and xtype = 'P ')  
 drop Procedure sp_QuoteGroups
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go


-- exec sp_QuoteGroups 657100
CREATE  proc [dbo].[sp_QuoteGroups]
    @Q_ID int
as

SET NOCOUNT ON

declare @sql_str varchar(2000)

if exists (select * from tempdb..sysobjects where id = object_id(N'tempdb..#qt_grp'))
drop table #qt_grp

create table #qt_grp
( q_id int null,
 group_num int null,
 group_desc varchar(30) null,
 group_parts int null,
 group_qty int null,
 group_base_flat money null,
 group_base_run_per_m money null,
 group_tot_flat money null,
 group_tot_run_per_m money null,
 group_tot_price_per_m money null,
 group_lot_price money null,
 group_discount money null,
 group_cost_per_m money null,
 group_cost money null,
 group_shipping money null,
 group_cost_per_m_plus_ship_per_m money null,
 group_total money null,
 group_price money null,
 group_price_with_shipping money null,
 TOT_group_price_with_shipping money null,
 GroupingYN varchar(1))

set @sql_str='
select '+cast(@Q_ID as varchar)+', qg.qg_num, qg.qg_desc, qg.qg_parts, qg.qg_qty,
qg.qg_baseflat, qg.qg_BaseRunPerM,qg.qg_TotFlat,qg.qg_TotRunPerM,0,
qg.qg_LotPrice,qg.qg_DiscountPercent,0, qg.QG_DealerCostLot, qg.QG_Shipping, 0, 0, 0,
qg.QG_DealerTotCost, 0,
qh.Q_GroupingYN
from q_group qg
left outer join Q_Head qh ON qh.Q_ID = qg.Q_ID 
where qg.q_id='+cast(@Q_ID as varchar)

insert #qt_grp
exec(@sql_str)

update #qt_grp
set group_tot_price_per_m=((group_lot_price * 1000)/group_qty)

update #qt_grp
set group_cost_per_m=(group_cost * 1000) / group_qty

update #qt_grp
set group_price=(select sum(group_cost) from #qt_grp)
where group_num=1

update #qt_grp
set group_total = group_cost + group_shipping

update #qt_grp
set group_cost_per_m_plus_ship_per_m = group_cost_per_m + ((group_shipping * 1000) / group_qty)

update #qt_grp
set TOT_group_price_with_shipping = (select sum(group_price_with_shipping) from #qt_grp)

select * from #qt_grp













Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_QuoteInfo]') and xtype = 'P ')  
 drop Procedure sp_QuoteInfo
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

CREATE  proc [dbo].[sp_QuoteInfo]
    @Q_ID int
as

SET NOCOUNT ON

declare @QG_NUM int,
        @ID int,
        @RecCount int,
        @RecNum int,
        @PaperColorCatString varchar(100),
        @DlrColor varchar(10),
        @Q_GroupingYN varchar(1)


if exists (select * from tempdb..sysobjects where id = object_id(N'tempdb..#tmpGroup'))
drop table #tmpGroup
if exists (select * from tempdb..sysobjects where id = object_id(N'tempdb..#tmpPaper'))
drop table #tmpPaper

create table #tmpGroup
(
  QG_NUM int null,
  CommaColors varchar(50) null
)

set @QG_NUM = 1

while @QG_NUM < 6
begin

  create table #tmpPaper
  (
    id int IDENTITY(1,1) not null,
    DlrColor varchar(3) null
  )

  insert #tmpPaper
  select C.PaperColorDlrAbbrev 
  from Q_Parts QP
  left join CB_PaperSpec PS on PS.PaperSpecID = QP.PaperSpecID
  left join CB_PaperColor C on C.PaperColorCode = PS.PaperColorCode
  where QP.Q_ID = @Q_ID
  and QP.QG_NUM = @QG_NUM
  order by QP.pi_seq

  SELECT @RecCount = max(id) FROM #tmpPaper
  SET @RecNum = 1
  SET @PaperColorCatString = ''

--  process each rec
  while @RecNum <= @RecCount
  begin
    SELECT @DlrColor = ISNULL(DlrColor,'')
    FROM #tmpPaper
    WHERE id = @RecNum

    if  (LEN(@PaperColorCatString) > 0) 
      set @PaperColorCatString = @PaperColorCatString + ','

    set @PaperColorCatString = @PaperColorCatString + @DlrColor

    set @RecNum = @RecNum + 1
  end

  if  (LEN(@PaperColorCatString) > 0)
  begin
    INSERT #tmpGroup
    select @QG_NUM, @PaperColorCatString
  end

--  select * from #tmpPaper

  drop table #tmpPaper

  set @QG_NUM = @QG_NUM + 1
end

-- SELECT * FROM #tmpGroup

SELECT @Q_GroupingYN = Q_GroupingYN
FROM Q_Head
WHERE Q_ID = @Q_ID

if  (@Q_GroupingYN = 'Y')
begin
    SELECT QP.pi_seq, QP.PaperSpecID, Pap.MWeight, Pap.PaperColorCode, Pap.PaperType, QG.QG_Num, QG.QG_Desc, QG.QG_Qty, QG.QG_Parts, TG.CommaColors, 'Y' as Q_Grouping
	FROM Q_Parts QP
	LEFT JOIN CB_PaperSpec Pap ON Pap.PaperSpecID = QP.PaperSpecID
	LEFT JOIN Q_Group QG ON QG.Q_ID = QP.Q_ID AND QG.QG_Num = QP.QG_NUM
	LEFT JOIN #tmpGroup TG ON TG.QG_Num = QG.QG_Num
	WHERE QP.Q_ID = @Q_ID
	ORDER BY QG.QG_Num, QP.pi_seq
end
else
begin
	SELECT QP.pi_seq, QP.PaperSpecID, Pap.MWeight, Pap.PaperColorCode, Pap.PaperType, QG.QG_Num, QG.QG_Desc, QG.QG_Qty, QG.QG_Parts, TG.CommaColors, 'N' as Q_Grouping
	FROM Q_Parts QP
	LEFT JOIN CB_PaperSpec Pap ON Pap.PaperSpecID = QP.PaperSpecID
	LEFT JOIN Q_Group QG ON QG.Q_ID = QP.Q_ID AND QG.QG_Num = QP.QG_NUM
	LEFT JOIN #tmpGroup TG ON TG.QG_Num = 1
	WHERE QP.Q_ID = @Q_ID
	ORDER BY QG.QG_Num, QP.pi_seq
end






Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_QuoteOptions]') and xtype = 'P ')  
 drop Procedure sp_QuoteOptions
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go








-- exec sp_QuoteOptions 568541
-- exec sp_QuoteOptions 568559

CREATE  proc [dbo].[sp_QuoteOptions]
    @Q_ID int
as

declare @sql_str varchar(2000)

if exists (select * from tempdb..sysobjects where id = object_id(N'tempdb..#qt_opt'))
drop table #qt_opt

create table #qt_opt
(
 opt_seq int IDENTITY(1,1) not null,
 q_id int null,
 opt_desc varchar(45) null,
 opt_qty varchar(50) null,
 opt_flat money null,
 opt_run money null,
 opt_form varchar(10) null,
 opt_SortRPT int,
 opt_SortRPT2 int,
 Option_1Line_Desc varchar(200) null)

set @sql_str='
select '+cast(@Q_ID as varchar)+', og_desc+'' ''+ogi.ogi_desc, ValueDesc, qo.QO_CalcDet_Flat, qo.QO_CalcDet_Run, null, og.QG_SortRPT, ogi.OGI_Sort,
og_desc+'' ''+ogi.ogi_desc + '' '' + ValueDesc
from q_options qo
join cq_optgrpitm ogi on qo.ogi_id=ogi.ogi_id
join cq_optgrp og on ogi.og_id=og.og_id
where og.OG_Column>0 and qo.q_id='+cast(@Q_ID as varchar)

insert #qt_opt
exec(@sql_str)

declare @QTyp varchar(5)

SELECT @QTyp = Q_QType FROM Q_Head WHERE Q_ID = @Q_ID

if  ((@QTyp = 'RLAB') OR (@QTyp = 'DLAB'))
begin
	set @sql_str='
	select q_id, ''Wind Direction: '' +
	CASE ISNULL(Q_RLAB_WindDirNotRequestedTF, ''F'')
	WHEN ''F'' THEN
	CASE Q_RLAB_WindDir
	WHEN 1 THEN ''Top First''
	WHEN 2 THEN ''Bottom First''
	WHEN 3 THEN ''Right First''
	WHEN 4 THEN ''Left First''
	ELSE ''Unspecified''
	END
	ELSE ''Not Requested by Dealer''
	END
	 AS opt_desc,
	''0'', 0, 0, null, 99999, 99999,
	''Wind Direction: '' +
	CASE ISNULL(Q_RLAB_WindDirNotRequestedTF, ''F'')
	WHEN ''F'' THEN
	CASE Q_RLAB_WindDir
	WHEN 1 THEN ''Top First''
	WHEN 2 THEN ''Bottom First''
	WHEN 3 THEN ''Right First''
	WHEN 4 THEN ''Left First''
	ELSE ''Unspecified''
	END
	ELSE ''Not Requested by Dealer''
	END
	 AS opt_1Linedesc
	from Q_Head
	where Q_ID='+cast(@Q_ID as varchar)

	insert #qt_opt
	exec(@sql_str)
end

select * from #qt_opt order by opt_SortRPT, opt_SortRPT2



























































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_QuotePrint]') and xtype = 'P ')  
 drop Procedure sp_QuotePrint
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go




CREATE   proc [dbo].[sp_QuotePrint]
    @Q_ID int, @sum_det char(1)
as
SET NOCOUNT ON;

declare
 @server_name varchar(50),
 @db_name varchar(50),
 @sql_str varchar(5000),
 @RunLengthLW char(1),
 @Min_pi_size_value float,
 @Min_pi_size varchar(12),
 @Max_pi_size_value float,
 @Max_pi_size varchar(12),
 @Q_Width varchar(12),
 @Q_Length varchar(12),
 @Act_Dim1 varchar(12),
 @Act_Dim2 varchar(12),
 @Q_Repeat_Q_ID int,
 @Repeat_Q_Whs varchar(3),
 @Repeat_Q_Num int,
 @Repeat_QuoteNum varchar(10),
 @Repeat_DktNumList varchar(60),
 @ID int,
 @RecCount int,
 @RecNum int,
 @DktNum varchar(10),
 @AnotherRepeat_DktNumList varchar(20),
 @StockDesc varchar(5000),
 @QType varchar(5),
 @RLAB_PaperShortDesc varchar(15),
 @RLAB_PaperFullDesc varchar(40)

-- @Promo_Note varchar(512)
-- 
--if exists (select CWTX_Type from CW_CtlText WHERE CWTX_Whs='ALL' AND CWTX_Type='QUOTE_PROMO_NOTE')
--  select @Promo_Note=ISNULL(CWTX_Text, '') from CW_CtlText where CWTX_Whs='ALL' AND CWTX_Type='QUOTE_PROMO_NOTE'
--else
--  select @Promo_Note = 'No Note'

select @server_name=tr_server_name, @db_name=tr_db from database_setup

if exists (select * from tempdb..sysobjects where id = object_id(N'tempdb..#quote'))
drop table #quote

create table #quote
(sum_det char(1) null,
 q_id int null,
 q_whs varchar(3) null,
 q_num int null,
 q_status int null,
 sls_id int null,
 sman_code varchar(5) null,
 sman_desc varchar(50) null,
 customer_id int null,
 cust_code varchar(5) null,
 cust_name varchar(50) null,
 cust_addr1 varchar(50) null,
 cust_addr2 varchar(50) null,
 cust_addr3 varchar(50) null,
 cust_city varchar(50) null,
 cust_prov varchar(5) null,
 cust_postal varchar(15) null,
 cust_phone varchar(20) null,
 cust_fax varchar(20) null,
 cust_client_name varchar(50) null,
 job_type varchar(20) null,
 paper varchar(20) null,
 grouped_forms int null,
 quote_date datetime null,
 cust_attn varchar(50) null,
 form_desc varchar(50) null,
 form_parts int null,
 form_size varchar(20) null,
 ink1 varchar(5) null,
 ink2 varchar(5) null,
 ink3 varchar(5) null,
 ink4 varchar(5) null,
 ink5 varchar(5) null,
 inkDesc1 varchar(10) null,
 inkDesc2 varchar(10) null,
 inkDesc3 varchar(10) null,
 inkDesc4 varchar(10) null,
 inkDesc5 varchar(10) null,
 qc_id int null,
 company_name varchar(50) null,
 whs_desc varchar(50) null,
 whs_phone varchar(20) null,
 whs_fax varchar(20) null,
 whs_tf_phone varchar(25) null,
 whs_tf_fax varchar(25) null,
 promo_note text null,
 GeneralNotes text null,
 GroupingYN varchar(1) null,
 TOLL_FREE_FAX varchar(20) null,
 Repeat_QuoteNum varchar(10) null,
 Repeat_DktNumList varchar(60) null,
 Q_Repeat_NEC varchar(1),
 AnotherRepeatDktNumList varchar(20) null,
 StockDesc text null,
 RLAB_PaperShortDesc varchar(15) null,
 RLAB_PaperFullDesc varchar(40) null,
 DieCutSize varchar(20) null,
 WindDirectionImage image null,
 Q_RLAB_RollSize varchar(10) null,
 Q_RLAB_LabelsAcross int null,
 Q_FBC1 varchar(1) null,
 Q_FBC2 varchar(1) null,
 Q_FBC3 varchar(1) null,
 Q_FBC4 varchar(1) null,
 Q_FBC5 varchar(1) null,
 Q_Ink1_Plates int null,
 Q_Ink2_Plates int null,
 Q_Ink3_Plates int null,
 Q_Ink4_Plates int null,
 Q_Ink5_Plates int null 
)
-- create index idx_id on #quote(q_id)


-- Setup L x W String by searching for largest L and W for all Q_Parts Recs
select @RunLengthLW = DT.RunLengthLW, @Q_Width = Q_Width, @Q_Length = Q_Length, @QType=Q_QType
from Q_Head QH
left join C_QuoteType QT on QT.QType = QH.Q_QType
left join C_DocketType DT on DT.DktType = QT.DktType
where QH.Q_ID= cast(@Q_ID as varchar)

select @Min_pi_size_value = MIN(CBF.FractionValue)
from Q_Parts QP
left join CB_Fraction CBF ON CBF.FractionString = QP.pi_size
where QP.Q_ID= cast(@Q_ID as varchar)

select @Min_pi_size = FractionString
from CB_Fraction
where FractionValue = @Min_pi_size_value

select @Max_pi_size_value = MAX(CBF.FractionValue)
from Q_Parts QP
left join CB_Fraction CBF ON CBF.FractionString = QP.pi_size
where QP.Q_ID= cast(@Q_ID as varchar)

select @Max_pi_size = FractionString
from CB_Fraction
where FractionValue = @Max_pi_size_value

if  ((@QType = 'RLAB') OR (@QType = 'DLAB'))
begin
  select @StockDesc = ISNULL(PS.StockDesc, ''), @RLAB_PaperShortDesc = ISNULL(PS.ShortDesc,''), @RLAB_PaperFullDesc = ISNULL(PS.FullDesc,'')
  from Q_Parts QP
  left join CB_PaperSpec PS on PS.PaperSpecID = QP.PaperSpecID
  WHERE QP.Q_ID=cast(@Q_ID as varchar) AND QP.QG_NUM=1 
end
else
begin
  set @StockDesc = ''
  set @RLAB_PaperShortDesc = ''
  set @RLAB_PaperFullDesc = ''
end

if (@RunLengthLW = 'W')
begin
  set @Act_Dim1 = @Q_Width

  if  (@Min_pi_size = @Max_pi_size)
    set @Act_Dim2 = @Min_pi_size
  else
    set @Act_Dim2 = 'VAR'
end
else
begin
  if  (@Min_pi_size = @Max_pi_size)
    set @Act_Dim1 = @Min_pi_size
  else
    set @Act_Dim1 = 'VAR'

  set @Act_Dim2 = @Q_Length
end

--select @Act_Dim1 + ' x ' + @Act_Dim2 as REAL_SIZE

SELECT @Q_Repeat_Q_ID=ISNULL(Q_Repeat_Q_ID,-1)
FROM Q_Head
WHERE Q_ID=@Q_ID

set @Repeat_QuoteNum = ''
set @Repeat_DktNumList = ''

if @Q_Repeat_Q_ID <> -1
begin
  SELECT @Repeat_Q_Whs = ISNULL(Q_Whs, ''), @Repeat_Q_Num = ISNULL(Q_Num, -1)
  FROM Q_Head
  WHERE Q_ID = @Q_Repeat_Q_ID

  if  (@Repeat_Q_Num = -1)
    set @Repeat_QuoteNum = ''
  else
  begin
    set @Repeat_QuoteNum = @Repeat_Q_Whs + CAST(@Repeat_Q_Num as varchar)

    create table #tmpDkt
	(
	  id int IDENTITY(1,1) not null,
	  DktNum varchar(10) null
	)

    INSERT #tmpDkt
    SELECT oh_type + ' ' + CAST(oh_num as varchar)
    FROM D_Head
    WHERE Q_ID = @Q_Repeat_Q_ID

    SELECT @RecCount = max(id) FROM #tmpDkt
    SET @RecNum = 1

--  process each rec
    while @RecNum <= @RecCount
    begin
      SELECT @DktNum = ISNULL(DktNum,'')
      FROM #tmpDkt
      WHERE id = @RecNum

      if  (LEN(@Repeat_DktNumList) > 0) 
        set @Repeat_DktNumList = @Repeat_DktNumList + ', '

      set @Repeat_DktNumList = @Repeat_DktNumList + @DktNum

      set @RecNum = @RecNum + 1
    end
  end
end

--  Build String of Repeat Dockets as <One Docket Num> (# of Dockets)
declare @C_Q_Repeat_Q_ID int

set @AnotherRepeat_DktNumList=''

select @C_Q_Repeat_Q_ID = ISNULL(Q_Repeat_Q_ID, -1)
FROM Q_Head where Q_ID=@Q_ID

if  (@C_Q_Repeat_Q_ID <> -1)
begin
	declare @cnt int

	select @cnt = count(*) from D_Head
	where Q_ID=
	(SELECT Q_Repeat_Q_ID FROM Q_Head where Q_ID=@Q_ID)

	if  (@cnt = 1)
	begin
		SELECT @AnotherRepeat_DktNumList = oh_type + ' ' + CAST(min(oh_num) as varchar)
		FROM D_Head
		WHERE Q_ID = 
		(SELECT Q_Repeat_Q_ID FROM Q_Head where Q_ID=@Q_ID)
		GROUP BY oh_type
	end

	if  (@cnt > 1)
	begin
		SELECT @AnotherRepeat_DktNumList = oh_type + ' ' + CAST(min(oh_num) as varchar) + ' (' + CAST(count(*) as varchar) + ')'
		FROM D_Head
		WHERE Q_ID = 
		(SELECT Q_Repeat_Q_ID FROM Q_Head where Q_ID=@Q_ID)
		GROUP BY oh_type
	 end
   end

--exec @AnotherRepeat_DktNumList = sp_GetRepeatDocketNums @Q_ID


set @sql_str='
select '''+@sum_det+''', qh.q_id, qh.q_whs, qh.q_num, qh.q_status, qh.sls_id,
ISNULL(sm.salesperson_code,''''), UPPER(ISNULL(sm.firstname,'''')) AS firstname,
qh.customer_id, c.customer_code, UPPER(c.name) AS name,
UPPER(c.bill_address_1), UPPER(c.bill_address_2), UPPER(c.bill_address_3), UPPER(c.bill_city), UPPER(c.bill_state), UPPER(c.bill_zip),
c.telephone, c.fax,
qh.q_clientname,qt.QTypeShortDesc,qh.PaperCategory,0,
qh.q_DatePriced,qh.Q_Attn, UPPER(qg.qg_desc),qg.qg_parts, ''' + @Act_Dim1 + ' x ' + @Act_Dim2 + ''',qh.q_ink1,qh.q_ink2,qh.q_ink3,qh.q_ink4,qh.q_ink5,
ISNULL(COL1.Color_Desc, ''''), ISNULL(COL2.Color_Desc, ''''), ISNULL(COL3.Color_Desc, ''''), ISNULL(COL4.Color_Desc, ''''), ISNULL(COL5.Color_Desc, ''''),
0, UPPER(co.companyname), UPPER(wh.description), wh.phone, wh.fax, wh.phone2, wh.email,
isnull(TXT.CWTX_Text, ''''), isnull(qh.Q_GeneralNotes, ''''), qh.Q_GroupingYN, wh.TOLL_FREE_FAX,
''' + @Repeat_QuoteNum + ''', ''' + @Repeat_DktNumList + ''', qh.Q_Repeat_NEC, ''' + @AnotherRepeat_DktNumList + ''',
''' + @StockDesc + ''', ''' + @RLAB_PaperShortDesc + ''', ''' + @RLAB_PaperFullDesc + ''', ISNULL(qh.Q_RLAB_DieCutSize,''''),
CASE ISNULL(QH.Q_RLAB_WindDirNotRequestedTF, ''F'')
WHEN ''F'' THEN
CASE QH.Q_RLAB_WindDir
WHEN 1 THEN LS.LBC_WindTopFirstImg
WHEN 2 THEN LS.LBC_WindBottomFirstImg
WHEN 3 THEN LS.LBC_WindRightFirstImg
WHEN 4 THEN LS.LBC_WindLeftFirstImg
ELSE null
END
ELSE null
END, qh.Q_RLAB_RollSize, qh.Q_RLAB_LabelsAcross, qh.Q_FBC1, qh.Q_FBC2, qh.Q_FBC3, qh.Q_FBC4, qh.Q_FBC5,
qh.Q_Ink1_Plates, qh.Q_Ink2_Plates, qh.Q_Ink3_Plates, qh.Q_Ink4_Plates, qh.Q_Ink5_Plates
from q_head qh
left join '+@db_name+'..salespersons sm on qh.sls_id=sm.sls_id
join '+@db_name+'..customers c on c.customer_id=qh.customer_id
join '+@db_name+'..warehouse wh on wh.warehouse=qh.q_whs
join '+@db_name+'..company co on co.companyname<>''''
join q_group qg on qg.q_id=qh.q_id and qg.qg_num=1
left outer join C_QuoteType qt on qt.QType = qh.Q_QType
left outer join CW_CtlText TXT on TXT.CWTX_Whs=''ALL'' AND TXT.CWTX_Type=''QUOTE_PROMO_NOTE''
left join CB_InkColor COL1 ON COL1.Color_Code = qh.q_ink1
left join CB_InkColor COL2 ON COL2.Color_Code = qh.q_ink2
left join CB_InkColor COL3 ON COL3.Color_Code = qh.q_ink3
left join CB_InkColor COL4 ON COL4.Color_Code = qh.q_ink4
left join CB_InkColor COL5 ON COL5.Color_Code = qh.q_ink5
left join q_head rqh on rqh.Q_ID = qh.Q_Repeat_Q_ID
LEFT JOIN CB_LabelSetup LS ON (LS.LBC_Key = ''ROLL'') AND (QH.Q_QType = ''RLAB'') 
where qh.q_id='+cast(@Q_ID as varchar)

insert #quote
exec(@sql_str)

select * from #quote






















Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_QuotePrintDEBUG]') and xtype = 'P ')  
 drop Procedure sp_QuotePrintDEBUG
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go


-- exec sp_QuotePrintDEBUG 283250, 'S'
-- exec sp_QuotePrintDEBUG 769, 'D'
CREATE   proc [dbo].[sp_QuotePrintDEBUG]
    @Q_ID int, @sum_det char(1)
as
declare
 @server_name varchar(50),
 @db_name varchar(50),
 @sql_str varchar(5000)
-- @Promo_Note varchar(512)
-- 
--if exists (select CWTX_Type from CW_CtlText WHERE CWTX_Whs='ALL' AND CWTX_Type='QUOTE_PROMO_NOTE')
--  select @Promo_Note=ISNULL(CWTX_Text, '') from CW_CtlText where CWTX_Whs='ALL' AND CWTX_Type='QUOTE_PROMO_NOTE'
--else
--  select @Promo_Note = 'No Note'

select @server_name=tr_server_name, @db_name=tr_db from database_setup

if exists (select * from tempdb..sysobjects where id = object_id(N'tempdb..#quote'))
drop table #quote

create table #quote
(sum_det char(1) null,
 q_id int null,
 q_whs varchar(3) null,
 q_num int null,
 q_status int null,
 sls_id int null,
 sman_code varchar(5) null,
 sman_desc varchar(50) null,
 customer_id int null,
 cust_code varchar(5) null,
 cust_name varchar(50) null,
 cust_addr1 varchar(50) null,
 cust_addr2 varchar(50) null,
 cust_addr3 varchar(50) null,
 cust_city varchar(50) null,
 cust_prov varchar(5) null,
 cust_postal varchar(15) null,
 cust_phone varchar(20) null,
 cust_fax varchar(20) null,
 cust_client_name varchar(50) null,
 job_type varchar(20) null,
 paper varchar(20) null,
 grouped_forms int null,
 quote_date datetime null,
 cust_attn varchar(50) null,
 form_desc varchar(50) null,
 form_parts int null,
 form_size varchar(20) null,
 ink1 varchar(20) null,
 ink2 varchar(20) null,
 ink3 varchar(20) null,
 ink4 varchar(20) null,
 ink5 varchar(20) null,
 qc_id int null,
 comments varchar(255) null,
 notes varchar(255) null,
 company_name varchar(50) null,
 whs_desc varchar(50) null,
 whs_phone varchar(20) null,
 whs_fax varchar(20) null,
 whs_tf_phone varchar(25) null,
 whs_tf_fax varchar(25) null,
 promo_note text null,
 GeneralNotes text null
)

 create index idx_id on #quote(q_id)

set @sql_str='
select '''+@sum_det+''', qh.q_id, qh.q_whs, qh.q_num, qh.q_status, qh.sls_id, sm.salesperson_code,sm.firstname,
qh.customer_id, c.customer_code,c.name, c.bill_address_1,c.bill_address_2,c.bill_address_3,
c.bill_city,c.bill_state,c.bill_zip,c.telephone,c.fax,qh.q_clientname,qt.QTypeShortDesc,qh.PaperCategory,0,
qh.q_DatePriced,qh.Q_Attn,qg.qg_desc,qg.qg_parts,qh.q_width+'' x ''+qh.q_length,qh.q_ink1,qh.q_ink2,qh.q_ink3,qh.q_ink4,qh.q_ink5,
0,cq.comment,cq.notes,co.companyname,wh.description,wh.phone,wh.fax,wh.phone2,wh.email,
isnull(TXT.CWTX_Text, ''''), isnull(qh.Q_GeneralNotes, '''')
from q_head qh
join '+@db_name+'..salespersons sm on qh.sls_id=sm.sls_id
join '+@db_name+'..customers c on c.customer_id=qh.customer_id
join '+@db_name+'..warehouse wh on wh.warehouse=qh.q_whs
join '+@db_name+'..company co on co.companyname<>''''
join q_group qg on qg.q_id=qh.q_id and qg.qg_num=1
left outer join cq_comment cq on cq.comment<>''''
left outer join C_QuoteType qt on qt.QType = qh.Q_QType
left outer join CW_CtlText TXT on TXT.CWTX_Whs=''ALL'' AND TXT.CWTX_Type=''QUOTE_PROMO_NOTE''
where qh.q_id='+cast(@Q_ID as varchar)

select @sql_str

insert #quote
exec(@sql_str)

select * from #quote















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptBillInvoices]') and xtype = 'P ')  
 drop Procedure sp_RptBillInvoices
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go




-- sp_RptBillInvoices 'ABC Company', 14

CREATE PROCEDURE [dbo].[sp_RptBillInvoices]
  @CompanyName varchar(200),
  @USER_SLS_ID varchar(2000)
AS
SET NOCOUNT ON;


declare @sql varchar(5000), @tr_db varchar(50)

select @tr_db=isnull(tr_db,'') from database_setup

set @sql=
'
SELECT ''' + @CompanyName + ''' AS ReportCompanyName, RPT.seq, DH.R2B_BillBatchNumber, DH.R2B_InvoiceNumber As InvoiceNumber,
 CST.NAME, CST.BILL_ADDRESS_1, CST.BILL_ADDRESS_2, CST.BILL_ADDRESS_3,
 CST.BILL_CITY, CST.BILL_STATE, CST.BILL_ZIP, 
 DH.oh_ship_name, DH.oh_ship_suite, DH.oh_ship_addr, DH.oh_ship_city, DH.oh_ship_prov, DH.oh_ship_postal,
 DH.Rel2BillComment,
 CASE UPPER(LTRIM(RTRIM(ISNULL(DH.oh_number_from,''''))))
   WHEN '''' THEN ''None''
   WHEN ''NONE'' THEN ''None''
   ELSE  isNull(DH.oh_num_prefix, '''') + RTRIM(DH.oh_number_from) + isNull(DH.oh_num_suffix, '''') + '' - '' +
         isNull(DH.oh_num_prefix, '''') + LTRIM(DH.oh_number_to) + isNull(DH.oh_num_suffix, '''')
 END as Numbering,
 CASE UPPER(LTRIM(RTRIM(ISNULL(DH.oh_micr_from,''''))))
   WHEN '''' THEN ''None''
   WHEN ''NONE'' THEN ''None''
   ELSE  RTRIM(DH.oh_micr_from) + '' - '' + LTRIM(DH.oh_micr_to)
 END as ConsecMicr,
 TRNH.INVOICE_DATE, DH.oh_dealer_po as YourOrderNumber,
 CASE DH.oh_fob
   WHEN ''C'' THEN ''CUSTOMER''
   WHEN ''D'' THEN ''DEALER''
   else ''OTHER''
 END AS FOB, T.DESCRIPTION AS Terms, DH.oh_type as DocketType, DH.oh_num as DocketNumber, CST.CUSTOMER_CODE AS AccountNumber,
 TER.TERRITORY_CODE AS OurOffice, MAN.MAN_PackSlipDate AS DateShipped, COU.COU_ShortDesc AS ShippedVia,
 QG.DktGSTDesc AS GST_Description, QG.DktPSTDesc AS PST_Description,
 QG.QG_Qty AS Qty_Ordered, MAN.MAN_FormsShipped AS Qty_Shipped,
 QG.QG_Desc AS FormDescription, QH.Q_ClientName as ClientName,
 ((QG.QG_DktBillAmt - QG.QG_DktGST - QG.QG_DktPST) * 1000)/QG.QG_Qty AS UnitPrice, ''M'' AS Unit,
 CASE WHEN (QG.QG_DktPST=0 AND QG.QG_DktGST=0) THEN ''''
      WHEN (QG.QG_DktPST>0 AND QG.QG_DktGST=0) THEN ''PST''
      WHEN (QG.QG_DktPST=0 AND QG.QG_DktGST>0) THEN ''GST''
      ELSE ''GST/HST''
 END AS Tax_Desc,
 CST.GST_CODE, QG.QG_DktGST AS GST_Amt, QG.QG_DktPST AS PST_Amt, QG.QG_DktBillAmt AS TotalCost,
 MAN.MAN_Boxes1 AS BOXES1, MAN.MAN_FormsPerBox1 AS FORMS_PER_BOX1, 
 MAN.MAN_Boxes2 AS BOXES2, MAN.MAN_FormsPerBox2 AS FORMS_PER_BOX2, 
 MAN.MAN_Boxes3 AS BOXES3, MAN.MAN_FormsPerBox3 AS FORMS_PER_BOX3,
CASE WHEN (RPT.Seq = 0) THEN
  ''DOCKET''
ELSE
  ISNULL(TRNM.DESCRIPTION, ''XXX'')
END AS MISC_DESCRIPTION,
 TRNM.QUANTITY AS MISC_QUANTITY, TRNM.UNIT_PRICE AS MISC_UNIT_PRICE,
TRNM.TOTAL AS MISC_TOTAL, TRNM.PST_CODE AS MISC_PST_TF, TRNM.GST_CODE AS MISC_GST_TF,
CASE WHEN (TRNM.PST_CODE=''T'') THEN PST.SALES_TAX ELSE 0 END AS MISC_PST_PERCENT,
CASE WHEN (TRNM.GST_CODE=''T'') THEN QG.QG_DktGST_Pct ELSE 0 END AS MISC_GST_PERCENT
                      FROM RPT_Invoice RPT LEFT OUTER JOIN
                      D_Head DH ON DH.oh_id = RPT.oh_id LEFT OUTER JOIN
                      Q_Head QH ON DH.Q_ID = QH.Q_ID LEFT OUTER JOIN
                      Q_Group QG ON DH.QG_ID = QG.QG_ID LEFT OUTER JOIN
                      SHIP_Manifest MAN ON MAN.MAN_ID = DH.SHIP_MAN_ID LEFT OUTER JOIN ' +
                      @tr_db + '..CUSTOMERS CST ON QH.Customer_ID = CST.CUSTOMER_ID LEFT OUTER JOIN
                      COU_Courier COU ON COU.COU_ID = MAN.COU_ID LEFT OUTER JOIN ' +
                      @tr_db + '..TERMS T ON T.TERMS_ID = CST.TERMS_ID LEFT OUTER JOIN ' +
                      @tr_db + '..TERRITORY TER ON TER.TERRITORY_ID = CST.TERRITORY_ID LEFT OUTER JOIN ' +
                      @tr_db + '..SO_TRN_HDR TRNH ON TRNH.SO_TRN_ID = DH.R2B_SO_TRN_ID AND TRNH.SO_ID=0 LEFT OUTER JOIN ' +
                      @tr_db + '..SO_TRN_MISC TRNM ON TRNM.SO_TRN_MISC_ID = RPT.SO_TRN_MISC_ID LEFT OUTER JOIN ' +
                      @tr_db + '..SALES_TAXES PST ON (PST.SALES_TAX_ID=CST.SALES_TAX_ID) LEFT OUTER JOIN ' +
@tr_db + '..GST_CODES GST ON (GST.GST_CODE = CST.GST_CODE) ' +
'WHERE RPT.User_SLS_ID=' + @USER_SLS_ID +
'ORDER BY DH.oh_num, RPT.seq'

exec(@sql)
































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptBillLabels]') and xtype = 'P ')  
 drop Procedure sp_RptBillLabels
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

-- sp_RptBillLabels 'ABC Company', 14

CREATE PROCEDURE [dbo].[sp_RptBillLabels]
  @CompanyName varchar(200),
  @USER_SLS_ID varchar(2000)
AS
SET NOCOUNT ON;

declare @sql varchar(5000), @tr_db varchar(50)

select @tr_db=isnull(tr_db,'') from database_setup

-- Only want 1 label per Unique Customer
select QH.Customer_ID AS Customer_ID, min(R.oh_id) AS MIN_OH_ID, count(*) as INVC_COUNT
INTO #tmp
FROM RPT_Invoice R
LEFT JOIN D_HEAD DH ON DH.oh_ID = R.oh_id
LEFT JOIN Q_HEAD QH ON QH.Q_ID = DH.Q_ID
WHERE R.User_SLS_ID=@USER_SLS_ID AND R.seq=0
GROUP BY QH.Customer_ID


set @sql=
'
SELECT ''' + @CompanyName + ''' AS ReportCompanyName, DH.R2B_BillBatchNumber, DH.R2B_InvoiceNumber As InvoiceNumber,
 CST.NAME, CST.BILL_ADDRESS_1, CST.BILL_ADDRESS_2, CST.BILL_ADDRESS_3,
 CST.BILL_CITY, CST.BILL_STATE, CST.BILL_ZIP, INVC_COUNT, DH.oh_type, DH.oh_num
 FROM #tmp T LEFT OUTER JOIN
 D_Head DH ON DH.oh_id = T.MIN_OH_ID LEFT OUTER JOIN
 Q_Head QH ON DH.Q_ID = QH.Q_ID LEFT OUTER JOIN ' +
 @tr_db + '..CUSTOMERS CST ON QH.Customer_ID = CST.CUSTOMER_ID ' +
'ORDER BY DH.R2B_InvoiceNumber'

exec(@sql)




Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptBillRegister]') and xtype = 'P ')  
 drop Procedure sp_RptBillRegister
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go


-- sp_RptBillRegister 'ABC Company', 'WHERE R2B_OnHoldYN=''N'''

CREATE PROCEDURE [dbo].[sp_RptBillRegister]
  @CompanyName varchar(200),
  @where_clause varchar(2000)
 AS

declare @sql varchar(5000),
        @tr_db varchar(50)

select @tr_db=isnull(tr_db,'') from database_setup

CREATE TABLE #TMP1
(
	oh_id int,
	tot money,
    tot_PST money,
    tot_GST money
)


INSERT #TMP1
SELECT oh_id, SUM(R2BM_Extended), SUM(R2BM_PST_Amt), SUM(R2BM_GST_Amt) FROM SHIP_Rel2BillMiscChrg
GROUP BY oh_id

set @sql=
'
SELECT ''' + @CompanyName + ''' AS ReportCompanyName, R2B.R2B_BillBatchNumber, R2B.R2B_HoldNote, R2B.R2B_OnHoldYN,
 DH.oh_type, DH.oh_num, DH.oh_rel2B_CustMaxSeq as Duplicates, DH.oh_rel2B_CustSeq,
 QH.Customer_ID, QH.Q_ClientName, QG.QG_DktBillAmt AS TotalCost,
 QG.QG_Desc, QG.QG_DktGST AS GST_Amt, QG.QG_DktPST AS PST_Amt,
 QG.QG_LotPrice, QG.QG_DiscountPercent, QG.QG_DealerCostLot, QG.QG_Shipping, QG.QG_DealerTotCost,
 IsNull(T.tot,0) AS MISC_CHARGE_TOTAL, IsNull(T.tot_PST,0) AS MISC_CHARGE_PST_AMT, IsNull(T.tot_GST,0) AS MISC_CHARGE_GST_AMT,
 CASE WHEN (QG.QG_DktPST=0 AND QG.QG_DktGST=0) THEN ''''
      WHEN (QG.QG_DktPST>0 AND QG.QG_DktGST=0) THEN ''PST''
      WHEN (QG.QG_DktPST=0 AND QG.QG_DktGST>0) THEN ''GST''
      ELSE ''GST/PST''
 END AS Tax_Desc,
 VC.NAME, VC.CUSTOMER_CODE, VC.GST_CODE,
 COU.COU_ShortDesc
FROM         SHIP_Rel2Bill R2B LEFT OUTER JOIN
                      D_Head DH ON R2B.oh_id = DH.oh_ID LEFT OUTER JOIN
                      Q_Head QH ON DH.Q_ID = QH.Q_ID LEFT OUTER JOIN
                      Q_Group QG ON DH.QG_ID = QG.QG_ID LEFT OUTER JOIN
                      ' + @tr_db + '..CUSTOMERS VC ON QH.Customer_ID = VC.CUSTOMER_ID LEFT OUTER JOIN
                      COU_Courier COU ON COU.COU_ID = DH.SHIP_COU_ID LEFT OUTER JOIN
                      #TMP1 T ON T.oh_id = DH.oh_id
' + @where_clause + '
AND     (R2B.R2B_Status = ''1'') 
ORDER BY DH.oh_type, DH.oh_num


'

exec(@sql)


















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptConfirmation]') and xtype = 'P ')  
 drop Procedure sp_RptConfirmation
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go





-- exec sp_RptConfirmation 655237

CREATE   proc [dbo].[sp_RptConfirmation] @OH_ID int
as
SET NOCOUNT ON;

declare @tr_db varchar(50),
        @sql_str varchar(5000)

SELECT @tr_db=tr_db FROM database_setup

declare @O_oh_type varchar(1),				@O_oh_num int,						@O_oh_prev int,
		@O_ScheduleShip datetime,			@SOLD_TO_NAME varchar(40),
        @O_BILL_ADDRESS_1 varchar(40),		@O_BILL_ADDRESS_2 varchar(40),		@O_BILL_ADDRESS_3 varchar(40),
		@O_BILL_CITY varchar(20),			@O_BILL_STATE varchar(2),			@O_BILL_ZIP varchar(10),	 
        @O_Q_ClientName varchar(40),        @O_JobDescription varchar(30),      @O_YourPO varchar(12),
		@O_YourDocket varchar(11),			@O_YourCost money,
		@O_GST money,					    @O_PST money,						@O_Total money,
		@O_QG_Qty int,						@O_QG_Parts int,					@O_Size varchar(24),
        @O_Size_W varchar(20),              @O_Size_L varchar(20),		        @O_Dim1 varchar(20),  @O_Dim2 varchar(20),		
        @O_Stub varchar(13),		        @O_Numbering varchar(69),
        @O_number_from varchar(11),         @O_number_to varchar(11),           @O_oh_position varchar(8),
		@O_num_prefix varchar(11),          @O_num_suffix varchar(11), 
        @O_Face_Inks varchar(34),           @O_Back_Inks varchar(34),           @O_Cover_Inks varchar(34),
        @O_Stock1 varchar(15),				@O_Stock2 varchar(15),				@O_Stock3 varchar(15),
		@O_Stock4 varchar(15),				@O_Stock5 varchar(15),				@O_Stock6 varchar(15),
		@O_Stock7 varchar(15),				@O_Stock8 varchar(15),				@O_Stock9 varchar(15), 
        @O_CoverStock varchar(15),
		@O_Punch_Holes int,					@O_Punch_Posn varchar(4),			@O_Punch_Diam varchar(5),
        @O_Punch_CC varchar(6),				@O_Punch_Throat varchar(6),			@O_Punch_Pts varchar(8),
        @O_Bind_BookIn varchar(6),			@O_Bind_At varchar(13),				@O_Bind_Pts varchar(12),
        @O_ShrinkIn varchar(19),			@O_Bind_Note varchar(5000),
		@O_Proof_YN varchar(1),
        @O_oh_ship_name varchar(40),        @O_oh_ship_suite varchar(40),       @O_oh_ship_addr varchar(40),
        @O_oh_ship_city varchar(27),        @oh_ship_prov varchar(2),           @oh_ship_postal varchar(10),
		@O_oh_ship_phone varchar(20),


		@O_SmanName varchar(35),
		@O_SmanPhone varchar(20),
        @O_TOLL_FREE_FAX varchar(20)


declare @DH_SLS_ID_TakenBy int,
		@QH_Customer_ID int,  @oh_cust_po varchar(18),   @oh_proof_date varchar(15),
        @Q_FBC1 varchar(1),   @Q_FBC2 varchar(1),        @Q_FBC3 varchar(1),        @Q_FBC4 varchar(1),   @Q_FBC5 varchar(1),
        @ohp_ink1 varchar(5), @ohp_ink2 varchar(5),      @ohp_ink3 varchar(5),      @ohp_ink4 varchar(5), @ohp_ink5 varchar(5),
        @ohp_PaperSpecID int, @oh_micr_from varchar(10), @oh_micr_to varchar(10)

--Get Scheduled Ship Date--> @O_ScheduleShip

SELECT	@O_oh_type=ISNULL(oh_type,''),						@O_oh_num=ISNULL(oh_num,0),
        @O_oh_prev = ISNULL(oh_prev,0),						@O_ScheduleShip = DH.oh_ship_date,
		@O_Q_ClientName = ISNULL(QH.Q_ClientName,''),		@DH_SLS_ID_TakenBy = DH.SLS_ID_TakenBy,
		@O_YourPO = ISNULL(DH.oh_dealer_po,''),				@O_YourDocket = ISNULL(DH.oh_dealer_docket,''),
	    @O_Stub = ISNULL(DH.oh_snap,'') + ' ' + ISNULL(DH.oh_book,''), 
		@O_Numbering = ISNULL(DH.oh_num_prefix,'') + ISNULL(DH.oh_number_from,'') + ISNULL(DH.oh_num_suffix,'') + ' - ' + ISNULL(DH.oh_num_prefix,'') + ISNULL(DH.oh_number_to,'') + ISNULL(DH.oh_num_suffix,''),
        @O_number_from = ISNULL(DH.oh_number_from,''),		@O_number_to = ISNULL(DH.oh_number_to, ''),
        @O_oh_position = ISNULL(DH.oh_position,''),
        @oh_micr_from =  ISNULL(DH.oh_micr_from,''),        @oh_micr_to  = ISNULL(DH.oh_micr_to, ''),
		@oh_cust_po = ISNULL(DH.oh_cust_po,''),
		@O_num_prefix = ISNULL(DH.oh_num_prefix,''),		@O_num_suffix = ISNULL(DH.oh_num_suffix,''),
		@oh_proof_date = ISNULL(DH.oh_proof_date,''),
        @ohp_ink1 = ISNULL(DH.ohp_ink1,''),					@ohp_ink2 = ISNULL(DH.ohp_ink2,''),
		@ohp_ink3 = ISNULL(DH.ohp_ink3,''),			        @ohp_ink4 = ISNULL(DH.ohp_ink4,''),
		@ohp_ink5 = ISNULL(DH.ohp_ink5,''),			        @ohp_PaperSpecID = ISNULL(DH.ohp_PaperSpecID,''),
		@O_Punch_Holes = ISNULL(oh_holes,0),				@O_Punch_Posn = ISNULL(oh_pos,''),
		@O_Punch_Diam = ISNULL(oh_dia,''),					@O_Punch_CC = ISNULL(oh_c_c,''),
		@O_Punch_Throat = ISNULL(oh_trt,''),				@O_Punch_Pts = ISNULL(oh_press_parts,''),
        @O_Bind_BookIn = ISNULL(oh_bookin,''),				@O_Bind_At = ISNULL(oh_at,''),
		@O_Bind_Pts = ISNULL(oh_pts,''),			        @O_ShrinkIn=ISNULL(oh_BookShrinkIn,''),
		@O_Bind_Note=ISNULL(oh_BookNote,''),				@O_Proof_YN=ISNULL(oh_proof_yn,'N'),
        @O_oh_ship_name=ISNULL(oh_ship_name,''),			@O_oh_ship_suite=ISNULL(oh_ship_suite,''),
		@O_oh_ship_addr=ISNULL(oh_ship_addr,''),			@O_oh_ship_city=ISNULL(oh_ship_city,''),
		@oh_ship_prov=ISNULL(oh_ship_prov,''),				@oh_ship_postal=ISNULL(oh_ship_postal,''),
		@O_oh_ship_phone=ISNULL(oh_ship_phone,''),
		@QH_Customer_ID = QH.Customer_ID,
        @O_Size_W = QH.Q_Width,                             @O_Size_L = QH.Q_Length,
        @O_Dim1 = ISNULL(DH.oh_ActualDim1, ''),             @O_Dim2 = ISNULL(DH.oh_ActualDim2, ''),
        @Q_FBC1 = ISNULL(QH.Q_FBC1,''),						@Q_FBC2 = ISNULL(QH.Q_FBC2,''),
		@Q_FBC3 = ISNULL(QH.Q_FBC3,''),						@Q_FBC4 = ISNULL(QH.Q_FBC4,''),
		@Q_FBC5 = ISNULL(QH.Q_FBC5,''),

		@O_JobDescription = ISNULL(QG.QG_Desc,''),			@O_YourCost = QG.QG_DktNetCost,
		@O_GST = QG.QG_DktGST,				    @O_PST = QG.QG_DktPST,					@O_Total = QG.QG_DktBillAmt,
 		@O_QG_Qty = QG.QG_Qty,					@O_QG_Parts = QG.QG_Parts					
 


FROM D_Head DH
LEFT OUTER JOIN Q_Head QH ON DH.Q_ID = QH.Q_ID
LEFT OUTER JOIN Q_GROUP QG ON DH.QG_ID = QG.QG_ID
WHERE DH.oh_ID=@OH_ID

-- oh_ActualDim1 and oh_ActualDim2 are new fields and may be null for pre-existing Dockets
if ((@O_Dim1 = '') OR (@O_Dim2 = ''))
  set @O_Size = @O_Size_W + ' x ' + @O_Size_L
else
  set @O_Size = @O_Dim1 + ' x ' + @O_Dim2

--Get Sold To Info --> @O_SOLD_TO_<Stuff>
CREATE TABLE #TMP1
(
	SOLD_TO_NAME varchar(40),
	BILL_ADDRESS_1 varchar(40),
	BILL_ADDRESS_2 varchar(40),
	BILL_ADDRESS_3 varchar(40),
	BILL_CITY varchar(20),
	BILL_STATE varchar(2),
	BILL_ZIP varchar(10)	 
)
set @sql_str='SELECT ISNULL(NAME,''''), ISNULL(BILL_ADDRESS_1,''''), ISNULL(BILL_ADDRESS_2,''''), 
ISNULL(BILL_ADDRESS_3,''''), ISNULL(BILL_CITY,''''), ISNULL(BILL_STATE,''''), ISNULL(BILL_ZIP,'''')
FROM ' + @tr_db + '..CUSTOMERS WHERE CUSTOMER_ID=' + cast(@QH_Customer_ID as varchar)
insert #TMP1
exec(@sql_str)
SELECT 
	@SOLD_TO_NAME = SOLD_TO_NAME,		@O_BILL_ADDRESS_1 =	BILL_ADDRESS_1,		@O_BILL_ADDRESS_2 =	BILL_ADDRESS_2,
	@O_BILL_ADDRESS_3 =	BILL_ADDRESS_3,	@O_BILL_CITY = 	BILL_CITY,				@O_BILL_STATE =	BILL_STATE,
	@O_BILL_ZIP	= BILL_ZIP	 
FROM #TMP1
DROP TABLE #TMP1


--Get TakenBy Salesman Name--> @O_SmanName

declare @WhsID int

CREATE TABLE #TMP3
(
  Nm varchar(200),
  WHSE_ID int 
)

set @sql_str = 'SELECT IsNull(FIRSTNAME, '''') + '' '' + IsNull(LASTNAME, ''''), WHSE_ID FROM ' +
@tr_db + '..SALESPERSONS WHERE SLS_ID=' + cast(@DH_SLS_ID_TakenBy as varchar)
insert #TMP3
exec(@sql_str)
SELECT @O_SmanName=Nm, @WhsID=WHSE_ID from #TMP3
DROP TABLE #TMP3 


--Get PHONE2 from TR..WAREHOUSE--> @O_SmanPhone
CREATE TABLE #TMP2
(
	WHS_Phone varchar(20),
    TMP_TOLL_FREE_FAX varchar(20)
)
set @sql_str='SELECT PHONE2, ISNULL(TOLL_FREE_FAX,'''') FROM ' + @tr_db + '..WAREHOUSE WHERE WHSE_ID=' + cast(@WhsID as varchar)
insert #TMP2
exec(@sql_str)
SELECT @O_SmanPhone = WHS_Phone, @O_TOLL_FREE_FAX = TMP_TOLL_FREE_FAX FROM #TMP2
DROP TABLE #TMP2


--Get Inks-->  @O_FaceInks, @O_BackInks

set @O_Face_Inks = ''
set @O_Back_Inks = '' 
set @O_Cover_Inks = '' 
    

declare @sINK varchar(10)


--Ink1
SELECT @sINK = ''
SELECT @sINK = pi_ink1 FROM D_Parts
WHERE oh_ID=@OH_ID AND ISNULL(pi_ink1,'') <> ''

if (@sINK <> '')
begin
  if  (@Q_FBC1 = 'F')
  begin
    if  (@O_Face_Inks <> '')
      set @O_Face_Inks = @O_Face_Inks + ', '
    set @O_Face_Inks = @O_Face_Inks + @sINK
  end
  else
  begin
    if  (@O_Back_Inks <> '')
      set @O_Back_Inks = @O_Back_Inks + ', '
    set @O_Back_Inks = @O_Back_Inks + @sINK
  end
end

--Ink2
SELECT @sINK = ''
SELECT @sINK = pi_ink2 FROM D_Parts
WHERE oh_ID=@OH_ID AND ISNULL(pi_ink2,'') <> ''

if (@sINK <> '')
begin
  if  (@Q_FBC2 = 'F')
  begin
    if  (@O_Face_Inks <> '')
      set @O_Face_Inks = @O_Face_Inks + ', '
    set @O_Face_Inks = @O_Face_Inks + @sINK
  end
  else
  begin
    if  (@O_Back_Inks <> '')
      set @O_Back_Inks = @O_Back_Inks + ', '
    set @O_Back_Inks = @O_Back_Inks + @sINK
  end
end

--Ink3
SELECT @sINK = ''
SELECT @sINK = pi_ink3 FROM D_Parts
WHERE oh_ID=@OH_ID AND ISNULL(pi_ink3,'') <> ''

if (@sINK <> '')
begin
  if  (@Q_FBC3 = 'F')
  begin
    if  (@O_Face_Inks <> '')
      set @O_Face_Inks = @O_Face_Inks + ', '
    set @O_Face_Inks = @O_Face_Inks + @sINK
  end
  else
  begin
    if  (@O_Back_Inks <> '')
      set @O_Back_Inks = @O_Back_Inks + ', '
    set @O_Back_Inks = @O_Back_Inks + @sINK
  end
end

--Ink4
SELECT @sINK = ''
SELECT @sINK = pi_ink4 FROM D_Parts
WHERE oh_ID=@OH_ID AND ISNULL(pi_ink4,'') <> ''

if (@sINK <> '')
begin
  if  (@Q_FBC4 = 'F')
  begin
    if  (@O_Face_Inks <> '')
      set @O_Face_Inks = @O_Face_Inks + ', '
    set @O_Face_Inks = @O_Face_Inks + @sINK
  end
  else
  begin
    if  (@O_Back_Inks <> '')
      set @O_Back_Inks = @O_Back_Inks + ', '
    set @O_Back_Inks = @O_Back_Inks + @sINK
  end
end

--Ink5
SELECT @sINK = ''
SELECT @sINK = pi_ink5 FROM D_Parts
WHERE oh_ID=@OH_ID AND ISNULL(pi_ink5,'') <> ''

if (@sINK <> '')
begin
  if  (@Q_FBC5 = 'F')
  begin
    if  (@O_Face_Inks <> '')
      set @O_Face_Inks = @O_Face_Inks + ', '
    set @O_Face_Inks = @O_Face_Inks + @sINK
  end
  else
  begin
    if  (@O_Back_Inks <> '')
      set @O_Back_Inks = @O_Back_Inks + ', '
    set @O_Back_Inks = @O_Back_Inks + @sINK
  end
end

--Cover Inks

if (@ohp_ink1 <> '')
begin
  if (@O_Cover_Inks <> '')
    set @O_Cover_Inks = @O_Cover_Inks + ', '
  set @O_Cover_Inks = @O_Cover_Inks + @ohp_ink1
end

if (@ohp_ink2 <> '')
begin
  if (@O_Cover_Inks <> '')
    set @O_Cover_Inks = @O_Cover_Inks + ', '
  set @O_Cover_Inks = @O_Cover_Inks + @ohp_ink2
end

if (@ohp_ink3 <> '')
begin
  if (@O_Cover_Inks <> '')
    set @O_Cover_Inks = @O_Cover_Inks + ', '
  set @O_Cover_Inks = @O_Cover_Inks + @ohp_ink3
end

if (@ohp_ink4 <> '')
begin
  if (@O_Cover_Inks <> '')
    set @O_Cover_Inks = @O_Cover_Inks + ', '
  set @O_Cover_Inks = @O_Cover_Inks + @ohp_ink4
end

if (@ohp_ink5 <> '')
begin
  if (@O_Cover_Inks <> '')
    set @O_Cover_Inks = @O_Cover_Inks + ', '
  set @O_Cover_Inks = @O_Cover_Inks + @ohp_ink5
end

--Stock
SELECT @O_Stock1=S.ShortDesc FROM D_Parts D
LEFT OUTER JOIN CB_PaperSpec S ON S.PaperSpecID = D.PaperSpecID
WHERE D.oh_ID=@OH_ID AND D.pi_seq=1

SELECT @O_Stock2=S.ShortDesc FROM D_Parts D
LEFT OUTER JOIN CB_PaperSpec S ON S.PaperSpecID = D.PaperSpecID
WHERE D.oh_ID=@OH_ID AND D.pi_seq=2

SELECT @O_Stock3=S.ShortDesc FROM D_Parts D
LEFT OUTER JOIN CB_PaperSpec S ON S.PaperSpecID = D.PaperSpecID
WHERE D.oh_ID=@OH_ID AND D.pi_seq=3

SELECT @O_Stock4=S.ShortDesc FROM D_Parts D
LEFT OUTER JOIN CB_PaperSpec S ON S.PaperSpecID = D.PaperSpecID
WHERE D.oh_ID=@OH_ID AND D.pi_seq=4

SELECT @O_Stock5=S.ShortDesc FROM D_Parts D
LEFT OUTER JOIN CB_PaperSpec S ON S.PaperSpecID = D.PaperSpecID
WHERE D.oh_ID=@OH_ID AND D.pi_seq=5

SELECT @O_Stock6=S.ShortDesc FROM D_Parts D
LEFT OUTER JOIN CB_PaperSpec S ON S.PaperSpecID = D.PaperSpecID
WHERE D.oh_ID=@OH_ID AND D.pi_seq=6

SELECT @O_Stock7=S.ShortDesc FROM D_Parts D
LEFT OUTER JOIN CB_PaperSpec S ON S.PaperSpecID = D.PaperSpecID
WHERE D.oh_ID=@OH_ID AND D.pi_seq=7

SELECT @O_Stock8=S.ShortDesc FROM D_Parts D
LEFT OUTER JOIN CB_PaperSpec S ON S.PaperSpecID = D.PaperSpecID
WHERE D.oh_ID=@OH_ID AND D.pi_seq=8

SELECT @O_Stock9=S.ShortDesc FROM D_Parts D
LEFT OUTER JOIN CB_PaperSpec S ON S.PaperSpecID = D.PaperSpecID
WHERE D.oh_ID=@OH_ID AND D.pi_seq=9

if  (@ohp_PaperSpecID <> '')
begin
  SELECT @O_CoverStock=ShortDesc FROM CB_PaperSpec
  WHERE PaperSpecID = @ohp_PaperSpecID
end



SELECT
@O_oh_type + ' ' + cast(@O_oh_num as varchar) as DocketNumber,
CASE WHEN @O_oh_prev<=0 THEN
  ''
ELSE
  @O_oh_type + ' ' + cast(@O_oh_prev as varchar)
END as RepeatOf,
@O_ScheduleShip as ShipDate, @SOLD_TO_NAME as SoldTo_Name,		@O_BILL_ADDRESS_1 as SoldTo_Addr1,
@O_BILL_ADDRESS_2 as SoldTo_Addr2,	@O_BILL_ADDRESS_3 as SoldTo_Addr3,	@O_BILL_CITY as SoldTo_City,
@O_BILL_STATE as SoldTo_State,		@O_BILL_ZIP as SoldTo_Zip,			@O_Q_ClientName as ClientName,
@O_JobDescription as Description,	@O_YourPO as YourPO,				@O_YourDocket as YourDocket,
@oh_cust_po as YourCustPO,
@O_YourCost as YourPrice,			@O_GST as GST,						@O_PST as PST,
@O_Total as Total,
@O_QG_Qty as Qty,					@O_QG_Parts as Parts,				@O_Size as Size,
@O_Stub as Stub,					@O_Numbering as Numbering,
@O_number_from as NumberFrom,		@O_number_to as NumberTo,           @O_oh_position as NumberPosition,
@O_num_prefix as NumberPrefix,		@O_num_suffix as NumberSuffix,
@oh_micr_from as MicrFrom,          @oh_micr_to as MicrTo,
@O_Face_Inks as FaceInks,			@O_Back_Inks as BackInks,           @O_Cover_Inks as CoverInks,
ISNULL(@O_Stock1,'') as Stock1,		ISNULL(@O_Stock2,'') as Stock2,		ISNULL(@O_Stock3,'') as Stock3,
ISNULL(@O_Stock4,'') as Stock4,		ISNULL(@O_Stock5,'') as Stock5,		ISNULL(@O_Stock6,'') as Stock6,
ISNULL(@O_Stock7,'') as Stock7,		ISNULL(@O_Stock8,'') as Stock8,		ISNULL(@O_Stock9,'') as Stock9,
ISNULL(@O_CoverStock,'') as CoverStock, 
@O_Punch_Holes as Punch_Holes,		@O_Punch_Posn as Punch_Posn,		@O_Punch_Diam as Punch_Diam,
@O_Punch_CC as Punch_CC,			@O_Punch_Throat as Punch_Throat,	@O_Punch_Pts as Punch_Pts,
@O_Bind_BookIn as Bind_BookIn,		@O_Bind_At as Bind_At,				@O_Bind_Pts as Bind_Pts,
@O_ShrinkIn as ShrinkIn,			@O_Bind_Note as Bind_Note,			@O_Proof_YN as Proof_YN,
@oh_proof_date as ProofMsg,
@O_oh_ship_name as Ship_Name,       @O_oh_ship_suite as Ship_Suite,     @O_oh_ship_addr as Ship_Addr,
@O_oh_ship_city as Ship_City,       @oh_ship_prov as Ship_Prov,         @oh_ship_postal as Ship_Postal,
@O_oh_ship_phone as Ship_Phone,
@O_SmanName as SalesPersonName,		@O_SmanPhone as SalesPersonPhone,   @O_TOLL_FREE_FAX as TOLL_FREE_FAX














Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptCopyFolder]') and xtype = 'P ')  
 drop Procedure sp_RptCopyFolder
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
-- =============================================
-- Description:	
-- =============================================
-- sp_RptCopyFolder 'ABC Company', 282911
CREATE PROCEDURE [dbo].[sp_RptCopyFolder]
    @CompanyName varchar(200),
 	@OH_ID int
AS
BEGIN
  SELECT @CompanyName AS ReportCompanyName,
  CASE WHEN CF.CFO_Repeat_oh_type=' ' THEN 'New'
  ELSE CF.CFO_Repeat_oh_type + ' ' + CONVERT(varchar(10), CF.CFO_Repeat_oh_num)
  END AS Repeat_Docket,
--  CF.CFO_Repeat_oh_num,
  DH.oh_type AS Dkt_Type, DH.oh_num AS Dkt_Num,
  CF.CFO_StdBodyNo, CF.PANTO_Code, PAN.PANTO_Desc, CF.CFO_2ndColor,
  BLKOS.BLKOS_Desc AS BlockoutStyle, CF.CFO_BlockOutColor,
  BLKO1.BLKO_Desc AS BlockPart1, BLKO2.BLKO_Desc AS BlockPart2, BLKO3.BLKO_Desc AS BlockPart3, BLKO4.BLKO_Desc AS BlockPart4,
  BLKO5.BLKO_Desc AS BlockPart5, BLKO6.BLKO_Desc AS BlockPart6, BLKO7.BLKO_Desc AS BlockPart7,
  CF.CFO_ReferBody, CF.CFO_ReferLogo, CF.CFO_ReferMisc,
  CASE WHEN CF.CFO_PunchOut_FB='F' THEN 'Face'
       WHEN CF.CFO_PunchOut_FB='B' THEN 'Back'
       WHEN CF.CFO_PunchOut_FB='A' THEN 'F/B'
       ELSE ''
  END AS PunchFaceBack,
  CASE WHEN CF.CFO_MatSupBody_MECD='M' THEN 'MOD'
       WHEN CF.CFO_MatSupBody_MECD='E' THEN 'EMAIL'
       WHEN CF.CFO_MatSupBody_MECD='C' THEN 'COPY'
       WHEN CF.CFO_MatSupBody_MECD='D' THEN 'DISC'
       ELSE ''
  END AS MaterialSuppliedForBody,
  CASE WHEN CF.CFO_MatSupLogo_MECD='M' THEN 'MOD'
       WHEN CF.CFO_MatSupLogo_MECD='E' THEN 'EMAIL'
       WHEN CF.CFO_MatSupLogo_MECD='C' THEN 'COPY'
       WHEN CF.CFO_MatSupLogo_MECD='D' THEN 'DISC'
       ELSE ''
  END AS MaterialSuppliedForLogo,
  CASE WHEN CF.CFO_MatSupMisc_MECD='M' THEN 'MOD'
       WHEN CF.CFO_MatSupMisc_MECD='E' THEN 'EMAIL'
       WHEN CF.CFO_MatSupMisc_MECD='C' THEN 'COPY'
       WHEN CF.CFO_MatSupMisc_MECD='D' THEN 'DISC'
       ELSE ''
  END AS MaterialSuppliedForMisc,
  CF.CFO_Area1_Co AS Notes1, CF.CFO_Area2_Bank AS Notes2, CF.CFO_Area3_Sign AS Notes3, CF.CFO_Area4_Vouch AS Notes4, CFO_Area5_Vouch AS Notes5, CFO_SpecialInstr AS NotesSpecialInstr,
  CASE WHEN CF.CFO_SignLine_1_or_2='1' THEN '1 Line'
       WHEN CF.CFO_SignLine_1_or_2='2' THEN '2 Line'
       ELSE ''
  END AS SignatureLine,
  CF.CFO_UseCPA_YN, CF.CFO_CPA_DateFmt, 
  CASE WHEN CF.CFO_PPDateRqd_YNB = 'Y' THEN 'YES'
       WHEN CF.CFO_PPDateRqd_YNB = 'N' THEN 'NO'
       ELSE ''
  END AS PrePrintedDateRqd,
  CASE WHEN CF.CFO_PPDolRqd_YNB = 'Y' THEN 'YES'
       WHEN CF.CFO_PPDolRqd_YNB = 'N' THEN 'NO'
       ELSE ''
  END AS PrePrintedDollarSignRqd,
  CASE WHEN CF.CFO_IsMicrClear_YNB = 'Y' THEN 'YES'
       WHEN CF.CFO_IsMicrClear_YNB = 'N' THEN 'NO'
       ELSE ''
  END AS IsMICRBandClear,
  CASE WHEN CF.CFO_IfCustBord_YNB = 'Y' THEN 'YES'
       WHEN CF.CFO_IfCustBord_YNB = 'N' THEN 'NO'
       ELSE ''
  END AS IsCustomBorderMICRClear,
  CF.CFO_CopyChkBy_Press, CF.CFO_CopyChkBy_Coll, CF.CFO_CopyChkBy_Bind,
  CF.CFO_Desg1, CF.CFO_Desg2, CF.CFO_Desg3, CF.CFO_Desg4, CF.CFO_Desg5, CF.CFO_Desg6, CF.CFO_Desg7, 
  CF.CFO_Desg2_ChkYN, CF.CFO_Desg3_ChkYN, CF.CFO_Desg4_ChkYN, CF.CFO_Desg5_ChkYN, CF.CFO_Desg6_ChkYN, CF.CFO_Desg7_ChkYN,
  CF.CFO_PI_BW  AS ProofInstr_BW,  CF.CFO_PI_CP  AS ProofInstr_CP,  CF.CFO_PI_OS AS ProofInstr_OS,
  CF.CFO_PI_FAX AS ProofInstr_FAX, CF.CFO_PI_PDF AS ProofInstr_PDF, CF.CFO_PI_POL AS ProofInstr_POL,
  CF.CFO_PI_CC  AS ProofInstr_CC,
  BNK.BANK_Desc AS BankDescription,
  CF.CFO_Bank_43, CF.CFO_Bank_42, CF.CFO_Bank_41, CF.CFO_Bank_40, CF.CFO_Bank_39, CF.CFO_Bank_38, CF.CFO_Bank_37,
  CF.CFO_Bank_36, CF.CFO_Bank_35, CF.CFO_Bank_34, CF.CFO_Bank_33, CF.CFO_Bank_32, CF.CFO_Bank_31, CF.CFO_Bank_30,
  CF.CFO_Bank_29, CF.CFO_Bank_28, CF.CFO_Bank_27, CF.CFO_Bank_26, CF.CFO_Bank_25, CF.CFO_Bank_24, CF.CFO_Bank_23,
  CF.CFO_Bank_22, CF.CFO_Bank_21, CF.CFO_Bank_20, CF.CFO_Bank_19, CF.CFO_Bank_18, CF.CFO_Bank_17, CF.CFO_Bank_16,
  CF.CFO_Bank_15, CF.CFO_Bank_14, CF.CFO_Bank_13      

  FROM D_CopyFolder CF
  LEFT OUTER JOIN CC_BlockoutStyle BLKOS ON BLKOS.BLKOS_ID   = CF.BLKOS_ID
  LEFT OUTER JOIN CC_Blockout      BLKO1 ON BLKO1.BLKO_ID    = CF.BLKO_ID_Pt1
  LEFT OUTER JOIN CC_Blockout      BLKO2 ON BLKO2.BLKO_ID    = CF.BLKO_ID_Pt2
  LEFT OUTER JOIN CC_Blockout      BLKO3 ON BLKO3.BLKO_ID    = CF.BLKO_ID_Pt3
  LEFT OUTER JOIN CC_Blockout      BLKO4 ON BLKO4.BLKO_ID    = CF.BLKO_ID_Pt4
  LEFT OUTER JOIN CC_Blockout      BLKO5 ON BLKO5.BLKO_ID    = CF.BLKO_ID_Pt5
  LEFT OUTER JOIN CC_Blockout      BLKO6 ON BLKO6.BLKO_ID    = CF.BLKO_ID_Pt6
  LEFT OUTER JOIN CC_Blockout      BLKO7 ON BLKO7.BLKO_ID    = CF.BLKO_ID_Pt7
  LEFT OUTER JOIN CC_Bank          BNK   ON BNK.BANK_ID      = CF.BANK_ID
  LEFT OUTER JOIN CC_Panto         PAN   ON PAN.PANTO_Code   = CF.PANTO_Code
  LEFT OUTER JOIN D_Head           DH    ON DH.oh_ID         = CF.oh_ID 
  WHERE CF.oh_id=@OH_ID

END






















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptDocketLabel]') and xtype = 'P ')  
 drop Procedure sp_RptDocketLabel
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

-- sp_RptDocketLabel 'C', '123456'

CREATE proc [dbo].[sp_RptDocketLabel]
    @Dkt_Type varchar(1),
    @Dkt_Num varchar(7)
as
  SELECT @Dkt_Type as DocketType, @Dkt_Num as DocketNum























































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptMonthEndScore]') and xtype = 'P ')  
 drop Procedure sp_RptMonthEndScore
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

-- sp_RptMonthEndScore 'ABC Company', 2007, 1

CREATE PROCEDURE [dbo].[sp_RptMonthEndScore]
  @CompanyName varchar(200),
  @YEAR_NUM int,
  @MONTH_NUM int
 AS
        
SET @MONTH_NUM=7
SET @YEAR_NUM=2007

SELECT '' + @CompanyName + '' AS ReportCompanyName, DH.oh_type, sum(cast(DH.oh_net_cost as decimal (15,2))) AS Amount, RGS.RegionGrp_ID, RGH.RegionGrp_Desc
FROM D_Head DH
LEFT OUTER JOIN C_RegionGroupSet  RGS ON (RGS.TERRITORY_ID = DH.oh_region)
LEFT OUTER JOIN C_RegionGroupHead RGH ON (RGH.RegionGrp_ID = RGS.RegionGrp_ID)
LEFT OUTER JOIN C_DocketType DTyp ON (DTyp.DktType = DH.oh_type)
WHERE (DH.oh_s_flag='D') AND (DH.oh_status <> 'H') AND (DH.oh_status <> 'N')
  AND (month(DH.oh_s_date) = @MONTH_NUM) AND (year(DH.oh_s_date) = @YEAR_NUM)
GROUP BY DTyp.QSel_Sort, DH.oh_type, RGS.RegionGrp_ID, RGH.RegionGrp_Desc
ORDER BY DTyp.QSel_Sort 
























































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptPackSlip]') and xtype = 'P ')  
 drop Procedure sp_RptPackSlip
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go




-- sp_RptPackSlip '', '2009', '1', '21', 'Y'
-- sp_RptPackSlip 'AND SD.oh_id=369139 AND BoxLabelOnly=''N''', '2009', '1', '21', 'Y'

CREATE proc [dbo].[sp_RptPackSlip]
    @AND_CLAUSE varchar(2000),
    @PACKSLIP_DATE_YEAR varchar(5),
    @PACKSLIP_DATE_MONTH varchar(5),
    @PACKSLIP_DATE_DAY varchar(5),
    @EMAIL_YN varchar(1)
as
SET NOCOUNT ON;

declare
  @sql_str varchar(8000),  @tr_db varchar(50),
  @DHL_CustID varchar(20)

  select @tr_db=isnull(tr_db,'') from database_setup

  select @DHL_CustID=isnull(Value,'CUSTID') from CU_CtlString WHERE CSTR_Type='SHIP_DHL_REALCUSTID' AND PCUser='ALL'


  set @sql_str =
'SELECT SD.sd_seq, ' + SUBSTRING(@PACKSLIP_DATE_YEAR, 3, 2) + ' AS PackSlipDate_Year, ' + @PACKSLIP_DATE_MONTH  + ' AS PackSlipDate_Month, ' +
@PACKSLIP_DATE_DAY + ' AS PackSlipDate_Day, ' +
' SD.oh_id, DH.oh_type, DH.oh_num, DH.oh_dealer_po,
CASE WHEN (DH.oh_fob=''D'') THEN DH.oh_dealer_po ELSE DH.oh_cust_po END AS ShipToPO,
  From_Name       = CASE WHEN ((LAB.dlr_logo_bmp is null) OR (VC.NAME <> SH.Fr_Name)) THEN SH.Fr_Name ELSE '''' END,
  From_DealerLogo = CASE WHEN ((LAB.dlr_logo_bmp is null) OR (VC.NAME <> SH.Fr_Name)) THEN null ELSE LAB.dlr_logo_bmp END,
 SH.Fr_Addr1, SH.Fr_Addr2, SH.Fr_Addr3, SH.Fr_City, SH.Fr_Prov, SH.Fr_Zip,
  To_Name         = CASE WHEN ((LAB.dlr_logo_bmp is null) OR (VC.NAME <> SH.To_Name)) THEN SH.To_Name ELSE '''' END,
  To_DealerLogo   = CASE WHEN ((LAB.dlr_logo_bmp is null) OR (VC.NAME <> SH.To_Name)) THEN null ELSE LAB.dlr_logo_bmp END,
 SH.To_Addr1, SH.To_Addr2, SH.To_Addr3, SH.To_City, SH.To_Prov, SH.To_Zip, SH.To_Attn,
 SH.WaybillFr_Name, SH.WaybillFr_Addr1, SH.WaybillFr_Addr2, SH.WaybillFr_Addr3, SH.WaybillFr_City, SH.WaybillFr_Prov,
 SH.WaybillFr_Zip, SH.WaybillTo_Name, SH.WaybillTo_Addr1, SH.WaybillTo_Addr2, SH.WaybillTo_Addr3, SH.WaybillTo_City,
 SH.WaybillTo_Prov, SH.WaybillTo_Zip, SH.WaybillTo_Attn, SH.WaybillTo_Country, SH.WaybillTo_Phone,
 SH.Waybill_Num, COU.COU_Desc AS Courier_Desc, isnull(COU.COU_Account, '''') AS Courier_Account, isnull(COU.COU_Phone, '''') AS Courier_Phone, isnull(SH.Waybill_Notes, '''') AS WaybillNotes,
 CASE WHEN isNull(SD.sd_num_from, '''') <> '''' then isNull(SD.sd_prefix, '''') + SD.sd_num_from + isNull(SD.sd_suffix, '''') + '' to ''
      ELSE ''''
 END + isNull(SD.sd_prefix, '''') + isNull(SD.sd_num_to, '''') + isNull(SD.sd_suffix, '''') AS Numbering,
 SH.Prepaid_Collect, SH.oh_QG_Qty AS OrderQty, SH.MissingNumbers, QG.QG_Desc AS Description,
 SD.sd_boxes AS Cartons, isnull(DH.oh_wt_per_k,0) * isnull(SD.sd_per_box,0) / 1000 AS Weight, SD.sd_per_box AS QtyPerCarton,
 CASE WHEN (COU.COU_IntegrationType = ''D'') THEN ''Y'' ELSE ''N'' END AS IsDHL_YN,
 SH.Fr_Phone, SH.To_Phone, SH.SH_FIRST_PARCEL_OUTPUT_STRING
FROM SHIP_Detail SD
LEFT OUTER JOIN SHIP_Head SH ON SH.oh_id = SD.oh_id
LEFT OUTER JOIN D_Head DH ON DH.oh_id = SH.oh_id
LEFT OUTER JOIN Q_HEAD QH ON QH.Q_ID = DH.Q_ID 
LEFT OUTER JOIN Q_Group QG ON QG.QG_ID = DH.QG_ID
LEFT OUTER JOIN dlr_lab LAB ON LAB.dlr_Customer_ID = QH.Customer_ID
LEFT OUTER JOIN ' + @tr_db + '..CUSTOMERS VC ON VC.CUSTOMER_ID = QH.Customer_ID
LEFT OUTER JOIN COU_Courier COU ON COU.COU_ID = SH.COU_ID
WHERE (SD.oh_id > 0) ' + @AND_CLAUSE + '
UNION ALL
SELECT SD.sd_seq, ' + SUBSTRING(@PACKSLIP_DATE_YEAR, 3, 2) + ' AS PackSlipDate_Year, ' + @PACKSLIP_DATE_MONTH  + ' AS PackSlipDate_Month, ' +
@PACKSLIP_DATE_DAY + ' AS PackSlipDate_Day, ' +
' SD.oh_id, ''M'' AS oh_type, M.SO_NO as oh_num, '''' as oh_dealer_po, M.PONO AS ShipToPO,
  From_Name       = CASE WHEN ((LAB.dlr_logo_bmp is null) OR (VC.NAME <> SH.Fr_Name)) THEN SH.Fr_Name ELSE '''' END,
  From_DealerLogo = CASE WHEN ((LAB.dlr_logo_bmp is null) OR (VC.NAME <> SH.Fr_Name)) THEN null ELSE LAB.dlr_logo_bmp END,
 SH.Fr_Addr1, SH.Fr_Addr2, SH.Fr_Addr3, SH.Fr_City, SH.Fr_Prov, SH.Fr_Zip,
  To_Name         = CASE WHEN ((LAB.dlr_logo_bmp is null) OR (VC.NAME <> SH.To_Name)) THEN SH.To_Name ELSE '''' END,
 To_DealerLogo   = CASE WHEN ((LAB.dlr_logo_bmp is null) OR (VC.NAME <> SH.To_Name)) THEN null ELSE LAB.dlr_logo_bmp END,
 SH.To_Addr1, SH.To_Addr2, SH.To_Addr3, SH.To_City, SH.To_Prov, SH.To_Zip, SH.To_Attn,
 SH.WaybillFr_Name, SH.WaybillFr_Addr1, SH.WaybillFr_Addr2, SH.WaybillFr_Addr3, SH.WaybillFr_City, SH.WaybillFr_Prov,
 SH.WaybillFr_Zip, SH.WaybillTo_Name, SH.WaybillTo_Addr1, SH.WaybillTo_Addr2, SH.WaybillTo_Addr3, SH.WaybillTo_City,
 SH.WaybillTo_Prov, SH.WaybillTo_Zip, SH.WaybillTo_Attn, SH.WaybillTo_Country, SH.WaybillTo_Phone,
 SH.Waybill_Num, COU.COU_ShortDesc AS Courier_Desc, isnull(COU.COU_Account, '''') AS Courier_Account, isnull(COU.COU_Phone, '''') AS Courier_Phone, isnull(SH.Waybill_Notes, '''') AS WaybillNotes,
 CASE WHEN isNull(SD.sd_num_from, '''') <> '''' then isNull(SD.sd_prefix, '''') + SD.sd_num_from + isNull(SD.sd_suffix, '''') + '' to ''
      ELSE ''''
 END + isNull(SD.sd_prefix, '''') + isNull(SD.sd_num_to, '''') + isNull(SD.sd_suffix, '''') AS Numbering,
 SH.Prepaid_Collect, SH.oh_QG_Qty AS OrderQty, SH.MissingNumbers, MD.DESCRIPTION AS Description,
 SD.sd_boxes AS Cartons, INV.WEIGHT * isnull(SD.sd_per_box,0) / 1000 AS Weight, SD.sd_per_box AS QtyPerCarton,
 CASE WHEN (COU.COU_IntegrationType = ''D'') THEN ''Y'' ELSE ''N'' END AS IsDHL_YN,
 SH.Fr_Phone, SH.To_Phone, SH.SH_FIRST_PARCEL_OUTPUT_STRING
FROM SHIP_Detail SD
LEFT OUTER JOIN SHIP_Head SH ON SH.oh_id = SD.oh_id
LEFT OUTER JOIN ' + @tr_db + '..SO_MASTER_DET_LINE MDL ON MDL.SO_LINE_DUE_ID = - SD.oh_ID
LEFT OUTER JOIN ' + @tr_db + '..SO_MASTER_DETAIL MD ON MD.SO_LINE_ID = MDL.SO_LINE_ID
LEFT OUTER JOIN ' + @tr_db + '..SO_MASTER_HDR M ON M.SO_ID=MD.SO_ID
LEFT OUTER JOIN dlr_lab LAB ON LAB.dlr_Customer_ID = M.Customer_ID
LEFT OUTER JOIN ' + @tr_db + '..CUSTOMERS VC ON VC.CUSTOMER_ID = M.Customer_ID
LEFT OUTER JOIN COU_Courier COU ON COU.COU_ID = SH.COU_ID
LEFT OUTER JOIN ' + @tr_db + '..INVENTORY INV ON INV.INV_ID = MD.INV_ID
WHERE (SD.oh_id < 0) ' + @AND_CLAUSE + '
ORDER BY oh_type, oh_num, SD.sd_seq'

exec (@sql_str)
--select @sql_str









Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptPayrExportReg]') and xtype = 'P ')  
 drop Procedure sp_RptPayrExportReg
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

-- sp_RptPayrExportReg 'ABC Company', 'N', 'WHERE 1=1'

CREATE PROCEDURE [dbo].[sp_RptPayrExportReg]
  @CompanyName varchar(200),
  @BreakOnEmployee_YN varchar(1),
  @where_clause varchar(2000)
 AS

declare @sql varchar(5000)

set @sql=
'
SELECT ''' + @CompanyName + ''' AS ReportCompanyName, ''' + @BreakOnEmployee_YN + ''' AS BreakOnEmployee, Emp_Last_Name, Emp_First_Name, Docket_Type, Docket_Number,
CASE WHEN (Is_Press = ''T'') THEN ''Press'' ELSE ''Collate'' END AS JobDescription,
Is_Press, Is_Collate, Pct_Complete, PY_Calc_Amount, Entry_Date,
Setup_Hrs, Run_hrs, Total_Hrs, PY_Rate, PY_Group, PY_Code, Adj_Amount, Comment, ''' + @BreakOnEmployee_YN + ''' AS BreakOnEmployee_YN 
FROM working_py_integration
' +
@where_clause

exec(@sql)















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptPresstin]') and xtype = 'P ')  
 drop Procedure sp_RptPresstin
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go


-- sp_RptPresstin 'ABC Company', 1000, 'My TestRun', 'AND isnull(Current_Status,'''')<>''MM''', ''
-- sp_RptPresstin 'ABC Company', 1000, 'My TestRun', '', 'INNER JOIN SELECT_ID SEL ON SEL.ID=D.oh_id AND SEL.PCUser=''FRED'''

CREATE PROCEDURE [dbo].[sp_RptPresstin]
  @CompanyName varchar(200),
  @PressFactor varchar(20),
  @RunDescription varchar(200),
  @and_clause varchar(2000),
  @join_clause varchar(2000)
 AS
 
declare @tr_db varchar(50)
select @tr_db=isnull(tr_db,'') from database_setup


declare @sql varchar(5000)

set @sql=
'
SELECT ''' + @CompanyName + ''' AS ReportCompanyName, ' + @PressFactor + ' AS PressFactor, ''' + @RunDescription + ''' AS RunDescription,
 D.oh_ID, D.oh_type, D.oh_num, D.oh_ship_date, D.oh_proof_yn, D.oh_prio as PRIORITY_YN, oh_Nrcv_date as ORDER_DATE, oh_backprint, oh_micr_from, oh_micr_to,  
CASE WHEN (LEN(D.oh_bookin) > 0) OR (LEN(D.oh_at) > 0) OR (LEN(D.oh_pts) > 0) OR (LEN(D.oh_BookShrinkIn) > 0) OR (LEN(REPLACE(REPLACE(CAST(D.oh_BookNote AS Varchar), CHAR(13), ''''), CHAR(10), '''')) > 1)  THEN ''Y'' ELSE ''N'' END AS BIND_YN,
CASE WHEN (LEN(D.oh_micr_from) > 0) THEN ''Y'' ELSE ''N'' END AS CODA_YN,
CASE WHEN (D.oh_cut_sheet = ''Y'') THEN ''Y'' ELSE ''N'' END AS CUT_YN,
Q.Q_Width, Q.Q_Length, Q.Customer_ID, VC.NAME, Q.Q_ClientName, VC.TERRITORY_ID, T.TERRITORY_DESC,
STAT.Current_Status, STAT.Date_Last_Changed, DATEDIFF(dd, STAT.Date_Last_Changed, GETDATE()) AS Status_Days, CSC.Status_Desc AS Current_Status_Desc,
STAT.Current_Dept, STAT.Date_Last_Dept_Chg, DATEDIFF(dd, STAT.Date_Last_Dept_Chg, GETDATE()) AS Dept_Days, DEPT.Dept_Description AS Current_Dept_Desc,
QG.QG_Parts, QG.QG_Qty, QG.QG_DealerTotCost, QG_GoldPress_SetupHr + QG_GoldPress_RunHr AS HOURS_PRESS, QG_GoldCollate_SetupHr + QG_GoldCollate_RunHr AS HOURS_COLLATE,
QG.QG_Desc, QG.QG_PresstinBinderyHr AS HOURS_BINDERY,
CASE WHEN (Q_FBC1=''B'') THEN ''('' ELSE '''' END + RTRIM(Q.Q_Ink1) + CASE WHEN (Q_FBC1=''B'') THEN '')'' ELSE '''' END +
CASE WHEN (LEN(RTRIM(Q.Q_Ink2)) = 0) THEN '''' ELSE '','' +
  CASE WHEN (Q_FBC2=''B'') THEN ''('' ELSE '''' END + RTRIM(Q.Q_Ink2) + CASE WHEN (Q_FBC2=''B'') THEN '')'' ELSE '''' END END +
CASE WHEN (LEN(RTRIM(Q.Q_Ink3)) = 0) THEN '''' ELSE '','' +
  CASE WHEN (Q_FBC3=''B'') THEN ''('' ELSE '''' END + RTRIM(Q.Q_Ink3) + CASE WHEN (Q_FBC3=''B'') THEN '')'' ELSE '''' END END +
CASE WHEN (LEN(RTRIM(Q.Q_Ink4)) = 0) THEN '''' ELSE '','' +
  CASE WHEN (Q_FBC4=''B'') THEN ''('' ELSE '''' END + RTRIM(Q.Q_Ink4) + CASE WHEN (Q_FBC4=''B'') THEN '')'' ELSE '''' END END +
CASE WHEN (LEN(RTRIM(Q.Q_Ink5)) = 0) THEN '''' ELSE '','' +
  CASE WHEN (Q_FBC5=''B'') THEN ''('' ELSE '''' END + RTRIM(Q.Q_Ink5) + CASE WHEN (Q_FBC5=''B'') THEN '')'' ELSE '''' END END   AS Inks
FROM D_Head D
LEFT OUTER JOIN Q_Head Q ON Q.Q_ID = D.Q_ID
LEFT OUTER JOIN Q_Group QG ON D.QG_ID = QG.QG_ID
LEFT OUTER JOIN ' + @tr_db + '..CUSTOMERS VC ON Q.Customer_ID= VC.CUSTOMER_ID
LEFT OUTER JOIN ' + @tr_db + '..TERRITORY T ON T.TERRITORY_ID = VC.TERRITORY_ID
LEFT OUTER JOIN D_Status STAT ON STAT.Docket_ID = D.oh_ID
LEFT OUTER JOIN C_Status_Ctrl CSC ON CSC.Status_Code = STAT.Current_Status
LEFT OUTER JOIN C_Dept_Ctrl DEPT ON DEPT.Dept_Code = STAT.Current_Dept
' +
@join_clause + '
WHERE D.oh_status != ''N'' AND D.oh_status != ''Z'' AND D.oh_status != ''X''
' +
@and_clause
exec(@sql)






Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptProcessHistory]') and xtype = 'P ')  
 drop Procedure sp_RptProcessHistory
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go


-- exec sp_RptProcessHistory 646388
CREATE   proc [dbo].[sp_RptProcessHistory] @OH_ID int
as
declare @tr_db varchar(50),
        @sql_str varchar(5000)

select @tr_db=tr_db from database_setup

set @sql_str='
SELECT DH.oh_type, DH.oh_num, QH.Q_Whs, QH.Q_Num, QG.QG_Desc AS FormDescription,
  UPPER(VC.NAME) AS CustomerName, QH.Q_ClientName, 
  UPPER(VSM_IN.FIRSTNAME) AS VSM_INSIDE_SMAN_NAME, UPPER(VSM_BY.FIRSTNAME) AS VSM_TAKENBY_SMAN_NAME,
  DH.oh_ship_date, DH.oh_OrigShipDate,
  TX.Scan_Date, TX.Swipe_Code, TX.Status_Code, CC.Status_Desc, CC.Dept_Code, DEPT.Dept_Description,
  TX.Pct_Complete 
  FROM D_Status ST
  LEFT JOIN D_BC_Transactions TX on TX.Docket_ID = ' + cast(@OH_ID as varchar(50)) + '
  LEFT JOIN C_Status_Ctrl CC ON CC.Status_Code = TX.Status_Code
  LEFT JOIN C_Dept_Ctrl DEPT ON DEPT.Dept_Code = CC.Dept_Code 
  LEFT JOIN D_HEAD DH ON DH.OH_ID = ' + cast(@OH_ID as varchar(50)) + ' 
  LEFT JOIN Q_HEAD QH ON DH.Q_ID = QH.Q_ID 
  LEFT JOIN Q_GROUP QG ON DH.QG_ID = QG.QG_ID
  LEFT JOIN '+@tr_db+'..SALESPERSONS VSM_BY ON DH.SLS_ID_TakenBy = VSM_BY.SLS_ID
  LEFT JOIN '+@tr_db+'..SALESPERSONS VSM_IN ON DH.SLS_ID_Inside  = VSM_IN.SLS_ID
  LEFT JOIN '+@tr_db+'..customers vc ON QH.Customer_ID = VC.CUSTOMER_ID
  LEFT JOIN '+@tr_db+'..territory t ON T.TERRITORY_ID=VC.TERRITORY_ID
WHERE ST.Docket_ID = '+cast(@OH_ID as varchar(50)) + '
ORDER BY TX.pk_id DESC'
exec(@sql_str)




















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptReleaseToInventory]') and xtype = 'P ')  
 drop Procedure sp_RptReleaseToInventory
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go







-- sp_RptReleaseToInventory 'Factor', 'WHERE (DH.oh_ID = 646387) or (DH.oh_ID = 646388)'
-- sp_RptReleaseToInventory 'ABC Company', 'WHERE DH.oh_ID=646387'

CREATE   proc [dbo].[sp_RptReleaseToInventory]
 @CompanyName varchar(200),
 @where_clause varchar(2000)
as
declare @tr_db varchar(50),
        @sql_str varchar(5000)

select @tr_db=tr_db from database_setup

set @sql_str='
SELECT ''' + @CompanyName + ''', DH.oh_type, DH.oh_num, DH.oh_Whs, WHS.DESCRIPTION AS Whs_Description,
 getdate() AS ReleaseToInventoryDate,
 VC.CUSTOMER_ID, VC.CUSTOMER_CODE, UPPER(VC.NAME) AS NAME, UPPER(VC.BILL_ADDRESS_1) AS BILL_ADDRESS_1, UPPER(VC.BILL_ADDRESS_2) AS BILL_ADDRESS_2,
 UPPER(VC.BILL_ADDRESS_3) AS BILL_ADDRESS_3, UPPER(VC.BILL_CITY) AS BILL_CITY, UPPER(VC.BILL_STATE) AS BILL_STATE, UPPER(VC.BILL_ZIP) AS BILL_ZIP,
 VC.TELEPHONE, VC.FAX, T.TERRITORY_ID, UPPER(T.TERRITORY_CODE) AS TERRITORY_CODE, UPPER(T.TERRITORY_DESC) AS TERRITORY_DESC,
 UPPER(VSM_IN.FIRSTNAME) AS VSM_INSIDE_SMAN_NAME, UPPER(VSM_BY.FIRSTNAME) AS VSM_TAKENBY_SMAN_NAME,
 RTRIM(QH.Q_Whs) + '' '' +  LTRIM(STR(QH.Q_Num)) AS QUOTE_NUMBER,
 QH.InternalInventoryPartNum, INV.DESCRIPTION AS PartDescription,
 QG.QG_Qty, QG.QG_Desc, QG.QG_Parts, QG.QG_DealerCostLot, QG.QG_DealerTotCost, QG.QG_InternalCost
  FROM D_Head DH
  LEFT OUTER JOIN Q_HEAD QH ON DH.Q_ID = QH.Q_ID 
  LEFT OUTER JOIN Q_GROUP QG ON DH.QG_ID = QG.QG_ID
  LEFT OUTER JOIN '+@tr_db+'..SALESPERSONS VSM_BY ON DH.SLS_ID_TakenBy = VSM_BY.SLS_ID
  LEFT OUTER JOIN '+@tr_db+'..SALESPERSONS VSM_IN ON DH.SLS_ID_Inside  = VSM_IN.SLS_ID
  LEFT OUTER JOIN '+@tr_db+'..customers vc ON QH.Customer_ID = VC.CUSTOMER_ID
  LEFT OUTER JOIN '+@tr_db+'..territory t ON T.TERRITORY_ID=VC.TERRITORY_ID
  LEFT OUTER JOIN '+@tr_db+'..INVENTORY INV ON INV.PART_NUMBER = QH.InternalInventoryPartNum
  LEFT OUTER JOIN '+@tr_db+'..WAREHOUSE WHS ON WHS.WAREHOUSE = DH.oh_Whs
' + @where_clause

exec(@sql_str)




















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptReorderCard]') and xtype = 'P ')  
 drop Procedure sp_RptReorderCard
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
-- exec sp_RptReorderCard 427812

CREATE   proc [dbo].[sp_RptReorderCard] @OH_ID int
as
SET NOCOUNT ON;

declare @tr_db varchar(50),
        @web_dbname varchar(50),
        @AutoID varchar(10),
        @sql_str varchar(5000)

select @web_dbname=web_db, @tr_db=tr_db, @AutoID=AutoID FROM database_setup

declare	@O_DEALER_NAME varchar(40),
        @O_BILL_ADDRESS_1 varchar(40),		@O_BILL_ADDRESS_2 varchar(40),		@O_BILL_ADDRESS_3 varchar(40),
		@O_BILL_CITY varchar(20),			@O_BILL_STATE varchar(2),			@O_BILL_ZIP varchar(10),	 
	    @O_TELEPHONE varchar(20),           @O_FAX varchar(20),					@O_EMAIL varchar(40)

declare @DH_oh_type varchar(1),				@DH_oh_num int,						@QH_Customer_ID int,
		@QH_Q_ClientName varchar(40),		@QG_Desc varchar(30)

--Get Scheduled Ship Date--> @O_ScheduleShip

SELECT	@DH_oh_type=ISNULL(oh_type,''),				@DH_oh_num=ISNULL(oh_num,0),
		@QH_Customer_ID = QH.Customer_ID,			@QH_Q_ClientName = ISNULL(QH.Q_ClientName,''),
		@QG_Desc = ISNULL(QG.QG_Desc,'')	
FROM D_Head DH
LEFT OUTER JOIN Q_Head QH ON DH.Q_ID = QH.Q_ID
LEFT OUTER JOIN Q_GROUP QG ON DH.QG_ID = QG.QG_ID
WHERE DH.oh_ID=@OH_ID


-- Get Default Email as per rules in FDGlobal.Lookup_Cust
CREATE TABLE #TMP2
(
  EMAIL varchar(200)
)

set @sql_str ='
 SELECT Email1Address
  FROM ' + @web_dbname + '..relations R
  LEFT JOIN ' + @web_dbname + '..contact C on C.id = R.itemid
  LEFT JOIN ' + @web_dbname + '..communication_defaults DEF ON DEF.CONTACT_ID = C.id
  WHERE R.targetid = ' + cast(@QH_Customer_ID as varchar) + '
  AND R.CompanyID=' + @AutoID + '
  AND DEF.COMM_PURPOSE_ID=7'
insert #TMP2
exec(@sql_str)
SELECT @O_EMAIL = EMAIL
FROM #TMP2
DROP TABLE #TMP2

IF (@O_EMAIL = '')
begin
  SELECT @O_EMAIL = ISNULL(dlr_email,'')
  FROM dlr_lab
  WHERE dlr_Customer_ID = @QH_Customer_ID
end


--Get Other Info from TR DB Customers --> @O_<Stuff>
CREATE TABLE #TMP1
(
	DEALER_NAME varchar(40),
	BILL_ADDRESS_1 varchar(40),
	BILL_ADDRESS_2 varchar(40),
	BILL_ADDRESS_3 varchar(40),
	BILL_CITY varchar(20),
	BILL_STATE varchar(2),
	BILL_ZIP varchar(10),
	TELEPHONE varchar(20),
	FAX	varchar(20),
)
set @sql_str='SELECT NAME, BILL_ADDRESS_1, BILL_ADDRESS_2, BILL_ADDRESS_3, BILL_CITY, BILL_STATE, BILL_ZIP,
TELEPHONE, FAX
FROM ' + @tr_db + '..CUSTOMERS WHERE CUSTOMER_ID=' + cast(@QH_Customer_ID as varchar)
insert #TMP1
exec(@sql_str)
SELECT 
	@O_DEALER_NAME = DEALER_NAME,		@O_BILL_ADDRESS_1 =	BILL_ADDRESS_1,
	@O_BILL_ADDRESS_2 =	BILL_ADDRESS_2,	@O_BILL_ADDRESS_3 =	BILL_ADDRESS_3,
	@O_BILL_CITY = 	BILL_CITY,			@O_BILL_STATE =	BILL_STATE,
	@O_BILL_ZIP	= BILL_ZIP,			    @O_TELEPHONE = TELEPHONE,
	@O_FAX = FAX 
FROM #TMP1
DROP TABLE #TMP1

SELECT
@QH_Q_ClientName as CompanyName,		@QG_Desc as FormDesc,
@DH_oh_type + ' ' + cast(@DH_oh_num as varchar) as DocketNumber,
@O_DEALER_NAME as DealerName,			@O_BILL_ADDRESS_1 as BILL_ADDRESS_1,
@O_BILL_ADDRESS_2 as BILL_ADDRESS_2,	@O_BILL_ADDRESS_3 as BILL_ADDRESS_3,
@O_BILL_CITY as	BILL_CITY,				@O_BILL_STATE as BILL_STATE,
@O_BILL_ZIP	as BILL_ZIP,			    @O_TELEPHONE as TELEPHONE,
@O_FAX as FAX,							@O_EMAIL as EMAIL	 



Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptRolCount]') and xtype = 'P ')  
 drop Procedure sp_RptRolCount
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go


-- sp_RptRolCount 'ABC Company'

CREATE PROCEDURE [dbo].[sp_RptRolCount]
  @CompanyName varchar(200)
 AS

SET NOCOUNT ON;


declare @sql varchar(5000)

set @sql=
'
SELECT PT.pt_type, PT.pt_color, PT.pt_m, PT.pt_roll_width, PT.pt_desc, PT.pt_roll_weight AS FullWeight,
PT.pt_cost_cwt,  ((PT.pt_roll_weight * SI.si_percent_left) / 100) * PT.pt_cost_cwt AS TotalPrice,
SI.si_dt_added AS DateRcvd, SI.si_percent_left, (PT.pt_roll_weight * SI.si_percent_left) / 100 AS WeightLeft,
SI.si_roll_barcode, SU.su_Abbrev AS SupplierShortName, SU.su_Desc AS SupplierLongName,
ISNULL(SP.st_partno, '''') AS SupplierProductCode,
''Y'' AS InInventoryButNotCounted, ''N'' AS CountedButNotInInventory, FR_RW.FractionValue AS Width_Value
FROM ROL_SerInv SI
LEFT OUTER JOIN ROL_ProdType PT ON PT.pt_ID = SI.pt_ID
LEFT OUTER JOIN CB_Fraction FR_RW ON FR_RW.FractionString = PT.pt_roll_width
LEFT OUTER JOIN ROL_Supplier SU ON SU.su_ID = SI.su_ID
LEFT OUTER JOIN ROL_SupPartNo SP ON (SP.su_ID = SI.su_ID) AND (SP.pt_ID = SI.pt_ID)
WHERE ISNULL(SI.si_CountedYN, ''N'') <> ''Y''
UNION
SELECT PT.pt_type, PT.pt_color, PT.pt_m, PT.pt_roll_width, PT.pt_desc, PT.pt_roll_weight AS FullWeight,
PT.pt_cost_cwt,  ((PT.pt_roll_weight * SH.si_percent_left) / 100) * PT.pt_cost_cwt AS TotalPrice,
SH.si_dt_added AS DateRcvd, SH.si_percent_left, (PT.pt_roll_weight * SH.si_percent_left) / 100 AS WeightLeft,
SH.si_roll_barcode, SU.su_Abbrev AS SupplierShortName, SU.su_Desc AS SupplierLongName,
ISNULL(SP.st_partno, '''') AS SupplierProductCode,
''N'' AS InInventoryButNotCounted, ''Y'' AS CountedButNotInInventory, FR_RW.FractionValue AS Width_Value
FROM ROL_SerInvHst SH
LEFT OUTER JOIN ROL_ProdType PT ON PT.pt_ID = SH.pt_ID
LEFT OUTER JOIN CB_Fraction FR_RW ON FR_RW.FractionString = PT.pt_roll_width
LEFT OUTER JOIN ROL_Supplier SU ON SU.su_ID = SH.su_ID
LEFT OUTER JOIN ROL_SupPartNo SP ON (SP.su_ID = SH.su_ID) AND (SP.pt_ID = SH.pt_ID)
WHERE ISNULL(SH.si_CountedYN, ''N'') = ''Y''
ORDER BY CountedButNotInInventory, pt_type, pt_color, pt_m, Width_Value


'
exec(@sql)






















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptRolDocket]') and xtype = 'P ')  
 drop Procedure sp_RptRolDocket
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

-- sp_RptRolDocket 'ABC Company', ''
-- sp_RptRolDocket 'ABC Company', 'INNER JOIN SELECT_ID SEL ON SEL.ID=D.oh_id AND SEL.PCUser=''FRED'''

CREATE PROCEDURE [dbo].[sp_RptRolDocket]
  @CompanyName varchar(200),
  @join_clause varchar(2000)
 AS

declare @sql varchar(5000)

set @sql=
'
SELECT ''' + @CompanyName + ''' AS ReportCompanyName,
D.oh_type, D.oh_num, P.pi_seq AS PartNumber,
PT.pt_type, PT.pt_color, PT.pt_m, PT.pt_roll_width,
RD.d_pi_size AS FormWidth, RD.d_tot_wt, RD.d_rolls_rqd
FROM ROL_Docket RD
LEFT OUTER JOIN D_Parts P ON P.pi_ID = RD.pi_ID
LEFT OUTER JOIN D_Head D ON D.oh_ID = P.oh_ID
LEFT OUTER JOIN ROL_ProdType PT ON PT.pt_ID = RD.pt_ID
' +
@join_clause +
'
ORDER BY D.oh_type, D.oh_num, P.pi_seq'
exec(@sql)






















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptRolInvList]') and xtype = 'P ')  
 drop Procedure sp_RptRolInvList
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

-- sp_RptRolInvList 'ABC Company', ''
-- sp_RptRolInvList 'ABC Company', 'INNER JOIN SELECT_ID SEL ON SEL.ID=SI.si_roll_barcode AND SEL.PCUser=''FRED'''

CREATE PROCEDURE [dbo].[sp_RptRolInvList]
  @CompanyName varchar(200),
  @join_clause varchar(2000)
 AS

SET NOCOUNT ON;


declare @sql varchar(5000)

set @sql=
'
SELECT PT.pt_type, PT.pt_color, PT.pt_m, PT.pt_roll_width, PT.pt_desc, SI.si_roll_weight AS FullWeight,
SI.si_cost_cwt,  ((SI.si_roll_weight * SI.si_percent_left) / 10000) * SI.si_cost_cwt AS TotalPrice,
SI.si_dt_added AS DateRcvd, SI.si_percent_left, (SI.si_roll_weight * SI.si_percent_left) / 100 AS WeightLeft,
SI.si_roll_barcode, SU.su_Abbrev AS SupplierShortName, SU.su_Desc AS SupplierLongName,
ISNULL(SP.st_partno, '''') AS SupplierProductCode
FROM ROL_SerInv SI
LEFT OUTER JOIN ROL_ProdType PT ON PT.pt_ID = SI.pt_ID
LEFT OUTER JOIN CB_Fraction FR_RW ON FR_RW.FractionString = PT.pt_roll_width
LEFT OUTER JOIN ROL_Supplier SU ON SU.su_ID = SI.su_ID
LEFT OUTER JOIN ROL_SupPartNo SP ON (SP.su_ID = SI.su_ID) AND (SP.pt_ID = SI.pt_ID)
' +
@join_clause +
'
ORDER BY PT.pt_type, PT.pt_color, PT.pt_m, FR_RW.FractionValue
'
exec(@sql)





























Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptRolLabel]') and xtype = 'P ')  
 drop Procedure sp_RptRolLabel
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
-- sp_RptRolLabel 'where si_roll_barcode=84587'
-- sp_RptRolLabel 'where si_update_flag=1'

CREATE PROCEDURE [dbo].[sp_RptRolLabel]
  @where_clause varchar(2000)

 AS

declare @sql varchar(5000)

set @sql=
'
SELECT SI.si_roll_barcode AS ROLL_BARCODE, SI.si_roll_weight AS ROLL_WEIGHT, SI.si_dt_added AS ROLL_DATE_RCVD,
  SUP.su_Abbrev AS SUPPLIER_SHORTNAME, SUP.su_Desc AS SUPPLIER_LONGNAME,
  PROD.pt_desc AS PRODUCT_DESCRIPTION
FROM ROL_SerInv SI
LEFT OUTER JOIN ROL_Supplier SUP ON SUP.su_ID = SI.su_ID
LEFT OUTER JOIN ROL_ProdType PROD ON PROD.pt_ID = SI.pt_ID
' + @where_clause + '
ORDER BY ROLL_BARCODE
'

exec(@sql)










Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptRolPaperSum]') and xtype = 'P ')  
 drop Procedure sp_RptRolPaperSum
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

-- sp_RptRolPaperSum 'ABC Company', ''
-- sp_RptRolPaperSum 'ABC Company', 'INNER JOIN SELECT_ID SEL ON SEL.ID=PT.pt_ID AND SEL.PCUser=''FRED'''

CREATE PROCEDURE [dbo].[sp_RptRolPaperSum]
  @CompanyName varchar(200),
  @join_clause varchar(2000)
 AS

SET NOCOUNT ON;


declare @sql varchar(5000)

set @sql=
'
SELECT PT.pt_type, PT.pt_color, PT.pt_m, PT.pt_roll_width, PT.pt_desc,
ISNULL(PT.pt_qoh,0) AS Rolls_InStock, ISNULL(PT.pt_qoo,0) AS Rolls_Ordered, ISNULL(PT.pt_alloc,0) AS Rolls_RqdByDockets,
ISNULL(PT.pt_min_avail,0) AS MinQty, ISNULL(PT.pt_need_to_order,0) AS NeedToOrder
FROM ROL_ProdType PT
LEFT OUTER JOIN CB_Fraction FR_RW ON FR_RW.FractionString = PT.pt_roll_width
' +
@join_clause +
'
ORDER BY PT.pt_type, PT.pt_color, PT.pt_m, FR_RW.FractionValue
'
exec(@sql)






















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptRolPoPrint]') and xtype = 'P ')  
 drop Procedure sp_RptRolPoPrint
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

-- exec sp_RptRolPoPrint 'Factor Edmonton', 57
CREATE   proc [dbo].[sp_RptRolPoPrint]
    @CompanyName varchar(200),
    @poh_ID int
as

UPDATE ROL_ProdType
SET pt_alloc =  (SELECT ISNULL(SUM(d_rolls_rqd),0)
                 FROM ROL_Docket AS RD
                 WHERE RD.pt_ID = ROL_ProdType.pt_ID)
UPDATE ROL_ProdType
SET pt_qoh =  (SELECT ISNULL(SUM(si_percent_left),0) / 100
               FROM ROL_SerInv AS RS
               WHERE RS.pt_ID = ROL_ProdType.pt_ID)


SELECT @CompanyName AS ReportCompanyName,
PH.poh_RefNum AS PO_Number, PH.poh_gen_date AS Generated_Date,
PD.pod_seq, (PD.pod_ord_qty - PD.pod_rcvd_qty) AS OrderQty, 
PT.pt_type AS PaperType, PT.pt_color AS PaperColor, PT.pt_m AS PaperM, PT.pt_roll_width AS RollWidth,
PT.pt_desc AS ProductDescription, (PT.pt_qoh - PT.pt_alloc) AS Available, PT.pt_min_avail AS Min_Qty,
PT.pt_cost_cwt AS CostPerCWT,  (PT.pt_roll_weight * PT.pt_cost_cwt)/100 AS RollCost,
 ((PD.pod_ord_qty - PD.pod_rcvd_qty) * PT.pt_roll_weight * PT.pt_cost_cwt)/100 AS LineCost,
SU.su_Abbrev AS Supplier_Code, SU.su_Desc AS Supplier_Name,
SP.st_partno AS Supplier_PartNumber

FROM ROL_PO_Det PD
LEFT OUTER JOIN ROL_PO_Head PH ON PH.poh_ID = PD.poh_ID
LEFT OUTER JOIN ROL_ProdType PT ON PT.pt_ID = PD.pt_ID
LEFT OUTER JOIN ROL_Supplier SU ON SU.su_ID = PH.su_ID
LEFT OUTER Join ROL_SupPartNo SP ON (SP.su_ID = PH.su_ID) AND (SP.pt_ID = PD.pt_ID)
WHERE PD.poh_id=@poh_ID























































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptRolPress]') and xtype = 'P ')  
 drop Procedure sp_RptRolPress
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

-- sp_RptRolPress 'ABC Company', ''
-- sp_RptRolPress 'ABC Company', 'INNER JOIN SELECT_ID SEL ON SEL.ID=D.oh_id AND SEL.PCUser=''FRED'''

CREATE PROCEDURE [dbo].[sp_RptRolPress]
  @CompanyName varchar(200),
  @join_clause varchar(2000)
 AS

declare @sql varchar(5000)

set @sql=
'
SELECT ''' + @CompanyName + ''' AS ReportCompanyName,
ISNULL(ST.Current_Status, -1) AS Status_Code, ISNULL(STC.Status_Desc, ''UnScanned'') AS Status_Desc,
D.oh_type, D.oh_num, P.pi_seq AS PartNumber,
PT.pt_type, PT.pt_color, PT.pt_m, PT.pt_roll_width,
RD.d_pi_size AS FormWidth, RD.d_tot_wt, RD.d_rolls_rqd
FROM ROL_Docket RD
LEFT OUTER JOIN D_Parts P ON P.pi_ID = RD.pi_ID
LEFT OUTER JOIN D_Head D ON D.oh_ID = P.oh_ID
LEFT OUTER JOIN ROL_ProdType PT ON PT.pt_ID = RD.pt_ID
LEFT OUTER JOIN CB_Fraction FR_RW ON FR_RW.FractionString = PT.pt_roll_width
LEFT OUTER JOIN D_Status ST ON ST.Docket_ID = D.oh_id
LEFT OUTER JOIN C_Status_Ctrl STC ON STC.Status_Code = ST.Current_Status 
' +
@join_clause +
'
ORDER BY Status_Code, PT.pt_type, PT.pt_color, PT.pt_m, FR_RW.FractionValue, D.oh_type, D.oh_num, P.pi_seq
'
exec(@sql)






















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptRolRemoved]') and xtype = 'P ')  
 drop Procedure sp_RptRolRemoved
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

-- sp_RptRolRemoved 'ABC Company', ''
-- sp_RptRolRemoved 'ABC Company', 'INNER JOIN SELECT_ID SEL ON SEL.ID=SIH.si_roll_barcode AND SEL.PCUser=''FRED'''

CREATE PROCEDURE [dbo].[sp_RptRolRemoved]
  @CompanyName varchar(200),
  @join_clause varchar(2000)
 AS

declare @sql varchar(5000)

set @sql=
'
SELECT ''' + @CompanyName + ''' AS ReportCompanyName,
SIH.si_dt_removed, SIH.si_roll_barcode, S.su_Abbrev,  
PT.pt_type, PT.pt_color, PT.pt_m, PT.pt_roll_width 
FROM ROL_SerInvHst SIH
LEFT OUTER JOIN ROL_ProdType PT ON PT.pt_ID = SIH.pt_ID
LEFT OUTER JOIN ROL_Supplier S ON S.su_ID = SIH.su_ID
' +
@join_clause +
'
ORDER BY SIH.si_roll_barcode
'
exec(@sql)






















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptRolUsage]') and xtype = 'P ')  
 drop Procedure sp_RptRolUsage
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

-- sp_RptRolUsage 'ABC Company', ''
-- sp_RptRolUsage 'ABC Company', 'INNER JOIN SELECT_ID SEL ON SEL.ID=SIH.si_roll_barcode AND SEL.PCUser=''FRED'''

CREATE PROCEDURE [dbo].[sp_RptRolUsage]
  @CompanyName varchar(200),
  @join_clause varchar(2000)
 AS

SET NOCOUNT ON;


declare @sql varchar(5000)

CREATE TABLE #TMP1
(
    Yr int,
    Mon int,
    pt_type varchar(5),
    pt_color varchar(6),
    pt_m int,
    pt_roll_width varchar(50),
)


set @sql=
'
SELECT YEAR(SIH.si_dt_removed) AS Yr, MONTH(SIH.si_dt_removed) AS Mon,  
PT.pt_type, PT.pt_color, PT.pt_m, PT.pt_roll_width 
FROM ROL_SerInvHst SIH
LEFT OUTER JOIN ROL_ProdType PT ON PT.pt_ID = SIH.pt_ID
LEFT OUTER JOIN CB_Fraction FR_RW ON FR_RW.FractionString = PT.pt_roll_width
' +
@join_clause +
'
ORDER BY Yr, Mon, PT.pt_type, PT.pt_color, PT.pt_m, FR_RW.FractionValue
'
INSERT #TMP1
exec(@sql)

CREATE TABLE #TMP2
(
    Yr int,
    Mon int,
    pt_type varchar(5),
    pt_color varchar(6),
    pt_m int,
    pt_roll_width varchar(50),
    cnt int
)

INSERT #TMP2
SELECT Yr, Mon,
 pt_type, pt_color, pt_m, pt_roll_width, count(*) AS RollCount FROM #TMP1
GROUP BY Yr, Mon, pt_type, pt_color, pt_m, pt_roll_width
ORDER BY Yr, Mon, pt_type, pt_color, pt_m, pt_roll_width

SELECT @CompanyName AS ReportCompanyName, Yr, Mon,
			CASE Mon
			WHEN 1  THEN 'JAN'
			WHEN 2  THEN 'FEB'
			WHEN 3  THEN 'MAR'
			WHEN 4  THEN 'APR'
			WHEN 5  THEN 'MAY'
			WHEN 6  THEN 'JUN'
			WHEN 7  THEN 'JUL'
			WHEN 8  THEN 'AUG'
			WHEN 9  THEN 'SEP'
			WHEN 10 THEN 'OCT'
			WHEN 11 THEN 'NOV'
			WHEN 12 THEN 'DEC'
			END as Month_Desc,
pt_type, pt_color, pt_m, pt_roll_width, cnt AS RollCount
FROM #TMP2






















































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptScore]') and xtype = 'P ')  
 drop Procedure sp_RptScore
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
-- sp_RptScore 'FRED'
CREATE PROCEDURE [dbo].[sp_RptScore]
  @UserName varchar(50)
AS
BEGIN
    
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    SELECT RGH.RegionGrp_Desc, WS.*  FROM Working_Score WS
    LEFT JOIN C_RegionGroupSet RGS ON RGS.TERRITORY_ID = WS.TERRITORY_ID
    LEFT JOIN C_RegionGroupHead RGH ON RGH.RegionGrp_ID = RGS.RegionGrp_ID
    WHERE username = @UserName
END





























































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptScoreAVolByBr]') and xtype = 'P ')  
 drop Procedure sp_RptScoreAVolByBr
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
-- =============================================
-- Create date: Aug 8/2007
-- Description:	Factor Score Report: Volume By Branch
-- =============================================
-- sp_RptScoreAVolByBr "Factor Forms Edmonton", "Fred", 2008
--
CREATE PROCEDURE [dbo].[sp_RptScoreAVolByBr] 
	@CompanyName varchar(200) = 'Factor Forms',
    @UserName varchar(50) = 'DEFAULT',
	@Year int = 2004
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	CREATE TABLE #TMP_Part
	(
		RegionGrp_ID int,
		RegionGrp_Desc varchar(25),
		Year int,
		MonthNum int,   
		MonthDesc varchar(3),
		AmtForMonth decimal(19,3),  
	)

	CREATE TABLE #TMP
	(
		RegionGrp_ID int,
		RegionGrp_Desc varchar(25),
		This_Year int,
		Last_Year int,
		MonthNum int,   
		MonthDesc varchar(3),
		ThisYear_AmtForMonth decimal(19,3),  
		ThisYear_AmtCum decimal(19,3),
		LastYear_AmtForMonth decimal(19,3),
		LastYear_AmtCum decimal(19,3)
	)

	DECLARE @MONTH_NUM int
	SET @MONTH_NUM = 1
	WHILE (@MONTH_NUM <= 12)
	BEGIN
		DECLARE @MONTH_ABBREV varchar(3)
		SET @MONTH_ABBREV =
			CASE @MONTH_NUM
			WHEN 1  THEN 'JAN'
			WHEN 2  THEN 'FEB'
			WHEN 3  THEN 'MAR'
			WHEN 4  THEN 'APR'
			WHEN 5  THEN 'MAY'
			WHEN 6  THEN 'JUN'
			WHEN 7  THEN 'JUL'
			WHEN 8  THEN 'AUG'
			WHEN 9  THEN 'SEP'
			WHEN 10 THEN 'OCT'
			WHEN 11 THEN 'NOV'
			WHEN 12 THEN 'DEC'
			END

		DECLARE @ThisYr_FROM_SDate datetime
		DECLARE @ThisYr_TO_SDate datetime
		DECLARE @LastYr_FROM_SDate datetime
		DECLARE @LastYr_TO_SDate datetime

		DECLARE @FROM_DA int
		SET @FROM_DA = 1
		SET @LastYr_FROM_SDate = dateadd(month,((@Year-1-1900)*12)+ (@MONTH_NUM-1),(@FROM_DA-1))
		SET @LastYr_TO_SDate = dateadd(month, 1, @LastYr_FROM_SDate)

		SET @ThisYr_FROM_SDate = dateadd(month,((@Year-1900)*12)+ (@MONTH_NUM-1),(@FROM_DA-1))
		SET @ThisYr_TO_SDate = dateadd(month, 1, @ThisYr_FROM_SDate)

        exec sp_Score @UserName, @LastYr_FROM_SDate, @LastYr_TO_SDate, 1, 1, 'E', 'E'

		INSERT INTO #TMP_Part
			SELECT ISNULL(RGS.RegionGrp_ID, -1), ISNULL(RGH.RegionGrp_Desc, 'Unknown'), @Year-1, @MONTH_NUM, @MONTH_ABBREV,  ISNULL(SUM(WS.Dollars_Entered), 0)/1000
            FROM Working_Score WS LEFT OUTER JOIN
            C_RegionGroupSet RGS ON RGS.TERRITORY_ID = WS.TERRITORY_ID LEFT OUTER JOIN
            C_RegionGroupHead RGH ON RGH.RegionGrp_ID = RGS.RegionGrp_ID
            WHERE WS.username=@UserName
			GROUP BY RGS.RegionGrp_ID, RGH.RegionGrp_Desc

        exec sp_Score @UserName, @ThisYr_FROM_SDate, @ThisYr_TO_SDate, 1, 1, 'E', 'E'

		INSERT INTO #TMP_Part
			SELECT ISNULL(RGS.RegionGrp_ID, -1), ISNULL(RGH.RegionGrp_Desc, 'Unknown'), @Year, @MONTH_NUM, @MONTH_ABBREV,  ISNULL(SUM(WS.Dollars_Entered), 0)/1000
            FROM Working_Score WS LEFT OUTER JOIN
            C_RegionGroupSet RGS ON RGS.TERRITORY_ID = WS.TERRITORY_ID LEFT OUTER JOIN
            C_RegionGroupHead RGH ON RGH.RegionGrp_ID = RGS.RegionGrp_ID
            WHERE WS.username=@UserName
			GROUP BY RGS.RegionGrp_ID, RGH.RegionGrp_Desc

	 	SET @MONTH_NUM = @MONTH_NUM + 1
	END

	INSERT INTO #TMP
		SELECT P1.RegionGrp_ID, P1.RegionGrp_Desc, @Year, @Year-1, P1.MonthNum, P1.MonthDesc,
			P1.AmtForMonth, 0, P2.AmtForMonth, 0
		FROM #TMP_Part P1 LEFT OUTER JOIN
		#TMP_Part P2 ON (P2.Year = @Year-1) AND (P2.MonthNum = P1.MonthNum) AND (P2.RegionGrp_ID = P1.RegionGrp_ID)
		WHERE P1.Year = @Year



-- There is probably a better way to do this but fill in Accumulation (YTD) fields
-- by stepping through temp table with a Cursor

	DECLARE @RegionGrp_ID int

	DECLARE c1 CURSOR FOR SELECT RegionGrp_ID FROM C_RegionGroupHead
	OPEN c1

	FETCH NEXT FROM c1 INTO @RegionGrp_ID
	WHILE @@FETCH_STATUS = 0
	BEGIN
		SET @MONTH_NUM = 1	
		WHILE (@MONTH_NUM <= 12)
		BEGIN
			DECLARE @Cum_This int
			DECLARE @Cum_Last int

			SELECT @Cum_This = ISNULL(SUM(AmtForMonth), 0)
			FROM #TMP_Part
			WHERE RegionGrp_ID = @RegionGrp_ID
			AND (MonthNum <= @MONTH_NUM)
			AND Year = @Year	

			SELECT @Cum_Last = ISNULL(SUM(AmtForMonth), 0)
			FROM #TMP_Part
			WHERE RegionGrp_ID = @RegionGrp_ID
			AND (MonthNum <= @MONTH_NUM)
			AND Year = @Year-1	

			UPDATE #TMP
			SET ThisYear_AmtCum = @Cum_This, LastYear_AmtCum = @Cum_Last
			WHERE RegionGrp_ID = @RegionGrp_ID
			AND (MonthNum = @MONTH_NUM)

	 		SET @MONTH_NUM = @MONTH_NUM + 1
		END

		FETCH NEXT FROM c1 INTO @RegionGrp_ID
	END

	CLOSE c1
	DEALLOCATE c1


	SELECT @CompanyName AS CompanyName,
	RegionGrp_ID, RegionGrp_Desc, This_Year, Last_Year, MonthNum, MonthDesc, 
	ThisYear_AmtForMonth AS ThisYear_AmtForMonth_Thousands,
	ThisYear_AmtCum AS ThisYear_AmtCum_Thousands,
    LastYear_AmtForMonth AS LastYear_AmtForMonth_Thousands,
	LastYear_AmtCum AS LastYear_AmtCum_Thousands
    FROM #TMP
    ORDER BY RegionGrp_Desc, MonthNum   

END





























































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptScoreAVolByProd]') and xtype = 'P ')  
 drop Procedure sp_RptScoreAVolByProd
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

-- =============================================
-- Create date: Aug 7/2007
-- Description:	Factor Score Report: Annual Volume By Product
-- =============================================
-- sp_RptScoreAVolByProd "Factor Forms Edmonton", "FRED", 2008
CREATE PROCEDURE [dbo].[sp_RptScoreAVolByProd] 
	@CompanyName varchar(200) = 'Factor Forms',
    @UserName varchar(50) = 'DEFAULT',
	@Year int = 2004
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	CREATE TABLE #AVBP_Part
	(
     DocketType varchar(1),
     Year int,
     MonthNum int,   
     MonthDesc varchar(3),
     AmtForMonth int,  
	)

	CREATE TABLE #AVBP
	(
     DocketType varchar(1),
     This_Year int,
     Last_Year int,
     MonthNum int,   
     MonthDesc varchar(3),
     ThisYear_AmtForMonth int,  
     ThisYear_AmtCum int,
     LastYear_AmtForMonth int,
     LastYear_AmtCum int
	)

	DECLARE @MONTH_NUM int
	SET @MONTH_NUM = 1
	WHILE (@MONTH_NUM <= 12)
	BEGIN
		DECLARE @MONTH_ABBREV varchar(3)
		SET @MONTH_ABBREV =
			CASE @MONTH_NUM
			WHEN 1  THEN 'JAN'
			WHEN 2  THEN 'FEB'
			WHEN 3  THEN 'MAR'
			WHEN 4  THEN 'APR'
			WHEN 5  THEN 'MAY'
			WHEN 6  THEN 'JUN'
			WHEN 7  THEN 'JUL'
			WHEN 8  THEN 'AUG'
			WHEN 9  THEN 'SEP'
			WHEN 10 THEN 'OCT'
			WHEN 11 THEN 'NOV'
			WHEN 12 THEN 'DEC'
			END

		DECLARE @ThisYr_FROM_SDate datetime
		DECLARE @ThisYr_TO_SDate datetime
		DECLARE @LastYr_FROM_SDate datetime
		DECLARE @LastYr_TO_SDate datetime

		DECLARE @FROM_DA int
		SET @FROM_DA = 1
		SET @LastYr_FROM_SDate = dateadd(month,((@Year-1-1900)*12)+ (@MONTH_NUM-1),(@FROM_DA-1))
		SET @LastYr_TO_SDate = dateadd(month, 1, @LastYr_FROM_SDate)

		SET @ThisYr_FROM_SDate = dateadd(month,((@Year-1900)*12)+ (@MONTH_NUM-1),(@FROM_DA-1))
		SET @ThisYr_TO_SDate = dateadd(month, 1, @ThisYr_FROM_SDate)

        exec sp_Score @UserName, @ThisYr_FROM_SDate, @ThisYr_TO_SDate, 1, 1, 'E', 'E'

		INSERT INTO #AVBP_Part
			SELECT DT.DktType, @Year, @MONTH_NUM, @MONTH_ABBREV,  ISNULL(SUM(WS.Dollars_Entered), 0)
 			FROM C_DocketType DT LEFT OUTER JOIN
            Working_Score WS ON WS.oh_type = DT.DktType  AND WS.username=@UserName
			GROUP BY DT.DktType

        exec sp_Score @UserName, @LastYr_FROM_SDate, @LastYr_TO_SDate, 1, 1, 'E', 'E'

		INSERT INTO #AVBP_Part
			SELECT DT.DktType, @Year-1, @MONTH_NUM, @MONTH_ABBREV,  ISNULL(SUM(WS.Dollars_Entered), 0)
 			FROM C_DocketType DT LEFT OUTER JOIN
            Working_Score WS ON WS.oh_type = DT.DktType AND WS.username=@UserName
			GROUP BY DT.DktType

	 	SET @MONTH_NUM = @MONTH_NUM + 1
	END

	INSERT INTO #AVBP
		SELECT P1.DocketType, @Year, @Year-1, P1.MonthNum, P1.MonthDesc,
			P1.AmtForMonth, 0, P2.AmtForMonth, 0
		FROM #AVBP_Part P1 LEFT OUTER JOIN
		#AVBP_Part P2 ON (P2.Year = @Year-1) AND (P2.MonthNum = P1.MonthNum) AND (P2.DocketType = P1.DocketType)
		WHERE P1.Year = @Year
		ORDER BY P1.DocketType, P1.MonthNum


-- There is probably a better way to do this but fill in Accumulation (YTD) fields
-- by stepping through temp table with a Cursor

	DECLARE @DktType varchar(1)

	DECLARE c1 CURSOR FOR SELECT DktType FROM C_DocketType
	OPEN c1

	FETCH NEXT FROM c1 INTO @DktType
	WHILE @@FETCH_STATUS = 0
	BEGIN
		SET @MONTH_NUM = 1	
		WHILE (@MONTH_NUM <= 12)
		BEGIN
			DECLARE @Cum_This int
			DECLARE @Cum_Last int

			SELECT @Cum_This = ISNULL(SUM(AmtForMonth), 0)
			FROM #AVBP_Part
			WHERE DocketType=@DktType
			AND (MonthNum <= @MONTH_NUM)
			AND Year = @Year	

			SELECT @Cum_Last = ISNULL(SUM(AmtForMonth), 0)
			FROM #AVBP_Part
			WHERE DocketType=@DktType
			AND (MonthNum <= @MONTH_NUM)
			AND Year = @Year-1	


			UPDATE #AVBP
			SET ThisYear_AmtCum = @Cum_This, LastYear_AmtCum = @Cum_Last
			WHERE DocketType=@DktType
			AND (MonthNum = @MONTH_NUM)

	 		SET @MONTH_NUM = @MONTH_NUM + 1
		END

		FETCH NEXT FROM c1 INTO @DktType
	END

	CLOSE c1
	DEALLOCATE c1


	SELECT @CompanyName AS CompanyName, * FROM #AVBP
END







































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptScoreMonthEnd]') and xtype = 'P ')  
 drop Procedure sp_RptScoreMonthEnd
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

-- =============================================
-- Create date: Aug 8/2007
-- Description:	Factor Score Report: Month End Score
-- =============================================
-- sp_RptScoreMonthEnd "Factor Forms Edmonton", "FRED", 2008, 6
CREATE PROCEDURE [dbo].[sp_RptScoreMonthEnd] 
	@CompanyName varchar(200) = 'Factor Forms',
    @UserName varchar(50) = 'DEFAULT',
	@Year int = 2004,
    @Month int = 1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @MONTH_DESC varchar(20)
	SET @MONTH_DESC =
		CASE @Month
		WHEN 1  THEN 'January'
		WHEN 2  THEN 'February'
		WHEN 3  THEN 'March'
		WHEN 4  THEN 'April'
		WHEN 5  THEN 'May'
		WHEN 6  THEN 'June'
		WHEN 7  THEN 'July'
		WHEN 8  THEN 'August'
		WHEN 9  THEN 'September'
		WHEN 10 THEN 'October'
		WHEN 11 THEN 'November'
		WHEN 12 THEN 'December'
        ELSE 'Invalid Month'
		END


	CREATE TABLE #TMP
	(
     DocketType varchar(1),
     RegionGrp_ID int,
     RegionGrp_Desc varchar(25),
     AmtForMonth money,  
	)

	DECLARE @FROM_SDate datetime
	DECLARE @TO_SDate datetime

	DECLARE @FROM_DA int
	SET @FROM_DA = 1

	SET @FROM_SDate = dateadd(month,((@Year-1900)*12)+ (@Month-1),(@FROM_DA-1))
	SET @TO_SDate   = dateadd(month, 1, @FROM_SDate)

    exec sp_Score @UserName, @FROM_SDate, @TO_SDate, 1, 1, 'E', 'E'
--SELECT * FROM Working_Score
	INSERT INTO #TMP
			SELECT DT.DktType, ISNULL(RGS.RegionGrp_ID, -1), ISNULL(RGH.RegionGrp_Desc, 'Unknown'), ISNULL(SUM(WS.Dollars_Entered), 0)
 			FROM C_DocketType DT
            LEFT JOIN Working_Score WS ON WS.oh_type = DT.DktTYpe
            LEFT JOIN C_RegionGroupSet RGS ON RGS.TERRITORY_ID = WS.TERRITORY_ID
            LEFT JOIN C_RegionGroupHead RGH ON RGH.RegionGrp_ID = RGS.RegionGrp_ID 
            WHERE WS.username = @UserName
			GROUP BY DT.DktType, RGS.RegionGrp_ID, RGH.RegionGrp_Desc

	SELECT @CompanyName AS CompanyName, @MONTH_DESC AS MonthDesc, * FROM #TMP
	ORDER BY DocketType, RegionGrp_Desc  
END





































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptScoreVolByProdByBr]') and xtype = 'P ')  
 drop Procedure sp_RptScoreVolByProdByBr
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
-- =============================================
-- Create date: Aug 8/2007
-- Description:	Factor Score Report: Volume By Product By Branch
-- =============================================
-- sp_RptScoreVolByProdByBr "Factor Forms Edmonton", "FRED", 2008
CREATE PROCEDURE [dbo].[sp_RptScoreVolByProdByBr] 
	@CompanyName varchar(200) = 'Factor Forms',
    @UserName varchar(50) = 'DEFAULT',
	@Year int = 2004
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON

	CREATE TABLE #TMP
	(
		DocketType varchar(1),
		MonthNum int,   
		MonthDesc varchar(3),
		RegionGrp_ID int,
		RegionGrp_Desc varchar(25),
		Amt money,
	)

	DECLARE @MONTH_NUM int
	SET @MONTH_NUM = 1
	WHILE (@MONTH_NUM <= 12)
	BEGIN
		DECLARE @MONTH_ABBREV varchar(3)
		SET @MONTH_ABBREV =
			CASE @MONTH_NUM
			WHEN 1  THEN 'JAN'
			WHEN 2  THEN 'FEB'
			WHEN 3  THEN 'MAR'
			WHEN 4  THEN 'APR'
			WHEN 5  THEN 'MAY'
			WHEN 6  THEN 'JUN'
			WHEN 7  THEN 'JUL'
			WHEN 8  THEN 'AUG'
			WHEN 9  THEN 'SEP'
			WHEN 10 THEN 'OCT'
			WHEN 11 THEN 'NOV'
			WHEN 12 THEN 'DEC'
			END

		DECLARE @FROM_SDate datetime
		DECLARE @TO_SDate datetime

		DECLARE @FROM_DA int
		SET @FROM_DA = 1

		SET @FROM_SDate = dateadd(month,((@Year-1900)*12)+ (@MONTH_NUM-1),(@FROM_DA-1))
		SET @TO_SDate   = dateadd(month, 1, @FROM_SDate)

        exec sp_Score @UserName, @FROM_SDate, @TO_SDate, 1, 1, 'E', 'E'

		INSERT INTO #TMP
			SELECT DT.DktType, @MONTH_NUM, @MONTH_ABBREV, ISNULL(RGS.RegionGrp_ID, -1), ISNULL(RGH.RegionGrp_Desc, 'Unknown'), ISNULL(SUM(WS.Dollars_Entered), 0)
 			FROM C_DocketType DT
            LEFT JOIN Working_Score WS ON WS.oh_type = DT.DktType AND WS.username=@UserName
            LEFT JOIN C_RegionGroupSet RGS ON RGS.TERRITORY_ID = WS.TERRITORY_ID
            LEFT JOIN C_RegionGroupHead RGH ON RGH.RegionGrp_ID = RGS.RegionGrp_ID
			GROUP BY RGS.RegionGrp_ID, RGH.RegionGrp_Desc, DT.DktType

	 	SET @MONTH_NUM = @MONTH_NUM + 1
	END

	SELECT @CompanyName AS CompanyName, @Year AS Year,
	DocketType,	MonthNum, MonthDesc, RegionGrp_ID, RegionGrp_Desc,
	CAST(Amt/1000 AS decimal(9,1)) AS AmtThousands
    FROM #TMP
    ORDER BY RegionGrp_Desc, DocketType, MonthNum
END





























































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptScoreVolByProdByBrHC]') and xtype = 'P ')  
 drop Procedure sp_RptScoreVolByProdByBrHC
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
-- =============================================
-- Create date: Aug 8/2007
-- Description:	Factor Score Report: Volume By Product By Branch Hard Coded Version
-- This will require code change to display different Docket Types
-- =============================================
-- sp_RptScoreVolByProdByBrHC "Factor Forms Edmonton", 2004
CREATE PROCEDURE [dbo].[sp_RptScoreVolByProdByBrHC] 
	@CompanyName varchar(200) = 'Factor Forms',
	@Year int = 2004
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON

	CREATE TABLE #TMP
	(
		DocketType varchar(1),
		MonthNum int,   
		MonthDesc varchar(3),
		RegionGrp_ID int,
		RegionGrp_Desc varchar(25),
		Amt money,
	)

	DECLARE @MONTH_NUM int
	SET @MONTH_NUM = 1
	WHILE (@MONTH_NUM <= 12)
	BEGIN
		DECLARE @MONTH_ABBREV varchar(3)
		SET @MONTH_ABBREV =
			CASE @MONTH_NUM
			WHEN 1  THEN 'JAN'
			WHEN 2  THEN 'FEB'
			WHEN 3  THEN 'MAR'
			WHEN 4  THEN 'APR'
			WHEN 5  THEN 'MAY'
			WHEN 6  THEN 'JUN'
			WHEN 7  THEN 'JUL'
			WHEN 8  THEN 'AUG'
			WHEN 9  THEN 'SEP'
			WHEN 10 THEN 'OCT'
			WHEN 11 THEN 'NOV'
			WHEN 12 THEN 'DEC'
			END

		DECLARE @FROM_SDate datetime
		DECLARE @TO_SDate datetime

		DECLARE @FROM_DA int
		SET @FROM_DA = 1

		SET @FROM_SDate = dateadd(month,((@Year-1900)*12)+ (@MONTH_NUM-1),(@FROM_DA-1))
		SET @TO_SDate   = dateadd(month, 1, @FROM_SDate)

		INSERT INTO #TMP
			SELECT DT.DktType, @MONTH_NUM, @MONTH_ABBREV, ISNULL(RGS.RegionGrp_ID, -1), ISNULL(RGH.RegionGrp_Desc, 'Unknown'), ISNULL(SUM(QG.QG_DealerCostLot), 0)
 			FROM C_DocketType DT LEFT OUTER JOIN
			D_Head DH ON (DH.oh_type = DT.DktType) AND  (DH.oh_s_date >= @FROM_SDate) AND (DH.oh_s_date < @TO_SDate)
				AND (DH.oh_status <> 'H') AND (DH.oh_status <> 'N') LEFT OUTER JOIN
            C_RegionGroupSet RGS ON RGS.TERRITORY_ID = DH.oh_region LEFT OUTER JOIN
            C_RegionGroupHead RGH ON RGH.RegionGrp_ID = RGS.RegionGrp_ID LEFT OUTER JOIN
			Q_Group QG ON QG.QG_ID = DH.QG_ID
			GROUP BY RGS.RegionGrp_ID, RGH.RegionGrp_Desc, DT.DktType

	 	SET @MONTH_NUM = @MONTH_NUM + 1
	END

	SELECT @CompanyName AS CompanyName, @Year AS Year,
	TC.MonthNum, TC.MonthDesc, TC.RegionGrp_ID, TC.RegionGrp_Desc,
	ISNULL(CAST(TC.Amt/1000 AS decimal(9,1)),0) AS AmtThousands_C,
	ISNULL(CAST(TA.Amt/1000 AS decimal(9,1)),0) AS AmtThousands_A,
	ISNULL(CAST(TS.Amt/1000 AS decimal(9,1)),0) AS AmtThousands_S,
	ISNULL(CAST(TR.Amt/1000 AS decimal(9,1)),0) AS AmtThousands_R,
	ISNULL(CAST(TE.Amt/1000 AS decimal(9,1)),0) AS AmtThousands_E,
	ISNULL(CAST(TL.Amt/1000 AS decimal(9,1)),0) AS AmtThousands_L,
	ISNULL(CAST(TD.Amt/1000 AS decimal(9,1)),0) AS AmtThousands_D,
	ISNULL(CAST(TM.Amt/1000 AS decimal(9,1)),0) AS AmtThousands_M
    
    FROM #TMP TC  LEFT OUTER JOIN
	#TMP TA ON (TC.MonthNum = TA.MonthNum) AND (TA.DocketType='A') AND (TC.RegionGrp_ID = TA.RegionGrp_ID) LEFT OUTER JOIN
	#TMP TS ON (TC.MonthNum = TS.MonthNum) AND (TS.DocketType='S') AND (TC.RegionGrp_ID = TS.RegionGrp_ID) LEFT OUTER JOIN
	#TMP TR ON (TC.MonthNum = TR.MonthNum) AND (TR.DocketType='R') AND (TC.RegionGrp_ID = TR.RegionGrp_ID) LEFT OUTER JOIN
	#TMP TE ON (TC.MonthNum = TE.MonthNum) AND (TE.DocketType='E') AND (TC.RegionGrp_ID = TE.RegionGrp_ID) LEFT OUTER JOIN
	#TMP TL ON (TC.MonthNum = TL.MonthNum) AND (TL.DocketType='L') AND (TC.RegionGrp_ID = TL.RegionGrp_ID) LEFT OUTER JOIN
	#TMP TD ON (TC.MonthNum = TD.MonthNum) AND (TD.DocketType='D') AND (TC.RegionGrp_ID = TD.RegionGrp_ID) LEFT OUTER JOIN
	#TMP TM ON (TC.MonthNum = TM.MonthNum) AND (TM.DocketType='M') AND (TC.RegionGrp_ID = TM.RegionGrp_ID)

    WHERE TC.DocketType = 'C'
    ORDER BY TC.RegionGrp_Desc, TC.MonthNum
END
























































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptShipLabel]') and xtype = 'P ')  
 drop Procedure sp_RptShipLabel
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go




-- sp_RptShipLabel '', 'TEST', '10/30/2007', 'P'
-- sp_RptShipLabel 'AND ((SD.oh_id=655237))', 'XXXX', '10/30/2007', 'P'
-- sp_RptShipLabel 'AND ((SD.oh_id=-51893) OR (SD.oh_id=-51645))', 'FFWL', '1/12/2009 12:00:00 AM', 'P'

-- NOTE DHL_CUST_ID Parameter not used
CREATE proc [dbo].[sp_RptShipLabel]
    @AND_CLAUSE varchar(2000),
    @DHL_CUSTID varchar(7),
    @ShipDate datetime,
    @Print_or_Update varchar(1)
as
SET NOCOUNT ON;

declare
  @sql_str varchar(8000),  @tr_db varchar(50)

select @tr_db=isnull(tr_db,'') from database_setup

create table #tmp_recs
(
  id int IDENTITY(1,1) not null,
  oh_id int null,
  oh_type varchar(1) null,
  oh_num int null,
  oh_dealer_po varchar(12) null,
  ShipToPO varchar(20) null,
  To_NameOrBlank varchar(50) null,
  To_DealerLogo image null,
  To_Name varchar(40) null,
  To_Addr1 varchar(40) null,
  To_Addr2 varchar(40) null,
  To_Addr3 varchar(40) null,
  To_City  varchar(25) null,
  To_Prov  varchar(2) null,
  To_Zip varchar(10) null,
  To_Attn varchar(40) null,
  DHL_IATA varchar(3) null,
  DHL_ZoneDesc varchar(30) null,
  Fr_NameOrBlank varchar(50) null,
  Fr_DealerLogo image null,
  Fr_Name varchar(40) null,
  Fr_Addr1 varchar(40) null,
  Fr_Addr2 varchar(40) null,
  Fr_Addr3 varchar(40) null,
  Fr_City  varchar(25) null,
  Fr_Prov  varchar(2) null,
  Fr_Zip varchar(10) null,
  DHL_Delivery varchar(3) null,
  Prepaid_Collect varchar(1) null,
  SizeDesc varchar(25) null,
  sd_DHLParcelNum int null,
  Parts int null,
  FormName varchar(150) null,
  OrderQty int null,
  sd_num_from varchar(11),
  sd_num_from_INT int null,
  sd_prefix varchar(11),
  sd_suffix varchar(11),
  Numbering varchar(40) null,
  Seq int null,
  NumberOfBoxes int null,
  QtyThisBox int null,
  Weight int null,
  COU_IntegrationType varchar(1) null,
  COURIER_Logo image null,
  BoxLabelOnly varchar(1) null,
  TotalBoxes int null,
  Waybill_Num varchar(30) null,
  SHFirstParcelNumber int null,
  SHWaybillTo_Zip varchar(20) null,
  GDHL_CUSTID varchar(64) null
 )

create table #tmp_labels
(
  oh_id int null,
  oh_type varchar(1) null,
  oh_num  int null,
  Seq int null,
  BoxNumber int null,
  BarcodeData varchar(23),
  BarcodeText varchar(23),
  BoxLabel_or_ShipLabel varchar(1) null,
  Numbering varchar(40) null,
  SHFirstParcelOutputString varchar(23) null
)


-- Get All Rqd Data into a Temp Table to deal with the where clause parameter

SET @sql_str =
'
SELECT 
 SD.oh_id, DH.oh_type, DH.oh_num, DH.oh_dealer_po,
 CASE WHEN (DH.oh_fob=''D'') THEN DH.oh_dealer_po ELSE DH.oh_cust_po END AS ShipToPO,
 To_NameOrBlank  = CASE WHEN ((LAB.dlr_lbl_logo_bmp is null) OR (VC.NAME <> SH.To_Name)) THEN SH.To_Name ELSE '''' END,
 To_DealerLogo   = CASE WHEN ((LAB.dlr_lbl_logo_bmp is null) OR (VC.NAME <> SH.To_Name)) THEN null ELSE LAB.dlr_lbl_logo_bmp END,
 SH.To_Name, SH.To_Addr1, SH.To_Addr2, SH.To_Addr3, SH.To_City,
 SH.To_Prov, SH.To_Zip, SH.To_Attn, SH.DHL_IATA, SH.DHL_ZoneDesc,
 From_NameOrBlank = CASE WHEN ((LAB.dlr_lbl_logo_bmp is null) OR (VC.NAME <> SH.Fr_Name)) THEN SH.Fr_Name ELSE '''' END,
 From_DealerLogo  = CASE WHEN ((LAB.dlr_lbl_logo_bmp is null) OR (VC.NAME <> SH.Fr_Name)) THEN null ELSE LAB.dlr_lbl_logo_bmp END,
 SH.Fr_Name, SH.Fr_Addr1, SH.Fr_Addr2, SH.Fr_Addr3, SH.Fr_City,
 SH.Fr_Prov, SH.Fr_Zip,
 CASE WHEN SH.DHL_DeliverySpeed=''E'' THEN ''EXP''
      WHEN SH.DHL_DeliverySpeed=''A'' THEN ''EXP''
      ELSE ''GRD''
 END AS DHL_Delivery, 
 SH.Prepaid_Collect,
 CASE WHEN ISNULL(DH.oh_ActualDim1,'''') = '''' THEN QH.Q_Width + '' x '' + QH.Q_Length
      ELSE ISNULL(DH.oh_ActualDim1,'''') + '' x '' + ISNULL(DH.oh_ActualDim2,'''')
 END as Size,
 SD.sd_DHLParcelNum,
 QG.QG_Parts, QG.QG_Desc As FormName,
 CASE WHEN SH.SplitShipYN=''Y'' THEN SH.Partial_Qty ELSE QG.QG_Qty END as OrderQty,
 SD.sd_num_from, SD.sd_num_from_INT, ISNULL(SD.sd_prefix, ''''), ISNULL(SD.sd_suffix, ''''),
 Numbering =
   CASE WHEN isNull(SD.sd_num_from, '''') <> '''' then isNull(SD.sd_prefix, '''') + SD.sd_num_from + isNull(SD.sd_suffix, '''') + '' to ''
   ELSE ''''
   END + isNull(SD.sd_prefix, '''') + isNull(SD.sd_num_to, '''') + isNull(SD.sd_suffix, ''''),
 SD.sd_seq as Seq, SD.sd_boxes as NumberOfBoxes, SD.sd_per_box as QtyPerBox, SD.sd_weight,
 COU.COU_IntegrationType, COU.COU_logo_bmp, SH.BoxLabelOnly, SH.TotalBoxes, SH.Waybill_Num, SH.FirstParcelNumber,
 SH.WaybillTo_Zip, COU.COU_DHL_CustomerID
FROM SHIP_Detail SD
LEFT OUTER JOIN SHIP_Head SH ON SH.oh_id = SD.oh_id
LEFT OUTER JOIN D_Head DH ON DH.oh_id = SH.oh_id
LEFT OUTER JOIN Q_HEAD QH ON QH.Q_ID = DH.Q_ID 
LEFT OUTER JOIN Q_Group QG ON QG.QG_ID = DH.QG_ID
LEFT OUTER JOIN dlr_lab LAB on LAB.dlr_Customer_ID = QH.Customer_ID
LEFT OUTER JOIN ' + @tr_db + '..CUSTOMERS VC on VC.CUSTOMER_ID = QH.Customer_ID
LEFT OUTER JOIN COU_Courier COU ON COU.COU_ID = SH.COU_ID
WHERE (SD.oh_id > 0) ' + @AND_CLAUSE + '
UNION ALL
SELECT 
 SD.oh_id, ''M'' as oh_type, M.SO_NO as oh_num, '''', M.PONO AS ShipToPO,
 To_NameOrBlank  = CASE WHEN ((LAB.dlr_lbl_logo_bmp is null) OR (VC.NAME <> SH.To_Name)) THEN SH.To_Name ELSE '''' END,
 To_DealerLogo   = CASE WHEN ((LAB.dlr_lbl_logo_bmp is null) OR (VC.NAME <> SH.To_Name)) THEN null ELSE LAB.dlr_lbl_logo_bmp END,
 SH.To_Name, SH.To_Addr1, SH.To_Addr2, SH.To_Addr3, SH.To_City,
 SH.To_Prov, SH.To_Zip, SH.To_Attn, SH.DHL_IATA, SH.DHL_ZoneDesc,
 From_NameOrBlank = CASE WHEN ((LAB.dlr_lbl_logo_bmp is null) OR (VC.NAME <> SH.Fr_Name)) THEN SH.Fr_Name ELSE '''' END,
 From_DealerLogo  = CASE WHEN ((LAB.dlr_lbl_logo_bmp is null) OR (VC.NAME <> SH.Fr_Name)) THEN null ELSE LAB.dlr_lbl_logo_bmp END,
 SH.Fr_Name, SH.Fr_Addr1, SH.Fr_Addr2, SH.Fr_Addr3, SH.Fr_City,
 SH.Fr_Prov, SH.Fr_Zip,
 CASE WHEN SH.DHL_DeliverySpeed=''E'' THEN ''EXP''
      WHEN SH.DHL_DeliverySpeed=''A'' THEN ''AIR''
      ELSE ''GRD''
 END AS DHL_Delivery,  
 SH.Prepaid_Collect,   
 '''' as Size, SD.sd_DHLParcelNum,
 1, MD.DESCRIPTION As FormName,
 CASE WHEN SH.SplitShipYN=''Y'' THEN SH.Partial_Qty ELSE SH.oh_QG_Qty END as OrderQty,
 SD.sd_num_from, SD.sd_num_from_INT, ISNULL(SD.sd_prefix, ''''), ISNULL(SD.sd_suffix, ''''),
 Numbering =
   CASE WHEN isNull(SD.sd_num_from, '''') <> '''' then isNull(SD.sd_prefix, '''') + SD.sd_num_from + isNull(SD.sd_suffix, '''') + '' to ''
   ELSE ''''
   END + isNull(SD.sd_prefix, '''') + isNull(SD.sd_num_to, '''') + isNull(SD.sd_suffix, ''''),
 SD.sd_seq as Seq, SD.sd_boxes as NumberOfBoxes, SD.sd_per_box as QtyPerBox, SD.sd_weight,
 COU.COU_IntegrationType, COU.COU_logo_bmp, SH.BoxLabelOnly, SH.TotalBoxes, SH.Waybill_Num, SH.FirstParcelNumber,
 SH.WaybillTo_Zip, COU.COU_DHL_CustomerID
FROM SHIP_Detail SD
LEFT OUTER JOIN SHIP_Head SH ON SH.oh_id = SD.oh_id
LEFT OUTER JOIN ' + @tr_db + '..SO_MASTER_DET_LINE MDL ON MDL.SO_LINE_DUE_ID = - SD.oh_ID
LEFT OUTER JOIN ' + @tr_db + '..SO_MASTER_DETAIL MD ON MD.SO_LINE_ID = MDL.SO_LINE_ID
LEFT OUTER JOIN ' + @tr_db + '..SO_MASTER_HDR M ON M.SO_ID=MD.SO_ID
LEFT OUTER JOIN dlr_lab LAB on LAB.dlr_Customer_ID = M.Customer_ID
LEFT OUTER JOIN ' + @tr_db + '..CUSTOMERS VC on VC.CUSTOMER_ID = M.Customer_ID
LEFT OUTER JOIN COU_Courier COU ON COU.COU_ID = SH.COU_ID
WHERE (SD.oh_id < 0) ' + @AND_CLAUSE + '
ORDER BY oh_type, oh_num, Seq'


INSERT #tmp_recs
exec (@sql_str)

--select * from #tmp_recs

DECLARE
  @RecNum int,
  @RecCount int,
  @InsNum int,
  @oh_type varchar(1),
  @oh_num  int,
  @Seq int,
  @NumberOfBoxes int,
  @COU_IntegrationType varchar(1),
  @To_Zip varchar(10),
  @BarcodeData varchar(23),
  @BarcodeText varchar(23),
  @SHFirstParcelNumberString varchar(23),
  @SHFirstParcelOutputString varchar(23),
  @ParcelNumAsStr varchar(7),
  @First_DHLParcelNum int,
  @This_DHLParcelNum int,
  @SHFirstParcelNumber int,
  @BoxLabelOnly varchar(1),
  @BSX varchar(1),
  @sd_num_from_INT int,
  @sd_num_from varchar(11),
  @NumberingIfNotInt varchar(40),
  @LabelNumbering varchar(40),
  @QtyThisBox int,
  @LAST_OH_ID int,
  @oh_id int,
  @Box_Count int,
  @AAPart varchar(10),
  @PrepaidOrCollect varchar(10),
  @DHLDelivery varchar(10),
  @SHWaybillTo_Zip varchar(20),
  @prefix varchar(11),
  @suffix varchar(11),
  @DHL_Box_Weight int,
  @GDHL_CUSTID varchar(64)

SELECT @DHL_Box_Weight = ISNULL(Value, 16) FROM CU_CtlString WHERE CSTR_TYPE = 'SHIP_DHL_BOX_WEIGHT'

SET @LAST_OH_ID = -1
SET @Box_Count = 0

SELECT @RecCount = max(id) FROM #tmp_recs
SET @RecNum = 1

SET @SHFirstParcelNumberString = ''

--  process each rec
WHILE @RecNum <= @RecCount
BEGIN
  SELECT @oh_id=oh_id, @oh_type=oh_type, @oh_num=oh_num, @Seq=Seq, @NumberOfBoxes=NumberOfBoxes, @BoxLabelOnly=BoxLabelOnly,
         @COU_IntegrationType=COU_IntegrationType, @To_Zip=ISNULL(To_Zip,''), @First_DHLParcelNum=ISNULL(sd_DHLParcelNum, 0),
         @SHFirstParcelNumber=ISNULL(SHFirstParcelNumber, 0), @PrepaidOrCollect=ISNULL(Prepaid_Collect, 'P'),
         @DHLDelivery = DHL_Delivery, @SHWaybillTo_Zip = SHWaybillTo_Zip,
         @sd_num_from_INT=sd_num_from_INT, @sd_num_from=sd_num_from,  @NumberingIfNotInt=Numbering, @QtyThisBox=QtyThisBox,
         @prefix=sd_prefix, @suffix=sd_suffix, @GDHL_CUSTID=GDHL_CUSTID
  FROM #tmp_recs
  WHERE (id = @RecNum)

  if (@LAST_OH_ID <> @oh_id)
  BEGIN
    set @Box_Count = 0
    set @LAST_OH_ID = @oh_id
  END 

  set @SHWaybillTo_Zip = REPLACE(@SHWaybillTo_Zip,' ','')
  set @SHWaybillTo_Zip = REPLACE(@SHWaybillTo_Zip,'_','')

  set @To_Zip = REPLACE(@To_Zip,' ','')
  set @To_Zip = REPLACE(@To_Zip,'_','')
  IF  LEN(@To_Zip) > 6 
    set @To_Zip = SUBSTRING(@To_Zip, 1, 6)
  WHILE  LEN(@To_Zip) < 6
    set @To_Zip = @To_Zip + '_'


  SELECT @InsNum = 1

-- for each rec, create a label record for each box (Use Join to Create Multiple Duplicate Labels)
  WHILE @InsNum <= @NumberOfBoxes
  BEGIN
     if  @sd_num_from_INT = -1
	   set @LabelNumbering = @sd_num_from
	 else
--       set @LabelNumbering = @NumberingIfNotInt
       set @LabelNumbering = @prefix + CAST(@sd_num_from_INT + ((@InsNum -1) * @QtyThisBox) as varchar(40)) + @suffix + ' to ' +
                             @prefix + CAST((@sd_num_from_INT-1) + (@InsNum * @QtyThisBox) as varchar(40)) + @suffix

    IF @COU_IntegrationType = 'D'
    BEGIN
	  set @This_DHLParcelNum = @First_DHLParcelNum + @InsNum -1
	  set @ParcelNumAsStr = CAST(@This_DHLParcelNum AS varchar(7))
	  WHILE LEN(@ParcelNumAsStr) < 6
		set @ParcelNumAsStr = '0' + @ParcelNumAsStr

      IF (@PrepaidOrCollect = 'P')
      BEGIN
        IF  (@DHLDelivery = 'EXP')
          SET @AAPart = 'AB'
        ELSE
          SET @AAPart = 'AA'
      END
      ELSE
      BEGIN
         IF  (@DHLDelivery = 'EXP')
           SET @AAPart = 'AF'
        ELSE
           SET @AAPart = 'AE'
      END

      set @BarcodeData = 'LQ' + @SHWaybillTo_Zip + @AAPart + @GDHL_CUSTID + @ParcelNumAsStr
      set @BarcodeText = @GDHL_CUSTID + @ParcelNumAsStr

      if  (@Seq = 1)
      BEGIN

        set @SHFirstParcelNumberString = CAST(@SHFirstParcelNumber AS varchar(7))
        WHILE LEN(@SHFirstParcelNumberString) < 6
	      set @SHFirstParcelNumberString = '0' + @SHFirstParcelNumberString


        set @SHFirstParcelOutputString = @GDHL_CUSTID + @SHFirstParcelNumberString
      END
    END
    ELSE
    BEGIN
      set @BarcodeData = ''
      set @BarcodeText = ''
      set @SHFirstParcelOutputString = ''
    END

-- (S)hipping Lbl, (B)ox Lbl, or X -Both

    set @BSX = 'S'

    IF (@Print_or_Update = 'P')
      set @BSX = 'B'

--      INSERT INTO #tmp_labels(oh_type, oh_num, Seq, BoxNumber, BarcodeData, BarcodeText, BoxLabel_or_ShipLabel)
--      VALUES              (@oh_type, @oh_num, @Seq, @InsNum, @BarcodeData, @BarcodeText, 'B')

    IF @COU_IntegrationType = 'D'
    BEGIN
      IF (@Print_or_Update = 'U') OR (@BoxLabelOnly = 'N')
        IF  (@BSX = 'B')
          set @BSX = 'X'
    END

    set @Box_Count = @Box_Count + 1


    INSERT INTO #tmp_labels(oh_id, oh_type, oh_num, Seq, BoxNumber, BarcodeData, BarcodeText, BoxLabel_or_ShipLabel, Numbering, SHFirstParcelOutputString)
    VALUES              (@oh_id, @oh_type, @oh_num, @Seq, @Box_Count, @BarcodeData, @BarcodeText, @BSX, @LabelNumbering, @SHFirstParcelOutputString)


    SET @InsNum = @InsNum + 1 
  END

  SET @RecNum = @RecNum + 1 
END


SELECT R.oh_id, L.oh_type, L.oh_num, L.BoxLabel_or_ShipLabel, R.oh_dealer_po, R.ShipToPO,
  R.To_NameOrBlank, R.To_DealerLogo, R.COURIER_Logo, @ShipDate as ShipDate,
  R.To_Name, R.To_Addr1, R.To_Addr2, R.To_Addr3, R.To_City, R.To_Prov, R.To_Zip, R.To_Attn,
  R.DHL_IATA AS DHL_Routing_IATA, R.DHL_ZoneDesc AS DHL_Routing_ZoneDesc,
  CASE WHEN @COU_IntegrationType = 'D' THEN
    DHL_Delivery
  ELSE
    ''
  END AS DHL_Delivery,  SH.Prepaid_Collect,
  R.Fr_NameOrBlank,  R.Fr_DealerLogo,
  R.Fr_Name, R.Fr_Addr1, R.Fr_Addr2, R.Fr_Addr3, R.Fr_City, R.Fr_Prov, R.Fr_Zip,
  R.QtyThisBox, L.BoxNumber, R.TotalBoxes AS NumberOfBoxes, R.OrderQty, R.Weight,
  R.SizeDesc, R.Parts, R.FormName as FormDescription,
 L.Numbering AS Numbering,
  L.BarcodeData, L.BarcodeText, R.Waybill_Num,
 SH.WaybillFr_Name, SH.WaybillFr_Addr1, SH.WaybillFr_Addr2, SH.WaybillFr_Addr3, SH.WaybillFr_City, SH.WaybillFr_Prov,
 SH.WaybillFr_Zip, SH.WaybillTo_Name, SH.WaybillTo_Addr1, SH.WaybillTo_Addr2, SH.WaybillTo_Addr3, SH.WaybillTo_City,
 SH.WaybillTo_Prov, SH.WaybillTo_Zip, SH.WaybillTo_Attn, SH.WaybillTo_Country, SH.WaybillTo_Phone, L.SHFirstParcelOutputString,
 SH.Fr_Phone, SH.To_Phone, @DHL_Box_Weight AS DHL_Box_Weight
FROM #tmp_labels L
LEFT OUTER JOIN #tmp_recs R ON ((R.oh_id = L.oh_id) AND (R.Seq = L.Seq))
LEFT OUTER JOIN SHIP_Head SH ON SH.oh_id = R.oh_id
ORDER BY L.oh_type, L.oh_num, L.Seq, L.BoxNumber, L.BoxLabel_or_ShipLabel





























Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptShipManifest]') and xtype = 'P ')  
 drop Procedure sp_RptShipManifest
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go


-- This is the Standard Manifest (sp_RptShipManifestDHL is the DHL Driver Manifest)
-- sp_RptShipManifest 'ABC Company', 'WHERE 1=1'
-- sp_RptShipManifest 'Factor', 'WHERE MAN_PackSlipDate=''11/7/2007 12:00:00 AM'' AND M.COU_ID=3'
-- **Note that the above double set of quotes is necessary to run stored proc from Query Analyser
-- **but Crystal will not work with doubled up single quotes,  Needs parameters as below
-- sp_RptShipManifest 'Factor', 'WHERE MAN_PackSlipDate='11/7/2007 12:00:00 AM' AND M.COU_ID=3'

CREATE PROCEDURE [dbo].[sp_RptShipManifest]
  @CompanyName varchar(200),
  @where_clause varchar(2000)
 AS
 
declare @tr_db varchar(50)
select @tr_db=isnull(tr_db,'') from database_setup


declare @sql varchar(5000)

set @sql=
'
SELECT ''' + @CompanyName + ''' AS ReportCompanyName, COU.COU_ShortDesc as CourierShortName,
COU.COU_Desc as CourierName, M.MAN_PackSlipDate,
M.MAN_Pieces, M.Ord_Type, M.Ord_Num, M.MAN_Waybill_Num, VC.CUSTOMER_CODE as DealerNum, VC.NAME as DealerName,
M.ClientName as Customer, M.MAN_ShipToName, M.MAN_ShipToAddr1, M.MAN_ShipToAddr2, M.MAN_ShipToAddr3,
M.MAN_ShipToCity, M.MAN_ShipToProv, M.MAN_ShipToZip,
M.WaybillTo_Name, M.WaybillTo_Addr1, M.WaybillTo_Addr2, M.WaybillTo_Addr3, M.WaybillTo_City,
M.WaybillTo_Prov, M.WaybillTo_Zip, WaybillTo_Attn, WaybillTo_Country,
M.MAN_Weight, M.MAN_Waybill_To as ShipToAbbreviation
FROM SHIP_Manifest M
LEFT OUTER JOIN ' + @tr_db + '..CUSTOMERS VC ON M.Customer_ID = VC.CUSTOMER_ID
LEFT OUTER JOIN COU_Courier COU ON COU.COU_ID = M.COU_ID
' +
@where_clause + '
ORDER BY M.MAN_PackSlipDate, COU.COU_ShortDesc, M.MAN_Waybill_Num, M.Ord_Type, M.Ord_Num
'

exec(@sql)







































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_RptShipManifestDHL]') and xtype = 'P ')  
 drop Procedure sp_RptShipManifestDHL
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

-- sp_RptShipManifestDHL 'ABC Company', 513
-- sp_RptShipManifestDHL 'Factor Forms', 513
-- **Note that the above double set of quotes is necessary to run stored proc from Query Analyser
-- **but Crystal will not work with doubled up single quotes,  Needs parameters as below

CREATE PROCEDURE [dbo].[sp_RptShipManifestDHL]
  @CompanyName varchar(200),
  @MH_Batch int
 AS

declare @sql varchar(5000),
        @DHL_Box_Weight int

SELECT @DHL_Box_Weight = ISNULL(Value, 16) FROM CU_CtlString WHERE CSTR_TYPE = 'SHIP_DHL_BOX_WEIGHT'

set @sql=
'
SELECT ''' + @CompanyName + ''' AS ReportCompanyName, H.MH_Batch, H.MH_DateShipped,
H.MH_TimeShippedHHMM, H.MH_LegacyAcct, H.MH_TotPieces, H.MH_TotWeight,
H.MH_FromName, H.MH_FromAddr1, H.MH_FromAddr2, H.MH_FromCity, H.MH_FromProv,
H.MH_FromCountry, H.MH_FromPostal, H.MH_FromContact, H.MH_FromPhone,
H.MH_FromIATA, H.MH_DHL_Acct, H.MH_SystemName, H.MH_SoftwareVers,
M.MAN_Waybill_Num, M.Ord_Type + CAST(M.Ord_Num AS varchar) AS ShipperReference, M.MAN_DHL_IATA as DEST_IATA,
M.MAN_Pieces, M.MAN_Weight, M.Prepaid_Collect, M.CollectAcctNum, 
M.ClientName as Customer, M.MAN_ShipToName, M.MAN_ShipToAddr1, M.MAN_ShipToAddr2, M.MAN_ShipToAddr3,
M.MAN_ShipToCity, M.MAN_ShipToProv, M.MAN_ShipToZip,
M.WaybillTo_Name, M.WaybillTo_Addr1, M.WaybillTo_Addr2, M.WaybillTo_Addr3, M.WaybillTo_City,
M.WaybillTo_Prov, M.WaybillTo_Zip, WaybillTo_Attn, WaybillTo_Country,
M.MAN_Waybill_To as ShipToAbbreviation,
VC.CUSTOMER_CODE as DealerNum, VC.NAME as DealerName,
M.DHL_DeliverySpeed, M.DHL_ValueAdd1, M.DHL_ValueAdd2, M.DHL_ValueAdd3, M.DHL_ValueAdd4, M.DHL_ValueAdd5,
QG.QG_Desc AS FormName, M.SH_FIRST_PARCEL_OUTPUT_STRING,
' + CAST(@DHL_Box_Weight as varchar) + ' AS DHL_PerBoxWeight
FROM SHIP_Manifest M
LEFT JOIN SHIP_Manifest_Head H ON H.MH_Batch = M.MH_Batch
LEFT JOIN VIEW_CUSTOMERS VC ON M.Customer_ID = VC.CUSTOMER_ID
LEFT JOIN COU_Courier COU ON COU.COU_ID = M.COU_ID
LEFT JOIN D_Head DH ON DH.oh_ID = M.oh_ID
LEFT JOIN Q_Group QG ON QG.QG_ID = DH.QG_ID
WHERE M.MH_Batch=' +  CAST(@MH_Batch as varchar) + '
ORDER BY M.MAN_Waybill_Num, M.Ord_Type, M.Ord_Num
'

exec(@sql)

































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_Score]') and xtype = 'P ')  
 drop Procedure sp_Score
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
-- IEO  (I)nclude, (E)xclude, (O)nly
-- sp_Score 'FRED', '2008-5-1 11:11.123', '2008-5-2 01:13.221', 6, 20, 'E', 'E'
-- sp_Score 'FRED', '2008-6-1 11:11.123', '2008-7-1 01:13.221', 6, 20, 'I', 'I'
-- select * from Working_Score WHERE username='FRED'
-- select * from Working_ScoreDetail WHERE username='FRED'
--
--select D.*, CUST.SLS_ID2, SLS.LASTNAME, SLS.FIRSTNAME from Working_ScoreDetail D
--LEFT JOIN TR_FACTOR..CUSTOMERS CUST ON CUST.CUSTOMER_ID = D.Customer_ID
--LEFT JOIN TR_FACTOR..SALESPERSONS SLS ON SLS.SLS_ID = CUST.SLS_ID2
--LEFT JOIN TR_FACTOR..TERRITORY TER ON TER.TERRITORY_ID = CUST.TERRITORY_ID
--LEFT JOIN TR_FACTOR..WAREHOUSE WHS ON WHS.WAREHOUSE = D.oh_Whs
--WHERE username = 'FRED'
--AND oh_type='A' and oh_num=1003610
--
CREATE PROCEDURE [dbo].[sp_Score]
  @UserName varchar(50),
  @Parm_FromDate Datetime,
  @ToDate Datetime,
  @WorkDayNum integer,
  @WorkDaysThisMonth integer,
  @Internal_IEO varchar(1),
  @ReRun_IEO varchar(1)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

-- Strip off any hhmmss from parameter dates
    SET @Parm_FromDate = dateadd(day, datediff(day, 0, @Parm_FromDate), 0)
    SET @ToDate        = dateadd(day, datediff(day, 0, @ToDate), 0)

--SELECT @Parm_FromDate AS FROM_DATE, @ToDate AS TO_DATE

    DELETE Working_ScoreDetail WHERE username = @UserName
    DELETE Working_Score WHERE username = @UserName

--	create table #tmp_score
--	(
--      Source varchar(2) null,
--	  oh_type varchar(1) null,
--	  oh_num  int null,
--      Customer_ID int null,
--      oh_whs  varchar(3) null,
--      Dollars_Entered money null,
--      Dollars_Invoiced money null,
--      One_If_Entered smallint null,
--      One_If_Invoiced smallint null
--	)

    declare @sql varchar(5000), @tr varchar(50), @STR_WorkDayNum varchar(20), @STR_WorkDaysThisMonth varchar(20),  @STR_DayOfDays varchar(10)
  
    set @STR_WorkDayNum = CAST(@WorkDayNum as varchar(20))
    set @STR_WorkDaysThisMonth = CAST(@WorkDaysThisMonth as varchar(20))
    set @STR_DayOfDays = @STR_WorkDayNum + ' of ' + @STR_WorkDaysThisMonth

    select @tr=isnull(tr_db,'') from database_setup

--    IF EXISTS(SELECT name FROM ..sysobjects WHERE name = N'#tmp_score' AND xtype='U')
--	  DROP TABLE Tmp_Score

----------------------------------------------------
-- 'Source': DE = Entered Dockets from D_Head
-- oh_status (H)old, i(N)complete, read(Y)toprint, (R)eleased, releasefor(B)illing, X-Billed
    SET @sql =
'   INSERT INTO Working_ScoreDetail
    SELECT ''' + @UserName + ''', ''DE'', DH.oh_type, DH.oh_num, DH.oh_ID, QH.Customer_ID, DH.oh_Whs, DH.oh_net_cost, 0, 1, 0
	FROM D_Head DH
    LEFT OUTER JOIN Q_Head QH ON QH.Q_ID = DH.Q_ID
 	WHERE (oh_Nrcv_date >= ''' + CAST(@Parm_FromDate as varchar(50)) + ''' AND oh_Nrcv_date < ''' + CAST(@ToDate as varchar(50)) + ''')
    AND   (not oh_status in (''H'', ''N'', ''Z''))
    AND   (''' + @Internal_IEO + ''' = ''I'' OR (''' + @Internal_IEO + ''' = ''E'' AND (LTRIM(ISNULL(QH.Q_Internal_YN,''N''))<>''Y'')) OR (''' + @Internal_IEO + '''=''O'' AND (LTRIM(ISNULL(QH.Q_Internal_YN,''N''))=''Y'')))
    AND   (''' + @ReRun_IEO + ''' = ''I'' OR (''' + @ReRun_IEO + ''' = ''E'' AND (LTRIM(ISNULL(QH.ReRun_YN,''N'')) <> ''Y'')) OR (''' + @ReRun_IEO + ''' = ''O'' AND (LTRIM(ISNULL(QH.ReRun_YN,''N'')) = ''Y'')))
'
--SELECT @sql
    exec(@sql)


----------------------------------------------------
-- 'Source':  DI = Invoiced Dockets From D_Head + SO_TRN_HDR
    SET @sql =
'   INSERT INTO Working_ScoreDetail
    SELECT ''' + @UserName + ''', ''DI'', DH.oh_type, DH.oh_num, DH.oh_ID, QH.Customer_ID, DH.oh_Whs,
    0 AS Dollars_Entered, ISNULL(STH.Total_Products,0) + ISNULL(STH.TOTAL_OTHER_MISC,0) AS Dollars_Invoiced, 0 AS One_If_Entered, 1 AS One_If_Invoiced
	FROM D_Head DH
    LEFT OUTER JOIN Q_Head QH ON QH.Q_ID = DH.Q_ID
    JOIN ' + @tr + '..SO_TRN_HDR STH ON STH.SO_TRN_ID = DH.R2B_SO_TRN_ID
 	WHERE (STH.INVOICE_DATE >= ''' + CAST(@Parm_FromDate as varchar(50)) + ''' AND STH.INVOICE_DATE < ''' + CAST(@ToDate as varchar(50)) + ''')
    AND   oh_status = ''X''
    AND   (''' + @Internal_IEO + ''' = ''I'' OR (''' + @Internal_IEO + ''' = ''E'' AND (LTRIM(ISNULL(QH.Q_Internal_YN,''N''))<>''Y'')) OR (''' + @Internal_IEO + '''=''O'' AND (LTRIM(ISNULL(QH.Q_Internal_YN,''N''))=''Y'')))
    AND   (''' + @ReRun_IEO + ''' = ''I'' OR (''' + @ReRun_IEO + ''' = ''E'' AND (LTRIM(ISNULL(QH.ReRun_YN,''N'')) <> ''Y'')) OR (''' + @ReRun_IEO + ''' = ''O'' AND (LTRIM(ISNULL(QH.ReRun_YN,''N'')) = ''Y'')))
'
--SELECT @sql
    exec(@sql)


----------------------------------------------------
-- 'Source': MI -M Ord Invoiced From SO_TRN_HDR
    SET @sql =
'   INSERT INTO Working_ScoreDetail
    SELECT ''' + @UserName + ''', ''MI'', ''M'', -1 AS oh_num, -1 as oh_ID, STH.Customer_ID, WHS.WAREHOUSE,
    0 AS Dollars_Entered, ISNULL(STH.Total_Products,0) + ISNULL(STH.TOTAL_OTHER_MISC,0) AS Dollars_Invoiced, 0 AS One_If_Entered, 1 AS One_If_Invoiced
	FROM ' + @tr + '..SO_TRN_HDR STH
    JOIN ' + @tr + '..SO_MASTER_HDR SMH ON SMH.SO_ID = STH.SO_ID
    LEFT JOIN ' + @tr + '..WAREHOUSE WHS ON WHS.WHSE_ID = SMH.WHSE_ID
 	WHERE (STH.INVOICE_DATE >= ''' + CAST(@Parm_FromDate as varchar(50)) + ''' AND STH.INVOICE_DATE < ''' + CAST(@ToDate as varchar(50)) + ''')
    AND (STH.SO_ID <> 0)
    AND   (''' + @Internal_IEO + ''' <> ''O'')
    AND   (''' + @ReRun_IEO + ''' <> ''O'')
'
-- SELECT @sql
    exec(@sql)


--SELECT * FROM Working_ScoreDetail

-- For consistency with all score reports, From Date is inclusive, To Date is not
-- Both From and To Date are stripped and have zero hh:ss:mmm
    DECLARE @ToDateMinusSec datetime
    SET @ToDateMinusSec = dateadd(second, -1, @ToDate)
--SELECT @ToDate AS TODATE, @ToDateMinusMS AS TODATEMINUSMS

    SET @sql =
'INSERT INTO Working_Score
        SELECT ''' + @UserName + ''', D.oh_type, CUST.TERRITORY_ID AS Territory, TER.TERRITORY_DESC, D.oh_Whs AS Whs_Code, WHS.DESCRIPTION AS Whs_Desc,
		CUST.SLS_ID2 AS Salesman_ID, ISNULL(SLS.FIRSTNAME,'''') + '' '' + ISNULL(SLS.LASTNAME,'''') AS SalesmanName,
		SUM(D.One_If_Entered) AS Count_Entered, SUM(D.Dollars_Entered) AS Dollars_Entered,
        SUM(D.One_If_Invoiced) AS Count_Invoiced, SUM(D.Dollars_Invoiced) as Dollars_Invoiced,
        CASE WHEN (' + @STR_WorkDayNum + ' > 0) THEN CAST(SUM(D.One_If_Entered) * ' + @STR_WorkDaysThisMonth + ' AS decimal) / ' + @STR_WorkDayNum + '  ELSE 0 END AS Projected_Count_Entered,
        CASE WHEN (' + @STR_WorkDayNum + ' > 0) THEN SUM(D.Dollars_Entered) * ' + @STR_WorkDaysThisMonth + ' / ' + @STR_WorkDayNum + ' ELSE 0 END AS Projected_Dollars_Entered,
        CASE WHEN (' + @STR_WorkDayNum + ' > 0) THEN CAST(SUM(D.One_If_Invoiced) * ' + @STR_WorkDaysThisMonth + ' AS decimal) / ' + @STR_WorkDayNum + '  ELSE 0 END AS Projected_Count_Invoiced,
        CASE WHEN (' + @STR_WorkDayNum + ' > 0) THEN SUM(D.Dollars_Invoiced) * ' + @STR_WorkDaysThisMonth + ' / ' + @STR_WorkDayNum + ' ELSE 0 END AS Projected_Dollars_Invoiced, ''' +
        CAST(@Parm_FromDate AS varchar(20)) + ''' AS PARM_FromDate, ''' +
        CAST(@ToDateMinusSec AS varchar(20)) + ''' AS PARM_ToDate, ''' +
        @STR_DayOfDays + ''' AS PARM_DayOfDays, ''' + @ReRun_IEO + ''' AS PARM_ReRun_IEO, ''' + @Internal_IEO + ''' PARM_Internal_IEO' + '
	FROM Working_ScoreDetail D
    LEFT OUTER JOIN ' + @tr + '..CUSTOMERS CUST ON CUST.CUSTOMER_ID = D.Customer_ID
    LEFT OUTER JOIN ' + @tr + '..SALESPERSONS SLS ON SLS.SLS_ID = CUST.SLS_ID2
    LEFT OUTER JOIN ' + @tr + '..TERRITORY TER ON TER.TERRITORY_ID = CUST.TERRITORY_ID
    LEFT OUTER JOIN ' + @tr + '..WAREHOUSE WHS ON WHS.WAREHOUSE = D.oh_Whs
    WHERE username = ''' + @UserName + '''
	GROUP BY D.oh_type, CUST.TERRITORY_ID, TER.TERRITORY_DESC, CUST.SLS_ID2, SLS.FIRSTNAME, SLS.LASTNAME, D.oh_Whs, WHS.DESCRIPTION
'
--   SELECT @sql
    exec(@sql)

END




































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_SQquickE]') and xtype = 'P ')  
 drop Procedure sp_SQquickE
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

create proc [dbo].[sp_SQquickE]
@CompanyAutoID int,@username varchar(100),@Customer_ID int
as
--exec [sp_SQquickE] 12, 'hm',3887
declare 
@sq_id int,
@sqlStartStr varchar(8000)

set @sq_id = 6


----------------------------------------------------------------------------------
--================================Declare ALL Parameters here=====================
----------------------------------------------------------------------------------
--Starting declarations for parameters(default parameters in list are username and companyautoid)
set @sqlStartStr ='declare '+
'@username varchar(100),' +
'@CompanyAutoID int,'+
'@Customer_ID '



--------------------------------------------------------------------
--sets for each declared parameter
set @sqlStartStr = @sqlStartStr + ' 
set @username ='''+@username +''' 
set @CompanyAutoID = '+ cast(@CompanyAutoID as varchar(8000))+' ''
set @Customer_ID = '+ cast(@Customer_ID as varchar(8000))+' '


----------------------------------------------------------------------------------
--================================DO NOT EDIT BELOW===============================
----------------------------------------------------------------------------------
delete working_performance_indicator_value where username = @username
set @sqlStartStr = @sqlStartStr + '
'



if exists (select * from tempdb..sysobjects where id = object_id(N'tempdb..#tempValue'))
  drop table #tempValue
create table #tempValue(
vvalue varchar(8000)
)

declare --cursor variables
@id int,
@flavour varchar(8000),
@sqlquery varchar(8000),
@replacementString varchar(8000),
@value varchar(8000)



declare cursor_performanceIndicators cursor for
select id,flavour,sqlquery from security_performance_indicators
where sq_id = @sq_id and active = 1

open cursor_performanceIndicators

fetch next from cursor_performanceIndicators
into @id,@flavour,@sqlquery

while @@FETCH_STATUS = 0
begin
set @replacementString = dbo.fn_tableprefix(@CompanyAutoID,@flavour)

set @sqlquery = replace(@sqlQuery,'dbo.',@replacementString)

declare @RunSQL varchar(8000)
select @RunSQL = cast(@sqlStartStr as varchar(8000)) + cast(@sqlquery as varchar(8000))


insert into #tempValue
exec (@RunSQL)

	if @@Error <> 0
	begin
		print '===========@@Error = '+ cast(@@Error as varchar(100))
		print 'Security Performance Indicator ID = ' + cast(@id as varchar(100))
		print @RunSQL

	end
	else
	begin
		select @value = vvalue from #tempValue
		--now update the row
		if exists(select * from working_performance_indicator_value where username = @username and spi_id = @id)
		begin--row exists update it
			update working_performance_indicator_value set value = @value where username = @username and spi_id = @id
		end
		else--row does not exist create it
		begin	
		insert into working_performance_indicator_value(spi_id,value,username)values(@id,@value,@username)
	end
	delete #tempValue
end
fetch next from cursor_performanceIndicators
into @id,@flavour,@sqlquery
end
deallocate cursor_performanceIndicators












































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_TestAddrLabel]') and xtype = 'P ')  
 drop Procedure sp_TestAddrLabel
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
-- =============================================
-- sp_TestAddrLabel
CREATE PROCEDURE [dbo].[sp_TestAddrLabel] 
AS
BEGIN
  SET NOCOUNT ON;

  SELECT Name = CASE WHEN (LAB.dlr_logo_bmp is null) THEN T.TLab_Name ELSE '' END,
  TLab_Addr as Address, LAB.dlr_logo_bmp as DealerLogo, dlr_lbl_logo_bmp as DealerLabelLogo
  FROM Test_AddrLabel T
  LEFT OUTER JOIN dlr_lab LAB on LAB.dlr_Customer_ID = T.TLab_Cust_ID_for_logo
END






































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_TmpFred]') and xtype = 'P ')  
 drop Procedure sp_TmpFred
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
-- =============================================
-- Description:	Generate Dataset for Goldenrod Employee Lookup
--
-- Gets Data from:
-- D_PY_History      -Piece Work released to Payroll
-- D_PY_Integration  -Piece Work Imported from D_BC_Transactions (Download Function)
-- D_BC_Transactions -Scanned but not 'Downloaded' into D_PY_Integration yet 
--
-- Parameter MaxPayPeriodNum is number of Unique Dates for which Records were exported to Payroll
-- (0 =UnExported, 1 =Last Pay Period,  rest are history in D_PY_History)
-- =============================================
-- sp_TmpFred 564, 0
CREATE PROCEDURE [dbo].[sp_TmpFred] 
	@Emp_No int = 0,
	@MaxPayPeriodNum int = 999
AS
BEGIN
	SET NOCOUNT ON;

    declare @hr_db varchar(50), @sql varchar(5000), @emp_name varchar(70), @emp_long_name varchar(80)

    select @hr_db=isnull(hr_db,'') from database_setup

CREATE TABLE #TMP1
(
	Name varchar(70),
    LongName varchar(80)    
)

    set @sql= '
      SELECT emp_first_name + '' '' + emp_last_name,
      emp_first_name + '' '' +
      CASE WHEN (ISNULL(emp_initial,'''') <> '''') THEN emp_initial + '' '' ELSE '''' END +
      emp_last_name
      FROM ' + @hr_db + '..employee
      WHERE emp_no=' + CAST(@Emp_No AS varchar(10))

    INSERT #TMP1
    exec(@sql)

    SELECT @emp_name = Name FROM #TMP1
    SELECT @emp_long_name = LongName FROM #TMP1

    DELETE Working_PayPeriodNum
    WHERE emp_no = @Emp_No

	INSERT Working_PayPeriodNum (emp_no, et_date)
       SELECT @Emp_No, et_date
	           FROM D_PY_History
	           WHERE Emp_No = @Emp_No
               GROUP BY et_date
	           ORDER BY et_date DESC
	
    DECLARE @RNum int
    SET @RNum = 0

    UPDATE Working_PayPeriodNum
	SET @RNum = PayPeriodNum = @RNum +1
	WHERE Emp_No = @Emp_No

--	SELECT * FROM Working_PayPeriodNum

--	SELECT @emp_name as Name,  @emp_long_name as LongName, 3 AS OrderSeq, 'Released' AS PayStatus, Pct_Complete, Setup_Hrs, Run_hrs, Total_Hrs, PY_Rate,
--    CASE WHEN Is_Press='T' THEN 'Press' ELSE 'Collate' END AS MachineType, PYH.Entry_Date AS ScanDate,
--    CASE WHEN PYH.Manual_Entry='T' THEN 'Added' ELSE 'Wanded' END AS Wanded_Added,
--	PY_Calc_Amount, DTMP.et_date AS PayDate,
--	DH.oh_type, DH.oh_num, DH.oh_fold_YN as Fold,
--    QH.Q_ClientName, QH.Q_Width + ' x ' + QH.Q_Length AS SIZE, QH.Q_DblWide AS RunWide, 
--    CASE QH.Q_QE_YN  WHEN 'Y' THEN 'QE' WHEN 'N' THEN 'Custom' ELSE '' END AS Pricing,
--    QG.QG_Desc, QG.QG_Qty, QG.QG_Parts, QG.QG_NumPlates,
--	DTMP.PayPeriodNum
--    FROM D_PY_History PYH LEFT OUTER JOIN
--    D_Head DH ON  DH.oh_ID = PYH.Docket_ID LEFT OUTER JOIN
--	Q_Head QH ON  QH.Q_ID = DH.Q_ID LEFT OUTER JOIN
--	Q_Group QG ON QG.QG_ID = DH.QG_ID LEFT OUTER JOIN
--	Working_PayPeriodNum DTMP ON (PYH.et_date = DTMP.et_date)
--	WHERE PYH.Emp_No = @Emp_No
--    AND DTMP.PayPeriodNum <= @MaxPayPeriodNum    
--
--	UNION
--
--	SELECT @emp_name as Name,  @emp_long_name as LongName, 2 AS OrderSeq, 'In Review' AS PayStatus, Pct_Complete, Setup_Hrs, Run_hrs, Total_Hrs, PY_Rate,
--    CASE WHEN Is_Press='T' THEN 'Press' ELSE 'Collate' END AS MachineType, PYH.Entry_Date AS ScanDate,
--    CASE WHEN PYH.Manual_Entry='T' THEN 'Added' ELSE 'Wanded' END AS Wanded_Added,
--	PY_Calc_Amount, NULL AS PayDate,
--	DH.oh_type, DH.oh_num, DH.oh_fold_YN as Fold,
--    QH.Q_ClientName, QH.Q_Width + ' x ' + QH.Q_Length AS SIZE, QH.Q_DblWide AS RunWide, 
--    CASE QH.Q_QE_YN  WHEN 'Y' THEN 'QE' WHEN 'N' THEN 'Custom' ELSE '' END AS Pricing,
--    QG.QG_Desc, QG.QG_Qty, QG.QG_Parts, QG.QG_NumPlates,
--	0 AS PayPeriodNum
--	FROM D_PY_Integration PYH LEFT OUTER JOIN
--    D_Head DH ON  DH.oh_ID = PYH.Docket_ID LEFT OUTER JOIN
--	Q_Head QH ON  QH.Q_ID = DH.Q_ID LEFT OUTER JOIN
--	Q_Group QG ON QG.QG_ID = DH.QG_ID
--	WHERE PYH.Emp_No = @Emp_No
--
--	UNION

--	SELECT @emp_name as Name,  @emp_long_name as LongName, 1 AS OrderSeq, 'Scanned' AS PayStatus, Pct_Complete,
--	CASE WHEN STAT.Is_Press='T' THEN QG.QG_GoldPress_SetupHr ELSE QG.QG_GoldCollate_SetupHr END  AS Setup_Hrs,
--	CASE WHEN STAT.Is_Press='T' THEN QG.QG_GoldPress_RunHr   ELSE QG.QG_GoldCollate_RunHr   END  AS Run_hrs,
--	0 AS Total_Hrs, 0 AS PY_Rate,
--    CASE WHEN STAT.Is_Press='T' THEN 'Press' ELSE 'Collate' END AS MachineType, TX.Scan_Date AS ScanDate,
--    CASE WHEN TX.Terminal_No='MANUAL' THEN 'Added' ELSE 'Wanded' END AS Wanded_Added,
--	0 As PY_Calc_Amount, NULL AS PayDate,
--	DH.oh_type, DH.oh_num, DH.oh_fold_YN as Fold,
--    QH.Q_ClientName, QH.Q_Width + ' x ' + QH.Q_Length AS SIZE, QH.Q_DblWide AS RunWide, 
--    CASE QH.Q_QE_YN  WHEN 'Y' THEN 'QE' WHEN 'N' THEN 'Custom' ELSE '' END AS Pricing,
--    QG.QG_Desc, QG.QG_Qty, QG.QG_Parts, QG.QG_NumPlates,
--	0 AS PayPeriodNum
--	FROM D_BC_Transactions TX LEFT OUTER JOIN
--    D_Head DH ON  DH.oh_ID = TX.Docket_ID LEFT OUTER JOIN
--	Q_Head QH ON  QH.Q_ID = DH.Q_ID LEFT OUTER JOIN
--	Q_Group QG ON QG.QG_ID = DH.QG_ID LEFT OUTER JOIN
--	D_StatusBadgeSwipe_Ctrl SWIPE ON SWIPE.Swipe_Code = TX.Swipe_Code LEFT OUTER JOIN
--	C_Status_Ctrl STAT ON STAT.Status_Code = SWIPE.Status_Code
--	WHERE TX.Emp_No = @Emp_No
--    AND TX.exported_py='F' and TX.status_code in (select status_code from c_status_ctrl where is_press='T' or is_collate='T')
--	ORDER BY PayPeriodNum, OrderSeq, DH.oh_type, DH.oh_num
 


	SELECT * FROM D_BC_Transactions TX LEFT OUTER JOIN
    D_Head DH ON  DH.oh_ID = TX.Docket_ID LEFT OUTER JOIN
	Q_Head QH ON  QH.Q_ID = DH.Q_ID LEFT OUTER JOIN
	Q_Group QG ON QG.QG_ID = DH.QG_ID LEFT OUTER JOIN
	D_StatusBadgeSwipe_Ctrl SWIPE ON SWIPE.Swipe_Code = TX.Swipe_Code LEFT OUTER JOIN
	C_Status_Ctrl STAT ON STAT.Status_Code = SWIPE.Status_Code
	WHERE TX.Emp_No = @Emp_No
    AND TX.exported_py='F' and TX.status_code in (select status_code from c_status_ctrl where is_press='T' or is_collate='T')
	ORDER BY DH.oh_type, DH.oh_num

END













































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spAddPrimaryKey]') and xtype = 'P ')  
 drop Procedure spAddPrimaryKey
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
--IF EXISTS (Select 1 from sysobjects where [name]='spAddPrimaryKey' and xtype='P')
--Drop procedure [dbo].[spAddPrimaryKey]
--GO

--spAddPrimaryKey 0, 1, 0, 1, 0 
create procedure [dbo].[spAddPrimaryKey] @Print bit = 1, @Execute bit = 0, @Identitycheck bit = 0, @PrintStatement bit = 0, @IncludeTempTable bit = 0 as 


/*************************************************************************************************************************************

????Store Procedure Name :: spAddPrimaryKey
????Created By???????????? :: Vinay Kumar

????Purpose???????????????? ::????Add Primary key in those tables which doesn't contain primary key.
???????????????????????? If table already contains any identity column with unique data then it'll convert it into a primary key
????????????????????????????OtherWise It'll add a new identity column with a primary key constraint.
????????????????????????????Primary key constraint = PK_+<Column_name>+<Table_Name>

????Input Parameters :: 5????
???????? @Print???????????????? Type = Bit
???????????????????????? 1 for Print Statement 
???????????????????????????? 0 for not Print Statement.
???????????????????????????? [Default Value :: 1]

???????? @Execute???????????? Type = Bit 
???????????????????????????? 1 for Execute Statement
???????????????????????????? 0 for Not Execute Statement.
???????????????????????????? [Default Value :: 0]

???????? @Identitycheck Type = Bit 
???????????????????????????? check that Is any identity column contains duplicate value. 
???????????????????????????? [Default Value :: 0]

???????? @PrintStatement???? Type = Bit 
???????????????????????????? This show resutl information that which tables are updated.
???????????????????????????? 1 for print statements.
???????????????????????????? [Default Value :: 0]

???????? @IncludeTempTable???? Type = Bit 
???????????????????????????? This Flag is used to include Temporary Tables.
???????????????????????????? 1 for include Temporary tables.
???????????????????????????? [Default Value :: 0] 


????Output Parameters :: 0

*************************************************************************************************************************************/

SET NOCOUNT ON 
-- Create Tmp Table 
create table #temp11 (id int identity(1,1), tid int ,tname varchar(200),CoLName varchar(200),isidentity bit,IsTempTable bit) 
 
insert into #temp11 (tid, tname, ColName, isidentity, IsTempTable) 
select id,[name],	bb.ColName,	case when bb.tabid is null then 0 else 1 end,
case when substring([name],0,4)='temp' then 1
	when substring([name],len([name])-3,4)='temp' then 1
	When substring([name],0,3)='temp' then 1
	When substring([name],len([name])-2,3)='temp' then 1
	else 0 end
from (select distinct id, [Name] 
		from sysobjects where xtype='U' 
		and id not in 
			(select distinct parent_obj from sysobjects where xtype in ('PK','UQ') 
			and parent_obj in (select id from sysobjects where xtype='U'))) aa 
left join (select c.object_id as [tabid],	c.[Name] as ColName,	s.[name] as tabName 
			from sys.objects s 
			inner join sys.columns c on s.object_id=c.object_id 
			where type='U' and c.is_identity=1) bb 
on aa.id=bb.tabid --where tabid is null


Declare @Counter int 
Declare @Maxid int 
Declare @Tname varchar(200) 
Declare @Tcol varchar(100) 
Declare @Tid int 
Declare @ColName varchar(200) 
Declare @isidentity bit 
Declare @SqlQuery nvarchar(4000) 
declare @Result varchar(8000) 
Declare @IdentitycheckString varchar(8000)
Declare @IsTempTable bit
 
 
set @Counter=1
set @IsTempTable=0 
set @Result='' 
set @IdentitycheckString='
---------- Show identity column information'
select @Maxid=max(id) from #temp11 
 
set @SqlQuery =' 
/***************************************************************************************************************************** 
 If any table which doesn''t contain Primary key, This script add the primary key. 
 Note:- If table already contains any identity column and contain unique data then it''ll converted in to primary key 
*****************************************************************************************************************************/  
' 
print(@SqlQuery) 

set @SQlQuery='
Select ''--------- Before Script ---------'' 
select distinct [Name] as ''Table which doesn''''t contain primary key'' from sysobjects where xtype=''U'' 
and id not in (select distinct parent_obj from sysobjects where xtype in (''PK'',''UQ'') 
and parent_obj in (select id from sysobjects where xtype=''U'')) order by 1'

Execute (@sqlQuery)


 
while (@counter<=@Maxid) 
begin 
	select @Tid=Tid, @Tname=Tname, @ColName=ColName, @isidentity=isidentity, @IsTempTable=IsTempTable 
	from #temp11 where id=@counter 
	
	select top 1 @Tcol= case when substring([Name],1,charindex('_',[Name]))='' then (Upper(substring(@Tname,1,3)))+'_' 
	else substring([Name],1,charindex('_',[Name])) end from syscolumns where id=object_id(@Tname) 

	 
	set @Tcol=@Tcol+'PKkey'

	if (@Identitycheck=1 and @isidentity=1)
	begin
		set @IdentitycheckString = @IdentitycheckString +'
		select '''+@Tname+''' as ''Table_Name'', '''+@ColName+''' as ''Identity_Column_Name'', 1 as ''ISDuplicateValue'' from '+@Tname+' group by '+@ColName+' having count(*)>1
		GO'
	end 

	if (@isidentity=1) 
	begin 
		set @SQlQuery=' 
		/********************************************************************* 
		 Table Name :: '+@Tname+', Column Name :: '+@ColName+' 
		*********************************************************************/ 
		IF NOT EXISTS (select top 1 * from '+@Tname+' group by '+@ColName+' having count(*)>1) 
		begin 
			IF NOT EXISTS(select top 1 * from sysobjects where sysobjects.[name]=''PK_'+@ColName+'_'+@Tname+''') 
			begin 
				alter table ['+@Tname+'] add constraint [PK_'+@ColName+'_'+@Tname+'] primary key (['+@ColName+']) 
			End 
		End' 
		set @Result=@Result+' 
		Print ''Table :: ['+@Tname +'] Primary Column :: ['+@ColName+']'' ' 
	end 

	else 
	begin  
		set @SQlQuery=' 
		/********************************************************************* 
		 Table Name :: '+@Tname+', Column Name :: '+@Tcol+' 
		*********************************************************************/ 
		IF NOT EXISTS(select top 1 * from sysobjects where sysobjects.[name]=''PK_'+@Tcol+'_'+@Tname+''') 
		begin 
			Alter table ['+@Tname+'] add ['+@Tcol+'] int identity(1,1) constraint [PK_'+@Tcol+'_'+@Tname+'] primary key 
		end' 
		set @Result=@Result+' 
		Print ''Table :: ['+@Tname +'] Primary Column :: ['+@Tcol+']'' ' 
	end 
	
	if (@Execute=1)
	begin 
		if (@IncludeTempTable=1)
			execute (@SQlQuery) 
		else if (@IsTempTable=0)
			execute (@SQlQuery) 
	end 
	
	if (@print=1) 
	begin 
		set @SQlQuery=@SQlQuery+' 
		GO ' 
		if (@IncludeTempTable=1)
			print (@SQlQuery)
		else
		begin
			if (@IsTempTable=0)
				print (@SQlQuery)
		end
end 

set @SQlQuery=''


if (@IdentityCheck=1) 
Print (@IdentityCheckString) 

if (@printstatement=1) 
print (@Result) 


set @SQlQuery=''
set @Result=' 
' 
set @IdentitycheckString='
'
set @counter=@counter+1 

	
end-- While end 
 
drop table #temp11 
set @SQlQuery='
Select ''--------- After Script ---------'' 
select distinct [Name] as ''Table which doesn''''t contain primary key'' from sysobjects where xtype=''U'' 
and id not in (select distinct parent_obj from sysobjects where xtype in (''PK'',''UQ'') 
and parent_obj in (select id from sysobjects where xtype=''U'')) order by 1'

Execute (@sqlQuery)

---- Execute statement

--EXECUTE spAddPrimaryKey 0,1,0,0,0





Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tr_d_bc_transactions_insert]') and xtype = 'TR')  
 drop Trigger tr_d_bc_transactions_insert
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go








CREATE        trigger [dbo].[tr_d_bc_transactions_insert] on [dbo].[D_BC_Transactions]
for insert
as

declare
@sql varchar(4000),
@hr_db varchar(50),
@docket_id int,
@cur_pk_id int


select @hr_db=isnull(hr_db,'') from database_setup

select @docket_id=docket_id, @cur_pk_id=pk_id from inserted

--INSERT INTO DEBUG_TBL (DEBUG_MSG) Values ('DEBUG0' + ' DktID = ' + CAST(@docket_id AS varchar))

if not exists (select * from d_status where docket_id=@docket_id)
begin
  insert d_status (docket_id, current_status, operator_last_changed, date_last_changed)
  select inserted.docket_id, inserted.status_code, inserted.emp_badge_num, inserted.scan_date from inserted
end
else
begin
  update d_status set current_status=inserted.status_code, operator_last_changed=inserted.emp_badge_num, date_last_changed=inserted.scan_date from inserted 
  where d_status.docket_id=inserted.docket_id
end


declare @DeptCode varchar(10),
        @IsPress varchar(1),
        @IsCollate varchar(1)

select @DeptCode=s.dept_code, @IsPress=s.Is_Press, @IsCollate=s.Is_Collate
from c_status_ctrl s join inserted on inserted.status_code=s.status_code

if @DeptCode<>(select isnull(d.current_dept,'') from d_status d join inserted on inserted.docket_id=d.docket_id)
  update d_status set current_dept=@DeptCode, date_last_dept_chg=getdate() where docket_id=(select docket_id from inserted)

--Handle Percent Complete for Press Types, Collate Types and Non-Press + Non-Collate Types

if @IsPress = 'T'
begin
  if exists(
    select * from D_BC_Transactions TX
    join C_Status_Ctrl ST on ST.Status_Code = TX.Status_Code
    where ST.IS_Press='T' and TX.docket_id=@docket_id and TX.pk_id <> @cur_pk_id
  )
  begin
    update d_bc_transactions set pct_complete=100-(
      select isnull(sum(TX.pct_complete),0)
      from d_bc_transactions TX
      join C_Status_Ctrl ST on ST.Status_Code = TX.Status_Code
      where ST.IS_Press='T' and TX.docket_id=@docket_id and TX.pk_id <> @cur_pk_id
    )
    where pk_id=(select inserted.pk_id from inserted)
  end
end

if @IsCollate = 'T'
begin
  if exists(
    select * from D_BC_Transactions TX
    join C_Status_Ctrl ST on ST.Status_Code = TX.Status_Code
    where ST.IS_Collate='T' and TX.docket_id=@docket_id and TX.pk_id <> @cur_pk_id
  )
  begin
    update d_bc_transactions set pct_complete=100-(
      select isnull(sum(TX.pct_complete),0)
      from d_bc_transactions TX
      join C_Status_Ctrl ST on ST.Status_Code = TX.Status_Code
      where ST.Is_Collate='T' and TX.docket_id=@docket_id and TX.pk_id <> @cur_pk_id
    )
    where pk_id=(select inserted.pk_id from inserted)
  end
end

if ((@IsPress <> 'T') and (@IsCollate <> 'T'))
begin
  if exists (select * from d_bc_transactions where docket_id=(select docket_id from inserted) and swipe_code=(select swipe_code from inserted))
  begin
    update d_bc_transactions set pct_complete=100-(select isnull(sum(d_bc_transactions.pct_complete),0) from d_bc_transactions, inserted 
							where d_bc_transactions.docket_id=inserted.docket_id	
					  		and d_bc_transactions.swipe_code=inserted.swipe_code 
							and d_bc_transactions.pk_id<>inserted.pk_id)
	where pk_id=(select inserted.pk_id from inserted)
  end
end



declare @pct_complete decimal(18,2)
select @pct_complete=pct_complete from d_bc_transactions where pk_id=(select pk_id from inserted)
if @pct_complete<0
   update d_bc_transactions set pct_complete=0 where pk_id=(select pk_id from inserted)


declare @Date datetime, @Emp_No int
select @date=cast(cast(datepart(mm,inserted.scan_date) as varchar)
+'/'+cast(datepart(dd,inserted.scan_date) as varchar)
+'/'+cast(datepart(yyyy,inserted.scan_date) as varchar) as datetime),
@Emp_No=emp_no
from inserted

--if not exists (select * from time_transactions where emp_no=@Emp_No and entry_date=@Date)
--   insert time_transactions(emp_no, entry_date, time_in) select @Emp_No, @Date, inserted.scan_date from inserted

--get the department from the status code swiped in
--if the department is different than inserted.current_dept then update it and set the date_last_dept_changed field










































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tr_d_bc_transactions_update]') and xtype = 'TR')  
 drop Trigger tr_d_bc_transactions_update
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
CREATE   trigger tr_d_bc_transactions_update on d_bc_transactions
for update
as
update d_py_integration set pct_complete=inserted.pct_complete,
py_calc_amount=round(py_rate*total_hrs*(inserted.pct_complete/100),2)
from inserted
where d_py_integration.scan_id=inserted.pk_id and d_py_integration.exported<>'T'
























































































































Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
