CREATE TABLE [dbo].[Settings] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[DefaultSettingsValues] (
    [SettingId] INT           NOT NULL,
    [Value]     VARCHAR (MAX) NOT NULL,
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_DefaultSettingsValues] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DefaultSettingsValues_Settings] FOREIGN KEY ([SettingId]) REFERENCES [dbo].[Settings] ([Id])
);

CREATE TABLE [dbo].[TemplateSettings] (
    [TemplateId] INT NOT NULL,
    [SettingId]  INT NOT NULL,
    CONSTRAINT [PK_TemplateSettings] PRIMARY KEY CLUSTERED ([TemplateId] ASC, [SettingId] ASC),
    CONSTRAINT [FK_TemplateSettings_Templates] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[Templates] ([Id]),
    CONSTRAINT [FK_TemplateSettings_Settings] FOREIGN KEY ([SettingId]) REFERENCES [dbo].[Settings] ([Id])
);

