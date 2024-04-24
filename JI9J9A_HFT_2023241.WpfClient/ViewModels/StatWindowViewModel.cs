using JI9J9A_HFT_2023241.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
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

        public StatWindowViewModel()
        {
            rest = new RestService("http://localhost:27031/", "firearm");

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


            link = "Stat/FirearmsUsingSpecifiedAmmo";
            //ez lekér egy ammot a usertől:
            //rest.Get Ammo
            //var value = rest.Get<IEnumerable<Firearm>>(id, link);

        }
    }
}
