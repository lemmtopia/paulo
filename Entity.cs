using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;
using System;
using System.Reflection;

namespace paulo;

public class Entity
{
    List<Component> components = new List<Component>(); 
    
    public void Update(GameTime gameTime) 
    {
        foreach (Component component in components)
        {
            component.Update(gameTime);
        }
    }

    public void AddComponent(Component component)
    {
        components.Add(component);
    }

    public List<Component> GetComponents()
    {
        return components;
    }

    public Component GetComponent<T>()
    {
        foreach (Component component in components)
        {
            if (component is T)
            {
                return component;
            }
        }

        return null;
    }
}
