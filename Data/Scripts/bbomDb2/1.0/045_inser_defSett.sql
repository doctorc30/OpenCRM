USE bbomDb2
INSERT INTO bbomDb2.dbo.DefaultSettingsValues (SettingId, Value, Description) VALUES (2, 'app-header-bg.jpg', 'BG');
INSERT INTO bbomDb2.dbo.TemplateSettings (TemplateId, SettingId) VALUES (3, 2);