using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate that at least one file path is provided.
        if (args == null || args.Length == 0)
        {
            Console.Error.WriteLine("Please provide one or more diagram file paths as arguments.");
            return;
        }

        // Process each diagram file supplied via command‑line arguments.
        foreach (string inputPath in args)
        {
            // Guard against missing files.
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required by the API.
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Determine whether the diagram contains at least one shape.
                    bool hasAtLeastOneShape = false;
                    foreach (Page page in diagram.Pages)
                    {
                        if (page.Shapes != null && page.Shapes.Count > 0)
                        {
                            hasAtLeastOneShape = true;
                            break;
                        }
                    }

                    // If no shapes are present, report and skip further processing.
                    if (!hasAtLeastOneShape)
                    {
                        Console.Error.WriteLine($"Diagram '{inputPath}' contains no shapes.");
                        continue;
                    }

                    // Example processing: count total shapes and report.
                    int totalShapeCount = 0;
                    foreach (Page page in diagram.Pages)
                    {
                        if (page.Shapes != null)
                        {
                            totalShapeCount += page.Shapes.Count;
                        }
                    }

                    Console.WriteLine($"Diagram '{inputPath}' contains {totalShapeCount} shape(s).");
                    
                    // Additional processing can be placed here.
                }
            }
            catch (Exception ex)
            {
                // Report any errors that occur during loading or processing.
                Console.Error.WriteLine($"Error processing file '{inputPath}': {ex.Message}");
            }
        }
    }
}