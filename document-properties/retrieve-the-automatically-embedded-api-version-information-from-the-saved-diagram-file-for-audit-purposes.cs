using System.IO;
using System;
using Aspose.Diagram;

class RetrieveDiagramVersion
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram from file
            var diagram = new Diagram("sample.vsdx");

            // Retrieve the embedded Visio version information
            string visioVersion = diagram.Version;   // e.g., "14" for Visio 2010
            long buildNumber = diagram.Buildnum;     // Build number of the Visio instance

            // Output the retrieved version details for audit purposes
            Console.WriteLine($"Visio Version: {visioVersion}");
            Console.WriteLine($"Build Number: {buildNumber}");

            // Dispose the diagram object to release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
