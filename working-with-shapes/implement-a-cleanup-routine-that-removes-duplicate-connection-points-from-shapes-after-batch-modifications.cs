using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path (first argument) and output path (second argument)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output_cleaned.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Collection of connections for the current shape
                    ConnectionCollection connections = shape.Connections;

                    // Track unique connection points using a hash set
                    HashSet<string> uniquePoints = new HashSet<string>();
                    List<int> duplicateIndices = new List<int>();

                    // Identify duplicate connection points
                    for (int i = 0; i < connections.Count; i++)
                    {
                        Connection conn = connections[i];
                        // Build a key based on the X and Y formulas of the connection point
                        string key = $"{conn.X.Ufe.F}|{conn.Y.Ufe.F}";
                        if (!uniquePoints.Add(key))
                        {
                            // Duplicate found; remember its index for removal
                            duplicateIndices.Add(i);
                        }
                    }

                    // Remove duplicates in reverse order to keep indices valid
                    for (int i = duplicateIndices.Count - 1; i >= 0; i--)
                    {
                        connections.RemoveAt(duplicateIndices[i]);
                    }
                }
            }

            // Save the cleaned diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram cleaned and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}