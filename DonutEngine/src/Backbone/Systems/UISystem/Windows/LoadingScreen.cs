namespace DonutEngine.Backbone.Systems.UI;
using ImGuiNET;
using DonutEngine.Backbone.Systems;
using Raylib_cs;
using System.Numerics;

public class LoadingScreen : DocumentWindow
{
    string currentline = "";
    Vector2 loadingWindowSize = new(400, 400);

    
    public void FadeIn()
    {

    }
    public void FadeOut()
    {

    }
    public override void Shutdown()
    {
        
    }

    public override void DrawUpdate()
    {
        
        //Raylib.DrawText(LoadingItem, Raylib.GetScreenWidth() / 4, Raylib.GetScreenHeight() / 2, 4, Color.WHITE);
    }

    public void SetLoadingItem(string setItem)
    {
        currentline = setItem;
    }
    public override void Setup()
    {

    }

    public override void Show()
    {
        if (!Open)
        {
            return;
        }
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));
        ImGui.SetNextWindowSizeConstraints(loadingWindowSize, new(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()));
        if(ImGui.Begin("LoadingWindow", ref Open, ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize))
        {
            if(ImGui.BeginTabBar("Loading", ImGuiTabBarFlags.AutoSelectNewTabs))
            {
                if(ImGui.BeginTabItem("Loading"))
                {
                    ImGui.Text(currentline);
                    ImGui.EndTabItem();
                }
                ImGui.EndTabBar();
            }
            ImGui.End();
        }
        ImGui.PopStyleVar();
    }

    public void ToggleLoadScreen()
    {
        if(Open)
        {
            Open = false;
        }
        else
        {
            Open = true;
        }
    }
}