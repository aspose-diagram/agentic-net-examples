using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Output file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Example orientation list for each page (can be customized)
            // For demonstration, we alternate between Portrait, Landscape, and an invalid value
            string[] orientations = new string[diagram.Pages.Count];
            for (int i = 0; i < orientations.Length; i++)
            {
                if (i % 3 == 0)
                    orientations[i] = "Portrait";
                else if (i % 3 == 1)
                    orientations[i] = "Landscape";
                else
                    orientations[i] = "InvalidOrientation";
            }

            // Process each page and apply orientation
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];
                string orientation = orientations[i];

                try
                {
                    // Set page dimensions based on desired orientation
                    if (orientation.Equals("Portrait", StringComparison.OrdinalIgnoreCase))
                    {
                        // Portrait: height greater than width
                        page.PageSheet.PageProps.PageWidth.Value = 8.5;   // inches
                        page.PageSheet.PageProps.PageHeight.Value = 11.0; // inches
                    }
                    else if (orientation.Equals("Landscape", StringComparison.OrdinalIgnoreCase))
                    {
                        // Landscape: width greater than height
                        page.PageSheet.PageProps.PageWidth.Value = 11.0; // inches
                        page.PageSheet.PageProps.PageHeight.Value = 8.5; // inches
                    }
                    else
                    {
                        // Unsupported orientation value – throw to be caught below
                        throw new ArgumentException($"Unsupported orientation '{orientation}' for page index {i}.");
                    }

                    Console.WriteLine($"Page {i + 1}: Orientation set to {orientation}.");
                }
                catch (Exception ex)
                {
                    // Handle unsupported orientation or any other page‑level errors
                    Console.Error.WriteLine($"Error processing page {i + 1}: {ex.Message}");
                    // Continue processing remaining pages
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Global error handling for loading, saving, or other unexpected issues
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}