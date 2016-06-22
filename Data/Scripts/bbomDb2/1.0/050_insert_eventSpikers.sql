USE bbomDb2
CREATE TABLE [dbo].[EventSpikers] (
    [EventId]  INT            NOT NULL,
    [SpikerId] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_EventSpikers] PRIMARY KEY CLUSTERED ([EventId] ASC, [SpikerId] ASC),
    CONSTRAINT [FK_EventSpikers_Events] FOREIGN KEY ([EventId]) REFERENCES [dbo].[Events] ([Id]),
    CONSTRAINT [FK_EventSpikers_AspNetUsers] FOREIGN KEY ([SpikerId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);