go 
alter table SecurityFunction add
	HelpBookmarkDocument varchar(200),
	DestinationName varchar(200)
go
GO

/****** Object:  Table [dbo].[Geo_Link]    Script Date: 2017-07-18 4:06:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Geo_Link](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LinkTableName] [varchar](50) NOT NULL,
	[LinkCode] [varchar](50) NOT NULL,
	[Feature] [varchar](50) NOT NULL,
	[LinkType] [smallint] NOT NULL,
	[LinkId] [int] NOT NULL,
 CONSTRAINT [PK_Geo_Link] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Geo_Point]    Script Date: 2017-07-18 4:06:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Geo_Point](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL,
	[TimeStamp] [datetime] NULL,
 CONSTRAINT [PK_Geo_Point] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Geo_Shape]    Script Date: 2017-07-18 4:06:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Geo_Shape](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ShapeId] [int] NOT NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL,
	[PolygonId] [int] NOT NULL,
 CONSTRAINT [PK_Geo_Shape] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Geo_Polyline]    Script Date: 2017-07-18 4:06:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Geo_Polyline](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PolylineId] [int] NOT NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL,
	[LineId] [int] NOT NULL,
 CONSTRAINT [PK_Geo_Polyline] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Geo_Event]    Script Date: 2017-07-18 4:07:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Geo_Event](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LinkCode] [varchar](50) NOT NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL,
	[EventTypeId] [int] NOT NULL,
	[Description] [varchar](200) NOT NULL,
	[EventDate] [datetime] NULL,
 CONSTRAINT [PK_Geo_Event] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Geo_EventType]    Script Date: 2017-07-18 4:08:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Geo_EventType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LinkTableName] [varchar](50) NOT NULL,
	[TypeName] [varchar](50) NOT NULL,
	[MarkerUri] [varchar](200) NULL,
	[HilightMarkerUri] [varchar](200) NULL,
	[ReferenceMarkerUri] [varchar](200) NULL,
	[Feature] [varchar](50) NULL,
 CONSTRAINT [PK_Geo_EventType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

	alter table working_PC_Previous_Billings add ar_balance money

go
alter table Working_PROJ_LOT add lot_id int
go
alter table Working_PROJ_LOT add current_price_change_comments varchar(max)

go

alter table LD_Setup add CalTaxOnLotAgreementCloseDate bit default(0)

go

alter table LD_Setup add Disallow_Unregister_Lot_Sale bit default(0),
                Track_Current_Lot_Price_Change bit default(0)

go

CREATE TABLE [dbo].[LD_Lot_CurrentPriceChange_Log]   -- drop table LD_Lot_CurrentPriceChange_Log
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](200) NULL,
	[DateSaved] [datetime] NULL,
	[Comments] [varchar](max) NULL,
	[proj_lot_id] [int] NULL,
	[Previous_Current_Price] [money] ,

PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

go
alter table Working_PROJ_LOT add Previous_Current_Price money
go
go
create table WM_WasteSetup(
	id int not null identity(1,1) primary key,
	WasteCode varchar(20) not null,
	WasteDescription varchar(100) not null)
go
create table WM_AuditedMaterials(
	id int not null identity(1,1) primary key,
	AuditedMaterialCode varchar (20) not null,
	AuditedMaterialDescription varchar (100) not null)
go
create table WM_LocationSetup(
	id int not null identity(1,1) primary key,
	LocationCode varchar(20) not null,
	LocationDescription varchar(100) not null)
go
create table WM_SourceSetupScreen(
	id int not null identity(1,1) primary key,
	SourceCode varchar(10) not null,
	SourceDescription varchar(80))
go
create table WM_FeeType(
	id int not null identity(1,1) primary key,
	FeeCode varchar(30) not null,
	FeeDescription varchar(30) not null,
	DefaultAmount money not null,
	FeeRevenue varchar(21) not null,
	Hauling bit,
	Handling bit,
	Disposal bit,
	Fuel bit)
go
create table ContractSetup(
	id int not null identity(1,1) primary key,
	ContractTypeCode varchar(20) not null,
	ContractTypeDescription varchar(100) not null)
go
create table PermitSetup(
	id int not null identity(1,1) primary key,
	PermitTypeCode varchar(20) not null,
	PermitTypeDescription varchar(100) not null)
go
create table WM_WasteManagementSetup(
	id int not null identity(1,1) primary key,
	CurrentYear int)
go
if not exists (select * from WM_WasteManagementSetup)
begin
	insert WM_WasteManagementSetup(CurrentYear)
	select datepart(year, getdate())
end

go 
create table WM_RevenueUOMControlCodes(
	id int not null identity(1,1) primary key,
	UOM_Code varchar(6) not null,
	BillingControl varchar(30) not null)
go
create table WM_LandCellNumber(
	id int not null identity(1,1) primary key,
	Code varchar(5) not null,
	Description varchar(25))
go
go
alter table PROJ_LOT_AGREEMENT_DISCOUNTS add unsoldReversed bit not null default(0)
go
create table working_ld_unsell_discounts(
username varchar(500),
PROJ_LOT_AGREEMENT_DISCOUNTS_ID int
)
go
alter table WS_EMP_Time_Code add
	BillingRateType varchar(10)--(R)egular, (O)vertime, (D)ouble, (T)ravel
go
alter table WS_EMP_Time_Code alter column BillingRateType varchar(10)--(R)egular, (O)vertime, (D)ouble, (T)ravel
go


go

create table working_ld_discounts(
ID int identity(1,1),
username varchar(500),
working_ld_lot_search_id int,
ActionRequired varchar(1),
PROJ_LOT_AGREEMENT_DISCOUNTS_ID int,
LD_DISCOUNTS_ID int,
Percentage decimal(16,4),
Amount decimal(16,4),
Selected bit)

go

alter table working_ld_discounts add primary key (ID) 
go
alter table working_ld_discounts drop column percentage
go
alter table working_ld_discounts add Type varchar(1)
go
alter table working_ld_discounts add Value decimal(16,4)
go

go

	alter table WO_ServiceOrder_Request add
		AutomationFail bit,
		ManualAttentionReason varchar(max)

go

	alter table WO_ServiceOrder_Request add inv_id int

go

	alter table WO_ServiceOrder_Request add source varchar(1)

go

	alter table WO_CNC_Hdr drop column pri_id

go

	alter table WO_CNC_Hdr add WO_ServiceOrder_Request_ID int

go

	alter table WO_ServiceOrder_Request add AttentionCode varchar(1)

go

	alter table WO_CNC_Hdr drop column bomAttention

go

	alter table WO_CNC_Hdr add AttentionCode varchar(1)

go

	create table WO_AttentionCodes (
		code varchar(1),
		description varchar(50),
		manual bit
	)

go

	if not exists(select * from WO_AttentionCodes where code = 'B')	
	begin
		insert into WO_AttentionCodes(code, description, manual) 
		select 'B', 'Materials Required', 0
	end

go

	if not exists(select * from WO_AttentionCodes where code = 'M')	
	begin
		insert into WO_AttentionCodes(code, description, manual) 
		select 'M', 'Manual', 1
	end

go

	create table WO_ServiceOrder_Request_AutoResults (
		Result_ID int primary key identity(1,1),
		WO_SO_Request_ID int,
		InfoDate datetime,
		InfoMessage varchar(max),
		ErrorID int
	)

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 215)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 215, 'BFS-AUTO', null, 'CNC-H001', 'Warehouse not populated on CNC Header table.', null, 1, 0, 0
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 216)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 216, 'BFS-AUTO', null, 'CNC-H002', 'Inventory not populated on CNC Header table.', null, 1, 0, 0
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 217)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 217, 'BFS-AUTO', null, 'CNC-H003', 'Work Order Template not populated on CNC Header table.', null, 1, 0, 0
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 218)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 218, 'BFS-AUTO', null, 'CNC-H004', 'Quantity not populated on CNC Header table.', null, 1, 0, 0
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 219)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 219, 'BFS-AUTO', null, 'CNC-H005', 'Entered by not populated on CNC Header table.', null, 1, 0, 0
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 220)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 220, 'BFS-AUTO', null, 'CNC-H006', 'Warehouse entered on CNC Header table does not exist in Warehouse table.', null, 1, 0, 0
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 221)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 221, 'BFS-AUTO', null, 'CNC-H007', 'Inventory entered on CNC Header table does not exist in Inventory table.', null, 1, 0, 0
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 222)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 222, 'BFS-AUTO', null, 'CNC-H008', 'Inventory/Warehouse combo entered on CNC Header table does not exist in Inventory Warehouse table.', null, 1, 0, 0
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 223)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 223, 'BFS-AUTO', null, 'CNC-H009', 'Work Order Template entered on CNC Header table does not exist in Work Order Template table.', null, 1, 0, 0
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 224)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 224, 'BFS-AUTO', null, 'CNC-H010', 'Quantity entered on CNC Header table is less than or equal to zero.', null, 1, 0, 0
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 225)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 225, 'BFS-AUTO', null, 'CNC-H011', 'Entered By entered on CNC Header table does not exist in Contact table.', null, 1, 0, 0
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 226)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 226, 'BFS-AUTO', null, 'CNC-H012', 'BOM Attention has been flagged on CNC Header table.', null, 1, 0, 0
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 227)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 227, 'BFS-AUTO', null, 'CNC-D001', 'Type not populated on CNC Detail table.', null, 1, 0, 0
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 228)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 228, 'BFS-AUTO', null, 'CNC-D002', 'BOM Inventory not populated on CNC Detail table.', null, 1, 0, 0
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 229)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 229, 'BFS-AUTO', null, 'CNC-D003', 'BOM Required Quantity not populated on CNC Detail table.', null, 1, 0, 0
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 230)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 230, 'BFS-AUTO', null, 'CNC-D004', 'BOL Work Class not populated on CNC Detail table.', null, 1, 0, 0
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 231)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 231, 'BFS-AUTO', null, 'CNC-D005', 'BOL Work Class required hours not populated on CNC Detail table.', null, 1, 0, 0
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 232)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 232, 'BFS-AUTO', null, 'CNC-D006', 'Inventory entered on CNC Detail table does not exist in Inventory table.', null, 1, 0, 0
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 233)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 233, 'BFS-AUTO', null, 'CNC-D007', 'Inventory/Warehouse combo entered on CNC Detail table does not exist in Inventory Warehouse table.', null, 1, 0, 0
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 234)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 234, 'BFS-AUTO', null, 'CNC-D008', 'Work Class entered on CNC Detail table does not exist in Work Class table.', null, 1, 0, 0
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 235)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 235, 'BFS-AUTO', null, 'CNC-D009', 'BOM Required Quantity entered on CNC Detail table is less than or equal to zero.', null, 1, 0, 0
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 236)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 236, 'BFS-AUTO', null, 'CNC-D010', 'BOL Work Class entered on CNC Detail table is less than or equal to zero.', null, 1, 0, 0
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 237)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 237, 'BFS-AUTO', null, 'CNC-D011', 'Materials are required.', null, 1, 0, 0
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 238)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 238, 'BFS-AUTO', null, 'CNC-D012', 'Attention Code has been set.', null, 1, 0, 0
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 239)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 239, 'BFS-AUTO', null, 'CNC-D013', 'Work Class ({0}) entered on Detail does not have a valid operation set on it.', null, 1, 0, 1
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 240)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 240, 'BFS-AUTO', null, 'CNC-D014', 'Unable to calculate default ''Level'' on Item Detail.', null, 1, 0, 0
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 241)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 241, 'BFS-AUTO', null, 'CNC-WOC1', 'Error converting clipboard to BFS work order. {0}', null, 1, 0, 1
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 242)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 242, 'BFS-AUTO', null, 'CNC-WOC2', 'Error releasing BFS work order to production. {0}', null, 1, 0, 1
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 243)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 243, 'BFS-AUTO', null, 'CNC-WOC3', 'Error releasing BFS work order bill of materials. {0}', null, 1, 0, 1
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 244)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 244, 'BFS-AUTO', null, 'CNC-WOC4', 'Error releasing BFS work order to inventory. {0}', null, 1, 0, 1
	end

go

	if not exists(select * from WS_SO_SheetErrorCode where ErrorID = 245)	
	begin
		insert into WS_SO_SheetErrorCode(ErrorID, SheetCode, SheetNumber, ErrorCode, ErrorMessage, OverrideMessage, InternalAlert, ExternalAlert, StringFormatItems) 
		select 245, 'BFS-AUTO', null, 'CNC-H013', 'Automation Complete', null, 0, 0, 0
	end

go




go
Alter table work_class
add CeilingCostOverTime decimal(19,4),
CeilingCostDouble decimal(19,4),
CeilingCostTravel decimal(19,4)
go
alter table Working_Proj_Summary add
	pri_est_completion_date datetime
go
ALTER TABLE emp_time_hist ADD CostingType VARCHAR(1)
go
	
	if exists(select * from WS_MenuItemCtrl where ItemID = 47)
		update WS_MenuItemCtrl set Item_Desc='Key Business Indicators', Item_FunctionCode='S_DB_KBI', Item_PageDesc='Relations Management > Key Business Indicators' where ItemID = 47
	else
		insert into WS_MenuItemCtrl(ItemID, Item_Desc, Item_FunctionCode, Item_PageDesc) select 47, 'Key Business Indicators', 'S_DB_KBI', 'Relations Management > Key Business Indicators'
go
go

create table working_ld_levy_update(
ID int identity(1,1) primary key,
username varchar(500),
Pri_id int,
ProjectCode int,
ProjectName varchar(200),
Community varchar(50),
Municipality varchar(50),
Region varchar(50),
Purchaser_ID int,
PurchaserName varchar(100),
Lot_num varchar(10),
Block_Num varchar(10),
Plan_Num varchar(12),
Lot_Class_ID int,
Lot_ClassCode varchar(5),
LevyType_ID int,
LevyType varchar(50),
LevyDescription varchar(50),
Levy_ID int,
Proj_lot_agreement_levy_id int,
OldAmount decimal(16,4),
NewAmount decimal(16,4),
Updated bit
)

go
alter table working_ld_levy_update add LastUpdate bit

go
alter table working_ld_lotsearch add Municipality varchar(50)
go
alter table working_ld_lotsearch add Region varchar(50)
go

	alter table WO_ServiceOrder_Request alter column DESCRIPTION varchar(150)

go





go

create table CHARGE_BACK_MULTI_WORKING (
	ID int primary key identity(1,1),
	USERNAME varchar(50),
	AP_GL_ALLOC_ID int,
	SUPPLIER varchar(10),
	CUSTOMER_ID int,
	PO_ID int,
	PO varchar(20),
	AP_AMOUNT money,
	CB_AMOUNT money,
	PRI_ID int,
	LV1ID int,
	LV2ID int,
	LV3ID int,
	LV4ID int,
	LEM_COMP varchar(1),
	EXPENSE_TYPE varchar(1),
	BILLABLE varchar(1),
	REFERENCE varchar(150),
	GL_ACCOUNT varchar(21),
	GL_DESC varchar(50)
)

go

alter table working_ld_costs_to_transfer add LevelsValid bit

go
alter table Geo_Point drop column LinkTableName, LinkFieldName, LinkCode, LinkFeature

go
go


CREATE TABLE [dbo].[LD_Report_LandType_List](
	[username] [varchar](500) NULL,
	[Land_Type] [varchar](5) NULL
) ON [PRIMARY]

go

--CREATE TABLE [dbo].[working_PYIndustryCodesSelect]  -- drop table working_PYIndustryCodesSelect
--(
--	[username] [varchar](500) NULL,
--	[industry_code_id] [int] NULL
--) ON [PRIMARY]

go


alter table pay_report_employees
add current_earnings money,
    ytd_earnings   money,
	monthly_insurable money,
	ytd_insurable money,
	wsib_max  money,
	industry_rate money
go

-- alter table pay_report_employees  drop column WSIB_Remittance 
--add	WSIB_Remittance money
--go
alter table PY_REPORT_OPTIONS add industry_code_id int NULL
go

go
if not exists(select * from LD_COS_Calc_Methods where code = 'M' )
begin
	insert into LD_COS_Calc_Methods (id, code, description, formula) values ( 2, 'M', 'Margin Driven', 
	'A= (Sum of current price on all unsold lots except current lot) + (Actual sell price of current lot)
B= Actual Sell Price of Current Lot
C= Total Project Budget
D= COS to date for project
Formula = B*(C-D)/A
' )
end
else
begin
	update LD_COS_Calc_Methods set description='Margin Driven',
	 formula='A= (Sum of current price on all unsold lots except current lot) + (Actual sell price of current lot)
B= Actual Sell Price of Current Lot
C= Total Project Budget
D= COS to date for project
Formula = B*(C-D)/A

' where code = 'M' 
end

go

alter table py_report_options add emp_res_prov varchar(5)
go
alter table assignment_time_track_working add Assignee_ID int
go
alter table SecurityFunction add FieldServices bit
go


go

create table working_ld_laa_proj_lot_agreement_discounts
(
	
	[ID] [int] IDENTITY(1,1) primary key,
	[username] varchar(500),
	[agreement_id] [int] NULL,
	[LD_Discounts_ID] [int] NULL,
	[Type] [varchar](1) NULL,
	[Value] [decimal](16, 4) NULL,
	[invoice_id] [int] NULL,
	[processed] [bit] NULL,
	[AmountDiscounted] [decimal](16, 4) NULL,
	[Origional_ID] int
	)

go

if not exists (select * from earn_ctrl_codes where ecc_code = 'G')
	insert into earn_ctrl_codes (ecc_code, ecc_desc)
	select 'G', 'GST'
GO


alter table working_ld_LotSearch add InterimCreated bit

go

create table LD_Lot_Payout_Interim_Header(
LD_Lot_Payout_Interim_Header_ID int identity(1,1) primary key,
Agreement_ID int,
LastSavedBy varchar(10),
LastSavedDate datetime,
Completed bit)

go

CREATE TABLE LD_Lot_Payout_Interim_AdditionalFees(
	[LD_Lot_Payout_Interim_AdditionalFees_ID] [int] IDENTITY(1,1) NOT NULL,
	[LD_Lot_Payout_Interim_Header_ID] int,
	[LD_Additional_Fees_ID] [int] NULL,
	[Amount] [money] NULL,
	[LD_Agreement_AdditionalFess_ID] [int] NULL,
	[lv1ID] [int] NULL,
	[lv2ID] [int] NULL,
	[lv3ID] [int] NULL,
	[lv4ID] [int] NULL,
	[GL_Account] [varchar](21) NULL,
	[GST] [bit] NULL,
	[PST] [bit] NULL,
	[FromAgreement] bit)
go

alter table working_ld_lpa_AdditionalFees add FromAgreement bit

go


CREATE TABLE LD_Lot_Payout_Interim_ProgramFees(
	[LD_Lot_Payout_Interim_ProgramFees_ID] [int] IDENTITY(1,1) primary key,
	[LD_Lot_Payout_Interim_Header_ID] int,
	[LD_ProgramFee_ID] [int] NULL,
	[LD_Agreement_ProgramFee_ID] [int] NULL,
	[Amount] [money] NULL,
	[lv1ID] [int] NULL,
	[lv2ID] [int] NULL,
	[lv3ID] [int] NULL,
	[lv4ID] [int] NULL,
	[GL_Account] [varchar](21) NULL,
	[GST] [bit] NULL,
	[PST] [bit] NULL,
	[FromAgreement] bit)
go
alter table working_ld_lpa_ProgramFees add FromAgreement bit

go
alter table WS_EMP_Time_Code 
add Time_Code_ID_OT int,Time_Code_ID_DT int
go
alter table equip_assign
add eg_code varchar(5),
    ec_code varchar(5)

go

CREATE TABLE FA_RatSchedule_Setup   -- drop table FA_RatSchedule_Setup
(
	ID  int IDENTITY(1,1) NOT NULL,
	uom_id int,
	Cost_Rate  money,
	Bill_Out_Rate money,
	Rate_Type  varchar(2), -- L: Class C: Category
	IS_Default bit  default 0
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



go
CREATE TABLE FA_RatSchedule_Class_Category   -- drop table FA_RatSchedule_Class_Category
(
	ID  int IDENTITY(1,1) NOT NULL,
	uom_id int,
	Cost_Rate  money,
	Bill_Out_Rate money,
	Code varchar(5) , -- Class Code or Category Code
	Rate_Type  varchar(2), -- L: Class C: Category
	IS_Default bit  default 0
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

go

if not exists ( select * from FA_RatSchedule_Setup )
begin
	insert FA_RatSchedule_Setup(uom_id,Cost_Rate,Bill_Out_Rate,Rate_Type,IS_Default )
	select id, 0,0,'L',0
	from unit_time_measurement
    order by Id

	insert FA_RatSchedule_Setup(uom_id,Cost_Rate,Bill_Out_Rate,Rate_Type,IS_Default )
	select id, 0,0,'C',0
	from unit_time_measurement
    order by Id

end

go



CREATE TABLE FA_RatSchedule_Eqi   -- drop table FA_RatSchedule_Eqi
(
	ID  int IDENTITY(1,1) NOT NULL,
	uom_id int,
	Cost_Rate  money,
	Bill_Out_Rate money,
	IS_Default bit  default 0,
	eqi_num varchar(10)
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

go


CREATE TABLE [dbo].[FA_RatSchedule_Eqi_Class]   -- drop table FA_RatSchedule_Eqi_Class
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[cer_id] [int] NOT NULL,
	[uom_id] [int] NULL,
	[Cost_Rate] [money] NULL,
	[Bill_Out_Rate] [money] NULL,
	[IS_Default] [bit] NULL DEFAULT ((0)),
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]




go
go
alter table working_SecurityFunctions add 
	HelpBookmarkDocument varchar(200),
	DestinationName varchar(200)
go
go
Alter table SO_Setup add 
	BypassVAZeroCost bit not null default 0,
	BypassVAFGDescOverwrite bit not null default 0,
	AutoLoadTaskTemplates bit not null default 0
go
if not exists(select * from CFS_ElectronicSaveSetup where ID = 31)
begin
	insert CFS_ElectronicSaveSetup(ID, Module, AutoSave, ContextItem_ID, Description, DisableAutoSave)
	select 31, 'Warehouse Management', 0, null, 'Value Add Work Order', 0
end
go
create table SO_TEMPLATE_HDR(
	id int not null identity(-100,-1) primary key,--we will go backwards to differentiate templates from order lines
	Inv_ID int,-- -1 if OTP
	PartNumber varchar(30) not null,
	PartDescription varchar(150),
	Class_ID int,
	TemplateCode varchar(30),
	TemplateDescription varchar(150))
go
alter table SO_TEMPLATE_HDR add DefaultTemplate bit not null default 0
go
alter table SO_TEMPLATE_HDR add UOM varchar(6)
go
alter table so_tasks add OriginID int
go
alter table working_WMSPickSlipHeader add 
	WarehouseCode varchar(3),
	WarehouseDescription varchar(45)
go
alter table working_wms_po_det add Locations varchar(100)
go
alter table Working_WMS_PO_HDR add LandingFactorComment varchar(500)
go
create table working_wms_po_det_contents(
	id int not null identity(1,1) primary key,
	WOR_PKkey int,--working_wms_po_det identity field
	h_ID int,
	PO_ID int,
	SO_ID int,
	SO_LINE_ID int,
	Task_ID int,
	Parent_Task_ID int,
	Seq int,
	ProcessType varchar(30),
	Destination varchar(30),
	RequiredProduct varchar(30),
	[Description] varchar(150),
	NextSeq int,
	Location varchar(100),
	Username varchar(10))
go
alter table so_tasks add WorkOrderComment varchar(max)
go
alter table working_wms_po_det add WorkOrderComment varchar(max)
go
alter table working_wms_po_det add WOR_PKkey int not null identity(1,1) primary key
go
go

if exists(select SO_TYPE_CODE from SO_TYPE where SO_TYPE_CODE = 'AI')
	update SO_TYPE set Description = 'Accrued Interest', SO_DEFAULT = 'F', IS_SYSTEM='T' where SO_TYPE_CODE = 'AI'
else
begin
	if exists (select * from tempdb..sysobjects where id = object_id(N'tempdb..#tp'))    
		drop table #tp    
	CREATE TABLE #tp ( ID int )
	insert into #tp
	exec sp_GetNextSystemID 'NEXT_SO_TYPE_ID'	
	declare @ID int
	select @ID=ID from #tp
	insert into SO_TYPE(SO_TYPE_ID, SO_TYPE_CODE, DESCRIPTION, SO_DEFAULT, IS_SYSTEM) select @ID, 'AI', 'Accrued Interest', 'F', 'T'
end
go


create table working_ld_agreement_interest(
username varchar(500),
agreement_id int,
agreement_num int,
ProjectName varchar(150),
Block varchar(5),
Lot varchar(5),
PrincipleBalance decimal(16,4),
OutstandingInterest decimal(16,4),
Regular decimal(16,4),
Arrears decimal(16,4),
Total decimal(16,4)
)

go
go



create table [dbo].[working_ld_financialInvoices](
ID int identity(1,1) primary key,
Lot_ID int,
InvoiceDate datetime,
InvoiceNo int,
InvoiceAmount money,
GST money,
PST money,
TotalInvoiceAmount money,
TotalCostsRecognized money,
MarginDollar money,
MarginPercent money,
Type varchar(50),
PurchaserName varchar(50),
AgreementNum int,
BillQuantity money,
Block varchar(5),
Lot varchar(5),
PlanNum varchar(12),
HoldbackAmount money,
Homebuilder varchar(50),
MISCCharges money)


go

alter table  hr_cntl add DaysPrior int
go

CREATE TABLE [dbo].[alert_emp_certificate_reminder](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cer_id] [int] NULL,
	[AAP_ID] [int] NULL,
	[Alert_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
go

CREATE TABLE [dbo].[alert_fa_preventative_maintenance_reminder](    -- drop table alert_fa_preventative_maintenance_reminder
	[id] [int] IDENTITY(1,1) NOT NULL,  
	[PM_Asset_Schedule_ID] [int] NULL,
	[AAP_ID] [int] NULL,
	[Alert_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
go
go
alter table LD_Shares alter column SHARE_PCT decimal(16,9)
go
ALTER TABLE AR_REPORT_INVOICE_HEADER ADD
	PROJ_CO int NULL
GO

create table WM_ContractSetup(
	id int not null identity(1,1) primary key,
	ContractTypeCode varchar(20) not null,
	ContractTypeDescription varchar(100) not null)
go
create table WM_PermitSetup(
	id int not null identity(1,1) primary key,
	PermitTypeCode varchar(20) not null,
	PermitTypeDescription varchar(100) not null)
go

alter table working_CostRevenueResults add COCode int

go
alter table Report_AR_Invoice_SO_TRN_HDR_Proj_Info add 
	EngineerSupplierName varchar(40)
go
go

alter table WM_ContractSetup add NextNumber int

go

alter table WM_ContractSetup add MinNumber int

go

alter table WM_ContractSetup add MaxNumber int

go

alter table WM_PermitSetup add NextNumber int

go

alter table WM_PermitSetup add MinNumber int

go

alter table WM_PermitSetup add MaxNumber int

go

create table WM_PermitHeader(
ID int identity(1,1) primary key,
CustomerID int,
WM_PermitSetup_ID int,
PermitNumber varchar(100),
StartDate datetime,
EndDate datetime,
IssuedTo int,
ProjectLocation varchar(100),
IncludeWeighTickets bit,
IncludeTonnageReport bit,
HaulingLoadApplicable bit,
JobRequired bit)

go

create table WM_PermitDetail(
ID int identity(1,1) primary key,
WM_PermitHeader_ID int,
WM_WasteSetup_ID int,
WM_FeeType_ID int,
WM_RevenueUOMControlCodes_ID int,
FeeAmount decimal(16,4),
VolumePricingLower decimal(16,4),
VolumePricingUpper decimal(16,4),
RevenueGL varchar(21),
EstimatedQuantity decimal(16,4),
ActualQuantity decimal(16,4),
LastQtyResetDate datetime,
LastQtyReset decimal(16,4),
StartDate datetime,
EndDate datetime,
Hauling bit,
Handling bit,
Fuel bit,
Disposal bit
)

go

create table WM_PermitJob(
ID int identity(1,1) primary key,
WM_PermitHeader_ID int,
JobName varchar(180),
BillTo_CustomerID int
)

go

ALTER TABLE Report_AR_Invoice_SO_TRN_HDR_Proj_Info ADD
	EngineerAddress1 varchar(40),
	EngineerAddress2 varchar(40),
	EngineerCity varchar(20),
	EngineerProv varchar(15),
	EngineerPostal varchar(10),
	TotalHoldbacksToDate money,
	LessPreviousHoldbacksReleased money
GO
alter table gl_report_options add 
	FromTransactionDate datetime,
	ToTransactionDate datetime
go
alter table gl_report_options add
	UseTransactionDateRange bit not null default 0
go
	alter table WS_PCDL_LogHeader add TimeStamp datetime
go
	alter table WS_PCDL_LogHeader add IsFLEMSheet bit
go
	alter table WS_PCDL_LogHeader add LemNum varchar(20)
go
	alter table WS_PCDL_LogHeader add ApprovalComments varchar(max)
go
	alter table WS_PCDL_LogHeader add MatchId int
go
	alter table WS_PCDL_LogHeader add BillAmount decimal(19,4)
go 
	alter table WS_PCDL_LogHeader add LEMApproved bit
go
	alter table WS_PCDL_LogHeader add Description varchar(max)
go
	alter table WS_EMP_TimeClock add Billable bit
go
	alter table WS_EMP_TimeClock add TimeStamp datetime
go
	alter table WS_EMP_TimeClock add eqi_num varchar(10)
go
	alter table WS_EMP_TimeClock add Quantity decimal(14,4)
go
	alter table WS_EMP_TimeClock add BillCycle char(1)
go
	alter table WS_PCDL_LogEntry add eqi_num varchar(10)
go
	alter table WS_EMP_TimeClock add EstId int
go
	alter table WS_EMP_TimeClock add MatchId int
go
	alter table WS_EMP_TimeClockHours add BillAmt money
go
	alter table WS_EMP_TimeClock add
		Manual bit, 
		IncludedHours decimal(19, 4) 
go
	alter table PO_HEADER add MatchId int
go
	alter table PO_DETAIL add MatchId int
go
	alter table emp_time_equip add BillCycle char(1)
go
	alter table emp_time_equip_hist add BillCycle char(1)
go
	alter table emp_time_equip_hist add TimeClock_ID int
go
	alter table po_setup add FieldPOBuyer varchar(6)
go
	create table WS_PCDL_LEM_AP (
		LEM_AP_Id int identity(1,1) primary key, 
		WS_PCDL_LogHeaderId int, 
		ap_gl_alloc_id int, 
		Amount decimal(19,4),
		MarkupPct decimal(19,4),
		MarkupAmt decimal(19,4),
		BillAmt decimal(19,4)
	)

go

	if exists(select * from WS_MenuItemCtrl where ItemID = 48)
		update WS_MenuItemCtrl set Item_Desc='LEM Approval', Item_FunctionCode='S_CL_LEMApproval', Item_PageDesc='Customer Portal > LEM Approval' where ItemID = 48
	else
		insert into WS_MenuItemCtrl(ItemID, Item_Desc, Item_FunctionCode, Item_PageDesc) select 48, 'LEM Approval', 'S_CL_LEMApproval', 'Customer Portal > LEM Approval'
	
go

	if exists(select * from WS_MenuItemCtrl where ItemID = 49)
		update WS_MenuItemCtrl set Item_Desc='LEM History', Item_FunctionCode='S_CL_LEMHistory', Item_PageDesc='Customer Portal > LEM History' where ItemID = 49
	else
		insert into WS_MenuItemCtrl(ItemID, Item_Desc, Item_FunctionCode, Item_PageDesc) select 49, 'LEM History', 'S_CL_LEMHistory', 'Customer Portal > LEM History'
	
go

	if not exists(select * from WS_PCDL_LogType where Description = 'Equipment')
	begin
		insert WS_PCDL_LogType(DL_LogType_ID, Description, Seq, Active)	
		select 7, 'Equipment Rental', 7, 0
	end 
	else
	begin
		update WS_PCDL_LogType
		set Description = 'Equipment Rental', Seq = 7
		where DL_LogType_ID = 7
	end

go
	
	if not exists(select * from WS_PCDL_LogType where Description = 'Time Entry')
	begin
		insert WS_PCDL_LogType(DL_LogType_ID, Description, Seq, Active)	
		select 8, 'Time Entry', 8, 0
	end 

go

	if not exists(select * from WS_PCDL_LogType where DL_LogType_ID = 9)
	begin
		insert into WS_PCDL_LogType (DL_LogType_ID, Description, Seq, Active)
		select 9, 'Equipment', 9, 0
	end
	else
	begin
		update WS_PCDL_LogType
		set Description = 'Equipment', Seq = 9
		where DL_LogType_ID = 9
	end

go

	if not exists( select * from SecurityOverride where id = 12 ) 
	begin
		insert into SecurityOverride (ID, Description, Active)
		select 12, 'Field Services', 0
	end

go

	if not exists( select * from SecurityOverrideDetail where id = 17 ) 
	begin
		insert into SecurityOverrideDetail (ID, SecurityOverrideID, SecurityFunctionID)
		select 17, 12, 21773 -- security id for Field Services
	end

go

	alter table ap_inv_header add NonProjPO_Pri_ID int

go

	create table WS_FLEM_HeaderSess (
		id int primary key identity(1,1),
		session_id int,
		active bit,
		WS_PCDL_LH_ID int,
		Date datetime,
		LEMNum varchar(50),
		pri_id int,
		ProjNum int,
		Project varchar(50),
		SiteLocation varchar(50),
		ProjPORef varchar(50),
		Amount decimal(19,4),
		BillingStatus varchar(15)
	)

go

	create table WS_FLEM_EquipSess (
		id int primary key identity(1,1),
		session_id int,
		active bit,
		summary bit,
		WS_PCDL_LH_ID int,
		WS_ETC_ID int,		
		EquipmentClass varchar(30),		
		ChangeOrder varchar(25),
		Level1 varchar(50),
		Level2 varchar(50),
		Level3 varchar(50),
		Level4 varchar(50),
		Equipment varchar(100),
		AssetCode varchar(15),
		BillingCycle varchar(50),
		Quantity decimal(14,4),
		Rate decimal(19,4),
		BillAmount decimal(19,4)
	)

go

	create table WS_FLEM_APSess (
		id int primary key identity(1,1),
		session_id int,
		active bit,
		summary bit,
		WS_PCDL_LH_ID int,				
		Level1 varchar(50),
		Level2 varchar(50),
		Level3 varchar(50),
		Level4 varchar(50),
		PO varchar(20),
		Supplier varchar(40),
		InvoiceNo varchar(15),
		Description varchar(150),
		Component varchar(15),
		Amount decimal(19,4)
	)
	
go

	create table WS_FLEM_LabSess (
		id int primary key identity(1,1),
		session_id int,
		active bit,
		summary bit,
		WS_PCDL_LH_ID int,
		WS_ETC_ID int,
		WorkClass varchar(30),
		ChangeOrder varchar(25),
		Level1 varchar(50),
		Level2 varchar(50),
		Level3 varchar(50),
		Level4 varchar(50),
		Employee varchar(75),
		TotalHrs decimal(19,4),
		BillAmount decimal(19,4),
		WTC1_Hrs decimal(19,4),
		WTC1_Rate decimal(19,4),
		WTC2_Hrs decimal(19,4),
		WTC2_Rate decimal(19,4),
		WTC3_Hrs decimal(19,4),
		WTC3_Rate decimal(19,4),
		WTC4_Hrs decimal(19,4),
		WTC4_Rate decimal(19,4),
		WTC5_Hrs decimal(19,4),
		WTC5_Rate decimal(19,4),
		WTC6_Hrs decimal(19,4),
		WTC6_Rate decimal(19,4),
		WTC7_Hrs decimal(19,4),
		WTC7_Rate decimal(19,4),
		WTC8_Hrs decimal(19,4),
		WTC8_Rate decimal(19,4),
		WTC9_Hrs decimal(19,4),
		WTC9_Rate decimal(19,4),
		WTC10_Hrs decimal(19,4),
		WTC10_Rate decimal(19,4),
		WTC11_Hrs decimal(19,4),
		WTC11_Rate decimal(19,4),
		WTC12_Hrs decimal(19,4),
		WTC12_Rate decimal(19,4),
		WTC13_Hrs decimal(19,4),
		WTC13_Rate decimal(19,4),
		WTC14_Hrs decimal(19,4),
		WTC14_Rate decimal(19,4),
		WTC15_Hrs decimal(19,4),
		WTC15_Rate decimal(19,4),
		WTC16_Hrs decimal(19,4),
		WTC16_Rate decimal(19,4),
		WTC17_Hrs decimal(19,4),
		WTC17_Rate decimal(19,4),
		WTC18_Hrs decimal(19,4),
		WTC18_Rate decimal(19,4),
		WTC19_Hrs decimal(19,4),
		WTC19_Rate decimal(19,4),
		WTC20_Hrs decimal(19,4),
		WTC20_Rate decimal(19,4)
	)

go

	alter table WS_FLEM_LabSess add
		emp_no int,
		Work_Class_ID int

go

	create table PC_FLEM_SheetSel_working (
		id int identity(1,1) primary key,
		username varchar(50),
		selected bit,
		LEM_ID int,
		LemNum varchar(20),
		LemDate datetime,
		pri_id int,
		ProjectNum int,
		Project varchar(50),
		CustomerCode varchar(10),
		Customer varchar(40),
		CreatedBy varchar(50),
		BillAmount decimal(19,4)
	)

go

	create table PC_FLEM_Lab_working (
		id int primary key identity(1,1),
		username varchar(50),
		LEM_ID int,
		WS_ETC_ID int,
		WorkClass varchar(30),
		ChangeOrder varchar(25),
		Level1 varchar(50),
		Level2 varchar(50),
		Level3 varchar(50),
		Level4 varchar(50),
		Employee varchar(75),
		TotalHrs decimal(19,4),
		BillAmount decimal(19,4),
		Billable bit,
		WTC1_Hrs decimal(19,4),
		WTC1_Rate decimal(19,4),
		WTC2_Hrs decimal(19,4),
		WTC2_Rate decimal(19,4),
		WTC3_Hrs decimal(19,4),
		WTC3_Rate decimal(19,4),
		WTC4_Hrs decimal(19,4),
		WTC4_Rate decimal(19,4),
		WTC5_Hrs decimal(19,4),
		WTC5_Rate decimal(19,4),
		WTC6_Hrs decimal(19,4),
		WTC6_Rate decimal(19,4),
		WTC7_Hrs decimal(19,4),
		WTC7_Rate decimal(19,4),
		WTC8_Hrs decimal(19,4),
		WTC8_Rate decimal(19,4),
		WTC9_Hrs decimal(19,4),
		WTC9_Rate decimal(19,4),
		WTC10_Hrs decimal(19,4),
		WTC10_Rate decimal(19,4),
		WTC11_Hrs decimal(19,4),
		WTC11_Rate decimal(19,4),
		WTC12_Hrs decimal(19,4),
		WTC12_Rate decimal(19,4),
		WTC13_Hrs decimal(19,4),
		WTC13_Rate decimal(19,4),
		WTC14_Hrs decimal(19,4),
		WTC14_Rate decimal(19,4),
		WTC15_Hrs decimal(19,4),
		WTC15_Rate decimal(19,4),
		WTC16_Hrs decimal(19,4),
		WTC16_Rate decimal(19,4),
		WTC17_Hrs decimal(19,4),
		WTC17_Rate decimal(19,4),
		WTC18_Hrs decimal(19,4),
		WTC18_Rate decimal(19,4),
		WTC19_Hrs decimal(19,4),
		WTC19_Rate decimal(19,4),
		WTC20_Hrs decimal(19,4),
		WTC20_Rate decimal(19,4)
	)

go

	alter table PC_FLEM_Lab_working add
		emp_no int,
		Work_Class_ID int

go

	create table PC_FLEM_Equip_working (
		id int primary key identity(1,1),
		username varchar(50),
		LEM_ID int,
		WS_ETC_ID int,		
		EquipmentClass varchar(30),		
		ChangeOrder varchar(25),
		Level1 varchar(50),
		Level2 varchar(50),
		Level3 varchar(50),
		Level4 varchar(50),
		Equipment varchar(100),
		AssetCode varchar(15),
		BillingCycle varchar(50),
		Quantity decimal(14,4),
		Rate decimal(19,4),
		BillAmount decimal(19,4),
		Billable bit
	)

go

	create table PC_FLEM_AP_working (
		id int primary key identity(1,1),
		username varchar(50),
		LEM_ID int,		
		ap_gl_alloc_id int,	
		Level1 varchar(50),
		Level2 varchar(50),
		Level3 varchar(50),
		Level4 varchar(50),
		PO varchar(20),
		Supplier varchar(40),
		InvoiceNo varchar(15),
		Description varchar(150),
		Component varchar(15),
		Amount decimal(19,4)
	)

go

	alter table costing_TimeTicket add WS_PCDL_LH_ID int

go

	alter table emp_time_equip add TimeClock_ID int

go

	create table PC_FLEM_SheetQuery_working (
		id int identity(1,1) primary key,
		username varchar(50),
		LEM_ID int,
		LemNum varchar(20),
		LemDate datetime,
		pri_id int,
		ProjectNum int,
		ProjectName varchar(50),
		CustomerCode varchar(10),
		CustomerName varchar(40),
		SiteLocation varchar(50),
		ProjPORef varchar(50),
		LEMTotal money,
		BillingStatus varchar(15),
		ProjectManager varchar(50),
		EnteredBy varchar(50),
		InvoiceNos varchar(500),
		InvoiceDates varchar(500),
		LEMInvoiceAmount money
	)

go

	alter table PC_FLEM_SheetQuery_working add LEMStatus varchar(10)

go

	alter table working_TT_Bill_Release add WS_PCDL_LH_ID int

go

	create table PC_TT_Lab_working (
		id int primary key identity(1,1),
		username varchar(50),
		LEM_ID int,
		WS_ETC_ID int,
		WorkClass varchar(30),
		ChangeOrder varchar(25),
		Level1 varchar(50),
		Level2 varchar(50),
		Level3 varchar(50),
		Level4 varchar(50),
		Employee varchar(75),
		TotalHrs decimal(19,4),
		BillAmount decimal(19,4),
		Billable bit,
		WTC1_Hrs decimal(19,4),
		WTC1_Rate decimal(19,4),
		WTC2_Hrs decimal(19,4),
		WTC2_Rate decimal(19,4),
		WTC3_Hrs decimal(19,4),
		WTC3_Rate decimal(19,4),
		WTC4_Hrs decimal(19,4),
		WTC4_Rate decimal(19,4),
		WTC5_Hrs decimal(19,4),
		WTC5_Rate decimal(19,4),
		WTC6_Hrs decimal(19,4),
		WTC6_Rate decimal(19,4),
		WTC7_Hrs decimal(19,4),
		WTC7_Rate decimal(19,4),
		WTC8_Hrs decimal(19,4),
		WTC8_Rate decimal(19,4),
		WTC9_Hrs decimal(19,4),
		WTC9_Rate decimal(19,4),
		WTC10_Hrs decimal(19,4),
		WTC10_Rate decimal(19,4),
		WTC11_Hrs decimal(19,4),
		WTC11_Rate decimal(19,4),
		WTC12_Hrs decimal(19,4),
		WTC12_Rate decimal(19,4),
		WTC13_Hrs decimal(19,4),
		WTC13_Rate decimal(19,4),
		WTC14_Hrs decimal(19,4),
		WTC14_Rate decimal(19,4),
		WTC15_Hrs decimal(19,4),
		WTC15_Rate decimal(19,4),
		WTC16_Hrs decimal(19,4),
		WTC16_Rate decimal(19,4),
		WTC17_Hrs decimal(19,4),
		WTC17_Rate decimal(19,4),
		WTC18_Hrs decimal(19,4),
		WTC18_Rate decimal(19,4),
		WTC19_Hrs decimal(19,4),
		WTC19_Rate decimal(19,4),
		WTC20_Hrs decimal(19,4),
		WTC20_Rate decimal(19,4)
	)

go

	alter table PC_TT_Lab_working add
		emp_no int,
		Work_Class_ID int

go

	create table PC_TT_Equip_working (
		id int primary key identity(1,1),
		username varchar(50),
		LEM_ID int,
		WS_ETC_ID int,		
		EquipmentClass varchar(30),		
		ChangeOrder varchar(25),
		Level1 varchar(50),
		Level2 varchar(50),
		Level3 varchar(50),
		Level4 varchar(50),
		Equipment varchar(100),
		AssetCode varchar(15),
		BillingCycle varchar(50),
		Quantity decimal(14,4),
		Rate decimal(19,4),
		BillAmount decimal(19,4),
		Billable bit
	)

go

	create table PC_TT_AP_working (
		id int primary key identity(1,1),
		username varchar(50),
		LEM_ID int,		
		ap_gl_alloc_id int,	
		Level1 varchar(50),
		Level2 varchar(50),
		Level3 varchar(50),
		Level4 varchar(50),
		PO varchar(20),
		Supplier varchar(40),
		InvoiceNo varchar(15),
		Description varchar(150),
		Component varchar(15),
		Amount decimal(19,4),
		MarkupPct decimal(19,4),
		MarkupAmt decimal(19,4),
		BillAmt decimal(19,4)
	)

go



CREATE TABLE PC_FLEM_SheetQuery_Employee_Working(
	id int IDENTITY(1,1) primary key,
	username varchar(50),
	LEM_ID int,
	WS_ETC_ID int,
	WorkClass varchar(30),
	ChangeOrder varchar(25),
	CostingReference varchar(150),
	EmployeeNum int,
	Employee varchar(75) ,
	TotalHrs decimal(19, 4) ,
	BillAmount decimal(19, 4) ,
	Billable bit ,
	WTC1_Hrs decimal(19, 4) ,
	WTC1_Rate decimal(19, 4) ,
	WTC2_Hrs decimal(19, 4) ,
	WTC2_Rate decimal(19, 4) ,
	WTC3_Hrs decimal(19, 4) ,
	WTC3_Rate decimal(19, 4) ,
	WTC4_Hrs decimal(19, 4) ,
	WTC4_Rate decimal(19, 4) ,
	WTC5_Hrs decimal(19, 4) ,
	WTC5_Rate decimal(19, 4) ,
	WTC6_Hrs decimal(19, 4) ,
	WTC6_Rate decimal(19, 4) ,
	WTC7_Hrs decimal(19, 4) ,
	WTC7_Rate decimal(19, 4) ,
	WTC8_Hrs decimal(19, 4) ,
	WTC8_Rate decimal(19, 4) ,
	WTC9_Hrs decimal(19, 4) ,
	WTC9_Rate decimal(19, 4) ,
	WTC10_Hrs decimal(19, 4) ,
	WTC10_Rate decimal(19, 4) ,
	WTC11_Hrs decimal(19, 4) ,
	WTC11_Rate decimal(19, 4) ,
	WTC12_Hrs decimal(19, 4) ,
	WTC12_Rate decimal(19, 4) ,
	WTC13_Hrs decimal(19, 4) ,
	WTC13_Rate decimal(19, 4) ,
	WTC14_Hrs decimal(19, 4) ,
	WTC14_Rate decimal(19, 4) ,
	WTC15_Hrs decimal(19, 4) ,
	WTC15_Rate decimal(19, 4) ,
	WTC16_Hrs decimal(19, 4) ,
	WTC16_Rate decimal(19, 4) ,
	WTC17_Hrs decimal(19, 4) ,
	WTC17_Rate decimal(19, 4) ,
	WTC18_Hrs decimal(19, 4) ,
	WTC18_Rate decimal(19, 4) ,
	WTC19_Hrs decimal(19, 4) ,
	WTC19_Rate decimal(19, 4) ,
	WTC20_Hrs decimal(19, 4) ,
	WTC20_Rate decimal(19, 4),
	Date datetime 
)

go

	alter table PC_FLEM_SheetQuery_Employee_Working add
		emp_no int,
		Work_Class_ID int

go

CREATE TABLE PC_FLEM_SheetQuery_Equip_working(
	id int IDENTITY(1,1) primary key ,
	username varchar(50) ,
	LEM_ID int ,
	WS_ETC_ID int ,
	EmployeeNum int,
	Employee varchar(75) ,
	EquipmentClass varchar(30) ,
	ChangeOrder varchar(25) ,
	CostingReference varchar(150),
	AssetDescription varchar(100) ,
	AssetCode varchar(15) ,
	BillingCycle varchar(50) ,
	Quantity decimal(14, 4) ,
	Rate decimal(19, 4) ,
	BillAmount decimal(19, 4) ,
	Billable bit )

go

CREATE TABLE dbo.PC_FLEM_SheetQuery_AP_working(
	id int IDENTITY(1,1) primary key ,
	username varchar(50) ,
	LEM_ID int ,
	ap_gl_alloc_id int ,
	CostingReference varchar(150),
	PO varchar(20) ,
	Supplier varchar(40) ,
	InvoiceNo varchar(15) ,
	Description varchar(150) ,
	Component varchar(15) ,
	Amount decimal(19, 4) )

go


create table working_wo_BidType(
username varchar(50),
EST_Bid_Type_ID int
)

go
create table working_wo_Revenue_Type(
username varchar(50),
Revenue_Types_ID int
)

go

create table Working_WO_Profit(
username varchar(30),
segment_value varchar(6))

go

	alter table unit_time_measurement add hrly_equiv int

go

	if exists( select * from unit_time_measurement where TimeCode = 'H' and hrly_equiv is null )
	begin
		update unit_time_measurement set hrly_equiv=1 where TimeCode = 'H'
	end

go

	if exists( select * from unit_time_measurement where TimeCode = 'D' and hrly_equiv is null )
	begin
		update unit_time_measurement set hrly_equiv=1 where TimeCode = 'D'
	end

go

	if exists( select * from unit_time_measurement where TimeCode = 'W' and hrly_equiv is null )
	begin
		update unit_time_measurement set hrly_equiv=1 where TimeCode = 'W'
	end

go

	if exists( select * from unit_time_measurement where TimeCode = 'M' and hrly_equiv is null )
	begin
		update unit_time_measurement set hrly_equiv=1 where TimeCode = 'M'
	end

go
alter table equip_assign add id int identity
go
alter table WORKING_HOLD_INVOICES add Customer_File_Num varchar(50)
go
alter table WORKING_HOLD_INVOICES add InternalReference varchar(50)
go
alter table WORKING_HOLD_INVOICES add AFE_NO varchar(20)
go
	alter table SO_TRN_DETAIL_PROJ add temp_id int
go
	alter table emp_time_costing_hist add rte_te_id int
go
	alter table emp_time_costing_hist add cost_reference varchar(150)
go
	alter table WS_EMP_Time_Code add ReportTypeColumn varchar(20)
/*
Regular
Overtime
Travel
LOA
Equipment
Other
Double Time
*/
go
	alter table working_req_po_detail add billable varchar(1)
go
alter table proj_cntl add
	AllowCustomerChange bit not null default 0
go
alter table working_PC_Billing_Summary add InternalReference varchar(50)
go
alter table so_trn_hdr_proj add InternalReference varchar(50)
go
alter table WORKING_PC_INV_REG add InternalReference varchar(50)
go
alter table Report_AR_Invoice_SO_TRN_HDR add InternalReference varchar(50)
go
alter table Report_AR_Invoice_SO_TRN_HDR_Proj_Info add 
	InternalReference varchar(50),
	ProjectExtendedDescription varchar(max),--pri_desc
	EngineerStakeholder varchar(101),
	EngineerStakeholderPosition varchar(128)
go
alter table working_ar_payments add InternalReference varchar(50)
go
alter table working_ar_hb_payments add InternalReference varchar(50)
go
alter table AR_REPORT_INVOICE_HEADER add InternalReference varchar(50)
go
CREATE TABLE [working_CostRevenueResults](
	[id] [int] IDENTITY(1,1) NOT NULL primary key,
	[pri_id] [int] NULL,
	[Project] [varchar](50) NULL,
	[ProjectCode] [int] NULL,
	[Type] [char](1) NULL,
	[ARInvoiceDate] [datetime] NULL,
	[ARInvoiceNumber] [int] NULL,
	[ARInternalReference] [varchar](50) NULL,
	[ARRevenue] [money] NULL,
	[CostLEMComponent] [varchar](1) NULL,
	[CostModule] [varchar](20) NULL,
	[CostCodeLvl1] [varchar](6) NULL,
	[CostCodeDescriptionLvl1] [varchar](30) NULL,
	[CostCodeLvl2] [varchar](6) NULL,
	[CostCodeDescriptionLvl2] [varchar](30) NULL,
	[CostCodeLvl3] [varchar](6) NULL,
	[CostCodeDescriptionLvl3] [varchar](30) NULL,
	[CostCodeLvl4] [varchar](6) NULL,
	[CostCodeDescriptionLvl4] [varchar](30) NULL,
	[CostInvoiceNumber] [varchar](15) NULL,
	[CostDescription] [varchar](max) NULL,
	[CostAssetCode] [varchar](10) NULL,
	[CostAssetDescription] [varchar](max) NULL,
	[CostPart] [varchar](30) NULL,
	[CostPartDescription] [varchar](max) NULL,
	[CostDate] [datetime] NULL,
	[CostTotal] [money] NULL,
	[CompanyName] [varchar](50) NULL,
	[RunDescription] [varchar](50) NULL,
	[SuppressZeroValues] [varchar](10) NULL,
	[AsAt] [varchar](10) NULL,
	[FromYear] [int] NULL,
	[FromPeriod] [int] NULL,
	[ToYear] [int] NULL,
	[ToPeriod] [int] NULL,
	[SummaryType] [char](1) NULL,
	[Username] [varchar](10) NULL) 
go
CREATE TABLE [working_CostRevenueEmployeeResults](
	ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[pri_id] [int] NOT NULL,
	[emp_no] [int] NOT NULL,
	[emp_first_name] [varchar](30) NULL,
	[Emp_Last_name] [varchar](30) NULL,
	[Username] [varchar](10) NULL) 
GO

	alter table costing_budget add cost_code varchar(15)

go

	create table PROJ_FLEMDefaults (
		FLEMDef_ID int identity(1,1) primary key,
		PRI_ID int,
		Lv1ID int,
		Lv2ID int,
		Lv3ID int,
		Lv4ID int,
		Type varchar(1)
	)

go

	alter table proj_header add 
		AlertWorkDaySuc int,
		AlertXDayPrior int

go

	CREATE TABLE [dbo].[AlertEmployeeDate](
       [ID] [int] IDENTITY(1,1) primary key NOT NULL,
       [ProjectId] [int] NOT NULL,
       [EmpNum] [int] NOT NULL,
       [AlertedDate] [datetime] NOT NULL
	)	
	
go
	alter table PROJ_HEADER add PurchaseDate datetime
go
	
	alter table WS_EMP_TimeClock add CO_Pri_Id int

go

	ALTER TABLE Report_AR_Invoice_SO_TRN_HDR_Proj_Info ADD
		EngineerAddress1 varchar(40),
		EngineerAddress2 varchar(40),
		EngineerCity varchar(20),
		EngineerProv varchar(15),
		EngineerPostal varchar(10),
		TotalHoldbacksToDate money,
		LessPreviousHoldbacksReleased money

go
go
alter table working_PODetailPrintX add
	lv1_code varchar(6),
	lv1_desc varchar(30),
	lv2_code varchar(6),
	lv2_desc varchar(30),
	lv3_code varchar(6),
	lv3_desc varchar(30),
	lv4_code varchar(6),
	lv4_desc varchar(30)
go
alter table working_communication_email_fax_Attachments add
	username varchar(10)
go
alter table working_communication_email_fax_Attachments add
	DateAdded datetime
go
alter table working_communication_email_fax_Attachments add
	FileTypeDescription varchar(max),
	AddedAsAttachment bit not null default 0
go
alter table gl_report_options add 
	FromTransactionDate datetime,
	ToTransactionDate datetime
go
alter table gl_report_options add
	UseTransactionDateRange bit not null default 0
go

go

create table WM_Contract_Header    -- drop table WM_Contract_Header
(
	id int not null identity(1,1) primary key,
	Customer_ID int,
	Contract_Type_ID  int,
	Contract_Number  varchar(50),
	[Start_Date]  datetime,
	End_Date  datetime,
	Issued_To varchar(40),
	Project_Name varchar(100),
	Project_Location varchar(100),
	Minimum_Volume int ,  -- Number field no decimal places
    Include_Weigh_Tickets bit default 0,
	Include_Tonnage_Report  bit default 0,
	Hauling_Applicable  bit default 0,
	Job_Required bit default 0
	)
go


-- Job Setup

create table WM_Contract_Job   -- drop table WM_Contract_Job
(
	id int not null identity(1,1) primary key,
	Contract_Header_ID  int,
	Job_Name varchar(180),
	Bill_To_Customer_ID   int
	)
go


create table WM_Contract_Details    -- drop table WM_Contract_Details
(
	id int not null identity(1,1) primary key,
	Contract_Header_ID  int,
	Waste_Code_ID  int,
	Fee_Code_ID varchar(30)  null,
	WM_RevenueUOMControlCodes_ID int,
	Fee_Amount money null,
	Volume_Pricing_Lower money,
	Volume_Pricing_Upper money,
	Revenue_Acct varchar(21) null,
	Estimated_Quantity  decimal(19,2),
	Actual_Quantity  decimal(19,2),
	Reset_Date  datetime,
	Reset_Quantity  decimal(19,2),
	[Start_Date]  datetime,
	End_Date  datetime,
	Hauling bit,
	Handling bit,
	Disposal bit,
	Fuel bit

	)

	
go

create table WM_Volume_Pricing_Header    -- drop table WM_Volume_Pricing_Header
(
	id int not null identity(1,1) primary key,
	Customer_ID int,
	Volume_Pricing_Code varchar(5),
	Volume_Pricing_Desc varchar(10),
	[Start_Date]  datetime,
	End_Date  datetime,
	
	)
go

create table WM_Volume_Pricing_Detail    -- drop table WM_Volume_Pricing_Detail
(
	id int not null identity(1,1) primary key,
	Volume_Pricing_Header_ID int,
	Permit_Contract_Header_ID  int,
	Type varchar(1),
	Waste_Code_ID  int,
	Pricing_Cal_Type_ID int
	)
go

create table WM_Volume_Pricing    -- drop table WM_Volume_Pricing
(
	id int not null identity(1,1) primary key,
	Volume_Pricing_Detail_ID int,
	Volume_Quantity_Lower money,
	Volume_Quantity_Upper money,
	price money
	)
go

create table WM_Pricing_Cal_Type   -- drop table WM_Pricing_Cal_Type
(
	id int not null identity(1,1) primary key,
	Code varchar(1),
	[Type] varchar(30)
	
	)
go

if not exists (select * from WM_Pricing_Cal_Type where code ='A')
begin
    insert WM_Pricing_Cal_Type (Code, Type)
	select 'A', 'Actual to Date'
end
go
if not exists (select * from WM_Pricing_Cal_Type where code ='C')
begin
    insert WM_Pricing_Cal_Type (Code, Type)
	select 'C', 'Current Billing'
end

go

alter table WM_Contract_Header
alter column Issued_To int 

go


CREATE TABLE [dbo].[Working_WM_Cust](
	[username] [varchar](200) NULL,
	[customer_id] [int] NULL
)
go

CREATE TABLE [dbo].[Working_WM_Permit](
	[username] [varchar](200) NULL,
	[id] [int] NULL
)
go

CREATE TABLE [dbo].[Working_WM_Contract](
	[username] [varchar](200) NULL,
	[id] [int] NULL
)
go


alter table WM_FeeType 
add ChargeType varchar(10)  -- Handling, Hauling,Disposal, Fuel

go

alter table WM_Contract_Details add ChargeType varchar(10) -- Handling, Hauling,Disposal, Fuel
go
alter table WM_PermitDetail add ChargeType varchar(10) -- Handling, Hauling,Disposal, Fuel
go

alter table WM_PermitHeader add ProjectName varchar(100)
go

CREATE TABLE Working_WM_WasteCode   -- drop table Working_WM_WasteCode
	(
	username varchar(200),
	seq int  ,
	Waste_Code_ID  int,
	WasteCode varchar(20),
	WasteDescription varchar(100)
		)

go
alter table WM_Contract_Header
add Material_Audit bit default 0,
    SourceCode_ID  int

go
alter table WM_PermitHeader
add Material_Audit bit default 0,
    SourceCode_ID  int

go

create table WM_Setup    -- drop table WM_Setup
(
	id int not null identity(1,1) primary key,
	Next_Ticket_Num int,
	Min_Ticket_Num int, 
	Max_Ticket_Num int  
	     
	)
go

if not exists (select * from WM_Setup )
begin
	insert WM_Setup (Next_Ticket_Num, Min_Ticket_Num, Max_Ticket_Num)
	select 0,0,0
end
go
go
alter table equip_id  add Hauling_Applicable bit
go
go
if not exists(select * from sys.tables where name = 'MLConversions') and exists(select * from mluser where name like '% %')
begin
	select *
	into MLConversions
	from mluser where name like '% %'

	update mluser set 
	name = REPLACE(name, ' ', '_')
	where name like '% %'
end
else if exists(select * from sys.tables where name = 'MLConversions') and exists(select * from mluser where name like '% %')
begin
	insert MLConversions(Name, FULL_NAME, Department, ContactID)
	select Name, FULL_NAME, Department, ContactID
	from mluser	
	where name like '% %'

	update mluser set 
	name = REPLACE(name, ' ', '_')
	where name like '% %'
end
go
alter table PO_Setup add ContractPO_Subtype_ID int
go

go
	alter table po_setup add AllowProjContractPOEdit bit
go
ALTER TABLE burden_type ADD
	ap_only varchar(1) NULL
GO
update burden_type set ap_only = 'F' where ap_only is null
GO

go
	alter table po_setup add SubTypeSigThreshold decimal(19,4)
go

drop table WM_Volume_Pricing_Detail
go
drop table WM_Volume_Pricing
go

alter table WM_Volume_Pricing_Header add Waste_Code_ID int
go
alter table WM_Volume_Pricing_Header add Pricing_Cal_Type_ID int

go


create table WM_Volume_Pricing_ContractPermits(
id int identity(1,1) primary key,
WM_Volume_Pricing_Header_ID int,
ContractPermitHeader_ID int,
ContractPermit varchar(1)
)
go

create table WM_Volume_Pricing_Limits(
id int identity(1,1) primary key,
WM_Volume_Pricing_Header_ID int,
UpperLimit decimal(16,4),
Price decimal(16,4)
)













go
	alter table EST_ctrl add allowCOMod bit
go
	alter table EST_Header add coModified bit
go
	alter table EST_Bill_of_Build_Requests add status varchar(1)
go
	create table EST_COBudget (
		COBudgetId int primary key identity(1,1),
		Est_Header_ID int,
		lv1ID int null, 
		lv2ID int null, 
		lv3ID int null, 
		lv4ID int null, 
		LemComp varchar(1) null, 
		Budget money null,
		BudgetHrs money null
	)
go
	alter table WORKING_PC_INV_REG add 
		lastClaimNo int,
		nextClaimNo int
go
	alter table SO_TRN_HDR add ClaimNo int
go
	alter table PROJ_Contract add SuppressBill bit
go
	alter table working_PC_Billing_ContractDetails add SuppressBill bit
go
	alter table PROJ_Contract add Seq int
go
	alter table working_PC_Billing_ContractDetails add Seq int
go
	alter table ap_setup add WebForceInvValid bit
go
	alter table WS_PCPO_Sess_Hdr add po_id int
go

go
	alter table WM_SETUP add EnableWSBillingAssistant bit
go
create table WM_WeighScales(
	id int not null identity(1,1),
	ScaleCode varchar(5),
	ScaleDescription varchar(30),
	IPAddress varchar(45), --accomodate ipv6
	[Port] varchar(10),
	Active bit)
go
alter table WM_WeighScales
ADD  CONSTRAINT [PK_WM_WeighScales] PRIMARY KEY NONCLUSTERED 
(
	id
)  
go
go
CREATE TABLE [dbo].[Working_WM_Waste](
	[username] [varchar](200) NULL,
	[WasteSetup_ID] [int] NULL
)
go
CREATE TABLE [dbo].[Working_WM_CellNumber]
(
	[username] [varchar](200) NULL,
	[CellNumberSetup_ID] [int] NULL
)
go
CREATE TABLE [dbo].[Working_WM_Driver]
(
	[username] [varchar](200) NULL,
	[DriverName]  varchar(80)
)
go
CREATE TABLE [dbo].[Working_WM_Waste_Code](
	[username] [varchar](200) NULL,
	[WasteSetup_ID] [int] NULL
)
go
CREATE TABLE [dbo].[Working_WM_Waste](
	[username] [varchar](200) NULL,
	[WasteSetup_ID] [int] NULL
)
go

CREATE TABLE [dbo].[Working_WM_ProjectLocation]
(
	[username] [varchar](200) NULL,
	[ProjectLocation] [varchar](100) NULL
)
go

CREATE TABLE [dbo].[Working_WM_ProjectName]
(
	[username] [varchar](200) NULL,
	[ProjectName] [varchar](100) NULL
)
go

CREATE TABLE [dbo].[Working_WM_CustomertName]    -- drop table Working_WM_CustomertName
(
	[username] [varchar](200) NULL,
	[customer_id] [varchar](100) NULL
)
go

alter table Rental_Report_Options
add 	DateIn_From datetime, DateIn_To datetime,
DateOut_From datetime, DateOut_To datetime


go

CREATE TABLE [dbo].[Working_WM_IssuedTo]    -- drop table Working_WM_IssuedTo
(
	[username] [varchar](200) NULL,
	[IssuedTo] int
)
go

alter table Rental_Report_Options add Report_Year int

go

alter table WM_WeighScaleEntry_Log
add FieldType varchar(2),
PreviousValue_Date datetime,
CurrentValue_Date datetime
go

alter table Rental_Report_Options add EditedTickets varchar(1)
go
alter table Rental_Report_Options add VoidTickets varchar(1)
go
CREATE TABLE [dbo].[Working_WM_TicketNUm](
	[username] [varchar](200) NULL,
	[WeighScaleEntry_ID] [int] NULL
)
go
CREATE TABLE [dbo].[Working_WM_CompanyVehicle](
	[username] [varchar](200) NULL,
	[eqi_id] [int] NULL
)
go
CREATE TABLE [dbo].[Working_WM_CustomerVehicle](
	[username] [varchar](200) NULL,
	[CUST_Vehicle_ID] [int] NULL
)
go
CREATE TABLE [dbo].[Working_WM_Grouping](
	[username] [varchar](200) NULL,
	[GroupDescription] [varchar](100) NULL,
	[GroupCode] [varchar](50) NULL,
	[seq] [int] NULL
) ON [PRIMARY]


alter table Rental_Report_Options add Run_Description varchar(200)

go


go
create table WM_WeighScaleEntry(
ID int identity(1,1) primary key,
EntryStatus varchar(20),
CustomerID int,
CoVehicle_Equip_ID int,
CUST_Vehicle_ID int,
ContractPermitHeader_ID int,
ContractPermit varchar(1),
RadioNum varchar(20),
WM_LandCellNumber_ID int,
DriverName varchar(80),
WM_WasteSetup_ID int,
DateIn datetime,
GrossWeight decimal(16,4),
WM_SourceSetupScreen_ID int,
LocationCode varchar(5),
LocationDescription varchar(20),
FuelAmount decimal(16,4),
Notes varchar(150),
TrailerNumber varchar(20),
MaterialAuditSelection varchar(max),
TareWeight decimal(16,4),
DateOut datetime,
TicketNum int)
go

alter table WM_WeighScaleEntry add ContractPermitString varchar(40)
go
alter table WM_WeighScaleEntry add MaterialAuditSelectionCode varchar(max)
go
alter table WM_WeighScaleEntry add MaterialAuditSelectionDescription varchar(max)
go
alter table WM_WeighScaleEntry add WeightDirection varchar(20)--Incoming,Outgoing
go
alter table WM_WeighScaleEntry add ModifiedBy varchar(20)
go
alter table WM_WeighScaleEntry add ModifiedDate Datetime
go
alter table WM_WeighScaleEntry add ManualEntry bit 
go
alter table WM_WeighScaleEntry add VoidReason varchar(180)
go
alter table WM_WeighScaleEntry add Invoiced bit
go
create table WM_WeighScaleEntryMatAudit(
ID int identity(1,1) primary key,
WM_WeighScaleEntry_ID int,
WM_AuditedMaterials_ID int)
go
create table WM_WeighScaleEntry_Log   -- drop table WM_WeighScaleEntry_Log
(ID int identity(1,1) primary key,
WM_WeighScaleEntry_ID int,
FieldName varchar(200),
FieldCaption varchar(200),
ModifiedBy  varchar(200),
ModifiedDate   datetime,
ModifiedReason   varchar(500),
PreviousValue varchar(50),
PreviousDescription varchar(500),
CurrentValue varchar(50),
CurrentDescription varchar(500))
go
alter table WM_WeighScaleEntry add [Signature] varbinary(max)
go 
alter table WM_WeighScaleEntry add whse_id int
go
alter table warehouse add CurrentWeighTicketID int not null default -1--per warehouse, tracks the ticket id of the current tablet signature ticket
go
alter table WM_WeighScaleEntry_Log
add FieldType varchar(2),
PreviousValue_Date datetime,
CurrentValue_Date datetime
go
create table WM_WeighScaleErrorLog(
	id int not null identity(1,1),
	WM_WeighScaleID int,
	ErrorUser varchar(10),
	ErrorStamp datetime not null default getdate(),
	ErrorCodeLocation varchar(max),
	ErrorText varchar(max))
go
alter table WM_WeighScaleEntry 
add EditedTickets bit
go
alter table WM_WeighScaleEntry add
	CashUnitPrice money,
	CashExtraCharge money,
	CashSubTotal money,
	CashGST money,
	CashPST money,
	CashExtendedToal money,
	CashTotalPaid money
go
alter table SO_PAYMENT_METHOD add 
	WeighTicketID int
go
alter table WM_WeighScaleEntry add WM_ContractPermit_Job_ID int
go
alter table WM_WeighScaleEntry add WM_ContractPermit_Job_String varchar(40)
go
alter table WM_WeighScaleEntry add CashSalePaid char not null default 'F'
go
alter table WM_WeighScales add MaxErrorMessages int not null default 100
go
go
alter table WS_Setup add
	EnableWeeklyAutoOT bit not null default 0,
	WeeklyAutoOTCostCodeOverride bit not null default 0,
	AutoOTLevel1ID int,
	AutoOTLevel2ID int,
	AutoOTLevel3ID int,
	AutoOTLevel4ID int
go
alter table PY_GetProjectWebTimeEntryData add OTHours money
go
CREATE TABLE [working_OTDetail](
	[id] [int] IDENTITY(1,1) NOT NULL primary key,
	[Origin] [varchar](25) NULL,
	[RecordStatus] [varchar](20) NULL,
	[CodeOrColumn] [varchar](10) NULL,
	[IsEditable] [bit] NULL,
	[IncludeInWeeklyOTHours] [bit] NULL,
	[WeekNumber] [int] NULL,
	[TimeDate] [datetime] NULL,
	[HoursWorked] [money] NULL,
	[WeeklyTotal] [money] NULL,
	[DailyOvertime] [money] NULL,
	[WeeklyOvertime] [money] NULL,
	[EditTable] [varchar](50) NULL,
	[EC_Code] [varchar](10) NULL,
	[EG_Code] [varchar](10) NULL,
	[EC_Desc] [varchar](100) NULL,
	[EG_Desc] [varchar](100) NULL,
	[TimeClock_ID] [int] NULL,
	[et_id] [int] NULL,
	[emp_time_id] [int] NULL,
	[Emp_NO] [int] NULL,
	[WebCode] [varchar](50) NULL,	
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[EarliestDate] [datetime] NULL,
	[LatestDate] [datetime] NULL,
	[StartOfTheWorkWeek] [datetime] NULL,
	[EndOfTheLatestWorkWeek] [datetime] NULL,	
	[RegularGroup] [varchar](10) NULL,
	[RegularCode] [varchar](10) NULL,
	[TravelGroup] [varchar](10) NULL,
	[TravelCode] [varchar](10) NULL,
	[OTGroup] [varchar](10) NULL,
	[OTCode] [varchar](10) NULL,
	[DTGroup] [varchar](10) NULL,
	[DTCode] [varchar](10) NULL,
	[OTDaily] [money] NULL,
	[OTWeekly] [money] NULL,
	[WorkingTableColumnForRegular] [int] NULL,
	[Username] [varchar](10) NULL
) 
GO
alter table working_OTDetail add
	RegHoursAfterDistribution money,
	OTHoursAfterDistribution money
go
alter table working_OTDetail add
	SumOfDailyHours money
go
alter table working_OTDetail add
	SumOfPreviouslyCapturedOT money
go
alter table working_OTDetail add
	OTToDistribute money
go
alter table working_OTDetail add
	OTDistributionMethod varchar(100)
go
alter table working_OTDetail add Updated bit not null default 0
go
alter table PY_GetProjectWebTimeEntryData add Updated bit not null default 0
go
alter table PY_GetProjectWebTimeEntryData add WorkingTableColumnForRegular int
go
alter table [working_OTDetail] add 
	IsOT bit,
	Pri_id int,
	Level1ID int,
	Level2ID int,
	Level3ID int,
	Level4ID int
go
alter table PO_Setup add ContractPO_Subtype_ID int
go
alter table working_OTDetail add OT_Limit_Description varchar(30)
go
go

alter table so_trn_hdr_proj add WM_ContractPermit_ID int
go

alter table so_trn_hdr_proj add WM_ContractPermitType varchar(1)

go

alter table WM_WeighScaleEntry add SO_TRN_HDR_PROJ_ID int
go

Create table WM_Billing_Release_Details(
ID int identity(1,1) primary key,
SO_TRN_HDR_PROJ_ID int,
SO_TRN_DET_PROJ_ID int,
WM_FeeType_ID int,
WM_RevenueUOMControlCodes_ID int,/*Fee Based On*/
Quantity decimal(16,4),
Price decimal(16,4),
VolumePricing bit,
VolumePricingLevel varchar(100)
)

go
alter table WM_Billing_Release_Details add SourceID int
go
alter table WM_Billing_Release_Details add SourceType varchar(1)
go

create table Working_WM_ContractPermitSelection(
ID int identity(1,1) primary key,
username varchar(50),
selected bit,
customer_id int,
CustomerCode varchar(10),
CustomerName varchar(40),
ContractPermit varchar(15),
ContractPermit_ID int,
ContractPermitNumber varchar(50),
StartDate datetime,
EndDate datetime,
IssuedTo varchar(40),
ProjectName varchar(100),
PermitTypeDesc varchar(100),
ContractTypeDesc varchar(100),
Release bit,
TotalProducts decimal(16,4),
TotalMisc decimal(16,4),
TotalGST decimal(16,4),
TotalPST decimal(16,4),
TotalInvoice decimal(16,4)
)
go

create table working_WM_WeighScaleProcessing(
username varchar(50),
WM_WeighScaleEntry_ID int,
Working_WM_ContractPermitSelection_ID int
)
go

create table working_WM_ContractPermitWasteCode(
ID int identity(1,1) primary key,
username varchar(50),
Working_WM_ContractPermitSelection_ID int,
Waste_Code_ID int,
WasteCode varchar(20),
WasteDescription varchar(100),
CurrentQuantity Decimal(16,4),
QuantityToDate Decimal(16,4)
)
go

create table working_WM_ContractPermitCharges(
ID int identity(1,1) primary key,
username varchar(50),
Working_WM_ContractPermitSelection_ID int,
Fee_Code_ID int,
WM_RevenueUOMControlCodes_ID int,
Quantity decimal(16,4),
Price decimal(16,4),
Total decimal(16,4),
VolumePricing bit,
VolumePricingLevel varchar(30),
SourceID int,
SourceType varchar(1),
TicketCount int,
TotalNetWeight decimal(16,4),
TotalFuel decimal(16,4),
BillingControl varchar(30)
)

go

CREATE TABLE [dbo].[working_WM_Misc_Chg](
	id int identity(1,1) primary key,
	Working_WM_ContractPermitSelection_ID int,
	username varchar(50),
	misc_code varchar(10) NULL,
	misc_desc varchar(40) NULL,
	quantity decimal(15, 4) NULL,
	unit_price money NULL,
	gst varchar(1) NULL,
	pst varchar(1) NULL,
	total money NULL)

go



go
	create table AR_WM_Invoice_working (
		id int primary key identity(1,1),
		username varchar(50),
		selected bit,
		so_trn_proj_id int,
		customer_id int,
		CustomerCode varchar(10),
		Customer varchar(40),
		AgreementNo varchar(100),
		AgreementDescription varchar(25),
		StartDate datetime,
		EndDate datetime,
		InvoiceAmount money,
		MiscCharges money,
		GST money,
		PST money,
		Total money,
		BillingReleaseDate datetime,
		CommunicationStatus varchar(1),
		CommunicationType varchar(25)
	)
go
	alter table AR_WM_Invoice_working add subledger_no int
go
	alter table AR_WM_Invoice_working add
	acct_year int,
	acct_period varchar(40),
	register_date datetime,
	invoiceno int,
	invoice_date datetime,
	company_name varchar(50)
go
	alter table WM_SETUP add EnableWSBillingAssistant bit
go
	create table AR_BillingWMAdditionAttach_Working (
		ID int primary key identity(1,1),
		username varchar(50),
		so_trn_proj_id int,
		Repository_ID int,
		FileName varchar(255),
		FileType varchar(max),
		Manual bit
	)
go
	
go
create table WM_WeighScaleEntryCashDetail(
	id int not null identity(1,1) primary key,
	WM_WeighScaleEntryID int,
	ContractOrPermit char,
	ContractOrPermitID int,
	ContractOrPermitDetailsID int,
	Qty money,
	FeeAmount money,
	FeeDescription varchar(max),
	UOM varchar(6),
	BillingControl varchar(30))
go
create table working_WM_DailyUpdate(
	id int not null identity(1,1) primary key,
	username varchar(10),
	WM_WeighScaleEntryID int,
	Selected bit)
go
alter table SO_TRN_HDR add WM_WeighScaleEntryID int
go
alter table SO_TRN_DETAIL add WM_WeighScaleEntryCashDetail int
go
alter table ar_unapply add WM_WeighScaleEntryID int
go
alter table ar_deposit add WM_WeighScaleEntryID int
go
--alter table WM_WeighScaleEntry drop column LocationDescription
go
	create table Report_AR_Invoice_SO_TRN_HDR_AGING (
		USERNAME varchar(50),
		CUSTOMER_ID int,
		PreviousBalance money,
		PaymentReceived money,
		PaymentDate datetime,
		BalanceForward money,
		DaysCurrent money,
		Days30 money,
		Days60 money,
		Days90 money,
		Days120 money,
		AmountDue money
	)
go

go

alter table WM_Volume_Pricing_Header add WM_RevenueUOMControlCodes_ID int
go
alter table WM_Volume_Pricing_Header add Fee_Code_ID int
go
alter table WM_Volume_Pricing_Header add Estimated_Quantity decimal(16,4)

alter table WM_Volume_Pricing_Header add Actual_Quantity decimal(16,4)
go

alter table WM_Volume_Pricing_Header add ResetDate datetime
go

alter table WM_Volume_Pricing_Header add ResetQuantity decimal(16,4)
go
alter table WM_Volume_Pricing_Header add ChargeType varchar(10)
go
alter table Working_WM_ContractPermitSelection add TonnageSelected bit not null default 0
go


go
alter table WM_WeighScaleEntry add LoadNum int
go

go
	create table Report_AR_Invoice_SO_TRN_HDR_AGING (
		USERNAME varchar(50),
		CUSTOMER_ID int,
		PreviousBalance money,
		PaymentReceived money,
		PaymentDate datetime,
		BalanceForward money,
		DaysCurrent money,
		Days30 money,
		Days60 money,
		Days90 money,
		Days120 money,
		AmountDue money
	)
go
go


alter table working_WM_ContractPermitCharges add WM_ContractPermitJOB_ID int

go

alter table WM_Billing_Release_Details add WM_ContractPermitJOB_ID int

go
alter table Rental_Report_Options add Billed varchar(1)
go

alter table Rental_Report_Options
add 	BilledDate_From datetime, BilledDate_To datetime
go
if not exists (select * from CFS_ElectronicSaveSetup where Description = 'Weigh Scale Ticket Print')
begin
	insert CFS_ElectronicSaveSetup (ID, Module, AutoSave, ContextItem_ID, Description, DisableAutoSave)
	select 32, 'Waste Management', 0, null, 'Weigh Scale Ticket Print', 0
end
go
create table working_WM_BillingForcast(
ID int identity(1,1) primary key,
username varchar(50),
CustomerCode varchar(10),
CustomerName varchar(40),
ContractPermitNumber varchar(50),
JobName varchar(180),
Billed_LoadsIn int,
Billed_TonsIn decimal(16,4),
Billed_Disposal decimal(16,4),
Billed_Hauling decimal(16,4),
Billed_Fuel decimal(16,4),
Billed_Handling decimal(16,4),
Billed_Revenue decimal(16,4),
Unbilled_LoadsIn int,
Unbilled_TonsIn decimal(16,4),
Unbilled_Disposal decimal(16,4),
Unbilled_Hauling decimal(16,4),
Unbilled_Fuel decimal(16,4),
Unbilled_Handling decimal(16,4),
Unbilled_Revenue decimal(16,4))

go

alter table working_WM_BillingForcast add ContractPermit varchar(1)
go

alter table working_WM_BillingForcast add ContractPermit_ID int
go

alter table working_WM_BillingForcast add Job_ID int
go

CREATE TABLE [dbo].[Working_WM_LoadNum](   -- drop table Working_WM_LoadNUm
	[username] [varchar](200) NULL,
	[LoadNum] [int] NULL
)
go


CREATE TABLE [dbo].[Working_WM_ContractPermitSummary](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NULL,
	[working_wm_contractpermitselection_ID] int,
	[customer_id] [int] NULL,
	[CustomerCode] [varchar](10) NULL,
	[CustomerName] [varchar](40) NULL,
	[ContractPermit] [varchar](15) NULL,
	[ContractPermit_ID] [int] NULL,
	[ContractPermit_Job_ID] int,
	[ContractPermitJob] varchar(180),
	[ContractPermitNumber] [varchar](50) NULL,
	[BillingCustomer_ID] int,
	[BillingCustomerCode] varchar(10),
	[BillingCustomerName] varchar(40),
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[IssuedTo] [varchar](40) NULL,
	[ProjectName] [varchar](100) NULL,
	[ContractPermitTypeDesc] [varchar](100) NULL,
	[TotalProducts] [decimal](16, 4) NULL,
	[TotalMisc] [decimal](16, 4) NULL,
	[TotalGST] [decimal](16, 4) NULL,
	[TotalPST] [decimal](16, 4) NULL,
	[TotalInvoice] [decimal](16, 4) NULL
	)

go
alter table working_WM_ContractPermitCharges add Working_WM_ContractPermitSummary_ID int 
go
alter table working_WM_Misc_Chg add Working_WM_ContractPermitSummary_ID int 
go
alter table working_WM_Misc_Chg add ContractPermitJob_ID int 
go
alter table Working_WM_ContractPermitSelection drop column totalproducts
go
alter table Working_WM_ContractPermitSelection drop column totalMisc
go
alter table Working_WM_ContractPermitSelection drop column totalGST
go
alter table Working_WM_ContractPermitSelection drop column totalPST
go
alter table Working_WM_ContractPermitSelection drop column totalInvoice
go
alter table WM_WeighScales add 
	DefaultIncoming bit not null default 0,
	DefaultOutgoing bit not null default 0
go
alter table WM_WeighScaleEntry alter column LocationCode varchar(20)
go
go
	alter table PC_FLEM_SheetSel_working add 
		NonBillAmount decimal(19,4),
		TotalAmount decimal(19,4)
go
	alter table WS_PCDL_LogHeader add 
		NonBillAmount decimal(19,4),
		TotalAmount decimal(19,4)
go
alter table Report_AR_Invoice_SO_TRN_HDR_AGING add customer_code varchar(10)
go
	
go
alter table proj_cntl add AutoLoadTeamList bit	
go

CREATE TABLE [dbo].[PC_ShopDrawing_Proj](
	[PC_ShopDrawing_Proj_ID] [int] IDENTITY(1,1) NOT NULL,
	[pri_id] [int] NULL,
	[ShopDrawingNum] [int] NULL,
	[LogDate] [date] NULL,
	[Description] [varchar](max) NULL,
	[ReceivedFrom_ContactID] [int] NULL,
	[SubmittedTo_ContactID] [int] NULL,
	[ReceivedDate] [date] NULL,
	[SentDate] [date] NULL,
	[ForwardedDate] [date] NULL,
	[PC_ShopDrawingStatus_ID] [int] NULL,
	[Comments] [varchar](max) NULL,
	[Closed] [bit] NULL,
	[RevisionNum] [int] NULL,
	[ReturnedDate] [date] NULL,
	[RequiredDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[PC_ShopDrawing_Proj_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[PC_ShopDrawingStatus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](5) NULL,
	[Description] [varchar](50) NULL,
	[isDefault] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

if( not exists(select * from PC_ShopDrawingStatus where Code=1))
	insert PC_ShopDrawingStatus(Code, Description, isDefault) values (1, 'One', 1)
GO

if( not exists(select * from PC_ShopDrawingStatus where Code=2))
	insert PC_ShopDrawingStatus(Code, Description, isDefault) values (2, 'Two', 0)
GO

alter table costing_work_class add RegularCeiling money, OvertimeCeiling money, DoubleTimeCeiling money, TravelCeiling money
GO

CREATE TABLE [dbo].[working_pc_rpt_shopdrawing_proj](
	[id] [int] NOT NULL,
	[username] [varchar](500) NOT NULL,
	[entryType] [varchar](20) NOT NULL,
	[entryid] [int] NOT NULL,
	[selected] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[working_pc_rpt_shopdrawing_proj] ADD  DEFAULT ((1)) FOR [selected]
GO
go
CREATE TABLE [dbo].[Working_WM_ContractPermit](
	[username] [varchar](200) NULL,
	[WM_WeighScaleEntry_ID] [int] NULL
)
go

alter table Rental_Report_Options add AsAt_Actual varchar(10) , From_Date_Actual datetime,  To_Date_Actual datetime
go

go
	alter table WM_Setup add ConsolBillWeighTicket bit
go
	create table WM_RPT_WeightTicket_Working (
		username varchar(50),
		WM_WeighScaleEntry_ID int
	)
go
	alter table Report_AR_Invoice_SO_TRN_HDR_AGING add
		InvoiceNo int,
		DueDate datetime
go
	alter table Report_AR_Invoice_SO_TRN_HDR_AGING add so_trn_id int
go
	alter table Report_AR_Invoice_SO_TRN_HDR_AGING add balance money
go
	alter table Report_AR_Invoice_SO_TRN_HDR add HOLDBACK_PST money
go
	alter table ar_report_invoice_header add HOLDBACK_PST money
go
if not exists (select * from earn_ctrl_codes where ecc_code = 'TR')
	insert into earn_ctrl_codes (ecc_code, ecc_desc)
	select 'TR', 'Travel Time'
GO
alter table PROJ_CONTACTS add field_manager_2 int
go
alter table EST_Contacts add field_manager_2 int
go
	if not exists(select * from PC_Stakeholder_Type where Proj_Contact_Fieldname = 'field_manager_2')
	begin
		insert into PC_Stakeholder_Type (PC_Stakeholder_Type_ID, Proj_Contact_Fieldname, DisplayName,Web_Contact_Fieldname, Est_Contact_Fieldname)
		select 14, 'field_manager_2', 'Project Field Manager 2','IsFieldManager2', 'field_manager_2'
	end
	else
	begin
		update PC_Stakeholder_Type
		set Proj_Contact_Fieldname = 'field_manager_2', DisplayName = 'Field Manager 2',Web_Contact_Fieldname = 'IsFieldManager2', Est_Contact_Fieldname='field_manager_2'
		where PC_Stakeholder_Type_ID = 14 
	end
go

go
	alter table ar_setup add 
		PST_ON_HOLDBACK bit,
		SWAP_PST_SEG bit
go
	alter table Sales_Taxes add PST_HB_Acct varchar(21)
go
	alter table so_trn_hdr add HOLDBACK_PST money
go
	alter table so_trn_hdr_batch add HOLDBACK_PST money
go
	alter table so_trn_hdr_proj add HOLDBACK_PST money
go
	alter table Report_AR_Invoice_SO_TRN_HDR add HOLDBACK_PST money
go
	alter table ar_report_invoice_header add HOLDBACK_PST money
go
	alter table SOH_TRN_HDR add HOLDBACK_PST money
go
	alter table working_PC_Billing_Summary add 
		holdback_gst money,
		holdback_pst money
go
	alter table working_PC_Billing_ContractDetails add
		holdback_gst money,
		holdback_pst money
go
