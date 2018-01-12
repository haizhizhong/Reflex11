if exists(select * from Approval_Topic where ID =1)
begin
update Approval_Topic set [Approval_Topic] = 'Option Confirmation - Pre-Sale - Standard Option',[Module] = 'SalesCenter',Seq = null,[personality] = 'Options',ResultProc = null,SqlDetail = null,applicationmoduleID = null where ID = 1
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(1,'Option Confirmation - Pre-Sale - Standard Option','SalesCenter',null,'Options',null,null,null)
end
go
if exists(select * from Approval_Topic where ID =2)
begin
update Approval_Topic set [Approval_Topic] = 'Option Confirmation - Pre-Sale - Structural Option',[Module] = 'SalesCenter',Seq = null,personality = null,ResultProc = null,SqlDetail = null,applicationmoduleID = null where ID = 2
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(2,'Option Confirmation - Pre-Sale - Structural Option','SalesCenter',null,null,null,null,null)
end
go
if exists(select * from Approval_Topic where ID =3)
begin
update Approval_Topic set [Approval_Topic] = 'Option Confirmation - Pre-Sale - Option Package',[Module] = 'SalesCenter',Seq = null,personality = null,ResultProc = null,SqlDetail = null,applicationmoduleID = null where ID = 3
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(3,'Option Confirmation - Pre-Sale - Option Package','SalesCenter',null,null,null,null,null)
end
go
if exists(select * from Approval_Topic where ID =4)
begin
update Approval_Topic set [Approval_Topic] = 'Option Confirmation - Pre-Deadline - Standard Option',[Module] = 'SalesCenter',Seq = null,personality = null,ResultProc = null,SqlDetail = null,applicationmoduleID = null where ID = 4
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(4,'Option Confirmation - Pre-Deadline - Standard Option','SalesCenter',null,null,null,null,null)
end
go
if exists(select * from Approval_Topic where ID =5)
begin
update Approval_Topic set [Approval_Topic] = 'Option Confirmation - Pre-Deadline - Structural Option',[Module] = 'SalesCenter',Seq = null,personality = null,ResultProc = null,SqlDetail = null,applicationmoduleID = null where ID = 5
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(5,'Option Confirmation - Pre-Deadline - Structural Option','SalesCenter',null,null,null,null,null)
end
go
if exists(select * from Approval_Topic where ID =6)
begin
update Approval_Topic set [Approval_Topic] = 'Option Confirmation - Pre-Deadline - Option Package',[Module] = 'SalesCenter',Seq = null,personality = null,ResultProc = null,SqlDetail = null,applicationmoduleID = null where ID = 6
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(6,'Option Confirmation - Pre-Deadline - Option Package','SalesCenter',null,null,null,null,null)
end
go
if exists(select * from Approval_Topic where ID =7)
begin
update Approval_Topic set [Approval_Topic] = 'Option Confirmation - Post-Deadline - Standard Option',[Module] = 'SalesCenter',Seq = null,personality = null,ResultProc = null,SqlDetail = null,applicationmoduleID = null where ID = 7
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(7,'Option Confirmation - Post-Deadline - Standard Option','SalesCenter',null,null,null,null,null)
end
go
if exists(select * from Approval_Topic where ID =8)
begin
update Approval_Topic set [Approval_Topic] = 'Option Confirmation - Post-Deadline - Structural Option',[Module] = 'SalesCenter',Seq = null,personality = null,ResultProc = null,SqlDetail = null,applicationmoduleID = null where ID = 8
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(8,'Option Confirmation - Post-Deadline - Structural Option','SalesCenter',null,null,null,null,null)
end
go
if exists(select * from Approval_Topic where ID =9)
begin
update Approval_Topic set [Approval_Topic] = 'Option Confirmation - Post-Deadline - Option Package',[Module] = 'SalesCenter',Seq = null,personality = null,ResultProc = null,SqlDetail = null,applicationmoduleID = null where ID = 9
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(9,'Option Confirmation - Post-Deadline - Option Package','SalesCenter',null,null,null,null,null)
end
go
if exists(select * from Approval_Topic where ID =10)
begin
update Approval_Topic set [Approval_Topic] = 'Option Confirmation - Pre-Sale - Custom Option',[Module] = 'SalesCenter',Seq = null,personality = null,ResultProc = null,SqlDetail = null,applicationmoduleID = null where ID = 10
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(10,'Option Confirmation - Pre-Sale - Custom Option','SalesCenter',null,null,null,null,null)
end
go
if exists(select * from Approval_Topic where ID =11)
begin
update Approval_Topic set [Approval_Topic] = 'Option Confirmation - Post-Sale - Custom Option',[Module] = 'SalesCenter',Seq = null,personality = null,ResultProc = null,SqlDetail = null,applicationmoduleID = null where ID = 11
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(11,'Option Confirmation - Post-Sale - Custom Option','SalesCenter',null,null,null,null,null)
end
go
if exists(select * from Approval_Topic where ID =12)
begin
update Approval_Topic set [Approval_Topic] = 'Quote Confirmation - Sale',[Module] = 'SalesCenter',Seq = null,personality = null,ResultProc = null,SqlDetail = null,applicationmoduleID = null where ID = 12
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(12,'Quote Confirmation - Sale','SalesCenter',null,null,null,null,null)
end
go
if exists(select * from Approval_Topic where ID =13)
begin
update Approval_Topic set [Approval_Topic] = 'Start Confirmation - Change',[Module] = 'SalesCenter',Seq = null,personality = null,ResultProc = null,SqlDetail = null,applicationmoduleID = null where ID = 13
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(13,'Start Confirmation - Change','SalesCenter',null,null,null,null,null)
end
go
if exists(select * from Approval_Topic where ID =14)
begin
update Approval_Topic set [Approval_Topic] = 'Inventory Sale - Create Customer',[Module] = 'SalesCenter',Seq = null,personality = null,ResultProc = null,SqlDetail = null,applicationmoduleID = null where ID = 14
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(14,'Inventory Sale - Create Customer','SalesCenter',null,null,null,null,null)
end
go
if exists(select * from Approval_Topic where ID =15)
begin
update Approval_Topic set [Approval_Topic] = 'Manual Invoice Amount',[Module] = 'Billing',Seq = null,personality = null,[ResultProc] = 'sp_SO_TRN_HDR_Update_Status',SqlDetail = null,applicationmoduleID = null where ID = 15
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(15,'Manual Invoice Amount','Billing',null,null,'sp_SO_TRN_HDR_Update_Status',null,null)
end
go
if exists(select * from Approval_Topic where ID =16)
begin
update Approval_Topic set [Approval_Topic] = 'Credit Warning',[Module] = 'Forms Management',Seq = null,[personality] = 'Factor',[ResultProc] = 'sp_FormsManagementCreditRequest',[SqlDetail] = 'Sp_Approval_Topic_Factor',applicationmoduleID = null where ID = 16
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(16,'Credit Warning','Forms Management',null,'Factor','sp_FormsManagementCreditRequest','Sp_Approval_Topic_Factor',null)
end
go
if exists(select * from Approval_Topic where ID =17)
begin
update Approval_Topic set [Approval_Topic] = 'AP Check Approval',[Module] = 'Accounts Payable',Seq = null,[personality] = 'AP',[ResultProc] = 'sp_AP_CHK_SELECT_BATCH_StatusUpdate',SqlDetail = null,applicationmoduleID = null where ID = 17
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(17,'AP Check Approval','Accounts Payable',null,'AP','sp_AP_CHK_SELECT_BATCH_StatusUpdate',null,null)
end
go
if exists(select * from Approval_Topic where ID =18)
begin
update Approval_Topic set [Approval_Topic] = 'AR Customer Creation Approval',[Module] = 'Accounts Receivable',Seq = null,[personality] = 'Customer',[ResultProc] = 'sp_AR_CustomerCreate',SqlDetail = null,applicationmoduleID = null where ID = 18
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(18,'AR Customer Creation Approval','Accounts Receivable',null,'Customer','sp_AR_CustomerCreate',null,null)
end
go
if exists(select * from Approval_Topic where ID =19)
begin
update Approval_Topic set [Approval_Topic] = 'AP Supplier Creation Approval',[Module] = 'Accounts Payable',Seq = null,[personality] = 'Supplier',[ResultProc] = 'sp_AP_SupplierCreate',SqlDetail = null,applicationmoduleID = null where ID = 19
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(19,'AP Supplier Creation Approval','Accounts Payable',null,'Supplier','sp_AP_SupplierCreate',null,null)
end
go
if exists(select * from Approval_Topic where ID =20)
begin
update Approval_Topic set [Approval_Topic] = 'AR Customer Active Change',[Module] = 'Accounts Receivable',Seq = null,[personality] = 'Customer',[ResultProc] = 'sp_AR_CustomerChange',SqlDetail = null,applicationmoduleID = null where ID = 20
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(20,'AR Customer Active Change','Accounts Receivable',null,'Customer','sp_AR_CustomerChange',null,null)
end
go
if exists(select * from Approval_Topic where ID =21)
begin
update Approval_Topic set [Approval_Topic] = 'AP Supplier Active Change',[Module] = 'Accounts Payable',Seq = null,[personality] = 'Supplier',[ResultProc] = 'sp_AP_SupplierChange',SqlDetail = null,applicationmoduleID = null where ID = 21
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(21,'AP Supplier Active Change','Accounts Payable',null,'Supplier','sp_AP_SupplierChange',null,null)
end
go
if exists(select * from Approval_Topic where ID =22)
begin
update Approval_Topic set [Approval_Topic] = 'AR Manual Invoice Limit',[Module] = 'Accounts Receivable',Seq = null,[personality] = 'AR Inv Adj',[ResultProc] = 'sp_AR_ManInvLimit',SqlDetail = null,applicationmoduleID = null where ID = 22
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(22,'AR Manual Invoice Limit','Accounts Receivable',null,'AR Inv Adj','sp_AR_ManInvLimit',null,null)
end
go
if exists(select * from Approval_Topic where ID =23)
begin
update Approval_Topic set [Approval_Topic] = 'AR NSF Limit',[Module] = 'Accounts Receivable',Seq = null,personality = null,[ResultProc] = 'sp_AR_NSFLimit',SqlDetail = null,applicationmoduleID = null where ID = 23
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(23,'AR NSF Limit','Accounts Receivable',null,null,'sp_AR_NSFLimit',null,null)
end
go
if exists(select * from Approval_Topic where ID =24)
begin
update Approval_Topic set [Approval_Topic] = 'AR Discount Limit',[Module] = 'Accounts Receivable',Seq = null,personality = null,[ResultProc] = 'sp_AR_Discount',SqlDetail = null,applicationmoduleID = null where ID = 24
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(24,'AR Discount Limit','Accounts Receivable',null,null,'sp_AR_Discount',null,null)
end
go
if exists(select * from Approval_Topic where ID =25)
begin
update Approval_Topic set [Approval_Topic] = 'AR Write Off Limit',[Module] = 'Accounts Receivable',Seq = null,personality = null,[ResultProc] = 'sp_AR_Writeoff',SqlDetail = null,applicationmoduleID = null where ID = 25
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(25,'AR Write Off Limit','Accounts Receivable',null,null,'sp_AR_Writeoff',null,null)
end
go
if exists(select * from Approval_Topic where ID =26)
begin
update Approval_Topic set [Approval_Topic] = 'RFP Convert to PO',[Module] = 'Purchase Orders',Seq = null,personality = null,ResultProc = null,SqlDetail = null,applicationmoduleID = null where ID = 26
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(26,'RFP Convert to PO','Purchase Orders',null,null,null,null,null)
end
go
if exists(select * from Approval_Topic where ID =29)
begin
update Approval_Topic set [Approval_Topic] = 'Inventory Creation',[Module] = 'Inventory',Seq = null,[personality] = 'Inventory',[ResultProc] = 'sp_INV_Routing_InventoryCreate',SqlDetail = null,applicationmoduleID = null where ID = 29
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(29,'Inventory Creation','Inventory',null,'Inventory','sp_INV_Routing_InventoryCreate',null,null)
end
go
if exists(select * from Approval_Topic where ID =31)
begin
update Approval_Topic set [Approval_Topic] = 'Inventory Movement',[Module] = 'Inventory',Seq = null,personality = null,ResultProc = null,SqlDetail = null,applicationmoduleID = null where ID = 31
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(31,'Inventory Movement','Inventory',null,null,null,null,null)
end
go
if exists(select * from Approval_Topic where ID =33)
begin
update Approval_Topic set [Approval_Topic] = 'Equipment Entry',[Module] = 'Fixed Assets',Seq = null,[personality] = 'Fixed Assets',[ResultProc] = 'sp_FA_Routing_EquipmentCreate',SqlDetail = null,applicationmoduleID = null where ID = 33
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(33,'Equipment Entry','Fixed Assets',null,'Fixed Assets','sp_FA_Routing_EquipmentCreate',null,null)
end
go
if exists(select * from Approval_Topic where ID =34)
begin
update Approval_Topic set [Approval_Topic] = 'Building Entry',[Module] = 'Fixed Assets',Seq = null,personality = null,ResultProc = null,SqlDetail = null,applicationmoduleID = null where ID = 34
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(34,'Building Entry','Fixed Assets',null,null,null,null,null)
end
go
if exists(select * from Approval_Topic where ID =35)
begin
update Approval_Topic set [Approval_Topic] = 'Intangible Entry',[Module] = 'Fixed Assets',Seq = null,personality = null,ResultProc = null,SqlDetail = null,applicationmoduleID = null where ID = 35
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(35,'Intangible Entry','Fixed Assets',null,null,null,null,null)
end
go
if exists(select * from Approval_Topic where ID =36)
begin
update Approval_Topic set [Approval_Topic] = 'PO Buyer Level Limit 1',[Module] = 'Purchase Orders',Seq = null,[personality] = 'PO',[ResultProc] = 'sp_POApproval',SqlDetail = null,applicationmoduleID = null where ID = 36
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(36,'PO Buyer Level Limit 1','Purchase Orders',null,'PO','sp_POApproval',null,null)
end
go
if exists(select * from Approval_Topic where ID =37)
begin
update Approval_Topic set [Approval_Topic] = 'PO Buyer Level Limit 2',[Module] = 'Purchase Orders',Seq = null,[personality] = 'PO',[ResultProc] = 'sp_POApproval',SqlDetail = null,applicationmoduleID = null where ID = 37
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(37,'PO Buyer Level Limit 2','Purchase Orders',null,'PO','sp_POApproval',null,null)
end
go
if exists(select * from Approval_Topic where ID =38)
begin
update Approval_Topic set [Approval_Topic] = 'PO Buyer Level Limit 3',[Module] = 'Purchase Orders',Seq = null,[personality] = 'PO',[ResultProc] = 'sp_POApproval',SqlDetail = null,applicationmoduleID = null where ID = 38
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(38,'PO Buyer Level Limit 3','Purchase Orders',null,'PO','sp_POApproval',null,null)
end
go
if exists(select * from Approval_Topic where ID =39)
begin
update Approval_Topic set [Approval_Topic] = 'PO Buyer Level Limit 4',[Module] = 'Purchase Orders',Seq = null,[personality] = 'PO',[ResultProc] = 'sp_POApproval',SqlDetail = null,applicationmoduleID = null where ID = 39
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(39,'PO Buyer Level Limit 4','Purchase Orders',null,'PO','sp_POApproval',null,null)
end
go
if exists(select * from Approval_Topic where ID =40)
begin
update Approval_Topic set [Approval_Topic] = 'PO Buyer Level Limit 5',[Module] = 'Purchase Orders',Seq = null,[personality] = 'PO',[ResultProc] = 'sp_POApproval',SqlDetail = null,applicationmoduleID = null where ID = 40
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(40,'PO Buyer Level Limit 5','Purchase Orders',null,'PO','sp_POApproval',null,null)
end
go
if exists(select * from Approval_Topic where ID =41)
begin
update Approval_Topic set [Approval_Topic] = 'Credit Warning',[Module] = 'Order Entry',Seq = null,[personality] = 'Order Entry Generic',[ResultProc] = 'sp_OE_CreditApproval',SqlDetail = null,applicationmoduleID = null where ID = 41
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(41,'Credit Warning','Order Entry',null,'Order Entry Generic','sp_OE_CreditApproval',null,null)
end
go
if exists(select * from Approval_Topic where ID =42)
begin
update Approval_Topic set [Approval_Topic] = 'Misc Bank Transactions',[Module] = 'Bank Reconciliation',Seq = null,personality = null,[ResultProc] = 'sp_BM_MiscTransApproval',SqlDetail = null,applicationmoduleID = null where ID = 42
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(42,'Misc Bank Transactions','Bank Reconciliation',null,null,'sp_BM_MiscTransApproval',null,null)
end
go
if exists(select * from Approval_Topic where ID =43)
begin
update Approval_Topic set [Approval_Topic] = 'GL Posting Approval',[Module] = 'General Ledger',Seq = null,[personality] = 'GL Posting',[ResultProc] = 'sp_GL_Approval',SqlDetail = null,applicationmoduleID = null where ID = 43
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(43,'GL Posting Approval','General Ledger',null,'GL Posting','sp_GL_Approval',null,null)
end
go
if exists(select * from Approval_Topic where ID =44)
begin
update Approval_Topic set [Approval_Topic] = 'Contract Override',[Module] = 'Order Entry',Seq = null,[personality] = 'Order Entry Contract',[ResultProc] = 'sp_SO_UpdateContract',SqlDetail = null,applicationmoduleID = null where ID = 44
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(44,'Contract Override','Order Entry',null,'Order Entry Contract','sp_SO_UpdateContract',null,null)
end
go
if exists(select * from Approval_Topic where ID =45)
begin
update Approval_Topic set [Approval_Topic] = 'NRV Write-Down Approval',[Module] = 'Inventory',Seq = null,[personality] = 'NRV',[ResultProc] = 'sp_INV_NRVRouting',SqlDetail = null,applicationmoduleID = null where ID = 45
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(45,'NRV Write-Down Approval','Inventory',null,'NRV','sp_INV_NRVRouting',null,null)
end
go
if exists(select * from Approval_Topic where ID =47)
begin
update Approval_Topic set [Approval_Topic] = 'SO RMA Routing',[Module] = 'Order Entry',Seq = null,[personality] = 'SO Return',[ResultProc] = 'sp_SO_RMAApproval',SqlDetail = null,applicationmoduleID = null where ID = 47
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(47,'SO RMA Routing','Order Entry',null,'SO Return','sp_SO_RMAApproval',null,null)
end
go
if exists(select * from Approval_Topic where ID =48)
begin
update Approval_Topic set [Approval_Topic] = 'PO RMA Routing',[Module] = 'Purchase Orders',Seq = null,[personality] = 'PO Return',[ResultProc] = 'sp_PO_RMAApproval',SqlDetail = null,applicationmoduleID = null where ID = 48
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(48,'PO RMA Routing','Purchase Orders',null,'PO Return','sp_PO_RMAApproval',null,null)
end
go
if exists(select * from Approval_Topic where ID =49)
begin
update Approval_Topic set [Approval_Topic] = 'SO Gross Margin Level Limit 1',[Module] = 'Order Entry',Seq = null,[personality] = 'Order Entry Generic',[ResultProc] = 'sp_SO_GMLimitApproval',SqlDetail = null,applicationmoduleID = null where ID = 49
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(49,'SO Gross Margin Level Limit 1','Order Entry',null,'Order Entry Generic','sp_SO_GMLimitApproval',null,null)
end
go
if exists(select * from Approval_Topic where ID =50)
begin
update Approval_Topic set [Approval_Topic] = 'SO Gross Margin Level Limit 2',[Module] = 'Order Entry',Seq = null,[personality] = 'Order Entry Generic',[ResultProc] = 'sp_SO_GMLimitApproval',SqlDetail = null,applicationmoduleID = null where ID = 50
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(50,'SO Gross Margin Level Limit 2','Order Entry',null,'Order Entry Generic','sp_SO_GMLimitApproval',null,null)
end
go
if exists(select * from Approval_Topic where ID =51)
begin
update Approval_Topic set [Approval_Topic] = 'SO Gross Margin Level Limit 3',[Module] = 'Order Entry',Seq = null,[personality] = 'Order Entry Generic',[ResultProc] = 'sp_SO_GMLimitApproval',SqlDetail = null,applicationmoduleID = null where ID = 51
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(51,'SO Gross Margin Level Limit 3','Order Entry',null,'Order Entry Generic','sp_SO_GMLimitApproval',null,null)
end
go
if exists(select * from Approval_Topic where ID =52)
begin
update Approval_Topic set [Approval_Topic] = 'SO Gross Margin Level Limit 4',[Module] = 'Order Entry',Seq = null,[personality] = 'Order Entry Generic',[ResultProc] = 'sp_SO_GMLimitApproval',SqlDetail = null,applicationmoduleID = null where ID = 52
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(52,'SO Gross Margin Level Limit 4','Order Entry',null,'Order Entry Generic','sp_SO_GMLimitApproval',null,null)
end
go
if exists(select * from Approval_Topic where ID =53)
begin
update Approval_Topic set [Approval_Topic] = 'Sales Person Margin Limits',[Module] = 'Order Entry',Seq = null,[personality] = 'Order Entry Generic',[ResultProc] = 'sp_SO_GMLimitApproval',SqlDetail = null,applicationmoduleID = null where ID = 53
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(53,'Sales Person Margin Limits','Order Entry',null,'Order Entry Generic','sp_SO_GMLimitApproval',null,null)
end
go
if exists(select * from Approval_Topic where ID =54)
begin
update Approval_Topic set [Approval_Topic] = 'Payment Approval Routing',[Module] = 'Project Costing',Seq = null,personality = null,ResultProc = null,SqlDetail = null,applicationmoduleID = null where ID = 54
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(54,'Payment Approval Routing','Project Costing',null,null,null,null,null)
end
go
if exists(select * from Approval_Topic where ID =55)
begin
update Approval_Topic set [Approval_Topic] = 'AR Manual Invoice Credit Limit',[Module] = 'Accounts Receivable',Seq = null,[personality] = 'AR Inv Adj',[ResultProc] = 'sp_AR_ManInvCreditLimit',SqlDetail = null,applicationmoduleID = null where ID = 55
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(55,'AR Manual Invoice Credit Limit','Accounts Receivable',null,'AR Inv Adj','sp_AR_ManInvCreditLimit',null,null)
end
go
if exists(select * from Approval_Topic where ID =56)
begin
update Approval_Topic set [Approval_Topic] = 'Inventory Quarantine Disposal',[Module] = 'Inventory',Seq = null,personality = null,ResultProc = null,SqlDetail = null,applicationmoduleID = null where ID = 56
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(56,'Inventory Quarantine Disposal','Inventory',null,null,null,null,null)
end
go
if exists(select * from Approval_Topic where ID =57)
begin
update Approval_Topic set [Approval_Topic] = 'Inventory Quarantine RMA',[Module] = 'Inventory',Seq = null,personality = null,ResultProc = null,SqlDetail = null,applicationmoduleID = null where ID = 57
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(57,'Inventory Quarantine RMA','Inventory',null,null,null,null,null)
end
go
if exists(select * from Approval_Topic where ID =58)
begin
update Approval_Topic set [Approval_Topic] = 'AP Unapproved Contract PO Matching',[Module] = 'Accounts Payable',Seq = null,[personality] = 'AP PO',[ResultProc] = 'sp_AP_UnapprovedContractPOMatch',SqlDetail = null,applicationmoduleID = null where ID = 58
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(58,'AP Unapproved Contract PO Matching','Accounts Payable',null,'AP PO','sp_AP_UnapprovedContractPOMatch',null,null)
end
go
delete Approval_Topic where ID = 59
go
delete Approval_Topic where ID = 60
go
if exists(select * from Approval_Topic where ID =61)
begin
update Approval_Topic set [Approval_Topic] = 'PO Subcontractor Compliance',[Module] = 'Purchase Orders',Seq = null,[personality] = 'PO',[ResultProc] = 'PO_SubConApproval',SqlDetail = null,applicationmoduleID = null where ID = 61
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(61,'PO Subcontractor Compliance','Purchase Orders',null,'PO','PO_SubConApproval',null,null)
end
go
if exists(select * from Approval_Topic where ID =62)
begin
update Approval_Topic set [Approval_Topic] = 'Applicant Submission',[Module] = 'Human Resources',Seq = null,[personality] = 'Applicant',[ResultProc] = 'HR_Routing_Applicant',SqlDetail = null,applicationmoduleID = null where ID = 62
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(62,'Applicant Submission','Human Resources',null,'Applicant','HR_Routing_Applicant',null,null)
end
go
if exists(select * from Approval_Topic where ID =63)
begin
update Approval_Topic set [Approval_Topic] = 'Position Posting',[Module] = 'Human Resources',Seq = null,[personality] = 'Posting',[ResultProc] = 'HR_Routing_Position',SqlDetail = null,applicationmoduleID = null where ID = 63
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(63,'Position Posting','Human Resources',null,'Posting','HR_Routing_Position',null,null)
end
go
if exists(select * from Approval_Topic where ID =64)
begin
update Approval_Topic set [Approval_Topic] = 'Purchaser Creation Approval',[Module] = 'Land Development',Seq = null,[personality] = 'Purchaser',[ResultProc] = 'sp_AR_CustomerCreate',SqlDetail = null,applicationmoduleID = null where ID = 64
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(64,'Purchaser Creation Approval','Land Development',null,'Purchaser','sp_AR_CustomerCreate',null,null)
end
go
if exists(select * from Approval_Topic where ID =65)
begin
update Approval_Topic set [Approval_Topic] = 'Purchaser Active Change',[Module] = 'Land Development',Seq = null,[personality] = 'Purchaser',[ResultProc] = 'sp_AR_CustomerChange',SqlDetail = null,applicationmoduleID = null where ID = 65
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(65,'Purchaser Active Change','Land Development',null,'Purchaser','sp_AR_CustomerChange',null,null)
end
go
if exists(select * from Approval_Topic where ID =66)
begin
update Approval_Topic set [Approval_Topic] = '3rd Party Purchaser Creation Approval',[Module] = 'Land Development',Seq = null,[personality] = 'Purchaser',[ResultProc] = 'sp_AR_CustomerCreate',SqlDetail = null,applicationmoduleID = null where ID = 66
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(66,'3rd Party Purchaser Creation Approval','Land Development',null,'Purchaser','sp_AR_CustomerCreate',null,null)
end
go
if exists(select * from Approval_Topic where ID =67)
begin
update Approval_Topic set [Approval_Topic] = '3rd Party Purchaser Active Change',[Module] = 'Land Development',Seq = null,[personality] = 'Purchaser',[ResultProc] = 'sp_AR_CustomerChange',SqlDetail = null,applicationmoduleID = null where ID = 67
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(67,'3rd Party Purchaser Active Change','Land Development',null,'Purchaser','sp_AR_CustomerChange',null,null)
end
go
if exists(select * from Approval_Topic where ID =69)
begin
update Approval_Topic set [Approval_Topic] = 'Gross/Net Approval',[Module] = 'Payroll',Seq = null,[personality] = 'Payroll',[ResultProc] = 'PY_GrossNetApproval',SqlDetail = null,applicationmoduleID = null where ID = 69
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(69,'Gross/Net Approval','Payroll',null,'Payroll','PY_GrossNetApproval',null,null)
end
go
if exists(select * from Approval_Topic where ID =70)
begin
update Approval_Topic set [Approval_Topic] = 'Cancel 3rd Party Sale',[Module] = 'Land Development',Seq = null,[personality] = 'Cancel Third Party Sale',[ResultProc] = 'WS_Routing_CancelThirdPartySale',SqlDetail = null,applicationmoduleID = null where ID = 70
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(70,'Cancel 3rd Party Sale','Land Development',null,'Cancel Third Party Sale','WS_Routing_CancelThirdPartySale',null,null)
end
go
if exists(select * from Approval_Topic where ID =71)
begin
update Approval_Topic set [Approval_Topic] = 'AP Holdback Approval',[Module] = 'Accounts Payable',Seq = null,[personality] = 'AP HB',[ResultProc] = 'AP_HB_Release_Approval',SqlDetail = null,applicationmoduleID = null where ID = 71
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(71,'AP Holdback Approval','Accounts Payable',null,'AP HB','AP_HB_Release_Approval',null,null)
end
go
if exists(select * from Approval_Topic where ID =72)
begin
update Approval_Topic set [Approval_Topic] = 'Lot Hold Request',[Module] = 'Land Development',Seq = null,[personality] = 'Lot Hold Request',[ResultProc] = 'LD_Lot_Hold_Approval',SqlDetail = null,applicationmoduleID = null where ID = 72
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(72,'Lot Hold Request','Land Development',null,'Lot Hold Request','LD_Lot_Hold_Approval',null,null)
end
go
if exists(select * from Approval_Topic where ID =73)
begin
update Approval_Topic set [Approval_Topic] = 'Model Submission',[Module] = 'Land Development',Seq = null,[personality] = 'Model Submission',[ResultProc] = 'WS_Routing_ModelSubmission',SqlDetail = null,applicationmoduleID = null where ID = 73
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(73,'Model Submission','Land Development',null,'Model Submission','WS_Routing_ModelSubmission',null,null)
end
go
if exists(select * from Approval_Topic where ID =74)
begin
update Approval_Topic set [Approval_Topic] = 'Lot Purchase Request',[Module] = 'Land Development',Seq = null,[personality] = 'Lot Purchase Request',[ResultProc] = 'LD_Lot_Purchase_Approval',SqlDetail = null,applicationmoduleID = null where ID = 74
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(74,'Lot Purchase Request','Land Development',null,'Lot Purchase Request','LD_Lot_Purchase_Approval',null,null)
end
go
if exists(select * from Approval_Topic where ID =75)
begin
update Approval_Topic set [Approval_Topic] = 'Enter Home Owner',[Module] = 'Land Development',Seq = null,[personality] = 'Enter Home Owner',[ResultProc] = 'LD_EnterHomeOwner',SqlDetail = null,applicationmoduleID = null where ID = 75
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(75,'Enter Home Owner','Land Development',null,'Enter Home Owner','LD_EnterHomeOwner',null,null)
end
go
if exists(select * from Approval_Topic where ID =76)
begin
update Approval_Topic set [Approval_Topic] = 'Home Submission',[Module] = 'Land Development',Seq = null,[personality] = 'Home Submission',[ResultProc] = 'WS_Routing_HomeSubmission',SqlDetail = null,applicationmoduleID = null where ID = 76
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(76,'Home Submission','Land Development',null,'Home Submission','WS_Routing_HomeSubmission',null,null)
end
go
if exists(select * from Approval_Topic where ID =77)
begin
update Approval_Topic set [Approval_Topic] = 'Order Entry Training',[Module] = 'Order Entry',Seq = null,[personality] = 'Order Entry Generic',[ResultProc] = 'OE_TrainingRouting',SqlDetail = null,applicationmoduleID = null where ID = 77
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(77,'Order Entry Training','Order Entry',null,'Order Entry Generic','OE_TrainingRouting',null,null)
end
go
if exists(select * from Approval_Topic where ID =78)
begin
update Approval_Topic set [Approval_Topic] = 'Credit Days Warning',[Module] = 'Order Entry',Seq = null,[personality] = 'Order Entry Generic',[ResultProc] = 'sp_OE_CreditDaysApproval',SqlDetail = null,applicationmoduleID = null where ID = 78
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(78,'Credit Days Warning','Order Entry',null,'Order Entry Generic','sp_OE_CreditDaysApproval',null,null)
end
go
if exists(select * from Approval_Topic where ID =79)
begin
update Approval_Topic set [Approval_Topic] = 'Quote Routing Level 1',[Module] = 'Order Entry',Seq = null,[personality] = 'Order Entry Generic',[ResultProc] = 'OE_QuoteApproval',SqlDetail = null,applicationmoduleID = null where ID = 79
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(79,'Quote Routing Level 1','Order Entry',null,'Order Entry Generic','OE_QuoteApproval',null,null)
end
go
if exists(select * from Approval_Topic where ID =80)
begin
update Approval_Topic set [Approval_Topic] = 'Quote Routing Level 2',[Module] = 'Order Entry',Seq = null,[personality] = 'Order Entry Generic',[ResultProc] = 'OE_QuoteApproval',SqlDetail = null,applicationmoduleID = null where ID = 80
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(80,'Quote Routing Level 2','Order Entry',null,'Order Entry Generic','OE_QuoteApproval',null,null)
end
go
if exists(select * from Approval_Topic where ID =81)
begin
update Approval_Topic set [Approval_Topic] = 'Quote Routing Level 3',[Module] = 'Order Entry',Seq = null,[personality] = 'Order Entry Generic',[ResultProc] = 'OE_QuoteApproval',SqlDetail = null,applicationmoduleID = null where ID = 81
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(81,'Quote Routing Level 3','Order Entry',null,'Order Entry Generic','OE_QuoteApproval',null,null)
end
go
if exists(select * from Approval_Topic where ID =82)
begin
update Approval_Topic set [Approval_Topic] = 'Quote Routing Level 4',[Module] = 'Order Entry',Seq = null,[personality] = 'Order Entry Generic',[ResultProc] = 'OE_QuoteApproval',SqlDetail = null,applicationmoduleID = null where ID = 82
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(82,'Quote Routing Level 4','Order Entry',null,'Order Entry Generic','OE_QuoteApproval',null,null)
end
go
if exists(select * from Approval_Topic where ID =83)
begin
update Approval_Topic set [Approval_Topic] = 'Employee Copy Create',[Module] = 'Payroll',Seq = null,[personality] = 'Employee Create',[ResultProc] = 'PY_Routing_EmployeeCreate',SqlDetail = null,applicationmoduleID = null where ID = 83
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(83,'Employee Copy Create','Payroll',null,'Employee Create','PY_Routing_EmployeeCreate',null,null)
end
go
if exists(select * from Approval_Topic where ID =84)
begin
update Approval_Topic set [Approval_Topic] = 'Project Investor Distribution',[Module] = 'Land Development',Seq = null,[personality] = 'Project Investor Distribution',[ResultProc] = 'LD_RoutingDistribution',SqlDetail = null,applicationmoduleID = null where ID = 84
end
else
begin
insert into Approval_Topic([ID],[Approval_Topic],[Module],[Seq],[personality],[ResultProc],[SqlDetail],[applicationmoduleID])values(84,'Project Investor Distribution','Land Development',null,'Project Investor Distribution','LD_RoutingDistribution',null,null)
end
go
go
delete COMMUNICATION_PURPOSE 
go
if exists(select * from COMMUNICATION_PURPOSE where ID =1)
begin
update COMMUNICATION_PURPOSE set [ID] = 1,[DESCRIPTION] = 'RFQ Contact',[Email_Fax_Option] = 'F',Legend = null,Subject_Default = null where ID = 1
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(1,'RFQ Contact','F',null,null)
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =2)
begin
update COMMUNICATION_PURPOSE set [ID] = 2,[DESCRIPTION] = 'RFQ Purchase Order',[Email_Fax_Option] = 'F',Legend = null,Subject_Default = null where ID = 2
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(2,'RFQ Purchase Order','F',null,null)
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =3)
begin
update COMMUNICATION_PURPOSE set [ID] = 3,[DESCRIPTION] = 'WMS Reports',[Email_Fax_Option] = 'F',Legend = null,Subject_Default = null where ID = 3
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(3,'WMS Reports','F',null,null)
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =5)
begin
update COMMUNICATION_PURPOSE set [ID] = 5,[DESCRIPTION] = 'RFQ Approval Spreadsheet',[Email_Fax_Option] = 'F',Legend = null,Subject_Default = null where ID = 5
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(5,'RFQ Approval Spreadsheet','F',null,null)
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =6)
begin
update COMMUNICATION_PURPOSE set [ID] = 6,[DESCRIPTION] = 'Customer Expediting Spreadsheet',[Email_Fax_Option] = 'F',Legend = null,Subject_Default = null where ID = 6
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(6,'Customer Expediting Spreadsheet','F',null,null)
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =7)
begin
update COMMUNICATION_PURPOSE set [ID] = 7,[DESCRIPTION] = 'Customer Sales Order',[Email_Fax_Option] = 'F',Legend = null,Subject_Default = null where ID = 7
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(7,'Customer Sales Order','F',null,null)
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =8)
begin
update COMMUNICATION_PURPOSE set [ID] = 8,[DESCRIPTION] = 'Customer Quote',[Email_Fax_Option] = 'F',Legend = null,Subject_Default = null where ID = 8
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(8,'Customer Quote','F',null,null)
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =9)
begin
update COMMUNICATION_PURPOSE set [ID] = 9,[DESCRIPTION] = 'Property Owner',[Email_Fax_Option] = 'F',Legend = null,Subject_Default = null where ID = 9
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(9,'Property Owner','F',null,null)
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =10)
begin
update COMMUNICATION_PURPOSE set [ID] = 10,[DESCRIPTION] = 'Residential Contact',[Email_Fax_Option] = 'F',Legend = null,Subject_Default = null where ID = 10
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(10,'Residential Contact','F',null,null)
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =11)
begin
update COMMUNICATION_PURPOSE set [ID] = 11,[DESCRIPTION] = 'Commercial Contact',[Email_Fax_Option] = 'F',Legend = null,Subject_Default = null where ID = 11
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(11,'Commercial Contact','F',null,null)
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =12)
begin
update COMMUNICATION_PURPOSE set [ID] = 12,[DESCRIPTION] = 'AP Payment Approver',[Email_Fax_Option] = 'F',Legend = null,Subject_Default = null where ID = 12
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(12,'AP Payment Approver','F',null,null)
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =13)
begin
update COMMUNICATION_PURPOSE set [ID] = 13,[DESCRIPTION] = 'MTR Delivery',[Email_Fax_Option] = 'F',Legend = null,Subject_Default = null where ID = 13
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(13,'MTR Delivery','F',null,null)
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =14)
begin
update COMMUNICATION_PURPOSE set [ID] = 14,[DESCRIPTION] = 'Home Owner Survey',[Email_Fax_Option] = 'F',Legend = null,Subject_Default = null where ID = 14
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(14,'Home Owner Survey','F',null,null)
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =15)
begin
update COMMUNICATION_PURPOSE set [ID] = 15,[DESCRIPTION] = 'AR Invoice',[Email_Fax_Option] = 'T',[Legend] = '',[Subject_Default] = '(Subject) + (Invoice #) + (Date)' where ID = 15
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(15,'AR Invoice','T','','(Subject) + (Invoice #) + (Date)')
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =16)
begin
update COMMUNICATION_PURPOSE set [ID] = 16,[DESCRIPTION] = 'Sales Order Print',[Email_Fax_Option] = 'F',Legend = null,Subject_Default = null where ID = 16
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(16,'Sales Order Print','F',null,null)
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =17)
begin
update COMMUNICATION_PURPOSE set [ID] = 17,[DESCRIPTION] = 'Web',[Email_Fax_Option] = 'F',Legend = null,Subject_Default = null where ID = 17
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(17,'Web','F',null,null)
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =18)
begin
update COMMUNICATION_PURPOSE set [ID] = 18,[DESCRIPTION] = 'DEFT Confirmation',[Email_Fax_Option] = 'T',[Legend] = '',[Subject_Default] = '(Subject) + (Date)' where ID = 18
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(18,'DEFT Confirmation','T','','(Subject) + (Date)')
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =19)
begin
update COMMUNICATION_PURPOSE set [ID] = 19,[DESCRIPTION] = 'Primary Lot Sale Contact',[Email_Fax_Option] = 'F',Legend = null,Subject_Default = null where ID = 19
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(19,'Primary Lot Sale Contact','F',null,null)
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =20)
begin
update COMMUNICATION_PURPOSE set [ID] = 20,[DESCRIPTION] = 'Secondary Lot Sale Contact',[Email_Fax_Option] = 'F',Legend = null,Subject_Default = null where ID = 20
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(20,'Secondary Lot Sale Contact','F',null,null)
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =21)
begin
update COMMUNICATION_PURPOSE set [ID] = 21,[DESCRIPTION] = 'WMS Release to Invoice',[Email_Fax_Option] = 'T',[Legend] = '~SO~ Sales Order #
~PO~ Sales Order PO #
~CFL~ Sales Order PO #3 Label
~CF~ Sales Order PO #3',[Subject_Default] = '' where ID = 21
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(21,'WMS Release to Invoice','T','~SO~ Sales Order #
~PO~ Sales Order PO #
~CFL~ Sales Order PO #3 Label
~CF~ Sales Order PO #3','')
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =22)
begin
update COMMUNICATION_PURPOSE set [ID] = 22,[DESCRIPTION] = 'AR Statement',[Email_Fax_Option] = 'T',[Legend] = '~SDate~ Statement Date',[Subject_Default] = '(Subject) + (Customer Name) + Statement + (Statement Date)' where ID = 22
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(22,'AR Statement','T','~SDate~ Statement Date','(Subject) + (Customer Name) + Statement + (Statement Date)')
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =23)
begin
update COMMUNICATION_PURPOSE set [ID] = 23,[DESCRIPTION] = 'PO Warning/Expiry',[Email_Fax_Option] = 'F',Legend = null,Subject_Default = null where ID = 23
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(23,'PO Warning/Expiry','F',null,null)
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =24)
begin
update COMMUNICATION_PURPOSE set [ID] = 24,[DESCRIPTION] = 'AP DEFT Payment Notification',[Email_Fax_Option] = 'T',[Legend] = '~DEFT #~
~Pay Date~',[Subject_Default] = '(Subject)+ AP Payment Notification + (Pay Date)' where ID = 24
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(24,'AP DEFT Payment Notification','T','~DEFT #~
~Pay Date~','(Subject)+ AP Payment Notification + (Pay Date)')
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =25)
begin
update COMMUNICATION_PURPOSE set [ID] = 25,[DESCRIPTION] = 'Agreement Word Merge',[Email_Fax_Option] = 'F',Legend = null,Subject_Default = null where ID = 25
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(25,'Agreement Word Merge','F',null,null)
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =26)
begin
update COMMUNICATION_PURPOSE set [ID] = 26,[DESCRIPTION] = 'Contract Compliance',[Email_Fax_Option] = 'F',Legend = null,Subject_Default = null where ID = 26
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(26,'Contract Compliance','F',null,null)
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =27)
begin
update COMMUNICATION_PURPOSE set [ID] = 27,[DESCRIPTION] = 'PO',[Email_Fax_Option] = 'T',Legend = null,Subject_Default = null where ID = 27
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(27,'PO','T',null,null)
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =28)
begin
update COMMUNICATION_PURPOSE set [ID] = 28,[DESCRIPTION] = 'Project RFI',[Email_Fax_Option] = 'T',[Legend] = '~PM~',[Subject_Default] = '(Subject) + (Project Name)' where ID = 28
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(28,'Project RFI','T','~PM~','(Subject) + (Project Name)')
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =29)
begin
update COMMUNICATION_PURPOSE set [ID] = 29,[DESCRIPTION] = 'Project Submittals',[Email_Fax_Option] = 'T',[Legend] = '~PM~',[Subject_Default] = '(Subject) + (Project Name)' where ID = 29
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(29,'Project Submittals','T','~PM~','(Subject) + (Project Name)')
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =30)
begin
update COMMUNICATION_PURPOSE set [ID] = 30,[DESCRIPTION] = 'Estimate RFI',[Email_Fax_Option] = 'T',[Legend] = '~PM~',[Subject_Default] = '(Subject) + (Project Name)' where ID = 30
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(30,'Estimate RFI','T','~PM~','(Subject) + (Project Name)')
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =31)
begin
update COMMUNICATION_PURPOSE set [ID] = 31,[DESCRIPTION] = 'Service Order',[Email_Fax_Option] = 'F',Legend = null,Subject_Default = null where ID = 31
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(31,'Service Order','F',null,null)
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =32)
begin
update COMMUNICATION_PURPOSE set [ID] = 32,[DESCRIPTION] = 'Rental Request',[Email_Fax_Option] = 'F',Legend = null,Subject_Default = null where ID = 32
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(32,'Rental Request','F',null,null)
end
go
if exists(select * from COMMUNICATION_PURPOSE where ID =33)
begin
update COMMUNICATION_PURPOSE set [ID] = 33,[DESCRIPTION] = 'Project Shop Drawing Log',[Email_Fax_Option] = 'T',[Legend] = '~PM~',[Subject_Default] = '(Subject) + (Project Name)' where ID = 33
end
else
begin
insert into COMMUNICATION_PURPOSE([ID],[DESCRIPTION],[Email_Fax_Option],[Legend],[Subject_Default])values(33,'Project Shop Drawing Log','T','~PM~','(Subject) + (Project Name)')
end
go
