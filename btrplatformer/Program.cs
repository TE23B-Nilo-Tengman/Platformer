// using System.Drawing;
// using System.Numerics;
// using Raylib_cs;

Raylib.InitWindow(1200, 900, "yupp");
Raylib.SetTargetFPS(60);

Rectangle dude = new Rectangle(20, 40, 20, 20);
Rectangle dirt;
Rectangle wall;
wall = new Rectangle(800, 300, 200, 300);

float velocityY = 0;
float maxVelocity = 20;

while (Raylib.WindowShouldClose() != true)
{
    dirt = new Rectangle(0, 700, 1200, 100);

    Vector2 wallMiddle = new Vector2(wall.X + (wall.Width / 2), wall.Y + (wall.Height / 2));

    // Move horizontally
    if (Raylib.IsKeyDown(KeyboardKey.D))
    {
        dude.X += 10;
        // Check if the player collides with the wall on the right side
        if (Raylib.CheckCollisionRecs(wall, dude))
        {
            dude.X -= 10;  // Undo the movement to prevent going through the wall
        }
    }

    if (Raylib.IsKeyDown(KeyboardKey.A))
    {
        dude.X -= 10;
        // Check if the player collides with the wall on the left side
        if (Raylib.CheckCollisionRecs(wall, dude))
        {
            dude.X += 10;  // Undo the movement to prevent going through the wall
        }
    }

    // Handle vertical velocity (gravity)
    if (velocityY < maxVelocity)
    {
        velocityY += 1;  // Gravity pulling down
    }

    dude.Y += velocityY;

    // Collision with the ground (dirt)
    if (Raylib.CheckCollisionRecs(dirt, dude))
    {
        Rectangle dirtOverlap = Raylib.GetCollisionRec(dirt, dude);
        dude.Y -= (int)dirtOverlap.Height;  // Adjust Y position to the ground
        velocityY = 0;  // Stop downward velocity
    }

    // Jump logic
    if (Raylib.IsKeyPressed(KeyboardKey.W) && velocityY == 0)
    {
        velocityY = -20;  // Apply an initial upward velocity
    }

    // Check wall collision for vertical movement
    if (Raylib.CheckCollisionRecs(wall, dude))
    {
        Rectangle wallOverlap = Raylib.GetCollisionRec(wall, dude);

        // Handle collision on the X-axis (horizontal)
        if (dude.X > wallMiddle.X - wall.Width / 2 && dude.X < wallMiddle.X + wall.Width / 2)
        {
            if (dude.X < wallMiddle.X)
                dude.X -= 10;  // Undo leftward movement when moving into the wall
            else
                dude.X += 10;  // Undo rightward movement when moving into the wall
        }

        // Handle vertical collisions with the wall (on Y-axis)
        if (dude.Y < wall.Y + wall.Height && dude.Y + dude.Height > wall.Y)
        {
            if (velocityY > 0)
            {
                dude.Y -= (int)wallOverlap.Height;  // Stop downward movement on the top edge of the wall
                velocityY = 0;
            }
            else if (velocityY < 0)
            {
                dude.Y += (int)wallOverlap.Height;  // Stop upward movement on the bottom edge of the wall
                velocityY = 0;
            }
        }
    }

    // Drawing everything
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.White);
    Raylib.DrawRectangleRec(wall, Color.Gray);
    Raylib.DrawRectangleRec(dirt, Color.Brown);
    Raylib.DrawRectangleRec(dude, Color.Yellow);
    Raylib.EndDrawing();
}
