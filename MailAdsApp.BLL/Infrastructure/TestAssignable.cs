using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAdsApp.BLL.Infrastructure
{
    public class TestAssignable : IEqualityComparer<Type>
    {
        public bool Equals(Type x, Type y)
        {
            return x.IsAssignableFrom(y);
        }
        public int GetHashCode(Type obj)
        {
            return obj.GetHashCode();
        }
    }
}
