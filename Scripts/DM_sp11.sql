if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[fn_diagramobjects]') and xtype = 'FN')  
 drop Function fn_diagramobjects
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

	CREATE FUNCTION dbo.fn_diagramobjects() 
	RETURNS int
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		declare @id_upgraddiagrams		int
		declare @id_sysdiagrams			int
		declare @id_helpdiagrams		int
		declare @id_helpdiagramdefinition	int
		declare @id_creatediagram	int
		declare @id_renamediagram	int
		declare @id_alterdiagram 	int 
		declare @id_dropdiagram		int
		declare @InstalledObjects	int

		select @InstalledObjects = 0

		select 	@id_upgraddiagrams = object_id(N'dbo.sp_upgraddiagrams'),
			@id_sysdiagrams = object_id(N'dbo.sysdiagrams'),
			@id_helpdiagrams = object_id(N'dbo.sp_helpdiagrams'),
			@id_helpdiagramdefinition = object_id(N'dbo.sp_helpdiagramdefinition'),
			@id_creatediagram = object_id(N'dbo.sp_creatediagram'),
			@id_renamediagram = object_id(N'dbo.sp_renamediagram'),
			@id_alterdiagram = object_id(N'dbo.sp_alterdiagram'), 
			@id_dropdiagram = object_id(N'dbo.sp_dropdiagram')

		if @id_upgraddiagrams is not null
			select @InstalledObjects = @InstalledObjects + 1
		if @id_sysdiagrams is not null
			select @InstalledObjects = @InstalledObjects + 2
		if @id_helpdiagrams is not null
			select @InstalledObjects = @InstalledObjects + 4
		if @id_helpdiagramdefinition is not null
			select @InstalledObjects = @InstalledObjects + 8
		if @id_creatediagram is not null
			select @InstalledObjects = @InstalledObjects + 16
		if @id_renamediagram is not null
			select @InstalledObjects = @InstalledObjects + 32
		if @id_alterdiagram  is not null
			select @InstalledObjects = @InstalledObjects + 64
		if @id_dropdiagram is not null
			select @InstalledObjects = @InstalledObjects + 128
		
		return @InstalledObjects 
	END
	

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CFS_AttachFile]') and xtype = 'P ')  
 drop Procedure CFS_AttachFile
Go
SET QUOTED_IDENTIFIER ON
Go
SET ANSI_NULLS ON 
Go

CREATE proc [dbo].[CFS_AttachFile] @FileName varchar(255), @FileData varbinary(max), @ContactID int,
	@FileType varchar(2), @TempID int, @TempLink varchar(50), @InternalOnly bit, @FileStatus char(1), @FileOrigin varchar(10),
	@OriginLink int, @OriginLink2 int, @permanent_tf varchar(1), @CurrentTCSE_ID int
as
begin
	declare @FileRepository_ID int
			
	insert into CFS_FileRepository (FileName, FileData, ContactID, DateAdded, FileType, TempID, TempLink, Mime_type, 
		InternalOnly, FileStatus, FileOrigin, OriginLink, OriginLink2, permanent_tf, CurrentTCSE_ID)
	select @FileName, @FileData, @ContactID, getdate(), @FileType, @TempID, @TempLink, null,
		isnull(@InternalOnly, 0), @FileStatus, @FileOrigin, @OriginLink, @OriginLink2, isnull(@permanent_tf,'F'), @CurrentTCSE_ID
		
	select @FileRepository_ID=SCOPE_IDENTITY()
	
	--mime_type list from: http://stackoverflow.com/questions/1029740/get-mime-type-from-filename-extension
	update r
	set r.Mime_Type=m.MimeType
	from CFS_FileRepository r 
	join CFS_MimeType m on m.Extension=r.Ext
	where r.ID=@FileRepository_ID
	
	select @FileRepository_ID [FileRepository_ID]
end


Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CFS_CreateFileLink]') and xtype = 'P ')  
 drop Procedure CFS_CreateFileLink
Go
SET QUOTED_IDENTIFIER ON
Go
SET ANSI_NULLS ON 
Go

CREATE proc [dbo].[CFS_CreateFileLink] @FileRepository_ID int, @CompanyID int, @DBFlavour varchar(5), @TableDotField varchar(60), @IDValue int, 
	@ContextItem_ID int, @IsOrigin bit,
	--NEED TO DISCUSS BELOW PARAMS
	@TargetPrint varchar(3),
	@Comment varchar(max),
	@ElectronicSaveSetup_ID int,
	@LinkOrigin varchar(30),
	@DraftingFileTypeID int,
	@OriginalSrcID int
as
begin
	insert into CFS_FileLink (FileRepository_ID,CompanyID, DBFlavour, ContextItem_ID, IsOrigin, TableDotField, IDValue, TargetPrint, Comment, LinkOrigin )
	select @FileRepository_ID,@CompanyID, @DBFlavour, @ContextItem_ID, isnull(@IsOrigin,0), @TableDotField, @IDValue, @TargetPrint, @Comment, @LinkOrigin
end



Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[PP_DeleteFileData]') and xtype = 'P ')  
 drop Procedure PP_DeleteFileData
Go
SET QUOTED_IDENTIFIER ON
Go
SET ANSI_NULLS ON 
Go
create  proc [dbo].[PP_DeleteFileData]
@Report_Schedule_Hist_ID int , @contact_id int
as

--declare @Report_Schedule_Hist_ID int , @contact_id int
--select @Report_Schedule_Hist_ID = 64
  
select  @contact_id  = isnull(@contact_id,0)

declare @FileRepository_ID	 int
 
select @FileRepository_ID = FileRepository_ID from dbo.CFS_FileLink 
where TableDotField = 'Report_Schedule_Hist.id'  and IDValue = @Report_Schedule_Hist_ID 	

declare @count int
select @count = COUNT(id) 
from dbo.CFS_FileLink 
where TableDotField = 'Contact.id'  and IDValue <> @contact_id 	
and FileRepository_ID =@FileRepository_ID

if  @contact_id = 0
begin
	delete CFS_FileLink where FileRepository_ID =@FileRepository_ID
	delete CFS_FileRepository where id =@FileRepository_ID
end
else
begin
	if  @count >0
	begin
	   delete CFS_FileLink where FileRepository_ID =@FileRepository_ID
		 and TableDotField = 'Contact.id'  and IDValue = @contact_id 	 
	end
	else
	begin
		delete CFS_FileLink where FileRepository_ID =@FileRepository_ID
		delete CFS_FileRepository where id =@FileRepository_ID
	end
end 
select 	@count 'Count'



Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[PPM_DeleteAttachments]') and xtype = 'P ')  
 drop Procedure PPM_DeleteAttachments
Go
SET QUOTED_IDENTIFIER ON
Go
SET ANSI_NULLS ON 
Go
create proc PPM_DeleteAttachments
 
 @type varchar(60), @TypeID int
 as
 begin
 
     
     --declare  @type varchar(60), @TypeID int
     --select @type ='PPM_Meeting.ID', @TypeID = 1
     
	 if exists (select * from tempdb..sysobjects where id = object_id(N'tempdb..#FileRepository_ID'))
			drop table #FileRepository_ID
	 create table #FileRepository_ID(
			ID int
		)
		
	 insert #FileRepository_ID
	 select FileRepository_ID from CFS_FileLink where TableDotField= @type and IDValue = @TypeID
		
  	 delete CFS_FileLink where FileRepository_ID in (select ID from #FileRepository_ID )
	 
	 delete CFS_FileRepository 
	 where  ID in (select ID from #FileRepository_ID )
	
 end    


Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_alterdiagram]') and xtype = 'P ')  
 drop Procedure sp_alterdiagram
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

	CREATE PROCEDURE dbo.sp_alterdiagram
	(
		@diagramname 	sysname,
		@owner_id	int	= null,
		@version 	int,
		@definition 	varbinary(max)
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
	
		declare @theId 			int
		declare @retval 		int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
		declare @ShouldChangeUID	int
	
		if(@diagramname is null)
		begin
			RAISERROR ('Invalid ARG', 16, 1)
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID();	 
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		revert;
	
		select @ShouldChangeUID = 0
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		
		if(@DiagId IS NULL or (@IsDbo = 0 and @theId <> @UIDFound))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1);
			return -3
		end
	
		if(@IsDbo <> 0)
		begin
			if(@UIDFound is null or USER_NAME(@UIDFound) is null) -- invalid principal_id
			begin
				select @ShouldChangeUID = 1 ;
			end
		end

		-- update dds data			
		update dbo.sysdiagrams set definition = @definition where diagram_id = @DiagId ;

		-- change owner
		if(@ShouldChangeUID = 1)
			update dbo.sysdiagrams set principal_id = @theId where diagram_id = @DiagId ;

		-- update dds version
		if(@version is not null)
			update dbo.sysdiagrams set version = @version where diagram_id = @DiagId ;

		return 0
	END
	

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_creatediagram]') and xtype = 'P ')  
 drop Procedure sp_creatediagram
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

	CREATE PROCEDURE dbo.sp_creatediagram
	(
		@diagramname 	sysname,
		@owner_id		int	= null, 	
		@version 		int,
		@definition 	varbinary(max)
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
	
		declare @theId int
		declare @retval int
		declare @IsDbo	int
		declare @userName sysname
		if(@version is null or @diagramname is null)
		begin
			RAISERROR (N'E_INVALIDARG', 16, 1);
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID(); 
		select @IsDbo = IS_MEMBER(N'db_owner');
		revert; 
		
		if @owner_id is null
		begin
			select @owner_id = @theId;
		end
		else
		begin
			if @theId <> @owner_id
			begin
				if @IsDbo = 0
				begin
					RAISERROR (N'E_INVALIDARG', 16, 1);
					return -1
				end
				select @theId = @owner_id
			end
		end
		-- next 2 line only for test, will be removed after define name unique
		if EXISTS(select diagram_id from dbo.sysdiagrams where principal_id = @theId and name = @diagramname)
		begin
			RAISERROR ('The name is already used.', 16, 1);
			return -2
		end
	
		insert into dbo.sysdiagrams(name, principal_id , version, definition)
				VALUES(@diagramname, @theId, @version, @definition) ;
		
		select @retval = @@IDENTITY 
		return @retval
	END
	

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_dropdiagram]') and xtype = 'P ')  
 drop Procedure sp_dropdiagram
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

	CREATE PROCEDURE dbo.sp_dropdiagram
	(
		@diagramname 	sysname,
		@owner_id	int	= null
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
		declare @theId 			int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
	
		if(@diagramname is null)
		begin
			RAISERROR ('Invalid value', 16, 1);
			return -1
		end
	
		EXECUTE AS CALLER;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		REVERT; 
		
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1)
			return -3
		end
	
		delete from dbo.sysdiagrams where diagram_id = @DiagId;
	
		return 0;
	END
	

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
		if @Name in ('ScheduleActivate_FixPredecessor', 'PPM_InsertAttachment', 'PPM_DeleteMilestoneTemp', 'PPM_DeleteDocAttachment', 'PPM_DeleteMilestone', 'PPM_MilestonePublish', 'PPM_GAPPublish', 'PPM_MeetingPublish', 'fn_GetConcatInvNo_New', 'sp_WMSFull_CreatePackStr', 'so_Line_insertUpdate', 'fn_GetProfile', 
		           'EmailFax_UpdateEmailFaxInfo', 'PY_EmpCopy_batch', 'PY_EmpCopy_Batch_Working', 'PY_EmpCopy_ClearBatchTables','PY_EmpCopy_ClearWorkingTables', 'PY_EmpCopy_FromBatch','PY_EmpCopy_FromWorkingTables',
				   'PY_EmpCopy_InsertWorkingTables', 'WO_LoadNewWOTemplateItems', 'WS_SO_SubmitOrder', 'AlertDelete', 'AlertSaveAttachment', 'SplitStrings_XML', 'tr_ScheduleTasksUpdate', 'CFS_CreateFileLink', 'CFS_AttachFile', 'PP_DeleteFileData','PPM_DeleteAttachments')
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
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_helpdiagramdefinition]') and xtype = 'P ')  
 drop Procedure sp_helpdiagramdefinition
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

	CREATE PROCEDURE dbo.sp_helpdiagramdefinition
	(
		@diagramname 	sysname,
		@owner_id	int	= null 		
	)
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		set nocount on

		declare @theId 		int
		declare @IsDbo 		int
		declare @DiagId		int
		declare @UIDFound	int
	
		if(@diagramname is null)
		begin
			RAISERROR (N'E_INVALIDARG', 16, 1);
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner');
		if(@owner_id is null)
			select @owner_id = @theId;
		revert; 
	
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname;
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId ))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1);
			return -3
		end

		select version, definition FROM dbo.sysdiagrams where diagram_id = @DiagId ; 
		return 0
	END
	

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_helpdiagrams]') and xtype = 'P ')  
 drop Procedure sp_helpdiagrams
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

	CREATE PROCEDURE dbo.sp_helpdiagrams
	(
		@diagramname sysname = NULL,
		@owner_id int = NULL
	)
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		DECLARE @user sysname
		DECLARE @dboLogin bit
		EXECUTE AS CALLER;
			SET @user = USER_NAME();
			SET @dboLogin = CONVERT(bit,IS_MEMBER('db_owner'));
		REVERT;
		SELECT
			[Database] = DB_NAME(),
			[Name] = name,
			[ID] = diagram_id,
			[Owner] = USER_NAME(principal_id),
			[OwnerID] = principal_id
		FROM
			sysdiagrams
		WHERE
			(@dboLogin = 1 OR USER_NAME(principal_id) = @user) AND
			(@diagramname IS NULL OR name = @diagramname) AND
			(@owner_id IS NULL OR principal_id = @owner_id)
		ORDER BY
			4, 5, 1
	END
	

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_renamediagram]') and xtype = 'P ')  
 drop Procedure sp_renamediagram
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

	CREATE PROCEDURE dbo.sp_renamediagram
	(
		@diagramname 		sysname,
		@owner_id		int	= null,
		@new_diagramname	sysname
	
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
		declare @theId 			int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
		declare @DiagIdTarg		int
		declare @u_name			sysname
		if((@diagramname is null) or (@new_diagramname is null))
		begin
			RAISERROR ('Invalid value', 16, 1);
			return -1
		end
	
		EXECUTE AS CALLER;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		REVERT;
	
		select @u_name = USER_NAME(@owner_id)
	
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1)
			return -3
		end
	
		-- if((@u_name is not null) and (@new_diagramname = @diagramname))	-- nothing will change
		--	return 0;
	
		if(@u_name is null)
			select @DiagIdTarg = diagram_id from dbo.sysdiagrams where principal_id = @theId and name = @new_diagramname
		else
			select @DiagIdTarg = diagram_id from dbo.sysdiagrams where principal_id = @owner_id and name = @new_diagramname
	
		if((@DiagIdTarg is not null) and  @DiagId <> @DiagIdTarg)
		begin
			RAISERROR ('The name is already used.', 16, 1);
			return -2
		end		
	
		if(@u_name is null)
			update dbo.sysdiagrams set [name] = @new_diagramname, principal_id = @theId where diagram_id = @DiagId
		else
			update dbo.sysdiagrams set [name] = @new_diagramname where diagram_id = @DiagId
		return 0
	END
	

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_upgraddiagrams]') and xtype = 'P ')  
 drop Procedure sp_upgraddiagrams
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

	CREATE PROCEDURE dbo.sp_upgraddiagrams
	AS
	BEGIN
		IF OBJECT_ID(N'dbo.sysdiagrams') IS NOT NULL
			return 0;
	
		CREATE TABLE dbo.sysdiagrams
		(
			name sysname NOT NULL,
			principal_id int NOT NULL,	-- we may change it to varbinary(85)
			diagram_id int PRIMARY KEY IDENTITY,
			version int,
	
			definition varbinary(max)
			CONSTRAINT UK_principal_name UNIQUE
			(
				principal_id,
				name
			)
		);


		/* Add this if we need to have some form of extended properties for diagrams */
		/*
		IF OBJECT_ID(N'dbo.sysdiagram_properties') IS NULL
		BEGIN
			CREATE TABLE dbo.sysdiagram_properties
			(
				diagram_id int,
				name sysname,
				value varbinary(max) NOT NULL
			)
		END
		*/

		IF OBJECT_ID(N'dbo.dtproperties') IS NOT NULL
		begin
			insert into dbo.sysdiagrams
			(
				[name],
				[principal_id],
				[version],
				[definition]
			)
			select	 
				convert(sysname, dgnm.[uvalue]),
				DATABASE_PRINCIPAL_ID(N'dbo'),			-- will change to the sid of sa
				0,							-- zero for old format, dgdef.[version],
				dgdef.[lvalue]
			from dbo.[dtproperties] dgnm
				inner join dbo.[dtproperties] dggd on dggd.[property] = 'DtgSchemaGUID' and dggd.[objectid] = dgnm.[objectid]	
				inner join dbo.[dtproperties] dgdef on dgdef.[property] = 'DtgSchemaDATA' and dgdef.[objectid] = dgnm.[objectid]
				
			where dgnm.[property] = 'DtgSchemaNAME' and dggd.[uvalue] like N'_EA3E6268-D998-11CE-9454-00AA00A3F36E_' 
			return 2;
		end
		return 1;
	END
	

Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Tools_CleanUpSequence]') and xtype = 'P ')  
 drop Procedure Tools_CleanUpSequence
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
create procedure Tools_CleanUpSequence
	@Table nvarchar(max),
	@idCol nvarchar(max),
	@SeqCol nvarchar(max),
    @WhereClause nvarchar(max)
as

--exec CleanUpSequence @Table = N'PPM_PublishExpectedAccomplishments',
--	@idCol = N'id',
--	@SeqCol = N'Seq',
--    @WhereClause = N'PPM_PublishPeriodID = 2 and ApplicablePeriod = 1'

--declare @Table nvarchar(max) = N'PPM_PublishExpectedAccomplishments',
--	@idCol nvarchar(max) = N'id',
--	@SeqCol nvarchar(max) = N'Seq',
--    @WhereClause nvarchar(max) = N'PPM_PublishPeriodID = 2 and ApplicablePeriod = 1'

declare @sql nvarchar(max)

set @sql = 
N'update ' + @table + N'
set ' + @SeqCol + N' = NewSeq
from (select ' + @idCol + N', ROW_NUMBER() over (order by ' + @SeqCol + N') [NewSeq] from ' + @Table + N'
where ' + @WhereClause + N') i
where i.' + @idCol + N' = ' + @Table + N'.' + @idCol +' and ' + @WhereClause

exec sp_executesql @sql



Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Tools_ReorderSeq]') and xtype = 'P ')  
 drop Procedure Tools_ReorderSeq
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

--/****** Object:  StoredProcedure [dbo].[Tools_ReorderSeq]    Script Date: 05/26/16 3:24:34 PM ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER OFF
--GO

CREATE procedure [dbo].[Tools_ReorderSeq] 
	@id int, 
	@new_seq int,
	@Table nvarchar(max),
	@idCol nvarchar(max),
	@SeqCol nvarchar(max),
    @WhereClause nvarchar(max)
as 

--begin tran
begin

	--declare
	--	@id int = 9, 
	--	@new_seq int = 5,
	--	@Table nvarchar(max) = 'AttachmentStatus',
	--	@idCol nvarchar(max) = 'id',
	--	@SeqCol nvarchar(max) = 'Seq',
	--    @WhereClause nvarchar(max) = 'pri_id = 59'

	declare @sql nvarchar(max)

	set @sql =	
	
	'declare @old_seq int, @id int = ' + convert(nvarchar(20), @id) + ', @new_seq int = ' + convert(nvarchar(20), @new_seq) + '

	select @old_seq = ' + @SeqCol + '
	from ' + @Table + '
	where ' + @idCol + ' = @id

	if( @new_seq <> @old_seq )
	begin	
		select @old_seq=ISNULL(@old_seq, 9999)

		if(@new_seq > @old_seq)
		begin
			update ' + @Table + '
			set ' + @SeqCol + '=Seq-1
			where ' + @SeqCol + ' <= @new_seq and ' + @idCol + '<>@id  and ' + @WhereClause + '
		end

		update ' + @Table + '
		set ' + @SeqCol + '=' + @SeqCol + ' + 1
		where ' + @SeqCol + ' >= @new_seq and ' + @WhereClause + '

		update ' + @Table + ' 
		set ' + @SeqCol + ' = @new_seq
		where ' + @idCol + ' = @id

		exec Tools_CleanUpSequence @Table = ''' + @Table + ''', @idCol = ''' + @idCol + ''', @SeqCol = ''' + @SeqCol + ''', @WhereClause = ''' + replace(@WhereClause, '''', '''''') + '''

	end'

	print @sql

	exec sp_executesql @sql

end


--rollback tran


Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go
