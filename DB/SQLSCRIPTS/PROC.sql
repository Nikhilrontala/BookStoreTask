
-- Get all rack Details

Alter PROCEDURE SP_GetAllRacks
with Encryption
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT rackId, rackCode, rackStatus
    FROM BS_RACKS 
END

--Insert New rack 

ALTER PROCEDURE SP_InsertRack
  @rackCode VARCHAR(20)
WITH ENCRYPTION
AS
BEGIN
  IF NOT EXISTS (SELECT 1 FROM BS_RACKS WHERE rackCode = @rackCode and rackStatus<>'Deleted')
  BEGIN
    INSERT INTO BS_RACKS (rackCode)
    VALUES (@rackCode)
  END
  ELSE
  BEGIN
    RAISERROR('Rack code already exists', 2,1)
  END
END


--update Rack

Alter PROCEDURE SP_UpdateRack
  @rackId INT,
  @newRackCode VARCHAR(20)
AS
BEGIN
  IF NOT EXISTS (SELECT 1 FROM BS_RACKS WHERE rackCode = @newRackCode AND rackId <> @rackId and rackStatus<>'Deleted')
  BEGIN
    UPDATE BS_RACKS
    SET rackCode = @newRackCode
    WHERE rackId = @rackId
  END
  ELSE
  BEGIN
    RAISERROR('-1', 2, 1)
  END
END


--Delete rack

Create PROCEDURE SP_DeleteRack
  @rackId INT
AS
BEGIN
    UPDATE BS_RACKS
    SET rackStatus = 'Deleted'
    WHERE rackId = @rackId
END


-- Get all self Details

CREATE PROCEDURE SP_GetAllBS_Shelves
with Encryption
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT BSS.selfId,BSS.selfCode,BSS.selfRackId,BSS.selfStatus,BSR.rackCode,BSR.rackStatus
    FROM BS_Shelves BSS INNER JOIN BS_RACKS BSR ON BSR.rackId= BSS.selfRackId
END



---insert Self
Alter PROCEDURE SP_InsertShelf
  @selfCode VARCHAR(20),
  @selfRackId INT
AS
BEGIN
  IF NOT EXISTS (SELECT 1 FROM BS_Shelves WHERE selfCode = @selfCode and selfStatus<>'Deleted')
  BEGIN
    INSERT INTO BS_Shelves (selfCode, selfRackId)
    VALUES (@selfCode, @selfRackId)
  END
  ELSE
  BEGIN
    -- Handle the case when the selfCode already exists
    -- You can choose to raise an error, return a specific value, or perform any other desired action
    RAISERROR('-1', 2, 1)
  END
END


-- Get Active Racks

Create PROCEDURE SP_GetActiveRacks
with Encryption
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT rackId, rackCode
    FROM BS_RACKS where rackStatus ='Active'
END



Alter PROCEDURE SP_UpdateShelf
    @selfId INT,
    @selfCode VARCHAR(20),
    @selfRackId INT
AS
BEGIN
 IF NOT EXISTS (SELECT 1 FROM BS_Shelves WHERE selfCode = @selfCode AND selfId <> @selfId and selfStatus<>'Deleted')
  BEGIN
     UPDATE BS_Shelves
    SET selfCode = @selfCode,
        selfRackId = @selfRackId
    WHERE selfId = @selfId;
  END
  ELSE
  BEGIN
    RAISERROR('-1', 2, 1)
  END

END


--Delete Shelves

Create PROCEDURE SP_DeleteShelves
  @selfId INT
AS
BEGIN
    UPDATE BS_Shelves
    SET selfStatus = 'Deleted'
    WHERE selfId = @selfId
END


-- Get all self Details

CREATE PROCEDURE SP_GetAllBooks
with Encryption
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT BSS.selfId,BSS.selfCode,BSS.selfStatus,BSR.rackCode,BOOK.BookId,BOOK.BookCode,BOOK.BookName,BOOK.BookAuthor,BOOK.BookIsAvail,BOOK.BookPrice,BOOK.BookselfId
    FROM BS_BOOKS BOOK INNER JOIN  BS_Shelves BSS ON BOOK.BookselfId= BSS.selfId
	INNER JOIN BS_RACKS BSR ON BSR.rackId= BSS.selfRackId

END


CREATE PROCEDURE InsertBook
    @BookCode VARCHAR(20),
    @BookName VARCHAR(100),
    @BookAuthor VARCHAR(20),
    @BookIsAvail VARCHAR(20),
    @BookPrice MONEY,
    @BookSelfId INT
	with Encryption
AS
BEGIN
    SET NOCOUNT ON;


	 IF NOT EXISTS (SELECT 1 FROM BS_BOOKS WHERE  BookCode <> @BookCode and BookStatus='Active')
  BEGIN
     INSERT INTO BS_BOOKS (BookCode, BookName, BookAuthor, BookIsAvail, BookPrice, BookSelfId)
    VALUES (@BookCode, @BookName, @BookAuthor, @BookIsAvail, @BookPrice, @BookSelfId);

  END
  ELSE
  BEGIN
    RAISERROR('Rack code already exists', 2,1)
  END

END