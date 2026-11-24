using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least three pages (page index is zero‑based).
                if (diagram.Pages.Count < 3)
                {
                    Console.Error.WriteLine("The diagram does not contain a third page.");
                    return;
                }

                // Retrieve page three (index 2).
                Page pageThree = diagram.Pages[2];

                // Iterate all shapes on page three.
                foreach (Shape shape in pageThree.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Iterate all text fragments within the shape.
                    foreach (var txt in shape.Text.Value)
                    {
                        // Only process Txt objects (actual text runs).
                        if (txt is Txt tx && tx.Text != null && tx.Text.Contains("[Date]"))
                        {
                            // Replace the placeholder with the current system date.
                            string currentDate = DateTime.Now.ToString("d");
                            tx.Text = tx.Text.Replace("[Date]", currentDate);
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}