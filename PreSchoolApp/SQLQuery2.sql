go
alter procedure AddChild
@firstName nvarchar(50),
@lastName nvarchar(50),
@unitsId int,
@id int output

As begin
	insert into PRS.Children (firstName, lastName, UnitsId) values(@firstName, @lastName, @unitsId)
	set @ID=SCOPE_IDENTITY(); 
	insert into PRS.Schedules (Weekdays, Dropoff, PickUp, ChildrenID) values('Måndag', '08:00:00', '16:00:00', @id)
	insert into PRS.Schedules (Weekdays, Dropoff, PickUp, ChildrenID) values('Tisdag', '08:00:00', '16:00:00', @id)
	insert into PRS.Schedules (Weekdays, Dropoff, PickUp, ChildrenID) values('Onsdag', '08:00:00', '16:00:00', @id)
	insert into PRS.Schedules (Weekdays, Dropoff, PickUp, ChildrenID) values('Torsdag', '08:00:00', '16:00:00', @id)
	insert into PRS.Schedules (Weekdays, Dropoff, PickUp, ChildrenID) values('Fredag', '08:00:00', '16:00:00', @id)
end