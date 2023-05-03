namespace DonutEngine.Backbone.Systems.SceneManager;
public sealed class MissingSceneException : Exception
{
    public MissingSceneException() { }
    public MissingSceneException(string message) : base(message) { }
    public MissingSceneException(string message, Exception inner) : base(message, inner) { }
}