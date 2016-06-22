using System.Collections.Generic;
using bbom.Admin.Core.ViewModels;
using bbom.Data;
using bbom.Data.ContentModel;
using bbom.Data.Repository.Interfaces;

namespace bbom.Admin.Core.Menu
{
    public class EventsMenuGenerator : MenuGenerator
    {
        public override ICollection<MenuJson> GetMenu()
        {
            ValidateSetting = "EventsMenus";
            var menusJson = new List<MenuJson>();
            if (!Validate())
            {
                return menusJson;
            }
            var et = DataFasade.GetRepository<EventType>().GetAll();
            int i = 9;
            foreach (var type in et)
            {
                menusJson.Add(new MenuJson
                {
                    name = GetName(type.Name),
                    discription = "glyph stroked video",
                    icon = "#stroked-video",
                    action = "Events",
                    controller = "Home",
                    exParam = type.Id.ToString(),
                    order = i++,
                    destinationType = typeof(IRepository<EventType>),
                    accessType = typeof(IRepository<AccessToEventType>)
                });
            }
            return menusJson;
        }

        private string GetName(string baseName)
        {
            if (baseName == "Вебинар")
            {
                return "Мероприятия";
            }
            if (baseName == "Стартовый тренинг")
            {
                return "Быстрый старт";
            }
            return baseName + "и";
        }
    }
}