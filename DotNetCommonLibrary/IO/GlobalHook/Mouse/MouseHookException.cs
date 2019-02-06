using System;

namespace DotNetCommonLibrary.IO.GlobalHook.Mouse
{
    public class MouseHookException : Exception
    {
        public MouseHookException()
        {
        }

        public MouseHookException(string message)
            : base(message)
        {
        }

        public MouseHookException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
