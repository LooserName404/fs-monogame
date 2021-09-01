namespace MouseClick

module Background =

    open Microsoft.Xna.Framework
    open Microsoft.Xna.Framework.Graphics

    type Background(game: Game) =
        let mutable texture = Unchecked.defaultof<_>
        do
            texture <- game.Content.Load<Texture2D>("assets/stage")
            
        member this.Draw (spriteBatch: SpriteBatch) =
            spriteBatch.Draw(
                texture,
                Vector2(80f, 15f),
                Rectangle(20,23, 320, 224),
                Color.White,
                0f,
                Vector2.Zero,
                2f,
                SpriteEffects.None,
                0f)
