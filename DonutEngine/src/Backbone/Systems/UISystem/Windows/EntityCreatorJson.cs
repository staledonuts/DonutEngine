using Newtonsoft.Json;


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

            File.WriteAllText(DonutFilePaths.app+Sys.settingsContainer.entitiesPath+json+".json", data);
        }
    }
}