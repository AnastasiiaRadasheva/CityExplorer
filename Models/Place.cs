
using SQLite;

namespace CityExplorer.Models;

public class Place
{
  
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name            { get; set; } = string.Empty;
    public string Description     { get; set; } = string.Empty;
    public string FullDescription { get; set; } = string.Empty;
    public string Address         { get; set; } = string.Empty;
    public string CategoryEmoji   { get; set; } = string.Empty;
    public string ImageUrl        { get; set; } = string.Empty;

    public bool IsFavorite { get; set; } = false;

    [Ignore]
    public string CardColor { get; set; } = "#ea580c";

    public static string GetCardColor(string emoji) => emoji switch
    {
        "🏰" => "#7c2d12",
        "🌳" => "#14532d",
        "🍽️" => "#1e3a8a",
        _    => "#ea580c"
    };

    public void ApplyCardColor() => CardColor = GetCardColor(CategoryEmoji);
}
