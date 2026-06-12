using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Define the desired text direction (e.g., vertical)
            TextDirection newDirection = new TextDirection(TextDirectionValue.Vertical);

            // Iterate through all shapes on the first page (adjust as needed)
            foreach (Shape shape in diagram.Pages[0].Shapes)
            {
                // Check whether the shape actually contains a TextBlock section
                if (shape.TextBlock != null)
                {
                    try
                    {
                        // Set the TextDirection safely
                        shape.TextBlock.TextDirection = newDirection;
                    }
                    catch (Exception ex)
                    {
                        // Log any unexpected errors while setting the property
                        Console.WriteLine($"Error setting TextDirection for shape ID {shape.ID}: {ex.Message}");
                    }
                }
                else
                {
                    // Inform that the shape lacks a TextBlock and will be skipped
                    Console.WriteLine($"Shape ID {shape.ID} does not have a TextBlock. Skipping.");
                }
            }

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
