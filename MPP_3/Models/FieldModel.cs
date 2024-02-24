using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyExplorer.Models
{
    public class FieldModel
    {
        public string name;
        public Type fieldType;
        public FieldAttributes attributes;

        public FieldModel(FieldInfo field)
        {
            this.attributes = field.Attributes;
            this.name = field.Name;
            this.fieldType = field.FieldType;
        }

        public override string ToString()
        {
            string res = "";
            if (this.attributes.HasFlag(FieldAttributes.FamORAssem))
            {
                res += "protected internal ";
            }
            else if (this.attributes.HasFlag(FieldAttributes.Public))
            {
                res += "public ";
            }
            else if (this.attributes.HasFlag(FieldAttributes.Family))
            {

                res += "protected ";
            }
            else if (this.attributes.HasFlag(FieldAttributes.Assembly))
            {

                res += "internal ";
            }
            else if (this.attributes.HasFlag(FieldAttributes.Private))
            {
                res += "private ";
            }
            else if (this.attributes.HasFlag(FieldAttributes.FamANDAssem))
            {
                res += "private protected ";
            }
            res += this.name;
            res += " : ";
            res += this.fieldType.Name;

            if (this.fieldType.IsGenericType)
            {
                res += CreateGenericTypeString(this.fieldType);
            }
            return res;
        }

        private string CreateGenericTypeString(Type type) {
            string res = "";
            int len = type.Name.ElementAt(type.Name.IndexOf('`')+1) - '0';
            var a = this.fieldType.GetGenericTypeDefinition();
            if (a != null)
            {
                res += a.Name;
                Type[] t = fieldType.GenericTypeArguments;
                while (res.Contains("`"))
                {
                    string s = "";
                    for (int i = 0; i <len; i++) {
                        if (i == 0)
                            s = $"<{t[i].Name}";
                        else
                            s += $", {t[i].Name}";
                    }
                    s += ">";
                    res = res.Replace($"`{len}", s);
                    for (int i = 0; i < len; i++)
                    {
                        if (t[i].IsGenericType)
                            t[i] = t[i].GenericTypeArguments[i];
                    }
                }
            }
            return res;
        }
    }
}
