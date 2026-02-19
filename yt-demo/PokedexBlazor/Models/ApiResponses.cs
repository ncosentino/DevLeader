using System.Text.Json.Serialization;

namespace PokedexBlazor.Models;

public class PokemonListResponse
{
    public int Count { get; set; }
    public string? Next { get; set; }
    public string? Previous { get; set; }
    public List<PokemonListItem> Results { get; set; } = new();
}

public class PokemonListItem
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;

    public int GetIdFromUrl()
    {
        var segments = Url.TrimEnd('/').Split('/');
        return int.TryParse(segments.Last(), out var id) ? id : 0;
    }
}

public class PokemonDetailsResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Height { get; set; }
    public int Weight { get; set; }
    [JsonPropertyName("base_experience")]
    public int BaseExperience { get; set; }
    public List<PokemonTypeResponse> Types { get; set; } = new();
    public List<PokemonAbilityResponse> Abilities { get; set; } = new();
    public List<PokemonStatResponse> Stats { get; set; } = new();
    public PokemonSpritesResponse Sprites { get; set; } = new();
}

public class PokemonTypeResponse
{
    public int Slot { get; set; }
    public TypeInfoResponse Type { get; set; } = new();
}

public class TypeInfoResponse
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}

public class PokemonAbilityResponse
{
    public AbilityInfoResponse Ability { get; set; } = new();
    [JsonPropertyName("is_hidden")]
    public bool IsHidden { get; set; }
    public int Slot { get; set; }
}

public class AbilityInfoResponse
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}

public class PokemonStatResponse
{
    [JsonPropertyName("base_stat")]
    public int BaseStat { get; set; }
    public int Effort { get; set; }
    public StatInfoResponse Stat { get; set; } = new();
}

public class StatInfoResponse
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}

public class PokemonSpritesResponse
{
    [JsonPropertyName("front_default")]
    public string? FrontDefault { get; set; }
    [JsonPropertyName("back_default")]
    public string? BackDefault { get; set; }
    [JsonPropertyName("front_shiny")]
    public string? FrontShiny { get; set; }
    [JsonPropertyName("back_shiny")]
    public string? BackShiny { get; set; }
    public OtherSpritesResponse? Other { get; set; }
}

public class OtherSpritesResponse
{
    [JsonPropertyName("official-artwork")]
    public OfficialArtworkResponse OfficialArtwork { get; set; } = new();
}

public class OfficialArtworkResponse
{
    [JsonPropertyName("front_default")]
    public string? FrontDefault { get; set; }
}