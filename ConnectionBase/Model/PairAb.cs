namespace ConnectionBase.Model
{
    public partial class PairAb
    {
        public int AbId { get; set; }
        public int Pair { get; set; }
        public int? PairIn { get; set; }
        public bool? BreakIn { get; set; }
    }
}
