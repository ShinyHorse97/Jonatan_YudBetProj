using Jonatan_YudBetProj.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Jonatan_YudBetProj.ViewModels
{
    internal class RacecarCRUDViewModel:ViewModelBase
    {
        private ObservableCollection<Racecar> carObsList;
        private Racecar? editingRacecar;
        private string nickname = string.Empty;
        private string team = string.Empty;
        private string carModel = string.Empty;
        private string marque = string.Empty;
        private string carImage = string.Empty;
        private string year = string.Empty;
        private string horsepower = string.Empty;
        private string torque = string.Empty;
        private string weight = string.Empty;
        private string validationMessage = string.Empty;

        public ObservableCollection<Racecar> CarObsList
        {
            get { return carObsList; }
            set {
                carObsList = value; 
                OnPropertyChanged(); 
            }
        }

        public string Nickname { get => nickname; set => SetProperty(ref nickname, value); }
        public string Team { get => team; set => SetProperty(ref team, value); }
        public string CarModel { get => carModel; set => SetProperty(ref carModel, value); }
        public string Marque { get => marque; set => SetProperty(ref marque, value); }
        public string CarImage { get => carImage; set => SetProperty(ref carImage, value); }
        public string Year { get => year; set => SetProperty(ref year, value); }
        public string Horsepower { get => horsepower; set => SetProperty(ref horsepower, value); }
        public string Torque { get => torque; set => SetProperty(ref torque, value); }
        public string Weight { get => weight; set => SetProperty(ref weight, value); }
        public string FormTitle => editingRacecar is null ? "Add racecar" : "Edit racecar";
        public string SaveButtonText => editingRacecar is null ? "Add racecar" : "Save changes";
        public bool IsEditing => editingRacecar is not null;
        public string ValidationMessage { get => validationMessage; private set => SetProperty(ref validationMessage, value); }

        public ICommand SaveCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }


        public RacecarCRUDViewModel()
        {
            List<Racecar> lst = services.Database.RacecarsList;
            CarObsList = new ObservableCollection<Racecar>(lst);
            SaveCommand = new Command(Save);
            EditCommand = new Command<Racecar>(Edit);
            DeleteCommand = new Command<Racecar>(Delete);
            ClearCommand = new Command(Clear);
        }

        private void Save()
        {
            ValidationMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(Nickname) || string.IsNullOrWhiteSpace(CarModel) ||
                !int.TryParse(Year, out int parsedYear) || !int.TryParse(Horsepower, out int parsedHorsepower) ||
                !int.TryParse(Torque, out int parsedTorque) || !double.TryParse(Weight, out double parsedWeight))
            {
                ValidationMessage = "Enter a nickname, model, and valid numeric values for year, horsepower, torque, and weight.";
                return;
            }

            if (editingRacecar is null)
            {
                var racecar = new Racecar
                {
                    Id = NextId(),
                    Nickname = Nickname.Trim(),
                    Team = Team.Trim(),
                    CarModel = CarModel.Trim(),
                    Marque = Marque.Trim(),
                    CarImage = CarImage.Trim(),
                    Year = parsedYear,
                    Horsepower = parsedHorsepower,
                    Torque = parsedTorque,
                    Weight = parsedWeight
                };
                services.Database.RacecarsList.Add(racecar);
                CarObsList.Add(racecar);
            }
            else
            {
                editingRacecar.Nickname = Nickname.Trim();
                editingRacecar.Team = Team.Trim();
                editingRacecar.CarModel = CarModel.Trim();
                editingRacecar.Marque = Marque.Trim();
                editingRacecar.CarImage = CarImage.Trim();
                editingRacecar.Year = parsedYear;
                editingRacecar.Horsepower = parsedHorsepower;
                editingRacecar.Torque = parsedTorque;
                editingRacecar.Weight = parsedWeight;
                int index = CarObsList.IndexOf(editingRacecar);
                if (index >= 0)
                {
                    CarObsList[index] = editingRacecar;
                }
            }

            Clear();
        }

        private void Edit(Racecar? racecar)
        {
            if (racecar is null)
            {
                return;
            }

            editingRacecar = racecar;
            Nickname = racecar.Nickname ?? string.Empty;
            Team = racecar.Team ?? string.Empty;
            CarModel = racecar.CarModel ?? string.Empty;
            Marque = racecar.Marque ?? string.Empty;
            CarImage = racecar.CarImage ?? string.Empty;
            Year = racecar.Year.ToString();
            Horsepower = racecar.Horsepower.ToString();
            Torque = racecar.Torque.ToString();
            Weight = racecar.Weight.ToString();
            OnPropertyChanged(nameof(FormTitle));
            OnPropertyChanged(nameof(SaveButtonText));
            OnPropertyChanged(nameof(IsEditing));
        }

        private void Delete(Racecar? racecar)
        {
            if (racecar is null)
            {
                return;
            }

            services.Database.RacecarsList.Remove(racecar);
            CarObsList.Remove(racecar);
            if (ReferenceEquals(editingRacecar, racecar))
            {
                Clear();
            }
        }

        private void Clear()
        {
            editingRacecar = null;
            Nickname = string.Empty;
            Team = string.Empty;
            CarModel = string.Empty;
            Marque = string.Empty;
            CarImage = string.Empty;
            Year = string.Empty;
            Horsepower = string.Empty;
            Torque = string.Empty;
            Weight = string.Empty;
            ValidationMessage = string.Empty;
            OnPropertyChanged(nameof(FormTitle));
            OnPropertyChanged(nameof(SaveButtonText));
            OnPropertyChanged(nameof(IsEditing));
        }

        private static string NextId()
        {
            int nextId = services.Database.RacecarsList
                .Select(racecar => int.TryParse(racecar.Id, out int id) ? id : 0)
                .DefaultIfEmpty(0)
                .Max() + 1;
            return nextId.ToString();
        }
    }
}
