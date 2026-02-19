using PokedexBlazor.Models;

namespace PokedexBlazor.Services;

public class PokemonStateService
{
    private readonly PokeApiService _apiService;
    private readonly PokemonStorageService _storageService;
    
    private List<Pokemon> _allPokemon = new();
    private Dictionary<int, PokemonTrackingInfo> _trackingInfo = new();
    private string _searchTerm = string.Empty;
    private PokemonFilter _currentFilter = PokemonFilter.All;
    
    public event Action? OnStateChanged;
    
    public List<Pokemon> AllPokemon => _allPokemon;
    public List<Pokemon> FilteredPokemon => GetFilteredPokemon();
    public int TotalCount => _allPokemon.Count;
    public int SeenCount => _trackingInfo.Count(x => x.Value.IsSeen);
    public int CapturedCount => _trackingInfo.Count(x => x.Value.IsCaptured);
    public bool IsLoading { get; private set; }
    public string ErrorMessage { get; private set; } = string.Empty;
    
    public PokemonStateService(PokeApiService apiService, PokemonStorageService storageService)
    {
        _apiService = apiService;
        _storageService = storageService;
    }
    
    public async Task InitializeAsync()
    {
        await LoadTrackingInfoAsync();
    }
    
    public async Task LoadPokemonAsync(int limit = 151, int offset = 0)
    {
        IsLoading = true;
        ErrorMessage = string.Empty;
        NotifyStateChanged();
        
        try
        {
            var pokemon = await _apiService.GetPokemonWithDetailsAsync(limit, offset);
            _allPokemon = pokemon;
            
            // Apply tracking info to loaded Pokemon
            foreach (var p in _allPokemon)
            {
                if (_trackingInfo.TryGetValue(p.Id, out var tracking))
                {
                    p.TrackingInfo = tracking;
                }
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Failed to load Pokemon: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
            NotifyStateChanged();
        }
    }
    
    public async Task LoadMorePokemonAsync(int count = 20)
    {
        if (IsLoading) return;
        
        IsLoading = true;
        NotifyStateChanged();
        
        try
        {
            var offset = _allPokemon.Count;
            var newPokemon = await _apiService.GetPokemonWithDetailsAsync(count, offset);
            
            // Apply tracking info to new Pokemon
            foreach (var p in newPokemon)
            {
                if (_trackingInfo.TryGetValue(p.Id, out var tracking))
                {
                    p.TrackingInfo = tracking;
                }
            }
            
            _allPokemon.AddRange(newPokemon);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Failed to load more Pokemon: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
            NotifyStateChanged();
        }
    }
    
    private async Task LoadTrackingInfoAsync()
    {
        _trackingInfo = await _storageService.GetAllTrackingInfoAsync();
    }
    
    public async Task MarkAsSeenAsync(int pokemonId)
    {
        await _storageService.MarkAsSeenAsync(pokemonId);
        await UpdateTrackingInfoForPokemon(pokemonId);
        NotifyStateChanged();
    }
    
    public async Task ToggleCapturedAsync(int pokemonId)
    {
        await _storageService.ToggleCapturedAsync(pokemonId);
        await UpdateTrackingInfoForPokemon(pokemonId);
        NotifyStateChanged();
    }
    
    private async Task UpdateTrackingInfoForPokemon(int pokemonId)
    {
        var tracking = await _storageService.GetTrackingInfoAsync(pokemonId);
        if (tracking != null)
        {
            _trackingInfo[pokemonId] = tracking;
            
            var pokemon = _allPokemon.FirstOrDefault(p => p.Id == pokemonId);
            if (pokemon != null)
            {
                pokemon.TrackingInfo = tracking;
            }
        }
    }
    
    public void SetSearchTerm(string searchTerm)
    {
        _searchTerm = searchTerm.ToLower();
        NotifyStateChanged();
    }
    
    public void SetFilter(PokemonFilter filter)
    {
        _currentFilter = filter;
        NotifyStateChanged();
    }
    
    private List<Pokemon> GetFilteredPokemon()
    {
        var filtered = _allPokemon.AsEnumerable();
        
        // Apply search filter
        if (!string.IsNullOrWhiteSpace(_searchTerm))
        {
            filtered = filtered.Where(p => 
                p.Name.ToLower().Contains(_searchTerm) ||
                p.Id.ToString().Contains(_searchTerm) ||
                (p.Details?.Types.Any(t => t.Type.Name.ToLower().Contains(_searchTerm)) ?? false));
        }
        
        // Apply status filter
        filtered = _currentFilter switch
        {
            PokemonFilter.Seen => filtered.Where(p => p.TrackingInfo.IsSeen),
            PokemonFilter.Captured => filtered.Where(p => p.TrackingInfo.IsCaptured),
            PokemonFilter.NotSeen => filtered.Where(p => !p.TrackingInfo.IsSeen),
            _ => filtered
        };
        
        return filtered.ToList();
    }
    
    public Pokemon? GetPokemonById(int id)
    {
        return _allPokemon.FirstOrDefault(p => p.Id == id);
    }
    
    public async Task RefreshPokemonDetailsAsync(int id)
    {
        var response = await _apiService.GetPokemonDetailsAsync(id);
        if (response != null)
        {
            var pokemon = _allPokemon.FirstOrDefault(p => p.Id == id);
            if (pokemon != null)
            {
                // Update details while preserving tracking info
                var trackingInfo = pokemon.TrackingInfo;
                var updatedPokemon = MapResponseToPokemon(response);
                updatedPokemon.TrackingInfo = trackingInfo;
                
                var index = _allPokemon.IndexOf(pokemon);
                _allPokemon[index] = updatedPokemon;
                NotifyStateChanged();
            }
        }
    }
    
    private Pokemon MapResponseToPokemon(PokemonDetailsResponse response)
    {
        return new Pokemon
        {
            Id = response.Id,
            Name = response.Name,
            Url = $"https://pokeapi.co/api/v2/pokemon/{response.Id}",
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
                    Type = new TypeInfo { Name = t.Type.Name, Url = t.Type.Url }
                }).ToList(),
                Abilities = response.Abilities.Select(a => new PokemonAbility
                {
                    Ability = new AbilityInfo { Name = a.Ability.Name, Url = a.Ability.Url },
                    IsHidden = a.IsHidden,
                    Slot = a.Slot
                }).ToList(),
                Stats = response.Stats.Select(s => new PokemonStat
                {
                    BaseStat = s.BaseStat,
                    Effort = s.Effort,
                    Stat = new StatInfo { Name = s.Stat.Name, Url = s.Stat.Url }
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
    
    private void NotifyStateChanged() => OnStateChanged?.Invoke();
}

public enum PokemonFilter
{
    All,
    Seen,
    Captured,
    NotSeen
}