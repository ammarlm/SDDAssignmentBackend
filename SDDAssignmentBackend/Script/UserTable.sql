-- Users Table
CREATE TABLE Users (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Username NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Salt NVARCHAR(255) NOT NULL,
    Role NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE()
);


---- add default admin user
  INSERT INTO [SDDAssignmentDB].[dbo].[Users] (id, Username, PasswordHash, Salt, Role, CreatedAt)
  VALUES ('A3E5C4E8-B1CF-4BB7-7E3C-08DD815FAC26', 
          'admin', 	
          'iD4fheaBwyF3xmc2k2Uhfuafe7d2wQG9seE0E9LWrVc=',	
          'DlGxbjMSbItwTWOKXoi7tw==',	
          'Administrator',	
          '2025-04-22 09:36:46.643');