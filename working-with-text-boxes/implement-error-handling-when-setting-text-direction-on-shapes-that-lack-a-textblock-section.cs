using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram.
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Verify that the shape contains a TextBlock section before setting direction.
                    if (shape.TextBlock != null)
                    {
                        // Set the text direction to vertical.
                        shape.TextBlock.TextDirection.Value = TextDirectionValue.Vertical;
                    }
                    else
                    {
                        // Handle shapes without a TextBlock section.
                        Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' does not have a TextBlock section.");
                    }
                }
            }

            // Save the modified diagram.
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
