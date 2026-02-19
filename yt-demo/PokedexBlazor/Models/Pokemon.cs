namespace PokedexBlazor.Models;

public class Pokemon
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public PokemonDetails? Details { get; set; }
    public PokemonTrackingInfo TrackingInfo { get; set; } = new();
}

public class PokemonDetails
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Height { get; set; }
    public int Weight { get; set; }
    public int BaseExperience { get; set; }
    public List<PokemonType> Types { get; set; } = new();
    public List<PokemonAbility> Abilities { get; set; } = new();
    public List<PokemonStat> Stats { get; set; } = new();
    public PokemonSprites Sprites { get; set; } = new();
}

public class PokemonType
{
    public int Slot { get; set; }
    public TypeInfo Type { get; set; } = new();
}

public class TypeInfo
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}

public class PokemonAbility
{
    public AbilityInfo Ability { get; set; } = new();
    public bool IsHidden { get; set; }
    public int Slot { get; set; }
}

public class AbilityInfo
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}

public class PokemonStat
{
    public int BaseStat { get; set; }
    public int Effort { get; set; }
    public StatInfo Stat { get; set; } = new();
}

public class StatInfo
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}

public class PokemonSprites
{
    public string? FrontDefault { get; set; }
    public string? BackDefault { get; set; }
    public string? FrontShiny { get; set; }
    public string? BackShiny { get; set; }
    public SpriteVersions? Other { get; set; }
}

public class SpriteVersions
{
    public OfficialArtwork OfficialArtwork { get; set; } = new();
}

public class OfficialArtwork
{
    public string? FrontDefault { get; set; }
}

public class PokemonTrackingInfo
{
    public bool IsSeen { get; set; }
    public bool IsCaptured { get; set; }
    public DateTime? SeenDate { get; set; }
    public DateTime? CapturedDate { get; set; }
}