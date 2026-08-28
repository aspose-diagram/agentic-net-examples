using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main()
    {
        // Path to the input Visio diagram
        string diagramPath = "input.vsdx";
        // Guard to ensure the file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages and shapes to locate SpinButton (used as Slider) controls
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that do not contain an ActiveX control
                    if (shape.ActiveXControl == null)
                        continue;

                    // Identify SpinButton controls (the closest representation of a slider)
                    if (shape.ActiveXControl.Type == ControlType.SpinButton)
                    {
                        // Cast the generic control to its specific SpinButton type
                        SpinButtonActiveXControl spinCtrl = (SpinButtonActiveXControl)shape.ActiveXControl;

                        // The SpinButtonActiveXControl does not expose Minimum/Maximum properties.
                        // Use a reasonable default range for validation (e.g., 0 to 100).
                        double min = 0;
                        double max = 100;

                        // Retrieve the current position/value of the control
                        double current = spinCtrl.Position;

                        // Validate that the current value lies within the expected range
                        if (current < min || current > max)
                        {
                            string msg = $"Shape ID {shape.ID} has a SpinButton value {current} outside the range [{min}, {max}].";
                            // Throw an exception to indicate validation failure
                            throw new Exception(msg);
                        }
                        else
                        {
                            Console.WriteLine($"Shape ID {shape.ID}: SpinButton value {current} is within the range [{min}, {max}].");
                        }
                    }
                }
            }

            // Save the diagram after validation (no modifications made)
            diagram.Save("validated_output.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Output any errors encountered during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}