



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;





namespace WindowsGame1
{
    public enum Element
    {
        earth, water, air, fire
    }

    /*Note on collision categories (Arbitrarily defined based on what was already in the code):
     * Category 1: Wall 
     * Category 2: Floor
     * Category 3: Player
     * Category 4: Fire Cone
     * Category 5: Water Beam
     * Category 6: Rock
     * Category 7: Midair Rock
     * Category 8: Tornado
     */




    public class Player : Body
    {
        public float damage = 1;
        float playerSize = 10;
        Texture2D playerTex;
        Controller playerController;
        public Fixture playerFixture;

        Player lastTouched;
        

        //Attack stuff
        Element currentEquip = Element.fire;
        float leftcharge = 0;
        float rightcharge = 0;

        float cooldown = 0;

        const int minCharge = 10;
        const int maxCharge = 150;

        //Player Stats
        private const float playerMass = 10f;
        private const float playerRotate = 0.15f;
        private const float MetersInPx = 64f;
        private const float playerAccel = 0.4f;

        //Flags
        public bool dead = false;
        bool fallingFlag = true;
        int index;

        KingsOfAlchemy game;

        SpriteFont font;
        SpriteFont font2;

        //HUD
        Texture2D manaBarFire;
        Texture2D manaBarWater;
        Texture2D manaBarEarth;
        Texture2D manaBarAir;

        public Player(World gameWorld, int playerNum, KingsOfAlchemy game, Vector2 offset) : base(gameWorld)
        {
            //Merged this with loadContent for simplicity
            this.game = game;
            this.index = playerNum;
            manaBarFire = game.Content.Load<Texture2D>("HealthBars/Yellow");
            manaBarWater = game.Content.Load<Texture2D>("HealthBars/Blue");
            manaBarEarth = game.Content.Load<Texture2D>("HealthBars/Green");
            manaBarAir = game.Content.Load<Texture2D>("HealthBars/Teal");
            font = game.Content.Load<SpriteFont>("Font");
            font2 = game.Content.Load<SpriteFont>("Font2");
            switch (playerNum)
            {
                //Positions will likely need to be changed based on world size
                case 1:
                    playerController = new Controller(PlayerIndex.One);
                    playerTex = game.Content.Load<Texture2D>("Player1");
                    Position = new Vector2(60, 60) + offset;
                    break;
                case 2:
                    playerController = new Controller(PlayerIndex.Two);
                    playerTex = game.Content.Load<Texture2D>("Player2");
                    Position = new Vector2(400, 60) + offset;
                    break;
                case 3:
                    playerController = new Controller(PlayerIndex.Three);
                    playerTex = game.Content.Load<Texture2D>("Player3");
                    Position = new Vector2(60, 400) + offset;
                    break;
                case 4:
                    playerController = new Controller(PlayerIndex.Four);
                    playerTex = game.Content.Load<Texture2D>("Player4");
                    Position = new Vector2(400, 400) + offset;
                    break;
            }

            // load player sprite
            Vector2 playerOrigin;
            playerOrigin.X = playerTex.Width / 2;
            playerOrigin.Y = playerTex.Height / 2;

            playerFixture = FixtureFactory.AttachCircle(playerSize, 1, this);
            playerFixture.Body.BodyType = BodyType.Dynamic;

            playerFixture.CollisionCategories = Category.Cat3;
            playerFixture.CollidesWith = Category.Cat1 | Category.Cat2 | Category.Cat3 | Category.Cat4 | Category.Cat5 | Category.Cat6 | Category.Cat8;

            playerFixture.OnCollision += playerOnCollision;

            playerFixture.Restitution = 0.9f;   //Energy Retained from bouncing
            playerFixture.Friction = 0.01f;      //friction with other objects

            lastTouched = this;

        }

        public bool playerOnCollision(Fixture fix1, Fixture fix2, Contact con)
        {
            Attack otherAttack;
            switch(fix2.CollisionCategories){
                case Category.Cat1:                         //Wall
                    return true;
                case Category.Cat2:                         //Floor
                    fallingFlag = false;
                    return false;
                case Category.Cat3:                         //Player
                    return true;
                case Category.Cat4:                         //Fire spray
                    otherAttack = (Attack)fix2.Body;
                    if (otherAttack.owner != this)
                    {
                        damage += 0.05f;
                        lastTouched = otherAttack.owner;
                    }
                    return false;
                case Category.Cat5:                         //Water beam
                    otherAttack = (Attack)fix2.Body;
                    if (otherAttack.owner != this)
                    {
                        damage += otherAttack.damage/100;
                        ApplyLinearImpulse(Vector2.Normalize(otherAttack.LinearVelocity) * otherAttack.impulse * damage);
                        lastTouched = otherAttack.owner;
                        return true;
                    }
                    return false;
                case Category.Cat6:                         //Rock
                    otherAttack = (Attack)fix2.Body;
                    damage += 0.05f;
                    LinearVelocity = LinearVelocity + Vector2.Normalize(fix1.Body.Position - Position) * 2 * (damage);
                    lastTouched = otherAttack.owner;
                    return false;
                case Category.Cat8:                         //Tornado
                    otherAttack = (Attack)fix2.Body;
                    if (otherAttack.owner != this)
                    {
                        Vector2 acceleration = Position - fix2.Body.Position;
                        float temp = acceleration.X;
                        acceleration.X = acceleration.Y;
                        acceleration.Y = temp;
                        acceleration.Normalize();
                        LinearVelocity += acceleration;
                    }
                    lastTouched = otherAttack.owner;
                    return false;
                default:
                    return false;

            }
        }

        public void Update()
        {
            AngularVelocity = 0;
            LinearVelocity = LinearVelocity * 0.9f;
            if (dead) return;
            goDoThis elementChange = playerController.getEquipChange();
            switch (elementChange)
            {
                case goDoThis.equipAir:
                    currentEquip = Element.air;
                    break;
                case goDoThis.equipEarth:
                    currentEquip = Element.earth;
                    break;
                case goDoThis.equipFire:
                    currentEquip = Element.fire;
                    break;
                case goDoThis.equipWater:
                    currentEquip = Element.water;
                    break;
                case goDoThis.rotateEquipLeft:
                    if (currentEquip == Element.air)
                        currentEquip = Element.fire;
                    else if (currentEquip == Element.fire)
                        currentEquip = Element.water;
                    else if (currentEquip == Element.water)
                        currentEquip = Element.earth;
                    else
                        currentEquip = Element.air;
                    break;
                case goDoThis.rotateEquipRight:
                    if (currentEquip == Element.air)
                        currentEquip = Element.earth;
                    else if (currentEquip == Element.fire)
                        currentEquip = Element.air;
                    else if (currentEquip == Element.water)
                        currentEquip = Element.fire;
                    else
                        currentEquip = Element.water;
                    break;

            }
            //if(elementChange = goDoThis.equipAir && 
            // Call methods to get info from controller
            // Do Stuff (this will include methods to shoot attacks)
            Vector2 newv = new Vector2();
            if (playerController.getRightCharge())
            {
                if(rightcharge < 150)
                    rightcharge += 1;
            }
            if (playerController.getLeftCharge())
            {
                if(leftcharge < 150)
                    leftcharge += 1;
            }
            if (cooldown > 0) cooldown -= 1;
            if (!playerController.getRightCharge() && rightcharge > 10 && cooldown == 0)
            {
                switch (currentEquip)
                {
                    case Element.fire:
                        if (rightcharge > 60)
                        {
                            attackFire((int)rightcharge);
                        }
                        rightcharge = 0;

                        break;
                    case Element.water:
                        game.attacks.Add(new Water(playerController.getRotation2(), Position + new Vector2(10, 10), game, this, rightcharge));
                        rightcharge -= 20;
                        break;
                    case Element.earth:
                        game.attacks.Add(new Water(playerController.getRotation2(), Position, game, this, rightcharge));
                        rightcharge = 0;
                        break;
                    case Element.air:
                        if (rightcharge > 60)
                        {
                            game.attacks.Add(new Air(playerController.getRotation2(), Position, game, this, rightcharge));
                        }
                        rightcharge = 0;
                        break;
                }
                cooldown += 10;
            }
            float rotation = playerController.getRotation();
            newv = playerController.getMovement();
            ApplyLinearImpulse(newv*250);
            //This is set to false in the collision detection if the player is safely standing on a tile, and set to false after each update
            if (fallingFlag)
            {
                CollisionCategories = Category.None;
                dead = true;
            }
            fallingFlag = true;
        }
        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (dead) return;
            //Draw health bars
            Vector2 offset;
            if (index == 1)
                offset = new Vector2(75, 30);
            else if (index == 2)
                offset = new Vector2(game.Window.ClientBounds.Width - 75, 30);
            else if(index == 3)
                offset = new Vector2(75, game.Window.ClientBounds.Height - 90);
            else
                offset = new Vector2(game.Window.ClientBounds.Width - 75, game.Window.ClientBounds.Height - 90);

            Texture2D manaBar;
            if (currentEquip == Element.fire)
                manaBar = manaBarFire;
            else if (currentEquip == Element.water)
                manaBar = manaBarWater;
            else if (currentEquip == Element.earth)
                manaBar = manaBarEarth;
            else
                manaBar = manaBarAir;

            spriteBatch.Draw(manaBar, new Rectangle((int)offset.X - (int)rightcharge/2, (int)offset.Y + 50, (int)rightcharge, manaBar.Height), Color.White);

            spriteBatch.DrawString(font, Math.Round(damage * 100 - 100).ToString() + "%", offset + new Vector2(-font.MeasureString(Math.Round(damage * 100).ToString() + "%").X / 2, -20), Color.White);
            spriteBatch.DrawString(font2, "Charge", offset + new Vector2(-font.MeasureString("Charge").X/2, font.MeasureString("Charge").Y), Color.White);

            //Draw player
            Vector2 playerOrigin;
            playerOrigin.X = playerTex.Width/2;
            playerOrigin.Y = playerTex.Height/2;
          

            spriteBatch.Draw(playerTex,
                new Rectangle((int)Position.X + 10, (int)Position.Y + 10, 
                playerTex.Width, playerTex.Height),
                new Rectangle(0,0,playerTex.Width,playerTex.Height),
                Color.White,
                playerController.getRotation(), 
                playerOrigin, 
                SpriteEffects.None,
                0f);
        }


        public void attackFire(float chargeAmount)
        {
            // min charge = 10, max = 150
            float chargeFraction = chargeAmount / (float)(maxCharge - minCharge);

            int totalShots = (int)(Fire.MIN_FIRE_SHOTS + (Fire.MAX_FIRE_SHOTS - Fire.MIN_FIRE_SHOTS) * chargeFraction);
            float totalFireSpread = Fire.MIN_FIRE_SPREAD + (Fire.MAX_FIRE_SPREAD - Fire.MIN_FIRE_SPREAD) * chargeFraction;
            int totalLife = (int)(Fire.MIN_FIRE_RANGE + (Fire.MAX_FIRE_RANGE - Fire.MIN_FIRE_RANGE) * chargeFraction);
            float anglePerShot = totalFireSpread / totalShots;

            float currentAngle = 0.0f - totalFireSpread / 2.0f;

            for (int i = 0; i < totalShots; i++)
            {
                game.attacks.Add(new Fire(game, playerController.getRotation2() + currentAngle, 100, totalLife, Position, this));
                currentAngle += anglePerShot;
                //game.attacks.Add(new Fire(playerController.getRotation2()+currentAngle, Position, game, this, rightcharge));
            }
        }


            
    }
}
