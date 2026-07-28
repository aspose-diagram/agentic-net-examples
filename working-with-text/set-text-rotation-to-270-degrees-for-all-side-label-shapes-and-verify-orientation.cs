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
            Diagram diagram = new Diagram(@"C:\Path\To\YourDiagram.vsdx");

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify side‑label shapes (adjust the condition as needed for your diagram)
                    // Here we assume side‑label shapes have "SideLabel" in their NameU property
                    if (!string.IsNullOrEmpty(shape.NameU) && shape.NameU.Contains("SideLabel", StringComparison.OrdinalIgnoreCase))
                    {
                        // Set text rotation to 270 degrees
                        shape.TextXForm.TxtAngle.Value = 270.0;

                        // Optional verification: output the shape ID and its text angle
                        Console.WriteLine($"Shape ID {shape.ID} ({shape.NameU}) TxtAngle set to {shape.TextXForm.TxtAngle.Value} degrees.");
                    }
                }
            }

            // Save the modified diagram (replace with your desired output path)
            diagram.Save(@"C:\Path\To\ModifiedDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
