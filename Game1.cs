using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;
using System;

namespace paulo;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    RenderTarget2D target;

    Texture2D lisaSheet;
    List<Entity> entities = new List<Entity>();

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = 960;
        _graphics.PreferredBackBufferHeight = 540;
        _graphics.ApplyChanges();

        target = new RenderTarget2D(GraphicsDevice, 320, 180);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        lisaSheet = Content.Load<Texture2D>("lisasheet");

        /* Adding entities */
        Entity e = new Entity();
        Transform t = new Transform(new Vector2(20, 30));
        e.AddComponent(t);
        e.AddComponent(new RigidBody(t, new Vector2(20, 10)));
        e.AddComponent(new Sprite(lisaSheet, new Rectangle(0, 0, 19, 25)));

        Sprite s = (Sprite)e.GetComponent<Sprite>();
        e.AddComponent(new Animation(s, 8, 1, 6, true));

        entities.Add(e);

        Entity e2 = new Entity();
        Transform t2 = new Transform(new Vector2(0, 30));
        e2.AddComponent(t2);
        e2.AddComponent(new RigidBody(t2, Vector2.Zero));
        e2.AddComponent(new Sprite(lisaSheet, new Rectangle(0, 0, 19, 25)));
        entities.Add(e2);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // Simple loop, the components handle the rest
        foreach (Entity entity in entities)
        {
            // This updates all the components inside this entity btw
            entity.Update(gameTime);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        /* RenderTarget makes virtual resolution possible */
        GraphicsDevice.SetRenderTarget(target);
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        foreach (Entity entity in entities)
        {
            // This is kinda cool ngl
            Transform transform = (Transform)entity.GetComponent<Transform>();
            Sprite sprite = (Sprite)entity.GetComponent<Sprite>();

            if (transform != null && sprite != null)
            {
                Rectangle dest = new Rectangle((int)transform.Position.X, (int)transform.Position.Y, sprite.rectangle.Width, sprite.rectangle.Height);
                _spriteBatch.Draw(sprite.texture, transform.Position, sprite.rectangle, Color.White, sprite.rotation, sprite.origin, sprite.scale, sprite.flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            }
        }
        _spriteBatch.End();

        // Render the virtual resolution
        GraphicsDevice.SetRenderTarget(null);
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _spriteBatch.Draw(target, new Rectangle(0, 0, 960, 540), Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
