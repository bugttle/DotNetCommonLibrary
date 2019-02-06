using System;
using System.Runtime.InteropServices;

namespace DotNetCommonLibrary.Native.Kernel
{
    internal partial class NativeMethods
    {
        const string KERNEL32_DLL = "kernel32.dll";

        [DllImport(KERNEL32_DLL, CharSet = CharSet.Unicode)]
        internal static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
