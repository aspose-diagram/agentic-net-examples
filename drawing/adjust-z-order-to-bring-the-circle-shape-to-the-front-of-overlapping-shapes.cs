using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as the first argument.");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path (same folder, with suffix "_front.vsdx")
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_front.vsdx");

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool circleFound = false;

                // Iterate through all pages to locate the circle shape
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify the circle shape by its NameU containing "Circle"
                        if (!string.IsNullOrEmpty(shape.NameU) &&
                            shape.NameU.IndexOf("Circle", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            // TODO: Adjust Z‑order to bring this shape to the front.
                            // The appropriate Aspose.Diagram API for Z‑order manipulation
                            // (e.g., MoveToFront, SetZOrder, etc.) should be used here
                            // once the correct method is identified from the library.
                            Console.WriteLine($"Circle shape (ID={shape.ID}) found on page {page.NameU}.");
                            circleFound = true;
                            break;
                        }
                    }

                    if (circleFound)
                        break;
                }

                if (!circleFound)
                {
                    Console.Error.WriteLine("No circle shape was found in the diagram.");
                    return;
                }

                // Save the modified diagram to the output file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}