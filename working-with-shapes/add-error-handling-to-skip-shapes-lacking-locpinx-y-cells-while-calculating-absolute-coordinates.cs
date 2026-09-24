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

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Verify that LocPinX and LocPinY cells exist
                    if (shape.XForm.LocPinX == null || shape.XForm.LocPinY == null)
                    {
                        Console.WriteLine($"Shape ID {shape.ID} does not contain LocPinX/Y cells. Skipping.");
                        continue;
                    }

                    try
                    {
                        // Calculate absolute coordinates based on Pin and LocPin values
                        double absoluteX = shape.XForm.PinX.Value - shape.XForm.LocPinX.Value;
                        double absoluteY = shape.XForm.PinY.Value - shape.XForm.LocPinY.Value;

                        Console.WriteLine($"Shape ID {shape.ID} absolute position: ({absoluteX}, {absoluteY})");
                    }
                    catch (Exception ex)
                    {
                        // Log any unexpected errors and continue processing other shapes
                        Console.WriteLine($"Error processing shape ID {shape.ID}: {ex.Message}");
                    }
                }
            }

            // Save the diagram after processing
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
