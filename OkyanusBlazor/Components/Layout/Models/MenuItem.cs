namespace Container.Components.Models;

// Models/MenuItem.cs
public class MenuItem
{
    public string Key { get; set; }
    public string Text { get; set; }
    public List<MenuItem> Items { get; set; } = new();
}
