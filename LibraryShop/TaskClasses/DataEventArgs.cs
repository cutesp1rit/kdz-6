namespace LibraryShop;

public class DataEventArgs : EventArgs
{
    public DateTime DataNewChanged{ get; init; }

    public DataEventArgs(DateTime newChanges)
    {
        DataNewChanged = newChanges;
    }
}