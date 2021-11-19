using System.Collections.ObjectModel;

namespace ConnectionBase.Model
{
    public partial class NumberOut
    {
        public int NumberId { get; set; }
        public string Number_Out { get; set; }
        public int? PairAts { get; set; }
        public int? Operator { get; set; }

        public ObservableCollection<Operator> Operators { get; set; }
    }
}
