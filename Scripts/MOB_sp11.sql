if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ReportLEMApproval]') and xtype = 'P ')  
 drop Procedure ReportLEMApproval
Go
SET QUOTED_IDENTIFIER OFF
Go
SET ANSI_NULLS ON 
Go

-- exec ReportLEMApproval 213, 'Reflex'
CREATE proc [dbo].[ReportLEMApproval] @id int, @username varchar(10) 
as
begin
	delete ReportLEMApprovalAP where username = @username
	delete ReportLEMApprovalEquipment where username = @username
	delete ReportLEMApprovalLabourDetail where username = @username
	delete ReportLEMApprovalLabour where username = @username
	delete ReportLEMApprovalHeader where username = @username

	declare @LowLevel int, @SepLOA bit, @SepTravel bit, @SepEquip bit, @pri_id int

	select @SepLOA=0, @SepTravel=0, @SepEquip=0

	select @LowLevel=c.MaxLevelCode, @pri_id=h.ProjectId 
	from LemHeader h
	join Company c on c.MatchId=h.CompanyId
	where h.id = @id 

	if exists(select *
		from LemHeader lh
		join DefaultEarning de on de.ProjectId=lh.ProjectId and de.CompanyId=lh.CompanyId and de.EarningType='L'
		where lh.id = @id and de.level1id is not null )
	begin
		select @SepLOA = 1
	end

	if exists(select *
		from LemHeader lh
		join DefaultEarning de on de.ProjectId=lh.ProjectId and de.CompanyId=lh.CompanyId and de.EarningType='T'
		where lh.id = @id and de.level1id is not null )
	begin
		select @SepTravel = 1
	end

	if exists(select *
		from LemHeader lh
		join DefaultEarning de on de.ProjectId=lh.ProjectId and de.CompanyId=lh.CompanyId and de.EarningType='E'
		where lh.id = @id and de.level1id is not null )
	begin
		select @SepEquip = 1
	end

	insert ReportLEMApprovalHeader(LogHeaderId, LogDate, LEM#, Project, ProjectName, CustomerCode, CustomerName, SiteLocation,
		POReference, CompanyName, Billable, Username, 
		CompanyAddress1, CompanyAddress2, CompanyAddress3, CompanyCity, CompanyState, CompanyZip, CompanyPhone, CompanyFax, CompanyEmail, CompanyWeb,
		SiteAddress, SiteCity, SiteState, SiteZip, CustomerAddress1, CustomerAddress2, CustomerAddress3, CustomerCity, CustomerState,  CustomerZip, 
		ProjectExtendedDescription, LEM_Desc)
	select h.Id [LogHeaderId], LogDate [Date], LemNum [LEM#], p.Code [Project], p.Name [ProjectName], p.CustomerCode, p.CustomerName, p.SiteLocation, p.POReference,
		c.CompanyName, p.Billable, @username Username,
		c.CompanyAddress1, c.CompanyAddress2, c.CompanyAddress3, c.CompanyCity, c.CompanyState, c.CompanyZip, c.CompanyPhone, c.CompanyFax, c.CompanyEmail, c.CompanyWeb,
		p.SiteAddress, p.SiteCity, p.SiteState, p.SiteZip, p.CustomerAddress, p.CustomerAddress2, p.CustomerAddress3, p.CustomerCity, p.CustomerState,  p.CustomerZip, 
		p.ProjectExtendedDescription, isnull(h.LEM_Desc,'')
	from LemHeader h
	join Project p on p.MatchId = h.ProjectId 
	join Company c on c.MatchId = p.CompanyId
	where h.id = @id
	

	-- Labor
	insert ReportLEMApprovalLabour(LogHeaderId, EntryID, EmpNum, Name, WorkClassCode, WorkClass, 
		Level1Code, Level1, Level2Code, Level2, Level3Code, Level3, Level4Code, Level4, 
		Billable, BillAmount, Component, Username, TotalHours, 
		RegularHours, RegularRate, OTHours, OTRate, TravelHours, TravelRate, LOAHours, LOARate, EquipmentHours, 
		EquipmentRate, OtherHours, OtherRate, DTHours, DTRate, Lv1Id, Lv2Id, Lv3Id, Lv4Id, COCode)
	select h.LogHeaderId, h.id EntryID,
		h.EmpNum, case when isnull(e.FirstName,'') != '' then isnull(e.FirstName,'') + ' ' else '' end + isnull(e.LastName,'') [Name],
		wc.Code [WorkClassCode], wc.[Desc] [WorkClass], 
		isnull(lc1.Code,'') [Level1Code], isnull(lc1.[Desc],'') [Level1], isnull(lc2.Code,'') [Level2Code], isnull(lc2.[Desc],'') [Level2],
		isnull(lc3.Code,'') [Level3Code], isnull(lc3.[Desc],'') [Level3], isnull(lc4.Code,'') [Level4Code], isnull(lc4.[Desc],'') [Level4],
		h.Billable, isnull(h.BillAmount,0), 'L' Component, @username Username,
		isnull((select sum(case when tc.ValueType = 'Hours' then d.WorkHours else 0 end) from LabourTimeDetail d join TimeCode tc on tc.MatchId = d.TimeCodeId where ReportTypeColumn = 'Regular' and d.EntryId = h.Id),0) +
		isnull((select sum(case when tc.ValueType = 'Hours' then d.WorkHours else 0 end) from LabourTimeDetail d join TimeCode tc on tc.MatchId = d.TimeCodeId where ReportTypeColumn = 'Overtime' and d.EntryId = h.Id),0) +
		case when @SepTravel = 1 then 0 else isnull((select sum(case when tc.ValueType = 'Hours' then d.WorkHours else 0 end) from LabourTimeDetail d join TimeCode tc on tc.MatchId = d.TimeCodeId where ReportTypeColumn = 'Travel' and d.EntryId = h.Id),0) end +		
		case when @SepLOA = 1 then 0 else isnull((select sum(case when tc.ValueType = 'Hours' then d.WorkHours else 0 end) from LabourTimeDetail d join TimeCode tc on tc.MatchId = d.TimeCodeId where ReportTypeColumn = 'LOA' and d.EntryId = h.Id),0) end +
		case when @SepEquip = 1 then 0 else isnull((select sum(case when tc.ValueType = 'Hours' then d.WorkHours else 0 end) from LabourTimeDetail d join TimeCode tc on tc.MatchId = d.TimeCodeId where ReportTypeColumn = 'Equipment' and d.EntryId = h.Id),0) end +
		isnull((select sum(case when tc.ValueType = 'Hours' then d.WorkHours else 0 end) from LabourTimeDetail d join TimeCode tc on tc.MatchId = d.TimeCodeId where ReportTypeColumn = 'Other' and d.EntryId = h.Id),0) +
		isnull((select sum(case when tc.ValueType = 'Hours' then d.WorkHours else 0 end) from LabourTimeDetail d join TimeCode tc on tc.MatchId = d.TimeCodeId where ReportTypeColumn = 'Double Time' and d.EntryId = h.Id),0),

		isnull((select sum(case when tc.ValueType = 'Hours' then d.WorkHours else case when isnull(d.amount,0) != 0 then 1 else 0 end end) from LabourTimeDetail d join TimeCode tc on tc.MatchId = d.TimeCodeId where ReportTypeColumn = 'Regular' and d.EntryId = h.Id),0),
		isnull((select max(case when tc.ValueType = 'Hours' then d.BillRate else d.Amount end) from LabourTimeDetail d join TimeCode tc on tc.MatchId = d.TimeCodeId where ReportTypeColumn = 'Regular' and d.EntryId = h.Id),0),

		isnull((select sum(case when tc.ValueType = 'Hours' then d.WorkHours else case when isnull(d.amount,0) != 0 then 1 else 0 end end) from LabourTimeDetail d join TimeCode tc on tc.MatchId = d.TimeCodeId where ReportTypeColumn = 'Overtime' and d.EntryId = h.Id),0),
		isnull((select max(case when tc.ValueType = 'Hours' then d.BillRate else d.Amount end) from LabourTimeDetail d join TimeCode tc on tc.MatchId = d.TimeCodeId where ReportTypeColumn = 'Overtime' and d.EntryId = h.Id),0),

		case when @SepTravel = 1 then 0 else isnull((select sum(case when tc.ValueType = 'Hours' then d.WorkHours else case when isnull(d.amount,0) != 0 then 1 else 0 end end) from LabourTimeDetail d join TimeCode tc on tc.MatchId = d.TimeCodeId where ReportTypeColumn = 'Travel' and d.EntryId = h.Id),0) end,
		case when @SepTravel = 1 then 0 else isnull((select max(case when tc.ValueType = 'Hours' then d.BillRate else d.Amount end) from LabourTimeDetail d join TimeCode tc on tc.MatchId = d.TimeCodeId where ReportTypeColumn = 'Travel' and d.EntryId = h.Id),0) end,

		case when @SepLOA = 1 then 0 else isnull((select sum(case when tc.ValueType = 'Hours' then d.WorkHours else case when isnull(d.amount,0) != 0 then 1 else 0 end end) from LabourTimeDetail d join TimeCode tc on tc.MatchId = d.TimeCodeId where ReportTypeColumn = 'LOA' and d.EntryId = h.Id),0) end,
		case when @SepLOA = 1 then 0 else isnull((select max(case when tc.ValueType = 'Hours' then d.BillRate else d.Amount end) from LabourTimeDetail d join TimeCode tc on tc.MatchId = d.TimeCodeId where ReportTypeColumn = 'LOA' and d.EntryId = h.Id),0) end,

		case when @SepEquip = 1 then 0 else isnull((select sum(case when tc.ValueType = 'Hours' then d.WorkHours else case when isnull(d.amount,0) != 0 then 1 else 0 end end) from LabourTimeDetail d join TimeCode tc on tc.MatchId = d.TimeCodeId where ReportTypeColumn = 'Equipment' and d.EntryId = h.Id),0) end,
		case when @SepEquip = 1 then 0 else isnull((select max(case when tc.ValueType = 'Hours' then d.BillRate else d.Amount end) from LabourTimeDetail d join TimeCode tc on tc.MatchId = d.TimeCodeId where ReportTypeColumn = 'Equipment' and d.EntryId = h.Id),0) end,

		isnull((select sum(case when tc.ValueType = 'Hours' then d.WorkHours else case when isnull(d.amount,0) != 0 then 1 else 0 end end) from LabourTimeDetail d join TimeCode tc on tc.MatchId = d.TimeCodeId where ReportTypeColumn = 'Other' and d.EntryId = h.Id),0),
		isnull((select max(case when tc.ValueType = 'Hours' then d.BillRate else d.Amount end) from LabourTimeDetail d join TimeCode tc on tc.MatchId = d.TimeCodeId where ReportTypeColumn = 'Other' and d.EntryId = h.Id),0),

		isnull((select sum(case when tc.ValueType = 'Hours' then d.WorkHours else case when isnull(d.amount,0) != 0 then 1 else 0 end end) from LabourTimeDetail d join TimeCode tc on tc.MatchId = d.TimeCodeId where ReportTypeColumn = 'Double Time' and d.EntryId = h.Id),0),
		isnull((select max(case when tc.ValueType = 'Hours' then d.BillRate else d.Amount end) from LabourTimeDetail d join TimeCode tc on tc.MatchId = d.TimeCodeId where ReportTypeColumn = 'Double Time' and d.EntryId = h.Id),0),
		h.Level1Id, h.Level2Id, h.Level3Id, h.Level4Id, cast(p.Code as varchar(15))+'-'+cast(co.ChangeOrderNum as varchar(10))
	from LabourTimeEntry h
	join LemHeader lh on lh.id=h.LogHeaderId and lh.CompanyId=h.CompanyId
	join Project p on p.MatchId = lh.ProjectId and p.CompanyId=lh.CompanyId
	join Employee e on e.EmpNum = h.EmpNum and e.CompanyId = h.CompanyId
	join WorkClass wc on wc.Code = h.WorkClassCode and wc.CompanyId = h.CompanyId
	left join Level1Code lc1 on lc1.MatchId = h.Level1Id and lc1.CompanyId = h.CompanyId
	left join Level2Code lc2 on lc2.MatchId = h.Level2Id and lc2.CompanyId = h.CompanyId
	left join Level3Code lc3 on lc3.MatchId = h.Level3Id and lc3.CompanyId = h.CompanyId
	left join Level4Code lc4 on lc4.MatchId = h.Level4Id and lc4.CompanyId = h.CompanyId
	left outer join changeorder co on co.EstimateId=h.ChangeOrderId and co.CompanyId=h.CompanyId
	where h.LogHeaderId = @id and h.Billable = 1
	

	if( @SepLOA = 1 )
	begin
		-- Labor - LOA
		insert ReportLEMApprovalLabour(LogHeaderId, EntryID, EmpNum, Name, WorkClassCode, WorkClass, 
			Level1Code, Level1, Level2Code, Level2, Level3Code, Level3, Level4Code, Level4, 
			Billable, BillAmount, Component, Username, TotalHours,
			LOAHours, LOARate, Lv1Id, Lv2Id, Lv3Id, Lv4Id, COCode, type)
		select h.LogHeaderId, h.id EntryID,
			h.EmpNum, case when isnull(e.FirstName,'') != '' then isnull(e.FirstName,'') + ' ' else '' end + isnull(e.LastName,'') [Name],
			wc.Code [WorkClassCode], wc.[Desc] [WorkClass], 
			isnull(lc1.Code,'') [Level1Code], isnull(lc1.[Desc],'') [Level1], isnull(lc2.Code,'') [Level2Code], isnull(lc2.[Desc],'') [Level2],
			isnull(lc3.Code,'') [Level3Code], isnull(lc3.[Desc],'') [Level3], isnull(lc4.Code,'') [Level4Code], isnull(lc4.[Desc],'') [Level4],
			h.Billable, isnull(h.BillAmount,0), 'L' Component, @username Username, 		
			isnull((select sum(case when tc.ValueType = 'Hours' then d.WorkHours else 0 end) 
					from LabourTimeDetail d 
					join TimeCode tc on tc.MatchId = d.TimeCodeId 
					where ReportTypeColumn = 'LOA' and d.EntryId = h.Id),0),
			isnull((select sum(case when tc.ValueType = 'Hours' then d.WorkHours else case when isnull(d.amount,0) != 0 then 1 else 0 end end) 
					from LabourTimeDetail d 
					join TimeCode tc on tc.MatchId = d.TimeCodeId 
					where ReportTypeColumn = 'LOA' and d.EntryId = h.Id),0),
			isnull((select max(case when tc.ValueType = 'Hours' then d.BillRate else d.Amount end) 
					from LabourTimeDetail d 
					join TimeCode tc on tc.MatchId = d.TimeCodeId 
					where ReportTypeColumn = 'LOA' and d.EntryId = h.Id),0),
			de.Level1Id, de.Level2Id, de.Level3Id, de.Level4Id, cast(p.Code as varchar(15))+'-'+cast(co.ChangeOrderNum as varchar(10)), 'L'
		from LabourTimeEntry h
		join LemHeader lh on lh.id=h.LogHeaderId and lh.CompanyId=h.CompanyId
		join Project p on p.MatchId = lh.ProjectId and p.CompanyId=lh.CompanyId
		join DefaultEarning de on de.ProjectId=lh.ProjectId and de.CompanyId=lh.CompanyId and de.EarningType = 'L'
		join Employee e on e.EmpNum = h.EmpNum and e.CompanyId = h.CompanyId
		join WorkClass wc on wc.Code = h.WorkClassCode and wc.CompanyId = h.CompanyId
		left join Level1Code lc1 on lc1.MatchId = de.Level1Id and lc1.CompanyId = de.CompanyId
		left join Level2Code lc2 on lc2.MatchId = de.Level2Id and lc2.CompanyId = de.CompanyId
		left join Level3Code lc3 on lc3.MatchId = de.Level3Id and lc3.CompanyId = de.CompanyId
		left join Level4Code lc4 on lc4.MatchId = de.Level4Id and lc4.CompanyId = de.CompanyId
		left outer join changeorder co on co.EstimateId=h.ChangeOrderId and co.CompanyId=h.CompanyId
		where h.LogHeaderId = @id and h.Billable = 1

		delete from ReportLEMApprovalLabour where username=@username and type='L' and LOAHours = 0
	end

	if( @SepTravel = 1 )
	begin
		-- Labor - Travel
		insert ReportLEMApprovalLabour(LogHeaderId, EntryID, EmpNum, Name, WorkClassCode, WorkClass, 
			Level1Code, Level1, Level2Code, Level2, Level3Code, Level3, Level4Code, Level4, 
			Billable, BillAmount, Component, Username, TotalHours,
			TravelHours, TravelRate, Lv1Id, Lv2Id, Lv3Id, Lv4Id, COCode, type)
		select h.LogHeaderId, h.id EntryID,
			h.EmpNum, case when isnull(e.FirstName,'') != '' then isnull(e.FirstName,'') + ' ' else '' end + isnull(e.LastName,'') [Name],
			wc.Code [WorkClassCode], wc.[Desc] [WorkClass], 
			isnull(lc1.Code,'') [Level1Code], isnull(lc1.[Desc],'') [Level1], isnull(lc2.Code,'') [Level2Code], isnull(lc2.[Desc],'') [Level2],
			isnull(lc3.Code,'') [Level3Code], isnull(lc3.[Desc],'') [Level3], isnull(lc4.Code,'') [Level4Code], isnull(lc4.[Desc],'') [Level4],
			h.Billable, isnull(h.BillAmount,0), 'L' Component, @username Username,
			isnull((select sum(case when tc.ValueType = 'Hours' then d.WorkHours else 0 end) 
					from LabourTimeDetail d 
					join TimeCode tc on tc.MatchId = d.TimeCodeId 
					where ReportTypeColumn = 'Travel' and d.EntryId = h.Id),0),
			isnull((select sum(case when tc.ValueType = 'Hours' then d.WorkHours else case when isnull(d.amount,0) != 0 then 1 else 0 end end) 
					from LabourTimeDetail d 
					join TimeCode tc on tc.MatchId = d.TimeCodeId 
					where ReportTypeColumn = 'Travel' and d.EntryId = h.Id),0),
			isnull((select max(case when tc.ValueType = 'Hours' then d.BillRate else d.Amount end) 
					from LabourTimeDetail d 
					join TimeCode tc on tc.MatchId = d.TimeCodeId 
					where ReportTypeColumn = 'Travel' and d.EntryId = h.Id),0),
			de.Level1Id, de.Level2Id, de.Level3Id, de.Level4Id, cast(p.Code as varchar(15))+'-'+cast(co.ChangeOrderNum as varchar(10)), 'T'
		from LabourTimeEntry h
		join LemHeader lh on lh.id=h.LogHeaderId and lh.CompanyId=h.CompanyId
		join Project p on p.MatchId = lh.ProjectId and p.CompanyId=lh.CompanyId
		join DefaultEarning de on de.ProjectId=lh.ProjectId and de.CompanyId=lh.CompanyId and de.EarningType = 'T'
		join Employee e on e.EmpNum = h.EmpNum and e.CompanyId = h.CompanyId
		join WorkClass wc on wc.Code = h.WorkClassCode and wc.CompanyId = h.CompanyId
		left join Level1Code lc1 on lc1.MatchId = de.Level1Id and lc1.CompanyId = de.CompanyId
		left join Level2Code lc2 on lc2.MatchId = de.Level2Id and lc2.CompanyId = de.CompanyId
		left join Level3Code lc3 on lc3.MatchId = de.Level3Id and lc3.CompanyId = de.CompanyId
		left join Level4Code lc4 on lc4.MatchId = de.Level4Id and lc4.CompanyId = de.CompanyId
		left outer join changeorder co on co.EstimateId=h.ChangeOrderId and co.CompanyId=h.CompanyId
		where h.LogHeaderId = @id and h.Billable = 1
	
		delete from ReportLEMApprovalLabour where username=@username and type='T' and TravelHours = 0
	end
		

	if( @SepEquip = 1 )
	begin
		-- Labor - Equip
		insert ReportLEMApprovalLabour(LogHeaderId, EntryID, EmpNum, Name, WorkClassCode, WorkClass, 
			Level1Code, Level1, Level2Code, Level2, Level3Code, Level3, Level4Code, Level4, 
			Billable, BillAmount, Component, Username, TotalHours,
			EquipmentHours, EquipmentRate, Lv1Id, Lv2Id, Lv3Id, Lv4Id, COCode, type)
		select h.LogHeaderId, h.id EntryID,
			h.EmpNum, case when isnull(e.FirstName,'') != '' then isnull(e.FirstName,'') + ' ' else '' end + isnull(e.LastName,'') [Name],
			wc.Code [WorkClassCode], wc.[Desc] [WorkClass], 
			isnull(lc1.Code,'') [Level1Code], isnull(lc1.[Desc],'') [Level1], isnull(lc2.Code,'') [Level2Code], isnull(lc2.[Desc],'') [Level2],
			isnull(lc3.Code,'') [Level3Code], isnull(lc3.[Desc],'') [Level3], isnull(lc4.Code,'') [Level4Code], isnull(lc4.[Desc],'') [Level4],
			h.Billable, isnull(h.BillAmount,0), 'L' Component, @username Username,
			isnull((select sum(case when tc.ValueType = 'Hours' then d.WorkHours else 0 end) 
					from LabourTimeDetail d 
					join TimeCode tc on tc.MatchId = d.TimeCodeId 
					where ReportTypeColumn = 'Equipment' and d.EntryId = h.Id),0),
			isnull((select sum(case when tc.ValueType = 'Hours' then d.WorkHours else case when isnull(d.amount,0) != 0 then 1 else 0 end end) 
					from LabourTimeDetail d 
					join TimeCode tc on tc.MatchId = d.TimeCodeId 
					where ReportTypeColumn = 'Equipment' and d.EntryId = h.Id),0),
			isnull((select max(case when tc.ValueType = 'Hours' then d.BillRate else d.Amount end) 
					from LabourTimeDetail d 
					join TimeCode tc on tc.MatchId = d.TimeCodeId 
					where ReportTypeColumn = 'Equipment' and d.EntryId = h.Id),0),
			de.Level1Id, de.Level2Id, de.Level3Id, de.Level4Id, cast(p.Code as varchar(15))+'-'+cast(co.ChangeOrderNum as varchar(10)), 'E'
		from LabourTimeEntry h
		join LemHeader lh on lh.id=h.LogHeaderId and lh.CompanyId=h.CompanyId
		join Project p on p.MatchId = lh.ProjectId and p.CompanyId=lh.CompanyId
		join DefaultEarning de on de.ProjectId=lh.ProjectId and de.CompanyId=lh.CompanyId and de.EarningType = 'E'
		join Employee e on e.EmpNum = h.EmpNum and e.CompanyId = h.CompanyId
		join WorkClass wc on wc.Code = h.WorkClassCode and wc.CompanyId = h.CompanyId
		left join Level1Code lc1 on lc1.MatchId = de.Level1Id and lc1.CompanyId = de.CompanyId
		left join Level2Code lc2 on lc2.MatchId = de.Level2Id and lc2.CompanyId = de.CompanyId
		left join Level3Code lc3 on lc3.MatchId = de.Level3Id and lc3.CompanyId = de.CompanyId
		left join Level4Code lc4 on lc4.MatchId = de.Level4Id and lc4.CompanyId = de.CompanyId
		left outer join changeorder co on co.EstimateId=h.ChangeOrderId and co.CompanyId=h.CompanyId
		where h.LogHeaderId = @id and h.Billable = 1

		delete from ReportLEMApprovalLabour where username=@username and type='E' and EquipmentHours = 0
	end
	
	
	update ReportLEMApprovalLabour
	set BillAmount = 
		round(isnull(RegularHours,0)*isnull(RegularRate,0),2) + 
		round(isnull(OTHours,0)*isnull(OTRate,0),2) + 
		round(isnull(TravelHours,0)*isnull(TravelRate,0),2) + 
		round(isnull(LOAHours,0)*isnull(LOARate,0),2) + 
		round(isnull(EquipmentHours,0)*isnull(EquipmentRate,0),2) + 
		round(isnull(OtherHours,0)*isnull(OtherRate,0),2) + 
		round(isnull(DTHours,0)*isnull(DTRate,0),2) 
	where username=@username
	


	-- clear up blank LOA - Travel - Equip
	if(@LowLevel = 1)
	begin
		update r
		set r.CostCode=c.MappingCode
		from ReportLEMApprovalLabour r
		join CostCodeMapping c on c.MappingId=r.Lv1Id and c.ProjectId=@pri_id
		where r.username=@username
	end

	if(@LowLevel = 2)
	begin
		update r
		set r.CostCode=c.MappingCode
		from ReportLEMApprovalLabour r
		join CostCodeMapping c on c.MappingId=r.Lv2Id and c.ProjectId=@pri_id
		where r.username=@username
	end

	if(@LowLevel = 3)
	begin
		update r
		set r.CostCode=c.MappingCode
		from ReportLEMApprovalLabour r
		join CostCodeMapping c on c.MappingId=r.Lv3Id and c.ProjectId=@pri_id
		where r.username=@username
	end

	if(@LowLevel = 4)
	begin
		update r
		set r.CostCode=c.MappingCode
		from ReportLEMApprovalLabour r
		join CostCodeMapping c on c.MappingId=r.Lv4Id and c.ProjectId=@pri_id
		where r.username=@username
	end

	insert ReportLEMApprovalLabourDetail(EntryId, BillRate, WorkHours, Amount, Description, ValueType, BillingRateType, Component, Username)
	select d.EntryId, isnull(BillRate,0), isnull(WorkHours,0), isnull(Amount,0), tc.[Desc], tc.ValueType, tc.BillingRateType, tc.Component Component, @username Username
	from LabourTimeDetail d join TimeCode tc on tc.MatchId = d.TimeCodeId 
	where d.EntryID in (select id from LabourTimeEntry h where h.LogHeaderId = @id)
	order by EntryId, tc.MatchId


	-- Equipment
	insert ReportLEMApprovalEquipment(LogHeaderId, Level1Code, Level1, Level2Code, Level2, Level3Code, Level3, Level4Code, Level4,
		AssetCode, Asset, ClassCode, Class, CategoryCode, Category, Billable, Quantity, BillAmount, UnitBillRate, BillCycle, Component, 
		Username, OwnerType, Lv1Id, Lv2Id, Lv3Id, Lv4Id, COCode)
	select h.LogHeaderId,
		isnull(lc1.Code,'') [Level1Code], isnull(lc1.[Desc],'') [Level1], isnull(lc2.Code,'') [Level2Code], isnull(lc2.[Desc],'') [Level2],
		isnull(lc3.Code,'') [Level3Code], isnull(lc3.[Desc],'') [Level3], isnull(lc4.Code,'') [Level4Code], isnull(lc4.[Desc],'') [Level4],
		e.AssetCode, e.[Desc] [Asset], e.ClassCode, ec.[Desc] [Class], e.CategoryCode, cat.[Desc] [Category],
		h.Billable, isnull(h.Quantity,0), isnull(h.BillAmount,0), 
		isnull(cast(case when isnull(h.Quantity,0) = 0 then 0 else BillAmount / h.Quantity end as money),0) UnitBillRate, 
		h.BillCycle, 'E' Component, @username Username, e.OwnerType, h.Level1Id, h.Level2Id, h.Level3Id, h.Level4Id, cast(p.Code as varchar(15))+'-'+cast(co.ChangeOrderNum as varchar(10))
	from EquipTimeEntry h
	join LemHeader lh on lh.id=h.LogHeaderId and lh.CompanyId=h.CompanyId
	join Project p on p.MatchId = lh.ProjectId and p.CompanyId=lh.CompanyId
	join Equipment e on e.EqpNum = h.EqpNum and e.CompanyId = h.CompanyId
	join EquipmentClass ec on ec.Code = e.ClassCode and ec.CompanyId = h.CompanyId
	join EquipmentCategory cat on cat.Code = e.CategoryCode and cat.CompanyId = h.CompanyId
	left join Level1Code lc1 on lc1.MatchId = h.Level1Id and lc1.CompanyId = h.CompanyId
	left join Level2Code lc2 on lc2.MatchId = h.Level2Id and lc2.CompanyId = h.CompanyId
	left join Level3Code lc3 on lc3.MatchId = h.Level3Id and lc3.CompanyId = h.CompanyId
	left join Level4Code lc4 on lc4.MatchId = h.Level4Id and lc4.CompanyId = h.CompanyId
	left outer join changeorder co on co.EstimateId=h.ChangeOrderId and co.CompanyId=h.CompanyId
	where h.LogHeaderId = @id and h.Billable = 1

	if(@LowLevel = 1)
	begin
		update r
		set r.CostCode=c.MappingCode
		from ReportLEMApprovalEquipment r
		join CostCodeMapping c on c.MappingId=r.Lv1Id and c.ProjectId=@pri_id
		where r.username=@username
	end

	if(@LowLevel = 2)
	begin
		update r
		set r.CostCode=c.MappingCode
		from ReportLEMApprovalEquipment r
		join CostCodeMapping c on c.MappingId=r.Lv2Id and c.ProjectId=@pri_id
		where r.username=@username
	end

	if(@LowLevel = 3)
	begin
		update r
		set r.CostCode=c.MappingCode
		from ReportLEMApprovalEquipment r
		join CostCodeMapping c on c.MappingId=r.Lv3Id and c.ProjectId=@pri_id
		where r.username=@username
	end

	if(@LowLevel = 4)
	begin
		update r
		set r.CostCode=c.MappingCode
		from ReportLEMApprovalEquipment r
		join CostCodeMapping c on c.MappingId=r.Lv4Id and c.ProjectId=@pri_id
		where r.username=@username
	end

	
	-- AP
	insert ReportLEMApprovalAP(LogHeaderId, InvoiceDate, InvoiceNum, PONum, LineNum, Description, Amount, InvoiceAmount, 
		HeaderMarkupAmount, MarkupAmount, MarkupPercent, BillAmount, Username, 
		SupplierName, SupplierCode,	 Level1Code, Level1, Level2Code, Level2, Level3Code, Level3, Level4Code, Level4, Lv1Id, Lv2Id, Lv3Id, Lv4Id)
	select h.LogHeaderId, InvoiceDate, h.InvoiceNum, h.PONum, 
		d.LineNum, d.[Description], isnull(d.Amount,0), isnull(h.InvoiceAmount,0), isnull(h.MarkupAmount,0) [HeaderMarkupAmount], isnull(d.MarkupAmount,0) [MarkupAmount], isnull(d.MarkupPercent,0), 
		isnull(d.BillAmount,0), @username Username,
		s.SupplierName, s.SupplierCode, 
		isnull(lc1.Code,'') [Level1Code], isnull(lc1.[Desc],'') [Level1], isnull(lc2.Code,'') [Level2Code], isnull(lc2.[Desc],'') [Level2],
		isnull(lc3.Code,'') [Level3Code], isnull(lc3.[Desc],'') [Level3], isnull(lc4.Code,'') [Level4Code], isnull(lc4.[Desc],'') [Level4],
		d.Level1Id, d.Level2Id, d.Level3Id, d.Level4Id
	from LemAP h
	join LemAPDetail d on h.Id = d.LemAPId and h.CompanyID = d.CompanyID
	left join Level1Code lc1 on lc1.MatchId = d.Level1Id and lc1.CompanyId = h.CompanyId
	left join Level2Code lc2 on lc2.MatchId = d.Level2Id and lc2.CompanyId = h.CompanyId
	left join Level3Code lc3 on lc3.MatchId = d.Level3Id and lc3.CompanyId = h.CompanyId
	left join Level4Code lc4 on lc4.MatchId = d.Level4Id and lc4.CompanyId = h.CompanyId
	join Supplier s on h.SupplierCode = s.SupplierCode and h.CompanyId = s.CompanyId
	where h.LogHeaderId = @id

	if(@LowLevel = 1)
	begin
		update r
		set r.CostCode=c.MappingCode
		from ReportLEMApprovalAP r
		join CostCodeMapping c on c.MappingId=r.Lv1Id and c.ProjectId=@pri_id
		where r.username=@username
	end

	if(@LowLevel = 2)
	begin
		update r
		set r.CostCode=c.MappingCode
		from ReportLEMApprovalAP r
		join CostCodeMapping c on c.MappingId=r.Lv2Id and c.ProjectId=@pri_id
		where r.username=@username
	end

	if(@LowLevel = 3)
	begin
		update r
		set r.CostCode=c.MappingCode
		from ReportLEMApprovalAP r
		join CostCodeMapping c on c.MappingId=r.Lv3Id and c.ProjectId=@pri_id
		where r.username=@username
	end

	if(@LowLevel = 4)
	begin
		update r
		set r.CostCode=c.MappingCode
		from ReportLEMApprovalAP r
		join CostCodeMapping c on c.MappingId=r.Lv4Id and c.ProjectId=@pri_id
		where r.username=@username
	end		


	update ReportLEMApprovalHeader set
	TotalAPAmt = isnull((select SUM(isnull(a.BillAmount,0)) from ReportLEMApprovalAP a where a.Username = ReportLEMApprovalHeader.Username and a.LogHeaderId = ReportLEMApprovalHeader.LogHeaderId),0),
	TotalEqAmt = isnull((select SUM(isnull(e.BillAmount,0)) from ReportLEMApprovalEquipment e where e.Username = ReportLEMApprovalHeader.Username and e.LogHeaderId = ReportLEMApprovalHeader.LogHeaderId),0),
	TotalLabHrs = isnull((select SUM(isnull(l.TotalHours,0)) from ReportLEMApprovalLabour l where l.Username = ReportLEMApprovalHeader.Username and l.LogHeaderId = ReportLEMApprovalHeader.LogHeaderId),0),
	TotalLabAmt = isnull((select SUM(isnull(l.BillAmount,0)) from ReportLEMApprovalLabour l where l.Username = ReportLEMApprovalHeader.Username and l.LogHeaderId = ReportLEMApprovalHeader.LogHeaderId),0),
	TotalLabTravelAmt = isnull((select SUM(isnull(l.TravelHours,0) * isnull(l.TravelRate,0)) from ReportLEMApprovalLabour l where l.Username = ReportLEMApprovalHeader.Username and l.LogHeaderId = ReportLEMApprovalHeader.LogHeaderId),0),
	TotalLabLOAAmt = isnull((select SUM(isnull(l.LOAHours,0) * isnull(l.LOARate,0)) from ReportLEMApprovalLabour l where l.Username = ReportLEMApprovalHeader.Username and l.LogHeaderId = ReportLEMApprovalHeader.LogHeaderId),0),
	TotalLabEqAmt = isnull((select SUM(isnull(l.EquipmentHours,0) * isnull(l.EquipmentRate,0)) from ReportLEMApprovalLabour l where l.Username = ReportLEMApprovalHeader.Username and l.LogHeaderId = ReportLEMApprovalHeader.LogHeaderId),0)
	where username = @username
end


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
create Procedure [dbo].[sp_GenerateScripts] @ScriptBuildType varchar(1) = 'D' AS
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
		if @Name in ('fn_GetConcatInvNo_New')
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
