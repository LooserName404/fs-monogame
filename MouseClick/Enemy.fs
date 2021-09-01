namespace MouseClick

module Enemy =
    open Microsoft.Xna.Framework
    open Microsoft.Xna.Framework.Graphics
    open Microsoft.Xna.Framework.Input
    open MonoLink
    
    type Enemy(texture: Texture2D) =
        [<DefaultValue>] val mutable RunAnimation : FrameAnimation
        [<DefaultValue>] val mutable DeathAnimation: FrameAnimation
        [<DefaultValue>] val mutable CurrentAnimation : FrameAnimation
      
        let mutable xVelocity = 2f
        
        let mutable layer = 3f
        
        member private this.texture = texture
        
        member this.Set velocity l =
            xVelocity <- velocity
            layer <- l
           
        member val IsEnabled = true with get, set 

        member this.Update gameTime =
            if layer = 0f then
                this.CurrentAnimation.Scale <- Vector2(2f)
            elif layer = 1f then
                this.CurrentAnimation.Scale <- Vector2(1.5f)
            else
                this.CurrentAnimation.Scale <- Vector2(1f)
            
            this.CurrentAnimation.Position <- this.CurrentAnimation.Position + Vector2(xVelocity, 0f)
            if xVelocity < 0f then
                this.CurrentAnimation.Effects <- SpriteEffects.FlipHorizontally
            
            this.CurrentAnimation.Update gameTime
            
            let state = Mouse.GetState()
            
            if state.LeftButton = ButtonState.Pressed then
                if this.CurrentAnimation.GetBounds().Contains(state.Position) then
                    this.CurrentAnimation <- this.DeathAnimation
                    this.CurrentAnimation.Position <- this.RunAnimation.Position
                    xVelocity <- 0f
                    
            if this.CurrentAnimation = this.DeathAnimation && this.CurrentAnimation.IsFinished then
                this.IsEnabled <- false
            
        member this.Draw gameTime spriteBatch =
            this.CurrentAnimation.Draw (gameTime, spriteBatch)
            
