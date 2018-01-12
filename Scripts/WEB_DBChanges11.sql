go
alter table system_ctrl add DefaultTransitionType varchar(30) not null default 'Fade'
/*
Clock
Comb
Cover
Dissolve
Fade
Push
PushFade
Shape
SlideFade
None
*/
go
create table DB_ManagementNotification(
	id int not null identity(1,1) primary key,
	LinkTable varchar(100),
	LinkField varchar(100),
	LinkID int,	
	[Group] varchar(50),
	SubGroup varchar(50),
	Description1 varchar(100),
	Description2 varchar(100),
	Notes varchar(max),
	Dismissed bit not null default 0,
	DismissedDate datetime,
	Viewed datetime,
	Flag int)
go
create table DB_ManagementReviewTypes(
	id int not null primary key,
	TypeDescription varchar(50),
	TypeImage varbinary(max))
go
alter table DB_ManagementReviewTypes add
	ImagePath varchar(max)
go
alter table system_ctrl add SqlUserLevel varchar(1) CONSTRAINT [DF_SqlUserLevel] DEFAULT 'S' NOT NULL
go
alter table DB_ManagementReviewTypes add 
	ForeColorName int,
	BackColorName int
go

	alter table DB_ManagementNotification add CompanyID int

go

	alter table DB_ManagementNotification alter column Description1 varchar(max)
go
	alter table DB_ManagementNotification alter column Description2 varchar(max)
go
Create table Security_KBI_Dashboard_Instance(
	id int not null identity(1,1) primary key,
	DashboardDescription varchar(100),
	DashboardType varchar(20)) --Page, Drilldown, Both
	
Create table Security_KBI_Dashboard_Instance_Datasource(--ties the dashboard instance to each of the datasources
	id int not null identity(1,1) primary key,
	Security_KBI_Dashboard_Instance_ID int,
	Security_KBI_Instance_ID int,
	Security_KBI_ID int)
go
alter table Security_KBI_Dashboard_Instance add
	ParentID int
go	
alter table Security_Queries add
	FriendlyQuerySQL varchar(max)
go
alter table Security_KBI_Dashboard_Instance add
	Dashboard varbinary(max)
go
alter table Security_KBI_Dashboard_Instance add
	username varchar(10)
go
alter table Security_Queries add
	FromClause varchar(max)
go
create table Security_KBI_Instance_Restricted_Columns(
	id int not null identity(1,1) primary key,
	Security_KBI_Instance_ID int,
	Security_Query_Columns_ID int)
go
create table DashboardSession(
	id int not null identity(1,1) primary key,
	Username varchar(10),
	Department varchar(20),
	Mode varchar(10),
	CompanyID int,
	DashboardID int,
	ServerName varchar(max),
	DatabaseName varchar(max),
	Dated datetime not null default getdate())
go
alter table DashboardSession add
	UnderlyingDataColumn varchar(max),
	UnderlyingData varchar(max)
go
CREATE TABLE working_Security_Query_Columns(
	id int not null identity(1,1) primary key,
	[Selected] [bit] NULL,
	[Column] [varchar](100) NULL,
	[Security_KBI_Instance_ID] [int] NULL,
	[Security_Query_Columns_ID] [int] NULL,
	[Username] [varchar](10) NULL
) 
GO				   
alter table working_Security_Query_Columns add
	TargetDepartment varchar(10),
	TargetUser varchar(10)
go
alter table System_Ctrl add DashboardMode varchar(30) not null default 'Classic'
go
create table Security_KBI_Instance_Target(	  
	id int not null identity(1,1) primary key,
	SKI_ID int,
	Target varchar(20),
	TargetType varchar(12),
	Source varchar(20))
go

if not exists(select * from Versions where VersionID = 10)
begin
	insert Versions(VersionID, Version, Path)
	select 10, '11.00', ''
end
go
update Versions set Version = '11.00' where VersionID = 10
go
update System_Ctrl set CurrentVersion = 10,CurrentVersioncv = '11.00'
go
alter table Security_KBI_Instance add Restricted bit not null default 0
go
alter table DB_ManagementReviewTypes alter column id int
GO
CREATE TABLE WS_Session(
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	SessionDate DATETIME,
	SessionStatus CHAR,--'O'pen, 'C'losed
	ClosedAt DATETIME,
	ContactID INT,
	CompanyID INT)
go

	create table WS_DB_KBI_Session (
		PK_ID int identity(1,1) primary key,
		Session_ID int,
		KBII_ID int,
		Run bit,
		Company varchar(10),
		Module varchar(50),
		KBI varchar(50),
		Period varchar(100),
		[Current] money,
		TargetValue money,
		Previous money,
		Direction varchar(1),
		Change float,
		TargetChange float,
		LastUpdated datetime,
		Owner varchar(100)
	)

go

	alter table ContextItem add FieldServices bit

go

	create table WS_Sync (
		SyncId int identity(1,1) primary key,
		SyncStart datetime,
		SyncComplete datetime,
		Status varchar(25)
	)

go

	alter table WS_Sync add ClientMac varchar(20)

go

	create table WS_SyncLogHeader (
		MatchId int identity(1,1) primary key, 
		PrevMatchId int,
		SyncId int, 
		ContactId int, 
		CompanyId int, 
		LogDate datetime, 
		LogStatus varchar(10), 
		ProjectId int, 
		LemNum varchar(20),
		BillAmount decimal(19,4),
		Deleted bit
	)

go

	alter table WS_SyncLogHeader add Description varchar(max)

go

	alter table WS_SyncLogHeader add EmailData varbinary(max)

go

	alter table WS_SyncLogHeader add Emailing bit

go

	create table WS_SyncLabourTimeEntry (
		MatchId int identity(1,1) primary key, 		
		PrevMatchId int,
		SyncId int, 
		CompanyId int, 		
		HeaderMatchId int, 
		EmpNum int, 
		Level1Id int, 
		Level2Id int,
		Level3Id int, 
		Level4Id int, 
		Billable bit, 
		wc_code varchar(5), 
		LemStatus char(1), 
		TotalHours decimal(19, 4), 
		BillAmount money, 
		EstId int,
		Deleted bit
	)

go

	alter table WS_SyncLabourTimeEntry add 
		Manual bit,
		IncludedHours decimal(19, 4)

go

	alter table WS_SyncLabourTimeEntry add emp_time bit		

go

	create table WS_SyncLabourTimeDetail (
		MatchId int identity(1,1) primary key, 			
		PrevMatchId int,
		SyncId int, 
		CompanyId int, 
		EntryId int, 
		TimeCodeId int, 
		WorkHours decimal(10,4), 
		BillAmount money,
		Deleted bit
	)

go

	alter table WS_SyncLabourTimeDetail add emp_time bit		

go

	create table WS_SyncEquipTimeEntry (
		MatchId int identity(1,1) primary key, 			
		PrevMatchId int,
		SyncId int, 
		CompanyId int, 
		HeaderMatchId int, 
		EqpNum varchar(10), 
		Level1Id int, 
		Level2Id int,
		Level3Id int, 
		Level4Id int, 
		Billable bit, 
		Quantity decimal(14,4), 
		LemStatus char(1), 
		BillCycle char(1), 
		BillAmount money, 
		EstId int,
		Deleted bit
	)

go

	alter table WS_SyncEquipTimeEntry add emp_time bit	

go

	create table WS_SyncAPDet (
		MatchId int identity(1,1) primary key, 
		PrevMatchId int,
		SyncId int, 
		CompanyId int, 		
		HeaderMatchId int, 
		ap_gl_alloc_id int, 
		Amount decimal(19,4),
		MarkupPct decimal(19,4),
		MarkupAmt decimal(19,4),
		BillAmt decimal(19,4),
		Deleted bit
	)

go

	create table WS_SyncPOHeader (
		MatchId int identity(1,1) primary key,			
		PrevMatchId int,
		SyncId int, 
		ContactId int, 
		CompanyId int, 		
		PONum varchar(20), 
		PODate datetime, 
		SupplierCode varchar(10), 
		ProjectId int 
	)

go

	create table WS_SyncPODetail (
		MatchId int identity(1,1) primary key,		
		PrevMatchId int,
		SyncId int, 
		CompanyId int, 
		HeaderMatchId int, 
		LineNum int, 
		Description varchar(150), 
		Level1Id int, 
		Level2Id int,
		Level3Id int, 
		Level4Id int, 
		LEMComp varchar(1),
		Billable bit, 
		Amount decimal(19, 4)
	)

go
	
	create table WS_SyncAttachment (
		MatchId int identity(1,1) primary key, 		
		PrevMatchId int,
		SyncId int, 
		LinkMatchId int, 
		CompanyId int, 
		ContextItemId int, 
		TableDotField varchar(60), 
		Comment varchar(max), 
		FileName varchar(255), 
		FileData varbinary(max), 
		FileTypeDescription varchar(max), 
		AddedBy varchar(10), 
		DateAdded datetime, 
		MimeType varchar(200), 
		ContactId int,
		Deleted bit
	)

go

	alter table WS_SyncAttachment add InternalOnly bit

go

	create table WS_SyncDeleteHistory (
		Id int identity(1,1) primary key, 		
		SyncId int, 
		TableName varchar(50),
		MatchId int,
		CompanyId int,
		Success bit
	)

go

	alter table WS_SyncDeleteHistory add emp_time bit		

go

	alter table system_ctrl add NumDayFLEMSheetAvail int

go

	CREATE TABLE [dbo].[MobileWebToken](
					[UserId] [int] NOT NULL,
					[AuthToken] [uniqueidentifier] NOT NULL,
					[IssuedOn] [datetime] NOT NULL,
					[ExpiresOn] [datetime] NOT NULL,
	CONSTRAINT [PK_MobileWebToken] PRIMARY KEY CLUSTERED 
	(
					[UserId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

GO

	CREATE TABLE [dbo].[MobileSync](
					[Id] [int] IDENTITY(1,1) NOT NULL,
					[CompanyId] [int] NOT NULL,
					[ClientMac] [varchar] (20) NOT NULL,
					[LastSyncTime] [datetime] NOT NULL,
	CONSTRAINT [PK_MobileSync] PRIMARY KEY CLUSTERED 
	(
					[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

GO

	alter table system_ctrl add 
		MobileCodeVersion varchar(100),
		MobileDbVersion varchar(100)

go

	alter table WS_SyncLogHeader add Quarantine bit
go
	alter table WS_SyncLabourTimeEntry add Quarantine bit
go
	alter table WS_SyncLabourTimeDetail add Quarantine bit
go
	alter table WS_SyncEquipTimeEntry add Quarantine bit
go
	alter table WS_SyncAPDet add Quarantine bit
go
	alter table WS_SyncPOHeader add Quarantine bit
go
	alter table WS_SyncPODetail add Quarantine bit
go
	alter table WS_SyncAttachment add Quarantine bit
go
	alter table WS_SyncDeleteHistory add Quarantine bit
go
	alter table WS_Sync add CompanyId int
go
go
create table working_SendTransmittalDetailsDocuments(
	id int not null identity(1,1) primary key,
	DocumentType varchar(150),
	DocumentDate datetime,
	DocumentName varchar(150),
	Username varchar(10))
go
alter table working_SendTransmittalHeader add
	ProjectCode int,
	ProjectName varchar(150),
	pri_site1 varchar(50),
	site_address varchar(200),
	site_city varchar(20),
	site_state varchar(2),
	site_zip varchar(10)
go
update system_ctrl set MobileCodeVersion='1.0.0.9', MobileDbVersion='1.0.0.4'
go

	declare @constraint_name varchar(100), @sql varchar(max)

	declare cur cursor for	
	SELECT constraint_name
	FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
	WHERE OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_SCHEMA + '.' + QUOTENAME(CONSTRAINT_NAME)), 'IsPrimaryKey') = 1 and TABLE_NAME = 'KBI_YEARLY_ACCOUNT_BALANCE' 
	
	open cur 
	fetch cur into @constraint_name
	while @@fetch_status=0
	begin
		select @sql = '
		alter table KBI_YEARLY_ACCOUNT_BALANCE drop constraint '+@constraint_name
		exec(@sql)

		fetch cur into @constraint_name
	end			
	close cur
	deallocate cur

	alter table KBI_YEARLY_ACCOUNT_BALANCE alter column KBI_PK bigint 
	alter table KBI_YEARLY_ACCOUNT_BALANCE add primary key (KBI_PK)

go


go
	alter table working_Chk_Details add
		ap_inv_header_id int,
		po_id int
go
	alter table working_Chk_Details add
		po_status varchar(10)
go



go
	alter table WS_SyncLogHeader add 
		NonBillAmount decimal(19,4),
		TotalAmount decimal(19,4)
go
go

alter table working_SendTransmittalDetailsDocuments add FileNameOveride varchar(200)
go

GO

/****** Object:  Table [dbo].[DBA_Reflex_Index_Storage]    Script Date: 1/3/2018 12:12:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DBA_Reflex_Index_Storage](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TableID] [int] NULL,
	[TableName] [varchar](max) NULL,
	[IndexID] [int] NULL,
	[IndexName] [varchar](max) NULL,
	[IndexDefinition] [varchar](max) NULL,
	[CurrentState] [varchar](50) NOT NULL,
	[IndexSource] [varchar](100) NOT NULL,
	[DropIndex] [bit] NOT NULL,
	[ResurectIndex] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[DBA_Reflex_Index_Storage] ADD  DEFAULT ('Implemented') FOR [CurrentState]
GO

ALTER TABLE [dbo].[DBA_Reflex_Index_Storage] ADD  DEFAULT ('Unknown') FOR [IndexSource]
GO

ALTER TABLE [dbo].[DBA_Reflex_Index_Storage] ADD  DEFAULT ((0)) FOR [DropIndex]
GO

ALTER TABLE [dbo].[DBA_Reflex_Index_Storage] ADD  DEFAULT ((0)) FOR [ResurectIndex]
GO

alter table DBA_MissingIndexes alter column Impact bigint
go
alter table contact add IsFieldManager2 varchar(1)
go
alter table companies add WebPortalActive bit
go
update companies set WebPortalActive=1 where WebPortalActive is null
go
