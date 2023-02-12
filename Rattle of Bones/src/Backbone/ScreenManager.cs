using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Raylib_cs;

namespace DonutEngine.Backbone
{
    public class ScreenManager
    {
        static GameScreen currentScreen = GameScreen.BootSplash;
        static Texture2D splashTex;
        public void InitScreenManager()
        {
            splashTex = Raylib.LoadTexture(FilePaths.app+"Assets/Splash/raylib-cs.png");
        }

        enum GameScreen
        {
            BootSplash,
            MainMenu,
            Game
        }
        //CurrentScreen Should be inside UpdateLoop
        public void CurrentScreen()
        {
            switch (currentScreen)
            {
                case GameScreen.BootSplash:
                    {
                        // TODO: Update LOGO screen variables here!

                        Program.framesCounter++;    // Count frames

                        // Wait for 2 seconds (120 frames) before jumping to TITLE screen
                        if (Program.framesCounter > 120)
                        {
                            currentScreen = GameScreen.MainMenu;
                            Raylib.UnloadTexture(splashTex);
                            splashTex = Raylib.LoadTexture(FilePaths.app+"Assets/Splash/Titlelogo.png");
                            splashTex.height = splashTex.height * 4;
                            splashTex.width = splashTex.width * 4;
                            
                        }
                    }
                    break;
                case GameScreen.MainMenu:
                    {
                        // TODO: Update TITLE screen variables here!

                        // Press enter to change to GAMEPLAY screen
                        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER) || Raylib.IsGestureDetected(Gesture.GESTURE_TAP))
                        {
                            currentScreen = GameScreen.Game;
                        }
                    }
                    break;
                case GameScreen.Game:
                    {
                        ECS.ProcessUpdate();
                        // TODO: Update GAMEPLAY screen variables here!

                        // Press enter to change to ENDING screen
                        /*if (IsKeyPressed(KeyboardKey.KEY_ENTER) || IsGestureDetected(Gesture.GESTURE_TAP))
                        {
                            currentScreen = GameScreen.ENDING;
                        }*/
                    }
                    break;
                /*case GameScreen.ENDING:
                    {
                        // TODO: Update ENDING screen variables here!

                        // Press enter to return to TITLE screen
                        if (IsKeyPressed(KeyboardKey.KEY_ENTER) || IsGestureDetected(Gesture.GESTURE_TAP))
                        {
                            currentScreen = GameScreen.TITLE;
                        }
                    }
                    break;*/
                default:
                    break;
            }
        }

        public void DrawCurrentScreen(int screenWidth, int screenHeight)
        {
            switch (currentScreen)
            {
                case GameScreen.BootSplash:
                    {
                        // TODO: Draw LOGO screen here!
                        Raylib.DrawText(Raylib.GetFPS().ToString(), 12, 24, 20, Color.WHITE);
                        Raylib.DrawTexture(splashTex, (Program.settingsVars.screenWidth / 2) - splashTex.width / 2,(Program.settingsVars.screenHeight / 2)  - splashTex.height /2,Color.WHITE);
                        Raylib.DrawText("Made With:", Program.settingsVars.screenWidth / 2, (Program.settingsVars.screenHeight / 2) +256, 20, Color.WHITE);
                    }
                    break;
                case GameScreen.MainMenu:
                    {
                        // TODO: Draw TITLE screen here!
                        Raylib.DrawText(Raylib.GetFPS().ToString(), 12, 24, 20, Color.WHITE);
                        Raylib.DrawTexture(splashTex, (Program.settingsVars.screenWidth / 2) - splashTex.width / 2,(Program.settingsVars.screenHeight / 2)  - splashTex.height /2,Color.WHITE);
                        Raylib.DrawText("PRESS ENTER or TAP to JUMP to GAMEPLAY SCREEN", 120, 220, 20, Color.DARKGREEN);
                    }
                    break;
                case GameScreen.Game:
                    {
                        Raylib.DrawText(Raylib.GetFPS().ToString(), 12, 24, 20, Color.WHITE);
                        Raylib.BeginMode2D(GameObjects.donutcam);
                        Raylib.DrawRectangle(0, 0, screenWidth, screenHeight, Color.PURPLE);
                        Raylib.DrawText("GAMEPLAY SCREEN", 20, 20, 40, Color.MAROON);
                        ECS.ProcessDrawUpdate();
                        Raylib.EndMode2D();

                    }
                    break;
                /*case GameScreen.ENDING:
                    {
                        // TODO: Draw ENDING screen here!
                        Raylib.DrawRectangle(0, 0, screenWidth, screenHeight, Color.BLUE);
                        Raylib.DrawText("ENDING SCREEN", 20, 20, 40, Color.DARKBLUE);
                        Raylib.DrawText("PRESS ENTER or TAP to RETURN to TITLE SCREEN", 120, 220, 20, DARKBLUE);

                    }
                    break;*/
                default:
                    break;
            }
        }

        public void LateCurrentScreen()
        {
            switch (currentScreen)
            {
                case GameScreen.Game:
                    {
                        GameObjects.donutcam.target = GameObjects.player.transform.position;
                    }
                    break;
                default:
                    break;
            }
            ECS.ProcessLateUpdate();
        }
    }
}
    