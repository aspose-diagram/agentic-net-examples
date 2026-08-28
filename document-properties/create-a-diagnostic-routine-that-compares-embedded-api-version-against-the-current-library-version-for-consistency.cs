using System.IO;
using System;
using Aspose.Diagram;

public class DiagramVersionDiagnostic
{
    // Checks if the Visio version embedded in the diagram matches the Aspose.Diagram library version.
    public static void Run(string diagramPath)
    {
        // Load the Visio diagram.
        Diagram diagram = new Diagram(diagramPath);

        // Retrieve the Visio version stored in the document (e.g., "14" for Visio 2010).
        string visioVersion = diagram.Version;

        // Retrieve the build number of the Visio instance that created the document.
        long visioBuildNumber = diagram.Buildnum;

        // Retrieve the Aspose.Diagram assembly version (e.g., "23.9.0").
        string libraryVersion = BuildVersionInfo.AssemblyVersion;

        // Retrieve the Aspose.Diagram file version (e.g., "23.9.0.0").
        string libraryFileVersion = BuildVersionInfo.FileVersion;

        // Output the gathered information.
        Console.WriteLine($"Diagram Path          : {diagramPath}");
        Console.WriteLine($"Embedded Visio Version: {visioVersion}");
        Console.WriteLine($"Embedded Visio Build #: {visioBuildNumber}");
        Console.WriteLine($"Aspose.Diagram Assembly Version: {libraryVersion}");
        Console.WriteLine($"Aspose.Diagram File Version    : {libraryFileVersion}");

        // Perform a simple consistency check.
        // Here we consider the versions consistent if the major part of the Visio version
        // (e.g., "14") matches the major part of the Aspose.Diagram assembly version.
        // This is a heuristic; adjust as needed for your environment.
        bool isConsistent = false;
        if (int.TryParse(visioVersion, out int visioMajor))
        {
            // Extract major version from library version (text before first dot).
            string[] libParts = libraryVersion.Split('.');
            if (libParts.Length > 0 && int.TryParse(libParts[0], out int libMajor))
            {
                isConsistent = visioMajor == libMajor;
            }
        }

        Console.WriteLine(isConsistent
            ? "Version consistency check: PASSED"
            : "Version consistency check: FAILED");
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            DiagramVersionDiagnostic.Run("");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
