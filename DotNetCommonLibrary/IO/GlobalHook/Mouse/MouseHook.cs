using System;

namespace DotNetCommonLibrary.IO.GlobalHook.Mouse
{
    public static class MouseHook
    {
        public delegate void HookHandler(ref MouseState state);

        public static event HookHandler HookEvent;

        public static bool IsHooking
        {
            get { return Windows.NativeMouseHook.IsHooking; }
        }

        public static MouseState MouseState
        {
            get { return Windows.NativeMouseHook.MouseState; }
        }

        public static void Start()
        {
            if (IsHooking)
            {
                return;
            }

            try
            {
                Windows.NativeMouseHook.HookCallback = HookCallback;
                Windows.NativeMouseHook.Start();
            }
            catch (Exception e)
            {
                throw new MouseHookException("failed to start", e);
            }
        }

        public static void Stop()
        {
            if (!IsHooking)
            {
                return;
            }

            Windows.NativeMouseHook.HookCallback = null;
            Windows.NativeMouseHook.Stop();
        }

        static void HookCallback(ref MouseState state)
        {
            HookEvent?.Invoke(ref state);
        }
    }
}
