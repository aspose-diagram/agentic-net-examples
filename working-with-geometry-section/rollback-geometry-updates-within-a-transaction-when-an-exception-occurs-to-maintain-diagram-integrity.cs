using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Ensure there is at least one page and one shape
        if (diagram.Pages.Count == 0)
        {
            Console.Error.WriteLine("Diagram contains no pages.");
            return;
        }

        Page page = diagram.Pages[0];
        Shape shape = null;
        double originalPinX = 0, originalPinY = 0;

        try
        {
            // Retrieve a shape by its ID (assuming ID 1 exists)
            shape = page.Shapes.GetShape(1);
            if (shape == null)
            {
                Console.Error.WriteLine("Shape with ID 1 not found.");
                return;
            }

            // Store original geometry for rollback
            originalPinX = shape.XForm.PinX.Value;
            originalPinY = shape.XForm.PinY.Value;

            // Modify shape position
            shape.XForm.PinX.Value += 1.0; // shift right by 1 inch
            shape.XForm.PinY.Value += 0.5; // shift up by 0.5 inch

            // Save the updated diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Roll back geometry changes if shape was modified
            if (shape != null)
            {
                shape.XForm.PinX.Value = originalPinX;
                shape.XForm.PinY.Value = originalPinY;
            }
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}