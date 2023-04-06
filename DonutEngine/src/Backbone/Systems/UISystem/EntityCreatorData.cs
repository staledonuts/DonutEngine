using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using ImGuiNET;
using Box2DX.Dynamics;
using DonutEngine.Backbone.Systems.UI;

namespace DonutEngine.Backbone.Systems.UI.EntityCreator;


public partial class EntityCreator : DocumentWindow
{
    static String name = "";
    Vector2 buttonSize = new(100, 20);
    static ComponentBools componentBools = new();
    static EntityData entityData = new();
    static GameCamera2D gameCamera2D = new();
    static PlayerComponent playerComponent = new();
    static SpriteComponent spriteComponent = new();

    private struct ComponentBools
    {
        public bool gameCamera2D;
        public bool playerComponent;
        public bool spriteComponent;
        public bool colorBoxComponent;
    }

    private struct EntityData
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;
        public int bodyType;
        public float Density;
        public float Friction;
        public float Restitution;
    }

    private struct GameCamera2D
    {
        public bool IsActive;
    }

    private struct PlayerComponent
    {

    }

    private struct SpriteComponent
    {
        public SpriteComponent()
        {
            sprite = "";
            animatorName = "";
        }
        public string sprite;
        public Int32 width;
        public Int32 height;
        public string animatorName;
        public Int32 framesPerRow;
        public Int32 rows;
        public Int32 frameRate;
        public bool playInReverse;
        public bool continuous;
        public bool looping;
    }
}