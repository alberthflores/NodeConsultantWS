using DomainLayer.BaseModels;

namespace DomainLayer
{
    public class NodeValue: BaseModel
    {
        public int NodeId { get; set; }
        public Node Node { get; set; }
        public int Value { get; set; }

    }
}
