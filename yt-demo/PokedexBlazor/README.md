# Pokédex - Blazor WebAssembly Edition

A modern Pokédex application built with Blazor WebAssembly and .NET 9, allowing users to browse, search, and track their Pokémon encounters.

## Features

- **Browse Pokémon**: View a comprehensive list of Pokémon with their images, types, and basic information
- **Search & Filter**: Search by name, number, or type; filter by seen/captured status
- **Track Progress**: Mark Pokémon as seen or captured with persistent storage
- **Detailed Views**: Click on any Pokémon to see detailed stats, abilities, and sprites
- **Offline Storage**: Uses browser localStorage to persist tracking data
- **Responsive Design**: Works seamlessly on desktop and mobile devices
- **Progressive Loading**: Load more Pokémon as you scroll

## Technology Stack

- **.NET 9**: Latest version of .NET for optimal performance
- **Blazor WebAssembly**: Client-side web app framework
- **C# 12**: Modern language features
- **PokéAPI**: RESTful API for Pokémon data
- **LocalStorage**: Browser storage for offline persistence

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- A modern web browser (Chrome, Firefox, Safari, Edge)

## Getting Started

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd PokedexBlazor
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Run the application:
   ```bash
   dotnet run
   ```

4. Open your browser and navigate to `https://localhost:5001` or `http://localhost:5000`

## Project Structure

```
PokedexBlazor/
├── Components/           # Reusable Blazor components
│   ├── PokemonCard.razor      # Individual Pokémon card display
│   ├── PokemonDetailModal.razor # Detailed Pokémon information modal
│   └── SearchFilter.razor     # Search and filter controls
├── Models/              # Data models
│   ├── Pokemon.cs            # Core Pokémon models
│   └── ApiResponses.cs       # API response DTOs
├── Pages/               # Blazor pages
│   └── Home.razor           # Main Pokédex page
├── Services/            # Business logic and data services
│   ├── PokeApiService.cs    # PokéAPI integration
│   ├── PokemonStateService.cs # Application state management
│   └── PokemonStorageService.cs # LocalStorage persistence
├── wwwroot/             # Static assets
├── Program.cs           # Application entry point
└── _Imports.razor       # Global imports
```

## Key Features Explained

### Pokémon Tracking
- **Seen**: Automatically marked when you view a Pokémon's details
- **Captured**: Toggle capture status with the capture button
- Tracking data persists across browser sessions using localStorage

### Search Functionality
- Search by Pokémon name (e.g., "Pikachu")
- Search by National Pokédex number (e.g., "25")
- Search by type (e.g., "electric")

### Filter Options
- **All**: Show all Pokémon
- **Seen**: Show only Pokémon you've viewed
- **Captured**: Show only Pokémon you've captured
- **Not Seen**: Show Pokémon you haven't encountered yet

### Performance Optimizations
- HTTP response caching to minimize API calls
- Lazy loading of images
- Progressive data loading (starts with 151, load more on demand)
- Efficient state management with event-driven updates

## API Integration

The application uses the [PokéAPI](https://pokeapi.co/) for Pokémon data:
- Base URL: `https://pokeapi.co/api/v2/`
- Rate limit: 100 requests per IP per minute
- Caching implemented to respect rate limits

## Browser Compatibility

- Chrome 90+
- Firefox 88+
- Safari 14+
- Edge 90+

## Contributing

Feel free to submit issues, create pull requests, or suggest new features!

## License

This project is for educational purposes. Pokémon and all related names are trademarks of Nintendo, Game Freak, and The Pokémon Company.

## Acknowledgments

- [PokéAPI](https://pokeapi.co/) for providing the Pokémon data
- The Blazor team at Microsoft for the excellent framework
- The Pokémon community for inspiration