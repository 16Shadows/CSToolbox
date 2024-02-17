using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CSToolbox
{
    public static class AssertEx
    {
        public static void IsArgumentNull<T>(T argument, [CallerArgumentExpression(nameof(argument))]string name = "")
        {
            if (argument == null)
                throw new ArgumentNullException(name);
        }
    }
}
