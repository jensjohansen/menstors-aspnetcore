IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'MentorsAcademyLog')
  CREATE DATABASE MentorsAcademyLog; 
GO

USE MentorsAcademyLog; 
GO


IF NOT EXISTS (SELECT[name] FROM sys.tables WHERE[name] = 'LogMessageTypes')
    CREATE TABLE LogMessageTypes (
    [Id] bigint NOT NULL Identity(1,1),
    [Name] varchar(70) DEFAULT NULL,
    [Description] varchar(255) DEFAULT NULL,
    [RetentionDays] int DEFAULT NULL,
    CONSTRAINT PK_LogMessageTypes PRIMARY KEY ([Id])
  );
GO

SET IDENTITY_INSERT LogMessageTypes ON
GO
IF NOT EXISTS (SELECT * FROM LogMessageTypes)
  Insert into LogMessageTypes(Id,[Name],[Description],[RetentionDays])
  Values
  (1,'Outage', 'SystemOutage', 365),
  (2,'Security', 'Security message', 30),
  (3,'Exception','Exception', 30),
  (4,'Error', 'Error', 30),
  (5,'Warning', 'Warning message', 15),
  (6,'Info', 'Informational message', 15),
  (7,'Debug', 'Debugging Message', 5)
  ;
GO

SET IDENTITY_INSERT LogMessageTypes OFF
GO

IF NOT EXISTS (SELECT[name] FROM sys.tables WHERE[name] = 'LogMessages')
    CREATE TABLE LogMessages (
    [Id] bigint NOT NULL IDENTITY(1,1),
    [LogMessageTypeId] bigint DEFAULT NULL INDEX IX_LogMessages_LogMessageTypeId ,
    [ApplicationName] varchar(70) DEFAULT NULL,
    [ApplicationMethod] varchar(70) DEFAULT NULL,
    [IpAddress] varchar(40) DEFAULT NULL,
    [LoginToken] varchar(255) DEFAULT NULL,
    [ShortMessage] varchar(255) DEFAULT NULL,
    [RequestHttpMethod] varchar(15) DEFAULT NULL,
    [RequestUri] text,
    [RequestParams] text,
    [RequestBody] text,
    [StatusCode] int DEFAULT NULL,
    [ResponseContent] text,
    [FullMessage] text,
    [Exception] text,
    [Trace] text,
    [Logged] datetime2 DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT PK_LogMessages PRIMARY KEY (Id),
    CONSTRAINT FK_LogMessages_LogMessageTypes_LogMessageTypeId FOREIGN KEY (LogMessageTypeId) REFERENCES LogMessageTypes (Id)
  );
GO

