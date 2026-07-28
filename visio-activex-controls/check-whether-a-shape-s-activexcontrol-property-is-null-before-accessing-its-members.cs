using System.IO;
using System;
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
                    // Check whether the shape contains an ActiveX control
                    if (shape.ActiveXControl != null)
                    {
                        // Safe to access members of the ActiveX control
                        bool isEnabled = shape.ActiveXControl.IsEnabled;

                        // Example usage: output the control state
                        Console.WriteLine($"Shape ID {shape.ID} has an ActiveX control. IsEnabled = {isEnabled}");
                    }
                    else
                    {
                        // Shape does not have an ActiveX control
                        Console.WriteLine($"Shape ID {shape.ID} does not contain an ActiveX control.");
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
