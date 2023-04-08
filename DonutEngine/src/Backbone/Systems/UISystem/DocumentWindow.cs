using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace DonutEngine.Backbone.Systems.UI;
public abstract class DocumentWindow
{
    public bool Open = false;

    public RenderTexture2D ViewTexture;

    public abstract void Setup();
    public abstract void Shutdown();
    public abstract void Show();
    public abstract void DrawUpdate();

    public bool Focused = false;
}
