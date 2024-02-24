using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyExplorer.Models
{
    public class AssemblyModel
    {
        private Assembly assembly;
        public Dictionary<string, NamespaceModel> namespaces;

        public AssemblyModel(Assembly assembly) {
            this.assembly = assembly;
            this.namespaces = new Dictionary<string, NamespaceModel>();
            Type[] types = assembly.GetTypes().Where(t => !t.Name.Contains("<>c")).ToArray(); ;
            foreach (Type type in types)
            {
                string _namespace = type.Namespace == null ? "-" : type.Namespace;
                if (!namespaces.ContainsKey(_namespace)) 
                {
                    var n = new NamespaceModel(_namespace);
                    n.AddClass(type);
                    namespaces.Add(_namespace, n);
                }
                else
                    namespaces[_namespace].AddClass(type);
            }
        }
    }
}
