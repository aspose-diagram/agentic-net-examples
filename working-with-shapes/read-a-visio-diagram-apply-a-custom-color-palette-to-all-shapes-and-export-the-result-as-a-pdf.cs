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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define a custom color palette (hex strings).
                string[] palette = new string[]
                {
                    "#FF5733", // reddish
                    "#33FF57", // greenish
                    "#3357FF", // bluish
                    "#FF33A8", // pink
                    "#A833FF", // purple
                    "#33FFF6"  // cyan
                };

                int shapeCounter = 0;

                // Iterate over all pages and shapes to apply the palette.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Choose a color from the palette, cycling if necessary.
                        string colorHex = palette[shapeCounter % palette.Length];

                        // Ensure the shape has a solid fill pattern.
                        shape.Fill.FillPattern.Value = 1; // 1 = solid fill

                        // Apply the foreground fill color.
                        shape.Fill.FillForegnd.Value = colorHex;

                        shapeCounter++;
                    }
                }

                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Explicitly set format.

                // Export the modified diagram to PDF.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Diagram successfully saved as PDF to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}