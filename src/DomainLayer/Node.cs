using DomainLayer.BaseModels;
using System.Collections.Generic;

namespace DomainLayer
{
    public class Node:BaseModel
    {
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public Node Parent { get; set; }
        public ICollection<NodeValue> Values { get; set; }
        public ICollection<Node> Children { get; set; }

    }
}
