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
    class EditBuildingViewModel : INotifyPropertyChanged
    {
        public EditBuildingViewModel()
        {
            Buildings = GetEntity.GetList<Building>("api/Building/all");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private bool _isSelect = false;
        public bool IsSelect
        {
            get { return _isSelect; }
            set
            {
                _isSelect = value;
                OnPropertyChanged("IsSelect");
            }
        }
        private bool _isNull = false;
        public bool IsNull
        {
            get { return _isNull; }
            set
            {
                _isNull = value;
                OnPropertyChanged("IsNull");
            }
        }

        private ObservableCollection<Building> buildings;
        public ObservableCollection<Building> Buildings
        {
            get => buildings;
            set
            {
                buildings = value;
                OnPropertyChanged("Buildings");
            }
        }

        private string searchBuilding = string.Empty;
        public string SearchBuilding
        {
            get { return searchBuilding; }
            set { searchBuilding = value; OnPropertyChanged("SearchBuilding"); }
        }

        private Building selectedBuilding;
        public Building SelectedBuilding
        {
            get => selectedBuilding;
            set
            {
                selectedBuilding = value;
                OnPropertyChanged("SelectedBuilding");
                if (SelectedBuilding != null) NameBuilding = SelectedBuilding.BuildingName;
            }
        }

        private string _nameBuilding;
        public string NameBuilding
        {
            get { return _nameBuilding; }
            set
            {
                _nameBuilding = value;
                OnPropertyChanged("NameBuilding");
                IsNull = !string.IsNullOrEmpty(NameBuilding);
                IsSelect = SelectedBuilding != null && IsNull;
            }
        }

        public ICommand AddBuildingCommand
        {
            get => new RelayCommand(
                    parameter =>
                    {
                        if (NameBuilding != null)
                        {
                            var building = new BuildingDto { BuildingId = 0, BuildingName = NameBuilding };
                            int result = GetEntity.Add<BuildingDto>($"api/Building/add", building);
                            Buildings = GetEntity.GetList<Building>("api/Building/all");
                            IsSelect = false;
                        }
                        else MessageBox.Show("Не заполнены данные");

                    });
        }


        public ICommand UpdateBuildingCommand
        {
            get => new RelayCommand(
                    parameter =>
                    {
                        if (SelectedBuilding != null && SelectedBuilding.BuildingId > 0 && !string.IsNullOrEmpty(NameBuilding))
                        {
                            MessageBoxResult m = MessageBox.Show("Изменить сооружение? (изменения в базе)", "Внимание", MessageBoxButton.OKCancel);
                            if (m == MessageBoxResult.OK)
                            {
                                var buildingCurrent = SelectedBuilding;
                                buildingCurrent.BuildingName = NameBuilding;
                                var config = new MapperConfiguration(cfg => cfg.CreateMap<Building, BuildingDto>());
                                var mapper = new Mapper(config);
                                var building = mapper.Map<Building, BuildingDto>(buildingCurrent);
                                building = GetEntity.Update<BuildingDto>($"api/Building/update/", building);
                                Buildings = GetEntity.GetList<Building>("api/Building/all");
                                IsSelect = false;
                            }
                            else { }
                        }
                        else MessageBox.Show("Не верные данные");
                    });
        }

        public ICommand DeleteBuildingCommand
        {
            get => new RelayCommand(
                    parameter =>
                    {
                        if (SelectedBuilding != null && SelectedBuilding.BuildingId > 0)
                        {
                            MessageBoxResult m = MessageBox.Show("Удалить текущее сооружение? (изменения в базе)", "Внимание", MessageBoxButton.OKCancel);
                            if (m == MessageBoxResult.OK)
                            {
                                bool result = GetEntity.Delete($"api/Building/{SelectedBuilding.BuildingId}");
                                Buildings = GetEntity.GetList<Building>("api/Building/all");
                                IsSelect = false;
                            }
                            else { }
                        }
                        else MessageBox.Show("Не выбрано сооружение для удаления");
                    });
        }

        public ICommand SearchBuildingCommand
        {
            get => new RelayCommand(
                    parameter =>
                    {
                        var buildingView = CollectionViewSource.GetDefaultView(Buildings);
                        if (!string.IsNullOrEmpty(SearchBuilding) && !string.IsNullOrWhiteSpace(SearchBuilding))
                        {
                            buildingView.Filter = p => (p as Building).BuildingName.ToLower().Contains(SearchBuilding.ToLower());
                        }
                        else
                        {
                            buildingView.Filter = null;
                        }
                    });
        }
    }
}
