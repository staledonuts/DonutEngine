namespace DonutEngine.Backbone.Systems.UI;
using System.Numerics;
using Raylib_cs;
using ImGuiNET;
using DonutEngine.Backbone.Systems;
public class EntitySpawnList : DocumentWindow
{
    Vector2 buttonSize = new(100, 18);
    int currentListItem = 0;
    int currentEntitiesListItem = 0;
    private Dictionary<int, Entity> entities;
    List<string> nameList = new();
    public override void DrawUpdate()
    {
        //throw new NotImplementedException();
    }

    public override void Setup()
    {
        entities = DonutSystems.entityManager.GetEntityList();
        CreateNameList();
    }

    public override void Show()
    {
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));
        ImGui.SetNextWindowSizeConstraints(new Vector2(400, 400), new Vector2((float)Raylib.GetScreenWidth(), (float)Raylib.GetScreenHeight()));
        
        if (ImGui.Begin("Entity Spawner", ref Open, ImGuiWindowFlags.None))
        {
            Focused = ImGui.IsWindowFocused(ImGuiFocusedFlags.RootAndChildWindows);
            Vector2 size = ImGui.GetContentRegionAvail();
            if(ImGui.BeginTabBar("Sound Tester"))
            {
                DoContent(size);
                ImGui.EndTabBar();
            }
            ImGui.End();
        }
        ImGui.PopStyleVar();
    }

    public override void Shutdown()
    {
        //throw new NotImplementedException();
    }

    private void DoContent(Vector2 width)
    {
        if(ImGui.BeginTabItem("Entity Spawner"))
        {
            if(ImGui.Button("Spawn", buttonSize))
            {            
                string currentSelection = Directory.GetFiles(DonutFilePaths.app+DonutSystems.settingsVars.entitiesPath, "*.json").GetValue(currentListItem).ToString();
                DonutSystems.entityManager.CreateEntity(currentSelection);
            }
            ImGui.NewLine();
            ImGui.BeginListBox("Entities", width);
            ImGui.ListBox("Entities", ref currentListItem, Directory.GetFiles(DonutFilePaths.app+DonutSystems.settingsVars.entitiesPath, "*.json"), Directory.GetFiles(DonutFilePaths.app+DonutSystems.settingsVars.entitiesPath, "*.json").Count());
            ImGui.EndListBox();
            ImGui.EndTabItem();
        }
        if(ImGui.BeginTabItem("Entity List"))
        {
            if(ImGui.Button("Destroy Entity", buttonSize))
            {
                string currentSelection = Directory.GetFiles(DonutFilePaths.app+DonutSystems.settingsVars.entitiesPath, "*.json").GetValue(currentListItem).ToString();
                DonutSystems.entityManager.CreateEntity(currentSelection);
            }
            if(ImGui.Button("Refresh List", buttonSize))
            {
                CreateNameList();
            }
            ImGui.NewLine();
            ImGui.BeginListBox("Entities", width);
            ImGui.ListBox("Entities", ref currentEntitiesListItem, nameList.ToArray(), entities.Count());
            ImGui.EndListBox();
            ImGui.EndTabItem();
        }       
    }

    private void CreateNameList()
    {
        nameList = new();
        
        foreach(KeyValuePair<int, Entity> keyValuePair in entities)
        {
            nameList.Add(keyValuePair.Value.Name);
        }
    }
}