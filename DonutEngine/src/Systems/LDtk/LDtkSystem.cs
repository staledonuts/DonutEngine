using Raylib_cs;
using Newtonsoft.Json;
using System.Numerics;

namespace DonutEngine
{
    public class LdtkSystem : SystemClass, IDrawUpdateSys
    {
        public LdtkData.LdtkJson LdtkProject { get; private set; }
        public LdtkData.Level CurrentLevel { get; private set; }
        private Dictionary<long, Texture2D> tilesetTextures = new();
        private Camera2D camera;
        private WorldSystem worldSystem;

        public LdtkSystem(WorldSystem worldSystem)
        {
            this.worldSystem = worldSystem;
        }

        // --- System Overrides ---
        public override void Initialize()
        {
            Console.WriteLine("LdtkSystem Initialized");
            camera = new Camera2D();
            camera.Zoom = 1.0f;
            camera.Offset = new Vector2(Raylib.GetScreenWidth() / 2.0f, Raylib.GetScreenHeight() / 2.0f);
        }

        public override void Shutdown()
        {
            foreach (var texture in tilesetTextures.Values)
            {
                Raylib.UnloadTexture(texture);
            }
            tilesetTextures.Clear();
            Console.WriteLine("LdtkSystem Shutdown");
        }

        public void LoadProject(string path)
        {
            Console.WriteLine($"Loading LDtk project from: {path}");
            string jsonContent = File.ReadAllText(path);
            
            LdtkProject = JsonConvert.DeserializeObject<LdtkData.LdtkJson>(jsonContent);

            if (LdtkProject == null)
            {
                Console.WriteLine("Failed to load or parse LDtk project.");
                return;
            }

            // Load all the tileset textures defined in the project
            string projectDirectory = Path.GetDirectoryName(path);
            foreach (var tilesetDef in LdtkProject.Defs.Tilesets)
            {
                string texturePath = Path.Combine(projectDirectory, tilesetDef.RelPath);
                if (File.Exists(texturePath))
                {
                    Texture2D texture = Raylib.LoadTexture(texturePath);
                    tilesetTextures.Add(tilesetDef.Uid, texture);
                    Console.WriteLine($"Loaded tileset: {tilesetDef.Identifier}");
                }
                else
                {
                    Console.WriteLine($"Warning: Tileset texture not found at {texturePath}");
                }
            }

            if (LdtkProject.Levels.Length > 0)
            {
                LoadLevel(LdtkProject.Levels[0].Iid);
            }
        }

        public void LoadLevel(string iid)
        {
            CurrentLevel = LdtkProject.Levels.FirstOrDefault(l => l.Iid == iid);
            if (CurrentLevel != null)
            {
                Console.WriteLine($"Successfully loaded level: {CurrentLevel.Identifier}");
                worldSystem.CreateEntitiesForLevel(CurrentLevel);
            }
            else
            {
                Console.WriteLine($"Error: Could not find level with IID: {iid}");
            }
        }
        
        public void DrawUpdate()
        {
            if (CurrentLevel == null) return;
            
            var player = worldSystem.GetEntity<Player>();
            if(player != null)
            {
                camera.Target = player.Position;
            }


            Raylib.BeginMode2D(camera);
            
            Raylib.ClearBackground(ParseColor(CurrentLevel.BgColor));

            foreach (var layer in CurrentLevel.LayerInstances.Reverse())
            {
                switch (layer.Type)
                {
                    case "Tiles":
                        DrawTileLayer(layer);
                        break;
                    case "AutoLayer":
                        DrawTileLayer(layer);
                        break;
                    case "IntGrid":
                        DrawIntGridDebug(layer);
                        break;
                }
            }

            Raylib.EndMode2D();
        }

        private void DrawTileLayer(LdtkData.LayerInstance layer)
        {
            if (!tilesetTextures.TryGetValue(layer.TilesetDefUid.Value, out var tilesetTexture))
            {
                return;
            }

            var tiles = layer.Type == "AutoLayer" ? layer.AutoLayerTiles : layer.GridTiles;

            foreach (var tile in tiles)
            {
                Rectangle sourceRec = new Rectangle(tile.Src[0], tile.Src[1], layer.GridSize, layer.GridSize);
                
                if ((tile.F & 1) == 1) sourceRec.Width *= -1;  // Flip X
                if ((tile.F & 2) == 2) sourceRec.Height *= -1; // Flip Y

                Rectangle destRec = new Rectangle(
                    CurrentLevel.WorldX + tile.Px[0], 
                    CurrentLevel.WorldY + tile.Px[1], 
                    layer.GridSize, 
                    layer.GridSize);

                Raylib.DrawTexturePro(tilesetTexture, sourceRec, destRec, Vector2.Zero, 0, Color.White);
            }
        }

        private void DrawIntGridDebug(LdtkData.LayerInstance layer)
        {
            for (int i = 0; i < layer.IntGridCsv.Length; i++)
            {
                if (layer.IntGridCsv[i] > 0)
                {
                    int gridX = i % (int)layer.CWid;
                    int gridY = i / (int)layer.CWid;
                    
                    int worldX = CurrentLevel.WorldX + gridX * (int)layer.GridSize;
                    int worldY = CurrentLevel.WorldY + gridY * (int)layer.GridSize;

                    Raylib.DrawRectangle(worldX, worldY, (int)layer.GridSize, (int)layer.GridSize, new Color(255, 0, 0, 100));
                }
            }
        }
        
        private Color ParseColor(string hex)
        {
            hex = hex.TrimStart('#');
            int r = Convert.ToInt32(hex.Substring(0, 2), 16);
            int g = Convert.ToInt32(hex.Substring(2, 2), 16);
            int b = Convert.ToInt32(hex.Substring(4, 2), 16);
            return new Color(r, g, b, 255);
        }
    }
}
