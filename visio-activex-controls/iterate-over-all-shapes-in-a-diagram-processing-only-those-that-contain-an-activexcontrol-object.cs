using System.IO;
using System;
using Aspose.Diagram;

class ActiveXControlProcessor
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape contains an ActiveX control
                    if (shape.ActiveXControl != null)
                    {
                        // Process the shape as needed.
                        // Example: output shape information to the console.
                        Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}, ActiveX Type: {shape.ActiveXControl.Type}");

                        // Additional processing can be added here.
                    }
                }
            }

            // Save the diagram after processing (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
