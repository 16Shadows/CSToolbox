using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSToolbox
{
	public delegate void TypedEventHandler<TSender>(TSender sender);
	public delegate void TypedEventHandler<TSender, TArgs>(TSender sender, TArgs args);
}
