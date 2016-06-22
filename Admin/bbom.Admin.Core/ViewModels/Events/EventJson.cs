using System;
using System.Collections.Generic;

namespace bbom.Admin.Core.ViewModels.Events
{
    public class EventJsonComparer : IComparer<EventJson>
    {
        public int Compare(EventJson x, EventJson y)
        {
            if (DateTime.Parse(x.start)  < DateTime.Parse(y.start))
                return -1;
            if (DateTime.Parse(x.start) > DateTime.Parse(y.start))
                return 1;
            return 0;
        }
    }
    public class EventJson
    {
        public string name { get; set; } 
        public string title { get; set; } 
        public string url { get; set; } 
        public string urlVideo { get; set; } 
        public string start { get; set; } 
        public string end { get; set; } 
        public string typeId { get; set; } 
        public string spikerId { get; set; } 
        public string userName { get; set; } 
        public string icon { get; set; } 
        public string spiker { get; set; } 
        public bool isShowDate { get; set; } 
        public bool isRun{ get; set; } 
        public string description { get; set; } 
        public string status { get; set; } 

    }

    public class EventLigthJson : EventJson
    {

    }

    public class EventListJson : EventJson
    {
        public string buttonColor { get; set; }
    }
}