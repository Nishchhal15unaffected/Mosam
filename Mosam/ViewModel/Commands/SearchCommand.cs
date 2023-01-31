using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mosam.ViewModel.Commands
{
    public class SearchCommand : ICommand
    {
        public WeatherVm VM { get; set; }
        public SearchCommand(WeatherVm vm) 
        {
            VM = vm;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value;}
        }

        public bool CanExecute(object? parameter)
        {
            string query = parameter as string;
            if (string.IsNullOrEmpty(query))
                return false;
            return true;
        }

        public void Execute(object? parameter)
        {
            VM.MakeQuery();
        }
    }
}
