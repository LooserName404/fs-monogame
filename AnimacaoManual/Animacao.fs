module AnimacaoManual.Animacao

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

type Animation(time: int, texture: Texture2D, frames: Rectangle list) as this=
    
    let mutable elapsedTime = 0
    let mutable currentIndex = 0
    let mutable isFinished = false
    
    member val Position = Vector2.Zero with get, set
    member this.IsFinished
        with get() = isFinished
        and private set value = isFinished <- value
    
    member this.Update (gameTime: GameTime) =
        isFinished <- false
        elapsedTime <- elapsedTime + gameTime.ElapsedGameTime.Milliseconds
        
        if elapsedTime > time then
            elapsedTime <- 0
            currentIndex <- currentIndex + 1
            
            if currentIndex > frames.Length - 1 then
                currentIndex <- 0
                isFinished <- true
    
    member this.Draw (gameTime: GameTime) (spriteBatch: SpriteBatch) =
        let currentFrame = frames.[currentIndex]
        spriteBatch.Draw (texture, this.Position, currentFrame, Color.White)