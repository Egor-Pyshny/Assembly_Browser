using MPP_3.Models;
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
        public string name;
        private Type _class;
        private List<MethodModel> _methods;
        private List<ConstructorModel> _constructors;
        private List<FieldModel> _fields;
        private List<PropertyModel> _properties;
        public List<string> methods;
        public List<string> constructors;
        public List<string> fields;
        public List<string> properties;
        public List<string> members;

        public ClassModel(Type _class)
        {
            this.name = _class.Name;
            this._class = _class;
            this._methods = new List<MethodModel>();
            this._constructors = new List<ConstructorModel>();
            this._fields = new List<FieldModel>();
            this._properties = new List<PropertyModel>();
            this.methods = new List<string>();
            this.constructors = new List<string>();
            this.fields = new List<string>();
            this.properties = new List<string>();
            this.members = new List<string>();
            FillConstructors();
            FillMethods();
            FillFields();
            FillProperties();
            FillMembers();
        }

        private void FillConstructors()
        {
            var __constructors = this._class.GetConstructors().ToList();
            foreach (ConstructorInfo constructor in __constructors)
            {
                var c = new ConstructorModel(constructor);
                _constructors.Add(c);
                constructors.Add(c.ToString());
            }
        }

        private void FillMembers()
        {
            foreach(var s in this.members) members.Add(s);
            foreach(var s in this.fields) members.Add(s);
            foreach(var s in this.properties) members.Add(s);
            foreach(var s in this.constructors) members.Add(s);
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
                    methods.Add(m.ToString());
                }
            }
        }

        private void FillFields()
        {
            var __fields = this._class.GetFields(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo field in __fields)
            {
                FieldModel s;
                if (!field.Name.Contains(">k__BackingField"))
                {
                    var f = new FieldModel(field);
                    _fields.Add(f);
                    fields.Add(f.ToString());
                }
            }
        }

        private void FillProperties()
        {
            var __properties = this._class.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in __properties)
            {
                var p = new PropertyModel(property);
                _properties.Add(p);
                properties.Add(p.ToString());
            }
        }
    }
}
