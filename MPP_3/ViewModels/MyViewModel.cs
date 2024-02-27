using AssemblyExplorer.Models;
using MPP_3.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MPP_3.ViewModel
{
    public class MyViewModel : INotifyPropertyChanged
    {
        private AssemblyModel selectedAssembly;

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      string path = obj as string ?? throw new NullReferenceException("Path is null");
                      Assembly a = Assembly.LoadFrom(path);
                      AssemblyModel assembly = new AssemblyModel(a);
                      if(this.Assemblies.Where(asm => asm.name == assembly.name).ToList().Count==0)
                          this.Assemblies.Add(assembly);
                  }));
            }
        }

        public List<AssemblyModel> Assemblies { get; }

        public MyViewModel()
        {
            this.Assemblies = new List<AssemblyModel>();
        }

        public void add(AssemblyModel a) { this.Assemblies.Add(a); }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

}
