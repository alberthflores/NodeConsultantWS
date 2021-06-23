using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.BaseModels;

namespace DomainLayer
{
    public class NodeValue: BaseModel
    {
        public int NodeId { get; set; }
        public Node Node { get; set; }

    }
}
