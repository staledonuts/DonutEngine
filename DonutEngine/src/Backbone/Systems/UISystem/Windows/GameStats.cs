﻿using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using ImGuiNET;

namespace DonutEngine.Backbone.Systems.UI;
public class GameStats : DocumentWindow
{
    Texture2D ImageTexture;
    Camera2D Camera = new Camera2D();

    Vector2 LastMousePos = new Vector2();
    Vector2 LastTarget = new Vector2();
    bool Dragging = false;

    bool DirtyScene = false;
    enum ToolMode
    {
        None,
        Move,
    }

    ToolMode CurrentToolMode = ToolMode.None;

    public override void Setup()
    {
        Camera.zoom = 1;
        Camera.target.X = 0;
        Camera.target.Y = 0;
        Camera.rotation = 0;
        Camera.offset.X = Raylib.GetScreenWidth() / 2.0f;
        Camera.offset.Y = Raylib.GetScreenHeight() / 2.0f;

        ViewTexture = Raylib.LoadRenderTexture(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        ImageTexture = Raylib.LoadTexture("resources/parrots.png");

        UpdateRenderTexture();
    }

    public override void Show()
    {
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));
        ImGui.SetNextWindowSizeConstraints(new Vector2(400, 400), new Vector2((float)Raylib.GetScreenWidth(), (float)Raylib.GetScreenHeight()));

        if (ImGui.Begin("GameStats", ref Open, ImGuiWindowFlags.NoScrollbar))
        {
            Focused = ImGui.IsWindowFocused(ImGuiFocusedFlags.RootAndChildWindows);

            Vector2 size = ImGui.GetContentRegionAvail();

            // center the scratch pad in the view
            Rectangle viewRect = new Rectangle();
            viewRect.x = ViewTexture.texture.width / 2 - size.X / 2;
            viewRect.y = ViewTexture.texture.height / 2 - size.Y / 2;
            viewRect.width = size.X;
            viewRect.height = -size.Y;

            if (ImGui.BeginChild("Toolbar", new Vector2(ImGui.GetContentRegionAvail().X, 25)))
            {
                ImGui.TextUnformatted(String.Format("Current Physics Bodies: "+Sys.physicsSystem.physicsInfo.currentPhysicsBodies.ToString()));
                ImGui.EndChild();
            }
            ImGui.End();
        }
        ImGui.PopStyleVar();
    }

    public override void Shutdown()
    {
        Raylib.UnloadRenderTexture(ViewTexture);
        Raylib.UnloadTexture(ImageTexture);
    }

    public override void DrawUpdate()
    {
        if (!Open)
            return;

        if (Raylib.IsWindowResized())
        {
            Raylib.UnloadRenderTexture(ViewTexture);
            ViewTexture = Raylib.LoadRenderTexture(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());

            Camera.offset.X = Raylib.GetScreenWidth() / 2.0f;
            Camera.offset.Y = Raylib.GetScreenHeight() / 2.0f;
        }

        if (Focused)
        {
            if (CurrentToolMode == ToolMode.Move)
            {
                if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    if (!Dragging)
                    {
                        LastMousePos = Raylib.GetMousePosition();
                        LastTarget = Camera.target;
                    }
                    Dragging = true;
                    Vector2 mousePos = Raylib.GetMousePosition();
                    Vector2 mouseDelta = Raymath.Vector2Subtract(LastMousePos, mousePos);

                    mouseDelta.X /= Camera.zoom;
                    mouseDelta.Y /= Camera.zoom;
                    Camera.target = Raymath.Vector2Add(LastTarget, mouseDelta);

                    DirtyScene = true;

                }
                else
                {
                    Dragging = false;
                }
            }
        }
        else
        {
            Dragging = false;
        }

        if (DirtyScene)
        {
            DirtyScene = false;
            UpdateRenderTexture();
        }
    }

    protected void UpdateRenderTexture()
    {
        Raylib.BeginTextureMode(ViewTexture);
        Raylib.ClearBackground(Color.BLUE);
        Raylib.BeginMode2D(Camera);
        Raylib.DrawTexture(ImageTexture, ImageTexture.width / -2, ImageTexture.height / -2, Color.WHITE);
        Raylib.EndMode2D();
        Raylib.EndTextureMode();
    }
}