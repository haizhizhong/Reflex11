go

	CREATE TABLE ReportLEMApprovalHeader(
		id int not null identity(1,1) primary key,
		LogHeaderId int NULL,
		LogDate datetime NULL,
		LEM# varchar(20) NULL,
		Project int NULL,
		ProjectName varchar(50) NULL,
		CustomerCode varchar(10) NULL,
		CustomerName varchar(40) NULL,
		SiteLocation varchar(40) NULL,
		POReference varchar(30) NULL,
		CompanyName varchar(50) NULL,
		Billable bit NULL,
		Username varchar(10) NULL
	)

go

	CREATE TABLE ReportLEMApprovalAP(
		id int not null identity(1,1) primary key,
		LogHeaderId int NULL,
		InvoiceDate datetime NULL,
		InvoiceNum varchar(15) NULL,
		PONum varchar(20) NULL,
		LineNum int NULL,
		Description varchar(150) NULL,
		Amount money NULL,
		InvoiceAmount money NULL,
		HeaderMarkupAmount money NULL,
		MarkupAmount money NULL,
		MarkupPercent decimal(19, 4) NULL,
		BillAmount money NULL,
		Username varchar(10) NULL
	)

go

	alter table ReportLEMApprovalAP add CostCode varchar(15)

go

	alter table ReportLEMApprovalAP add
		Lv1Id int,
		Lv2Id int,
		Lv3Id int,
		Lv4Id int

go

	alter table ReportLEMApprovalAP add COCode varchar(25)

go

	CREATE TABLE ReportLEMApprovalEquipment(
		id int not null identity(1,1) primary key,
		LogHeaderId int NULL,
		Level1Code varchar(6) NULL,
		Level1 varchar(30) NULL,
		Level2Code varchar(6) NULL,
		Level2 varchar(30) NULL,
		Level3Code varchar(6) NULL,
		Level3 varchar(30) NULL,
		Level4Code varchar(6) NULL,
		Level4 varchar(30) NULL,
		AssetCode varchar(15) NULL,
		Asset varchar(100) NULL,
		ClassCode varchar(5) NULL,
		Class varchar(50) NULL,
		CategoryCode varchar(5) NULL,
		Category varchar(50) NULL,
		Billable bit NULL,
		Quantity decimal(14, 4) NULL,
		BillAmount money NULL,
		UnitBillRate money NULL,
		BillCycle char(1) NULL,
		Component varchar(1) NULL,
		Username varchar(10) NULL
	) 

go

	alter table ReportLEMApprovalEquipment add CostCode varchar(15)

go

	alter table ReportLEMApprovalEquipment add
		Lv1Id int,
		Lv2Id int,
		Lv3Id int,
		Lv4Id int

go

	alter table ReportLEMApprovalEquipment add COCode varchar(25)

go

	CREATE TABLE ReportLEMApprovalLabour(
		id int not null identity(1,1) primary key,
		LogHeaderId int NULL,
		EntryID int NULL,
		EmpNum int NULL,
		Name varchar(62) NULL,
		WorkClassCode varchar(5) NULL,
		WorkClass varchar(30) NULL,
		Level1Code varchar(6) NULL,
		Level1 varchar(30) NULL,
		Level2Code varchar(6) NULL,
		Level2 varchar(30) NULL,
		Level3Code varchar(6) NULL,
		Level3 varchar(30) NULL,
		Level4Code varchar(6) NULL,
		Level4 varchar(30) NULL,
		Billable bit NULL,
		BillAmount money NULL,
		TotalHours money NULL,
		Component varchar(1) NULL,
		Username varchar(10) NULL
	) 

go

	alter table ReportLEMApprovalLabour add CostCode varchar(15)

go

	alter table ReportLEMApprovalLabour add
		Lv1Id int,
		Lv2Id int,
		Lv3Id int,
		Lv4Id int

go

	alter table ReportLEMApprovalLabour add COCode varchar(25)

go

	alter table ReportLEMApprovalLabour add type varchar(1)

go

	CREATE TABLE ReportLEMApprovalLabourDetail(
		id int not null identity(1,1) primary key,
		EntryId int NULL,
		BillRate money NULL,
		WorkHours decimal(10, 4) NULL,
		Amount money NULL,
		Description varchar(50) NULL,
		ValueType varchar(10) NULL,
		BillingRateType varchar(10) NULL,
		Component varchar(1) NULL,
		Username varchar(10) NULL
	) 

go

	alter table Project add
		SiteAddress Varchar (200),
		SiteCity Varchar (20),
		SiteState Varchar (2),
		SiteZip Varchar (10),
		CustomerAddress2 Varchar (40),
		CustomerAddress3 Varchar (40),
		CustomerCity Varchar (20),
		CustomerState Varchar (2), 
		CustomerZip Varchar (10),
		ProjectExtendedDescription varchar(max)

go

	alter table Company add
		CompanyAddress1 Varchar (40),
		CompanyAddress2 Varchar (40),
		CompanyAddress3 Varchar (40),
		CompanyCity Varchar (20),
		CompanyState Varchar (15),
		CompanyZip Varchar (9),
		CompanyPhone Varchar (20),
		CompanyFax Varchar (20),
		CompanyEmail Varchar (50),
		CompanyWeb Varchar (500)

go

	alter table LEMHeader add LEM_Desc varchar(max)
go

	alter table ReportLEMApprovalHeader add
		CompanyAddress1 Varchar (40),
		CompanyAddress2 Varchar (40),
		CompanyAddress3 Varchar (40),
		CompanyCity Varchar (20),
		CompanyState Varchar (15),
		CompanyZip Varchar (9),
		CompanyPhone Varchar (20),
		CompanyFax Varchar (20),
		CompanyEmail Varchar (50),
		CompanyWeb Varchar (500),
		SiteAddress Varchar (200),
		SiteCity Varchar (20),
		SiteState Varchar (2),
		SiteZip Varchar (10),
		CustomerAddress2 Varchar (40),
		CustomerAddress3 Varchar (40),
		CustomerCity Varchar (20),
		CustomerState Varchar (2), 
		CustomerZip Varchar (10),
		ProjectExtendedDescription varchar(max),
		LEM_Desc varchar(max)

go

	alter table ReportLEMApprovalHeader add CustomerAddress1 varchar(40)

go

	alter table ReportLEMApprovalEquipment add OwnerType char(1)

go

	alter table ReportLEMApprovalAP add
		SupplierName varchar(40),
		SupplierCode varchar(10),	
		Level1Code varchar(6),
		Level1 varchar(30),
		Level2Code varchar(6),
		Level2 varchar(30),
		Level3Code varchar(6),
		Level3 varchar(30),
		Level4Code varchar(6),
		Level4 varchar(30) 

go

	alter table LemAPDetail add
		[Level1Id] [int],
		[Level2Id] [int],
		[Level3Id] [int],
		[Level4Id] [int]

go

	alter table TimeCode add ReportTypeColumn varchar(20)

go

	alter table ReportLEMApprovalLabour add
		RegularHours money,
		RegularRate money,
		OTHours money,
		OTRate money,
		TravelHours money,
		TravelRate money,
		LOAHours money,
		LOARate money,
		EquipmentHours money,
		EquipmentRate money,
		OtherHours money,
		OtherRate money,
		DTHours money,
		DTRate money

go

	alter table OvertimeLimit alter column DailyLimit decimal(10, 2) null

go

	alter table OvertimeLimit alter column DailyDoubleLimit decimal(10, 2) null

go

	alter table OvertimeLimit alter column WeeklyLimit decimal(10, 2) null

go

	alter table OvertimeLimit alter column WeeklyDoubleLimit decimal(10, 2) null

go

	alter table ProjectWorkClass alter column RegularHours decimal(10, 2) NULL

go

	alter table ProjectWorkClass alter column TravelHours decimal(10, 2) NULL

go

	alter table ReportLEMApprovalLabour alter column RegularHours decimal(10,2)

go

	alter table ReportLEMApprovalLabour alter column OTHours decimal(10,2)

go

	alter table ReportLEMApprovalLabour alter column TravelHours decimal(10,2)

go

	alter table ReportLEMApprovalLabour alter column LOAHours decimal(10,2)

go

	alter table ReportLEMApprovalLabour alter column EquipmentHours decimal(10,2)

go

	alter table ReportLEMApprovalLabour alter column OtherHours decimal(10,2)

go

	alter table ReportLEMApprovalLabour alter column DTHours decimal(10,2)

go

	alter table Company drop column LemAvailableDays
go

	alter table CompanySyncProcess add SyncType varchar(20)

go

	delete CompanySyncProcess

go

	alter table ReportLEMApprovalHeader add
		TotalAPAmt money,
		TotalEqAmt money,
		TotalLabHrs Decimal(10,2),
		TotalLabAmt money,
		TotalLabTravelAmt money,
		TotalLabLOAAmt money,
		TotalLabEqAmt money

go
