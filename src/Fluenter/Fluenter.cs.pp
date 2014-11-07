using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace $rootnamespace$.Fluenter
{
    public class Fluenter<T> : DynamicObject
    {
        public static dynamic Get(params object[] args)
        {
            return new Fluenter<T>(args);
        }

        private readonly T instance;

        public Fluenter(params object[] args)
        {
            instance = (T)Activator.CreateInstance(typeof(T), args);
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return typeof(T).GetMethods().Select(x => x.Name);
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            if (binder.Type.IsAssignableFrom(typeof(T)))
            {
                result = instance;
                return true;
            }
            return base.TryConvert(binder, out result);
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var method = typeof(T).GetMethod(binder.Name);
            if (method != null)
            {
                method.Invoke(instance, args);
                result = this;
                return true;
            }
            return base.TryInvokeMember(binder, args, out result);
        }
    }
}
