using System.Numerics;
using Raylib_cs;

namespace Engine.Systems.UI.Skeleton;

public class SkeletonUISystem : SystemClass
{
    public Vector2 WindowSize 
    {
        get
        {
            return new(Raylib.GetRenderWidth(), Raylib.GetRenderHeight());
        }
    }
    public List<Container> Containers { get; private set; }
    public Style GlobalStyle { get; set; }

    public SkeletonUISystem(Style global_style) 
    {
        GlobalStyle = global_style;
        Containers = new List<Container>();
    }

    public override void Initialize()
    {
        EngineSystems.dUpdate += Update;
        EngineSystems.dDrawUpdate += DrawUpdate;
        AddContainer(new SettingsWindow(Vector2.Zero, new(Settings.graphicsSettings.ScreenWidth, Settings.graphicsSettings.ScreenHeight), GlobalStyle));
    }

    // Add a container to the interface if the contianer isn't already a child of the interface
    public bool AddContainer(Container c) 
    {
        if (Containers.Contains(c))
        {
	        return false;
        }

        c.Parent = this;
        Containers.Add(c);
        c.Initialize();
        return true;
    }

    // Send an event to all children, return true if the event was handled
    public bool HandleEvent(Event e) 
    {
        foreach (var C in Containers) 
        {
            if (C.Active && C.FireEvent(e))
            {
                return true;
            }
	    }

        return false;
    }

    // Update all containers and their elements
    public override void Update() 
    {
        // If GlobalStyle is set, make sure it's applied to all children
        if (GlobalStyle is not null) 
        {
			foreach (var C in Containers) 
            {
                // Container
                if (C.Active && !C.IgnoreGlobalStyle && C.Style != GlobalStyle)
                {
                    C.Style = GlobalStyle;
                }

                // Widgets
			    foreach (var W in C.Widgets) 
                {
                    if (!W.IgnoreGlobalStyle && W.Style != GlobalStyle)
                    {
                        W.Style = GlobalStyle;
                    }
				}
		    }
        }

        // Update all containers and their elements
        foreach (var C in Containers) 
        {
            if (C.Active)
            {
			    C.Update();
            }
        }
    }

    // Draw all containers and their elements
    public override void DrawUpdate() 
    {
        foreach (var C in Containers) 
        {
            if (C.Active)
            {
			    C.Draw();
            }
	    }
    }

    public override void LateUpdate()
    {
        
    }

    public override void Shutdown()
    {
        EngineSystems.dUpdate -= Update;
        EngineSystems.dDrawUpdate -= DrawUpdate;
    }
}
