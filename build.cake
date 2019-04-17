var target = Argument("Target", "Default");
Task("Default")
  .IsDependentOn("HotReload")
  .Does(() =>
  {

  });

Task("HotReload")
    .Does(() => {
        StartProcess("adb", "forward tcp:4290 tcp:4290");

        var processSettings = new ProcessSettings()
        {
            Arguments = $"./tools/Xamarin.Forms.HotReload.Observer.exe u=http://127.0.0.1:4290,http://127.0.0.1:4291",
        };

        using (var hotReloadProcess = StartAndReturnProcess("mono", processSettings))
        {
            hotReloadProcess.WaitForExit();
        }
    });

RunTarget(target);
