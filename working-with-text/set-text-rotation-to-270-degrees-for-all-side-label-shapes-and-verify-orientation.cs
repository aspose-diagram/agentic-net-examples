using System;
using Aspose.Diagram;

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
                Diagram diagram = new Diagram(inputPath);

                // Rotation angle in radians for 270 degrees
                double rotationRadians = (Math.PI / 180) * 270;

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify side‑label shapes by name (case‑insensitive)
                        if (!string.IsNullOrEmpty(shape.NameU) &&
                            shape.NameU.IndexOf("SideLabel", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            // Ensure the TextXForm object exists
                            if (shape.TextXForm != null)
                            {
                                // Set the text rotation
                                shape.TextXForm.TxtAngle.Value = rotationRadians;

                                // Verify the rotation was applied correctly
                                double actual = shape.TextXForm.TxtAngle.Value;
                                if (Math.Abs(actual - rotationRadians) > 0.0001)
                                {
                                    throw new Exception($"Rotation verification failed for shape ID {shape.ID}.");
                                }
                            }
                            else
                            {
                                throw new Exception($"Shape ID {shape.ID} does not have a TextXForm.");
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }