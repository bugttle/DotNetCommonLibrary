using System;
using System.Runtime.InteropServices;
using DotNetCommonLibrary.Native.WinDef;

namespace DotNetCommonLibrary.Native.WinUser
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct MSLLHOOKSTRUCT
    {
        internal POINT pt;
        internal uint mouseData;
        internal uint flags;
        internal uint time;
        internal UIntPtr dwExtraInfo;
    }
}
