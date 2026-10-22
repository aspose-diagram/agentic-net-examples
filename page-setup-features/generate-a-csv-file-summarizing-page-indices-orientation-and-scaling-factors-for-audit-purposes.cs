using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output CSV file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "pages_summary.csv";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare CSV writer
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                // Write CSV header
                writer.WriteLine("PageIndex,Orientation,ScaleX,ScaleY");

                // Iterate over pages using a for-loop
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Retrieve the page
                    Page page = diagram.Pages[i];

                    // Default values
                    string orientationStr = "Unknown";
                    double scaleX = 0;
                    double scaleY = 0;

                    // Attempt to read orientation
                    try
                    {
                        // PrintPageOrientationValue enum provides orientation info
                        var orientation = page.PageSheet.PrintProps.PrintPageOrientation.Value;
                        if (orientation != null)
                        {
                            orientationStr = orientation.ToString();
                        }
                    }
                    catch
                    {
                        // Keep orientation as "Unknown" on error
                    }

                    // Attempt to read scaling factors
                    try
                    {
                        scaleX = page.PageSheet.PrintProps.ScaleX.Value;
                        scaleY = page.PageSheet.PrintProps.ScaleY.Value;
                    }
                    catch
                    {
                        // Keep defaults (0) on error
                    }

                    // Write CSV line for the current page
                    writer.WriteLine($"{i},{orientationStr},{scaleX},{scaleY}");
                }
            }

            Console.WriteLine($"CSV summary generated successfully at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}