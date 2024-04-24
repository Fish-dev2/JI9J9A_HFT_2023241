using JI9J9A_HFT_2023241.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace JI9J9A_HFT_2023241.WpfClient.ViewModels
{
    public class FirearmWindowViewModel : ObservableRecipient
    {
        public RestCollection<Firearm> Firearms { get; set; }
        private Firearm selectedFirearm;

        public Firearm SelectedFirearm
        {
            get { return selectedFirearm; }
            set
            {
                if (value == null)
                {
                    return;
                }
                selectedFirearm = new Firearm()
                {
                    AmmoId = value.AmmoId,
                    AmmoType = value.AmmoType,
                    FireRate = value.FireRate,
                    GunId = value.GunId,
                    Manufacturer = value.Manufacturer,
                    Name = value.Name,
                    OwnersHavingThisGun = value.OwnersHavingThisGun,
                    Registers = value.Registers,
                    ReleaseDate = value.ReleaseDate,
                };
                OnPropertyChanged();
                (DeleteFirearmCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }


        public static bool IsIndesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public ICommand CreateFirearmCommand { get; set; }
        public ICommand DeleteFirearmCommand { get; set; }
        public ICommand UpdateFirearmCommand { get; set; }



        public FirearmWindowViewModel()
        {
            if (IsIndesignMode)
            {
                return;
            }
            string server = "http://localhost:27031/";

            Firearms = new RestCollection<Firearm>(server, "firearm", "hub");

            CreateFirearmCommand = new RelayCommand(() =>
            {
                Firearms.Add(new Firearm()
                {
                    Name = SelectedFirearm.Name,
                    FireRate = SelectedFirearm.FireRate,
                    AmmoId = SelectedFirearm.AmmoId,
                    Manufacturer = SelectedFirearm.Manufacturer,
                    ReleaseDate = SelectedFirearm.ReleaseDate
                    
                });
            });
            UpdateFirearmCommand = new RelayCommand(() =>
            {
                Firearms.Update(SelectedFirearm);
            });
            DeleteFirearmCommand = new RelayCommand(() =>
            {
                Firearms.Delete(SelectedFirearm.GunId);
            },
            () =>
            {
                return SelectedFirearm != null;
            });


            SelectedFirearm = new Firearm()
            {
                ReleaseDate = DateTime.Now,
            }
                ;

        }
    }
}
