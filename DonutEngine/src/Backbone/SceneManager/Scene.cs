namespace DonutEngine.Backbone.Systems.SceneManager;

public interface Scene
{
    void Init(SceneManager SM);
    
    void Enter();
    
    void Update(float deltaTime);
    void DrawUpdate();
    void Exit();
}