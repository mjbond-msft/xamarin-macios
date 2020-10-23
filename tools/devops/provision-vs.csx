#load "provision-shared.csx"

// Provision Xcode
//
// Overrides:
// * The current commit can be overridden by setting the PROVISION_FROM_COMMIT variable.

var vs_install_url = FindVariable("MIN_VISUAL_STUDIO_URL");
var vs_install_path = "/Applications/Visual Studio.app";

Console.WriteLine($"vs_install_url: {vs_install_url}");
Console.WriteLine($"vs_install_path: {vs_install_path}");

if (Directory.Exists(vs_install_path))
{
    var vs_version_min = FindVariable("MIN_VISUAL_STUDIO_VERSION");
    var vs_version_max = FindVariable("MAX_VISUAL_STUDIO_VERSION");

    Console.WriteLine($"vs_version_min: {vs_version_min}");
    Console.WriteLine($"vs_version_max: {vs_version_max}");

    var vs_version_current = string.Empty;

    var sourceDirectory = Env("BUILD_SOURCESDIRECTORY");
    Console.WriteLine($"BUILD_SOURCESDIRECTORY: {sourceDirectory}");

    var vs_version_current = string.Empty;

    try
    {
        vs_version_current = Plist(vs_install_path).CFBundleShortVersionString;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"ERROR: Failed to retrieve the CFBundleShortVersionString setting from {Path.Join(vs_install_path, "Contents", "Info.plist")}. EXCEPTION: {ex.Message}");
    }

    Console.WriteLine($"vs_version_current: {vs_version_current}");
}
else
{
    Console.WriteLine("Installing Visual Studio");
    Item(vs_install_url);
}


