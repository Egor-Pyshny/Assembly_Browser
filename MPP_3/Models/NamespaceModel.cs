using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyExplorer.Models
{
    public class NamespaceModel
    {
        public string name { get; }
        public List<ClassModel> classes { get; }

        public NamespaceModel(string name)
        { 
            this.name = name;
            this.classes = new List<ClassModel>();
        }

        public NamespaceModel(string name, List<ClassModel> classes)
        {
            this.name = name;
            this.classes = classes;
        }

        public void AddClass(Type _class)
        {
            this.classes.Add(new ClassModel(_class));
        }
    }
}
