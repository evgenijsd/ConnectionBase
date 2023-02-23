using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ConnectionBase.Model
{
    public class GenerationList : INotifyPropertyChanged
    {
        private int pairBegin;
        private int pairEnd;
        private int? crossBegin;
        private int? deviceBegin;
        private int pairNumBegin;
        private int? buildingBegin;
        private int? roomBegin;
        private int? crossEnd;
        private int? deviceEnd;
        private int pairNumEnd;
        private int? buildingEnd;
        private int? roomEnd;

        public int PairBegin { get => pairBegin; set { pairBegin = value; OnPropertyChanged("PairBegin"); } }
        public int PairEnd { get => pairEnd; set { pairEnd = value; OnPropertyChanged("PairEnd"); } }
        public int? CrossBegin { get => crossBegin; set { crossBegin = value; OnPropertyChanged("CrossBegin"); } }
        public int? DeviceBegin { get => deviceBegin; set { deviceBegin = value; OnPropertyChanged("DeviceBegin"); } }
        public int PairNumBegin { get => pairNumBegin; set { pairNumBegin = value; OnPropertyChanged("PairNumBegin"); } }
        public int? BuildingBegin { get => buildingBegin; set { buildingBegin = value; OnPropertyChanged("BuildingBegin"); } }
        public int? RoomBegin { get => roomBegin; set { roomBegin = value; OnPropertyChanged("RoomBegin"); } }
        public int? CrossEnd { get => crossEnd; set { crossEnd = value; OnPropertyChanged("CrossEnd"); } }
        public int? DeviceEnd { get => deviceEnd; set { deviceEnd = value; OnPropertyChanged("DeviceEnd"); } }
        public int PairNumEnd { get => pairNumEnd; set { pairNumEnd = value; OnPropertyChanged("PairNumEnd"); } }
        public int? BuildingEnd { get => buildingEnd; set { buildingEnd = value; OnPropertyChanged("BuildingEnd"); } }
        public int? RoomEnd { get => roomEnd; set { roomEnd = value; OnPropertyChanged("RoomEnd"); } }
        public string DevCrossBegin { get; set; }
        public string DevCrossEnd { get; set; }

        public ObservableCollection<Building> Buildings { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
