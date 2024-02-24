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
        public MethodAttributes attributes;
        public string name;
        public ParameterInfo[] parameters;
        public Type returnType;

        public MethodModel(MethodInfo method)
        {
            this.attributes = method.Attributes;
            this.name = method.Name;
            this.parameters = method.GetParameters();
            this.returnType = method.ReturnType;
        }

        public override string ToString()
        {
            string res = "";
            if (this.attributes.HasFlag(MethodAttributes.FamORAssem))
            {
                res += "protected internal ";
            }
            else if (this.attributes.HasFlag(MethodAttributes.Public))
            {
                res += "public ";
            }
            else if (this.attributes.HasFlag(MethodAttributes.Family))
            {

                    res += "protected ";
            }
            else if (this.attributes.HasFlag(MethodAttributes.Assembly))
            {

                    res += "internal ";
            }
            else if (this.attributes.HasFlag(MethodAttributes.Private))
            {
                res += "private ";
            }
            else if (this.attributes.HasFlag(MethodAttributes.FamANDAssem))
            {
                res += "private protected ";
            }
            res += this.name;
            int len = parameters.Length;
            if (len == 0) res += "()";
            for (int i = 0; i < len; i++) {
                if(i==0) res += "(";
                var param = this.parameters[i];
                res += param.ParameterType.Name;
                res += " ";
                res += param.Name;
                if (i == len - 1)
                    res += ")";
                else
                    res += ", ";
            }
            res += " : ";
            res += returnType.Name;
            return res;
        }
    }
}
