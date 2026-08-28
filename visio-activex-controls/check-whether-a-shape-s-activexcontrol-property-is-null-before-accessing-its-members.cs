using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

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
                    // Check if the shape contains an ActiveX control before accessing its members
                    if (shape.ActiveXControl != null)
                    {
                        // Example: read the IsEnabled property of the ActiveX control
                        bool isEnabled = shape.ActiveXControl.IsEnabled;

                        // Perform any required logic with the retrieved value
                        // (e.g., log, modify, or conditionally process the shape)
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
