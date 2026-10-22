using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
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
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Count the number of shapes on the current page.
                int shapeCount = page.Shapes.Count;

                // Define a height per shape (in inches). Adjust as needed.
                double heightPerShape = 1.0; // 1 inch per shape

                // Calculate the new page height.
                double newPageHeight = shapeCount * heightPerShape;

                // Set the page height using the PageProps property.
                // PageHeight is a DoubleValue; assign to its Value member.
                page.PageSheet.PageProps.PageHeight.Value = newPageHeight;

                // Optional: keep the page width unchanged or set a default.
                // Here we leave the existing width as is.
            }

            // Save the modified diagram to the output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}