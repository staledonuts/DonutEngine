namespace LdtkData
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class LdtkJson
    {
        [JsonProperty("jsonVersion")]
        public string JsonVersion { get; set; }

        [JsonProperty("defs")]
        public Defs Defs { get; set; }

        [JsonProperty("externalLevels")]
        public bool ExternalLevels { get; set; }

        [JsonProperty("iid")]
        public string Iid { get; set; }

        [JsonProperty("levels")]
        public Level[] Levels { get; set; }
        
        [JsonProperty("worldLayout")]
        public string WorldLayout { get; set; }

        [JsonProperty("worldGridWidth")]
        public long? WorldGridWidth { get; set; }
        
        [JsonProperty("worldGridHeight")]
        public long? WorldGridHeight { get; set; }

        [JsonProperty("worlds")]
        public World[] Worlds { get; set; }
    }

    public partial class Defs
    {
        [JsonProperty("entities")]
        public EntityDefinition[] Entities { get; set; }

        [JsonProperty("enums")]
        public EnumDefinition[] Enums { get; set; }

        [JsonProperty("externalEnums")]
        public EnumDefinition[] ExternalEnums { get; set; }

        [JsonProperty("layers")]
        public LayerDefinition[] Layers { get; set; }

        [JsonProperty("levelFields")]
        public FieldDefinition[] LevelFields { get; set; }

        [JsonProperty("tilesets")]
        public TilesetDefinition[] Tilesets { get; set; }
    }

    public partial class EntityDefinition
    {
        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("fieldDefs")]
        public FieldDefinition[] FieldDefs { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("uid")]
        public long Uid { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }

    public partial class FieldDefinition
    {
        [JsonProperty("__type")]
        public string Type { get; set; }

        [JsonProperty("identifier")]
        public string Identifier { get; set; }
        
        [JsonProperty("uid")]
        public long Uid { get; set; }
    }

    public partial class EnumDefinition
    {
        [JsonProperty("externalRelPath")]
        public string ExternalRelPath { get; set; }

        [JsonProperty("iconTilesetUid")]
        public long? IconTilesetUid { get; set; }

        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("uid")]
        public long Uid { get; set; }

        [JsonProperty("values")]
        public EnumValueDefinition[] Values { get; set; }
    }

    public partial class EnumValueDefinition
    {
        [JsonProperty("__tileSrcRect")]
        public long[] TileSrcRect { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("tileId")]
        public long? TileId { get; set; }
    }

    public partial class LayerDefinition
    {
        [JsonProperty("__type")]
        public string Type { get; set; }

        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("intGridValues")]
        public IntGridValueDefinition[] IntGridValues { get; set; }

        [JsonProperty("tilesetDefUid")]
        public long? TilesetDefUid { get; set; }

        [JsonProperty("uid")]
        public long Uid { get; set; }
        
        [JsonProperty("gridSize")]
        public long GridSize { get; set; }
    }

    public partial class IntGridValueDefinition
    {
        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("value")]
        public long Value { get; set; }
    }

    public partial class TilesetDefinition
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("relPath")]
        public string RelPath { get; set; }

        [JsonProperty("tileGridSize")]
        public long TileGridSize { get; set; }

        [JsonProperty("uid")]
        public long Uid { get; set; }
    }

    public partial class Level
    {
        [JsonProperty("bgColor")]
        public string BgColor { get; set; }

        [JsonProperty("externalRelPath")]
        public string ExternalRelPath { get; set; }

        [JsonProperty("fieldInstances")]
        public FieldInstance[] FieldInstances { get; set; }

        [JsonProperty("identifier")]
        public string Identifier { get; set; }
        
        [JsonProperty("iid")]
        public string Iid { get; set; }

        [JsonProperty("layerInstances")]
        public LayerInstance[] LayerInstances { get; set; }

        [JsonProperty("pxHei")]
        public long PxHei { get; set; }

        [JsonProperty("pxWid")]
        public long PxWid { get; set; }

        [JsonProperty("__neighbours")]
        public Neighbour[] Neighbours { get; set; }

        [JsonProperty("worldX")]
        public int WorldX { get; set; }

        [JsonProperty("worldY")]
        public int WorldY { get; set; }
    }

    public partial class FieldInstance
    {
        [JsonProperty("__identifier")]
        public string Identifier { get; set; }

        [JsonProperty("__type")]
        public string Type { get; set; }

        [JsonProperty("__value")]
        public object Value { get; set; }
        
        [JsonProperty("__tile")]
        public object Tile { get; set; }
    }

    public partial class LayerInstance
    {
        [JsonProperty("__cHei")]
        public long CHei { get; set; }

        [JsonProperty("__cWid")]
        public long CWid { get; set; }

        [JsonProperty("__gridSize")]
        public long GridSize { get; set; }

        [JsonProperty("__identifier")]
        public string Identifier { get; set; }

        [JsonProperty("__type")]
        public string Type { get; set; }
        
        [JsonProperty("entityInstances")]
        public EntityInstance[] EntityInstances { get; set; }

        [JsonProperty("gridTiles")]
        public TileInstance[] GridTiles { get; set; }
        
        [JsonProperty("autoLayerTiles")]
        public TileInstance[] AutoLayerTiles { get; set; }

        [JsonProperty("intGridCsv")]
        public long[] IntGridCsv { get; set; }

        [JsonProperty("layerDefUid")]
        public long LayerDefUid { get; set; }

        [JsonProperty("levelId")]
        public long LevelId { get; set; }

        [JsonProperty("tilesetDefUid")]
        public long? TilesetDefUid { get; set; }
    }
    
    public partial class EntityInstance
    {
        [JsonProperty("__grid")]
        public long[] Grid { get; set; }

        [JsonProperty("__identifier")]
        public string Identifier { get; set; }
        
        [JsonProperty("fieldInstances")]
        public FieldInstance[] FieldInstances { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("iid")]
        public string Iid { get; set; }

        [JsonProperty("px")]
        public long[] Px { get; set; }
        
        [JsonProperty("width")]
        public long Width { get; set; }
    }

    public partial class TileInstance
    {
        [JsonProperty("d")]
        public long[] D { get; set; }

        [JsonProperty("f")]
        public long F { get; set; }

        [JsonProperty("px")]
        public long[] Px { get; set; }

        [JsonProperty("src")]
        public long[] Src { get; set; }

        [JsonProperty("t")]
        public long T { get; set; }
    }
    
    public partial class Neighbour
    {
        [JsonProperty("dir")]
        public string Dir { get; set; }

        [JsonProperty("levelIid")]
        public string LevelIid { get; set; }
    }

    public partial class World
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("iid")]
        public string Iid { get; set; }

        [JsonProperty("levels")]
        public Level[] Levels { get; set; }
    }
}
