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
        Entity e = new Entity();
        Transform t = new Transform(new Vector2(20, 30));
        e.AddComponent(t);
        e.AddComponent(new RigidBody(t, new Vector2(20, 10)));
        e.AddComponent(new Sprite(new Rectangle(0, 0, 19, 25)));
        entities.Add(e);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        lisaSheet = Content.Load<Texture2D>("lisasheet");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach (Entity entity in entities)
        {
            entity.Update(gameTime);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        foreach (Entity entity in entities)
        {
            Transform transform = (Transform)entity.GetComponent<Transform>();
            Sprite sprite = (Sprite)entity.GetComponent<Sprite>();

            if (transform != null && sprite != null)
            {
                Rectangle dest = new Rectangle((int)transform.Position.X, (int)transform.Position.Y, sprite.rectangle.Width, sprite.rectangle.Height);
                _spriteBatch.Draw(lisaSheet, dest, sprite.rectangle, Color.White);
            }
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
