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

public class Animation : Component
{
    Sprite sprite;
    public float frame;
    public float speed;

    public int numFrames;
    public int start;
    public bool loop;

    public Animation(Sprite sprite, float speed, int start, int numFrames, bool loop)
    {
        this.sprite = sprite;
        this.speed = speed;
        this.numFrames = numFrames;
        this.start = start;
        this.loop = loop;
    }

    public override void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        frame += speed * dt;

        if ((int)frame >= start + numFrames)
        {
            if (loop) 
            {
                frame = start;
            }
            else
            {
                frame = start + numFrames;
            }
        }

        sprite.rectangle.X = (int)frame * sprite.rectangle.Width;
    }
}
