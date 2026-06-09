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
                // Search for a shape whose universal name is "Background"
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == "Background")
                    {
                        // Move the shape to the back of the Z‑order on this page
                        page.SendToBack(shape.ID);
                        // If you prefer the shape instance method, you could also call:
                        // shape.SendToBack();
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
