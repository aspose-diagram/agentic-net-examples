using System.IO;
using System;
using Aspose.Diagram;

class SetTextDirectionExample
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Desired text direction
            TextDirection newDirection = new TextDirection(TextDirectionValue.Vertical);

            // Iterate through all shapes in the diagram
            foreach (Shape shape in diagram.Pages[0].Shapes)
            {
                try
                {
                    // Check if the shape contains a TextBlock section
                    if (shape.TextBlock != null)
                    {
                        // Set the TextDirection property
                        shape.TextBlock.TextDirection = newDirection;

                        // Refresh shape data after modifying text properties
                        shape.RefreshData();
                    }
                    else
                    {
                        // Handle shapes without a TextBlock (e.g., log or ignore)
                        Console.WriteLine($"Shape ID {shape.ID} does not have a TextBlock. Skipping TextDirection assignment.");
                    }
                }
                catch (Exception ex)
                {
                    // General error handling for unexpected issues
                    Console.WriteLine($"Error processing shape ID {shape.ID}: {ex.Message}");
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
