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
        public MainWindowViewModel() 
        {
            OpenFirearmCommand = new RelayCommand(() =>
            {
                FirearmWindow fw = new FirearmWindow();
                fw.Show();
            });
        }
    }
}
