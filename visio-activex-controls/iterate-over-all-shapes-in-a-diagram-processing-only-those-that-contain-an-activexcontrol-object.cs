using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (uses the provided load rule)
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
                        // Example processing: display shape information
                        Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}, ActiveX Type: {shape.ActiveXControl.Type}");
                    }
                }
            }

            // Save the diagram (uses the provided save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
