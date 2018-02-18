-- SZ Architech : Initial database creating script  
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'MentorsAcademy')
  CREATE DATABASE MentorsAcademy; 
GO

USE MentorsAcademy; 
GO

-- Required Table for JWT Authentication 
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'JwtUsers' and xtype = 'U')
  CREATE TABLE [JwtUsers](
    [Id] bigint NOT NULL IDENTITY(1,1),
    [UserName] varchar(255) NOT NULL UNIQUE, 
    [Password] varchar(255) NOT NULL, 
    CONSTRAINT [PX_JwtUsers] PRIMARY KEY([Id]) 
  );
GO

IF NOT EXISTS (SELECT[name] FROM sys.tables WHERE[name] = 'Contributors')
  CREATE TABLE Contributors(
    [Id] bigint NOT NULL IDENTITY(1,1), -- COMMENT 'Primary Key for Contributors Entity'
    [Name] varchar(70), -- COMMENT 'Name for Contributors Entity'
    [Description] varchar(255), -- COMMENT 'Description or Tool Tip for Contributors Entity'
    [Degree] varchar(70), -- COMMENT 'College degree of Contributor'
    [AlmaMater] varchar(70), -- COMMENT 'Alma Mater Field'
    [Email] varchar(127), -- COMMENT 'Email Field'
    [Evaluations] int, -- COMMENT 'Evaluations Field'
    [Password] varchar(255), -- COMMENT 'Password Field'
    [Comments] varchar(max), -- COMMENT 'Comments for Contributors entry'
    [AuditEntered] datetime2, -- COMMENT 'Audit: date of entry for Contributors Entity'
    [AuditEnteredBy] bigint NULL INDEX IX_Contributors_AuditEnteredBy, -- COMMENT 'Audit: Contributors.Id of this Contributors entry'
    CONSTRAINT [FK_Contributors_Contributors_AuditEnteredBy] FOREIGN KEY ([AuditEnteredBy]) REFERENCES [Contributors] ([Id]),
    [AuditUpdated] datetime2, -- COMMENT 'Audit: date of last update for Contributors Entity'
    [AuditUpdatedBy] bigint NULL INDEX IX_Contributors_AuditUpdatedBy, -- COMMENT 'Audit: Contributors.Id of last updater of this Contributors entry'
    CONSTRAINT [FK_Contributors_Contributors_AuditUpdatedBy] FOREIGN KEY ([AuditUpdatedBy]) REFERENCES [Contributors] ([Id]),
    CONSTRAINT [PX_Contributors] PRIMARY KEY ([Id])
  );

GO

IF NOT EXISTS (SELECT[name] FROM sys.tables WHERE[name] = 'Papers')
  CREATE TABLE Papers(
    [Id] bigint NOT NULL IDENTITY(1,1), -- COMMENT 'Primary Key for Papers Entity'
    [Name] varchar(70), -- COMMENT 'Name for Papers Entity'
    [Description] varchar(255), -- COMMENT 'Description or Tool Tip for Papers Entity'
    [ContributorId] bigint NULL INDEX IX_Papers_ContributorId, -- COMMENT 'Foreign Key reference to Contributors Entity'
    CONSTRAINT [FK_Papers_Contributors_ContributorId] FOREIGN KEY ([ContributorId]) REFERENCES [Contributors] ([Id]),
    [Comments] varchar(max), -- COMMENT 'Comments for Papers entry'
    [AuditEntered] datetime2, -- COMMENT 'Audit: date of entry for Papers Entity'
    [AuditEnteredBy] bigint NULL INDEX IX_Papers_AuditEnteredBy, -- COMMENT 'Audit: Contributors.Id of this Papers entry'
    CONSTRAINT [FK_Papers_Contributors_AuditEnteredBy] FOREIGN KEY ([AuditEnteredBy]) REFERENCES [Contributors] ([Id]),
    [AuditUpdated] datetime2, -- COMMENT 'Audit: date of last update for Papers Entity'
    [AuditUpdatedBy] bigint NULL INDEX IX_Papers_AuditUpdatedBy, -- COMMENT 'Audit: Contributors.Id of last updater of this Papers entry'
    CONSTRAINT [FK_Papers_Contributors_AuditUpdatedBy] FOREIGN KEY ([AuditUpdatedBy]) REFERENCES [Contributors] ([Id]),
    CONSTRAINT [PX_Papers] PRIMARY KEY ([Id])
  );

GO

IF NOT EXISTS (SELECT[name] FROM sys.tables WHERE[name] = 'PaperVersions')
  CREATE TABLE PaperVersions(
    [Id] bigint NOT NULL IDENTITY(1,1), -- COMMENT 'Primary Key for PaperVersions Entity'
    [Name] varchar(70), -- COMMENT 'Name for PaperVersions Entity'
    [Description] varchar(255), -- COMMENT 'Description or Tool Tip for PaperVersions Entity'
    [Content] varchar(max), -- COMMENT 'Content Field'
    [ContributorId] bigint NULL INDEX IX_PaperVersions_ContributorId, -- COMMENT 'Contributor Id Field'
    CONSTRAINT [FK_PaperVersions_Contributors_ContributorId] FOREIGN KEY ([ContributorId]) REFERENCES [Contributors] ([Id]),
    [PaperId] bigint NULL INDEX IX_PaperVersions_PaperId, -- COMMENT 'Paper Id Field'
    CONSTRAINT [FK_PaperVersions_Papers_PaperId] FOREIGN KEY ([PaperId]) REFERENCES [Papers] ([Id]),
    [Comments] varchar(max), -- COMMENT 'Comments for PaperVersions entry'
    [AuditEntered] datetime2, -- COMMENT 'Audit: date of entry for PaperVersions Entity'
    [AuditEnteredBy] bigint NULL INDEX IX_PaperVersions_AuditEnteredBy, -- COMMENT 'Audit: Contributors.Id of this PaperVersions entry'
    CONSTRAINT [FK_PaperVersions_Contributors_AuditEnteredBy] FOREIGN KEY ([AuditEnteredBy]) REFERENCES [Contributors] ([Id]),
    [AuditUpdated] datetime2, -- COMMENT 'Audit: date of last update for PaperVersions Entity'
    [AuditUpdatedBy] bigint NULL INDEX IX_PaperVersions_AuditUpdatedBy, -- COMMENT 'Audit: Contributors.Id of last updater of this PaperVersions entry'
    CONSTRAINT [FK_PaperVersions_Contributors_AuditUpdatedBy] FOREIGN KEY ([AuditUpdatedBy]) REFERENCES [Contributors] ([Id]),
    CONSTRAINT [PX_PaperVersions] PRIMARY KEY ([Id])
  );

GO

IF NOT EXISTS (SELECT[name] FROM sys.tables WHERE[name] = 'Reviews')
  CREATE TABLE Reviews(
    [Id] bigint NOT NULL IDENTITY(1,1), -- COMMENT 'Primary Key for Reviews Entity'
    [Name] varchar(70), -- COMMENT 'Name for Reviews Entity'
    [Description] varchar(255), -- COMMENT 'Description or Tool Tip for Reviews Entity'
    [ContributorId] bigint NULL INDEX IX_Reviews_ContributorId, -- COMMENT 'Foreign Key reference to Contributors Entity'
    CONSTRAINT [FK_Reviews_Contributors_ContributorId] FOREIGN KEY ([ContributorId]) REFERENCES [Contributors] ([Id]),
    [PaperId] bigint NULL INDEX IX_Reviews_PaperId, -- COMMENT 'Foreign Key reference to Papers Entity'
    CONSTRAINT [FK_Reviews_Papers_PaperId] FOREIGN KEY ([PaperId]) REFERENCES [Papers] ([Id]),
    [PaperVersionId] bigint NULL INDEX IX_Reviews_PaperVersionId, -- COMMENT 'Foreign Key reference to Paper Versions Entity'
    CONSTRAINT [FK_Reviews_PaperVersions_PaperVersionId] FOREIGN KEY ([PaperVersionId]) REFERENCES [PaperVersions] ([Id]),
    [Comments] varchar(max), -- COMMENT 'Comments for Reviews entry'
    [AuditEntered] datetime2, -- COMMENT 'Audit: date of entry for Reviews Entity'
    [AuditEnteredBy] bigint NULL INDEX IX_Reviews_AuditEnteredBy, -- COMMENT 'Audit: Contributors.Id of this Reviews entry'
    CONSTRAINT [FK_Reviews_Contributors_AuditEnteredBy] FOREIGN KEY ([AuditEnteredBy]) REFERENCES [Contributors] ([Id]),
    [AuditUpdated] datetime2, -- COMMENT 'Audit: date of last update for Reviews Entity'
    [AuditUpdatedBy] bigint NULL INDEX IX_Reviews_AuditUpdatedBy, -- COMMENT 'Audit: Contributors.Id of last updater of this Reviews entry'
    CONSTRAINT [FK_Reviews_Contributors_AuditUpdatedBy] FOREIGN KEY ([AuditUpdatedBy]) REFERENCES [Contributors] ([Id]),
    CONSTRAINT [PX_Reviews] PRIMARY KEY ([Id])
  );

GO

IF NOT EXISTS (SELECT[name] FROM sys.tables WHERE[name] = 'TestFields')
  CREATE TABLE TestFields(
    [Id] bigint NOT NULL IDENTITY(1,1), -- COMMENT 'Primary Key for TestFields Entity'
    [Name] varchar(70), -- COMMENT 'Name for TestFields Entity'
    [Description] varchar(255), -- COMMENT 'Description or Tool Tip for TestFields Entity'
    [MyBoolean] bit, -- COMMENT 'My Boolean Field'
    [MyCreditCard] varchar(25), -- COMMENT 'My Credit Card Field'
    [MyCurrency] decimal(10,2), -- COMMENT 'My Currency Field'
    [MyDate] date, -- COMMENT 'My Date Field'
    [MyDateTime] datetime2, -- COMMENT 'My Date Time Field'
    [MyDouble] float, -- COMMENT 'My Double Field'
    [MyEmail] varchar(127), -- COMMENT 'My Email Field'
    [MyFloat] float, -- COMMENT 'My Float Field'
    [MyImageUrl] varchar(255), -- COMMENT 'My Image Url Field'
    [MyInteger] int, -- COMMENT 'My Integer Field'
    [MyLong] bigint, -- COMMENT 'My Long Field'
    [MyPhone] varchar(25), -- COMMENT 'My Phone Field'
    [MyPostalCode] varchar(15), -- COMMENT 'My Postal Code Field'
    [MyString] varchar(35), -- COMMENT 'My String Field'
    [MyTextArea] varchar(255), -- COMMENT 'My Text Area Field'
    [MyTicks] bigint, -- COMMENT 'My Ticks Field'
    [MyTime] time, -- COMMENT 'My Time Field'
    [MyUrl] varchar(255), -- COMMENT 'My Url Field'
    [Comments] varchar(max), -- COMMENT 'Comments for TestFields entry'
    [AuditEntered] datetime2, -- COMMENT 'Audit: date of entry for TestFields Entity'
    [AuditEnteredBy] bigint NULL INDEX IX_TestFields_AuditEnteredBy, -- COMMENT 'Audit: Contributors.Id of this TestFields entry'
    CONSTRAINT [FK_TestFields_Contributors_AuditEnteredBy] FOREIGN KEY ([AuditEnteredBy]) REFERENCES [Contributors] ([Id]),
    [AuditUpdated] datetime2, -- COMMENT 'Audit: date of last update for TestFields Entity'
    [AuditUpdatedBy] bigint NULL INDEX IX_TestFields_AuditUpdatedBy, -- COMMENT 'Audit: Contributors.Id of last updater of this TestFields entry'
    CONSTRAINT [FK_TestFields_Contributors_AuditUpdatedBy] FOREIGN KEY ([AuditUpdatedBy]) REFERENCES [Contributors] ([Id]),
    CONSTRAINT [PX_TestFields] PRIMARY KEY ([Id])
  );

GO



CREATE TABLE [dbo].[AspNetRoles] (
    [Id]               NVARCHAR (450) NOT NULL,
    [ConcurrencyStamp] NVARCHAR (MAX) NULL,
    [Name]             NVARCHAR (256) NULL,
    [NormalizedName]   NVARCHAR (256) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[AspNetRoles]([NormalizedName] ASC);

GO

CREATE TABLE [dbo].[AspNetUsers] (
    [Id]                   NVARCHAR (450)     NOT NULL,
    [AccessFailedCount]    INT                NOT NULL,
    [ConcurrencyStamp]     NVARCHAR (MAX)     NULL,
    [Email]                NVARCHAR (256)     NULL,
    [EmailConfirmed]       BIT                NOT NULL,
    [LockoutEnabled]       BIT                NOT NULL,
    [LockoutEnd]           DATETIMEOFFSET (7) NULL,
    [NormalizedEmail]      NVARCHAR (256)     NULL,
    [NormalizedUserName]   NVARCHAR (256)     NULL,
    [PasswordHash]         NVARCHAR (MAX)     NULL,
    [PhoneNumber]          NVARCHAR (MAX)     NULL,
    [PhoneNumberConfirmed] BIT                NOT NULL,
    [SecurityStamp]        NVARCHAR (MAX)     NULL,
    [TwoFactorEnabled]     BIT                NOT NULL,
    [UserName]             NVARCHAR (256)     NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [EmailIndex]
    ON [dbo].[AspNetUsers]([NormalizedEmail] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[AspNetUsers]([NormalizedUserName] ASC);
    
GO

CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] NVARCHAR (450) NOT NULL,
    [RoleId] NVARCHAR (450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId]
    ON [dbo].[AspNetUserRoles]([RoleId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_UserId]
    ON [dbo].[AspNetUserRoles]([UserId] ASC);

GO


CREATE TABLE [dbo].[AspNetUserTokens] (
    [UserId]        NVARCHAR (450) NOT NULL,
    [LoginProvider] NVARCHAR (450) NOT NULL,
    [Name]          NVARCHAR (450) NOT NULL,
    [Value]         NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED ([UserId] ASC, [LoginProvider] ASC, [Name] ASC)
);

GO


CREATE TABLE [dbo].[AspNetRoleClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    [RoleId]     NVARCHAR (450) NOT NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId]
    ON [dbo].[AspNetRoleClaims]([RoleId] ASC);

GO

CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    [UserId]     NVARCHAR (450) NOT NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId]
    ON [dbo].[AspNetUserClaims]([UserId] ASC);

GO

CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider]       NVARCHAR (450) NOT NULL,
    [ProviderKey]         NVARCHAR (450) NOT NULL,
    [ProviderDisplayName] NVARCHAR (MAX) NULL,
    [UserId]              NVARCHAR (450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO

CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId]
    ON [dbo].[AspNetUserLogins]([UserId] ASC);

GO
