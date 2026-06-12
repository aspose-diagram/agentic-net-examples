using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Ensure the diagram has at least five pages (zero‑based index)
                    if (diagram.Pages.Count > 4)
                    {
                        // Retrieve page five (index 4)
                        Page pageFive = diagram.Pages[4];

                        // Angle in radians for -30 degrees
                        double angleRad = -30.0 * Math.PI / 180.0;

                        // Iterate all shapes on the page
                        foreach (Shape shape in pageFive.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.False)
                            {
                                // Rotate the text block within the shape
                                shape.TextXForm.TxtAngle.Value = angleRad;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("The diagram does not contain a fifth page.");
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Text rotation applied and diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }