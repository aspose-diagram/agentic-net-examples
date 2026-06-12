using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths.
                // You can modify these paths as needed or pass them via command‑line arguments.
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                string outputPath = args.Length > 1 ? args[1] : "output_fixed.vsdx";

                // Load the Visio diagram.
                Diagram diagram = new Diagram(inputPath);

                // Default fill color to apply when a shape has no fill color.
                const string defaultFillColor = "#FF0000"; // Red

                // Iterate through all pages and shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the current fill foreground color.
                        string currentFill = shape.Fill.FillForegnd.Value;

                        // If the fill color is missing or empty, assign the default color.
                        if (string.IsNullOrWhiteSpace(currentFill))
                        {
                            // Ensure the fill pattern is solid (value 1).
                            shape.Fill.FillPattern.Value = 1;
                            // Assign the default fill color (hex string).
                            shape.Fill.FillForegnd.Value = defaultFillColor;
                        }
                    }
                }

                // Save the corrected diagram using a valid SaveFileFormat overload.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }