using System;

namespace DtoLayer
{
    public class NodeValue2Dto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public decimal AccumulatedValueOfChildren { get; set; }
        public DateTime ToTheDatetime { get; set; }
        public decimal Total => (Value + AccumulatedValueOfChildren);
    }
}
