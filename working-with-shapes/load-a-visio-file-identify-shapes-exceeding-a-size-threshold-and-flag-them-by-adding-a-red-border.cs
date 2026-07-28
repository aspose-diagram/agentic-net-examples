using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect input and output file paths as command‑line arguments.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioShapeFlagger <inputVisioFile> <outputVisioFile>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Size threshold in inches (example: shapes larger than 2 inches in width or height will be flagged).
            const double sizeThreshold = 2.0;

            try
            {
                // Load the Visio diagram.
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through all pages.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes.
                            if (shape.Del == BOOL.True)
                                continue;

                            // Retrieve shape dimensions.
                            double width = shape.XForm.Width.Value;
                            double height = shape.XForm.Height.Value;

                            // Check if the shape exceeds the size threshold.
                            if (width > sizeThreshold || height > sizeThreshold)
                            {
                                // Flag the shape by adding a red border.
                                shape.Line.LineColor.Value = "#FF0000";          // Red color.
                                shape.Line.LineWeight.Value = 0.02;             // Reasonable line weight (in inches).
                                // Optional: ensure a solid line pattern.
                                // shape.Line.LinePattern.Value = LinePatternValue.Solid;
                                
                                Console.WriteLine($"Flagged shape ID {shape.ID} on page '{page.Name}' (Width={width:F2}, Height={height:F2}).");
                            }
                        }
                    }

                    // Save the modified diagram.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved to '{outputPath}'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing diagram: {ex.Message}");
                throw;
            }
        }
    }