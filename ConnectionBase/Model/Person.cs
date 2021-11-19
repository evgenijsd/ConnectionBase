using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ConnectionBase.Model
{
    public partial class Person : INotifyPropertyChanged
    {
        private int personId;
        private string personName;
        private string position;
        private int? depart;

        public int PersonId { get => personId; set { personId = value; OnPropertyChanged("PersonId"); } }
        public string PersonName { get => personName; set { personName = value; OnPropertyChanged("PersonName"); } }
        public string Position { get => position; set { position = value; OnPropertyChanged("Position"); } }
        public int? Depart { get => depart; set { depart = value; OnPropertyChanged("Depart"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
