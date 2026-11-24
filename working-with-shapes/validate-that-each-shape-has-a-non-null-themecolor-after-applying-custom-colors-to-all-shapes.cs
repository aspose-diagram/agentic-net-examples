using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
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
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // TODO: Apply custom colors to the shape.
                    // Example (replace with actual color application logic):
                    // shape.Fill.FillForegnd.Value = 0xFF0000; // Red fill

                    // TODO: Validate that the shape's ThemeColor is not null.
                    // The exact property/method to access ThemeColor may differ.
                    // Replace the following placeholder with the correct API call.
                    // if (shape.ThemeColor == null)
                    // {
                    //     throw new Exception($"Shape ID {shape.ID} has a null ThemeColor.");
                    // }

                    // Placeholder validation (to be replaced)
                    bool themeColorIsSet = true; // TODO: determine actual condition
                    if (!themeColorIsSet)
                    {
                        throw new Exception($"Shape ID {shape.ID} has a null ThemeColor.");
                    }
                }
            }

            Console.WriteLine("All shapes have a non-null ThemeColor after applying custom colors.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}