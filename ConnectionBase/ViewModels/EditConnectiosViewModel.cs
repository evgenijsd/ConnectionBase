using ConnectionBase.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionBase.ViewModels
{
    public class EditConnectiosViewModel : INotifyPropertyChanged
    {
        public EditConnectiosViewModel()
        {
            People = GetEntity.GetList<PersonDto>("api/Person/all");
            Operators = GetEntity.GetList<OperatorDto>("api/Operator/all");
            Crosses = GetEntity.GetList<CrossDto>("api/Cross/all");
            Buildings = GetEntity.GetList<BuildingDto>("api/Building/all");
            Rooms = GetEntity.GetList<RoomDto>("api/Room/all");
            Devices = GetEntity.GetList<DeviceDto>("api/Device/all");
            Models = GetEntity.GetList<DeviceModelDto>("api/DeviceModel/all");
            Departs = GetEntity.GetList<DepartDto>("api/Depart/all");

            
            Pairs = GetEntity.GetList<PairDto>($"api/Pair/cross/{Crosses[2].CrossId}");
            PairOuts = GetEntity.GetList<PairDto>($"api/Pair/out/{Pairs[0].PairId}");
            foreach (PairDto d in Pairs)
            {
                d.DevCross = GetBox(d.Cross, Devices.FirstOrDefault(x => x.Pair == d.PairId)?.DeviceId).ToString();
                var pairin = GetEntity.GetById<PairDto>($"api/Pair/{d.PairIn}");
                if (pairin != null)
                {
                    d.PairNumIn = pairin.PairNum;
                    d.DevCrossIn = GetBox(pairin.Cross, Devices.FirstOrDefault(x => x.Pair == d.PairIn)?.DeviceId).ToString();
                }
            }
            foreach (PairDto d in PairOuts)
            {
                d.DevCross = GetBox(d.Cross, Devices.FirstOrDefault(x => x.Pair == d.PairId)?.DeviceId).ToString();
                var pairin = Pairs[0];
                if (pairin != null)
                {
                    d.PairNumIn = pairin.PairNum;
                    d.DevCrossIn = GetBox(pairin.Cross, Devices.FirstOrDefault(x => x.Pair == d.PairIn)?.DeviceId).ToString();
                }
            }
            var cross = Crosses.FirstOrDefault(x => x.CrossId == Pairs[0].Cross);
            var room = Rooms.FirstOrDefault(x => x.RoomId == cross.Room);
            var building = Buildings.FirstOrDefault(x => x.BuildingId == room.Building);
            DevCrossCurrent = Pairs[0].DevCross;
            PairNumCurrent = Pairs[0].PairNum;
            BuildingCurrent = building.BuildingName;
            RoomCurrent = room.RoomName;
            var pair = GetEntity.GetById<PairDto>($"api/Pair/{Pairs[0].PairIn}");
            cross = Crosses.FirstOrDefault(x => x.CrossId == pair.Cross);
            room = Rooms.FirstOrDefault(x => x.RoomId == cross?.Room);
            building = Buildings.FirstOrDefault(x => x.BuildingId == room?.Building);
            DevCrossIn = Pairs[0].DevCrossIn;
            PairNumIn = Pairs[0].PairNumIn;
            BuildingIn = building?.BuildingName;
            RoomIn = room?.RoomName;
            cross = Crosses.FirstOrDefault(x => x.CrossId == Pairs[0].Cross);
            room = Rooms.FirstOrDefault(x => x.RoomId == cross.Room);
            building = Buildings.FirstOrDefault(x => x.BuildingId == room.Building);
            DevCrossChoise = Pairs[0].DevCross;
            PairNumChoise = Pairs[0].PairNum;
            BuildingChoise = building.BuildingName;
            RoomChoise = room.RoomName;
        }

        public ObservableCollection<PairDto> Pairs { get; set; }
        public ObservableCollection<PairDto> PairOuts { get; set; }
        public ObservableCollection<DeviceModelDto> Models { get; set; }
        public ObservableCollection<DepartDto> Departs { get; set; }
        public ObservableCollection<CrossDto> Crosses { get; set; }
        public ObservableCollection<BuildingDto> Buildings { get; set; }
        public ObservableCollection<RoomDto> Rooms { get; set; }
        public ObservableCollection<DeviceDto> Devices { get; set; }
        public ObservableCollection<NumberInDto> GenNumberIn { get; set; }
        public ObservableCollection<NumberOutDto> GenNumberOut { get; set; }
        public ObservableCollection<GenerationList> GenList { get; set; }
        public ObservableCollection<GenerationChains> GenChain { get; set; }
        public ObservableCollection<PersonDto> People { get; set; }
        public ObservableCollection<OperatorDto> Operators { get; set; }
        public ObservableCollection<DevicePersonDto> DevicePeople { get; set; }

        public string GetBox(int? cross, int? device)
        {
            string result = string.Empty;
            if (device != null) result = Models.FirstOrDefault(x => x.ModelId == Devices.FirstOrDefault(x => x.DeviceId == device).Model).ModelName;
            if (cross != null) result += Crosses.FirstOrDefault(x => x.CrossId == cross).CrossName;
            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private string devCrossCurrent { get; set; }
        private int pairNumCurrent { get; set; }
        private string buildingCurrent { get; set; }
        private string roomCurrent { get; set; }
        
        public string DevCrossCurrent { get => devCrossCurrent; set { devCrossCurrent = value; OnPropertyChanged("DevCrossCurrent"); } }
        public int PairNumCurrent { get => pairNumCurrent; set { pairNumCurrent = value; OnPropertyChanged("PairNumCurrent"); } }
        public string BuildingCurrent { get => buildingCurrent; set { buildingCurrent = value; OnPropertyChanged("BuildingCurrent"); } }
        public string RoomCurrent { get => roomCurrent; set { roomCurrent = value; OnPropertyChanged("RoomCurrent"); } }

        private string devCrossIn { get; set; }
        private int pairNumIn { get; set; }
        private string buildingIn { get; set; }
        private string roomIn { get; set; }

        public string DevCrossIn { get => devCrossIn; set { devCrossIn = value; OnPropertyChanged("DevCrossIn"); } }
        public int? PairNumIn { get => pairNumIn; set { pairNumIn = (int)value; OnPropertyChanged("PairNumIn"); } }
        public string BuildingIn { get => buildingIn; set { buildingIn = value; OnPropertyChanged("BuildingIn"); } }
        public string RoomIn { get => roomIn; set { roomIn = value; OnPropertyChanged("RoomIn"); } }

        private string devCrossChoise { get; set; }
        private int pairNumChoise { get; set; }
        private string buildingChoise { get; set; }
        private string roomChoise { get; set; }

        public string DevCrossChoise { get => devCrossChoise; set { devCrossChoise = value; OnPropertyChanged("DevCrossChoise"); } }
        public int PairNumChoise { get => pairNumChoise; set { pairNumChoise = value; OnPropertyChanged("PairNumChoise"); } }
        public string BuildingChoise { get => buildingChoise; set { buildingChoise = value; OnPropertyChanged("BuildingChoise"); } }
        public string RoomChoise { get => roomChoise; set { roomChoise = value; OnPropertyChanged("RoomChoise"); } }
    }
}
