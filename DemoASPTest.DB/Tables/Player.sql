CREATE TABLE [dbo].[Player]
(
  [Id] INT PRIMARY KEY IDENTITY,
  [FirstName] VARCHAR(50) NOT NULL,
  [LastName] VARCHAR(50) NOT NULL,
  [Email] VARCHAR(50) NOT NULL,
  [Password] VARCHAR(MAX) NOT NULL,
  CONSTRAINT [UQ_Player_Email] UNIQUE ([Email]),
  CONSTRAINT [CK_Player_Password] CHECK (LEN([Password]) >= 8)
)
