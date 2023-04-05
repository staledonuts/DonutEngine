using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using ImGuiNET;
using Box2DX.Dynamics;
using DonutEngine.Backbone.Systems.UI;

namespace DonutEngine.Backbone.Systems.UI.EntityCreator;

public class EntityCreator : DocumentWindow
{
    EntityData entityData = new();
    Vector2 buttonSize = new(100, 20);

    ImGuiInputTextFlags inputFlags = ImGuiInputTextFlags.CharsHexadecimal | ImGuiInputTextFlags.EnterReturnsTrue | ImGuiInputTextFlags.AutoSelectAll | ImGuiInputTextFlags.NoHorizontalScroll | ImGuiInputTextFlags.CallbackAlways;
    public override void Setup()
    {
        //throw new NotImplementedException();
    }

    public override void Show()
    {
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));
        ImGui.SetNextWindowSizeConstraints(new Vector2(400, 400), new Vector2((float)Raylib.GetScreenWidth(), (float)Raylib.GetScreenHeight()));
        
        if (ImGui.Begin("Entity Creator", ref Open, ImGuiWindowFlags.MenuBar))
        {
            DoMenu();
            Focused = ImGui.IsWindowFocused(ImGuiFocusedFlags.RootAndChildWindows);
            Vector2 size = ImGui.GetContentRegionAvail();
            Vector2 width = new(ImGui.GetColumnWidth(), ImGui.GetContentRegionAvail().Y);
            EntityWindow();
            ImGui.End();
        }
        ImGui.PopStyleVar();
    }
    public override void Shutdown()
    {
        //throw new NotImplementedException();
    }

    public override void Update()
    {
        if (!Open)
        {
            return;
        }
    }

    private void DoMenu()
    {
        if(ImGui.BeginMenuBar())
        {
            if(ImGui.BeginMenu("File"))
            {
                if(ImGui.MenuItem("Reload Entities", "F5"))
                {
                    DonutSystems.entityManager.ReloadEntities();
                }
                if(ImGui.MenuItem("Save As"))
                {
                    
                }
                ImGui.EndMenu();
            }
            ImGui.EndMenuBar();
        }
    }

    private void EntityWindow()
    {
        ImGui.InputFloat(":X", ref entityData.X);
        ImGui.NewLine();
        ImGui.InputFloat(":Y", ref entityData.Y);
        ImGui.NewLine();
        ImGui.SliderFloat(":Collision Width", ref entityData.Width, 0, 100, entityData.Width.ToString("0.000"));
        ImGui.NewLine();
        ImGui.SliderFloat(":Collision Height", ref entityData.Height, 0, 100, entityData.Height.ToString("0.000"));
        ImGui.NewLine();
        ImGui.CheckboxFlags(":Dynamic", ref entityData.bodyType, 1);
        ImGui.NewLine();
        ImGui.SliderFloat(":Density", ref entityData.Density, 0, 3, entityData.Density.ToString("0.000"));
        ImGui.NewLine();
        ImGui.SliderFloat(":Friction", ref entityData.Friction, 0, 3, entityData.Friction.ToString("0.000"));
        ImGui.NewLine();
        ImGui.SliderFloat(":Restitution", ref entityData.Restitution, 0, 10, entityData.Restitution.ToString("0.000"));
        //ImGui.NewLine();
        
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
}

