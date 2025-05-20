class Level
{
    string name = "";
    int levelNummer;
    public List<Rectangle> dödare = new();
    public List<Rectangle> walls = new();
    public List<Rectangle> målen = new();


    public Level(string name, int levelNummer, List<Rectangle> dödare, List<Rectangle> walls, List<Rectangle> målen)
    {
        this.name = name;
        this.levelNummer = levelNummer;
        this.dödare = dödare;
        this.walls = walls;
        this.målen = målen;
    }
}