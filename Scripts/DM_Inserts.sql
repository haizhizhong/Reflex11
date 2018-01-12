go
if exists(select * from CFS_LinkKey where id =1)
begin
update CFS_LinkKey set [TableDotField] = 'CUSTOMERS.CUSTOMER_ID',[Description] = 'Customer' where id = 1
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('CUSTOMERS.CUSTOMER_ID','Customer')
end
go
if exists(select * from CFS_LinkKey where id =2)
begin
update CFS_LinkKey set [TableDotField] = 'SUPPLIER_MASTER.SUPPLIER_ID',[Description] = 'Supplier' where id = 2
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('SUPPLIER_MASTER.SUPPLIER_ID','Supplier')
end
go
if exists(select * from CFS_LinkKey where id =3)
begin
update CFS_LinkKey set [TableDotField] = 'EQUIP_ID.EQI_ID',[Description] = 'Fixed Assets' where id = 3
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('EQUIP_ID.EQI_ID','Fixed Assets')
end
go
if exists(select * from CFS_LinkKey where id =4)
begin
update CFS_LinkKey set [TableDotField] = 'CONTACTHISTORY.ID',[Description] = 'Contact Management Sent Email' where id = 4
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('CONTACTHISTORY.ID','Contact Management Sent Email')
end
go
if exists(select * from CFS_LinkKey where id =5)
begin
update CFS_LinkKey set [TableDotField] = 'CONTACTHISTORY.ID',[Description] = 'Contact Management Fax' where id = 5
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('CONTACTHISTORY.ID','Contact Management Fax')
end
go
if exists(select * from CFS_LinkKey where id =6)
begin
update CFS_LinkKey set [TableDotField] = 'CONTACTHISTORY.ID',[Description] = 'Contact Management Appointment' where id = 6
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('CONTACTHISTORY.ID','Contact Management Appointment')
end
go
if exists(select * from CFS_LinkKey where id =7)
begin
update CFS_LinkKey set [TableDotField] = 'CONTACTHISTORY.ID',[Description] = 'Contact Management Task' where id = 7
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('CONTACTHISTORY.ID','Contact Management Task')
end
go
if exists(select * from CFS_LinkKey where id =8)
begin
update CFS_LinkKey set [TableDotField] = 'NOTE.NOTE_ID',[Description] = 'Contact Management Notes' where id = 8
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('NOTE.NOTE_ID','Contact Management Notes')
end
go
if exists(select * from CFS_LinkKey where id =9)
begin
update CFS_LinkKey set [TableDotField] = 'CONTACTHISTORY.ID',[Description] = 'Contact Management Phone' where id = 9
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('CONTACTHISTORY.ID','Contact Management Phone')
end
go
if exists(select * from CFS_LinkKey where id =10)
begin
update CFS_LinkKey set [TableDotField] = 'CONTACTHISTORY.ID',[Description] = 'Contact Management Visit' where id = 10
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('CONTACTHISTORY.ID','Contact Management Visit')
end
go
if exists(select * from CFS_LinkKey where id =11)
begin
update CFS_LinkKey set [TableDotField] = 'CONTACTHISTORY.ID',[Description] = 'Contact Management Letter (NOT USED)' where id = 11
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('CONTACTHISTORY.ID','Contact Management Letter (NOT USED)')
end
go
if exists(select * from CFS_LinkKey where id =12)
begin
update CFS_LinkKey set [TableDotField] = 'EST_HEADER.ID',[Description] = 'Estimate Header (ALL TYPES)' where id = 12
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('EST_HEADER.ID','Estimate Header (ALL TYPES)')
end
go
if exists(select * from CFS_LinkKey where id =13)
begin
update CFS_LinkKey set [TableDotField] = 'ESTIMATE_TREE.ID',[Description] = 'Estimate Detail Level 1 (ALL TYPES)' where id = 13
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('ESTIMATE_TREE.ID','Estimate Detail Level 1 (ALL TYPES)')
end
go
if exists(select * from CFS_LinkKey where id =14)
begin
update CFS_LinkKey set [TableDotField] = 'ESTIMATE_TREE.ID',[Description] = 'Estimate Detail Level 2 (ALL TYPES)' where id = 14
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('ESTIMATE_TREE.ID','Estimate Detail Level 2 (ALL TYPES)')
end
go
if exists(select * from CFS_LinkKey where id =15)
begin
update CFS_LinkKey set [TableDotField] = 'ESTIMATE_TREE.ID',[Description] = 'Estimate Detail Level 3 (ALL TYPES)' where id = 15
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('ESTIMATE_TREE.ID','Estimate Detail Level 3 (ALL TYPES)')
end
go
if exists(select * from CFS_LinkKey where id =16)
begin
update CFS_LinkKey set [TableDotField] = 'ESTIMATE_TREE.ID',[Description] = 'Estimate Detail Level 4 (ALL TYPES)' where id = 16
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('ESTIMATE_TREE.ID','Estimate Detail Level 4 (ALL TYPES)')
end
go
if exists(select * from CFS_LinkKey where id =17)
begin
update CFS_LinkKey set [TableDotField] = 'ESTIMATE_TREE.ID',[Description] = 'Estimate Detail Take Off (ALL TYPES)' where id = 17
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('ESTIMATE_TREE.ID','Estimate Detail Take Off (ALL TYPES)')
end
go
if exists(select * from CFS_LinkKey where id =18)
begin
update CFS_LinkKey set [TableDotField] = 'PROJ_HEADER.PRI_ID',[Description] = 'Project General Construction' where id = 18
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('PROJ_HEADER.PRI_ID','Project General Construction')
end
go
if exists(select * from CFS_LinkKey where id =19)
begin
update CFS_LinkKey set [TableDotField] = 'PROJ_HEADER.PRI_ID',[Description] = 'Project Land Construction' where id = 19
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('PROJ_HEADER.PRI_ID','Project Land Construction')
end
go
if exists(select * from CFS_LinkKey where id =20)
begin
update CFS_LinkKey set [TableDotField] = 'PROJ_HEADER.PRI_ID',[Description] = 'Project Unit Construction' where id = 20
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('PROJ_HEADER.PRI_ID','Project Unit Construction')
end
go
if exists(select * from CFS_LinkKey where id =21)
begin
update CFS_LinkKey set [TableDotField] = 'PROJ_HEADER.PRI_ID',[Description] = 'Project Homebuilder Construction' where id = 21
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('PROJ_HEADER.PRI_ID','Project Homebuilder Construction')
end
go
if exists(select * from CFS_LinkKey where id =22)
begin
update CFS_LinkKey set [TableDotField] = 'WO_SERVICEORDER_REQUEST.ID',[Description] = 'Service Order Clipboard Header' where id = 22
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('WO_SERVICEORDER_REQUEST.ID','Service Order Clipboard Header')
end
go
if exists(select * from CFS_LinkKey where id =23)
begin
update CFS_LinkKey set [TableDotField] = 'WO_SO_RQ_DETAILS.ID',[Description] = 'Service Order Clipboard Details' where id = 23
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('WO_SO_RQ_DETAILS.ID','Service Order Clipboard Details')
end
go
if exists(select * from CFS_LinkKey where id =24)
begin
update CFS_LinkKey set [TableDotField] = 'WO_SERVICEORDER_REQUEST.ID',[Description] = 'Installation Order Clipboard Header' where id = 24
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('WO_SERVICEORDER_REQUEST.ID','Installation Order Clipboard Header')
end
go
if exists(select * from CFS_LinkKey where id =25)
begin
update CFS_LinkKey set [TableDotField] = 'WO_SO_RQ_DETAILS.ID',[Description] = 'Installation Order Clipboard Details' where id = 25
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('WO_SO_RQ_DETAILS.ID','Installation Order Clipboard Details')
end
go
if exists(select * from CFS_LinkKey where id =26)
begin
update CFS_LinkKey set [TableDotField] = 'WO_SERVICEORDER_REQUEST.ID',[Description] = 'Build For Order Clipboard Header' where id = 26
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('WO_SERVICEORDER_REQUEST.ID','Build For Order Clipboard Header')
end
go
if exists(select * from CFS_LinkKey where id =27)
begin
update CFS_LinkKey set [TableDotField] = 'WO_SO_RQ_DETAILS.ID',[Description] = 'Build For Order Clipboard Detail' where id = 27
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('WO_SO_RQ_DETAILS.ID','Build For Order Clipboard Detail')
end
go
if exists(select * from CFS_LinkKey where id =28)
begin
update CFS_LinkKey set [TableDotField] = 'WO_SERVICEORDER_REQUEST.ID',[Description] = 'Service Order Quote Header' where id = 28
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('WO_SERVICEORDER_REQUEST.ID','Service Order Quote Header')
end
go
if exists(select * from CFS_LinkKey where id =29)
begin
update CFS_LinkKey set [TableDotField] = 'WO_SO_RQ_DETAILS.ID',[Description] = 'Service Order Quote Details' where id = 29
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('WO_SO_RQ_DETAILS.ID','Service Order Quote Details')
end
go
if exists(select * from CFS_LinkKey where id =30)
begin
update CFS_LinkKey set [TableDotField] = 'WO_SERVICEORDER_REQUEST.ID',[Description] = 'Installation Order Quote Header' where id = 30
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('WO_SERVICEORDER_REQUEST.ID','Installation Order Quote Header')
end
go
if exists(select * from CFS_LinkKey where id =31)
begin
update CFS_LinkKey set [TableDotField] = 'WO_SO_RQ_DETAILS.ID',[Description] = 'Installation Order Quote Details' where id = 31
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('WO_SO_RQ_DETAILS.ID','Installation Order Quote Details')
end
go
if exists(select * from CFS_LinkKey where id =32)
begin
update CFS_LinkKey set [TableDotField] = 'WO_SERVICEORDER_REQUEST.ID',[Description] = 'Build For Order Quote Header' where id = 32
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('WO_SERVICEORDER_REQUEST.ID','Build For Order Quote Header')
end
go
if exists(select * from CFS_LinkKey where id =33)
begin
update CFS_LinkKey set [TableDotField] = 'WO_SO_RQ_DETAILS.ID',[Description] = 'Build For Order Quote Detail' where id = 33
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('WO_SO_RQ_DETAILS.ID','Build For Order Quote Detail')
end
go
if exists(select * from CFS_LinkKey where id =34)
begin
update CFS_LinkKey set [TableDotField] = 'PROJ_HEADER.PRI_ID',[Description] = 'Build For Order' where id = 34
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('PROJ_HEADER.PRI_ID','Build For Order')
end
go
if exists(select * from CFS_LinkKey where id =35)
begin
update CFS_LinkKey set [TableDotField] = 'PROJ_HEADER.PRI_ID',[Description] = 'Build For Stock' where id = 35
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('PROJ_HEADER.PRI_ID','Build For Stock')
end
go
if exists(select * from CFS_LinkKey where id =36)
begin
update CFS_LinkKey set [TableDotField] = 'PROJ_HEADER.PRI_ID',[Description] = 'Service Order' where id = 36
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('PROJ_HEADER.PRI_ID','Service Order')
end
go
if exists(select * from CFS_LinkKey where id =37)
begin
update CFS_LinkKey set [TableDotField] = 'PROJ_HEADER.PRI_ID',[Description] = 'Installation Order' where id = 37
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('PROJ_HEADER.PRI_ID','Installation Order')
end
go
if exists(select * from CFS_LinkKey where id =38)
begin
update CFS_LinkKey set [TableDotField] = 'PROJ_HEADER.PRI_ID',[Description] = 'Asset Maintenance Order' where id = 38
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('PROJ_HEADER.PRI_ID','Asset Maintenance Order')
end
go
if exists(select * from CFS_LinkKey where id =39)
begin
update CFS_LinkKey set [TableDotField] = 'PROJ_HEADER.PRI_ID',[Description] = 'Property Maintenance Order' where id = 39
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('PROJ_HEADER.PRI_ID','Property Maintenance Order')
end
go
if exists(select * from CFS_LinkKey where id =40)
begin
update CFS_LinkKey set [TableDotField] = 'RE_PROPERTY_HEAD.PROPERTYHEAD_ID',[Description] = 'Residential Property' where id = 40
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('RE_PROPERTY_HEAD.PROPERTYHEAD_ID','Residential Property')
end
go
if exists(select * from CFS_LinkKey where id =41)
begin
update CFS_LinkKey set [TableDotField] = 'RE_PROPERTYSPACEUNITS.PROPERTYSPACEUNITS_ID',[Description] = 'Residential Unit' where id = 41
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('RE_PROPERTYSPACEUNITS.PROPERTYSPACEUNITS_ID','Residential Unit')
end
go
if exists(select * from CFS_LinkKey where id =42)
begin
update CFS_LinkKey set [TableDotField] = 'RE_LEASE_HEAD.LEASE_HEAD_ID',[Description] = 'Residential Lease' where id = 42
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('RE_LEASE_HEAD.LEASE_HEAD_ID','Residential Lease')
end
go
if exists(select * from CFS_LinkKey where id =43)
begin
update CFS_LinkKey set [TableDotField] = 'RE_PROPERTY_HEAD.PROPERTYHEAD_ID',[Description] = 'Commercial Property' where id = 43
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('RE_PROPERTY_HEAD.PROPERTYHEAD_ID','Commercial Property')
end
go
if exists(select * from CFS_LinkKey where id =44)
begin
update CFS_LinkKey set [TableDotField] = 'RE_PROPERTYSPACEUNITS.PROPERTYSPACEUNITS_ID',[Description] = 'Commercial Space' where id = 44
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('RE_PROPERTYSPACEUNITS.PROPERTYSPACEUNITS_ID','Commercial Space')
end
go
if exists(select * from CFS_LinkKey where id =45)
begin
update CFS_LinkKey set [TableDotField] = 'RE_LEASE_HEAD.LEASE_HEAD_ID',[Description] = 'Commercial Lease' where id = 45
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('RE_LEASE_HEAD.LEASE_HEAD_ID','Commercial Lease')
end
go
if exists(select * from CFS_LinkKey where id =46)
begin
update CFS_LinkKey set [TableDotField] = 'SO_MASTER_HDR.SO_ID',[Description] = 'Sales Quote Header' where id = 46
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('SO_MASTER_HDR.SO_ID','Sales Quote Header')
end
go
if exists(select * from CFS_LinkKey where id =47)
begin
update CFS_LinkKey set [TableDotField] = 'SO_MASTER_DETAIL.SO_LINE_ID',[Description] = 'Sales Quote Detail Line' where id = 47
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('SO_MASTER_DETAIL.SO_LINE_ID','Sales Quote Detail Line')
end
go
if exists(select * from CFS_LinkKey where id =48)
begin
update CFS_LinkKey set [TableDotField] = 'SO_MASTER_HDR.SO_ID',[Description] = 'Sales Order Header' where id = 48
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('SO_MASTER_HDR.SO_ID','Sales Order Header')
end
go
if exists(select * from CFS_LinkKey where id =49)
begin
update CFS_LinkKey set [TableDotField] = 'SO_MASTER_DETAIL.SO_LINE_ID',[Description] = 'Sales Order Detail Line' where id = 49
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('SO_MASTER_DETAIL.SO_LINE_ID','Sales Order Detail Line')
end
go
if exists(select * from CFS_LinkKey where id =50)
begin
update CFS_LinkKey set [TableDotField] = 'PO_HEADER.PO_ID',[Description] = 'PO Header' where id = 50
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('PO_HEADER.PO_ID','PO Header')
end
go
if exists(select * from CFS_LinkKey where id =51)
begin
update CFS_LinkKey set [TableDotField] = 'EMPLOYEE.EMP_NO',[Description] = 'Employee' where id = 51
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('EMPLOYEE.EMP_NO','Employee')
end
go
if exists(select * from CFS_LinkKey where id =52)
begin
update CFS_LinkKey set [TableDotField] = 'SO_TRN_HDR.SO_TRN_ID',[Description] = 'Billing Release Manual Invoice' where id = 52
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('SO_TRN_HDR.SO_TRN_ID','Billing Release Manual Invoice')
end
go
if exists(select * from CFS_LinkKey where id =53)
begin
update CFS_LinkKey set [TableDotField] = 'SO_TRN_HDR.SO_TRN_ID',[Description] = 'Billing Release Sales Order Billing' where id = 53
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('SO_TRN_HDR.SO_TRN_ID','Billing Release Sales Order Billing')
end
go
if exists(select * from CFS_LinkKey where id =54)
begin
update CFS_LinkKey set [TableDotField] = 'SO_TRN_HDR.SO_TRN_ID',[Description] = 'Billing Release Service/Installation Order Billing' where id = 54
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('SO_TRN_HDR.SO_TRN_ID','Billing Release Service/Installation Order Billing')
end
go
if exists(select * from CFS_LinkKey where id =55)
begin
update CFS_LinkKey set [TableDotField] = 'SO_MASTER_HDR.SO_ID',[Description] = 'SO Buyin Release' where id = 55
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('SO_MASTER_HDR.SO_ID','SO Buyin Release')
end
go
if exists(select * from CFS_LinkKey where id =56)
begin
update CFS_LinkKey set [TableDotField] = 'AP_INV_HEADER.AP_INV_HEADER_ID',[Description] = 'AP Invoice' where id = 56
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('AP_INV_HEADER.AP_INV_HEADER_ID','AP Invoice')
end
go
if exists(select * from CFS_LinkKey where id =57)
begin
update CFS_LinkKey set [TableDotField] = 'SO_MASTER_HDR.SO_ID',[Description] = 'SO RMA' where id = 57
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('SO_MASTER_HDR.SO_ID','SO RMA')
end
go
if exists(select * from CFS_LinkKey where id =58)
begin
update CFS_LinkKey set [TableDotField] = 'PO_HEADER.PO_ID',[Description] = 'PO RMA' where id = 58
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('PO_HEADER.PO_ID','PO RMA')
end
go
if exists(select * from CFS_LinkKey where id =59)
begin
update CFS_LinkKey set [TableDotField] = 'SO_MASTER_HDR.SO_ID',[Description] = 'Bill Of Lading' where id = 59
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('SO_MASTER_HDR.SO_ID','Bill Of Lading')
end
go
if exists(select * from CFS_LinkKey where id =60)
begin
update CFS_LinkKey set [TableDotField] = 'SO_MASTER_HDR.SO_ID',[Description] = 'Packing Slip' where id = 60
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('SO_MASTER_HDR.SO_ID','Packing Slip')
end
go
if exists(select * from CFS_LinkKey where id =61)
begin
update CFS_LinkKey set [TableDotField] = 'SO_MASTER_HDR.SO_ID',[Description] = 'SO Pick Slip' where id = 61
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('SO_MASTER_HDR.SO_ID','SO Pick Slip')
end
go
if exists(select * from CFS_LinkKey where id =62)
begin
update CFS_LinkKey set [TableDotField] = 'PO_HEADER.PO_ID',[Description] = 'PO Receiving' where id = 62
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('PO_HEADER.PO_ID','PO Receiving')
end
go
if exists(select * from CFS_LinkKey where id =63)
begin
update CFS_LinkKey set [TableDotField] = 'RM_OPPORTUNITY.RM_OPPORTUNITY_ID',[Description] = 'Opportunity' where id = 63
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('RM_OPPORTUNITY.RM_OPPORTUNITY_ID','Opportunity')
end
go
if exists(select * from CFS_LinkKey where id =64)
begin
update CFS_LinkKey set [TableDotField] = 'PROJ_LOT.PROJ_LOT_ID',[Description] = 'Land Lot' where id = 64
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('PROJ_LOT.PROJ_LOT_ID','Land Lot')
end
go
if exists(select * from CFS_LinkKey where id =65)
begin
update CFS_LinkKey set [TableDotField] = 'PROJ_LOT_AGREEMENT.AGREEMENT_ID',[Description] = 'Land Agreement' where id = 65
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('PROJ_LOT_AGREEMENT.AGREEMENT_ID','Land Agreement')
end
go
if exists(select * from CFS_LinkKey where id =66)
begin
update CFS_LinkKey set [TableDotField] = 'AP_PAY_HEADER.AP_PAY_HEADER_ID',[Description] = 'AP Check Processing' where id = 66
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('AP_PAY_HEADER.AP_PAY_HEADER_ID','AP Check Processing')
end
go
if exists(select * from CFS_LinkKey where id =67)
begin
update CFS_LinkKey set [TableDotField] = 'SUPPLIER_SUBCON_COMPLIANCE.ID',[Description] = 'Supplier Master - Subcontractor Compliance' where id = 67
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('SUPPLIER_SUBCON_COMPLIANCE.ID','Supplier Master - Subcontractor Compliance')
end
go
if exists(select * from CFS_LinkKey where id =68)
begin
update CFS_LinkKey set [TableDotField] = 'AR_PAYMENT_HEADERS.DEPOSIT_NO',[Description] = 'AR Payment Batch' where id = 68
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('AR_PAYMENT_HEADERS.DEPOSIT_NO','AR Payment Batch')
end
go
if exists(select * from CFS_LinkKey where id =69)
begin
update CFS_LinkKey set [TableDotField] = '',[Description] = 'Purchase Requisition' where id = 69
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('','Purchase Requisition')
end
go
if exists(select * from CFS_LinkKey where id =70)
begin
update CFS_LinkKey set [TableDotField] = 'PPM_Meeting.ID',[Description] = 'Meeting Agenda/Minutes' where id = 70
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('PPM_Meeting.ID','Meeting Agenda/Minutes')
end
go
if exists(select * from CFS_LinkKey where id =71)
begin
update CFS_LinkKey set TableDotField = null,[Description] = 'Web Payment Request Session' where id = 71
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values(null,'Web Payment Request Session')
end
go
if exists(select * from CFS_LinkKey where id =72)
begin
update CFS_LinkKey set TableDotField = null,[Description] = 'Web Payment Request Live' where id = 72
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values(null,'Web Payment Request Live')
end
go
if exists(select * from CFS_LinkKey where id =73)
begin
update CFS_LinkKey set TableDotField = null,[Description] = 'Web PO Session' where id = 73
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values(null,'Web PO Session')
end
go
if exists(select * from CFS_LinkKey where id =74)
begin
update CFS_LinkKey set TableDotField = null,[Description] = 'Web PO Live' where id = 74
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values(null,'Web PO Live')
end
go
if exists(select * from CFS_LinkKey where id =75)
begin
update CFS_LinkKey set TableDotField = null,[Description] = 'Employee Time Clock' where id = 75
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values(null,'Employee Time Clock')
end
go
if exists(select * from CFS_LinkKey where id =76)
begin
update CFS_LinkKey set TableDotField = null,[Description] = 'Land Master Agreement' where id = 76
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values(null,'Land Master Agreement')
end
go
if exists(select * from CFS_LinkKey where id =77)
begin
update CFS_LinkKey set [TableDotField] = 'PPM_GAP.ID',[Description] = 'Gap Management' where id = 77
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('PPM_GAP.ID','Gap Management')
end
go
if exists(select * from CFS_LinkKey where id =78)
begin
update CFS_LinkKey set [TableDotField] = 'PC_RFI_Proj.PC_RFI_Proj_ID',[Description] = 'Project RFI' where id = 78
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('PC_RFI_Proj.PC_RFI_Proj_ID','Project RFI')
end
go
if exists(select * from CFS_LinkKey where id =79)
begin
update CFS_LinkKey set [TableDotField] = 'PC_Submittal_Proj.PC_Submittal_Proj_ID ',[Description] = 'Project Submittal' where id = 79
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('PC_Submittal_Proj.PC_Submittal_Proj_ID ','Project Submittal')
end
go
if exists(select * from CFS_LinkKey where id =80)
begin
update CFS_LinkKey set [TableDotField] = 'EST_RFI.EST_RFI_ID',[Description] = 'Estimate RFI' where id = 80
end
else
begin
insert into CFS_LinkKey([TableDotField],[Description])values('EST_RFI.EST_RFI_ID','Estimate RFI')
end
go
go
