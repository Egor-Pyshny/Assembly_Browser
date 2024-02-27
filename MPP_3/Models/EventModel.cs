using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MPP_3.Models
{
    public class EventModel
    {
        internal EventInfo _event;

        public EventModel(EventInfo eventModel)
        { 
            this._event = eventModel;
        }

        public override string ToString()
        {
            string res = "";
            res += SetModifier(_event.Attributes);
            res += _event.Name;
            res += SetProps(_event);
            return res;
        }

        private string SetModifier(EventAttributes attributes)
        {
            string res = "";

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
                res += "private";
            }
            else if (attributes.HasFlag(MethodAttributes.FamANDAssem))
            {
                res += "private protected ";
            }
            return res;
        }

        private string SetProps(EventInfo ev)
        {
            string res = "";
            res += " {";
            res += SetAdd(ev.AddMethod);
            res += SetRemove(ev.RemoveMethod);
            res += "}";
            return res;
        }

        private string SetRemove(MethodInfo? removeMethod)
        {
            string res = "";

            return res;
        }

        private string SetAdd(MethodInfo? addMethod)
        {
            string res = "";

            return res;
        }
    }
}
