using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpInform
{
    public interface IIP
    {
        double numberNetworkNodes { get; internal set; }
        string netAddress { get; internal set; }
        string networkMask { get; internal set; }
        string addressFirstNode { get; internal set; }
        string lastnameAddress { get; internal set; }
        string broadcast { get; internal set; }

        void Print();
    }
}
