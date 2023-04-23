using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace DonutEngine.Backbone.Systems.UI;
public abstract class DocumentWindow
{
    public bool Open = false;

    public abstract void Start();
    public abstract void Shutdown();
    public abstract void DrawUpdate();
    public abstract void Update();

    public bool Focused = false;
}
