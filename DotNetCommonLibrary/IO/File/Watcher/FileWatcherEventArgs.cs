using System;
using System.IO;

namespace DotNetCommonLibrary.IO.File.Watcher
{
    public class FileWatcherEventArgs : EventArgs
    {
        // FileSystemEventArgs
        public WatcherChangeTypes ChangeType { get; }

        public string FullPath { get; }
        public string Name { get; }

        // RenamedEventArgs
        public string OldFullPath { get; }
        public string OldName { get; }

        // ErrorEventArgs
        public Exception Exception { get; }

        public FileWatcherEventArgs(EventArgs e = null)
        {
            if (e != null)
            {
                if (e is FileSystemEventArgs)
                {
                    var fileSystemEvent = e as FileSystemEventArgs;
                    ChangeType = fileSystemEvent.ChangeType;
                    FullPath = fileSystemEvent.FullPath;
                    Name = fileSystemEvent.Name;

                    if (e is RenamedEventArgs) // it's only RenamedEvent
                    {
                        var renamedEvent = e as RenamedEventArgs;
                        OldFullPath = renamedEvent.OldFullPath;
                        OldName = renamedEvent.OldName;
                    }
                }
                else if (e is ErrorEventArgs)
                {
                    var errorEvent = e as ErrorEventArgs;
                    ChangeType = WatcherChangeTypes.All; // error
                    Exception = errorEvent.GetException();
                }
            }
        }
    }
}
