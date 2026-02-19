using System.Net.Http.Json;
using PokedexBlazor.Models;

namespace PokedexBlazor.Services;

public class PokeApiService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://pokeapi.co/api/v2/";
    private readonly Dictionary<string, object> _cache = new();
    private readonly Dictionary<string, DateTime> _cacheExpiry = new();
    private readonly TimeSpan _cacheTimeout = TimeSpan.FromMinutes(30);

    public PokeApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(BaseUrl);
    }

    public async Task<PokemonListResponse?> GetPokemonListAsync(int limit = 20, int offset = 0)
    {
        var cacheKey = $"pokemon-list-{limit}-{offset}";
        
        if (TryGetFromCache<PokemonListResponse>(cacheKey, out var cached))
            return cached;

        try
        {
            var response = await _httpClient.GetFromJsonAsync<PokemonListResponse>(
                $"pokemon?limit={limit}&offset={offset}");
            
            if (response != null)
                AddToCache(cacheKey, response);
            
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching Pokemon list: {ex.Message}");
            return null;
        }
    }

    public async Task<PokemonDetailsResponse?> GetPokemonDetailsAsync(int id)
    {
        var cacheKey = $"pokemon-details-{id}";
        
        if (TryGetFromCache<PokemonDetailsResponse>(cacheKey, out var cached))
            return cached;

        try
        {
            var response = await _httpClient.GetFromJsonAsync<PokemonDetailsResponse>(
                $"pokemon/{id}");
            
            if (response != null)
                AddToCache(cacheKey, response);
            
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching Pokemon details: {ex.Message}");
            return null;
        }
    }

    public async Task<PokemonDetailsResponse?> GetPokemonDetailsByNameAsync(string name)
    {
        var cacheKey = $"pokemon-details-{name}";
        
        if (TryGetFromCache<PokemonDetailsResponse>(cacheKey, out var cached))
            return cached;

        try
        {
            var response = await _httpClient.GetFromJsonAsync<PokemonDetailsResponse>(
                $"pokemon/{name.ToLower()}");
            
            if (response != null)
                AddToCache(cacheKey, response);
            
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching Pokemon details: {ex.Message}");
            return null;
        }
    }

    public async Task<List<Pokemon>> GetPokemonWithDetailsAsync(int limit = 20, int offset = 0)
    {
        var listResponse = await GetPokemonListAsync(limit, offset);
        if (listResponse == null) return new List<Pokemon>();

        var pokemonList = new List<Pokemon>();

        foreach (var item in listResponse.Results)
        {
            var id = item.GetIdFromUrl();
            var details = await GetPokemonDetailsAsync(id);
            
            if (details != null)
            {
                pokemonList.Add(MapToPokemon(details));
            }
        }

        return pokemonList;
    }

    private Pokemon MapToPokemon(PokemonDetailsResponse response)
    {
        return new Pokemon
        {
            Id = response.Id,
            Name = response.Name,
            Url = $"{BaseUrl}pokemon/{response.Id}",
            Details = new PokemonDetails
            {
                Id = response.Id,
                Name = response.Name,
                Height = response.Height,
                Weight = response.Weight,
                BaseExperience = response.BaseExperience,
                Types = response.Types.Select(t => new PokemonType
                {
                    Slot = t.Slot,
                    Type = new TypeInfo
                    {
                        Name = t.Type.Name,
                        Url = t.Type.Url
                    }
                }).ToList(),
                Abilities = response.Abilities.Select(a => new PokemonAbility
                {
                    Ability = new AbilityInfo
                    {
                        Name = a.Ability.Name,
                        Url = a.Ability.Url
                    },
                    IsHidden = a.IsHidden,
                    Slot = a.Slot
                }).ToList(),
                Stats = response.Stats.Select(s => new PokemonStat
                {
                    BaseStat = s.BaseStat,
                    Effort = s.Effort,
                    Stat = new StatInfo
                    {
                        Name = s.Stat.Name,
                        Url = s.Stat.Url
                    }
                }).ToList(),
                Sprites = new PokemonSprites
                {
                    FrontDefault = response.Sprites.FrontDefault,
                    BackDefault = response.Sprites.BackDefault,
                    FrontShiny = response.Sprites.FrontShiny,
                    BackShiny = response.Sprites.BackShiny,
                    Other = response.Sprites.Other != null ? new SpriteVersions
                    {
                        OfficialArtwork = new OfficialArtwork
                        {
                            FrontDefault = response.Sprites.Other.OfficialArtwork.FrontDefault
                        }
                    } : null
                }
            }
        };
    }

    private bool TryGetFromCache<T>(string key, out T? value)
    {
        value = default;
        
        if (_cache.ContainsKey(key) && _cacheExpiry.ContainsKey(key))
        {
            if (_cacheExpiry[key] > DateTime.UtcNow)
            {
                value = (T)_cache[key];
                return true;
            }
            else
            {
                _cache.Remove(key);
                _cacheExpiry.Remove(key);
            }
        }
        
        return false;
    }

    private void AddToCache<T>(string key, T value)
    {
        _cache[key] = value!;
        _cacheExpiry[key] = DateTime.UtcNow.Add(_cacheTimeout);
    }

    public void ClearCache()
    {
        _cache.Clear();
        _cacheExpiry.Clear();
    }
}