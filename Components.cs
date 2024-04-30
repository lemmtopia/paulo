using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace paulo;

public class Component
{
    public virtual void Update(GameTime gameTime) 
    {
        return;
    }
}

public class Transform : Component
{
    public Vector2 Position { get; set; }

    public Transform(Vector2 position)
    {
        Position = position;
    }

    public override void Update(GameTime gameTime)
    {
        return; 
    }
}

public class RigidBody : Component
{
    Transform transform;
    Vector2 Velocity { get; set; }

    public RigidBody(Transform transform, Vector2 velocity)
    {
        this.transform = transform;
        Velocity = velocity;
    }

    public override void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        transform.Position += Velocity * dt;
    }
}

public class Sprite : Component
{
    public Rectangle rectangle;

    public Sprite(Rectangle rect)
    {
        rectangle = rect;
    }

    public override void Update(GameTime gameTime)
    {
        return; 
    }
}
