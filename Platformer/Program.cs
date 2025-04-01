// using System.Drawing;
using System.Numerics;
using Raylib_cs;


Raylib.InitWindow(1200, 900, "yupp");
// Raylib.ToggleFullscreen();
Raylib.SetTargetFPS(60);



Rectangle dude = new Rectangle(20, 40, 20, 20);
Rectangle mål;
mål = new Rectangle(900, 750, 400, 150);
List<Rectangle> målen = new();
målen.Add(mål);


Rectangle dö;
dö = new Rectangle(300, 750, 50, 50);
List<Rectangle> dödare = new();
dödare.Add(dö);
dödare.Add(new Rectangle(400, 350, 50, 50));
dödare.Add(new Rectangle(790, 200, 10, 600));
dödare.Add(new Rectangle(300, 750, 50, 50));


Rectangle wall;
wall = new Rectangle(130, 600, 500, 50);
List<Rectangle> walls = new();
walls.Add(wall);
walls.Add(new Rectangle(100, 0, 30, 700));
walls.Add(new Rectangle(400, 400, 100, 50));
walls.Add(new Rectangle(800, 200, 100, 800));
walls.Add(new Rectangle(0, 800, 800, 500));
walls.Add(new Rectangle(600, 250, 100, 50));






float velocityY = 0;
float maxVelocity = 20;
float velocityX = 0;

bool dead = false;
bool vinn = false;

bool spelar = true;





while ((Raylib.WindowShouldClose() != true))
{
    while (spelar == true) //(Raylib.WindowShouldClose() != true)
    {
        // PÅ Å AV
        if (Raylib.IsKeyPressed(KeyboardKey.E))
        {
            // spelar = false;
            Raylib.WindowShouldClose();
        }
        if (Raylib.IsKeyPressed(KeyboardKey.Q))
        {
            // spelar = true;
            
        }
        // MOVEMENT
        if (Raylib.IsKeyDown(KeyboardKey.D))
        {
            dude.X += 10;

            foreach (Rectangle w in walls)
            {
                if (Raylib.CheckCollisionRecs(w, dude))
                {
                    dude.X -= 10;
                    break;
                }
            }
        }
        if (Raylib.IsKeyDown(KeyboardKey.A))
        {
            dude.X -= 10;

            foreach (Rectangle w in walls)
            {
                if (Raylib.CheckCollisionRecs(w, dude))
                {
                    dude.X += 10;
                    break;
                }
            }
        }
        // JUMP
        if (Raylib.IsKeyPressed(KeyboardKey.W) && velocityY == 0)
        {
            velocityY = -20;
        }
        // wall jump
        if (Raylib.IsKeyPressed(KeyboardKey.W) && CheckWalljumpH(dude, walls))
        {
            velocityY = -20;
            velocityX -= 5;

        }

        if (Raylib.IsKeyPressed(KeyboardKey.W) && CheckWalljumpV(dude, walls))
        {
            velocityY = -20;
            velocityX += 10;
        }

        // X velocity (walljumps)
        if (velocityX < 0)
        {
            velocityX += 1;
        }
        if (velocityX > 0)
        {
            velocityX -= 1;
        }
        dude.X += velocityX;

        // GRAVITY
        if (velocityY < maxVelocity)
        {
            velocityY += 1;
        }

        dude.Y += velocityY;

        // WALL COLLISH
        foreach (Rectangle w in walls)
        {
            (dude, velocityY) = WallCollision(dude, w, velocityY);
        }

        if (CheckGrounded(dude, walls))
        {
            velocityY = 0;
        }

        if (CheckWalljumpH(dude, walls))
        {
            velocityY = 0;
        }
        if (CheckWalljumpV(dude, walls))
        {
            velocityY = 0;
        }
        // DÖ
        if (ÄrDöd(dude, dödare))
        {
            dead = true;
        }
        else
        {
            dead = false;
        }
        if (dead == true)
        {
            dude.X = 20;
            dude.Y = 40;
        }
        // VINN
        if (ÄrVinn(dude, målen))
        {
            vinn = true;
        }
        else
        {
            vinn = false;
        }
        if (vinn == true)
        {
            dude.X = 20;
            dude.Y = 40;
        }
        // DRAW STUFF
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.White);

        foreach (Rectangle w in walls)
        {
            Raylib.DrawRectangleRec(w, Color.Gray);
        }

        foreach (Rectangle d in dödare)
        {
            Raylib.DrawRectangleRec(d, Color.Pink);
        }

        Raylib.DrawRectangleRec(mål, Color.Green);
        Raylib.DrawRectangleRec(dude, Color.Yellow);
        Raylib.EndDrawing();

        Console.WriteLine($"{velocityY},{velocityX}");
    }
}















// E DU DÖD?
bool ÄrDöd(Rectangle dude, List<Rectangle> dödare)
{
    foreach (Rectangle dö in dödare)
    {
        if (Raylib.CheckCollisionRecs(dude, dö))
        {
            return true;
        }
        ;
    }
    return false;
}
bool ÄrVinn(Rectangle dude, List<Rectangle> målen)
{
    foreach (Rectangle mål in målen)
    {
        if (Raylib.CheckCollisionRecs(dude, mål))
        {
            return true;
        }
        ;
    }
    return false;
}

// STÅR DU PÅ VÄGGEN ELLE?
static bool CheckGrounded(Rectangle dude, List<Rectangle> walls)
{
    Vector2 feeth = new(
        dude.X + dude.Width - 2,
        dude.Y + dude.Height + 2
    );
    Raylib.DrawCircleV(feeth, 4, Color.Yellow);
    foreach (Rectangle wall in walls)
    {
        if (Raylib.CheckCollisionPointRec(feeth, wall))
        {
            return true;
        }
    }
    Vector2 feetv = new(
        dude.X + 2,
        dude.Y + dude.Height + 2
    );
    Raylib.DrawCircleV(feetv, 4, Color.Yellow);
    foreach (Rectangle wall in walls)
    {
        if (Raylib.CheckCollisionPointRec(feetv, wall))
        {
            return true;
        }
    }
    return false;
}

// Walljump högerhand check
static bool CheckWalljumpH(Rectangle dude, List<Rectangle> walls)
{
    Vector2 handh = new(
        dude.X + dude.Width + 2,
        dude.Y + dude.Height / 2
    );
    Raylib.DrawCircleV(handh, 4, Color.Yellow);
    foreach (Rectangle wall in walls)
    {
        if (Raylib.CheckCollisionPointRec(handh, wall))
        {
            return true;
        }
    }
    Vector2 handv = new(
        dude.X - 2,
        dude.Y + dude.Height / 2
    );
    Raylib.DrawCircleV(handv, 4, Color.Yellow);
    foreach (Rectangle wall in walls)
    {
        if (Raylib.CheckCollisionPointRec(handv, wall))
        {
            return true;
        }
    }
    return false;

}












// Walljump vänsterhand check
static bool CheckWalljumpV(Rectangle dude, List<Rectangle> walls)
{
    Vector2 handv = new(
        dude.X - 2,
        dude.Y + dude.Height / 2
    );
    Raylib.DrawCircleV(handv, 4, Color.Yellow);
    foreach (Rectangle wall in walls)
    {
        if (Raylib.CheckCollisionPointRec(handv, wall))
        {
            return true;
        }
    }
    return false;
}

static (Rectangle dude, float velocityY) WallCollision(Rectangle dude, Rectangle wall, float velocityY)
{
    Vector2 wallMiddle = new Vector2(wall.X + (wall.Width / 2), wall.Y + (wall.Height / 2));
    if (Raylib.CheckCollisionRecs(wall, dude))
    {
        Rectangle wallOverlap = Raylib.GetCollisionRec(wall, dude);


        if (dude.Y < wallMiddle.Y - wall.Height / 2 && !Raylib.IsKeyPressed(KeyboardKey.W))
        {
            dude.Y -= (int)wallOverlap.Height;
            velocityY = 0;
        }

        if (dude.Y < wall.Y + wall.Height && dude.Y + dude.Height > wall.Y)
        {
            if (velocityY > 0)
            {
                dude.Y -= (int)wallOverlap.Height;
            }
            else if (velocityY < 0)
            {
                dude.Y += (int)wallOverlap.Height;
                velocityY = 0;
            }

        }
    }

    return (dude, velocityY);
}

