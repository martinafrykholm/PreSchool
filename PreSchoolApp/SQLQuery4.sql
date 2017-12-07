

Alter procedure AddChild
@firstName nvarchar(50),
@lastName nvarchar(50),
@unitsId int,
@id int output

As begin
	insert into PRS.Children (firstName, lastName, UnitsId) values(@firstName, @lastName, @unitsId)
	set @ID=SCOPE_IDENTITY(); 
	insert into PRS.Schedules (Weekdays, Dropoff, PickUp, ChildrenID) values(1, '08:00:00', '16:00:00', @id)
	insert into PRS.Schedules (Weekdays, Dropoff, PickUp, ChildrenID) values(2, '08:00:00', '16:00:00', @id)
	insert into PRS.Schedules (Weekdays, Dropoff, PickUp, ChildrenID) values(3, '08:00:00', '16:00:00', @id)
	insert into PRS.Schedules (Weekdays, Dropoff, PickUp, ChildrenID) values(4, '08:00:00', '16:00:00', @id)
	insert into PRS.Schedules (Weekdays, Dropoff, PickUp, ChildrenID) values(5, '08:00:00', '16:00:00', @id)
end


--select ID from PRS.Schedules where


go
alter procedure GetScheduleID
@childId int,
@weekdayNr int

as begin 
select ID from PRS.Schedules
where Weekdays=@weekdayNr and ChildrenID=@childId
end


--declare @IDRetur int
--execute GetScheduleID 7,3, @IDRetur output
