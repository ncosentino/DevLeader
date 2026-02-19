#!/bin/bash

echo "Starting Pokédex Blazor Application..."
echo "======================================="
echo ""
echo "Building application..."
dotnet build

if [ $? -eq 0 ]; then
    echo ""
    echo "Build successful! Starting the application..."
    echo ""
    echo "The application will open at:"
    echo "  → http://localhost:5000"
    echo "  → https://localhost:5001"
    echo ""
    echo "Press Ctrl+C to stop the application"
    echo ""
    dotnet run
else
    echo "Build failed. Please check the error messages above."
    exit 1
fi