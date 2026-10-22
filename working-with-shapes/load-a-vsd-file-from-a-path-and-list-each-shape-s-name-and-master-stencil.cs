using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to a VSD file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file.
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
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the shape's name (fallback to empty string if null).
                        string shapeName = shape.Name ?? string.Empty;

                        // Retrieve the master (stencil) name; handle shapes without a master.
                        string masterName = shape.Master != null ? shape.Master.Name : "None";

                        // Output shape information.
                        Console.WriteLine($"Shape ID: {shape.ID}, Name: {shapeName}, Master: {masterName}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}