using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page
                Page page = diagram.Pages[0];

                // Ensure there is at least one master shape to use for the watermark
                if (diagram.Masters.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no master shapes to create a watermark.");
                    return;
                }

                // Get the name of the first master (e.g., a rectangle)
                string masterName = diagram.Masters[0].Name;

                // Define watermark position and size (in inches)
                double posX = 1.0;   // left
                double posY = 1.0;   // top
                double width = 5.0;
                double height = 2.0;

                // Add the watermark shape to the page using the correct overload
                // Parameters: pinX, pinY, masterName, isCalculate (false)
                long watermarkShapeId = page.AddShape(posX, posY, masterName, false);

                // Retrieve the newly added shape
                Shape watermarkShape = page.Shapes.GetShape(watermarkShapeId);
                if (watermarkShape == null)
                {
                    Console.Error.WriteLine("Failed to retrieve the watermark shape.");
                    return;
                }

                // Optionally set the size of the watermark shape using the defined width/height
                watermarkShape.XForm.Width.Value = width;
                watermarkShape.XForm.Height.Value = height;

                // Set the watermark text
                watermarkShape.Text.Value.Clear();
                watermarkShape.Text.Value.Add(new Txt("CONFIDENTIAL"));

                // Style the watermark: light gray fill and invisible line
                if (watermarkShape.Fill != null && watermarkShape.Fill.FillForegnd != null)
                {
                    // Light gray fill (hex color)
                    watermarkShape.Fill.FillForegnd.Value = "#C8C8C8";
                }

                if (watermarkShape.Line != null && watermarkShape.Line.LineColor != null)
                {
                    // White line (effectively invisible)
                    watermarkShape.Line.LineColor.Value = "#FFFFFF";
                }

                // Send the watermark to the back so it does not obscure other shape text
                watermarkShape.SendToBack();

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
