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

            // Iterate through all shapes on all pages
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape contains an ActiveX control
                    if (shape.ActiveXControl != null)
                    {
                        // Identify CheckBox ActiveX controls
                        if (shape.ActiveXControl is CheckBoxActiveXControl checkBox)
                        {
                            // Disable user interaction
                            checkBox.IsEnabled = false;
                        }
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
