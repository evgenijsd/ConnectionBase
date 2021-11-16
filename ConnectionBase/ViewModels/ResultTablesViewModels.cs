using ConnectionBase.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ConnectionBase.ViewModels
{
    public class ResultTablesViewModels : INotifyPropertyChanged
    {
        

        public ResultTablesViewModels()
        {
            People = GetEntity.GetList<PersonDto>("api/Person/all");
            Operators = GetEntity.GetList<OperatorDto>("api/Operator/all");
            Crosses = GetEntity.GetList<CrossDto>("api/Cross/all");
            Buildings = GetEntity.GetList<BuildingDto>("api/Building/all");
            Rooms = GetEntity.GetList<RoomDto>("api/Room/all");
            Devices = GetEntity.GetList<DeviceDto>("api/Device/all");
            Models = GetEntity.GetList<DeviceModelDto>("api/DeviceModel/all");
            Departs = GetEntity.GetList<DepartDto>("api/Depart/all");

            DevicePeople = GetEntity.GetList<DevicePersonDto>("api/DevicePerson/all");
            foreach (DevicePersonDto d in DevicePeople){
                d.People = People; d.Departs = Departs;
                if (d.Person != null) d.Depart = People.FirstOrDefault(x => x.PersonId == d.Person).Depart;
            }

            GenList = GetEntity.GetList<GenerationList>("api/TableGenerator/list");
            //var gen = CollectionViewSource.GetDefaultView(GenList);
            //gen.Filter = p => (p as GenerationList).DeviceEnd == 4;
            foreach (GenerationList d in GenList){
                d.Buildings = Buildings; d.Rooms = Rooms;
                d.DevCrossBegin = GetBox(d.CrossBegin, d.DeviceBegin);
                d.DevCrossEnd = GetBox(d.CrossEnd, d.DeviceEnd);
            }
            SelectedListItem = GenList[0];
        }

        public ObservableCollection<DeviceModelDto> Models { get; set; }
        public ObservableCollection<DepartDto> Departs { get; set; }
        public ObservableCollection<CrossDto> Crosses { get; set; }
        public ObservableCollection<BuildingDto> Buildings { get; set; }
        public ObservableCollection<RoomDto> Rooms { get; set; }
        public ObservableCollection<DeviceDto> Devices { get; set; }

        
        
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

        private string searchNumberIn = string.Empty;
        public string SearchNumberIn
        {
            get { return searchNumberIn; }
            set { searchNumberIn = value; OnPropertyChanged("SearchNumberIn"); }
        }

        private string searchNumberOut = string.Empty;
        public string SearchNumberOut
        {
            get { return searchNumberOut; }
            set { searchNumberOut = value; OnPropertyChanged("SearchNumberOut"); }
        }

        private ObservableCollection<NumberInDto> genNumberIn;
        public ObservableCollection<NumberInDto> GenNumberIn
        {
            get { return genNumberIn; }
            set { genNumberIn = value; OnPropertyChanged("GenNumberIn"); }
        }
        private NumberInDto selectedNumInItem;
        public NumberInDto SelectedNumInItem
        {
            get { return selectedNumInItem; }
            set
            {
                selectedNumInItem = value;
                OnPropertyChanged("SelectedNumInItem");
                TablePositionIn();
            }
        }
        private ObservableCollection<NumberOutDto> genNumberOut;
        public ObservableCollection<NumberOutDto> GenNumberOut
        {
            get { return genNumberOut; }
            set { genNumberOut = value; OnPropertyChanged("GenNumberOut"); }
        }
        private NumberOutDto selectedNumOutItem;
        public NumberOutDto SelectedNumOutItem
        {
            get { return selectedNumOutItem; }
            set
            {
                selectedNumOutItem = value;
                OnPropertyChanged("SelectedNumOutItem");
                TablePositionOut();
            }
        }
        private ObservableCollection<GenerationChains> genChain;
        public ObservableCollection<GenerationChains> GenChain
        {
            get { return genChain; }
            set { genChain = value; OnPropertyChanged("GenChain"); }
        }
        private ObservableCollection<GenerationList> genList;
        public ObservableCollection<GenerationList> GenList
        {
            get { return genList; }
            set { genList = value; OnPropertyChanged("GenList"); }
        }
        private GenerationList selectedListItem;
        public GenerationList SelectedListItem
        {
            get { return selectedListItem; }
            set {
                selectedListItem = value;
                OnPropertyChanged("SelectedListItem");
                TableChanges();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public ICommand SearchPhoneIn
        {
            get => new RelayCommand(
                    parameter =>
                    {
                        SearchNumberIn = SearchNumberIn.Replace(" ", "");
                        if (!string.IsNullOrEmpty(SearchNumberIn))
                        {
                            GenNumberIn = GetEntity.GetList<NumberInDto>("api/NumberIn/all");
                            var numinView = CollectionViewSource.GetDefaultView(GenNumberIn);
                            numinView.Filter = p => (p as NumberInDto).Number_In.Contains(SearchNumberIn);
                        }
                    });
        }
        public ICommand SearchPhoneOut
        {
            get => new RelayCommand(
                    parameter =>
                    {
                        SearchNumberOut = SearchNumberOut.Replace(" ","");
                        if (!string.IsNullOrEmpty(SearchNumberOut))
                        {
                            GenNumberOut = GetEntity.GetList<NumberOutDto>("api/NumberOut/all");
                            var numoutView = CollectionViewSource.GetDefaultView(GenNumberOut);
                            numoutView.Filter = p => (p as NumberOutDto).Number_Out.Contains(SearchNumberOut);
                        }
                    });
        }
        public ICommand FilterCommand
        {
            get => new RelayCommand(
                    parameter =>
                    {
                        SelectedListItem = GenList[5];
                    });
        }

        public void TableChanges()
        {
            var collectionView = CollectionViewSource.GetDefaultView(DevicePeople);
            collectionView.Filter = p => (p as DevicePersonDto).Device == SelectedListItem.DeviceEnd;

            GenChain = GetEntity.GetList<GenerationChains>($"api/TableGenerator/{SelectedListItem.PairEnd}");
            foreach (GenerationChains d in GenChain)
            {
                d.Buildings = Buildings; d.Rooms = Rooms;
                d.DevCross = GetBox(d.Cross, d.Device);
            }
            GenNumberIn = GetEntity.GetList<NumberInDto>("api/NumberIn/all");
            var numinView = CollectionViewSource.GetDefaultView(GenNumberIn);
            numinView.Filter = p => (p as NumberInDto).PairAts == SelectedListItem.PairBegin;

            GenNumberOut = GetEntity.GetList<NumberOutDto>("api/NumberOut/all");
            foreach (NumberOutDto d in GenNumberOut) d.Operators = Operators;
            var numoutView = CollectionViewSource.GetDefaultView(GenNumberOut);
            numoutView.Filter = p => (p as NumberOutDto).PairAts == SelectedListItem.PairBegin;
        }

        public void TablePositionIn()
        {
            if (SelectedNumInItem != null)
                SelectedListItem = GenList.FirstOrDefault(x => x.PairBegin == SelectedNumInItem.PairAts);
        }
        public void TablePositionOut()
        {
            if (SelectedNumOutItem != null)
                SelectedListItem = GenList.FirstOrDefault(x => x.PairBegin == SelectedNumOutItem.PairAts);
        }
    }
}
