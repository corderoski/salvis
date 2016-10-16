using System.Collections.Generic;

namespace  Salvis.App.Web.Models
{
    public class GraphicData
    {
        public Line[] Line { get; set; }
        public IEnumerable<string> YaxisSub { get; set; }
        public IEnumerable<string> XaxisSub { get; set; }

        public IEnumerable<string[]> OperationDetails { get; set; }
        public string OperationExpectedNextDate { get; set; }
        public string OperationExpectedExpAmount { get; set; }
        public string OperationExpectedRealAmount { get; set; }
    }
    public class Line
    {
        // ReSharper disable InconsistentNaming
        // Dont change the case of those propierties

        public IEnumerable<string[]> data { get; set; }
        public string label { get; set; }

        // ReSharper restore InconsistentNaming
    }
}