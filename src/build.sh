if [[ "$OSTYPE" == "linux-gnu" ]]; then
    echo "# Building for linux"
    dotnet build && dotnet publish -c debug -r linux-x64
elif [[ "$OSTYPE" == "darwin"* ]]; then
    echo "# Building for macOS"
    dotnet build && dotnet publish -c debug -r osx-x64
elif [[ "$OSTYPE" == "cygwin" ]]; then
    echo "# Building for linux"
    dotnet build && dotnet publish -c debug -r linux-x64
elif [[ "$OSTYPE" == "msys" ]]; then
    echo "# Building for linux"
    dotnet build && dotnet publish -c debug -r linux-x64
else
    echo "# Building for windows"
    dotnet build && dotnet publish -c debug -r win-x64
fi