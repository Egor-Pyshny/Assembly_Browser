using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyExplorer.Models
{
    public class ClassModel
    {
        private Type _class;
        public List<MethodModel> methods;
        public List<string> methodsS = new List<string>();
        public List<string> fields;
        public List<string> properties;

        public ClassModel(Type _class)
        {
            this._class = _class;
            FillMethods();
            FillFields();
            FillProperties();
        }

        private void FillMethods()
        {
            var _methods = this._class.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Static);
            foreach (MethodInfo method in _methods)
            {
                //methods.Add(new MethodModel(method));
                if (!method.Attributes.HasFlag(MethodAttributes.SpecialName))
                {
                    var s = new MethodModel(method);
                    if (method.IsGenericMethod) { 
                        var asd = method.GetGenericArguments();
                        var sdf = method.GetGenericMethodDefinition();
                    }
                    methodsS.Add(s.ToString());
                }
            }
        }

        private void FillFields()
        {
            var fields = this._class.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                var s = new FieldModel(field);
                methodsS.Add(s.ToString());
            }
        }

        private void FillProperties()
        {

        }
    }
}
