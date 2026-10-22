using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least three pages
                if (diagram.Pages.Count < 3)
                {
                    Console.Error.WriteLine("The diagram does not contain a third page.");
                    return;
                }

                // Access page three (zero‑based index 2)
                Page page = diagram.Pages[2];

                // Current system date string to replace the placeholder
                string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

                // Iterate all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Iterate each text fragment within the shape
                    foreach (FormatTxt fmt in shape.Text.Value)
                    {
                        // We're interested only in plain text fragments (Txt)
                        if (fmt is Txt txt && txt.Text != null && txt.Text.Contains("[Date]"))
                        {
                            // Replace the placeholder with the current date
                            txt.Text = txt.Text.Replace("[Date]", currentDate);
                        }
                    }
                }

                // Save the modified diagram to the output file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}