namespace DonutEngine.Backbone
{
    public interface IRuntimeUpdater
    {
         void RegisterObserver(IObserver observer);
         void RemoveObserver(IObserver observer);
         void UpdateObservers();
    }
}