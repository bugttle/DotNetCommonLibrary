using System;
using System.Threading;

namespace DotNetCommonLibrary.Threading
{
    public class MutexLock : IDisposable
    {
        Mutex mutex;

        public MutexLock(bool initiallyOwned, string name)
        {
            try
            {
                mutex = new Mutex(initiallyOwned, name, out var createdNew);
                if (!createdNew)
                {
                    throw new InvalidOperationException("Failed to get a lock.");
                }
            }
            catch (AbandonedMutexException)
            {
                // it is just another thread has abandoned by exiting without releasing it
            }
        }

        public MutexLock(string name)
            : this(true, name)
        {
        }

        public void Dispose()
        {
            if (mutex != null)
            {
                try { mutex.ReleaseMutex(); } catch { }
                try { mutex.Close(); } catch { }
                try { mutex.Dispose(); } catch { }
                mutex = null;
            }
        }
    }
}
