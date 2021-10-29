CREATE database parkingDB;
GO
USE parkingDB
GO
CREATE TABLE reservation(
ReservationId uniqueidentifier,
CheckIn DateTime,
CheckOut DateTime,
CarPlate varchar(50),
CarType varchar(50),
CarColor varchar(50),
Cost decimal
)
GO
CREATE TABLE garage(
GarageId uniqueidentifier,
SlotsAvailable int,
Name varchar(50)
)
GO
DECLARE @NewID uniqueidentifier = NewID();
INSERT INTO garage(GarageId, SlotsAvailable, Name)
VALUES(@NewID, 10, 'My Garage')
GO
CREATE PROCEDURE AddReservation
(
@ReservationId uniqueidentifier,
@CheckIn DateTime,
@CheckOut DateTime,
@CarPlate varchar(50),
@CarType varchar(50),
@CarColor varchar(50),
@Cost decimal
)
AS
BEGIN
INSERT INTO reservation(ReservationId, CheckIn, CheckOut, CarPlate, CarType, CarColor, Cost)
VALUES(@ReservationId, @CheckIn, @CheckOut, @CarPlate, @CarType, @CarColor, @Cost)
END
GO
CREATE PROCEDURE GetReservation
(@ReservationId uniqueidentifier)
AS
BEGIN
SELECT * FROM reservation WHERE ReservationId=@ReservationId
END
GO
CREATE PROCEDURE GetAvailableSlots
AS
BEGIN
SELECT SlotsAvailable FROM garage
END
GO
CREATE PROCEDURE UpdateReservation (
@ReservationId uniqueidentifier,
@CheckOut DateTime,
@Cost Decimal
)
AS
BEGIN
UPDATE reservation 
SET CheckOut=@CheckOut, Cost= @Cost 
WHERE ReservationId=@ReservationId
END


