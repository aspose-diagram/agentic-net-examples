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
                    // Skip shapes that do not have LocPinX or LocPinY cells
                    if (shape.XForm == null ||
                        shape.XForm.LocPinX == null ||
                        shape.XForm.LocPinY == null)
                    {
                        continue;
                    }

                    try
                    {
                        // Retrieve local pin coordinates
                        double locPinX = shape.XForm.LocPinX.Value;
                        double locPinY = shape.XForm.LocPinY.Value;

                        // Example calculation of absolute coordinates (add page offset if required)
                        // Here we simply use the local coordinates as absolute for demonstration
                        double absoluteX = locPinX;
                        double absoluteY = locPinY;

                        // Move the shape to the calculated absolute position
                        shape.MoveTo(absoluteX, absoluteY);
                    }
                    catch (Exception ex)
                    {
                        // Log the error and continue with the next shape
                        Console.WriteLine($"Skipping shape ID {shape.ID}: {ex.Message}");
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
