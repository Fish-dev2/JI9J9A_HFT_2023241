using JI9J9A_HFT_2023241.WpfClient.Windows;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JI9J9A_HFT_2023241.WpfClient.ViewModels
{
    internal class MainWindowViewModel
    {

        public ICommand OpenFirearmCommand { get; set; }
        public ICommand OpenAmmoCommand { get; set; }
        public ICommand OpenOwnerCommand { get; set; }
        public ICommand OpenRegisterCommand { get; set; }
        public MainWindowViewModel() 
        {
            OpenFirearmCommand = new RelayCommand(() =>
            {
                FirearmWindow fw = new FirearmWindow();
                fw.Show();
            });
            OpenOwnerCommand = new RelayCommand(() =>
            {
                OwnerWindow ow = new OwnerWindow();
                ow.Show();
            });
            OpenAmmoCommand = new RelayCommand(() =>
            {
                AmmoWindow mw = new AmmoWindow();
                mw.Show();
            });
            OpenRegisterCommand = new RelayCommand(() =>
            {
                RegisterWindow rw = new RegisterWindow();
                rw.Show();
            });
            
        }
    }
}
