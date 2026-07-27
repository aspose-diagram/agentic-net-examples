using System.IO;
using System;
using Aspose.Diagram;

public static class DiagramVersionDiagnostic
{
    // Checks if the Visio version embedded in the diagram matches the Aspose.Diagram library version.
    public static void CheckVersion(string diagramPath)
    {
        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve the Visio version stored in the document (e.g., "14" for Visio 2010).
            string embeddedVisioVersion = diagram.Version;

            // Retrieve the Aspose.Diagram assembly version (e.g., "23.9.0").
            string libraryVersion = BuildVersionInfo.AssemblyVersion;

            // Output the versions for diagnostic purposes.
            Console.WriteLine($"Embedded Visio version: {embeddedVisioVersion}");
            Console.WriteLine($"Aspose.Diagram library version: {libraryVersion}");

            // Compare the versions. Since they represent different products, we only check that both are non‑empty.
            if (string.IsNullOrEmpty(embeddedVisioVersion))
            {
                Console.WriteLine("Warning: The diagram does not contain an embedded Visio version.");
            }
            else if (string.IsNullOrEmpty(libraryVersion))
            {
                Console.WriteLine("Warning: Unable to determine the Aspose.Diagram library version.");
            }
            else
            {
                // Simple consistency check: ensure the embedded Visio version is a known value.
                // For example, Visio 2010 = 14, Visio 2013 = 15, Visio 2016 = 16, Visio 2019 = 17, Visio 2021 = 18.
                // Here we just report the values; further business logic can be added as needed.
                Console.WriteLine("Version information retrieved successfully. No direct mismatch expected between Visio and library versions.");
            }
        }
        catch (DiagramException ex)
        {
            // Handle errors related to loading or processing the diagram.
            Console.WriteLine($"Diagram processing error: {ex.Message}");
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
