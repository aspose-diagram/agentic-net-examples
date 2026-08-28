using System.IO;
using System;
using Aspose.Diagram;

class DiagramVersionAudit
{
    static void Main()
    {
        try
        {

            // Path to the saved Visio diagram file
            string filePath = "sample.vsdx";

            // Load the diagram using the provided constructor (load rule)
            using (Diagram diagram = new Diagram(filePath))
            {
                // Retrieve the embedded Visio version information
                string visioVersion = diagram.Version;   // e.g., "14" for Visio 2010
                long buildNumber = diagram.Buildnum;    // Build number of the Visio instance

                // Output the version details for audit purposes
                Console.WriteLine($"Visio Version: {visioVersion}");
                Console.WriteLine($"Build Number: {buildNumber}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
