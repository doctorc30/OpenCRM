namespace bbom.Admin.Core.ViewModels.Common
{
    public class TableButton
    {
        public bool inRow;

        public string name { get; set; }

        public string link { get; set; }

        public bool needId { get; set; }
    }

    public class TableButtonInRow : TableButton
    {
        public string _class { get; set; }

        public bool ajax { get; set; }
    }

    public class TableButtonGroup
    {
        public int columnIndex { get; set; }

        public object style { get; set; }

        public TableButtonInRow[] buttons { get; set; }
    }

    public class TableModel
    {
        public string name { get; set; }

        public string link { get; set; }

        public bool select { get; set; }

        public string[] columns { get; set; }

        public TableButton[] buttons { get; set; }

        public TableButtonGroup[] buttonsInRow { get; set; }
    }
}