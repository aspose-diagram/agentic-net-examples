using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file containing the ActiveX control
        string inputPath = "input.vsdx";
        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that do not host an ActiveX control
                    if (shape.ActiveXControl == null)
                        continue;

                    // Process only SpinButton (used as a Slider) controls
                    if (shape.ActiveXControl.Type == ControlType.SpinButton)
                    {
                        // Cast the generic control to its specific type
                        SpinButtonActiveXControl spinCtrl = (SpinButtonActiveXControl)shape.ActiveXControl;

                        // Retrieve the defined range using the correct property names (Min/Max)
                        double min = spinCtrl.Min;      // Minimum value of the slider
                        double max = spinCtrl.Max;      // Maximum value of the slider
                        double current = spinCtrl.Position; // Current slider value

                        // Validate that the current value lies within the defined range
                        if (current < min || current > max)
                        {
                            string msg = $"Shape ID {shape.ID} on page {page.ID} has an out-of-range value. " +
                                         $"Position={current}, Minimum={min}, Maximum={max}.";
                            // Throw an exception to indicate validation failure
                            throw new Exception(msg);
                        }
                        else
                        {
                            Console.WriteLine($"Shape ID {shape.ID} on page {page.ID} is valid. " +
                                              $"Position={current} within [{min}, {max}].");
                        }
                    }
                }
            }

            Console.WriteLine("Validation completed successfully.");
        }
        catch (Exception ex)
        {
            // Output any errors encountered during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}