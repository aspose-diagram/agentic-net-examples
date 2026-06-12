using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram (uses the provided load rule)
            Aspose.Diagram.Diagram diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // Choose a shape to inspect – here we take the first shape on the first page
            Aspose.Diagram.Page page = diagram.Pages[0];
            Aspose.Diagram.Shape shape = page.Shapes[0];

            // Verify that the shape does NOT inherit fill formatting from a style or master shape
            // Inheritance is considered disabled when the shape's FillStyle is null
            if (shape.FillStyle == null)
            {
                // Retrieve fill details from the shape's own Fill object
                Aspose.Diagram.Fill fill = shape.Fill;

                // Foreground (stroke) color
                string foreColor = fill.FillForegnd?.Value?.ToString() ?? "None";

                // Background color
                string backColor = fill.FillBkgnd?.Value?.ToString() ?? "None";

                // Fill pattern (integer enum value)
                int? pattern = fill.FillPattern?.Value;

                // Output the retrieved values (replace with your own handling as needed)
                System.Console.WriteLine($"Foreground Color: {foreColor}");
                System.Console.WriteLine($"Background Color: {backColor}");
                System.Console.WriteLine($"Fill Pattern: {(pattern.HasValue ? pattern.Value.ToString() : "None")}");
            }
            else
            {
                System.Console.WriteLine("Shape inherits fill formatting; inheritance is enabled.");
            }

            // No saving required – only reading values (uses the provided lifecycle rules)

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
