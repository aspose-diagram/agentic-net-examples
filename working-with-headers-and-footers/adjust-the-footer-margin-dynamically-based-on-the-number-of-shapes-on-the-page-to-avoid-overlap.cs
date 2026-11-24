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

        // Determine output path (same folder, suffix "_updated.vsdx")
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_updated.vsdx");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Determine the maximum number of shapes on any page
            int maxShapeCount = 0;
            foreach (Page page in diagram.Pages)
            {
                // ShapeCollection provides a Count property
                int shapeCount = page.Shapes.Count;
                if (shapeCount > maxShapeCount)
                    maxShapeCount = shapeCount;
            }

            // Compute a dynamic footer margin based on shape count
            // Base margin: 0.5 inches; add 0.1 inch for every 10 shapes
            double baseMargin = 0.5;
            double extraMargin = (maxShapeCount / 10) * 0.1;
            double newFooterMargin = baseMargin + extraMargin;

            // Apply the calculated margin to the document's footer
            // FooterMargin is a Margin object; set its Value (in inches)
            diagram.HeaderFooter.FooterMargin.Value = newFooterMargin;

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            Console.WriteLine($"Applied footer margin: {newFooterMargin} inches (based on max {maxShapeCount} shapes on a page).");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}