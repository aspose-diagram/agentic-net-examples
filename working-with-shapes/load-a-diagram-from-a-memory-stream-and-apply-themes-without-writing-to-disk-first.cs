using System;
using System.IO;
using Aspose.Diagram;

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

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio file into a memory stream.
            using (FileStream fileStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            using (MemoryStream memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                memoryStream.Position = 0; // Reset position for reading.

                // Create a Diagram instance from the memory stream.
                Diagram diagram = new Diagram(memoryStream);

                // Apply a preset theme to each page in the diagram.
                // The Bubble theme is used as an example.
                foreach (Page page in diagram.Pages)
                {
                    page.PresetTheme = PresetThemeValue.Bubble;
                }

                // Optionally, apply the same theme to all shapes on each page.
                // Uncomment the following block if shape‑level theming is required.
                /*
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        shape.PresetTheme = PresetThemeValue.Bubble;
                    }
                }
                */

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during Aspose operations to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}