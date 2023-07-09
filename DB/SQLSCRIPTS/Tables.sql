CREATE TABLE BS_RACKS
(
rackId			int primary key identity(1,1),
rackCode		VARCHAR(20),
rackStatus		varchar(8) default('Active')
)
-----------------------------
GO

Create TABLE BS_Shelves
(
selfId			int primary key identity(1,1),
selfCode		VARCHAR(20),
selfRackId		int,
selfStatus		varchar(8) default('Active')
)
-----------------------------
GO

create TABLE BS_BOOKS
(
BookId  int primary key identity(1,1),
BookCode		VARCHAR(20),
BookName		VARCHAR(100),
BookAuthor		VARCHAR(20),
BookIsAvail		VARCHAR(20),
BookPrice		money,
BookselfId		int,
BookStatus		VARCHAR(20) default 'Active'
)
-------------------------
GO