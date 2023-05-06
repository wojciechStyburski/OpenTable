namespace OpenTable.Infrastructure.Common;

public sealed class AppOptions
{
    public string Name { get; set; }
    public int MaxDuration { get; set; }
    public int OpenFrom { get; set; }
    public int OpenTo { get; set; }
}