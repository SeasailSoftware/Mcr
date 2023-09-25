using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeNH.Communication
{
    public enum ConnectionWay
    {
        [Description("USB")]
        ByUsb,
        [Description("Bluetooth")]
        ByBluetooth,
    }
}
