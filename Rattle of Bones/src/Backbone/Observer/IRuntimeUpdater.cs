namespace DonutEngine.Backbone
{
    public interface IRuntimeUpdater
    {
         void RegisterObserver(DonutBehaviour observer);
         void RemoveObserver(DonutBehaviour observer);
         void UpdateRuntimeObservers();
    }
}