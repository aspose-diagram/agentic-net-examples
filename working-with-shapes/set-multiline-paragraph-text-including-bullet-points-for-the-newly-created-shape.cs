using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path (must exist). Adjust as needed.
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output diagram path.
        string outputPath = "output.vsdx";

        try
        {
            // Load the existing diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Draw a simple rectangle shape on the first page.
            // Parameters: pinX, pinY, width, height (in inches).
            // Adjust coordinates as needed.
            double pinX = 5.0;
            double pinY = 5.0;
            double width = 2.0;
            double height = 1.0;

            // The DrawRectangle method returns the shape ID.
            long shapeId = diagram.Pages[0].DrawRectangle(pinX, pinY, width, height);

            // Retrieve the newly created shape.
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine("Failed to retrieve the newly created shape.");
                return;
            }

            // ------------------------------------------------------------
            // Set multiline paragraph text with bullet points.
            // ------------------------------------------------------------

            // Ensure the shape has at least one paragraph.
            if (shape.Paras.Count == 0)
            {
                // Add a default paragraph if none exist.
                shape.Paras.Add(new Para());
            }

            // Configure the first paragraph to use bullet style.
            Para para = shape.Paras[0];
            para.Bullet.Value = BulletValue.Style1; // Use a standard bullet.
            para.BulletStr.Value = ""; // Empty string uses default bullet character.

            // Clear any existing text.
            shape.Text.Value.Clear();

            // Add multiline text. Each line will be a separate Txt object.
            // The bullet formatting applies to the whole paragraph.
            shape.Text.Value.Add(new Txt("First bullet point"));
            shape.Text.Value.Add(new Txt("\r\nSecond bullet point"));
            shape.Text.Value.Add(new Txt("\r\nThird bullet point"));

            // Optionally adjust paragraph spacing (example: 0.1 inches before and after).
            para.SpBefore.Value = 0.1;
            para.SpAfter.Value = 0.1;
            para.SpLine.Value = 1.0; // Single line spacing.

            // ------------------------------------------------------------
            // Save the modified diagram.
            // ------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}