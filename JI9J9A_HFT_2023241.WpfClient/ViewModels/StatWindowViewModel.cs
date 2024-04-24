using JI9J9A_HFT_2023241.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JI9J9A_HFT_2023241.WpfClient.ViewModels
{
    internal class StatWindowViewModel : ObservableRecipient
    {
        public static bool IsIndesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        RestService rest;
        public double AvgAmountOfGuns { get; set; }
        public IEnumerable<LicenceInfo> LicenceInfos { get; set; }
        public IEnumerable<LicenceStat> LicenceStats { get; set; }
        public IEnumerable<Owner> ExpiredLicences { get; set; }
        public IEnumerable<Ammo> Top3MostUsedAmmoTypes { get; set; }
        public ObservableCollection<Firearm> FirearmsUsingAmmo { get; set; }

        public RestCollection<Ammo> Ammos { get; set; }

        private LicenceStat selectedStat;

        public LicenceStat SelectedStat
        {
            get
            {
                return selectedStat;
            }
            set
            {
                if (value == null)
                {
                    return;
                }
                selectedStat = new LicenceStat()
                {
                    Firearm = value.Firearm,
                    licenceCounts = value.licenceCounts
                };
                OnPropertyChanged();
            }
        }

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
                    FirearmsUsingAmmo = value.FirearmsUsingAmmo,
                    BulletType = value.BulletType,
                    Diameter = value.Diameter,
                    Length = value.Length,
                    Name = value.Name
                };
                OnPropertyChanged();
                FetchFirearmsForSelectedAmmo();
            }
        }




        public StatWindowViewModel()
        {
            if (IsIndesignMode)
            {
                return;
            }
            string server = "http://localhost:27031/";
            rest = new RestService(server, "firearm");
            Ammos = new RestCollection<Ammo>(server, "ammo", "hub");
            string link = "Stat/AverageAmountOfGuns";
            AvgAmountOfGuns = rest.GetSingle<double>(link);

            link = "Stat/AmountOfEachLicenceGivenOut";
            LicenceInfos = rest.GetSingle<IEnumerable<LicenceInfo>>(link);

            link = "Stat/ExpiredLicences";
            ExpiredLicences = rest.GetSingle<IEnumerable<Owner>>(link);

            link = "Stat/FirearmsAndLicenceTypes";
            LicenceStats = rest.GetSingle<IEnumerable<LicenceStat>>(link);

            link = "Stat/Top3MostUsedAmmoTypes";
            Top3MostUsedAmmoTypes = rest.GetSingle<IEnumerable<Ammo>>(link);

            FirearmsUsingAmmo = new ObservableCollection<Firearm>();

            //link = "Stat/FirearmsUsingSpecifiedAmmo";
            //ez lekér egy ammot a usertől:
            //rest.Get Ammo
            //var value = rest.Get<IEnumerable<Firearm>>(id, link);

        }

        private void FetchFirearmsForSelectedAmmo()
        {
            if (SelectedAmmo != null)
            {
                string link = "Stat/FirearmsUsingSpecifiedAmmo";
                var value = rest.Get<IEnumerable<Firearm>>(SelectedAmmo.AmmoId, link);
                FirearmsUsingAmmo.Clear();
                foreach (var item in value)
                {
                    FirearmsUsingAmmo.Add(item);
                }
            }
        }
    }
}
