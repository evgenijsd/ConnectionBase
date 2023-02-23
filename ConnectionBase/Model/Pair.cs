namespace ConnectionBase.Model
{
    public partial class Pair
    {
        public int PairId { get; set; }
        public int? Cross { get; set; }
        public int PairNum { get; set; }
        public int? PairIn { get; set; }
        public bool? BreakIn { get; set; }
        public bool? PairAb { get; set; }

        public string DevCross { get; set; }
        public string DevCrossIn { get; set; }
        public int? PairNumIn { get; set; }
    }
}
