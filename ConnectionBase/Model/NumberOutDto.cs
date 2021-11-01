using System.Collections.ObjectModel;

namespace ConnectionBase.Model
{
    public partial class NumberOutDto
    {
        public int NumberId { get; set; }
        public string Number_Out { get; set; }
        public int? PairAts { get; set; }
        public int? Operator { get; set; }

        public ObservableCollection<OperatorDto> Operators { get; set; }
    }
}
