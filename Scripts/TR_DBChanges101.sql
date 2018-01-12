alter table working_pc_purchreq_items alter column ONE_TIME_PARTNO varchar(200)
go

go

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





go





