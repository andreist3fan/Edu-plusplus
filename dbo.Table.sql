CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Username] NCHAR(15) NOT NULL, 
    [Password] NCHAR(20) NOT NULL, 
    [Email] NCHAR(320) NOT NULL, 
    [School] NCHAR(320) NOT NULL, 
    [Grade] INT NOT NULL
)
