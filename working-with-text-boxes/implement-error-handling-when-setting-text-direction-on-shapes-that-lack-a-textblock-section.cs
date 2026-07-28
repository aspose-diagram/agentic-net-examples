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
            Diagram diagram = new Diagram("input.vsdx");

            // Define the desired text direction (e.g., vertical)
            var desiredDirection = new TextDirection(TextDirectionValue.Vertical);

            // Iterate through all pages and their shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape actually contains a TextBlock section
                    if (shape.TextBlock != null)
                    {
                        // Set the text direction safely
                        shape.TextBlock.TextDirection = desiredDirection;

                        // Refresh shape data after modifying the text block
                        shape.RefreshData();
                    }
                    else
                    {
                        // Handle shapes without a TextBlock (log, skip, etc.)
                        Console.WriteLine($"Shape ID {shape.ID} on page \"{page.Name}\" does not have a TextBlock.");
                    }
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
