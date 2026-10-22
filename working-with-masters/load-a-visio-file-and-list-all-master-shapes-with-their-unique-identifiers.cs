using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string visioPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required by Aspose.Diagram.
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Check if any masters are present.
                if (diagram.Masters == null || diagram.Masters.Count == 0)
                {
                    Console.WriteLine("No master shapes found in the diagram.");
                    return;
                }

                Console.WriteLine("Master Shapes in the diagram:");
                Console.WriteLine("--------------------------------");

                // Iterate over all masters and output their identifiers.
                foreach (Master master in diagram.Masters)
                {
                    // UniqueID is a Guid; convert to string for display.
                    string uniqueId = master.UniqueID.ToString();

                    // Output master name and its unique identifier.
                    Console.WriteLine($"Name: {master.Name}, UniqueID: {uniqueId}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}