using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure at least the input file path is provided
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: program <inputVisioPath> [outputReportPath]");
            return;
        }

        // Assign input path and verify the file exists
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Optional output path – if supplied, verify its directory exists
        string outputPath = args.Length > 1 ? args[1] : null;
        if (outputPath != null)
        {
            string dir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Console.Error.WriteLine($"Output directory does not exist: {dir}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram from the supplied file
            Diagram diagram = new Diagram(inputPath);

            // Collection to hold description lines for shapes with broken data links
            List<string> brokenShapes = new List<string>();

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Determine whether the shape contains any shape‑data fields (Data1‑Data3)
                    bool hasData = !string.IsNullOrEmpty(shape.Data1) ||
                                   !string.IsNullOrEmpty(shape.Data2) ||
                                   !string.IsNullOrEmpty(shape.Data3);

                    // Skip shapes that do not carry shape‑data
                    if (!hasData) continue;

                    // A shape is considered to have a valid link only if the diagram contains at least one data connection
                    bool linkValid = diagram.DataConnections != null && diagram.DataConnections.Count > 0;

                    // If no data connections exist, the shape’s data link is broken
                    if (!linkValid)
                    {
                        // Build a readable description of the problematic shape
                        string description = $"Page: {page.NameU}, Shape ID: {shape.ID}, NameU: {shape.NameU}, " +
                                             $"Data1: \"{shape.Data1}\", Data2: \"{shape.Data2}\", Data3: \"{shape.Data3}\"";

                        // Add the description to the report list
                        brokenShapes.Add(description);
                    }
                }
            }

            // Output the report either to a file or to the console
            if (outputPath != null)
            {
                // Write the report to the specified file
                using (StreamWriter writer = new StreamWriter(outputPath))
                {
                    writer.WriteLine("Broken Data Link Report");
                    writer.WriteLine($"Generated on: {DateTime.Now}");
                    writer.WriteLine($"Source diagram: {inputPath}");
                    writer.WriteLine();

                    if (brokenShapes.Count == 0)
                    {
                        writer.WriteLine("No broken data links found.");
                    }
                    else
                    {
                        foreach (string line in brokenShapes)
                        {
                            writer.WriteLine(line);
                        }
                    }
                }

                Console.WriteLine($"Report written to {outputPath}");
            }
            else
            {
                // Write the report directly to the console
                Console.WriteLine("Broken Data Link Report");
                Console.WriteLine($"Source diagram: {inputPath}");
                Console.WriteLine();

                if (brokenShapes.Count == 0)
                {
                    Console.WriteLine("No broken data links found.");
                }
                else
                {
                    foreach (string line in brokenShapes)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any unexpected errors and write them to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}