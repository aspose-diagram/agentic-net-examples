using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input diagram path and output diagram path as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages and shapes to reset text block margins.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Reset margins to default (0 inches).
                        shape.TextBlock.LeftMargin = new DoubleValue(0, MeasureConst.IN);
                        shape.TextBlock.RightMargin = new DoubleValue(0, MeasureConst.IN);
                        shape.TextBlock.TopMargin = new DoubleValue(0, MeasureConst.IN);
                        shape.TextBlock.BottomMargin = new DoubleValue(0, MeasureConst.IN);
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}