using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using ImGuiNET;
using Box2DX.Dynamics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DonutEngine.Backbone.Systems.UI;

namespace DonutEngine.Backbone.Systems.UI.Creator;

public partial class EntityCreator : DocumentWindow
{
    private static Dictionary<string, JsonSerializerSettings> serializerSettings = new Dictionary<string, JsonSerializerSettings>();
    
    private void SaveJson(string json)
    {
        if(json == "")
        {
            return;
        }
        else
        {
            var settings = new JsonSerializerSettings{Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore};
            var data = JsonConvert.SerializeObject(new {entityData, gameCamera2D, playerComponent, spriteComponent}, settings);

            File.WriteAllText(DonutFilePaths.app+Sys.settingsVars.entitiesPath+json+".json", data);
        }
    }
}