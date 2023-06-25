using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSToolbox
{
    public static class AssertEx
    {
        public static void IsArgumentNull<T>(T argument, string name)
        {
            if (argument == null)
                throw new ArgumentNullException(name);
        }
    }
}
