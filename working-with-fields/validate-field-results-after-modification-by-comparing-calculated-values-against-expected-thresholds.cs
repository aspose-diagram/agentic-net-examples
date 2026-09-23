using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page and one shape
            if (diagram.Pages.Count == 0)
                throw new Exception("The diagram contains no pages.");

            Page page = diagram.Pages[0];
            if (page.Shapes.Count == 0)
                throw new Exception("The first page contains no shapes.");

            // Retrieve the first shape on the page
            Shape shape = page.Shapes[0];

            // Ensure the shape has at least one field to modify
            if (shape.Fields.Count == 0)
                throw new Exception("The selected shape does not contain any fields.");

            // Modify the first field's value
            Field field = shape.Fields[0];
            // Example: set a numeric value as string
            field.Value.Val = "123.45";

            // Optionally, set a formula (Visio formula syntax) for the field
            // field.Value.Ufev.F = "Width*Height";

            // Refresh the shape to apply changes
            shape.RefreshData();

            // Validate the modified field against expected thresholds
            // Parse the field value to a double for comparison
            if (!double.TryParse(field.Value.Val, out double numericValue))
                throw new Exception("Failed to parse the field value to a numeric type.");

            double lowerThreshold = 100.0;
            double upperThreshold = 200.0;

            if (numericValue < lowerThreshold || numericValue > upperThreshold)
            {
                // Validation failed
                throw new Exception($"Field value {numericValue} is outside the expected range [{lowerThreshold}, {upperThreshold}].");
            }
            else
            {
                // Validation succeeded
                Console.WriteLine($"Field value {numericValue} is within the expected range.");
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
