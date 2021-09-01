namespace Animacao1

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

    override this.Initialize() =
        // TODO: Add your initialization logic here
        
        base.Initialize()

    override this.LoadContent() =
        spriteBatch <- new SpriteBatch(this.GraphicsDevice)

        // TODO: use this.Content to load your game content here
        let frames = seq {
            SpriteFrame(156, 230, 76, 86)
            SpriteFrame(244, 226, 54, 88)
            SpriteFrame(308, 228, 74, 86)
            SpriteFrame(388, 224, 80, 88)
            SpriteFrame(472, 230, 80, 82)
            SpriteFrame(560, 230, 76, 88)
            SpriteFrame(652, 230, 56, 90)
            SpriteFrame(716, 230, 74, 90)
            SpriteFrame(792, 230, 82, 92)
            SpriteFrame(878, 242, 78, 78)
        }
        
        let bonkers = this.Content.Load<Texture2D>("assets/bonkers")
        
        frameAnimation <-
            FrameAnimation(150, "", bonkers, ResizeArray frames)
        
    
    override this.Update gameTime =
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back = ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        then this.Exit();

        // TODO: Add your update logic here
//        frameAnimation.Update gameTime
    
        base.Update(gameTime)
 
    override this.Draw gameTime =
        this.GraphicsDevice.Clear Color.CornflowerBlue

        // TODO: Add your drawing code here

        spriteBatch.Begin()
//        frameAnimation.Draw(gameTime, spriteBatch)
        spriteBatch.End()
        
        base.Draw(gameTime)