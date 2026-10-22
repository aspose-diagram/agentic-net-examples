using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
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

                    // Process connection points if there are multiple connections
                    if (shape.Connections != null && shape.Connections.Count > 1)
                    {
                        // Track unique connection point signatures
                        HashSet<string> uniqueConnections = new HashSet<string>();

                        // Iterate backwards to safely remove duplicates
                        for (int i = shape.Connections.Count - 1; i >= 0; i--)
                        {
                            Connection conn = shape.Connections[i];

                            // Build a key based on X and Y values
                            string key = $"{conn.X}|{conn.Y}";

                            // If this connection point already exists, remove the duplicate
                            if (uniqueConnections.Contains(key))
                            {
                                shape.Connections.RemoveAt(i);
                            }
                            else
                            {
                                uniqueConnections.Add(key);
                            }
                        }
                    }
                }
            }

            // Save the cleaned diagram using the required SaveFileFormat enum
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}