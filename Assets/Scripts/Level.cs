public class Level
{
    public int id { get; set; }
    public bool complete = false;
    public bool unlocked = false;
    public bool tracking = false;

    public Level(int intializeID)
    {
        id = intializeID;
        complete = false;
        unlocked = false;
        tracking = false;
    }
}