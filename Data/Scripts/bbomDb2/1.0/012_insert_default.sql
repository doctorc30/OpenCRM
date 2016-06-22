update AspNetUsers set ActiveTemplateId = 3 where id = 'f4bef59a-0ba8-4c22-8094-a0221ad7d7df'

SET IDENTITY_INSERT [dbo].[Settings] ON
INSERT INTO [dbo].[Settings] ([Id], [Name]) VALUES (1, N'VideoLink')
SET IDENTITY_INSERT [dbo].[Settings] OFF


INSERT INTO [dbo].[TemplateSettings] ([TemplateId], [SettingId]) VALUES (3, 1)

SET IDENTITY_INSERT [dbo].[DefaultSettingsValues] ON
INSERT INTO [dbo].[DefaultSettingsValues] ([SettingId], [Value], [Id], [Description]) VALUES (1, N'https://www.youtube.com/embed/tGU3Pu8Cado', 1, N'Продающее видео')
INSERT INTO [dbo].[DefaultSettingsValues] ([SettingId], [Value], [Id], [Description]) VALUES (1, N'https://www.youtube.com/embed/tGU3Pu8Cado', 2, N'Представляющее видео')
SET IDENTITY_INSERT [dbo].[DefaultSettingsValues] OFF

