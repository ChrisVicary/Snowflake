namespace Snowflake.Events;

public abstract class Event
{
    public abstract string Name { get; }

    public abstract EventCategories Categories { get; }

    public bool Handled { get; set; }

    public bool IsInCategory(EventCategories categories) => Categories.HasFlag(categories);

    public override string ToString() => Name;
}
