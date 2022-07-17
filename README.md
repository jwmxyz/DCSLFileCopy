# DCSLFileCopy

## How To run

### Visual studio

Update `launchSettings.json` and include a `commandLineArgs`eg:

```json
 "commandLineArgs": "-s <source> -d <destination>"
```

Where `<source>` is your source path and `<destination>`is your destination

## `Dotnet Run` (With args)

Navigate to root folder and execute the following:

```dotnet run -p CopyDirectory -s <source> -d <destination>```

Where `<source>` is your source path and `<destination>`is your destination

## `Dotnet Run` (Without args)

Navigate to root folder and execute

```dotnet run -p CopyDirectory```

You will be prompted for source/destination folders.