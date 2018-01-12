
go
	-- cleans up blank External Equipment Requisition tabs through the system
	delete from SecurityFunction where ID in (10864, 10859, 20633, 20650, 20578, 20595, 20533, 20550, 10906, 20346, 20363, 
		10954, 10965, 10887, 10896, 20387, 20404, 21439, 21457)

go

	-- cleans up blank External Equipment Requisition tabs through the system
	delete from SECURITY where FUNCTION_ID  in (10864, 10859, 20633, 20650, 20578, 20595, 20533, 20550, 10906, 20346, 20363, 
		10954, 10965, 10887, 10896, 20387, 20404, 21439, 21457)
	
go
