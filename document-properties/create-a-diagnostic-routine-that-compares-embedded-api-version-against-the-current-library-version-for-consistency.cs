using System.IO;
using System;
using Aspose.Diagram;

public static class DiagramVersionDiagnostic
{
    /// <summary>
    /// Checks whether the Visio version embedded in the diagram matches the Aspose.Diagram library version.
    /// </summary>
    /// <param name="filePath">Path to the Visio (.vsdx/.vsd) file.</param>
    public static void CheckVersion(string filePath)
    {
        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(filePath);

            // Retrieve the Visio version stored in the document.
            // Example: "14" for Visio 2010.
            string embeddedVisioVersion = diagram.Version;

            // Retrieve the Aspose.Diagram assembly version.
            // This reflects the version of the library currently in use.
            string libraryVersion = BuildVersionInfo.AssemblyVersion;

            // Output the versions for diagnostic purposes.
            Console.WriteLine($"Embedded Visio version : {embeddedVisioVersion}");
            Console.WriteLine($"Aspose.Diagram version : {libraryVersion}");

            // Compare the two versions.
            if (embeddedVisioVersion == libraryVersion)
            {
                Console.WriteLine("Version check passed: Embedded Visio version matches the library version.");
            }
            else
            {
                Console.WriteLine("Version mismatch detected:");
                Console.WriteLine($"  - Document was created with Visio version {embeddedVisioVersion}.");
                Console.WriteLine($"  - Current Aspose.Diagram library version is {libraryVersion}.");
                Console.WriteLine("Consider updating the library or verifying compatibility.");
            }
        }
        catch (DiagramException dex)
        {
            // Handle errors specific to Aspose.Diagram operations.
            Console.WriteLine($"Diagram processing error: {dex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors.
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        DiagramVersionDiagnostic.CheckVersion("");
    }
}
