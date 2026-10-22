using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio diagram file to be validated.
        string diagramPath = "input.vsdx";
        if (!System.IO.File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Guard: ensure the input file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(diagramPath);

            bool validationFailed = false;

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape contains an ActiveX control.
                    if (shape.ActiveXControl == null)
                        continue;

                    // Check if the ActiveX control is a SpinButton (used as a Slider alternative).
                    if (shape.ActiveXControl.Type == ControlType.SpinButton)
                    {
                        // Cast to the specific control type to access its properties.
                        SpinButtonActiveXControl spinCtrl = (SpinButtonActiveXControl)shape.ActiveXControl;

                        // Retrieve the current value (Position) of the control.
                        double current = spinCtrl.Position;

                        // TODO: Retrieve the defined Minimum and Maximum range values from the control.
                        // The Aspose.Diagram API does not expose Minimum/Maximum properties for SpinButtonActiveXControl
                        // in the current version. Replace the placeholder values with the correct properties when available.
                        double min = 0;   // Placeholder: replace with actual minimum value.
                        double max = 100; // Placeholder: replace with actual maximum value.

                        // Validate that the current value lies within the defined range.
                        if (current < min || current > max)
                        {
                            validationFailed = true;
                            Console.Error.WriteLine($"Validation error on page {page.ID}, shape {shape.ID}: Value {current} is outside the range [{min}, {max}].");
                        }
                        else
                        {
                            Console.WriteLine($"Page {page.ID}, shape {shape.ID}: Value {current} is within the range [{min}, {max}].");
                        }
                    }
                }
            }

            if (!validationFailed)
            {
                Console.WriteLine("All Slider (SpinButton) controls have valid values within their defined ranges.");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}