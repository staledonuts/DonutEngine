using System;
using System.Collections.Generic;
namespace DonutEngine.Backbone
{
    public class RuntimeUpdater : IRuntimeUpdater
    {
        private static RuntimeUpdater _current;
        public static RuntimeUpdater current
        {
            get
            {
                if(_current == null)
                {
                    _current = new RuntimeUpdater();
                }
                return _current;
            }
            set
            {
                if(value != null)
                {
                    _current = value;
                }
            }
        }

        private List<IObserver> observers = new List<IObserver>();
        public RuntimeUpdater()
        {
            
        }
        public void RegisterObserver(IObserver observer)
        {
            observers.Add(observer);
        }
        public void AddObservers(IObserver observer)
        {
            observers.Add(observer);
        }
        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }
        public void UpdateObservers()
        {
            foreach (IObserver observer in observers)
            {
                observer.update();
            }
        }
    }
}

