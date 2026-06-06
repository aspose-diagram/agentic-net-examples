using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file containing the ActiveX control
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                bool validationPassed = true;

                // Iterate through all pages and shapes to find SpinButton ActiveX controls
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has an ActiveX control
                        if (shape.ActiveXControl == null)
                            continue;

                        // Check if the control type is SpinButton (used as a slider replacement)
                        if (shape.ActiveXControl.Type == ControlType.SpinButton)
                        {
                            // Cast to the specific control class
                            SpinButtonActiveXControl spinControl = (SpinButtonActiveXControl)shape.ActiveXControl;

                            double min = spinControl.Min;
                            double max = spinControl.Max;
                            double value = spinControl.Position; // Current value

                            // Validate the value is within the defined range
                            if (value < min || value > max)
                            {
                                validationPassed = false;
                                Console.WriteLine($"Validation failed for shape ID {shape.ID}: Value {value} is outside the range [{min}, {max}].");
                                // Optionally throw an exception to halt execution
                                throw new Exception($"SpinButton value out of bounds on shape ID {shape.ID}.");
                            }
                            else
                            {
                                Console.WriteLine($"Shape ID {shape.ID}: Value {value} is within the range [{min}, {max}].");
                            }
                        }
                    }
                }

                if (validationPassed)
                {
                    Console.WriteLine("All SpinButton (Slider) controls have values within their defined ranges.");
                }

                // Optionally save the diagram if any modifications were made
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }