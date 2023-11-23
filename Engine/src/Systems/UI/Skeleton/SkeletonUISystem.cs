using System.Numerics;
using Raylib_cs;

namespace Engine.Systems.UI.Skeleton;

public class SkeletonUISystem : SystemClass, IUpdateSys, IDrawUpdateSys
{
    public Vector2 WindowSize 
    {
        get
        {
            return RaylibHelper.ScreenSize;
        }
    }
    public List<Container> Containers { get; private set; }
    public static Style GlobalStyle { get; private set; }
    bool empty = true;

    public SkeletonUISystem(Style global_style) 
    {
        SkeletonUISystem.GlobalStyle = global_style;
        Containers = new List<Container>();
    }

    public override void Initialize()
    {
        
    }

    // Add a container to the interface if the container isn't already a child of the interface
    public bool AddContainer(Container c) 
    {
        if (Containers.Contains(c))
        {
	        return false;
        }

        c.Parent = this;
        Containers.Add(c);
        if(empty) { empty = false; }
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
    public void Update() 
    {
        if(!empty)
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
            foreach (var C in Containers) 
            {
                if (C.Active)
                {
                    C.Update();
                }
            }
        }

        // Update all containers and their elements
    }

    // Draw all containers and their elements
    public void DrawUpdate() 
    {
        if(!empty)
        {
            foreach (var C in Containers) 
            {
                if (C.Active)
                {
                    C.Draw();
                }
            }
        }
    }

    public override void Shutdown()
    {
        
    }
}
