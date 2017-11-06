# Notepad++ .NET Infrastructure

This projects based on [NotepadPlusPlusPluginPack.Net](https://github.com/kbilsted/NotepadPlusPlusPluginPack.Net)
but doesn't required installed Visual C++ and other stuff except of this submodule
(`LibToolPath` and `LibToolDllPath` has been deleted from NppPlugin.DllExport.targets).
Also it's a bit easy to use.

The main goal is independence of plugin and infrastructure code and simplicity.

## Steps to build your own Notepad++ plugin

1. Create plugin solution with .NET project like `MyAwesomePlugin`.
2. Add submodule <https://github.com/KvanTTT/NppNetInf.git> to your repository.
3. Add a line `<Import Project="$(SolutionDir)NppNetInf\src\DllExport\NppPlugin.DllExport.targets" />`
   to MyAwesomePlugin.csproj. This target executed after every build and changes
   assembly for corresponding to Notepad++ format.
4. Create subdirectory `NppNetInf` inside your project `MyAwesomePlugin` directory
   and "Add As Link" all *.cs files from NppNetInf submodule (Win32.cs, Scintilla.cs, PluginMain.cs, etc.).
5. Add a class `Main` that implements `PluginMain` and override required methods
  and properties (`PluginName`, `CommandMenuInit`). Also you can override other
  optional methods
  (`OnNotification(ScNotification notification)`, `PluginCleanUp()`, `SetToolBarIcon`).
6. Choose platform (x86 or x64), build project and move compilied assembly to
   Notepad++ plugins directory
   (as usual `C:\Program Files\Notepad++\plugins` or `C:\Program Files (x86)\Notepad++\plugins`).
7. Open Notepad++ and enjoy your awesome plugin!

## How it works

At start this infrastructure tries to find plugin `Main` class via reflection.
Such `Main` class should implement `PluginMain` abstract class.

## Plugin demo

[NppGist](https://github.com/KvanTTT/NppGist) is built on NppNetInf. It has
configured CI with both x86 and x64 builds.

## License

Apache 2.0.