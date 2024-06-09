using Mosa.Kernel.x86;
using System;
using Mosa.External.x86.Driver;

namespace MOSA1.Apps
{
    class Snake : Window
    {
        int aWidth = 10;
        int aHeight = 10;

        enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        class SnakeNode
        {
            public int X;
            public int Y;
        }

        Direction Dir = Direction.Right;
        SnakeNode[] SnakeNodes;

        int Max = 20;

        int FoodX = -1;
        int FoodY = -1;

        Random random;

        Window MessageBox;

        public Snake()
        {
            Title = "Snake";

            MessageBox = new MessageBox() { Visible = false, Info = "Game Over!", Width = 150, Height = 16, X = 300, Y = 350 };

            System.Windows.Add(MessageBox);

            SnakeNodes = new SnakeNode[Max];

            random = new Random();

            Add(new SnakeNode() { X = aWidth / 2, Y = aHeight / 2 });

            NewFood();
        }

        int Count = 0;
        void Add(SnakeNode snakeNode)
        {
            SnakeNodes[Count] = snakeNode;
            Count++;
        }

        public override void UIUpdate()
        {
            IsEatBody();
            if (Count == Max)
            {
                NewGame();
            }

            Refresh();
            if(!MessageBox.Actived)
            {
                MessageBox.Visible = false;
                Control();
            }

            Display();
        }

        public override void InputUpdate()
        {
            switch (PS2Keyboard.GetKeyPressed())
            {
                case KeyCode.W:
                    Dir = Direction.Up;
                    break;
                case KeyCode.A:
                    Dir = Direction.Left;
                    break;
                case KeyCode.S:
                    Dir = Direction.Down;
                    break;
                case KeyCode.D:
                    Dir = Direction.Right;
                    break;
            }
        }

        ulong W = 0;

        private void Control()
        {
            if (PIT.Tick < W + 200)
            {
                return;
            }
            W = PIT.Tick;

            switch (Dir)
            {
                case Direction.Up:
                    if (SnakeNodes[Count - 1].Y - 1 >= 0)
                    {
                        SetChildren();
                        SnakeNodes[Count - 1].Y--;
                    }

                    if (SnakeNodes[Count - 1].X == FoodX && SnakeNodes[Count - 1].Y == FoodY)
                    {
                        if (Count < Max)
                            Add(new SnakeNode() { X = SnakeNodes[Count - 1].X, Y = SnakeNodes[Count - 1].Y - 1 });
                        NewFood();
                    }

                    break;
                case Direction.Down:
                    if (SnakeNodes[Count - 1].Y + 1 < aHeight)
                    {
                        SetChildren();
                        SnakeNodes[Count - 1].Y++;
                    }

                    if (SnakeNodes[Count - 1].X == FoodX && SnakeNodes[Count - 1].Y == FoodY)
                    {
                        if (Count < Max)
                            Add(new SnakeNode() { X = SnakeNodes[Count - 1].X, Y = SnakeNodes[Count - 1].Y + 1 });
                        NewFood();
                    }

                    break;
                case Direction.Left:
                    if (SnakeNodes[Count - 1].X - 1 >= 0)
                    {
                        SetChildren();
                        SnakeNodes[Count - 1].X--;
                    }

                    if (SnakeNodes[Count - 1].X == FoodX && SnakeNodes[Count - 1].Y == FoodY)
                    {
                        if (Count < Max)
                            Add(new SnakeNode() { X = SnakeNodes[Count - 1].X - 1, Y = SnakeNodes[Count - 1].Y });
                        NewFood();
                    }

                    break;
                case Direction.Right:
                    if (SnakeNodes[Count - 1].X + 1 < aWidth)
                    {
                        SetChildren();
                        SnakeNodes[Count - 1].X++;
                    }

                    if (SnakeNodes[Count - 1].X == FoodX && SnakeNodes[Count - 1].Y == FoodY)
                    {
                        if (Count < Max)
                            Add(new SnakeNode() { X = SnakeNodes[Count - 1].X + 1, Y = SnakeNodes[Count - 1].Y });
                        NewFood();
                    }

                    break;
            }
        }

        private void SetChildren()
        {
            for (int i = 0; i < Count; i++)
            {
                if (i + 1 <= Count - 1)
                {
                    SnakeNodes[i].X = SnakeNodes[i + 1].X;
                    SnakeNodes[i].Y = SnakeNodes[i + 1].Y;
                }
            }
        }

        void NewFood()
        {
            int newX = random.Next(2, aWidth - 3);
            int newY = random.Next(2, aHeight - 3);
            for (int i = 0; i < Count; i++)
            {
                if (SnakeNodes[i].X == newX && SnakeNodes[i].Y == newY)
                {
                    i = 0;
                    newX = random.Next(2, aWidth - 3);
                    newY = random.Next(2, aHeight - 3);
                }
            }

            FoodX = newX;
            FoodY = newY;
        }

        void IsEatBody()
        {
            for (int i = 0; i < Count; i++)
            {
                for (int k = 0; k < Count; k++)
                {
                    if (i != k)
                    {
                        if (SnakeNodes[i].X == SnakeNodes[k].X && SnakeNodes[i].Y == SnakeNodes[k].Y)
                        {
                            NewGame();
                            MessageBox.X = this.X + 50;
                            MessageBox.Y = this.Y + 50;
                            MessageBox.Visible = true;
                            MessageBox.Active();
                        }
                    }
                }
            }
        }

        private void NewGame()
        {
            Count = 1;
        }

        void Refresh()
        {
            System.Graphics.DrawFilledRectangle(0x9FAE87, X, Y, Width, Height);
            for (int h = 0; h < aHeight; h++)
            {
                for (int w = 0; w < aWidth; w++)
                {
                    System.Graphics.DrawFilledRectangle(0x98A682, this.X + (w * SizePerBlock) + 3, this.Y + (h * SizePerBlock) + 3, SizePerBlock - 6, SizePerBlock - 6);
                }
            }
        }

        int SizePerBlock = 15;

        void Display()
        {

            for (int i = 0; i < Count; i++)
            {
                if (i == Count - 1)
                {
                    System.Graphics.DrawRectangle(0xFF262627, this.X + (SnakeNodes[i].X * SizePerBlock), this.Y + (SnakeNodes[i].Y * SizePerBlock), SizePerBlock, SizePerBlock, 2);

                    switch (Dir)
                    {
                        case Direction.Up:
                            System.Graphics.DrawFilledRectangle(0xFF262627, this.X + (SnakeNodes[i].X * SizePerBlock) + SizePerBlock / 5, this.Y + (SnakeNodes[i].Y * SizePerBlock) + SizePerBlock / 5, SizePerBlock / 5, SizePerBlock / 5);
                            System.Graphics.DrawFilledRectangle(0xFF262627, this.X + (SnakeNodes[i].X * SizePerBlock) + SizePerBlock - ((SizePerBlock / 5) * 2), this.Y + (SnakeNodes[i].Y * SizePerBlock) + SizePerBlock / 5, SizePerBlock / 5, SizePerBlock / 5);
                            break;
                        case Direction.Down:
                            System.Graphics.DrawFilledRectangle(0xFF262627, this.X + (SnakeNodes[i].X * SizePerBlock) + SizePerBlock / 5, this.Y + (SnakeNodes[i].Y * SizePerBlock) + SizePerBlock - ((SizePerBlock / 5) * 2), SizePerBlock / 5, SizePerBlock / 5);
                            System.Graphics.DrawFilledRectangle(0xFF262627, this.X + (SnakeNodes[i].X * SizePerBlock) + SizePerBlock - ((SizePerBlock / 5) * 2), this.Y + (SnakeNodes[i].Y * SizePerBlock) + SizePerBlock - ((SizePerBlock / 5) * 2), SizePerBlock / 5, SizePerBlock / 5);
                            break;
                        case Direction.Left:
                            System.Graphics.DrawFilledRectangle(0xFF262627, this.X + (SnakeNodes[i].X * SizePerBlock) + SizePerBlock / 5, this.Y + (SnakeNodes[i].Y * SizePerBlock) + SizePerBlock / 5, SizePerBlock / 5, SizePerBlock / 5);
                            System.Graphics.DrawFilledRectangle(0xFF262627, this.X + (SnakeNodes[i].X * SizePerBlock) + SizePerBlock / 5, this.Y + (SnakeNodes[i].Y * SizePerBlock) + SizePerBlock - ((SizePerBlock / 5) * 2), SizePerBlock / 5, SizePerBlock / 5);
                            break;
                        case Direction.Right:
                            System.Graphics.DrawFilledRectangle(0xFF262627, this.X + (SnakeNodes[i].X * SizePerBlock) + SizePerBlock - ((SizePerBlock / 5) * 2), this.Y + (SnakeNodes[i].Y * SizePerBlock) + SizePerBlock / 5, SizePerBlock / 5, SizePerBlock / 5);
                            System.Graphics.DrawFilledRectangle(0xFF262627, this.X + (SnakeNodes[i].X * SizePerBlock) + SizePerBlock - ((SizePerBlock / 5) * 2), this.Y + (SnakeNodes[i].Y * SizePerBlock) + SizePerBlock - (SizePerBlock / 5 * 2), SizePerBlock / 5, SizePerBlock / 5);
                            break;
                    }
                }
                else
                {
                    System.Graphics.DrawRectangle(0xFF262627, this.X + (SnakeNodes[i].X * SizePerBlock), this.Y + (SnakeNodes[i].Y * SizePerBlock), SizePerBlock, SizePerBlock, 2);
                    System.Graphics.DrawFilledRectangle(0xFF262627, this.X + (SnakeNodes[i].X * SizePerBlock) + 3, this.Y + (SnakeNodes[i].Y * SizePerBlock) + 3, SizePerBlock - 6, SizePerBlock - 6);
                }
            }

            //Food
            System.Graphics.DrawRectangle(0xFF262627, this.X + (FoodX * SizePerBlock), this.Y + (FoodY * SizePerBlock), SizePerBlock, SizePerBlock, 2);
            System.Graphics.DrawFilledRectangle(0xFF262627, this.X + (FoodX * SizePerBlock) + 3, this.Y + (FoodY * SizePerBlock) + 3, SizePerBlock - 6, SizePerBlock - 6);
        }
    }
}
