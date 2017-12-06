go
alter procedure EditSchedule
@Dropoff time,
@PickUp time,
@ID int

as begin
update PRS.Schedules
set Dropoff=@Dropoff,Pickup=@PickUp
where ID=@ID
end