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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Determine the maximum number of shapes on any page.
            int maxShapeCount = 0;
            foreach (Page page in diagram.Pages)
            {
                int shapeCount = 0;
                foreach (Shape shape in page.Shapes)
                {
                    shapeCount++;
                }

                if (shapeCount > maxShapeCount)
                    maxShapeCount = shapeCount;
            }

            // Compute a dynamic footer margin based on shape count.
            // Base margin is 0.5 inches; add 0.1 inch for every 20 shapes.
            double baseMargin = 0.5;
            double extraMargin = (maxShapeCount / 20.0) * 0.1;
            double newFooterMargin = baseMargin + extraMargin;

            // Apply the calculated margin to the diagram's footer.
            diagram.HeaderFooter.FooterMargin.Value = newFooterMargin;

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}