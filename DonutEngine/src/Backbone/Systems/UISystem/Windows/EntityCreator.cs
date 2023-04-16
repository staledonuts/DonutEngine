using System.Numerics;
using Raylib_cs;
using ImGuiNET;
using Box2DX.Dynamics;

namespace DonutEngine.Backbone.Systems.UI.Creator;

public partial class EntityCreator : DocumentWindow
{

    public override void Setup()
    {
        
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
            EntityWindow();
            ImGui.End();
        }
        ImGui.PopStyleVar();
    }
    public override void Shutdown()
    {
        
    }

    public override void DrawUpdate()
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
                if(ImGui.MenuItem(" - Reset Entity"))
                {
                    ResetEntityWindow();
                }
                if(ImGui.MenuItem(" - Save As"))
                {
                    SaveJson(entityData.Name);
                }
                if(ImGui.MenuItem(" - Reload Game Entities", "F5"))
                {
                    Sys.entityManager.ReloadEntities();
                }
                ImGui.EndMenu();
            }
            if(ImGui.BeginMenu("Components"))
            {
                if(ImGui.MenuItem(" - GameCamera 2D Component", "",  ref componentBools.gameCamera2D)){}
                if(ImGui.MenuItem(" - Player Component", "",  ref componentBools.playerComponent)){}
                if(ImGui.MenuItem(" - Sprite Component", "",  ref componentBools.spriteComponent)){}
                //if(ImGui.MenuItem("Particle Component", "",  ref componentBools.particleComponent)){}
                ImGui.EndMenu();
            }
            ImGui.EndMenuBar();
        }
    }

    public void ResetEntityWindow()
    {
        componentBools = new();
        entityData = new();
        gameCamera2D = new();
        playerComponent = new();
        spriteComponent = new();
    }


    private void EntityWindow()
    {
        if(entityData.Name == null)
        {
            entityData.Name = "";
        }
        ImGui.InputTextWithHint("Entity Filename", "Input Filename: will be saved as a Json", ref entityData.Name, 32, ImGuiInputTextFlags.AutoSelectAll);
        if(ImGui.CollapsingHeader("Physics", ImGuiTreeNodeFlags.SpanAvailWidth))
        {
            ImGui.InputFloat("X", ref entityData.X);
            if(ImGui.IsItemHovered())
            {
                ImGui.BeginTooltip();
                ImGui.SetTooltip("Position in the world");
                ImGui.EndTooltip();
            }
            ImGui.InputFloat("Y", ref entityData.Y);
            if(ImGui.IsItemHovered())
            {
                ImGui.BeginTooltip();
                ImGui.SetTooltip("Position in the world");
                ImGui.EndTooltip();
            }
            ImGui.SliderFloat("Collision Width", ref entityData.Width, 0, 100, entityData.Width.ToString("0.000"));
            if(ImGui.IsItemHovered())
            {
                ImGui.BeginTooltip();
                ImGui.SetTooltip("Rectangular Collision Size");
                ImGui.EndTooltip();
            }
            ImGui.SliderFloat("Collision Height", ref entityData.Height, 0, 100, entityData.Height.ToString("0.000"));
            if(ImGui.IsItemHovered())
            {
                ImGui.BeginTooltip();
                ImGui.SetTooltip("Rectangular Collision Size");
                ImGui.EndTooltip();
            }
            ImGui.CheckboxFlags("Dynamic", ref entityData.bodyType, 1);
            if(ImGui.IsItemHovered())
            {
                ImGui.BeginTooltip();
                ImGui.SetTooltip("Set as Dynamic Body");
                ImGui.EndTooltip();
            }
            ImGui.SliderFloat("Density", ref entityData.Density, 0, 3, entityData.Density.ToString("0.000"));
            ImGui.SliderFloat("Friction", ref entityData.Friction, 0, 3, entityData.Friction.ToString("0.000"));
            ImGui.SliderFloat("Restitution", ref entityData.Restitution, 0, 10, entityData.Restitution.ToString("0.000"));
            ImGui.NewLine();
            if(ImGui.Button("Reset", buttonSize))
            {
                entityData = new();
            }
            ImGui.NewLine();
        }
        if(componentBools.gameCamera2D)
        {
            if(ImGui.CollapsingHeader("GameCamera2D", ImGuiTreeNodeFlags.SpanAvailWidth))
            {
                ImGui.Checkbox("Is Active", ref gameCamera2D.IsActive);
            }
        }
        if(componentBools.playerComponent)
        {
            if(ImGui.CollapsingHeader("PlayerComponent", ImGuiTreeNodeFlags.SpanAvailWidth))
            {
                ImGui.BulletText("You should currently only use this component on 1 Entity in the game.");
                ImGui.BulletText("This could change in the future.");
                
            }
        }
        if(componentBools.spriteComponent)
        {
            if(ImGui.CollapsingHeader("SpriteComponent", ImGuiTreeNodeFlags.SpanAvailWidth))
            {
                ImGui.InputText("Sprite sheet filename", ref spriteComponent.sprite, 32, ImGuiInputTextFlags.AutoSelectAll);
                ImGui.InputInt("Sprite Frame Width", ref spriteComponent.width);
                ImGui.InputInt("Sprite Frame Height", ref spriteComponent.height);
                ImGui.InputInt("Frames Per Row", ref spriteComponent.framesPerRow);
                ImGui.InputInt("Rows", ref spriteComponent.rows);
                ImGui.InputInt("Framerate", ref spriteComponent.frameRate);
                ImGui.Checkbox("Play in reverse", ref spriteComponent.playInReverse);
                ImGui.Checkbox("Continuous", ref spriteComponent.continuous);
                ImGui.Checkbox("Looping", ref spriteComponent.looping);
                ImGui.NewLine();
                if(ImGui.Button("Reset", buttonSize))
                {
                    spriteComponent = new();
                }
                ImGui.NewLine();
            }
        }
        
        
    }
    
}