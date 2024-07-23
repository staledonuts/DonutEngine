using Engine.Assets;
using Engine.Systems;

using Engine.RenderSystems;
using System.Numerics;
using Engine.Enums;
using Engine.Systems.UI.Skeleton;
using Raylib_CSharp.Textures;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Transformations;
using Engine.Framework3D;
using Engine.Entities;
using Engine.Framework3D.Data;

namespace Engine;

public static class Draw3D
{
    public static void AddSpriteRenderer(Entity entity,IRenderData renderData)
    {
        EngineSystems.GetSystem<Rendering2Point5>().AddEntity(entity, renderData);
    }

    public static void UpdateSpriteData(Guid guid, Transform transform)
    {
        EngineSystems.GetSystem<Rendering2Point5>().UpdateSpriteData(guid, transform);
    }

    

}