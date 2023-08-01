-- Tariff table

IF OBJECT_ID(N'dbo.Tariffs', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[Tariffs] (
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[IsActive] [bit] NOT NULL,
		[Name] [nvarchar](256) NOT NULL,
		[Order] [int] NOT NULL,
		[CreatedDateTime] [datetime2] NOT NULL,
		[CreatedByUserId] [int] NOT NULL,
		[FirstHour] [decimal](18,2) NOT NULL,
		[SecondHour] [decimal](18,2) NULL,
		[ThirdHour] [decimal](18,2) NULL,
		[FourthHour] [decimal](18,2) NULL,
		[MaxTimeBill] [decimal](18,2) NULL,
		CONSTRAINT [PK_Tariffs] PRIMARY KEY CLUSTERED ([Id] ASC)
	);
END;

IF OBJECT_ID(N'dbo.FK_Tariffs_MarsUsers_CreatedByUserId', N'F') IS NULL
BEGIN
	ALTER TABLE [dbo].[Tariffs] WITH CHECK ADD CONSTRAINT [FK_Tariffs_MarsUsers_CreatedByUserId]
		FOREIGN KEY([CreatedByUserId]) REFERENCES [dbo].[MarsUsers] ([Id]);
	ALTER TABLE [dbo].[Tariffs] CHECK CONSTRAINT [FK_Tariffs_MarsUsers_CreatedByUserId];
END;

-- Visitors table

IF OBJECT_ID(N'dbo.Visitors', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[Visitors] (
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[IsActive] [bit] NOT NULL,
		[BadgeNumber] [nvarchar](32) NULL,
		[Name] [nvarchar](256) NULL,
		[TariffId] [int] NOT NULL,
		[OpenDateTime] [datetime2] NOT NULL,
		[OpenedByUserId] [int] NOT NULL,
		[CloseDateTime] [datetime2] NULL,
		[ClosedByUserId] [int] NULL,
		[PaymentAcceptedByUserId] [int] NULL,
		[Billed] [decimal](18,2) NULL,
		[Paid] [decimal](18,2) NULL,
		CONSTRAINT [PK_Visitors] PRIMARY KEY CLUSTERED ([Id] ASC)
	);
END;

IF OBJECT_ID(N'dbo.FK_Visitors_Tariffs_TariffId', N'F') IS NULL
BEGIN
	ALTER TABLE [dbo].[Visitors] WITH CHECK ADD CONSTRAINT [FK_Visitors_Tariffs_TariffId]
		FOREIGN KEY([TariffId]) REFERENCES [dbo].[Tariffs] ([Id]);
	ALTER TABLE [dbo].[Visitors] CHECK CONSTRAINT [FK_Visitors_Tariffs_TariffId];
END;

IF OBJECT_ID(N'dbo.FK_Visitors_MarsUsers_OpenedByUserId', N'F') IS NULL
BEGIN
	ALTER TABLE [dbo].[Visitors] WITH CHECK ADD CONSTRAINT [FK_Visitors_MarsUsers_OpenedByUserId]
		FOREIGN KEY([OpenedByUserId]) REFERENCES [dbo].[MarsUsers] ([Id]);
	ALTER TABLE [dbo].[Visitors] CHECK CONSTRAINT [FK_Visitors_MarsUsers_OpenedByUserId];
END;

IF OBJECT_ID(N'dbo.FK_Visitors_MarsUsers_ClosedByUserId', N'F') IS NULL
BEGIN
	ALTER TABLE [dbo].[Visitors] WITH CHECK ADD CONSTRAINT [FK_Visitors_MarsUsers_ClosedByUserId]
		FOREIGN KEY([ClosedByUserId]) REFERENCES [dbo].[MarsUsers] ([Id]);
	ALTER TABLE [dbo].[Visitors] CHECK CONSTRAINT [FK_Visitors_MarsUsers_ClosedByUserId];
END;
