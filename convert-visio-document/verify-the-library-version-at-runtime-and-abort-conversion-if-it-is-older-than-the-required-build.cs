using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Minimum required Aspose.Diagram version (adjust as needed)
            var requiredVersion = new Version("23.5.0.0");

            // Retrieve the current library version from BuildVersionInfo
            var currentVersionString = BuildVersionInfo.AssemblyVersion;
            var currentVersion = new Version(currentVersionString);

            // Abort if the current library version is older than the required one
            if (currentVersion < requiredVersion)
            {
                Console.WriteLine($"Aspose.Diagram version {currentVersion} is older than required {requiredVersion}. Conversion aborted.");
                return;
            }

            // Load the source Visio diagram (lifecycle rule)
            var diagram = new Diagram("input.vsdx");

            // Perform the conversion (e.g., save as PDF)
            diagram.Save("output.pdf", SaveFileFormat.Pdf);

            Console.WriteLine("Conversion completed successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
