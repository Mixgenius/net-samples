using System;

namespace Model
{
    public class AvailableGateway
    {
        public int ProcessorId { get; set; }
        public string ProcessorName { get; set; }
        public string LabelName { get; set; }
        public byte[] Image { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
