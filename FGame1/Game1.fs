namespace FGame1

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input
open System

type Game1 () as this =
    inherit Game()

    let graphics = new GraphicsDeviceManager(this)
    let mutable spriteBatch = Unchecked.defaultof<_>

    let mutable pachiminko = Unchecked.defaultof<_>
    let mutable pachiminkoSprite = Unchecked.defaultof<_>
    let mutable pachiminkoDuo = Unchecked.defaultof<_>

    do
        this.Content.RootDirectory <- "Content"
        this.IsMouseVisible <- true

    override this.Initialize() =
        // TODO: Add your initialization logic here
        graphics.PreferredBackBufferWidth <- 1280
        graphics.PreferredBackBufferHeight <- 720

        graphics.ApplyChanges()
        
        base.Initialize()

    override this.LoadContent() =
        spriteBatch <- new SpriteBatch(this.GraphicsDevice)

        // TODO: use this.Content to load your game content here
        pachiminko <- this.Content.Load<Texture2D>("Assets/pachiminko")
        pachiminkoSprite <- this.Content.Load<Texture2D>("Assets/pachiminko-sprite-sheet")
        pachiminkoDuo <- this.Content.Load<Texture2D>("Assets/pachiminko-duo")

    override this.Update (gameTime) =
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back = ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) then
            this.Exit()

        // TODO: Add your update logic here
    
        base.Update(gameTime)

    override this.Draw (gameTime) =
        this.GraphicsDevice.Clear Color.CornflowerBlue

        // TODO: Add your drawing code here
        spriteBatch.Begin()
        spriteBatch.Draw(
            texture = pachiminkoDuo,
            position = Vector2(400f, 250f),
            sourceRectangle = Nullable(),
            destinationRectangle = Nullable(),
            color = Color.White,
            rotation = 0f,
            origin = Vector2.Zero,
            scale = Vector2.One,
            effects = (SpriteEffects.FlipHorizontally ||| SpriteEffects.FlipVertically),
            layerDepth = 1f)
        spriteBatch.Draw(
            texture = pachiminkoDuo,
            position = Vector2(400f, 250f),
            sourceRectangle = Nullable(),
            destinationRectangle = Nullable(),
            color = Color.Black,
            rotation = 0f,
            origin = Vector2(50f, 50f),
            scale = Vector2.One,
            effects = (SpriteEffects.FlipHorizontally ||| SpriteEffects.FlipVertically),
            layerDepth = 0f)
        spriteBatch.End()

        base.Draw(gameTime)

