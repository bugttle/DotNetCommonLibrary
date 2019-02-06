using System.Runtime.InteropServices;

namespace DotNetCommonLibrary.Native.WinDef
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct POINT
    {
        internal int x;
        internal int y;
    }
}
