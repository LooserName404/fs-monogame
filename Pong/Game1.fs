namespace Pong

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input
open Microsoft.Xna.Framework.Audio
open Microsoft.Xna.Framework.Media

type Game1() as this =
    inherit Game()

    let _ = new GraphicsDeviceManager(this)
    let mutable spriteBatch = Unchecked.defaultof<_>

    do
        this.Content.RootDirectory <- "Content"
        this.IsMouseVisible <- true

    let mutable rightBar = Unchecked.defaultof<_>
    let mutable leftBar = Unchecked.defaultof<_>
    let mutable ball = Unchecked.defaultof<_>
    
    let mutable goalSound = Unchecked.defaultof<_>
    let mutable tapSound = Unchecked.defaultof<_>
    
    let mutable backSong = Unchecked.defaultof<_>

    let mutable rightPosition = Unchecked.defaultof<_>
    let mutable leftPosition = Unchecked.defaultof<_>
    let mutable ballPosition = Unchecked.defaultof<_>
    
    let barWidth = 20
    let barHeight = 200
    let ballSize = 20
    
    let barSpeed = 5
    
    let mutable ballVelocity = Vector2(-3f, -3f)
    
    let mutable view = Unchecked.defaultof<_>

    override this.Initialize() =
        // TODO: Add your initialization logic here
        view <- this.GraphicsDevice.Viewport.Bounds
        base.Initialize()

    override this.LoadContent() =
        spriteBatch <- new SpriteBatch(this.GraphicsDevice)

        // TODO: use this.Content to load your game content here
        rightBar <- this.Content.Load<Texture2D>("assets/bar")
        leftBar <- this.Content.Load<Texture2D>("assets/bar")
        ball <- this.Content.Load<Texture2D>("assets/ball")

        leftPosition <- Vector2(view.X + barWidth |> float32, view.Height / 2 - barHeight / 2 |> float32)
        rightPosition <- Vector2(view.Width - barWidth * 2 |> float32, view.Height / 2 - barHeight / 2 |> float32)
        ballPosition <- Vector2(view.Width / 2 - ballSize / 2 |> float32, view.Height / 2 - ballSize / 2 |> float32)
        
        goalSound <- this.Content.Load<SoundEffect>("assets/goal")
        tapSound <- this.Content.Load<SoundEffect>("assets/tap")
        backSong <- this.Content.Load<Song>("assets/back")
        
        MediaPlayer.Play(backSong)
        MediaPlayer.Volume <- 0.1f

    override this.Update gameTime =
        if
            GamePad.GetState(PlayerIndex.One).Buttons.Back = ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape)
        then
            this.Exit()

        // TODO: Add your update logic here
        
        let state = Keyboard.GetState()
        let newLeftPosition =
            let pos =
                if state.IsKeyDown Keys.W then leftPosition.Y - float32 barSpeed
                elif state.IsKeyDown Keys.S then leftPosition.Y + float32 barSpeed
                else leftPosition.Y
            if pos < float32 view.Y then view.Y |> float32
            elif pos + float32 barHeight > float32 view.Height then view.Height - barHeight |> float32
            else pos
        
        let newRightPosition =
            let pos =
                if state.IsKeyDown Keys.Up then rightPosition.Y - float32 barSpeed
                elif state.IsKeyDown Keys.Down then rightPosition.Y + float32 barSpeed
                else rightPosition.Y
            if pos < float32 view.Y then view.Y |> float32
            elif pos + float32 barHeight > float32 view.Height then view.Height - barHeight |> float32
            else pos
            
        let leftRectangle = Rectangle(leftPosition.ToPoint(), Point(barWidth, barHeight))
        let rightRectangle = Rectangle(rightPosition.ToPoint(), Point(barWidth, barHeight))
        let ballRectangle = Rectangle(ballPosition.ToPoint(), Point(ballSize, ballSize))
        
        let newBallPosition =
            if ballPosition.Y < float32 view.Y || ballPosition.Y + float32 ballSize > float32 view.Height then
                ballVelocity.Y <- ballVelocity.Y * -1f
            
            if ballRectangle.Intersects(leftRectangle)
                || ballRectangle.Intersects(rightRectangle)
            then
                tapSound.Play() |> ignore
                
                ballVelocity.X <- -1f * 
                    match ballVelocity.X with
                    | x when x > 0f -> x + 0.5f
                    | x -> x - 0.5f
            
            let pos = ballPosition + ballVelocity
            if
                pos.X < float32 view.X
                || pos.X + float32 ballSize > float32 view.Width
            then
                ballVelocity <-
                    match ballVelocity.X with
                    | x when x > 0f -> Vector2(-3f, -3f)
                    | _ -> Vector2(3f, 3f)
                goalSound.Play() |> ignore
                Vector2(view.Width / 2 - ballSize / 2 |> float32, view.Height / 2 - ballSize / 2 |> float32)
            else pos
        
        leftPosition.Y <- newLeftPosition
        rightPosition.Y <- newRightPosition
        
        ballPosition <- newBallPosition
        
        base.Update gameTime

    override this.Draw gameTime =
        this.GraphicsDevice.Clear Color.CornflowerBlue

        // TODO: Add your drawing code here

        spriteBatch.Begin()

        spriteBatch.Draw(leftBar, rightPosition, Color.White)
        spriteBatch.Draw(rightBar, leftPosition, Color.White)
        spriteBatch.Draw(ball, ballPosition, Color.White)

        spriteBatch.End()

        base.Draw(gameTime)
