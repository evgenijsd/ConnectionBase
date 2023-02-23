namespace ConnectionBase.Model
{
    public partial class Device
    {
        public int DeviceId { get; set; }
        public int Model { get; set; }
        public int? Room { get; set; }
        public int? Pair { get; set; }
        public string InvNum { get; set; }

        public int Building { get; set; }
    }
}
