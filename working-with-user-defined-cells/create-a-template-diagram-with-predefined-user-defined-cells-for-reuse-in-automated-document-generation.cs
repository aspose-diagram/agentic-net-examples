using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first page (default page is created automatically)
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page
            // Parameters: pinX, pinY, width, height, master name, isCalculate (bool)
            double pinX = 2.0;   // inches from left
            double pinY = 2.0;   // inches from top
            double width = 3.0;  // inches
            double height = 1.5; // inches
            long shapeId = page.AddShape(pinX, pinY, width, height, "Rectangle", false);

            // Retrieve the shape object using the returned ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Define user‑defined cells (custom properties) for the shape
            // Example 1: a numeric value
            User userCell1 = new User();
            userCell1.Name = "CustomWidth";
            userCell1.Value.Val = "800";          // store as string
            userCell1.Prompt.Value = "Width in pixels";
            shape.Users.Add(userCell1);

            // Example 2: a text value
            User userCell2 = new User();
            userCell2.Name = "Description";
            userCell2.Value.Val = "Template shape for reuse";
            userCell2.Prompt.Value = "Brief description";
            shape.Users.Add(userCell2);

            // Example 3: a date value
            User userCell3 = new User();
            userCell3.Name = "CreatedDate";
            userCell3.Value.Val = DateTime.Now.ToString("yyyy-MM-dd");
            userCell3.Prompt.Value = "Date the template was created";
            shape.Users.Add(userCell3);

            // Save the diagram as a VSDX template file
            string outputPath = "TemplateDiagram.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Template diagram saved to: {outputPath}");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
