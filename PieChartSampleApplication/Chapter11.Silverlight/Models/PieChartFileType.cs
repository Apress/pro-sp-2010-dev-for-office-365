using System;

namespace Chapter11.Silverlight.Models
{
    public class PieChartFileType
    {
        public PieChartFileType(string typeName, int count)
        {
            this.TypeName = typeName;
            this.Count = count;
        }

        public string TypeName { get; set; }
        public int Count { get; set; }
    }
}
