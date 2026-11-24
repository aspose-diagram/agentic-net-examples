using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to a Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Check if the diagram contains any masters.
                if (diagram.Masters == null || diagram.Masters.Count == 0)
                {
                    Console.WriteLine("No master shapes found in the diagram.");
                    return;
                }

                Console.WriteLine("Master shapes in the diagram:");
                // Iterate over each master and output its identifying information.
                foreach (Master master in diagram.Masters)
                {
                    // master.UniqueID is a Guid; it is never null.
                    Console.WriteLine($"Name: {master.Name}, ID: {master.ID}, UniqueID: {master.UniqueID}");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}