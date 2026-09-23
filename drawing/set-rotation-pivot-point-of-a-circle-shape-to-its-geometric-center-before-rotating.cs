using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Iterate through all pages and shapes to find the circle shape
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify the circle shape (adjust the condition as needed)
                        // Example: check the master name or a custom name
                        if (shape.Master != null && shape.Master.Name == "Ellipse")
                        {
                            // Set the local pivot (LocPin) to the geometric center
                            // Using formulas that calculate half of the width and height
                            shape.XForm.LocPinX.Ufe.F = "Width*0.5";
                            shape.XForm.LocPinY.Ufe.F = "Height*0.5";

                            // Rotate the shape by the desired angle (degrees)
                            shape.XForm.Angle.Value = 45; // rotate 45 degrees

                            // Optionally break after processing the first matching shape
                            // break;
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