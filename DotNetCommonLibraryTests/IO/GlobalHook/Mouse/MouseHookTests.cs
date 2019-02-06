using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCommonLibrary.IO.GlobalHook.Mouse.Tests
{
    [TestClass]
    public class MouseHookTests
    {
        void MouseHook_HookEvent(ref MouseState state)
        {
            Console.WriteLine(state);
        }

        [TestMethod]
        public void StartTest()
        {
            MouseHook.Start();
        }

        [TestMethod]
        public void StopTest()
        {
            MouseHook.Stop();
        }

        [TestMethod]
        public void BehaviourTest()
        {
            MouseHook.Start();
            MouseHook.HookEvent += MouseHook_HookEvent;
            MouseHook.HookEvent -= MouseHook_HookEvent;
            MouseHook.Stop();
        }
    }
}
