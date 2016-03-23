using Jungletribes_Common;
using System;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jungletribes
{
    public class CircleElement : Element
    {
        private static Texture2D Circle_Texture { get; set; }

        private Texture2D C1;
        private Texture2D C2;
        public override Vector2 center
        {
            get
            {
                return new Vector2(position.X + Circle_Texture.Width / 2, position.Y + Circle_Texture.Height / 2);
            }
        }
        public Circle collision_circle;

        public CircleElement()
        {
            commands = EnumMoveCommand.None;
            position = new Vector2(0, 0);
            speed = new Vector2(100, 100);

            //Circle_Texture = CreateCircle(100);

            if (Circle_Texture == null)
                using (var stream = TitleContainer.OpenStream("Content/autocollant-tete-de-mort.jpg"))
                {
                    Circle_Texture = Texture2D.FromStream(JungleTribesGame.Instance.GraphicsDevice, stream);
                }


            collision_circle = new Circle(center, Circle_Texture.Width / 2);
            C1 = CreateCircle(150);
            C2 = CreateCircle(5);
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 MoveToDo = new Vector2(0, 0);
            if (commands.HasFlag(EnumMoveCommand.Left))
            {
                MoveToDo.X -= this.speed.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (commands.HasFlag(EnumMoveCommand.Right))
            {
                MoveToDo.X += this.speed.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (commands.HasFlag(EnumMoveCommand.Up))
            {
                MoveToDo.Y -= this.speed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (commands.HasFlag(EnumMoveCommand.Bottom))
            {
                MoveToDo.Y += this.speed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (commands.HasFlag(EnumMoveCommand.Horizontal) && commands.HasFlag(EnumMoveCommand.Vertical))
            {
                MoveToDo.X = (float)(Math.Cos(45) * MoveToDo.X);
                MoveToDo.Y = (float)(Math.Sin(45) * MoveToDo.Y);
            }
            if (commands.HasFlag(EnumMoveCommand.RightClick))
            {
                MoveToDo = Helper.getVectorToPoint(center, Pipe.MouseClick, this.speed * (float)gameTime.ElapsedGameTime.TotalSeconds);//
            }

            this.position += MoveToDo;

            collision_circle.pos = center;
        }

        public override void Draw(GameTime gameTime)
        {
           /* if (!Circle.CircleToCircle(collision_circle, new Circle(Pipe.MousePosition, 5)))
            {*/
                JungleTribesGame.Instance.spriteBatch.Draw(Circle_Texture, new Rectangle((int)this.position.X, (int)this.position.Y, Circle_Texture.Width, Circle_Texture.Height), new Rectangle(0, 0, Circle_Texture.Width, Circle_Texture.Height), Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.9f);
                // JungleTribesGame.Instance.spriteBatch.Draw(C1, new Rectangle((int)this.position.X, (int)this.position.Y, C1.Width, C1.Height), Color.White);
          //  }

            JungleTribesGame.Instance.spriteBatch.Draw(C2, new Rectangle((int)Pipe.MousePosition.X-C2.Width/2, (int)Pipe.MousePosition.Y - C2.Height / 2, C2.Width, C2.Height), new Rectangle(0, 0, C2.Width, C2.Height), Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 1.0f);
        }

        public override void LoadContent()
        {
            isInit = true;
        }

        public override void UnloadContent()
        {
            isInit = false;
        }

        public override void Synchronise()
        {
            throw new NotImplementedException();
        }

        public override void Synchronise(NetIncomingMessage var)
        {
            throw new NotImplementedException();
        }



        public Texture2D CreateCircle(int radius)
        {
            int outerRadius = radius * 2 + 2; // So circle doesn't go out of bounds
            Texture2D texture = new Texture2D(JungleTribesGame.Instance.GraphicsDevice, outerRadius, outerRadius);

            Color[] data = new Color[outerRadius * outerRadius];

            // Colour the entire texture transparent first.
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.Transparent;

            // Work out the minimum step necessary using trigonometry + sine approximation.
            double angleStep = 1f / radius;

            for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
            {
                // Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
                int x = (int)Math.Round(radius + radius * Math.Cos(angle));
                int y = (int)Math.Round(radius + radius * Math.Sin(angle));

                data[y * outerRadius + x + 1] = Color.White;
            }

            //width
            for (int i = 0; i < outerRadius; i++)
            {
                int yStart = -1;
                int yEnd = -1;


                //loop through height to find start and end to fill
                for (int j = 0; j < outerRadius; j++)
                {

                    if (yStart == -1)
                    {
                        if (j == outerRadius - 1)
                        {
                            //last row so there is no row below to compare to
                            break;
                        }

                        //start is indicated by Color followed by Transparent
                        if (data[i + (j * outerRadius)] == Color.White && data[i + ((j + 1) * outerRadius)] == Color.Transparent)
                        {
                            yStart = j + 1;
                            continue;
                        }
                    }
                    else if (data[i + (j * outerRadius)] == Color.White)
                    {
                        yEnd = j;
                        break;
                    }
                }

                //if we found a valid start and end position
                if (yStart != -1 && yEnd != -1)
                {
                    //height
                    for (int j = yStart; j < yEnd; j++)
                    {
                        data[i + (j * outerRadius)] = Color.Red;
                    }
                }
            }

            texture.SetData(data);
            return texture;
        }

    }
}
