﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGP;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace WGP.Gui
{
    public class Button : Widget
    {
        private Text Label { get; set; }
        private RectangleShape Back { get; set; }
        private Vertex[] Border { get; set; }
        public string Title
        {
            get => Label.DisplayedString;
            set => Label.DisplayedString = value;
        }

        public Button() : base()
        {
            if (!Init.IsInitialized)
                throw new Init.NotInitializedException();
            Label = new Text();
            Back = new RectangleShape();
            Border = new Vertex[8];

            Label.Font = Init.Font;
            Label.CharacterSize = Init.TextSize;

            InternUpdate();
        }



        internal override void Draw(RenderTarget target, Vector2f decal)
        {
            Transformable tr  = new Transformable();
            tr.Position = decal;
            target.Draw(Back, new RenderStates(tr.Transform));
            target.Draw(Border, PrimitiveType.Lines, new RenderStates(tr.Transform));
            target.Draw(Label, new RenderStates(tr.Transform));
        }

        internal override Vector2f GetMinimumSize()
        {
            Vector2f result = new Vector2f(Label.FindCharacterPos((uint)Label.DisplayedString.Count()).X, Init.TextSize);
            result += Padding * 2;
            result += new Vector2f(20, 10);
            return result;
        }

        protected override void InternUpdate()
        {
            Back.Size = new Vector2f(ReservedSpace.Width, ReservedSpace.Height) - Padding * 2;
            Back.Position = new Vector2f(ReservedSpace.Left, ReservedSpace.Top) + Padding;
            Border[0].Position = new Vector2f((int)ReservedSpace.Left + .5f, (int)ReservedSpace.Top + .5f) + Padding;
            Border[1].Position = Border[0].Position + new Vector2f((int)ReservedSpace.Width - Padding.X * 2, 0);
            Border[2].Position = Border[1].Position;
            Border[3].Position = Border[2].Position + new Vector2f(0, (int)ReservedSpace.Height - Padding.Y * 2);
            Border[4].Position = Border[3].Position;
            Border[5].Position = Border[4].Position + new Vector2f(-(int)ReservedSpace.Width + Padding.X * 2, 0);
            Border[6].Position = Border[5].Position;
            Border[7].Position = Border[6].Position + new Vector2f(0, -(int)ReservedSpace.Height + Padding.Y * 2);
            Label.Position = new Vector2f((int)(ReservedSpace.Width / 2 - Label.FindCharacterPos((uint)Label.DisplayedString.Count()).X / 2 + ReservedSpace.Left),
                                          (int)(ReservedSpace.Height / 2 + ReservedSpace.Top - Init.TextSize / 2));

            if (Pressed)
            {
                Back.FillColor = Init.ControlDark;
                Label.Color = Init.TextLight;
                for (int i = 0; i < 8; i++)
                    Border[i].Color = Init.BorderDark;
            }
            else if (MouseOnWidget)
            {
                Back.FillColor = Init.ControlMedium;
                Label.Color = Init.TextDark;
                for (int i = 0; i < 8; i++)
                    Border[i].Color = Init.BorderMedium;
            }
            else
            {
                Back.FillColor = Init.ControlLight;
                Label.Color = Init.TextDark;
                for (int i = 0; i < 8; i++)
                    Border[i].Color = Init.BorderLight;
            }
        }
    }
}
