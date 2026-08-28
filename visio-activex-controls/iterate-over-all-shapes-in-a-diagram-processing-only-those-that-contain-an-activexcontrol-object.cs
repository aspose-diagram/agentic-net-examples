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

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Process only shapes that contain an ActiveX control
                    if (shape.ActiveXControl != null)
                    {
                        // Example processing: output shape ID and the type of the ActiveX control
                        Console.WriteLine($"Shape ID {shape.ID} contains ActiveX control of type {shape.ActiveXControl.Type}");

                        // Place additional logic here (e.g., modify properties, collect data, etc.)
                    }
                }
            }

            // Save the diagram (if any modifications were made)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
