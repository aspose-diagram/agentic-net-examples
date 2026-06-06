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
                    // Verify that the shape actually contains an ActiveX control
                    if (shape.ActiveXControl != null)
                    {
                        // Safe to access members of the ActiveX control
                        bool isEnabled = shape.ActiveXControl.IsEnabled;

                        // Example usage: output the control state
                        System.Console.WriteLine($"Shape ID {shape.ID} has ActiveX control. IsEnabled = {isEnabled}");
                    }
                    else
                    {
                        // Shape does not have an ActiveX control; avoid null reference
                        System.Console.WriteLine($"Shape ID {shape.ID} does not contain an ActiveX control.");
                    }
                }
            }

            // Save the diagram (if any changes were made)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
