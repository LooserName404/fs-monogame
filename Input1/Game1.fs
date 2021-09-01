namespace Input1

open System
open System.Collections.Generic
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input
open MonoLink

type Game1() as this =
    inherit Game()

    let graphics = new GraphicsDeviceManager(this)
    let mutable spriteBatch = Unchecked.defaultof<_>

    do
        this.Content.RootDirectory <- "Content"
        this.IsMouseVisible <- true

    let mutable texture = Unchecked.defaultof<_>
    let mutable color = Color.White
    
    let mutable oldState = Unchecked.defaultof<_>
    let mutable currentState = Unchecked.defaultof<_>

    let colors = List<Color>()
    let positions = List<Vector2>()
    let random = Random()

    override this.Initialize() =
        // TODO: Add your initialization logic here

        base.Initialize()

    override this.LoadContent() =
        spriteBatch <- new SpriteBatch(this.GraphicsDevice)

        // TODO: use this.Content to load your game content here
        texture <- GameHelper.GetFilledTexture(this, 100, 100, color)

    override this.Update(gameTime) =
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back = ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape)) then
            this.Exit()

        // TODO: Add your update logic here

//        let state = Keyboard.GetState()

//        if state.IsKeyDown(Keys.A) then
//            let r = random.Next(0, 256)
//            let g = random.Next(0, 256)
//            let b = random.Next(0, 256)
//            colors.Add(Color(r, g, b))
//
//            let x = random.Next(0, 700)
//            let y = random.Next(0, 450)
//            positions.Add(Vector2(float32 x, float32 y))
        oldState <- currentState
        currentState <- Keyboard.GetState()
        
        if oldState.IsKeyUp(Keys.A) && currentState.IsKeyDown(Keys.A) then
            let r = random.Next(0, 256)
            let g = random.Next(0, 256)
            let b = random.Next(0, 256)
            colors.Add(Color(r, g, b))

            let x = random.Next(0, 700)
            let y = random.Next(0, 450)
            positions.Add(Vector2(float32 x, float32 y))
            
        GamePad.SetVibration(PlayerIndex.One, 0.5f, 1.0f) |> ignore

        base.Update(gameTime)

    override this.Draw(gameTime) =
        this.GraphicsDevice.Clear Color.CornflowerBlue

        // TODO: Add your drawing code here

        spriteBatch.Begin()

        for i in 0..colors.Count - 1 do
            spriteBatch.Draw(texture, positions.[i], colors.[i])
        
        spriteBatch.End()

        base.Draw(gameTime)
