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
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Image = System.Windows.Controls.Image;
using System.Windows.Media.Imaging;

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
                      AssemblyNode assembly = new AssemblyNode(new AssemblyModel(a));
                      if(this.Assemblies.Where(asm => asm.Name == assembly.Name).ToList().Count==0)
                          this.Assemblies.Add(assembly);
                  }));
            }
        }

        public List<INode> Assemblies { get; }

        public MyViewModel()
        {
            this.Assemblies = new List<INode>();
        }

        public void add(AssemblyModel a) { this.Assemblies.Add(new AssemblyNode(a)); }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public interface INode: INotifyPropertyChanged
    {
        public string Name { get; }
    }

    public class AssemblyNode : INode
    {
        public AssemblyNode(AssemblyModel assemly) {
            this.Name = assemly.name;
            this.Items = new List<INode>();
            foreach (var n in assemly.namespaces) {
                Items.Add(new NamespaceNode(n));
            }
        }
        public string Name { get; set; }
        public List<INode> Items { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class NamespaceNode : INode
    {
        public NamespaceNode(NamespaceModel namespaceModel) { 
            this.Name = namespaceModel.name;
            this.Items = new List<INode>();
            foreach (var item in namespaceModel.Classes)
            {
                Items.Add(new ClassNode(item));
            }
        }
        public string Name { get; set; }
        public List<INode> Items { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class ClassNode : INode
    {
        public ClassNode(ClassModel cls) {
            this.Name = cls.name;
            this.Items = new List<INode>();
            if (cls._class.IsEnum)
            {
                ImagePath = "C:\\Users\\user\\Source\\Repos\\Egor-Pyshny\\MPP_3\\MPP_3\\Images\\Enum.png";
            }
            else if (cls._class.IsSubclassOf(typeof(Delegate)))
            { 
                ImagePath = "C:\\Users\\user\\Source\\Repos\\Egor-Pyshny\\MPP_3\\MPP_3\\Images\\Delegate.png"; 
            }
            else if (cls._class.IsValueType)
            {
                ImagePath = "C:\\Users\\user\\Source\\Repos\\Egor-Pyshny\\MPP_3\\MPP_3\\Images\\Struct.png";
            }
            else {
                ImagePath = "C:\\Users\\user\\Source\\Repos\\Egor-Pyshny\\MPP_3\\MPP_3\\Images\\Class.png";
            }

            foreach (var item in cls.innerClasses)
            {
                Items.Add(new ClassNode(item));       
            }
            foreach (var item in cls._properties)
            {
                Items.Add(new PropertyNode(item));
            }
            foreach (var item in cls._fields)
            {
                Items.Add(new FieldNode(item));
            }
            foreach (var item in cls._constructors)
            {
                Items.Add(new ConstructorNode(item));
            }
            foreach (var item in cls._methods)
            {
                Items.Add(new MethodNode(item));
            }
        }
        public List<INode> Items { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class MethodNode : INode
    {
        public MethodNode(MethodModel method)
        { 
            this.Name = method.ToString();
            if (method.extension)
            {
                ImagePath = "C:\\Users\\user\\Source\\Repos\\Egor-Pyshny\\MPP_3\\MPP_3\\Images\\ExtensionMethod.png";
            }
            else {
                ImagePath = "C:\\Users\\user\\Source\\Repos\\Egor-Pyshny\\MPP_3\\MPP_3\\Images\\Method.png";
            }
        }
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class FieldNode : INode
    {
        public FieldNode(FieldModel field)
        {
            this.Name = field.ToString();
            if (field.field.DeclaringType.IsEnum && !field.field.Name.Contains("value__"))
            {
                ImagePath = "C:\\Users\\user\\Source\\Repos\\Egor-Pyshny\\MPP_3\\MPP_3\\Images\\EnumValue.png";
            }
            else
            {
                ImagePath = "C:\\Users\\user\\Source\\Repos\\Egor-Pyshny\\MPP_3\\MPP_3\\Images\\Field.png";
            }
        }

        public string Name { get; set; }
        public string ImagePath { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class ConstructorNode : INode
    {
        public ConstructorNode(ConstructorModel constructor)
        {
            this.Name = constructor.ToString();
        }
        public string Name { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class PropertyNode : INode
    {
        public PropertyNode(PropertyModel property)
        {
            this.Name = property.ToString();
        }
        public string Name { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
