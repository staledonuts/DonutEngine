namespace DonutEngine.Backbone
{
    public class Observer : IObserver
    {
        public Observer(IRuntimeUpdater runtimeUpdater)
        {
            runtimeUpdater.RegisterObserver(this);
        }
        
        public void update()
        {
            
        }
    }
}