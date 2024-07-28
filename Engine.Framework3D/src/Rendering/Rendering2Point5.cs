using Engine.Entities;
using Engine.Framework3D.Data;
using Engine.Systems;
using Engine.Transformations;
using Raylib_CSharp.Rendering;

namespace Engine.Framework3D.RenderSystems;

public class Rendering2Point5 : SystemClass, IUpdateSys, IDrawUpdateSys, ILateUpdateSys
{
    
    Dictionary<Guid, IRenderData> _renderDatas = new();
    Queue<IRenderData> _renderQueue = new();

    public void AddEntity(IEntity entity, IRenderData renderData)
    {
        _renderDatas.Add(entity.GetGuid(), renderData);
    }

    public void UpdateSpriteData(Guid guid, Transform2D transform)
    {
        if(_renderDatas.TryGetValue(guid, out IRenderData data))
        {
            data.UpdateData(transform);
        }
    }
    
    public override void Initialize()
    {
        EngineSystems.AddSystem(new Cam3D());
    }

    public override void Shutdown()
    {
        EngineSystems.RemoveSystem<Cam3D>();
    }

    public void DrawUpdate()
    {
        Graphics.BeginMode3D(EngineSystems.GetSystem<Cam3D>().GetCamera());
        foreach(IRenderData rd in _renderQueue)
        {
            rd.RenderMe();
        }
        Graphics.EndMode3D();
    }

    public void LateUpdate()
    {
        
    }

    public void Update()
    {
        _renderQueue.TrimExcess();
        foreach(KeyValuePair<Guid, IRenderData> rd in _renderDatas)
        {
            _renderQueue.Enqueue(rd.Value);
        }
    }
}