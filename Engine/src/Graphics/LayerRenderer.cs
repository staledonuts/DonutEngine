using Engine.Systems;
using Raylib_cs;

namespace Engine;
public static class Rendering2D
{
    static Rendering2D()
    {
        layers = new();
    }

    static Dictionary<int, Layer> layers;

    public static void Render()
    {
        foreach(KeyValuePair<int, Layer> key in layers)
        {
            RenderLayer(key.Value);
        }
    }

    public static void QueueAtLayer(int layer, Action action)
    {
        if(!layers.ContainsKey(layer))
        {
            layers.Add(layer, new());
        }
        layers.GetValueOrDefault(layer).queue.Append(action);
    }

    static void RenderLayer(Layer layer)
    {
        layer.queue.TrimExcess();
        int length = layer.queue.Count;
        for (int i = 0; i < length; i++)
        {
            Action action;
            action = layer.queue.Dequeue();
            action.Invoke();
        }
    }
}

internal struct Layer
{
    public Layer()
    {
        queue = new();
    }

    public Queue<Action> queue;

}
