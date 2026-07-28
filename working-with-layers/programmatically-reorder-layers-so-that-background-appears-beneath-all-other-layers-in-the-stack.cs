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

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Search for the shape named "Background"
                foreach (Shape shape in page.Shapes)
                {
                    if (!string.IsNullOrEmpty(shape.Name) &&
                        shape.Name.Equals("Background", StringComparison.OrdinalIgnoreCase))
                    {
                        // Move the background shape to the back of the Z‑order
                        shape.SendToBack();
                    }
                }
            }

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
