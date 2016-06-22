/*CREATE TABLE [dbo].[UsersTemplateSettings] (
    [UserId]     NVARCHAR (128) NOT NULL,
    [TemplateId] INT            NOT NULL,
    [Id]         INT            IDENTITY (0, 1) NOT NULL,
    [SettingId] INT NOT NULL, 
    [Value] VARCHAR(MAX) NOT NULL, 
    CONSTRAINT [PK_UsersTemplateSettings] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UsersTemplateSettings_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_UsersTemplateSettings_Templates] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[Templates] ([Id]),
	CONSTRAINT [FK_UsersTemplateSettings_Settings] FOREIGN KEY ([SettingId]) REFERENCES [dbo].[Settings] ([Id])
);*/
