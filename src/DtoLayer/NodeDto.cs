using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer
{
    public class NodeDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public int Value { get; set; }
        public decimal TeenPercent { get; set; }
        public decimal AccumulatedPercentageOfChildren { get; set; }
    }
}
