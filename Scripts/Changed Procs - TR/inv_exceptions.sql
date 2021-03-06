
GO
/****** Object:  StoredProcedure [dbo].[inv_exceptions]    Script Date: 6/2/2017 10:23:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO







ALTER PROCEDURE [dbo].[inv_exceptions] AS      
    declare @inv_id int,  @whse_id int, @whs_on_hand float, @fifo_on_hand float, @counter int,      
                 @part_number char(20),  @serial_on_hand float, @serial char(1), @select_flag char(1)      
begin      
    select @counter = 0      
    declare curinvwhs  scroll cursor       
      for select w.whse_id, i.inv_id, round(qty_on_hand,3), part_number, serial      
      from inv_warehouse w, inventory i      
      where  i.inv_id = w.inv_id      
      
      if exists (select * from tempdb..sysobjects where id = object_id(N'tempdb..#InvExceptions'))       
         drop TABLE #InvExceptions      
      
      CREATE TABLE #InvExceptions(      
        INV_ID integer,      
        WHSE_ID integer,      
        WHS_ON_HAND float,      
        SERIAL_ON_HAND float,      
        FIFO_ON_HAND float,      
        SERIAL char(1),      
        PART_NUMBER char(20))      
      
      
    open curinvwhs      
    fetch next from curinvwhs into @whse_id, @inv_id,@whs_on_hand, @part_number, @serial      
    while @@fetch_status <> -1      
    begin      
       select @select_flag = 'N'      
       select @serial_on_hand = 0      
       select @fifo_on_hand = 0      
    
       if @whs_on_hand is null    
          select @whs_on_hand = 0    
     
      
       select @fifo_on_hand = sum(round(qty_on_hand,3))      
       from fifo      
       where inv_id = @inv_id      
       and   whse_id = @whse_id      
    
       if @fifo_on_hand is null    
          select @fifo_on_hand = 0    
      
       if @serial = 'T'      
       begin      
         select @serial_on_hand = sum(round(qty_on_hand,3))      
         from inv_serial s, inv_serial_whse w      
         where s.inv_id = @inv_id      
         and   w.whse_id = @whse_id      
         and   s.inv_serial_id = w.inv_serial_id      
    
         if @serial_on_hand is null    
            select @serial_on_hand = 0    
       end;      
      
       if abs(@whs_on_hand - @fifo_on_hand) <> 0
         select @select_flag = 'Y'      
       else      
       if @serial = 'T'      
       begin      
         if @serial_on_hand <> @fifo_on_hand      
        or @serial_on_hand <> @whs_on_hand      
            select @select_flag = 'Y'      
       end      
      
       if @select_flag = 'Y'      
       begin      
         insert #InvExceptions      
          ( INV_ID,  WHSE_ID, WHS_ON_HAND, SERIAL_ON_HAND, FIFO_ON_HAND, SERIAL, PART_NUMBER)      
          VALUES(@inv_id, @whse_id, @whs_on_hand, @serial_on_hand, @fifo_on_hand, @serial, @part_number)      
       end      
      
       fetch next from curinvwhs into @whse_id, @inv_id, @whs_on_hand, @part_number, @serial      
    end      
      
    select e.inv_id, e.whse_id, e.whs_on_hand, e.serial_on_hand, e.fifo_on_hand, e.serial, e.part_number, i.DESCRIPTION as gridColPartDesc
    from #InvExceptions e
	join inventory i on i.INV_ID = e.INV_ID
	      
    order by part_number      
      
    close curinvwhs       
    deallocate curinvwhs      
end

