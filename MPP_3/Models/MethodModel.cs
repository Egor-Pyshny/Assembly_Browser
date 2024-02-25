using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyExplorer.Models
{
    public class MethodModel
    {
        public MethodInfo method;


        public MethodModel(MethodInfo method)
        {
            this.method = method;
        }

        public override string ToString()
        {
            string res = "";
            res += SetModifier(this.method.Attributes);
            res += SetKeywords(this.method.Attributes);
            res += this.method.Name;

            var parameters = this.method.GetParameters();
            int len = parameters.Length;
            if (len == 0) res += "()";
            for (int i = 0; i < len; i++) {
                if(i==0) res += "(";
                var param = parameters[i];
                string tmp = param.ParameterType.Name;
                if (param.Attributes.HasFlag(ParameterAttributes.In))
                {
                    tmp = tmp.Replace("&", "");
                    res += "in ";
                    res += tmp;
                }
                else if (param.Attributes.HasFlag(ParameterAttributes.Out))
                {
                    tmp = tmp.Replace("&", "");
                    res += "out ";
                    res += tmp;
                }
                else if (tmp.Contains("&"))
                {
                    tmp = tmp.Replace("&", "");
                    res += "ref ";
                    res += tmp;
                }
                else
                {
                    res += tmp;
                }
                res += " ";
                res += param.Name;
                if (param.Attributes.HasFlag(ParameterAttributes.HasDefault))
                {
                    res += " = ";
                    var def = param.DefaultValue;
                    if(def == null)
                        res += "null";
                    else
                        res += def.ToString();
                }
                if (i == len - 1)
                    res += ")";
                else
                    res += ", ";
            }
            res += " : ";
            res += this.method.ReturnType.Name;
            return res;
        }

        private string SetModifier(MethodAttributes attributes) 
        {
            string res = "";
            if (attributes.HasFlag(MethodAttributes.FamORAssem))
            {
                res += "protected internal ";
            }
            else if (attributes.HasFlag(MethodAttributes.Public))
            {
                res += "public ";
            }
            else if (attributes.HasFlag(MethodAttributes.Family))
            {
                res += "protected ";
            }
            else if (attributes.HasFlag(MethodAttributes.Assembly))
            {
                res += "internal ";
            }
            else if (attributes.HasFlag(MethodAttributes.Private))
            {
                res += "private ";
            }
            else if (attributes.HasFlag(MethodAttributes.FamANDAssem))
            {
                res += "private protected ";
            }
            return res;
        }

        private string SetKeywords(MethodAttributes attributes)
        {
            string res = "";
            if (attributes.HasFlag(MethodAttributes.Static))
            {
                res += "static ";
            }
            else if (attributes.HasFlag(MethodAttributes.Virtual | MethodAttributes.NewSlot))
            {
                res += "virtual ";
            }
            else if (attributes.HasFlag(MethodAttributes.Final | MethodAttributes.Virtual))
            {
                res += "sealed ovveride ";
            }
            else if (attributes.HasFlag(MethodAttributes.Virtual))
            {
                res += "override ";
            }
            else if (attributes.HasFlag(MethodAttributes.Final))
            {
                res += "sealed ";
            }
            return res;
        }
    }
}
