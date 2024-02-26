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
        private List<MethodModel> _methods;
        private List<ConstructorModel> _constructors;
        private List<FieldModel> _fields;
        private List<PropertyModel> _properties;

        public string name { get; }
        public List<MemberModel> members { get; }

        public ClassModel(Type _class)
        {
            this._class = _class;
            this.name = GetName();
            this._methods = new List<MethodModel>();
            this._constructors = new List<ConstructorModel>();
            this._fields = new List<FieldModel>();
            this._properties = new List<PropertyModel>();
            this.members = new List<MemberModel>();
            FillConstructors();
            FillMethods();
            FillFields();
            FillProperties();
            FillMembers();
        }
        private string GetName()
        {
            string res = _class.Name;
            Type[] types = [];
            if (_class.IsGenericType)
                types = _class.GetGenericArguments();
            res = res.Replace($"`{types.Length}","");
            for (int i = 0; i < types.Length; i++)
            {
                if (i == 0) res += "<";
                res += types[i].Name;
                if (i == types.Length - 1)
                    res += ">";
                else
                    res += ", ";
            }
            return res;
        }

        private void FillConstructors()
        {
            var __constructors = this._class.GetConstructors().ToList();
            foreach (ConstructorInfo constructor in __constructors)
            {
                var c = new ConstructorModel(constructor);
                _constructors.Add(c);
            }
        }

        private void FillMembers()
        {
            foreach(var s in this._methods) members.Add(new MemberModel(s.ToString()));
            foreach(var s in this._fields) members.Add(new MemberModel(s.ToString()));
            foreach(var s in this._properties) members.Add(new MemberModel(s.ToString()));
            foreach(var s in this._constructors) members.Add(new MemberModel(s.ToString()));
        }

        private void FillMethods()
        {
            var __methods = this._class.GetMethods(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Static);
            foreach (MethodInfo method in __methods)
            {
                //methods.Add(new MethodModel(method));
                if (!method.Attributes.HasFlag(MethodAttributes.SpecialName))
                {
                    var m = new MethodModel(method);
                    _methods.Add(m);
                }
            }
        }

        private void FillFields()
        {
            var __fields = this._class.GetFields(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo field in __fields)
            {
                if (!field.Name.Contains(">k__BackingField"))
                {
                    var f = new FieldModel(field);
                    _fields.Add(f);
                }
            }
        }

        private void FillProperties()
        {
            var __properties = this._class.GetProperties(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in __properties)
            {
                var p = new PropertyModel(property);
                _properties.Add(p);
            }
        }
    }
}
