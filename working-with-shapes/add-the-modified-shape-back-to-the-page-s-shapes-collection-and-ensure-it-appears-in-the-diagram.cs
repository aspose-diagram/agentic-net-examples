using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Get the first page
                Page page = diagram.Pages[0];

                // Add a new rectangle shape to the page
                // Parameters: PinX, PinY, master name, isCalculate flag
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);

                // Retrieve the newly added shape using its ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Modify the shape's fill color
                shape.Fill.FillForegnd.Value = "#FF0000";

                // Update the shape's text
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt("Hello Aspose.Diagram"));

                // Refresh shape data to apply changes
                shape.RefreshData();

                // Add the modified shape back to the page's Shapes collection
                // (If the shape had been removed earlier, this ensures it is present)
                page.Shapes.Add(shape);

                // Save the modified diagram to the output file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved successfully.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}