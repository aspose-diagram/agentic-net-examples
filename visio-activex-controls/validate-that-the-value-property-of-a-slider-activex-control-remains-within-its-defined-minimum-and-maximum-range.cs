using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio diagram file
        string diagramPath = "input.vsdx";
        if (!System.IO.File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Guard to ensure the file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages and shapes to find Slider ActiveX controls
            foreach (Page page in diagram.Pages)
            {
                // Shapes collection is indexed by zero‑based integer
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];

                    // Ensure the shape actually contains an ActiveX control
                    if (shape.ActiveXControl == null)
                        continue;

                    // TODO: The Aspose.Diagram API does not currently expose a SliderActiveXControl class
                    // or a ControlType.Slider enumeration value. When such support becomes available,
                    // replace the placeholder logic below with proper casting and property checks,
                    // e.g.:
                    // if (shape.ActiveXControl.Type == ControlType.Slider)
                    // {
                    //     var slider = (SliderActiveXControl)shape.ActiveXControl;
                    //     // Validate slider.Value against slider.Minimum and slider.Maximum
                    // }

                    // Placeholder: output shape ID for manual inspection
                    Console.WriteLine($"Shape ID {shape.ID} contains an ActiveX control of type {shape.ActiveXControl.Type}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}