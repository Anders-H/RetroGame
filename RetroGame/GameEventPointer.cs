namespace RetroGame;

public struct GameEventPointer
{
    public bool Occured { get; set; }
    public ulong OccuredAt { get; set; }

    public GameEventPointer()
    {
        Occured = false;
        OccuredAt = 0;
    }

    public void Occure(ulong ticks)
    {
        Occured = true;
        OccuredAt = ticks;
    }

    public bool OccuredTicksAgo(ulong ticks, ulong delay) =>
        Occured && ticks - OccuredAt > delay;
}