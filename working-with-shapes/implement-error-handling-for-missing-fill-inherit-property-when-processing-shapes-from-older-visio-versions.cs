using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for input and output diagrams
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    try
                    {
                        // Attempt to read inherited fill properties.
                        // Older Visio versions may not expose InheritFill, causing an exception.
                        string foreColor = shape.InheritFill.FillForegnd.Value;
                        string backColor = shape.InheritFill.FillBkgnd.Value;

                        // Example fallback: if both colors are empty, assign a default solid fill.
                        if (string.IsNullOrWhiteSpace(foreColor) && string.IsNullOrWhiteSpace(backColor))
                        {
                            shape.Fill.FillPattern.Value = 1;               // Solid fill
                            shape.Fill.FillForegnd.Value = "#FFFFFF";       // White foreground
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle the case where InheritFill is unavailable.
                        Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' lacks InheritFill. Details: {ex.Message}");

                        // Apply a safe default fill to ensure the shape renders correctly.
                        shape.Fill.FillPattern.Value = 1;               // Solid fill
                        shape.Fill.FillForegnd.Value = "#CCCCCC";       // Light gray foreground
                    }
                }
            }

            // Save the modified diagram back to disk.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
