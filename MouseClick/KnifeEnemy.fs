namespace MouseClick

open System.Collections.Generic
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open MouseClick.Enemy
open MonoLink

module KnifeEnemy =

    type KnifeEnemy(game: Game) as this =
        inherit Enemy(game.Content.Load<Texture2D>("assets/knifeman"))

        let texture =
            game.Content.Load<Texture2D>("assets/knifeman")

        do
            let runFrames =
                List(
                    [|
                        SpriteFrame(320, 219, 46, 53)
                        SpriteFrame(497, 217, 54, 55)
                        SpriteFrame(656, 220, 31, 52)
                        SpriteFrame(384, 219, 46, 53)
                        SpriteFrame(584, 217, 42, 55)
                        SpriteFrame(656, 220, 31, 52)
                    |]
                )

            this.RunAnimation <- FrameAnimation(150, "run", texture, runFrames)
            this.CurrentAnimation <- this.RunAnimation

            this.DeathAnimation <-
                FrameAnimation(
                    500,
                    "death",
                    texture,
                    List([| SpriteFrame(97, 127, 27, 49); SpriteFrame(32, 117, 48, 58) |])
                )
