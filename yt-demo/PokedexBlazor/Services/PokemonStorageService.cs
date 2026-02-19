using PokedexBlazor.Models;
using Microsoft.JSInterop;
using System.Text.Json;

namespace PokedexBlazor.Services;

public class PokemonStorageService
{
    private readonly IJSRuntime _jsRuntime;
    private const string StorageKey = "pokedex_tracking";
    private Dictionary<int, PokemonTrackingRecord> _cache = new();
    private bool _isInitialized = false;

    public PokemonStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task InitializeAsync()
    {
        if (_isInitialized) return;
        
        try
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", StorageKey);
            if (!string.IsNullOrEmpty(json))
            {
                var records = JsonSerializer.Deserialize<List<PokemonTrackingRecord>>(json) ?? new List<PokemonTrackingRecord>();
                _cache = records.ToDictionary(r => r.PokemonId);
            }
        }
        catch
        {
            _cache = new Dictionary<int, PokemonTrackingRecord>();
        }
        
        _isInitialized = true;
    }

    private async Task SaveToStorageAsync()
    {
        var records = _cache.Values.ToList();
        var json = JsonSerializer.Serialize(records);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
    }

    public async Task<PokemonTrackingInfo?> GetTrackingInfoAsync(int pokemonId)
    {
        await InitializeAsync();
        
        if (_cache.TryGetValue(pokemonId, out var record))
        {
            return new PokemonTrackingInfo
            {
                IsSeen = record.IsSeen,
                IsCaptured = record.IsCaptured,
                SeenDate = record.SeenDate,
                CapturedDate = record.CapturedDate
            };
        }
        
        return new PokemonTrackingInfo();
    }

    public async Task<Dictionary<int, PokemonTrackingInfo>> GetAllTrackingInfoAsync()
    {
        await InitializeAsync();
        
        return _cache.ToDictionary(
            kvp => kvp.Key,
            kvp => new PokemonTrackingInfo
            {
                IsSeen = kvp.Value.IsSeen,
                IsCaptured = kvp.Value.IsCaptured,
                SeenDate = kvp.Value.SeenDate,
                CapturedDate = kvp.Value.CapturedDate
            });
    }

    public async Task MarkAsSeenAsync(int pokemonId)
    {
        await InitializeAsync();
        
        if (_cache.TryGetValue(pokemonId, out var existing))
        {
            existing.IsSeen = true;
            existing.SeenDate ??= DateTime.UtcNow;
        }
        else
        {
            _cache[pokemonId] = new PokemonTrackingRecord
            {
                PokemonId = pokemonId,
                IsSeen = true,
                SeenDate = DateTime.UtcNow
            };
        }
        
        await SaveToStorageAsync();
    }

    public async Task MarkAsCapturedAsync(int pokemonId)
    {
        await InitializeAsync();
        
        if (_cache.TryGetValue(pokemonId, out var existing))
        {
            existing.IsCaptured = true;
            existing.CapturedDate ??= DateTime.UtcNow;
            existing.IsSeen = true;
            existing.SeenDate ??= DateTime.UtcNow;
        }
        else
        {
            _cache[pokemonId] = new PokemonTrackingRecord
            {
                PokemonId = pokemonId,
                IsSeen = true,
                IsCaptured = true,
                SeenDate = DateTime.UtcNow,
                CapturedDate = DateTime.UtcNow
            };
        }
        
        await SaveToStorageAsync();
    }

    public async Task ToggleCapturedAsync(int pokemonId)
    {
        await InitializeAsync();
        
        if (_cache.TryGetValue(pokemonId, out var existing))
        {
            existing.IsCaptured = !existing.IsCaptured;
            existing.CapturedDate = existing.IsCaptured ? DateTime.UtcNow : null;
            
            if (existing.IsCaptured)
            {
                existing.IsSeen = true;
                existing.SeenDate ??= DateTime.UtcNow;
            }
        }
        else
        {
            _cache[pokemonId] = new PokemonTrackingRecord
            {
                PokemonId = pokemonId,
                IsSeen = true,
                IsCaptured = true,
                SeenDate = DateTime.UtcNow,
                CapturedDate = DateTime.UtcNow
            };
        }
        
        await SaveToStorageAsync();
    }

    public async Task<int> GetSeenCountAsync()
    {
        await InitializeAsync();
        return _cache.Values.Count(r => r.IsSeen);
    }

    public async Task<int> GetCapturedCountAsync()
    {
        await InitializeAsync();
        return _cache.Values.Count(r => r.IsCaptured);
    }

    public async Task ClearAllDataAsync()
    {
        _cache.Clear();
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", StorageKey);
    }
}

public class PokemonTrackingRecord
{
    public int PokemonId { get; set; }
    public bool IsSeen { get; set; }
    public bool IsCaptured { get; set; }
    public DateTime? SeenDate { get; set; }
    public DateTime? CapturedDate { get; set; }
}