namespace MouseClick

open System
open System.Collections.Generic
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input
open MouseClick.Background
open MouseClick.KnifeEnemy

type Game1() as this =
    inherit Game()

    let _ = new GraphicsDeviceManager(this)
    let mutable spriteBatch = Unchecked.defaultof<_>

    do
        this.Content.RootDirectory <- "Content"
        this.IsMouseVisible <- true

    let mutable background = Unchecked.defaultof<_>
    let mutable view = Unchecked.defaultof<_>
    let mutable enemies = List<KnifeEnemy>()

    let layerPositions =
        [| Vector2(0f, 350f); Vector2(0f, 320f); Vector2(0f, 300f) |]

    let random = Random()
    let mutable rightToLeft = false
    let mutable elapsedTime = 0
    let spawnTime = 1000

    override this.Initialize() =
        // TODO: Add your initialization logic here

        base.Initialize()

    override this.LoadContent() =
        spriteBatch <- new SpriteBatch(this.GraphicsDevice)

        background <- Background this
        view <- this.GraphicsDevice.Viewport

    override this.Update gameTime =
        if
            GamePad.GetState(PlayerIndex.One).Buttons.Back = ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape)
        then
            this.Exit()

        elapsedTime <-
            elapsedTime
            + gameTime.ElapsedGameTime.Milliseconds

        if elapsedTime > spawnTime then
            elapsedTime <- 0
            rightToLeft <- random.Next(0, 10) > 4
            let enemy = KnifeEnemy(this)
            let layer = random.Next(0, 3)
            let layerF = layer |> float32

            let position =
                if rightToLeft then
                    Vector2(view.Width |> float32, layerPositions.[layer].Y)
                else
                    layerPositions.[layer]

            let xVelocity =
                let x = random.Next(2, 9) |> float32
                if rightToLeft then -x else x

            enemy.Set xVelocity layerF
            enemy.CurrentAnimation.Position <- position

            enemies.Add enemy

        let len = enemies.Count
        for i in 0..len - 1 do
            enemies.[i].Update gameTime
            
            if not enemies.[i].IsEnabled
               || enemies.[i].CurrentAnimation.Position.X < float32 view.X
               || enemies.[i].CurrentAnimation.Position.X > float32 view.Width then
                enemies.RemoveAt i
            
        enemies |> Seq.iter (fun e -> e.Update gameTime)
        enemies <- List(enemies |> Seq.filter (fun e ->
           not (not e.IsEnabled
               || e.CurrentAnimation.Position.X < float32 view.X
               || e.CurrentAnimation.Position.X > float32 view.Width)))

        base.Update gameTime

    override this.Draw gameTime =
        this.GraphicsDevice.Clear Color.Black

        spriteBatch.Begin()

        background.Draw spriteBatch

        let len = enemies.Count
        for i in 0..len - 1 do
            enemies.[i].Draw gameTime spriteBatch

        spriteBatch.End()

        base.Draw gameTime
