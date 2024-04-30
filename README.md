# How to set up the files
It's a bit tricky, since I had to use a modified version of the source code for the secret character. (Big thanks to [Aristurtle](https://github.com/AristurtleDev) on the Monogame discord server)

You will need a version of Visual Studio that supports .NET 8.0

The directories should be setup as so, so you dont have to change the files for it to build correctly:
```
-\ root
----- cheesenaf.sln
-----\ Cheesenaf
----------\ (Cheesenaf game files)
------\ MonoGame
----------\ (monogame code stuff)
```


When cloning, you also need to do submodule update, so the full command would be
```
git clone https://github.com/MonoGame/MonoGame.git
cd MonoGame
git submodule update --init --recursive
```

Then [use this file](https://github.com/MonoGame/MonoGame/pull/8248/files) and replace or edit the file at `Monogame/MonoGame.Framework/Platform/SDL/SDLGamePlatform.cs`

# How to build

For exporting to Windows, I ran this command inside the Cheesenaf folder:
```
dotnet publish -c Release -r win-x64 /p:PublishReadyToRun=false /p:TieredCompilation=false --self-contained
```
For Linux:
```
dotnet publish -c Release -r linux-x64 /p:PublishReadyToRun=false /p:TieredCompilation=false --self-contained
```

For more information, [here is the official monogame packaging page](https://docs.monogame.net/articles/packaging_games.html)
