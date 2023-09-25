namespace ThreeNH.Communication.CommDevice.Bluetooth
{
    public struct BluetoothAddress
    {
        internal BluetoothApis.BluetoothAddress _bthaddr;
        internal BluetoothAddress(BluetoothApis.BluetoothAddress bthaddr)
        {
            _bthaddr = bthaddr;
        }
        public BluetoothAddress(long laddr)
        {
            _bthaddr.addr = laddr;
        }

        public long ToLong() { return _bthaddr.addr; }
    }
}
