using JI9J9A_HFT_2023241.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace JI9J9A_HFT_2023241.WpfClient.ViewModels
{
    public class RegisterWindowViewModel : ObservableRecipient
    {
        public RestCollection<Register> Registers { get; set; }


        private Register selectedRegister;

        public Register SelectedRegister
        {
            get { return selectedRegister; }
            set
            {
                if (value == null)
                {
                    return;
                }
                selectedRegister = new Register()
                {
                    OwnerId = value.OwnerId,
                    Firearm = value.Firearm,
                    FirearmId = value.FirearmId,
                    Id = value.Id,
                    Owner = value.Owner,
                    RegistrationDate = value.RegistrationDate,
                };
                OnPropertyChanged();
                (DeleteRegisterCommand as RelayCommand).NotifyCanExecuteChanged();
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

        public ICommand CreateRegisterCommand { get; set; }
        public ICommand DeleteRegisterCommand { get; set; }
        public ICommand UpdateRegisterCommand { get; set; }

        public RegisterWindowViewModel()
        {
            if (IsIndesignMode)
            {
                return;
            }
            string server = "http://localhost:27031/";

            Registers = new RestCollection<Register>(server, "register", "hub");
            CreateRegisterCommand = new RelayCommand(() =>
            {
                Registers.Add(new Register()
                {
                    OwnerId = SelectedRegister.OwnerId,
                    FirearmId = SelectedRegister.FirearmId,
                });
            });
            UpdateRegisterCommand = new RelayCommand(() =>
            {
                Registers.Update(SelectedRegister);
            });
            DeleteRegisterCommand = new RelayCommand(() =>
            {
                Registers.Delete(SelectedRegister.Id
                    );
            },
            () =>
            {
                return SelectedRegister != null;
            });

            SelectedRegister = new Register();

        }
    }
}
