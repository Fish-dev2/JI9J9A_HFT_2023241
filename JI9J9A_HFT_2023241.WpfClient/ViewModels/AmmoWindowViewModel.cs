using JI9J9A_HFT_2023241.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace JI9J9A_HFT_2023241.WpfClient.ViewModels
{
    public class AmmoWindowViewModel : ObservableRecipient
    {
        public RestCollection<Ammo> Ammos { get; set; }

        private Ammo selectedAmmo;

        public Ammo SelectedAmmo
        {
            get { return selectedAmmo; }
            set
            {
                if (value == null)
                {
                    return;
                }
                selectedAmmo = new Ammo()
                {
                    AmmoId = value.AmmoId,
                    Diameter = value.Diameter,
                    Length = value.Length,
                    BulletType = value.BulletType,
                    Name = value.Name,
                    FirearmsUsingAmmo = value.FirearmsUsingAmmo
                };
                OnPropertyChanged();
                (DeleteAmmoCommand as RelayCommand).NotifyCanExecuteChanged();
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

        public ICommand CreateAmmoCommand { get; set; }
        public ICommand DeleteAmmoCommand { get; set; }
        public ICommand UpdateAmmoCommand { get; set; }

        public AmmoWindowViewModel()
        {
            if (IsIndesignMode)
            {
                return;
            }
            string server = "http://localhost:27031/";

            Ammos = new RestCollection<Ammo>(server, "ammo", "hub");
            CreateAmmoCommand = new RelayCommand(() =>
            {
                Ammos.Add(new Ammo()
                {
                    Diameter = SelectedAmmo.Diameter,
                    Length = SelectedAmmo.Length,
                });
            });
            UpdateAmmoCommand = new RelayCommand(() =>
            {
                Ammos.Update(SelectedAmmo);
            });
            DeleteAmmoCommand = new RelayCommand(() =>
            {
                Ammos.Delete(SelectedAmmo.AmmoId);
            },
            () =>
            {
                return SelectedAmmo != null;
            });

            SelectedAmmo = new Ammo();

        }
    }
}
