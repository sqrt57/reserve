-- MarsUsers

CREATE TABLE [dbo].[MarsUsers] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	CONSTRAINT [PK_MarsUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);

-- MarsRoles

CREATE TABLE [dbo].[MarsRoles] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	CONSTRAINT [PK_MarsRole] PRIMARY KEY CLUSTERED ([Id] ASC)
);

-- MarsUserRoles

CREATE TABLE [dbo].[MarsUserRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	CONSTRAINT [PK_MarsUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC)
);

ALTER TABLE [dbo].[MarsUserRoles] WITH CHECK ADD CONSTRAINT [FK_MarsUserRoles_MarsRoles_RoleId]
FOREIGN KEY([RoleId]) REFERENCES [dbo].[MarsRoles] ([Id])
ON DELETE CASCADE;
ALTER TABLE [dbo].[MarsUserRoles] CHECK CONSTRAINT [FK_MarsUserRoles_MarsRoles_RoleId];

ALTER TABLE [dbo].[MarsUserRoles] WITH CHECK ADD CONSTRAINT [FK_MarsUserRoles_MarsUsers_UserId]
FOREIGN KEY([USerId]) REFERENCES [dbo].[MarsUsers] ([Id])
ON DELETE CASCADE;
ALTER TABLE [dbo].[MarsUserRoles] CHECK CONSTRAINT [FK_MarsUserRoles_MarsUsers_UserId];

-- MarsUserClaims

CREATE TABLE [dbo].[MarsUserClaims] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	CONSTRAINT [PK_MarsUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC)
);

ALTER TABLE [dbo].[MarsUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_MarsUserClaims_MarsUsers_UserId]
FOREIGN KEY([UserId]) REFERENCES [dbo].[MarsUsers] ([Id])
ON DELETE CASCADE;
ALTER TABLE [dbo].[MarsUserClaims] CHECK CONSTRAINT [FK_MarsUserClaims_MarsUsers_UserId];

-- MarsRoleClaims

CREATE TABLE [dbo].[MarsRoleClaims] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	CONSTRAINT [PK_MarsRoleClaims] PRIMARY KEY CLUSTERED ([Id] ASC)
);

ALTER TABLE [dbo].[MarsRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_MarsRoleClaims_MarsRoles_RoleId]
FOREIGN KEY([RoleId]) REFERENCES [dbo].[MarsRoles] ([Id])
ON DELETE CASCADE;
ALTER TABLE [dbo].[MarsRoleClaims] CHECK CONSTRAINT [FK_MarsRoleClaims_MarsRoles_RoleId];

-- MarsUserLogins

CREATE TABLE [dbo].[MarsUserLogins] (
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [int] NOT NULL,
	CONSTRAINT [PK_MarsUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC)
);

ALTER TABLE [dbo].[MarsUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_MarsUserLogins_MarsUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[MarsUsers] ([Id])
ON DELETE CASCADE;
ALTER TABLE [dbo].[MarsUserLogins] CHECK CONSTRAINT [FK_MarsUserLogins_MarsUsers_UserId];

-- MarsUserTokens

CREATE TABLE [dbo].[MarsUserTokens] (
	[UserId] [int] NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
	CONSTRAINT [PK_MarsUserTokens] PRIMARY KEY CLUSTERED ([UserId] ASC, [LoginProvider] ASC, [Name] ASC)
);

ALTER TABLE [dbo].[MarsUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_MarsUserTokens_MarsUsers_UserId]
FOREIGN KEY([UserId]) REFERENCES [dbo].[MarsUsers] ([Id])
ON DELETE CASCADE;
ALTER TABLE [dbo].[MarsUserTokens] CHECK CONSTRAINT [FK_MarsUserTokens_MarsUsers_UserId];
