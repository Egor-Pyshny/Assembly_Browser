using System.Reflection;

namespace AssemblyExplorer.Models
{
    public class AssemblyModel
    {
        private Assembly assembly;
        private Dictionary<string, NamespaceModel> _namespaces;

        public string name { get; }
        public List<NamespaceModel> namespaces { get; }

        public AssemblyModel(Assembly assembly) {
            this.assembly = assembly;
            this._namespaces = new Dictionary<string, NamespaceModel>();
            Type[] types = assembly.GetTypes().Where(t => !t.Name.Contains("<>c")).ToArray(); ;
            foreach (Type type in types)
            {
                string __namespace = type.Namespace == null ? "-" : type.Namespace;
                if (!_namespaces.ContainsKey(__namespace)) 
                {
                    var n = new NamespaceModel(__namespace);
                    n.AddClass(type);
                    _namespaces.Add(__namespace, n);
                }
                else
                    _namespaces[__namespace].AddClass(type);
            }
            this.namespaces = _namespaces.Values.ToList();
            this.name = assembly.GetName().Name ?? "null";
        }
    }
}
