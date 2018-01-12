alter proc DB_ManagementReview @ContactID int as
--declare @ContactID int = 2


if exists (select * from tempdb..sysobjects where id = object_id(N'tempdb..#DBMDetail'))
drop table #DBMDetail
CREATE TABLE #DBMDetail(
	id int not null identity(1,1) primary key,
	LinkTable varchar(100),
	LinkField varchar(100),
	LinkID int,
	[Type] varchar(50),
	[Group] varchar(50),
	SubGroup varchar(50),
	Description1 varchar(max),
	Description2 varchar(max),
	Notes varchar(max),
	[Status] varchar(50),
	EventDate datetime,
	NotificationDate datetime not null default getdate(),
	Dismissed bit not null default 0,
	Viewed datetime,
	Flag int)


	--get approval notifications which are addressed to me
	insert #DBMDetail(LinkTable, LinkField, LinkID, [Type], [Group], SubGroup, Description1, Description2, Notes, [Status], EventDate, Viewed, Flag)
	select 'Approval_Notification', 'ID', n.id, 'Approval Notifications', at.Module, at.Approval_Topic, r.Description, '',
	r.Additional_Notes, r.Status, n.Notify_Date, mn.Viewed, mn.Flag
	from Approval_Notification n 
	join Approval_Contacts c on n.AC_ID = c.ID 
	join Approvals_Requested r on n.AR_ID = r.id
	join Approval_Topic at on at.ID = r.AT_ID
	left outer join DB_ManagementNotification mn on mn.LinkTable = 'Approval_Notification' and LinkField = 'ID' and LinkID = n.id
	where c.Contact_ID = @ContactID 
	and n.Status not in ('Declined', 'Approved')
	and r.Status not in ('Recalled', 'Approved', 'Declined')
	and isnull(mn.Dismissed,0) = 0
		
													  
	--get approval responses to my requests
	insert #DBMDetail(LinkTable, LinkField, LinkID, [Type], [Group], SubGroup, Description1, Description2, Notes, [Status], EventDate, Viewed, Flag)
	select 'Approvals_Requested', 'ID', r.id, 'Approval Request Responses', at.Module, at.Approval_Topic, r.Description, n.Status + ' by ' + co.KnownAs,
	n.Notes, n.Status, n.Response_Date, mn.Viewed, mn.Flag
	from Approvals_Requested r				
	join Approval_Notification n on n.AR_ID = r.id   	
	join Approval_Contacts c on n.AC_ID = c.ID 	   
	join Contact co on co.id = c.Contact_ID
	join Approval_Topic at on at.ID = r.AT_ID
	left outer join DB_ManagementNotification mn on mn.LinkTable = 'Approvals_Requested' and LinkField = 'ID' and LinkID = r.id
	where r.Requestor_ID = @ContactID
	and n.Status in ('Approved', 'Declined')
	and isnull(mn.Dismissed,0) = 0

	
	--get incomming emails	

	--new chats

	--get alerts

	--get audit changes

	--get workflow routing

	--"watches"
	/*special queries which monitor certain tables to alert when a specific condition is met
	for example... a watch on the contact history table to notify when a prospect has an incomming communication recorded*/


select * from #DBMDetail d order by [Type], [Group], SubGroup, EventDate


