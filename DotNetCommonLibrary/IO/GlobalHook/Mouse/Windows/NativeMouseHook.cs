using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DotNetCommonLibrary.IO.GlobalHook.Mouse.Windows
{
    internal static class NativeMouseHook
    {
        internal delegate void HookHandler(ref MouseState state);

        internal static HookHandler HookCallback;

        internal static MouseState MouseState;

        internal static bool IsHooking
        {
            get { return NativeHookHandle != IntPtr.Zero; }
        }

        static IntPtr NativeHookHandle;

        static event Native.WinUser.NativeMethods.HookProc NativeHookProc;

        internal static void Start()
        {
            if (IsHooking)
            {
                return;
            }

            NativeHookProc = HookProcedure; // To avoide release the callback

            using (var p = Process.GetCurrentProcess())
            {
                using (var module = p.MainModule)
                {
                    NativeHookHandle = Native.WinUser.NativeMethods.SetWindowsHookEx(
                        Native.WinUser.HookType.WH_MOUSE_LL,
                        NativeHookProc,
                        Native.Kernel.NativeMethods.GetModuleHandle(module.ModuleName),
                        0);
                }
            }

            if (NativeHookHandle == IntPtr.Zero)
            {
                throw new System.ComponentModel.Win32Exception();
            }
        }

        internal static void Stop()
        {
            if (!IsHooking)
            {
                return;
            }

            HookCallback = null;
            NativeHookProc = null;
            NativeHookHandle = IntPtr.Zero;
            Native.WinUser.NativeMethods.UnhookWindowsHookEx(NativeHookHandle);
        }

        static IntPtr HookProcedure(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (0 <= nCode && HookCallback != null)
            {
                var msg = (Native.WinUser.WindowMessages)wParam;
                var s = (Native.WinUser.MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(Native.WinUser.MSLLHOOKSTRUCT));

                MouseState.button = GetButtonState(msg, ref s);
                MouseState.x = s.pt.x;
                MouseState.y = s.pt.y;
                MouseState.time = s.time;

                HookCallback(ref MouseState);
            }

            return Native.WinUser.NativeMethods.CallNextHookEx(NativeHookHandle, nCode, wParam, lParam);
        }

        static ButtonType GetButtonState(Native.WinUser.WindowMessages msg, ref Native.WinUser.MSLLHOOKSTRUCT s)
        {
            switch (msg)
            {
                case Native.WinUser.WindowMessages.WM_MOUSEMOVE:
                    return ButtonType.MOVE;

                case Native.WinUser.WindowMessages.WM_LBUTTONDOWN:
                    return ButtonType.L_DOWN;

                case Native.WinUser.WindowMessages.WM_LBUTTONUP:
                    return ButtonType.L_UP;

                case Native.WinUser.WindowMessages.WM_LBUTTONDBLCLK:
                    return ButtonType.UNKNOWN;

                case Native.WinUser.WindowMessages.WM_RBUTTONDOWN:
                    return ButtonType.R_DOWN;

                case Native.WinUser.WindowMessages.WM_RBUTTONUP:
                    return ButtonType.R_UP;

                case Native.WinUser.WindowMessages.WM_MBUTTONDOWN:
                    return ButtonType.M_DOWN;

                case Native.WinUser.WindowMessages.WM_MBUTTONUP:
                    return ButtonType.M_UP;

                case Native.WinUser.WindowMessages.WM_MOUSEWHEEL:
                    return ((short)((s.mouseData >> 16) & 0xffff) > 0) ? ButtonType.W_UP : ButtonType.W_DOWN;

                case Native.WinUser.WindowMessages.WM_XBUTTONDOWN:
                    switch (s.mouseData >> 16)
                    {
                        case 1:
                            return ButtonType.X1_DOWN;

                        case 2:
                            return ButtonType.X2_DOWN;

                        default:
                            return ButtonType.UNKNOWN;
                    }
                case Native.WinUser.WindowMessages.WM_XBUTTONUP:
                    switch (s.mouseData >> 16)
                    {
                        case 1:
                            return ButtonType.X1_UP;

                        case 2:
                            return ButtonType.X2_UP;

                        default:
                            return ButtonType.UNKNOWN;
                    }
                default:
                    return ButtonType.UNKNOWN;
            }
        }
    }
}
