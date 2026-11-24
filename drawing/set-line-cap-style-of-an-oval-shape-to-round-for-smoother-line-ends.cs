using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to find an oval shape
            bool ovalFound = false;
            foreach (Page page in diagram.Pages)
            {
                // Shape collection iteration
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Identify oval shapes by their NameU containing "Oval"
                    // (Visio master name for an oval is typically "Oval")
                    if (!string.IsNullOrEmpty(shape.NameU) && shape.NameU.IndexOf("Oval", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // Set line cap style to round (BOOL.True)
                        shape.Line.LineCap.Value = BOOL.True;

                        // Optional: you can also set rounding for smoother corners if desired
                        // shape.Line.Rounding.Value = 0.3125;

                        ovalFound = true;
                        Console.WriteLine($"Rounded line caps applied to shape ID {shape.ID} on page \"{page.NameU}\".");
                    }
                }
            }

            if (!ovalFound)
            {
                Console.WriteLine("No oval shape was found in the diagram.");
            }

            // Prepare output file path
            string directory = Path.GetDirectoryName(inputPath);
            string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_RoundedCap.vsdx");

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}