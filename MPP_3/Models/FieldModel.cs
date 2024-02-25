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
        public FieldInfo field;

        public FieldModel(FieldInfo field)
        {
            this.field = field;
        }

        public override string ToString()
        {
            string res = "";
            if (this.field.Attributes.HasFlag(FieldAttributes.FamORAssem))
            {
                res += "protected internal ";
            }
            else if (this.field.Attributes.HasFlag(FieldAttributes.Public))
            {
                res += "public ";
            }
            else if (this.field.Attributes.HasFlag(FieldAttributes.Family))
            {

                res += "protected ";
            }
            else if (this.field.Attributes.HasFlag(FieldAttributes.Assembly))
            {

                res += "internal ";
            }
            else if (this.field.Attributes.HasFlag(FieldAttributes.Private))
            {
                res += "private ";
            }
            else if (this.field.Attributes.HasFlag(FieldAttributes.FamANDAssem))
            {
                res += "private protected ";
            }
            res += this.field.Name;
            res += " : ";
            if (this.field.FieldType.IsGenericType)
                res += CreateGenericTypeString(this.field.FieldType);
            else
                res += this.field.FieldType.Name;
            return res;
        }

        private string CreateGenericTypeString(Type type) {
            string res = "";
            int len = type.Name.ElementAt(type.Name.IndexOf('`')+1) - '0';
            var a = type.GetGenericTypeDefinition();
            if (a != null)
            {
                res += a.Name;
                Type[] t = type.GenericTypeArguments;
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
