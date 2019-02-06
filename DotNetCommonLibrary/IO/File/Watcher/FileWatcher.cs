using System;
using System.IO;

namespace DotNetCommonLibrary.IO.File.Watcher
{
    public class FileWatcher
    {
        readonly FileSystemWatcher watcher;

        public delegate void FileWatcherEvent(FileWatcherEventArgs e);

        public event FileWatcherEvent AllChanged;

        public event FileWatcherEvent Changed;

        public event FileWatcherEvent Created;

        public event FileWatcherEvent Deleted;

        public event FileWatcherEvent Renamed;

        public event FileWatcherEvent Error;

        public FileWatcher(string path,
            string filter = "*.*",
            NotifyFilters filters = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastAccess | NotifyFilters.CreationTime,
            bool includeSubdirectories = true)
        {
            watcher = new FileSystemWatcher(path, filter);

            watcher.Changed += ChangedEventHandler;
            watcher.Created += CreatedEventHandler;
            watcher.Deleted += DeletedEventHandler;
            watcher.Renamed += RenamedEventHandler;
            watcher.Error += ErrorEventHandeler;

            watcher.NotifyFilter = filters;
            watcher.IncludeSubdirectories = includeSubdirectories;
        }

        public void Start()
        {
            watcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
        }

        /// <summary>
        /// Multithreading
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="e"></param>
        void EventHandler(FileWatcherEvent handler, EventArgs e)
        {
            var fileWatcherEventArgs = new FileWatcherEventArgs(e);
            handler?.Invoke(fileWatcherEventArgs);
            AllChanged?.Invoke(fileWatcherEventArgs);
        }

        void ChangedEventHandler(object source, FileSystemEventArgs e)
        {
            EventHandler(Changed, e);
        }

        void CreatedEventHandler(object source, FileSystemEventArgs e)
        {
            EventHandler(Created, e);
        }

        void DeletedEventHandler(object source, FileSystemEventArgs e)
        {
            EventHandler(Deleted, e);
        }

        void RenamedEventHandler(object source, RenamedEventArgs e)
        {
            EventHandler(Renamed, e);
        }

        void ErrorEventHandeler(object source, ErrorEventArgs e)
        {
            EventHandler(Error, e);
        }
    }
}
