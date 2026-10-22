using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define the output file path
        string outputPath = "output.vsdx";

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                // Add a default page if none exist
                diagram.Pages.Add(new Page());
            }

            // Get the first page
            Page page = diagram.Pages[0];

            // -----------------------------------------------------------------
            // Create a new rectangle shape on the page
            // DrawRectangle signature: (pinX, pinY, width, height)
            // -----------------------------------------------------------------
            long shapeId = page.DrawRectangle(2.0, 2.0, 2.0, 2.0);
            Shape shape = page.Shapes.GetShape(shapeId);

            // -----------------------------------------------------------------
            // Set multiline paragraph text with bullet points
            // -----------------------------------------------------------------
            // Clear any existing paragraphs
            shape.Paras.Clear();

            // Paragraph 1 – bullet
            Para para1 = new Para();
            para1.Bullet.Value = BulletValue.Style1; // standard bullet
            shape.Paras.Add(para1);

            // Paragraph 2 – bullet
            Para para2 = new Para();
            para2.Bullet.Value = BulletValue.Style1;
            shape.Paras.Add(para2);

            // Paragraph 3 – normal (no bullet)
            Para para3 = new Para();
            shape.Paras.Add(para3);

            // Clear existing text runs
            shape.Text.Value.Clear();

            // Add text runs corresponding to the paragraphs
            shape.Text.Value.Add(new Txt("First bullet item"));
            shape.Text.Value.Add(new Txt("\n"));
            shape.Text.Value.Add(new Txt("Second bullet item"));
            shape.Text.Value.Add(new Txt("\n"));
            shape.Text.Value.Add(new Txt("A normal line without bullet"));

            // -----------------------------------------------------------------
            // Save the diagram to VSDX format
            // -----------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}