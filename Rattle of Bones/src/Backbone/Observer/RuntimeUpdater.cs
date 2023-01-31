using System;
using System.Collections.Generic;
namespace DonutEngine.Backbone
{
    public sealed class RuntimeUpdater : IRuntimeUpdater
    {
        private static readonly object padlock = new object();
        private static RuntimeUpdater _current;

        private List<DonutBehaviour> observers = new List<DonutBehaviour>();
        public static RuntimeUpdater current
        {
            get
            {
                lock (padlock)
                {
                    if (_current == null)
                    {
                        _current = new RuntimeUpdater();
                    }
                    return _current;
                }
            }
        }
        public void AddObservers(DonutBehaviour observer)
        {
            observers.Add(observer);
        }
        public void RegisterObserver(DonutBehaviour observer)
        {
            observers.Add(observer);
        }
        public void RemoveObserver(DonutBehaviour observer)
        {
            observers.Remove(observer);
        }
        public void UpdateRuntimeObservers()
        {
            foreach (DonutBehaviour observer in observers)
            {
                observer.update();
            }
        }
    }
}

