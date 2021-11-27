using AutoMapper;
using ConnectionBase.DTO;
using ConnectionBase.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ConnectionBase.ViewModels
{
    public class EditConnectiosViewModel : INotifyPropertyChanged
    {
        public EditConnectiosViewModel()
        {

            People = GetEntity.GetList<Person>("api/Person/all");
            Operators = GetEntity.GetList<Operator>("api/Operator/all");
            Crosses = GetEntity.GetList<Cross>("api/Cross/all");
            Buildings = GetEntity.GetList<Building>("api/Building/all");
            Rooms = GetEntity.GetList<Room>("api/Room/all");
            Devices = GetEntity.GetList<Device>("api/Device/all");
            Models = GetEntity.GetList<DeviceModel>("api/DeviceModel/all");
            Departs = GetEntity.GetList<Depart>("api/Depart/all");

            if (Crosses.Count > 0)
            {
                GetTablePairs(Crosses[0].CrossId);
                foreach (Cross d in Crosses) d.Building = Rooms.FirstOrDefault(x => x.RoomId == d.Room).Building;
            }
            if (Devices.Count > 0) foreach (Device d in Devices) d.Building = Rooms.FirstOrDefault(x => x.RoomId == d.Room).Building;
        }

        
        public ObservableCollection<DeviceModel> Models { get; set; }
        public ObservableCollection<Depart> Departs { get; set; }
        public ObservableCollection<Cross> Crosses { get; set; }
        public ObservableCollection<Building> Buildings { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }
        public ObservableCollection<Device> Devices { get; set; }
        public ObservableCollection<NumberIn> GenNumberIn { get; set; }
        public ObservableCollection<NumberOut> GenNumberOut { get; set; }
        public ObservableCollection<GenerationList> GenList { get; set; }
        public ObservableCollection<GenerationChains> GenChain { get; set; }
        public ObservableCollection<Person> People { get; set; }
        public ObservableCollection<Operator> Operators { get; set; }
        public ObservableCollection<DevicePerson> DevicePeople { get; set; }

        public string GetBox(int? cross, int? device)
        {
            string result = string.Empty;
            if (device != null && device > 0) result = Models.FirstOrDefault(x => x.ModelId == Devices.FirstOrDefault(x => x.DeviceId == device).Model).ModelName;
            if (cross != null && cross > 0) result += Crosses.FirstOrDefault(x => x.CrossId == cross).CrossName;
            return result;
        }

        private Building selectedCrossBuilding;
        public Building SelectedCrossBuilding
        {
            get => selectedCrossBuilding;
            set
            {
                selectedCrossBuilding = value;
                OnPropertyChanged("SelectedCrossBuilding");
                if (SelectedCrossBuilding != null) {
                    var crossView = CollectionViewSource.GetDefaultView(Crosses);
                    crossView.Filter = p => (p as Cross).Building == SelectedCrossBuilding.BuildingId;
                }
            }
        }
        private Building selectedDeviceBuilding;
        public Building SelectedDeviceBuilding
        {
            get => selectedDeviceBuilding;
            set
            {
                selectedDeviceBuilding = value;
                OnPropertyChanged("SelectedDeviceBuilding");
                if (SelectedDeviceBuilding != null)
                {
                    var roomView = CollectionViewSource.GetDefaultView(Rooms);
                    roomView.Filter = p => (p as Room).Building == SelectedDeviceBuilding.BuildingId;
                }
            }
        }

        private Cross selectedCross;
        public Cross SelectedCross
        {
            get => selectedCross;
            set
            {
                selectedCross = value;
                OnPropertyChanged("SelectedCross");
            }
        }

        private Room selectedRoom;
        public Room SelectedRoom
        {
            get => selectedRoom;
            set
            {
                selectedRoom = value;
                OnPropertyChanged("SelectedRoom");
            }
        }

        private int idPairCurrent;
        private string devCrossCurrent;
        private string pairNumCurrent;
        private string buildingCurrent;
        private string roomCurrent;

        public int IdPairCurrent { get => idPairCurrent; set { idPairCurrent = value; OnPropertyChanged("IdPairCurrent"); } }
        public string DevCrossCurrent 
        { 
            get => devCrossCurrent; 
            set 
            { 
                devCrossCurrent = value;
                OnPropertyChanged("DevCrossCurrent");
            } 
        }
        public string PairNumCurrent { get => pairNumCurrent; set { pairNumCurrent = value; OnPropertyChanged("PairNumCurrent"); } }
        public string BuildingCurrent { get => buildingCurrent; set { buildingCurrent = value; OnPropertyChanged("BuildingCurrent"); } }
        public string RoomCurrent { get => roomCurrent; set { roomCurrent = value; OnPropertyChanged("RoomCurrent"); } }

        private int idPairIn { get; set; }
        private string devCrossIn { get; set; }
        private string pairNumIn { get; set; }
        private string buildingIn { get; set; }
        private string roomIn { get; set; }

        public int IdPairIn { get => idPairIn; set { idPairIn = value; OnPropertyChanged("IdPairIn"); } }
        public string DevCrossIn { get => devCrossIn; set { devCrossIn = value; OnPropertyChanged("DevCrossIn"); } }
        public string PairNumIn { get => pairNumIn; set { pairNumIn = value; OnPropertyChanged("PairNumIn"); } }
        public string BuildingIn { get => buildingIn; set { buildingIn = value; OnPropertyChanged("BuildingIn"); } }
        public string RoomIn { get => roomIn; set { roomIn = value; OnPropertyChanged("RoomIn"); } }

        private int idPairChoise { get; set; }
        private string devCrossChoise { get; set; }
        private string pairNumChoise { get; set; }
        private string buildingChoise { get; set; }
        private string roomChoise { get; set; }

        public int IdPairChoise { get => idPairChoise; set { idPairChoise = value; OnPropertyChanged("IdPairChoise"); } }
        public string DevCrossChoise { get => devCrossChoise; set { devCrossChoise = value; OnPropertyChanged("DevCrossChoise"); } }
        public string PairNumChoise { get => pairNumChoise; set { pairNumChoise = value; OnPropertyChanged("PairNumChoise"); } }
        public string BuildingChoise { get => buildingChoise; set { buildingChoise = value; OnPropertyChanged("BuildingChoise"); } }
        public string RoomChoise { get => roomChoise; set { roomChoise = value; OnPropertyChanged("RoomChoise"); } }

        private ObservableCollection<Pair> _pairs;
        public ObservableCollection<Pair> Pairs
        {
            get { return _pairs; }
            set { _pairs = value; OnPropertyChanged("Pairs"); }
        }
        private ObservableCollection<Pair> _pairOuts;
        public ObservableCollection<Pair> PairOuts
        {
            get { return _pairOuts; }
            set { _pairOuts = value; OnPropertyChanged("PairOuts"); }
        }
        private Pair _selectedPair;
        public Pair SelectedPair
        {
            get { return _selectedPair; }
            set
            {
                _selectedPair = value;
                OnPropertyChanged("SelectedPair");
                TableSelection();
            }
        }
        private Pair _selectedPairOut;
        public Pair SelectedPairOut
        {
            get { return _selectedPairOut; }
            set
            {
                _selectedPairOut = value;
                OnPropertyChanged("SelectedPairOut");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        void GetTablePairs(int CrossId)
        {
            Pairs = GetEntity.GetList<Pair>($"api/Pair/cross/{CrossId}");

            
            foreach (Pair d in Pairs)
            {
                d.DevCross = GetBox(d.Cross, Devices.FirstOrDefault(x => x.Pair == d.PairId)?.DeviceId).ToString();
                var pairin = GetEntity.GetById<Pair>($"api/Pair/{d.PairIn}");
                if (pairin != null)
                {
                    d.PairNumIn = pairin.PairNum;
                    d.DevCrossIn = GetBox(pairin.Cross, Devices.FirstOrDefault(x => x.Pair == d.PairIn)?.DeviceId).ToString();
                }
            }
        }

        void GetDevicePairs(Building building, Room room)
        {
            List<Device> device;
            if (room != null) device = Devices.Where(x => x.Room == room.RoomId).ToList();
            else if (building != null) device = Devices.Where(x => x.Building == building.BuildingId).ToList();
            else return;

            Pairs.Clear();
            foreach(Device d in device)
            {
                Pair p = GetEntity.GetById<Pair>($"api/Pair/{d.Pair}");
                p.DevCross = GetBox(null, d.DeviceId).ToString();
                var pairin = GetEntity.GetById<Pair>($"api/Pair/{p.PairIn}");
                if (pairin != null)
                {
                    p.PairNumIn = pairin.PairNum;
                    p.DevCrossIn = GetBox(pairin.Cross, Devices.FirstOrDefault(x => x.Pair == p.PairIn)?.DeviceId).ToString();
                }
                Pairs.Add(p);
            }
        }

        public void TableSelection()
        {
            if (SelectedPair == null) return;
            Cross cross;
            Room room;
            Building building;
            Device device;
            if (SelectedPair.Cross != null)
            {
                cross = Crosses.FirstOrDefault(x => x.CrossId == SelectedPair.Cross);
                room = Rooms.FirstOrDefault(x => x.RoomId == cross.Room);
            }
            else
            {
                device = Devices.FirstOrDefault(x => x.Pair == SelectedPair.PairId);
                room = Rooms.FirstOrDefault(x => x.RoomId == device.Room); 
            }
            building = Buildings.FirstOrDefault(x => x.BuildingId == room.Building);

            DevCrossChoise = SelectedPair.DevCross;
            PairNumChoise = $"{SelectedPair.PairNum:D3}";
            BuildingChoise = building.BuildingName;
            RoomChoise = room.RoomName;
            IdPairChoise = SelectedPair.PairId;
            PairOuts = GetEntity.GetList<Pair>($"api/Pair/out/{SelectedPair.PairId}");
            foreach (Pair d in PairOuts)
            {
                d.DevCross = GetBox(d.Cross, Devices.FirstOrDefault(x => x.Pair == d.PairId)?.DeviceId).ToString();
            }
        }

        public ICommand FilterCrossCommand
        {
            get => new RelayCommand(
                    parameter =>
                    {
                        if (SelectedCross != null) GetTablePairs(SelectedCross.CrossId);
                    });
        }

        public ICommand FilterDeviceCommand
        {
            get => new RelayCommand(
                    parameter =>
                    {
                        GetDevicePairs(SelectedDeviceBuilding, SelectedRoom);
                    });
        }
        public ICommand SetCurrentPairCommand
        {
            get => new RelayCommand(
                    parameter =>
                    {
                        if (SelectedPair != null) SetCurrentPair(IdPairChoise);
                    });
        }
        public ICommand SetCurrentPairInCommand
        {
            get => new RelayCommand(
                    parameter =>
                    {
                        if (idPairIn > 0) SetCurrentPair(IdPairIn);
                    });
        }
        public ICommand FindPairInCommand
        {
            get => new RelayCommand(
                    parameter =>
                    {
                        if (IdPairIn > 0) FindPair(IdPairIn);
                    });
        }

        public ICommand FindPairCurrentCommand
        {
            get => new RelayCommand(
                    parameter =>
                    {
                        if (IdPairCurrent > 0) FindPair(IdPairCurrent);
                    });
        }

        public ICommand FindPairOutCommand
        {
            get => new RelayCommand(
                    parameter =>
                    {
                        if (SelectedPairOut != null) FindPair(SelectedPairOut.PairId);
                    });
        }
        public ICommand SetChoisePairInCommand
        {
            get => new RelayCommand(
                    parameter =>
                    {
                        if (idPairChoise == idPairCurrent)
                        {
                            MessageBox.Show("Выбранная и текущая пара совпадают");
                            return;
                        }
                        if (idPairCurrent > 0)
                        {
                            MessageBoxResult m = MessageBox.Show("Установить входящую пару? (изменения в базе)", "Внимание", MessageBoxButton.OKCancel);
                            if (m == MessageBoxResult.OK)
                            {
                                var pairCurrent = GetEntity.GetById<Pair>($"api/Pair/{idPairCurrent}");
                                pairCurrent.PairIn = IdPairChoise;
                                var config = new MapperConfiguration(cfg => cfg.CreateMap<Pair, PairDto>());
                                var mapper = new Mapper(config);
                                var pair = mapper.Map<Pair, PairDto>(pairCurrent);
                                pair = GetEntity.Update<PairDto>($"api/Pair/update/", pair);

                                Cross cross;
                                Room room;
                                Building building;
                                Device device = new();
                                if (pairCurrent.PairIn != null)
                                {
                                    int? deviceId = null;
                                    var pairIn = GetEntity.GetById<Pair>($"api/Pair/{IdPairChoise}");
                                    if (pairIn.Cross != null)
                                    {
                                        cross = Crosses.FirstOrDefault(x => x.CrossId == pairIn.Cross);
                                        room = Rooms.FirstOrDefault(x => x.RoomId == cross.Room);
                                        deviceId = null;
                                    }
                                    else
                                    {
                                        device = Devices.FirstOrDefault(x => x.Pair == pairIn.PairId);
                                        deviceId = device.DeviceId;
                                        room = Rooms.FirstOrDefault(x => x.RoomId == device.Room);
                                    }
                                    pairIn.DevCross = GetBox(pairIn.Cross, deviceId).ToString();
                                    building = Buildings.FirstOrDefault(x => x.BuildingId == room.Building);

                                    IdPairIn = pairIn.PairId;
                                    DevCrossIn = pairIn.DevCross;
                                    PairNumIn = $"{pairIn.PairNum:D3}";
                                    BuildingIn = building.BuildingName;
                                    RoomIn = room.RoomName;
                                    FindPair(idPairCurrent);
                                }
                                else
                                {
                                    IdPairIn = 0;
                                    DevCrossIn = string.Empty;
                                    PairNumIn = string.Empty;
                                    BuildingIn = string.Empty;
                                    RoomIn = string.Empty;
                                }
                            }
                            else { }
                        }
                        else MessageBox.Show("Текущая пара не установлена");

                    });
        }

        public ICommand DeletePairInCommand
        {
            get => new RelayCommand(
                    parameter =>
                    {
                        if (idPairIn > 0)
                        {
                            MessageBoxResult m = MessageBox.Show("Отсоеденить входящую пару? (изменения в базе)", "Внимание", MessageBoxButton.OKCancel);
                            if (m == MessageBoxResult.OK)
                            {
                                var pairCurrent = GetEntity.GetById<Pair>($"api/Pair/{idPairCurrent}");
                                pairCurrent.PairIn = null;
                                var config = new MapperConfiguration(cfg => cfg.CreateMap<Pair, PairDto>());
                                var mapper = new Mapper(config);
                                var pair = mapper.Map<Pair, PairDto>(pairCurrent);
                                pair = GetEntity.Update<PairDto>($"api/Pair/update/", pair);

                                IdPairIn = 0;
                                DevCrossIn = string.Empty;
                                PairNumIn = string.Empty;
                                BuildingIn = string.Empty;
                                RoomIn = string.Empty;

                                FindPair(idPairCurrent);
                            }
                            else { }
                        }
                        else MessageBox.Show("Входящая пара не установлена");

                    });
        }

        public void SetCurrentPair(int id)
        {
            Cross cross;
            Room room;
            Building building;
            Device device = new();
            var pair = GetEntity.GetById<Pair>($"api/Pair/{id}");

            if (pair != null)
            {
                int? deviceId = null;
                if (pair.Cross != null)
                {
                    cross = Crosses.FirstOrDefault(x => x.CrossId == pair.Cross);
                    room = Rooms.FirstOrDefault(x => x.RoomId == cross.Room);
                }
                else
                {
                    device = Devices.FirstOrDefault(x => x.Pair == pair.PairId);
                    deviceId = device.DeviceId;
                    room = Rooms.FirstOrDefault(x => x.RoomId == device.Room);
                }
                pair.DevCross = GetBox(pair.Cross, deviceId).ToString();
                building = Buildings.FirstOrDefault(x => x.BuildingId == room.Building);

                IdPairCurrent = pair.PairId;
                DevCrossCurrent = pair.DevCross;
                PairNumCurrent = $"{pair.PairNum:D3}";
                BuildingCurrent = building.BuildingName;
                RoomCurrent = room.RoomName;

                if (pair.PairIn != null)
                {
                    var pairIn = GetEntity.GetById<Pair>($"api/Pair/{pair.PairIn}");
                    if (pairIn.Cross != null)
                    {
                        cross = Crosses.FirstOrDefault(x => x.CrossId == pairIn.Cross);
                        room = Rooms.FirstOrDefault(x => x.RoomId == cross.Room);
                        deviceId = null;
                    }
                    else
                    {
                        device = Devices.FirstOrDefault(x => x.Pair == pairIn.PairId);
                        deviceId = device.DeviceId;
                        room = Rooms.FirstOrDefault(x => x.RoomId == device.Room);
                    }
                    pairIn.DevCross = GetBox(pairIn.Cross, deviceId).ToString();
                    building = Buildings.FirstOrDefault(x => x.BuildingId == room.Building);

                    IdPairIn = pairIn.PairId;
                    DevCrossIn = pairIn.DevCross;
                    PairNumIn = $"{pairIn.PairNum:D3}";
                    BuildingIn = building.BuildingName;
                    RoomIn = room.RoomName;
                }
                else
                {
                    IdPairIn = 0;
                    DevCrossIn = string.Empty;
                    PairNumIn = string.Empty;
                    BuildingIn = string.Empty;
                    RoomIn = string.Empty;
                }
            }
            else
            {
                IdPairCurrent = 0;
                DevCrossCurrent = string.Empty;
                PairNumCurrent = string.Empty;
                BuildingCurrent = string.Empty;
                RoomCurrent = string.Empty;
            }
        }

        public void FindPair(int pairId)
        {
            var pair = GetEntity.GetById<Pair>($"api/Pair/{pairId}");

            if (pair != null)
            {
                if (pair.Cross != null)
                { 
                    GetTablePairs((int)pair.Cross); 
                }
                else
                {
                    var device = Devices.FirstOrDefault(x => x.Pair == pair.PairId);
                    var room = Rooms.FirstOrDefault(x => x.RoomId == device.Room);
                    var building = Buildings.FirstOrDefault(x => x.BuildingId == room.Building);
                    GetDevicePairs(building, null);
                }
                SelectedPair = Pairs.FirstOrDefault(x => x.PairId == pairId);
            }
        }
    }
}
