using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to a Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages in the diagram
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    Page page = diagram.Pages[i];

                    // Retrieve orientation from PrintProps
                    string orientationStr = "Unknown";
                    try
                    {
                        var orientation = page.PageSheet.PrintProps.PrintPageOrientation.Value;
                        orientationStr = orientation.ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Unable to read orientation for page {i}: {ex.Message}");
                    }

                    // Retrieve ScaleX from PrintProps
                    double scaleX = 0;
                    try
                    {
                        scaleX = page.PageSheet.PrintProps.ScaleX.Value;
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Unable to read ScaleX for page {i}: {ex.Message}");
                    }

                    // Output the page information
                    Console.WriteLine($"Page Index: {i}, Orientation: {orientationStr}, ScaleX: {scaleX}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}