delete RE_CashFlow_Type where cft_id < 0 delete RE_CashFlow_Group where CFG_ID < 0 delete RE_CashFlow_SubGroup where CFSG_ID < 0 
go
set identity_insert RE_CashFlow_Type on 
if exists(select * from RE_CashFlow_Type where CFT_ID =-1)
begin
update RE_CashFlow_Type set [CFT_DESC] = 'Cash Flow Regular (Type 1)',[CFT_Type] = 'Cash Flow',[CFT_TEMPLATE] = 1,CFT_TEMPLATE_ID = null,[DefaultCashFlow] = 0 where CFT_ID = -1
end
else
begin
insert into RE_CashFlow_Type([CFT_ID],[CFT_DESC],[CFT_Type],[CFT_TEMPLATE],[CFT_TEMPLATE_ID],[DefaultCashFlow])values(-1,'Cash Flow Regular (Type 1)','Cash Flow',1,null,0)
end
go
if exists(select * from RE_CashFlow_Type where CFT_ID =-2)
begin
update RE_CashFlow_Type set [CFT_DESC] = 'Balance Sheet Type 1',[CFT_Type] = 'Balance Sheet',[CFT_TEMPLATE] = 1,CFT_TEMPLATE_ID = null,[DefaultCashFlow] = 0 where CFT_ID = -2
end
else
begin
insert into RE_CashFlow_Type([CFT_ID],[CFT_DESC],[CFT_Type],[CFT_TEMPLATE],[CFT_TEMPLATE_ID],[DefaultCashFlow])values(-2,'Balance Sheet Type 1','Balance Sheet',1,null,0)
end
go
if exists(select * from RE_CashFlow_Type where CFT_ID =-3)
begin
update RE_CashFlow_Type set [CFT_DESC] = 'Income Statement Type 2',[CFT_Type] = 'Income Statement',[CFT_TEMPLATE] = 1,CFT_TEMPLATE_ID = null,[DefaultCashFlow] = 0 where CFT_ID = -3
end
else
begin
insert into RE_CashFlow_Type([CFT_ID],[CFT_DESC],[CFT_Type],[CFT_TEMPLATE],[CFT_TEMPLATE_ID],[DefaultCashFlow])values(-3,'Income Statement Type 2','Income Statement',1,null,0)
end
go
if exists(select * from RE_CashFlow_Type where CFT_ID =-18)
begin
update RE_CashFlow_Type set [CFT_DESC] = 'Cash Flow Regular (Type 2)',[CFT_Type] = 'Cash Flow',[CFT_TEMPLATE] = 1,CFT_TEMPLATE_ID = null,[DefaultCashFlow] = 0 where CFT_ID = -18
end
else
begin
insert into RE_CashFlow_Type([CFT_ID],[CFT_DESC],[CFT_Type],[CFT_TEMPLATE],[CFT_TEMPLATE_ID],[DefaultCashFlow])values(-18,'Cash Flow Regular (Type 2)','Cash Flow',1,null,0)
end
go
if exists(select * from RE_CashFlow_Type where CFT_ID =-23)
begin
update RE_CashFlow_Type set [CFT_DESC] = 'Cash Flow - Admin (Type 2)',[CFT_Type] = 'Cash Flow',[CFT_TEMPLATE] = 1,CFT_TEMPLATE_ID = null,[DefaultCashFlow] = 0 where CFT_ID = -23
end
else
begin
insert into RE_CashFlow_Type([CFT_ID],[CFT_DESC],[CFT_Type],[CFT_TEMPLATE],[CFT_TEMPLATE_ID],[DefaultCashFlow])values(-23,'Cash Flow - Admin (Type 2)','Cash Flow',1,null,0)
end
go
if exists(select * from RE_CashFlow_Type where CFT_ID =-26)
begin
update RE_CashFlow_Type set [CFT_DESC] = 'Cash Flow Type 3 (Consolidated)',[CFT_Type] = 'Cash Flow',[CFT_TEMPLATE] = 1,CFT_TEMPLATE_ID = null,[DefaultCashFlow] = 0 where CFT_ID = -26
end
else
begin
insert into RE_CashFlow_Type([CFT_ID],[CFT_DESC],[CFT_Type],[CFT_TEMPLATE],[CFT_TEMPLATE_ID],[DefaultCashFlow])values(-26,'Cash Flow Type 3 (Consolidated)','Cash Flow',1,null,0)
end
go
if exists(select * from RE_CashFlow_Type where CFT_ID =-25)
begin
update RE_CashFlow_Type set [CFT_DESC] = 'Cash Flow Type 3 (Development Project)',[CFT_Type] = 'Cash Flow',[CFT_TEMPLATE] = 1,CFT_TEMPLATE_ID = null,[DefaultCashFlow] = 0 where CFT_ID = -25
end
else
begin
insert into RE_CashFlow_Type([CFT_ID],[CFT_DESC],[CFT_Type],[CFT_TEMPLATE],[CFT_TEMPLATE_ID],[DefaultCashFlow])values(-25,'Cash Flow Type 3 (Development Project)','Cash Flow',1,null,0)
end
go
if exists(select * from RE_CashFlow_Type where CFT_ID =-24)
begin
update RE_CashFlow_Type set [CFT_DESC] = 'Cash Flow Type 3 (Raw Land)',[CFT_Type] = 'Cash Flow',[CFT_TEMPLATE] = 1,CFT_TEMPLATE_ID = null,[DefaultCashFlow] = 0 where CFT_ID = -24
end
else
begin
insert into RE_CashFlow_Type([CFT_ID],[CFT_DESC],[CFT_Type],[CFT_TEMPLATE],[CFT_TEMPLATE_ID],[DefaultCashFlow])values(-24,'Cash Flow Type 3 (Raw Land)','Cash Flow',1,null,0)
end
go
set identity_insert RE_CashFlow_Type off 
set identity_insert RE_CashFlow_Group on 
if exists(select * from RE_CashFlow_Group where CFG_ID =-1)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'CI',[CFG_DESC] = 'Cash In',[CFT_ID] = -1,[CFG_REQUIRED] = 1,[CFG_SQL] = '',[CFG_SEQ] = 1,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 1 where CFG_ID = -1
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-1,'CI','Cash In',-1,1,'',1,null,null,1)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-2)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'CO',[CFG_DESC] = 'Cash Out',[CFT_ID] = -1,[CFG_REQUIRED] = 1,[CFG_SQL] = '',[CFG_SEQ] = 2,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 1 where CFG_ID = -2
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-2,'CO','Cash Out',-1,1,'',2,null,null,1)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-3)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'NetNoFin',[CFG_DESC] = '',[CFT_ID] = -1,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 4,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -3
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-3,'NetNoFin','',-1,0,'',4,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-4)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'CumCashPos',[CFG_DESC] = '',[CFT_ID] = -1,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 5,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -4
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-4,'CumCashPos','',-1,0,'',5,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-5)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'Financing',[CFG_DESC] = '',[CFT_ID] = -1,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 6,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -5
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-5,'Financing','',-1,0,'',6,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-17)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'COF',[CFG_DESC] = '',[CFT_ID] = -1,[CFG_REQUIRED] = 1,[CFG_SQL] = '',[CFG_SEQ] = 3,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -17
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-17,'COF','',-1,1,'',3,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-6)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'Assets',[CFG_DESC] = 'Assets',[CFT_ID] = -2,[CFG_REQUIRED] = 1,[CFG_SQL] = '',[CFG_SEQ] = 1,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 1 where CFG_ID = -6
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-6,'Assets','Assets',-2,1,'',1,null,null,1)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-7)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'Equity',[CFG_DESC] = 'Equity',[CFT_ID] = -2,[CFG_REQUIRED] = 1,[CFG_SQL] = '',[CFG_SEQ] = 2,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 1 where CFG_ID = -7
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-7,'Equity','Equity',-2,1,'',2,null,null,1)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-8)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'Liability',[CFG_DESC] = 'Liabilites',[CFT_ID] = -2,[CFG_REQUIRED] = 1,[CFG_SQL] = '',[CFG_SEQ] = 3,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 1 where CFG_ID = -8
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-8,'Liability','Liabilites',-2,1,'',3,null,null,1)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-9)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'Tot-LE',[CFG_DESC] = '',[CFT_ID] = -2,[CFG_REQUIRED] = 1,[CFG_SQL] = '',[CFG_SEQ] = 4,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -9
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-9,'Tot-LE','',-2,1,'',4,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-11)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'IS',[CFG_DESC] = '',[CFT_ID] = -3,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 2,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -11
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-11,'IS','',-3,0,'',2,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-10)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'SP',[CFG_DESC] = 'Sales Projections',[CFT_ID] = -3,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 1,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 1 where CFG_ID = -10
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-10,'SP','Sales Projections',-3,0,'',1,null,null,1)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-78)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'CI',[CFG_DESC] = 'Cash Inflow',[CFT_ID] = -18,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 1,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -78
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-78,'CI','Cash Inflow',-18,0,'',1,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-79)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'CO',[CFG_DESC] = 'Cash Outflow',[CFT_ID] = -18,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 2,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -79
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-79,'CO','Cash Outflow',-18,0,'',2,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-80)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'NCBPF',[CFG_DESC] = '',[CFT_ID] = -18,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 3,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -80
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-80,'NCBPF','',-18,0,'',3,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-81)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'FIP',[CFG_DESC] = 'Financing / Investment Proceeds',[CFT_ID] = -18,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 4,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -81
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-81,'FIP','Financing / Investment Proceeds',-18,0,'',4,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-82)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'CCP',[CFG_DESC] = 'Cumulative Cash Position',[CFT_ID] = -18,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 5,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -82
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-82,'CCP','Cumulative Cash Position',-18,0,'',5,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-99)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'CI-AD',[CFG_DESC] = 'Cash In',[CFT_ID] = -23,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 1,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -99
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-99,'CI-AD','Cash In',-23,0,'',1,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-100)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'CO-AD',[CFG_DESC] = 'Cash Out',[CFT_ID] = -23,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 2,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -100
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-100,'CO-AD','Cash Out',-23,0,'',2,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-101)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'TOT-AD',[CFG_DESC] = 'Total',[CFT_ID] = -23,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 3,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -101
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-101,'TOT-AD','Total',-23,0,'',3,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-177)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'CI',[CFG_DESC] = 'Cash Inflow',[CFT_ID] = -26,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 1,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -177
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-177,'CI','Cash Inflow',-26,0,'',1,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-176)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'CO',[CFG_DESC] = 'Cash Outflow',[CFT_ID] = -26,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 2,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -176
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-176,'CO','Cash Outflow',-26,0,'',2,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-175)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'NCBPF',[CFG_DESC] = '',[CFT_ID] = -26,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 3,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -175
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-175,'NCBPF','',-26,0,'',3,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-174)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'FIP',[CFG_DESC] = 'Financing / Investment Proceeds',[CFT_ID] = -26,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 4,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -174
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-174,'FIP','Financing / Investment Proceeds',-26,0,'',4,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-173)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'CCP',[CFG_DESC] = 'Cumulative Cash Position',[CFT_ID] = -26,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 5,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -173
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-173,'CCP','Cumulative Cash Position',-26,0,'',5,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-172)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'CI',[CFG_DESC] = 'Cash Inflow',[CFT_ID] = -25,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 1,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -172
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-172,'CI','Cash Inflow',-25,0,'',1,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-171)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'CO',[CFG_DESC] = 'Cash Outflow',[CFT_ID] = -25,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 2,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -171
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-171,'CO','Cash Outflow',-25,0,'',2,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-170)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'NCBPF',[CFG_DESC] = '',[CFT_ID] = -25,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 3,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -170
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-170,'NCBPF','',-25,0,'',3,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-169)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'FIP',[CFG_DESC] = 'Financing / Investment Proceeds',[CFT_ID] = -25,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 4,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -169
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-169,'FIP','Financing / Investment Proceeds',-25,0,'',4,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-168)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'CCP',[CFG_DESC] = 'Cumulative Cash Position',[CFT_ID] = -25,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 5,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -168
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-168,'CCP','Cumulative Cash Position',-25,0,'',5,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-167)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'CI',[CFG_DESC] = 'Cash Inflow',[CFT_ID] = -24,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 1,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -167
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-167,'CI','Cash Inflow',-24,0,'',1,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-166)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'CO',[CFG_DESC] = 'Cash Outflow',[CFT_ID] = -24,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 2,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -166
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-166,'CO','Cash Outflow',-24,0,'',2,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-165)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'NCBPF',[CFG_DESC] = '',[CFT_ID] = -24,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 3,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -165
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-165,'NCBPF','',-24,0,'',3,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-164)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'FIP',[CFG_DESC] = 'Financing / Investment Proceeds',[CFT_ID] = -24,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 4,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -164
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-164,'FIP','Financing / Investment Proceeds',-24,0,'',4,null,null,0)
end
go
if exists(select * from RE_CashFlow_Group where CFG_ID =-163)
begin
update RE_CashFlow_Group set [CFG_CODE] = 'CCP',[CFG_DESC] = 'Cumulative Cash Position',[CFT_ID] = -24,[CFG_REQUIRED] = 0,[CFG_SQL] = '',[CFG_SEQ] = 5,OLD_CFG_ID = null,OLD_CFT_ID = null,[Bold] = 0 where CFG_ID = -163
end
else
begin
insert into RE_CashFlow_Group([CFG_ID],[CFG_CODE],[CFG_DESC],[CFT_ID],[CFG_REQUIRED],[CFG_SQL],[CFG_SEQ],[OLD_CFG_ID],[OLD_CFT_ID],[Bold])values(-163,'CCP','Cumulative Cash Position',-24,0,'',5,null,null,0)
end
go
set identity_insert RE_CashFlow_Group off 
set identity_insert RE_CashFlow_SubGroup on 
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-531)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -177,[CFG_CODE] = 'CI_COSTS',[CFG_DESC] = 'Raw Land Interest',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CI_CostCodes',[CFSG_SEQ] = 8,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CashIN_CFByCostCode (uses cost code references to determine cash in from AR Invoices/GL)' where CFSG_ID = -531
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-531,-177,'CI_COSTS','Raw Land Interest',0,'RE_Land_CF2_CI_CostCodes',8,null,null,0,'None','',1,0,'Calls RE_Land_CF2_CashIN_CFByCostCode (uses cost code references to determine cash in from AR Invoices/GL)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-530)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -177,[CFG_CODE] = 'CI_COSTS',[CFG_DESC] = 'Raw Land Rental',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CI_CostCodes',[CFSG_SEQ] = 7,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CashIN_CFByCostCode (uses cost code references to determine cash in from AR Invoices/GL)' where CFSG_ID = -530
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-530,-177,'CI_COSTS','Raw Land Rental',0,'RE_Land_CF2_CI_CostCodes',7,null,null,0,'None','',1,0,'Calls RE_Land_CF2_CashIN_CFByCostCode (uses cost code references to determine cash in from AR Invoices/GL)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-529)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -177,[CFG_CODE] = 'CI_COSTS',[CFG_DESC] = 'Project Interest',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CI_CostCodes',[CFSG_SEQ] = 6,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CashIN_CFByCostCode (uses cost code references to determine cash in from AR Invoices/GL)' where CFSG_ID = -529
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-529,-177,'CI_COSTS','Project Interest',0,'RE_Land_CF2_CI_CostCodes',6,null,null,0,'None','',1,0,'Calls RE_Land_CF2_CashIN_CFByCostCode (uses cost code references to determine cash in from AR Invoices/GL)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-528)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -177,[CFG_CODE] = 'CI_COSTS',[CFG_DESC] = 'Recoveries',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CI_CostCodes',[CFSG_SEQ] = 4,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CashIN_CFByCostCode (uses cost code references to determine cash in from AR Invoices/GL)' where CFSG_ID = -528
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-528,-177,'CI_COSTS','Recoveries',0,'RE_Land_CF2_CI_CostCodes',4,null,null,0,'None','',1,0,'Calls RE_Land_CF2_CashIN_CFByCostCode (uses cost code references to determine cash in from AR Invoices/GL)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-527)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -177,[CFG_CODE] = 'CI-LD',[CFG_DESC] = 'Lot Deposits',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CI_LD',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_AgreementTransactions if not previously called & generates deposit line' where CFSG_ID = -527
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-527,-177,'CI-LD','Lot Deposits',0,'RE_Land_CF2_CI_LD',2,null,null,0,'None','Include In Total',0,0,'Calls RE_Land_CF2_CI_AgreementTransactions if not previously called & generates deposit line')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-526)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -177,[CFG_CODE] = 'CI_LC',[CFG_DESC] = 'Lot Closings',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CI_LC',[CFSG_SEQ] = 3,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_AgreementTransactions if not previously called & generates closing line' where CFSG_ID = -526
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-526,-177,'CI_LC','Lot Closings',0,'RE_Land_CF2_CI_LC',3,null,null,0,'None','Include In Total',0,0,'Calls RE_Land_CF2_CI_AgreementTransactions if not previously called & generates closing line')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-525)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -177,[CFG_CODE] = 'CI_MISC',[CFG_DESC] = 'Miscellaneous (Incl. Gst Returns)',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CI_MISC',[CFSG_SEQ] = 5,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_CFByItem (Pulls misc AR from projections)' where CFSG_ID = -525
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-525,-177,'CI_MISC','Miscellaneous (Incl. Gst Returns)',0,'RE_Land_CF2_CI_MISC',5,null,null,0,'None','Include In Total',0,0,'Calls RE_Land_CF2_CI_CFByItem (Pulls misc AR from projections)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-524)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -177,[CFG_CODE] = 'CI_NETCASH',[CFG_DESC] = 'Net Cash Inflow',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_Total',[CFSG_SEQ] = 10,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Over',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -524
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-524,-177,'CI_NETCASH','Net Cash Inflow',0,'RE_Land_CF2_Total',10,null,null,1,'Single Over','Is The Total',0,0,'')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-516)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -177,[CFG_CODE] = 'BLANK',[CFG_DESC] = 'Cash Inflow',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Under',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -516
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-516,-177,'BLANK','Cash Inflow',0,'RE_Land_CF2_BLANK',1,null,null,1,'Single Under','',0,0,'')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-536)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -176,[CFG_CODE] = 'CO-GEN',[CFG_DESC] = 'General',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_General',[CFSG_SEQ] = 9,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)' where CFSG_ID = -536
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-536,-176,'CO-GEN','General',0,'RE_Land_CF2_CO_General',9,null,null,0,'None','',1,0,'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-535)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -176,[CFG_CODE] = 'CO_DC',[CFG_DESC] = 'Development Charges',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_DevelopmentCharges',[CFSG_SEQ] = 7,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)' where CFSG_ID = -535
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-535,-176,'CO_DC','Development Charges',0,'RE_Land_CF2_CO_DevelopmentCharges',7,null,null,0,'None','',1,0,'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-534)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -176,[CFG_CODE] = 'CO_SERV',[CFG_DESC] = 'Servicing',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_Servicing',[CFSG_SEQ] = 6,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)' where CFSG_ID = -534
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-534,-176,'CO_SERV','Servicing',0,'RE_Land_CF2_CO_Servicing',6,null,null,0,'None','',1,0,'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-533)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -176,[CFG_CODE] = 'CO-CON',[CFG_DESC] = 'Consulting',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_Consulting',[CFSG_SEQ] = 5,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)' where CFSG_ID = -533
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-533,-176,'CO-CON','Consulting',0,'RE_Land_CF2_CO_Consulting',5,null,null,0,'None','',1,0,'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-532)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -176,[CFG_CODE] = 'CO_PT',[CFG_DESC] = 'Property Taxes',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_PropertyTaxes',[CFSG_SEQ] = 4,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)' where CFSG_ID = -532
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-532,-176,'CO_PT','Property Taxes',0,'RE_Land_CF2_CO_PropertyTaxes',4,null,null,0,'None','',1,0,'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-523)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -176,[CFG_CODE] = 'CO-LP',[CFG_DESC] = 'Land Cost',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_LP',[CFSG_SEQ] = 3,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)' where CFSG_ID = -523
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-523,-176,'CO-LP','Land Cost',0,'RE_Land_CF2_CO_LP',3,null,null,0,'None','Include In Total',1,0,'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-522)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -176,[CFG_CODE] = 'CO-SFI',[CFG_DESC] = 'Financing Costs',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_SFI',[CFSG_SEQ] = 10,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)' where CFSG_ID = -522
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-522,-176,'CO-SFI','Financing Costs',0,'RE_Land_CF2_CO_SFI',10,null,null,0,'None','Include In Total',1,0,'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-521)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -176,[CFG_CODE] = 'CO-EXP',[CFG_DESC] = 'Misc. Expenses (Excl Gst)',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_MISC',[CFSG_SEQ] = 11,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 1,[Notes] = 'Calls RE_Land_CF2_CO_CFByGL uses GL for Actuals from GL/Subledger only' where CFSG_ID = -521
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-521,-176,'CO-EXP','Misc. Expenses (Excl Gst)',0,'RE_Land_CF2_CO_MISC',11,null,null,0,'None','Include In Total',0,1,'Calls RE_Land_CF2_CO_CFByGL uses GL for Actuals from GL/Subledger only')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-520)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -176,[CFG_CODE] = 'CO-MAN',[CFG_DESC] = 'Management Fees (Excl GST)',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_MAN',[CFSG_SEQ] = 8,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)' where CFSG_ID = -520
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-520,-176,'CO-MAN','Management Fees (Excl GST)',0,'RE_Land_CF2_CO_MAN',8,null,null,0,'None','Include In Total',1,0,'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-519)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -176,[CFG_CODE] = 'CO-NETCASH',[CFG_DESC] = 'Net Cash Outflow',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_Total',[CFSG_SEQ] = 12,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'None',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -519
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-519,-176,'CO-NETCASH','Net Cash Outflow',0,'RE_Land_CF2_Total',12,null,null,1,'None','Is The Total',0,0,'')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-515)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -176,[CFG_CODE] = 'BLANK',[CFG_DESC] = 'Cash Outflow',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Under',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -515
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-515,-176,'BLANK','Cash Outflow',0,'RE_Land_CF2_BLANK',2,null,null,1,'Single Under','',0,0,'')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-514)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -176,[CFG_CODE] = 'BLANK',[CFG_DESC] = '',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -514
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-514,-176,'BLANK','',0,'RE_Land_CF2_BLANK',1,null,null,0,'None','',0,0,'')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-518)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -175,[CFG_CODE] = 'NCBPF',[CFG_DESC] = 'Net Cash Before Financing',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_NCBPF',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'Single Under',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'creates a line for CO-NETCASH (Net before financing)' where CFSG_ID = -518
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-518,-175,'NCBPF','Net Cash Before Financing',0,'RE_Land_CF2_NCBPF',2,null,null,0,'Single Under','Is The Total',0,0,'creates a line for CO-NETCASH (Net before financing)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-513)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -175,[CFG_CODE] = 'BLANK',[CFG_DESC] = '',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -513
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-513,-175,'BLANK','',0,'RE_Land_CF2_BLANK',1,null,null,0,'None','',0,0,'')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-517)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -174,[CFG_CODE] = 'BLANK',[CFG_DESC] = '',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -517
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-517,-174,'BLANK','',0,'RE_Land_CF2_BLANK',1,null,null,0,'None','',0,0,'')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-512)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -174,[CFG_CODE] = 'FIP-PS-CON',[CFG_DESC] = 'Partner Contibutions',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_FIP_PS_CON',[CFSG_SEQ] = 3,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Generates a line for each share owners cash call' where CFSG_ID = -512
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-512,-174,'FIP-PS-CON','Partner Contibutions',0,'RE_Land_CF2_FIP_PS_CON',3,null,null,1,'None','',0,0,'Generates a line for each share owners cash call')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-511)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -174,[CFG_CODE] = 'BLANK',[CFG_DESC] = 'Financing/Investment Proceeds',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_FIP_CALC_FINANCING',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Under',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Calculates financing based on project financing setup(share owners, bank & Land Vendor)... creates interest line (also creates a table to be used by the next 3 subgroups below).' where CFSG_ID = -511
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-511,-174,'BLANK','Financing/Investment Proceeds',0,'RE_Land_CF2_FIP_CALC_FINANCING',2,null,null,1,'Single Under','',0,0,'Calculates financing based on project financing setup(share owners, bank & Land Vendor)... creates interest line (also creates a table to be used by the next 3 subgroups below).')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-510)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -174,[CFG_CODE] = 'FIP-PS-DIS',[CFG_DESC] = 'Partner Distributions',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_FIP_PS_DIST',[CFSG_SEQ] = 4,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Generates a line for each share owners cash distribution' where CFSG_ID = -510
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-510,-174,'FIP-PS-DIS','Partner Distributions',0,'RE_Land_CF2_FIP_PS_DIST',4,null,null,1,'None','',0,0,'Generates a line for each share owners cash distribution')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-509)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -174,[CFG_CODE] = 'FIP-PS-LN',[CFG_DESC] = 'Loans',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_FIP_PS_LOAN',[CFSG_SEQ] = 5,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Generates a line for banks (Line of credit) & VTB (Land vendor... min repay schedule for projections & VTB account/sub code)' where CFSG_ID = -509
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-509,-174,'FIP-PS-LN','Loans',0,'RE_Land_CF2_FIP_PS_LOAN',5,null,null,1,'None','',0,0,'Generates a line for banks (Line of credit) & VTB (Land vendor... min repay schedule for projections & VTB account/sub code)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-508)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -173,[CFG_CODE] = 'CCP',[CFG_DESC] = 'Cumulative Cash Position',[CFSG_REQUIRED] = 1,[CFSG_SQL] = 'RE_Land_CF2_CCP',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Over, Double Under',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Generates grand totals' where CFSG_ID = -508
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-508,-173,'CCP','Cumulative Cash Position',1,'RE_Land_CF2_CCP',1,null,null,1,'Single Over, Double Under','Is The Total',0,0,'Generates grand totals')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-500)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -172,[CFG_CODE] = 'CI_COSTS',[CFG_DESC] = 'Project Interest',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CI_CostCodes',[CFSG_SEQ] = 6,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CashIN_CFByCostCode (uses cost code references to determine cash in from AR Invoices/GL)' where CFSG_ID = -500
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-500,-172,'CI_COSTS','Project Interest',0,'RE_Land_CF2_CI_CostCodes',6,null,null,0,'None','',1,0,'Calls RE_Land_CF2_CashIN_CFByCostCode (uses cost code references to determine cash in from AR Invoices/GL)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-499)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -172,[CFG_CODE] = 'CI_COSTS',[CFG_DESC] = 'Recoveries',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CI_CostCodes',[CFSG_SEQ] = 4,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CashIN_CFByCostCode (uses cost code references to determine cash in from AR Invoices/GL)' where CFSG_ID = -499
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-499,-172,'CI_COSTS','Recoveries',0,'RE_Land_CF2_CI_CostCodes',4,null,null,0,'None','',1,0,'Calls RE_Land_CF2_CashIN_CFByCostCode (uses cost code references to determine cash in from AR Invoices/GL)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-498)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -172,[CFG_CODE] = 'CI-LD',[CFG_DESC] = 'Lot Deposits',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CI_LD',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_AgreementTransactions if not previously called & generates deposit line' where CFSG_ID = -498
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-498,-172,'CI-LD','Lot Deposits',0,'RE_Land_CF2_CI_LD',2,null,null,0,'None','Include In Total',0,0,'Calls RE_Land_CF2_CI_AgreementTransactions if not previously called & generates deposit line')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-497)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -172,[CFG_CODE] = 'CI_LC',[CFG_DESC] = 'Lot Closings',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CI_LC',[CFSG_SEQ] = 3,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_AgreementTransactions if not previously called & generates closing line' where CFSG_ID = -497
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-497,-172,'CI_LC','Lot Closings',0,'RE_Land_CF2_CI_LC',3,null,null,0,'None','Include In Total',0,0,'Calls RE_Land_CF2_CI_AgreementTransactions if not previously called & generates closing line')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-496)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -172,[CFG_CODE] = 'CI_MISC',[CFG_DESC] = 'Miscellaneous (Incl. Gst Returns)',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CI_MISC',[CFSG_SEQ] = 5,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_CFByItem (Pulls misc AR from projections)' where CFSG_ID = -496
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-496,-172,'CI_MISC','Miscellaneous (Incl. Gst Returns)',0,'RE_Land_CF2_CI_MISC',5,null,null,0,'None','Include In Total',0,0,'Calls RE_Land_CF2_CI_CFByItem (Pulls misc AR from projections)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-495)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -172,[CFG_CODE] = 'CI_NETCASH',[CFG_DESC] = 'Net Cash Inflow',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_Total',[CFSG_SEQ] = 10,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Over',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -495
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-495,-172,'CI_NETCASH','Net Cash Inflow',0,'RE_Land_CF2_Total',10,null,null,1,'Single Over','Is The Total',0,0,'')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-487)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -172,[CFG_CODE] = 'BLANK',[CFG_DESC] = 'Cash Inflow',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Under',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -487
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-487,-172,'BLANK','Cash Inflow',0,'RE_Land_CF2_BLANK',1,null,null,1,'Single Under','',0,0,'')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-507)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -171,[CFG_CODE] = 'CO-GEN',[CFG_DESC] = 'General',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_General',[CFSG_SEQ] = 9,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)' where CFSG_ID = -507
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-507,-171,'CO-GEN','General',0,'RE_Land_CF2_CO_General',9,null,null,0,'None','',1,0,'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-506)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -171,[CFG_CODE] = 'CO_DC',[CFG_DESC] = 'Development Charges',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_DevelopmentCharges',[CFSG_SEQ] = 7,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)' where CFSG_ID = -506
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-506,-171,'CO_DC','Development Charges',0,'RE_Land_CF2_CO_DevelopmentCharges',7,null,null,0,'None','',1,0,'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-505)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -171,[CFG_CODE] = 'CO_SERV',[CFG_DESC] = 'Servicing',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_Servicing',[CFSG_SEQ] = 6,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)' where CFSG_ID = -505
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-505,-171,'CO_SERV','Servicing',0,'RE_Land_CF2_CO_Servicing',6,null,null,0,'None','',1,0,'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-504)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -171,[CFG_CODE] = 'CO-CON',[CFG_DESC] = 'Consulting',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_Consulting',[CFSG_SEQ] = 5,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)' where CFSG_ID = -504
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-504,-171,'CO-CON','Consulting',0,'RE_Land_CF2_CO_Consulting',5,null,null,0,'None','',1,0,'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-494)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -171,[CFG_CODE] = 'CO-LP',[CFG_DESC] = 'Land Cost',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_LP',[CFSG_SEQ] = 3,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)' where CFSG_ID = -494
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-494,-171,'CO-LP','Land Cost',0,'RE_Land_CF2_CO_LP',3,null,null,0,'None','Include In Total',1,0,'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-493)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -171,[CFG_CODE] = 'CO-SFI',[CFG_DESC] = 'Financing Costs',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_SFI',[CFSG_SEQ] = 10,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)' where CFSG_ID = -493
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-493,-171,'CO-SFI','Financing Costs',0,'RE_Land_CF2_CO_SFI',10,null,null,0,'None','Include In Total',1,0,'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-1)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -1,[CFG_CODE] = 'CI-DownPay',[CFG_DESC] = 'Down Payments - Future Sales',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = '',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -1
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-1,-1,'CI-DownPay','Down Payments - Future Sales',0,'',1,null,null,0,'','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-2)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -1,[CFG_CODE] = 'CI-Princip',[CFG_DESC] = 'Principal Payments - Future Sales',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = '',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -2
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-2,-1,'CI-Princip','Principal Payments - Future Sales',0,'',2,null,null,0,'','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-3)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -1,[CFG_CODE] = 'CI-SaleRec',[CFG_DESC] = 'Sales Receipts',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 4,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = '',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -3
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-3,-1,'CI-SaleRec','Sales Receipts',0,'',4,null,null,0,'','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-4)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -1,[CFG_CODE] = 'CI-MisRev',[CFG_DESC] = 'Misc. Revenue',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 5,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = '',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -4
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-4,-1,'CI-MisRev','Misc. Revenue',0,'',5,null,null,0,'','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-5)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -1,[CFG_CODE] = 'SPACE',[CFG_DESC] = '',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 6,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = '',[Totaling] = 'Do Not Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -5
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-5,-1,'SPACE','',0,'',6,null,null,0,'','Do Not Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-6)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -1,[CFG_CODE] = 'CI-Int',[CFG_DESC] = 'Interest',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 7,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = '',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -6
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-6,-1,'CI-Int','Interest',0,'',7,null,null,0,'','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-7)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -1,[CFG_CODE] = 'CI-Total',[CFG_DESC] = 'Total Cash In',[CFSG_REQUIRED] = 1,[CFSG_SQL] = '',[CFSG_SEQ] = 8,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'Single Over',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -7
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-7,-1,'CI-Total','Total Cash In',1,'',8,null,null,0,'Single Over','Is The Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-64)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -1,[CFG_CODE] = 'CI-PriorAR',[CFG_DESC] = 'Prior/Existing Receivables',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 3,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -64
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-64,-1,'CI-PriorAR','Prior/Existing Receivables',0,'',3,null,null,0,'None','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-8)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -2,[CFG_CODE] = 'CO-LC',[CFG_DESC] = 'Land Costs',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = '',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -8
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-8,-2,'CO-LC','Land Costs',0,'',1,null,null,0,'','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-9)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -2,[CFG_CODE] = 'CO-DC',[CFG_DESC] = 'Development Costs',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = '',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -9
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-9,-2,'CO-DC','Development Costs',0,'',2,null,null,0,'','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-10)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -2,[CFG_CODE] = 'CO-AP',[CFG_DESC] = 'Accounts Payable',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 3,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = '',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -10
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-10,-2,'CO-AP','Accounts Payable',0,'',3,null,null,0,'','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-11)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -2,[CFG_CODE] = 'CO-OtherAP',[CFG_DESC] = 'Other Accounts Payable',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 4,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = '',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -11
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-11,-2,'CO-OtherAP','Other Accounts Payable',0,'',4,null,null,0,'','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-12)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -2,[CFG_CODE] = 'CO-MiscExp',[CFG_DESC] = 'Misc. Expenses',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 5,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = '',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -12
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-12,-2,'CO-MiscExp','Misc. Expenses',0,'',5,null,null,0,'','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-13)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -2,[CFG_CODE] = 'CO-TotXFin',[CFG_DESC] = 'Total Before Financing',[CFSG_REQUIRED] = 1,[CFSG_SQL] = '',[CFSG_SEQ] = 6,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Over',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -13
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-13,-2,'CO-TotXFin','Total Before Financing',1,'',6,null,null,1,'Single Over','Is The Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-17)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -3,[CFG_CODE] = 'NNF',[CFG_DESC] = 'Net Cash Flow Excluding Financing',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = '',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -17
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-17,-3,'NNF','Net Cash Flow Excluding Financing',0,'',1,null,null,0,'','Is The Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-18)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -4,[CFG_CODE] = 'CCP',[CFG_DESC] = 'Cumulative Cash Postition Exc. Financing',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = '',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -18
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-18,-4,'CCP','Cumulative Cash Postition Exc. Financing',0,'',1,null,null,0,'','Is The Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-20)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -5,[CFG_CODE] = 'F-Draws',[CFG_DESC] = 'Financing Draws',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = '',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -20
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-20,-5,'F-Draws','Financing Draws',0,'',1,null,null,0,'','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-21)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -5,[CFG_CODE] = 'F-Repay',[CFG_DESC] = 'Financing Repayments',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = '',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -21
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-21,-5,'F-Repay','Financing Repayments',0,'',2,null,null,0,'','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-22)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -5,[CFG_CODE] = 'F-CCPIF',[CFG_DESC] = 'Cumulative Cash Position Inc. Financing',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 3,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Over, Double Under',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -22
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-22,-5,'F-CCPIF','Cumulative Cash Position Inc. Financing',0,'',3,null,null,1,'Single Over, Double Under','Is The Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-65)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -17,[CFG_CODE] = 'COF-FinInt',[CFG_DESC] = 'Financing Interest Costs',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -65
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-65,-17,'COF-FinInt','Financing Interest Costs',0,'',1,null,null,0,'None','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-66)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -17,[CFG_CODE] = 'COF-Total',[CFG_DESC] = 'Total Cash Out',[CFSG_REQUIRED] = 1,[CFSG_SQL] = '',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Over',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -66
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-66,-17,'COF-Total','Total Cash Out',1,'',2,null,null,1,'Single Over','Is The Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-24)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -6,[CFG_CODE] = 'A-AR',[CFG_DESC] = 'Agreements Receivable',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -24
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-24,-6,'A-AR','Agreements Receivable',0,'',1,null,null,0,'None','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-25)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -6,[CFG_CODE] = 'A-OAR',[CFG_DESC] = 'Other Receivables',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -25
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-25,-6,'A-OAR','Other Receivables',0,'',2,null,null,0,'None','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-26)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -6,[CFG_CODE] = 'A-LUD',[CFG_DESC] = 'Land Under Development',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 3,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -26
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-26,-6,'A-LUD','Land Under Development',0,'',3,null,null,0,'None','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-27)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -6,[CFG_CODE] = 'A-OA',[CFG_DESC] = 'Other Assets',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 4,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -27
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-27,-6,'A-OA','Other Assets',0,'',4,null,null,0,'None','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-28)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -6,[CFG_CODE] = 'A-CB',[CFG_DESC] = 'Cash In Bank',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 5,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -28
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-28,-6,'A-CB','Cash In Bank',0,'',5,null,null,0,'None','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-29)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -6,[CFG_CODE] = 'A-Total',[CFG_DESC] = 'Total Assets',[CFSG_REQUIRED] = 1,[CFSG_SQL] = '',[CFSG_SEQ] = 6,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Over, Double Under',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -29
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-29,-6,'A-Total','Total Assets',1,'',6,null,null,1,'Single Over, Double Under','Is The Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-30)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -7,[CFG_CODE] = 'E-NIC',[CFG_DESC] = 'Net Investment - Cash',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -30
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-30,-7,'E-NIC','Net Investment - Cash',0,'',1,null,null,0,'None','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-31)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -7,[CFG_CODE] = 'E-NIRE',[CFG_DESC] = 'Net Investment - Retained Earnings',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -31
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-31,-7,'E-NIRE','Net Investment - Retained Earnings',0,'',2,null,null,0,'None','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-32)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -7,[CFG_CODE] = 'E-Total',[CFG_DESC] = 'Total Equity',[CFSG_REQUIRED] = 1,[CFSG_SQL] = '',[CFSG_SEQ] = 3,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Over, Double Under',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -32
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-32,-7,'E-Total','Total Equity',1,'',3,null,null,1,'Single Over, Double Under','Is The Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-33)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -8,[CFG_CODE] = 'L-PDC',[CFG_DESC] = 'Provision For Development Costs',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = '',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -33
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-33,-8,'L-PDC','Provision For Development Costs',0,'',1,null,null,0,'','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-34)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -8,[CFG_CODE] = 'L-PFP',[CFG_DESC] = 'Project Financing Payable',[CFSG_REQUIRED] = 0,[CFSG_SQL] = '',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = '',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -34
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-34,-8,'L-PFP','Project Financing Payable',0,'',2,null,null,0,'','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-35)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -8,[CFG_CODE] = 'L-Total',[CFG_DESC] = 'Total Liabilities',[CFSG_REQUIRED] = 1,[CFSG_SQL] = '',[CFSG_SEQ] = 3,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Over, Double Under',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -35
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-35,-8,'L-Total','Total Liabilities',1,'',3,null,null,1,'Single Over, Double Under','Is The Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-23)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -9,[CFG_CODE] = 'Tot-LE',[CFG_DESC] = 'Total Liabilities & Equity',[CFSG_REQUIRED] = 1,[CFSG_SQL] = '',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Over, Double Under',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -23
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-23,-9,'Tot-LE','Total Liabilities & Equity',1,'',1,null,null,1,'Single Over, Double Under','',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-388)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -11,[CFG_CODE] = 'Misc-Rev',[CFG_DESC] = 'Add: Misc. Revenue',[CFSG_REQUIRED] = 1,[CFSG_SQL] = 'RE_Land_CF2_CI_MISC',[CFSG_SEQ] = 5,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_CFByItem (misc a/r projections)' where CFSG_ID = -388
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-388,-11,'Misc-Rev','Add: Misc. Revenue',1,'RE_Land_CF2_CI_MISC',5,null,null,0,'None','Include In Total',0,0,'Calls RE_Land_CF2_CI_CFByItem (misc a/r projections)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-387)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -11,[CFG_CODE] = 'BLANK',[CFG_DESC] = '',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -387
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-387,-11,'BLANK','',0,'RE_Land_CF2_BLANK',1,null,null,0,'None','',0,0,'')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-386)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -11,[CFG_CODE] = 'BLANK',[CFG_DESC] = 'Income Statement',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -386
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-386,-11,'BLANK','Income Statement',0,'RE_Land_CF2_BLANK',2,null,null,1,'None','',0,0,'')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-42)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -11,[CFG_CODE] = 'NetIncome',[CFG_DESC] = 'Net Project Income',[CFSG_REQUIRED] = 1,[CFSG_SQL] = 'RE_Land_IS1_NetIncome',[CFSG_SEQ] = 10,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Over, Double Under',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Sums net income' where CFSG_ID = -42
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-42,-11,'NetIncome','Net Project Income',1,'RE_Land_IS1_NetIncome',10,null,null,1,'Single Over, Double Under','Is The Total',0,0,'Sums net income')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-41)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -11,[CFG_CODE] = 'BLANK',[CFG_DESC] = '',[CFSG_REQUIRED] = 1,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 9,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Do Not Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -41
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-41,-11,'BLANK','',1,'RE_Land_CF2_BLANK',9,null,null,0,'None','Do Not Include In Total',0,0,'')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-40)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -11,[CFG_CODE] = 'COS',[CFG_DESC] = 'Cost Of Sales',[CFSG_REQUIRED] = 1,[CFSG_SQL] = 'RE_Land_IS1_COS',[CFSG_SEQ] = 8,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'Single Over',[Totaling] = 'Include In Total',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_CFByCost with !!!OverRide - IS Requirement!!! - uses cost codes for costing actuals and also includes management fees from GL' where CFSG_ID = -40
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-40,-11,'COS','Cost Of Sales',1,'RE_Land_IS1_COS',8,null,null,0,'Single Over','Include In Total',1,0,'Calls RE_Land_CF2_CI_CFByCost with !!!OverRide - IS Requirement!!! - uses cost codes for costing actuals and also includes management fees from GL')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-39)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -11,[CFG_CODE] = 'BLANK',[CFG_DESC] = '',[CFSG_REQUIRED] = 1,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 7,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Do Not Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -39
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-39,-11,'BLANK','',1,'RE_Land_CF2_BLANK',7,null,null,0,'None','Do Not Include In Total',0,0,'')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-38)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -11,[CFG_CODE] = 'Tot-Income',[CFG_DESC] = 'Total Income',[CFSG_REQUIRED] = 1,[CFSG_SQL] = 'RE_Land_IS1_TotalIncome',[CFSG_SEQ] = 6,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'Single Over',[Totaling] = 'Do Not Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Sums up total income' where CFSG_ID = -38
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-38,-11,'Tot-Income','Total Income',1,'RE_Land_IS1_TotalIncome',6,null,null,0,'Single Over','Do Not Include In Total',0,0,'Sums up total income')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-37)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -11,[CFG_CODE] = 'Int-Income',[CFG_DESC] = 'Add: Interest Income',[CFSG_REQUIRED] = 1,[CFSG_SQL] = 'RE_Land_IS1_Interest',[CFSG_SEQ] = 4,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Creates a 0 line' where CFSG_ID = -37
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-37,-11,'Int-Income','Add: Interest Income',1,'RE_Land_IS1_Interest',4,null,null,0,'None','Include In Total',0,0,'Creates a 0 line')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-274)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -78,[CFG_CODE] = 'CI-LD',[CFG_DESC] = 'Lot Deposits',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CI_LD',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -274
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-274,-78,'CI-LD','Lot Deposits',0,'RE_Land_CF2_CI_LD',2,null,null,0,'None','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-275)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -78,[CFG_CODE] = 'CI_LC',[CFG_DESC] = 'Lot Closings',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CI_LC',[CFSG_SEQ] = 3,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -275
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-275,-78,'CI_LC','Lot Closings',0,'RE_Land_CF2_CI_LC',3,null,null,0,'None','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-276)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -78,[CFG_CODE] = 'CI_MISC',[CFG_DESC] = 'Miscellaneous (Incl. Gst Returns)',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CI_MISC',[CFSG_SEQ] = 4,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -276
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-276,-78,'CI_MISC','Miscellaneous (Incl. Gst Returns)',0,'RE_Land_CF2_CI_MISC',4,null,null,0,'None','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-277)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -78,[CFG_CODE] = 'CI_NETCASH',[CFG_DESC] = 'Net Cash Inflow',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_Total',[CFSG_SEQ] = 5,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Over',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -277
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-277,-78,'CI_NETCASH','Net Cash Inflow',0,'RE_Land_CF2_Total',5,null,null,1,'Single Over','Is The Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-285)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -78,[CFG_CODE] = 'BLANK',[CFG_DESC] = 'Cash Inflow',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Under',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -285
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-285,-78,'BLANK','Cash Inflow',0,'RE_Land_CF2_BLANK',1,null,null,1,'Single Under','',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-278)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -79,[CFG_CODE] = 'CO-LP',[CFG_DESC] = 'Land Payments',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_LP',[CFSG_SEQ] = 3,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -278
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-278,-79,'CO-LP','Land Payments',0,'RE_Land_CF2_CO_LP',3,null,null,0,'None','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-279)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -79,[CFG_CODE] = 'CO-SFI',[CFG_DESC] = 'Self Financed Interest',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_SFI',[CFSG_SEQ] = 4,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -279
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-279,-79,'CO-SFI','Self Financed Interest',0,'RE_Land_CF2_CO_SFI',4,null,null,0,'None','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-280)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -79,[CFG_CODE] = 'CO-EXP',[CFG_DESC] = 'Expenses (Excl Gst)',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_EXP',[CFSG_SEQ] = 5,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -280
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-280,-79,'CO-EXP','Expenses (Excl Gst)',0,'RE_Land_CF2_CO_EXP',5,null,null,0,'None','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-281)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -79,[CFG_CODE] = 'CO-MAN',[CFG_DESC] = 'Management Fees (Excl GST)',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_MAN',[CFSG_SEQ] = 6,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -281
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-281,-79,'CO-MAN','Management Fees (Excl GST)',0,'RE_Land_CF2_CO_MAN',6,null,null,0,'None','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-282)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -79,[CFG_CODE] = 'CO-NETCASH',[CFG_DESC] = 'Net Cash Outflow',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_Total',[CFSG_SEQ] = 7,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'None',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -282
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-282,-79,'CO-NETCASH','Net Cash Outflow',0,'RE_Land_CF2_Total',7,null,null,1,'None','Is The Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-286)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -79,[CFG_CODE] = 'BLANK',[CFG_DESC] = 'Cash Outflow',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Under',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -286
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-286,-79,'BLANK','Cash Outflow',0,'RE_Land_CF2_BLANK',2,null,null,1,'Single Under','',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-287)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -79,[CFG_CODE] = 'BLANK',[CFG_DESC] = '',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -287
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-287,-79,'BLANK','',0,'RE_Land_CF2_BLANK',1,null,null,0,'None','',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-283)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -80,[CFG_CODE] = 'NCBPF',[CFG_DESC] = 'Net Cash Before Financing',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_NCBPF',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'Single Under',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -283
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-283,-80,'NCBPF','Net Cash Before Financing',0,'RE_Land_CF2_NCBPF',2,null,null,0,'Single Under','Is The Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-288)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -80,[CFG_CODE] = 'BLANK',[CFG_DESC] = '',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -288
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-288,-80,'BLANK','',0,'RE_Land_CF2_BLANK',1,null,null,0,'None','',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-284)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -81,[CFG_CODE] = 'BLANK',[CFG_DESC] = '',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -284
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-284,-81,'BLANK','',0,'RE_Land_CF2_BLANK',1,null,null,0,'None','',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-289)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -81,[CFG_CODE] = 'FIP-PS-CON',[CFG_DESC] = 'Partner Contibutions',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_FIP_PS_CON',[CFSG_SEQ] = 3,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -289
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-289,-81,'FIP-PS-CON','Partner Contibutions',0,'RE_Land_CF2_FIP_PS_CON',3,null,null,1,'None','',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-290)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -81,[CFG_CODE] = 'BLANK',[CFG_DESC] = 'Financing/Investment Proceeds',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_FIP_CALC_FINANCING',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Under',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -290
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-290,-81,'BLANK','Financing/Investment Proceeds',0,'RE_Land_CF2_FIP_CALC_FINANCING',2,null,null,1,'Single Under','',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-291)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -81,[CFG_CODE] = 'FIP-PS-DIS',[CFG_DESC] = 'Partner Distributions',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_FIP_PS_DIST',[CFSG_SEQ] = 4,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -291
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-291,-81,'FIP-PS-DIS','Partner Distributions',0,'RE_Land_CF2_FIP_PS_DIST',4,null,null,1,'None','',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-292)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -81,[CFG_CODE] = 'FIP-PS-LN',[CFG_DESC] = 'Loans',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_FIP_PS_LOAN',[CFSG_SEQ] = 5,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -292
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-292,-81,'FIP-PS-LN','Loans',0,'RE_Land_CF2_FIP_PS_LOAN',5,null,null,1,'None','',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-293)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -82,[CFG_CODE] = 'CCP',[CFG_DESC] = 'Cumulative Cash Position',[CFSG_REQUIRED] = 1,[CFSG_SQL] = 'RE_Land_CF2_CCP',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Over, Double Under',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -293
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-293,-82,'CCP','Cumulative Cash Position',1,'RE_Land_CF2_CCP',1,null,null,1,'Single Over, Double Under','Is The Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-360)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -99,[CFG_CODE] = 'CI-AD-MF',[CFG_DESC] = 'Management Fee Revenue',[CFSG_REQUIRED] = 1,[CFSG_SQL] = 'RE_Land_CFA_CI_MF',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -360
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-360,-99,'CI-AD-MF','Management Fee Revenue',1,'RE_Land_CFA_CI_MF',1,null,null,1,'None','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-362)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -99,[CFG_CODE] = 'CI-AD-TOT',[CFG_DESC] = '',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_Total',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Over',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -362
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-362,-99,'CI-AD-TOT','',0,'RE_Land_CF2_Total',2,null,null,1,'Single Over','Is The Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-361)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -100,[CFG_CODE] = 'BLANK',[CFG_DESC] = 'Expenses',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -361
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-361,-100,'BLANK','Expenses',0,'RE_Land_CF2_BLANK',2,null,null,1,'None','',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-363)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -100,[CFG_CODE] = 'BLANK',[CFG_DESC] = '',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -363
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-363,-100,'BLANK','',0,'RE_Land_CF2_BLANK',1,null,null,0,'None','',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-366)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -100,[CFG_CODE] = 'CO-AD-EXP',[CFG_DESC] = 'General And Administration',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CFA_CO_EXP',[CFSG_SEQ] = 3,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -366
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-366,-100,'CO-AD-EXP','General And Administration',0,'RE_Land_CFA_CO_EXP',3,null,null,0,'None','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-367)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -100,[CFG_CODE] = 'CO-AD-EXP',[CFG_DESC] = 'Rent',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CFA_CO_EXP',[CFSG_SEQ] = 4,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -367
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-367,-100,'CO-AD-EXP','Rent',0,'RE_Land_CFA_CO_EXP',4,null,null,0,'None','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-368)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -100,[CFG_CODE] = 'CO-AD-EXP',[CFG_DESC] = 'Operation',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CFA_CO_EXP',[CFSG_SEQ] = 5,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -368
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-368,-100,'CO-AD-EXP','Operation',0,'RE_Land_CFA_CO_EXP',5,null,null,0,'None','Include In Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-369)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -100,[CFG_CODE] = 'CO-AD-TOT',[CFG_DESC] = '',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_Total',[CFSG_SEQ] = 6,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'Single Over',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -369
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-369,-100,'CO-AD-TOT','',0,'RE_Land_CF2_Total',6,null,null,0,'Single Over','Is The Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-364)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -101,[CFG_CODE] = 'BLANK',[CFG_DESC] = '',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -364
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-364,-101,'BLANK','',0,'RE_Land_CF2_BLANK',1,null,null,0,'None','',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-365)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -101,[CFG_CODE] = 'TOT-AD',[CFG_DESC] = 'Net Cash Position',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CFA_NC',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Double Under',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -365
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-365,-101,'TOT-AD','Net Cash Position',0,'RE_Land_CFA_NC',2,null,null,1,'Double Under','Is The Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-370)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -101,[CFG_CODE] = 'BLANK',[CFG_DESC] = '',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 3,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -370
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-370,-101,'BLANK','',0,'RE_Land_CF2_BLANK',3,null,null,0,'None','',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-371)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -101,[CFG_CODE] = 'TOT-CCP',[CFG_DESC] = 'Cumulative Cash Position',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CFA_CCP',[CFSG_SEQ] = 4,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Over, Double Under',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,Notes = null where CFSG_ID = -371
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-371,-101,'TOT-CCP','Cumulative Cash Position',0,'RE_Land_CFA_CCP',4,null,null,1,'Single Over, Double Under','Is The Total',0,0,null)
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-490)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -171,[CFG_CODE] = 'CO-NETCASH',[CFG_DESC] = 'Net Cash Outflow',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_Total',[CFSG_SEQ] = 12,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'None',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -490
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-490,-171,'CO-NETCASH','Net Cash Outflow',0,'RE_Land_CF2_Total',12,null,null,1,'None','Is The Total',0,0,'')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-486)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -171,[CFG_CODE] = 'BLANK',[CFG_DESC] = 'Cash Outflow',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Under',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -486
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-486,-171,'BLANK','Cash Outflow',0,'RE_Land_CF2_BLANK',2,null,null,1,'Single Under','',0,0,'')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-492)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -171,[CFG_CODE] = 'CO-EXP',[CFG_DESC] = 'Misc. Expenses (Excl Gst)',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_MISC',[CFSG_SEQ] = 11,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 1,[Notes] = 'Calls RE_Land_CF2_CO_CFByGL uses GL for Actuals from GL/Subledger only' where CFSG_ID = -492
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-492,-171,'CO-EXP','Misc. Expenses (Excl Gst)',0,'RE_Land_CF2_CO_MISC',11,null,null,0,'None','Include In Total',0,1,'Calls RE_Land_CF2_CO_CFByGL uses GL for Actuals from GL/Subledger only')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-491)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -171,[CFG_CODE] = 'CO-MAN',[CFG_DESC] = 'Management Fees (Excl GST)',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_MAN',[CFSG_SEQ] = 8,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)' where CFSG_ID = -491
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-491,-171,'CO-MAN','Management Fees (Excl GST)',0,'RE_Land_CF2_CO_MAN',8,null,null,0,'None','Include In Total',1,0,'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-485)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -171,[CFG_CODE] = 'BLANK',[CFG_DESC] = '',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -485
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-485,-171,'BLANK','',0,'RE_Land_CF2_BLANK',1,null,null,0,'None','',0,0,'')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-489)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -170,[CFG_CODE] = 'NCBPF',[CFG_DESC] = 'Net Cash Before Financing',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_NCBPF',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'Single Under',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'creates a line for CO-NETCASH (Net before financing)' where CFSG_ID = -489
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-489,-170,'NCBPF','Net Cash Before Financing',0,'RE_Land_CF2_NCBPF',2,null,null,0,'Single Under','Is The Total',0,0,'creates a line for CO-NETCASH (Net before financing)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-484)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -170,[CFG_CODE] = 'BLANK',[CFG_DESC] = '',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -484
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-484,-170,'BLANK','',0,'RE_Land_CF2_BLANK',1,null,null,0,'None','',0,0,'')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-488)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -169,[CFG_CODE] = 'BLANK',[CFG_DESC] = '',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -488
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-488,-169,'BLANK','',0,'RE_Land_CF2_BLANK',1,null,null,0,'None','',0,0,'')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-483)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -169,[CFG_CODE] = 'FIP-PS-CON',[CFG_DESC] = 'Partner Contibutions',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_FIP_PS_CON',[CFSG_SEQ] = 3,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Generates a line for each share owners cash call' where CFSG_ID = -483
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-483,-169,'FIP-PS-CON','Partner Contibutions',0,'RE_Land_CF2_FIP_PS_CON',3,null,null,1,'None','',0,0,'Generates a line for each share owners cash call')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-482)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -169,[CFG_CODE] = 'BLANK',[CFG_DESC] = 'Financing/Investment Proceeds',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_FIP_CALC_FINANCING',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Under',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Calculates financing based on project financing setup(share owners, bank & Land Vendor)... creates interest line (also creates a table to be used by the next 3 subgroups below).' where CFSG_ID = -482
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-482,-169,'BLANK','Financing/Investment Proceeds',0,'RE_Land_CF2_FIP_CALC_FINANCING',2,null,null,1,'Single Under','',0,0,'Calculates financing based on project financing setup(share owners, bank & Land Vendor)... creates interest line (also creates a table to be used by the next 3 subgroups below).')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-481)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -169,[CFG_CODE] = 'FIP-PS-DIS',[CFG_DESC] = 'Partner Distributions',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_FIP_PS_DIST',[CFSG_SEQ] = 4,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Generates a line for each share owners cash distribution' where CFSG_ID = -481
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-481,-169,'FIP-PS-DIS','Partner Distributions',0,'RE_Land_CF2_FIP_PS_DIST',4,null,null,1,'None','',0,0,'Generates a line for each share owners cash distribution')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-480)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -169,[CFG_CODE] = 'FIP-PS-LN',[CFG_DESC] = 'Loans',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_FIP_PS_LOAN',[CFSG_SEQ] = 5,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Generates a line for banks (Line of credit) & VTB (Land vendor... min repay schedule for projections & VTB account/sub code)' where CFSG_ID = -480
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-480,-169,'FIP-PS-LN','Loans',0,'RE_Land_CF2_FIP_PS_LOAN',5,null,null,1,'None','',0,0,'Generates a line for banks (Line of credit) & VTB (Land vendor... min repay schedule for projections & VTB account/sub code)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-479)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -168,[CFG_CODE] = 'CCP',[CFG_DESC] = 'Cumulative Cash Position',[CFSG_REQUIRED] = 1,[CFSG_SQL] = 'RE_Land_CF2_CCP',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Over, Double Under',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Generates grand totals' where CFSG_ID = -479
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-479,-168,'CCP','Cumulative Cash Position',1,'RE_Land_CF2_CCP',1,null,null,1,'Single Over, Double Under','Is The Total',0,0,'Generates grand totals')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-473)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -167,[CFG_CODE] = 'CI_COSTS',[CFG_DESC] = 'Raw Land Interest',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CI_CostCodes',[CFSG_SEQ] = 8,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CashIN_CFByCostCode (uses cost code references to determine cash in from AR Invoices/GL)' where CFSG_ID = -473
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-473,-167,'CI_COSTS','Raw Land Interest',0,'RE_Land_CF2_CI_CostCodes',8,null,null,0,'None','',1,0,'Calls RE_Land_CF2_CashIN_CFByCostCode (uses cost code references to determine cash in from AR Invoices/GL)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-472)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -167,[CFG_CODE] = 'CI_COSTS',[CFG_DESC] = 'Raw Land Rental',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CI_CostCodes',[CFSG_SEQ] = 7,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CashIN_CFByCostCode (uses cost code references to determine cash in from AR Invoices/GL)' where CFSG_ID = -472
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-472,-167,'CI_COSTS','Raw Land Rental',0,'RE_Land_CF2_CI_CostCodes',7,null,null,0,'None','',1,0,'Calls RE_Land_CF2_CashIN_CFByCostCode (uses cost code references to determine cash in from AR Invoices/GL)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-466)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -167,[CFG_CODE] = 'CI_NETCASH',[CFG_DESC] = 'Net Cash Inflow',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_Total',[CFSG_SEQ] = 10,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Over',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -466
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-466,-167,'CI_NETCASH','Net Cash Inflow',0,'RE_Land_CF2_Total',10,null,null,1,'Single Over','Is The Total',0,0,'')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-458)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -167,[CFG_CODE] = 'BLANK',[CFG_DESC] = 'Cash Inflow',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Under',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -458
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-458,-167,'BLANK','Cash Inflow',0,'RE_Land_CF2_BLANK',1,null,null,1,'Single Under','',0,0,'')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-478)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -166,[CFG_CODE] = 'CO-GEN',[CFG_DESC] = 'General',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_General',[CFSG_SEQ] = 9,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)' where CFSG_ID = -478
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-478,-166,'CO-GEN','General',0,'RE_Land_CF2_CO_General',9,null,null,0,'None','',1,0,'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-475)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -166,[CFG_CODE] = 'CO-CON',[CFG_DESC] = 'Consulting',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_Consulting',[CFSG_SEQ] = 5,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)' where CFSG_ID = -475
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-475,-166,'CO-CON','Consulting',0,'RE_Land_CF2_CO_Consulting',5,null,null,0,'None','',1,0,'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-474)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -166,[CFG_CODE] = 'CO_PT',[CFG_DESC] = 'Property Taxes',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_PropertyTaxes',[CFSG_SEQ] = 4,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)' where CFSG_ID = -474
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-474,-166,'CO_PT','Property Taxes',0,'RE_Land_CF2_CO_PropertyTaxes',4,null,null,0,'None','',1,0,'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-465)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -166,[CFG_CODE] = 'CO-LP',[CFG_DESC] = 'Land Cost',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_LP',[CFSG_SEQ] = 3,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)' where CFSG_ID = -465
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-465,-166,'CO-LP','Land Cost',0,'RE_Land_CF2_CO_LP',3,null,null,0,'None','Include In Total',1,0,'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-464)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -166,[CFG_CODE] = 'CO-SFI',[CFG_DESC] = 'Financing Costs',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_SFI',[CFSG_SEQ] = 10,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)' where CFSG_ID = -464
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-464,-166,'CO-SFI','Financing Costs',0,'RE_Land_CF2_CO_SFI',10,null,null,0,'None','Include In Total',1,0,'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-463)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -166,[CFG_CODE] = 'CO-EXP',[CFG_DESC] = 'Misc. Expenses (Excl Gst)',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_MISC',[CFSG_SEQ] = 11,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 1,[Notes] = 'Calls RE_Land_CF2_CO_CFByGL uses GL for Actuals from GL/Subledger only' where CFSG_ID = -463
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-463,-166,'CO-EXP','Misc. Expenses (Excl Gst)',0,'RE_Land_CF2_CO_MISC',11,null,null,0,'None','Include In Total',0,1,'Calls RE_Land_CF2_CO_CFByGL uses GL for Actuals from GL/Subledger only')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-462)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -166,[CFG_CODE] = 'CO-MAN',[CFG_DESC] = 'Management Fees (Excl GST)',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_CO_MAN',[CFSG_SEQ] = 8,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 1,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)' where CFSG_ID = -462
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-462,-166,'CO-MAN','Management Fees (Excl GST)',0,'RE_Land_CF2_CO_MAN',8,null,null,0,'None','Include In Total',1,0,'Calls RE_Land_CF2_CI_CFByCost uses cost codes for cost actuals & projections (also if group code = CO_MAN, gets I/S man fees)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-461)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -166,[CFG_CODE] = 'CO-NETCASH',[CFG_DESC] = 'Net Cash Outflow',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_Total',[CFSG_SEQ] = 12,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'None',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -461
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-461,-166,'CO-NETCASH','Net Cash Outflow',0,'RE_Land_CF2_Total',12,null,null,1,'None','Is The Total',0,0,'')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-457)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -166,[CFG_CODE] = 'BLANK',[CFG_DESC] = 'Cash Outflow',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Under',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -457
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-457,-166,'BLANK','Cash Outflow',0,'RE_Land_CF2_BLANK',2,null,null,1,'Single Under','',0,0,'')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-456)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -166,[CFG_CODE] = 'BLANK',[CFG_DESC] = '',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -456
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-456,-166,'BLANK','',0,'RE_Land_CF2_BLANK',1,null,null,0,'None','',0,0,'')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-460)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -165,[CFG_CODE] = 'NCBPF',[CFG_DESC] = 'Net Cash Before Financing',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_NCBPF',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'Single Under',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'creates a line for CO-NETCASH (Net before financing)' where CFSG_ID = -460
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-460,-165,'NCBPF','Net Cash Before Financing',0,'RE_Land_CF2_NCBPF',2,null,null,0,'Single Under','Is The Total',0,0,'creates a line for CO-NETCASH (Net before financing)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-455)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -165,[CFG_CODE] = 'BLANK',[CFG_DESC] = '',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -455
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-455,-165,'BLANK','',0,'RE_Land_CF2_BLANK',1,null,null,0,'None','',0,0,'')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-459)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -164,[CFG_CODE] = 'BLANK',[CFG_DESC] = '',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -459
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-459,-164,'BLANK','',0,'RE_Land_CF2_BLANK',1,null,null,0,'None','',0,0,'')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-454)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -164,[CFG_CODE] = 'FIP-PS-CON',[CFG_DESC] = 'Partner Contibutions',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_FIP_PS_CON',[CFSG_SEQ] = 3,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Generates a line for each share owners cash call' where CFSG_ID = -454
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-454,-164,'FIP-PS-CON','Partner Contibutions',0,'RE_Land_CF2_FIP_PS_CON',3,null,null,1,'None','',0,0,'Generates a line for each share owners cash call')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-453)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -164,[CFG_CODE] = 'BLANK',[CFG_DESC] = 'Financing/Investment Proceeds',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_FIP_CALC_FINANCING',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Under',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Calculates financing based on project financing setup(share owners, bank & Land Vendor)... creates interest line (also creates a table to be used by the next 3 subgroups below).' where CFSG_ID = -453
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-453,-164,'BLANK','Financing/Investment Proceeds',0,'RE_Land_CF2_FIP_CALC_FINANCING',2,null,null,1,'Single Under','',0,0,'Calculates financing based on project financing setup(share owners, bank & Land Vendor)... creates interest line (also creates a table to be used by the next 3 subgroups below).')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-452)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -164,[CFG_CODE] = 'FIP-PS-DIS',[CFG_DESC] = 'Partner Distributions',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_FIP_PS_DIST',[CFSG_SEQ] = 4,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Generates a line for each share owners cash distribution' where CFSG_ID = -452
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-452,-164,'FIP-PS-DIS','Partner Distributions',0,'RE_Land_CF2_FIP_PS_DIST',4,null,null,1,'None','',0,0,'Generates a line for each share owners cash distribution')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-451)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -164,[CFG_CODE] = 'FIP-PS-LN',[CFG_DESC] = 'Loans',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_FIP_PS_LOAN',[CFSG_SEQ] = 5,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Generates a line for banks (Line of credit) & VTB (Land vendor... min repay schedule for projections & VTB account/sub code)' where CFSG_ID = -451
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-451,-164,'FIP-PS-LN','Loans',0,'RE_Land_CF2_FIP_PS_LOAN',5,null,null,1,'None','',0,0,'Generates a line for banks (Line of credit) & VTB (Land vendor... min repay schedule for projections & VTB account/sub code)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-450)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -163,[CFG_CODE] = 'CCP',[CFG_DESC] = 'Cumulative Cash Position',[CFSG_REQUIRED] = 1,[CFSG_SQL] = 'RE_Land_CF2_CCP',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'Single Over, Double Under',[Totaling] = 'Is The Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Generates grand totals' where CFSG_ID = -450
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-450,-163,'CCP','Cumulative Cash Position',1,'RE_Land_CF2_CCP',1,null,null,1,'Single Over, Double Under','Is The Total',0,0,'Generates grand totals')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-36)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -11,[CFG_CODE] = 'Sales',[CFG_DESC] = 'Sales',[CFSG_REQUIRED] = 1,[CFSG_SQL] = 'RE_Land_IS1_Sales',[CFSG_SEQ] = 3,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = 'Include In Total',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'Calls RE_Land_CF2_CI_AgreementTransactions if not already called' where CFSG_ID = -36
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-36,-11,'Sales','Sales',1,'RE_Land_IS1_Sales',3,null,null,0,'None','Include In Total',0,0,'Calls RE_Land_CF2_CI_AgreementTransactions if not already called')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-385)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -10,[CFG_CODE] = 'SP-CALC',[CFG_DESC] = '',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_IS1_LP',[CFSG_SEQ] = 2,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 0,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = 'calls RE_Land_CF2_CI_AgreementTransactions (to get land sales $ and # and projections... revenue)' where CFSG_ID = -385
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-385,-10,'SP-CALC','',0,'RE_Land_IS1_LP',2,null,null,0,'None','',0,0,'calls RE_Land_CF2_CI_AgreementTransactions (to get land sales $ and # and projections... revenue)')
end
go
if exists(select * from RE_CashFlow_SubGroup where CFSG_ID =-384)
begin
update RE_CashFlow_SubGroup set [CFG_ID] = -10,[CFG_CODE] = 'SP',[CFG_DESC] = 'Sales Projections',[CFSG_REQUIRED] = 0,[CFSG_SQL] = 'RE_Land_CF2_BLANK',[CFSG_SEQ] = 1,OLD_CFSG_ID = null,OLD_CFG_ID = null,[Bold] = 1,[UnderLining] = 'None',[Totaling] = '',[CostCodeApplicable] = 0,[GLApplicable] = 0,[Notes] = '' where CFSG_ID = -384
end
else
begin
insert into RE_CashFlow_SubGroup([CFSG_ID],[CFG_ID],[CFG_CODE],[CFG_DESC],[CFSG_REQUIRED],[CFSG_SQL],[CFSG_SEQ],[OLD_CFSG_ID],[OLD_CFG_ID],[Bold],[UnderLining],[Totaling],[CostCodeApplicable],[GLApplicable],[Notes])values(-384,-10,'SP','Sales Projections',0,'RE_Land_CF2_BLANK',1,null,null,1,'None','',0,0,'')
end
go
set identity_insert RE_CashFlow_SubGroup off 
