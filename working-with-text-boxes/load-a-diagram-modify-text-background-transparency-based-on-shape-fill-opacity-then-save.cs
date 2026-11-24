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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a TextBlock section; otherwise skip.
                    if (shape.TextBlock == null)
                        continue;

                    // Retrieve the fill foreground transparency (0.0‑1.0 range).
                    double fillTransparency = shape.Fill.FillForegndTrans.Value;

                    // Convert to percentage for TextBlock background transparency (0‑100).
                    double textBackgroundTransparency = fillTransparency * 100.0;

                    // Apply the calculated transparency to the text background.
                    shape.TextBlock.TextBkgndTrans.Value = textBackgroundTransparency;
                }
            }

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}