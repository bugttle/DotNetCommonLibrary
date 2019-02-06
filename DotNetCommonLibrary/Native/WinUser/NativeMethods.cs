using System;
using System.Runtime.InteropServices;

namespace DotNetCommonLibrary.Native.WinUser
{
    internal partial class NativeMethods
    {
        const string USER32_DLL = "user32.dll";

        internal delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport(USER32_DLL, SetLastError = true)]
        internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport(USER32_DLL, SetLastError = true)]
        internal static extern IntPtr SetWindowsHookEx(HookType hookType, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport(USER32_DLL, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnhookWindowsHookEx(IntPtr hhk);
    }
}
