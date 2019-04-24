//--------------------------------------------------------------------------------------------
//File:   Items.cs
//Desc:   This class contains logic for the AI characters.
//---------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Binder.Environment
{

    public class AI : MovableCharacter, ISerialization<AI> 
    {
        public bool isAttacking = false;
        private int count = 0;
        private bool wait;
        private int direc;
        int attackTime = 100;
        public AI(int health, int damage, int speed, int x, int y)
        {
            Health = health;
            Damage = damage;
            Speed = speed;
            X = x;
            Y = y;
            Width = 120;
            Length = 120;
            direc = -1;
            wait = false;
            PictureName = "/Sprites/PsiZetaFront.png";
        }

        public void ChangeYFrames(int changeInY, Game game)
        {
            if (changeInY > 0)
            {
                howLongUp = 0;
                howLongRight = 0;
                howLongLeft = 0;
                if (isAttacking)
                {
                    PictureName = "/Sprites/PsiZetaFrontWhip.png";
                    game.Marcus.Health -= Damage;
                    isAttacking = false;
                }
                else if (attackTime > 10)
                {
                    if (howLongDown >= 0 && howLongDown < 10)
                    {
                        PictureName = "/Sprites/PsiZetaFront.png";
                    }
                    else if (howLongDown >= 10 && howLongDown < 20)
                    {
                        PictureName = "/Sprites/PsiZetaFront1.png";
                    }
                    else if (howLongDown >= 20 && howLongDown < 30)
                    {
                        PictureName = "/Sprites/PsiZetaFront.png";
                    }
                    else if (howLongDown >= 30 && howLongDown < 40)
                    {
                        PictureName = "/Sprites/PsiZetaFront2.png";
                    }
                    else
                    {
                        howLongDown = 0;
                    }
                }
                howLongDown++;
            }
            else if (changeInY < 0)
            {
                howLongDown = 0;
                howLongRight = 0;
                howLongLeft = 0;
                if (isAttacking)
                {
                    PictureName = "/Sprites/PsiZetaBackWhip.png";
                    game.Marcus.Health -= Damage;
                    isAttacking = false;
                }
                else if (attackTime > 10)
                {
                    if (howLongUp >= 0 && howLongUp < 10)
                    {
                        PictureName = "/Sprites/PsiZetaBack.png";
                    }
                    else if (howLongUp >= 10 && howLongUp < 20)
                    {
                        PictureName = "/Sprites/PsiZetaBack1.png";
                    }
                    else if (howLongUp >= 20 && howLongUp < 30)
                    {
                        PictureName = "/Sprites/PsiZetaBack.png";
                    }
                    else if (howLongUp >= 30 && howLongUp < 40)
                    {
                        PictureName = "/Sprites/PsiZetaBack2.png";
                    }
                    else
                    {
                        howLongUp = 0;
                    }
                }
            }
        }
        public void ChangeXFrames(int changeInX, Game game)
        {
            if (changeInX > 0)
            {
                howLongDown = 0;
                howLongUp = 0;
                howLongLeft = 0;
                if (isAttacking)
                {
                    PictureName = "/Sprites/PsiZetaRightWhip.png";
                    game.Marcus.Health -= Damage;
                    isAttacking = false;
                }
                else if (attackTime > 10)
                {
                    if (howLongRight >= 0 && howLongRight < 10)
                    {
                        PictureName = "/Sprites/PsiZetaRight.png";
                    }
                    else if (howLongRight >= 10 && howLongRight < 20)
                    {
                        PictureName = "/Sprites/PsiZetaRight1.png";
                    }
                    else if (howLongRight >= 20 && howLongRight < 30)
                    {
                        PictureName = "/Sprites/PsiZetaRight.png";
                    }
                    else if (howLongRight >= 30 && howLongRight < 40)
                    {
                        PictureName = "/Sprites/PsiZetaRight2.png";
                    }
                    else
                    {
                        howLongRight = 0;
                    }
                }

            }
            else if (changeInX < 0)
            {
                howLongDown = 0;
                howLongUp = 0;
                howLongRight = 0;
                if (isAttacking)
                {
                    PictureName = "/Sprites/PsiZetaLeftWhip.png";
                    game.Marcus.Health -= Damage;
                    isAttacking = false;
                }
                else if (attackTime > 10)
                {
                    if (howLongLeft >= 0 && howLongLeft < 10)
                    {
                        PictureName = "/Sprites/PsiZetaLeft.png";
                    }
                    else if (howLongLeft >= 10 && howLongLeft < 20)
                    {
                        PictureName = "/Sprites/PsiZetaLeft1.png";
                    }
                    else if (howLongLeft >= 20 && howLongLeft < 30)
                    {
                        PictureName = "/Sprites/PsiZetaLeft.png";
                    }
                    else if (howLongLeft >= 30 && howLongLeft < 40)
                    {
                        PictureName = "/Sprites/PsiZetaLeft2.png";
                    }
                    else
                    {
                        howLongLeft = 0;
                    }
                    howLongLeft++;
                }
            }
        }
        public void Patrol(Game game)
        {
            count++;
            if (count > 20)
            {
                if (wait)
                {
                    wait = false;
                    direc = -1;
                    count = 0;
                }
                else
                {
                    Random rand = new Random();
                    direc = rand.Next(4);
                    wait = true;
                    count = 0;
                }
            }
            if (direc == 0)
            {
                //west
                if (IsNotWall(game.CurBuilding, -Speed / 2, 0))
                {
                    X -= Speed;
                    ChangeXFrames(-Speed, game);
                }
                else
                {
                    X += Speed;
                    ChangeXFrames(Speed, game);
                }
            }
            else if (direc == 1)
            {
                //east
                if (IsNotWall(game.CurBuilding, (Speed / 2), 0))
                {
                    X += Speed;
                    ChangeXFrames(Speed, game);
                }
                else
                {
                    X -= Speed;
                    ChangeXFrames(-Speed, game);
                }
            }
            else if (direc == 2)
            {
                //north
                if (IsNotWall(game.CurBuilding, 0, (-Speed / 2)))
                {
                    Y -= Speed;
                    ChangeYFrames(-Speed, game);
                }
                else
                {
                    Y += Speed;
                    ChangeYFrames(Speed, game);
                }
            }
            else if (direc == 3)
            {
                //south
                if (IsNotWall(game.CurBuilding, 0, (Speed / 2)))
                {
                    Y += Speed / 2;
                    ChangeYFrames(Speed, game);
                }
                else
                {
                    Y -= Speed / 2;
                    ChangeYFrames(-Speed, game);
                }
            }
        }

        public void Chase(Game game)
        {
            if (game.Marcus.X < X)
            {
                if (IsNotWall(game.CurBuilding, (-Speed / 2), 0))
                {
                    X -= Speed / 2;
                    ChangeXFrames(-Speed / 2, game);
                }
            }
            else if (game.Marcus.X > X)
            {
                if (IsNotWall(game.CurBuilding, (Speed / 2), 0))
                {
                    X += Speed / 2;
                    ChangeXFrames(Speed / 2, game);
                }
            }
            if (game.Marcus.Y < Y)
            {
                if (IsNotWall(game.CurBuilding, 0, (-Speed / 2)))
                {
                    Y -= Speed / 2;
                    ChangeYFrames(-Speed / 2, game);
                }
            }
            else if (game.Marcus.Y > Y)
            {
                if (IsNotWall(game.CurBuilding,0, (Speed / 2)))
                {
                    Y += Speed / 2;
                    ChangeYFrames(Speed / 2, game);
                }
            }
        }

        public void Move(Game game)
        {
            if (Game.isPaused != true)
            {
                if ((400 * 400) >= (((X - game.Marcus.X) * (X - game.Marcus.X)) + ((Y - game.Marcus.Y) * (Y - game.Marcus.Y))))
                {
                    Chase(game);
                    if ((150 * 150) >= (((X - game.Marcus.X) * (X - game.Marcus.X)) + ((Y - game.Marcus.Y) * (Y - game.Marcus.Y))))
                    {
                        if (game.IsCheatOn != true)
                        {
                            attackTime++;
                            if (attackTime > 100)
                            {
                                isAttacking = true;
                                attackTime = 0;
                            }
                        }
                    }
                    else
                        attackTime = 100;
                }
                else
                {
                    Patrol(game);
                }
            }
        }

        //Turns the AI object into a string
        public string Serialize()
        {
            return string.Format("AI?6,HEALTH!{0},DAMAGE!{1},SPEED!{2},X!{3},Y!{4},PICTURENAME!{5}", Health, Damage, Speed, X, Y, PictureName);
        }

        //Updates an AI object using a string
        public AI Deserialize(string obj)
        {
            List<string> properties = new List<string>(obj.Split(',', '!', '#', ':', '?', ';'));

            for (int i = 0; i < properties.Count; i += 2)
            {
                switch (properties[i])
                {
                    case "HEALTH":
                        Health = int.Parse(properties[i + 1]);
                        break;
                    case "DAMAGE":
                        Damage = int.Parse(properties[i + 1]);
                        break;
                    case "SPEED":
                        Speed = int.Parse(properties[i + 1]);
                        break;
                    case "X":
                        X = int.Parse(properties[i + 1]);
                        break;
                    case "Y":
                        Y = int.Parse(properties[i + 1]);
                        break;
                    case "PICTURENAME":
                        PictureName = properties[i + 1];
                        break;
                }
            }

            return this;
        }
    }
}
