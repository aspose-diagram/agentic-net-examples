using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file (adjust as needed)
        string inputPath = "input.vsdx";

        // Verify that the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram does not contain any pages.");
                return;
            }

            // Access the first page in the diagram
            Page page = diagram.Pages[0];

            // Ensure the page contains at least one shape
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The page does not contain any shapes.");
                return;
            }

            // Retrieve the first shape on the page
            Shape shape = page.Shapes[0];

            // Define custom width and height for the shape's text block (in inches)
            double customTextWidth = 2.5;   // example width
            double customTextHeight = 1.0;  // example height

            // Apply the custom dimensions to the shape's TextXForm (text block transform)
            shape.TextXForm.TxtWidth.Value = customTextWidth;
            shape.TextXForm.TxtHeight.Value = customTextHeight;

            // Retrieve the resulting bounding box dimensions of the text block
            double boundingBoxWidth = shape.TextXForm.TxtWidth.Value;
            double boundingBoxHeight = shape.TextXForm.TxtHeight.Value;

            // Output the calculated bounding box dimensions to the console
            Console.WriteLine($"Bounding box width: {boundingBoxWidth} inches");
            Console.WriteLine($"Bounding box height: {boundingBoxHeight} inches");

            // (Optional) Save the modified diagram to a new file to persist changes
            string outputPath = "output_modified.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram exceptions to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}