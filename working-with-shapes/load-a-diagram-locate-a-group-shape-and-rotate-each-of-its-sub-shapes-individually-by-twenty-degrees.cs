using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram from a file
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Get the first page of the diagram
                Page page = diagram.Pages[0];

                // Find group shapes on the page
                foreach (Shape groupShape in page.Shapes)
                {
                    // Identify a group shape by its Type
                    if (groupShape.Type == TypeValue.Group)
                    {
                        // Rotate each sub‑shape inside the group by 20 degrees
                        foreach (Shape subShape in groupShape.Shapes)
                        {
                            // Current rotation angle (in radians)
                            double currentAngle = subShape.XForm.Angle.Value;

                            // Convert 20 degrees to radians
                            double delta = 20.0 * Math.PI / 180.0;

                            // Set the new rotation angle
                            subShape.XForm.Angle.Value = currentAngle + delta;
                        }

                        // If only the first group should be processed, uncomment the next line
                        // break;
                    }
                }

                // Save the modified diagram (optional)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }