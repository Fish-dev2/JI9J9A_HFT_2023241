using JI9J9A_HFT_2023241.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Linq;

namespace JI9J9A_HFT_2023241.WpfClient.ViewModels
{
    public class OwnerWindowViewModel : ObservableRecipient
    {
        public static IEnumerable<LicenceType> LicenceValues
        {
            get { return Enum.GetValues(typeof(LicenceType)).Cast<LicenceType>(); }
        }

        public RestCollection<Owner> Owners { get; set; }

        private Owner selectedOwner;

        public Owner SelectedOwner
        {
            get { return selectedOwner; }
            set
            {
                if (value == null)
                {
                    return;
                }
                selectedOwner = new Owner()
                {
                    OwnerId = value.OwnerId,
                    FirstName = value.FirstName,
                    LastName = value.LastName,
                    LicenceType = value.LicenceType,
                    LicenceValidUntil = value.LicenceValidUntil,
                    LicensedGuns = value.LicensedGuns,
                    Registers = value.Registers
                };
                OnPropertyChanged();
                (DeleteOwnerCommand as RelayCommand).NotifyCanExecuteChanged();
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

        public ICommand CreateOwnerCommand { get; set; }
        public ICommand DeleteOwnerCommand { get; set; }
        public ICommand UpdateOwnerCommand { get; set; }

        public OwnerWindowViewModel()
        {
            if (IsIndesignMode)
            {
                return;
            }
            string server = "http://localhost:27031/";

            Owners = new RestCollection<Owner>(server, "owner", "hub");
            CreateOwnerCommand = new RelayCommand(() =>
            {
                Owners.Add(new Owner()
                {
                    FirstName = SelectedOwner.FirstName,
                    LastName = SelectedOwner.LastName,
                });
            });
            UpdateOwnerCommand = new RelayCommand(() =>
            {
                Owners.Update(SelectedOwner);
            });
            DeleteOwnerCommand = new RelayCommand(() =>
            {
                Owners.Delete(SelectedOwner.OwnerId);
            },
            () =>
            {
                return SelectedOwner != null;
            });

            SelectedOwner = new Owner();

        }
    }
}
