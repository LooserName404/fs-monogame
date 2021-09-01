namespace Animacao1

open System
open System.Collections.Generic
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input
open MonoLink

type Game1 () as this =
    inherit Game()
 
    let graphics = new GraphicsDeviceManager(this)
    let mutable spriteBatch = Unchecked.defaultof<_>
    
    do
        this.Content.RootDirectory <- "Content"
        this.IsMouseVisible <- true

    let mutable frameAnimation = Unchecked.defaultof<_>
    let mutable textureAnimation = Unchecked.defaultof<_>
    
    let mutable verticalLine = Unchecked.defaultof<_>
    let mutable horizontalLine = Unchecked.defaultof<_>

    override this.Initialize() =
        // TODO: Add your initialization logic here
        
        base.Initialize()

    override this.LoadContent() =
        spriteBatch <- new SpriteBatch(this.GraphicsDevice)
        let view = this.GraphicsDevice.Viewport
        
        verticalLine <- GameHelper.GetFilledTexture(this, 2, view.Height, Color.White)
        horizontalLine <- GameHelper.GetFilledTexture(this, view.Width, 2, Color.White)
        
        let frames = [|
            SpriteFrame(12, 338, 68, 84)
            SpriteFrame(86, 334, 70, 86, originY = 2f)
            SpriteFrame(12, 338, 68, 84)
            SpriteFrame(86, 334, 70, 86, originY = 2f)
            SpriteFrame(12, 338, 68, 84)
            SpriteFrame(86, 334, 70, 86, originY = 2f)
            SpriteFrame(170, 350, 100, 76, originY = -8f)
            SpriteFrame(274, 352, 92, 74, originY = -10f)
            SpriteFrame(372, 344, 92, 84)
            SpriteFrame(472, 344, 96, 82, originY = 2f)
            SpriteFrame(592, 348, 92, 72, originY = -12f)
            SpriteFrame(700, 348, 96, 72, originY = -12f)
            SpriteFrame(816, 334, 64, 88, originY = 4f)
            SpriteFrame(816, 334, 64, 88, originY = 4f)
            SpriteFrame(816, 334, 64, 88, originY = 4f)
            SpriteFrame(816, 334, 64, 88, originY = 4f)
        |]
        
        let bonkers = this.Content.Load<Texture2D>("assets/bonkers")
        
        frameAnimation <-
            FrameAnimation(60, "", bonkers, List frames)
            
        frameAnimation.Scale <- Vector2 2f
        frameAnimation.Position <- Vector2 (332f, 100f)
        
        let textures = [|
            this.Content.Load<Texture2D>("assets/horse-run-01")
            this.Content.Load<Texture2D>("assets/horse-run-02")
            this.Content.Load<Texture2D>("assets/horse-run-03")
            this.Content.Load<Texture2D>("assets/horse-run-04")
            this.Content.Load<Texture2D>("assets/horse-run-05")
            this.Content.Load<Texture2D>("assets/horse-run-06")
        |]
        
        textureAnimation <- TextureAnimation(150, "horse-run", textures)
        
    
    override this.Update gameTime =
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back = ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        then this.Exit();

        // TODO: Add your update logic here
        frameAnimation.Update gameTime
        textureAnimation.Update gameTime
    
        base.Update(gameTime)
 
    override this.Draw gameTime =
        this.GraphicsDevice.Clear Color.CornflowerBlue

        spriteBatch.Begin()
        frameAnimation.Draw(gameTime, spriteBatch)
        textureAnimation.Draw(gameTime, spriteBatch)
        spriteBatch.End()
        
        base.Draw(gameTime)