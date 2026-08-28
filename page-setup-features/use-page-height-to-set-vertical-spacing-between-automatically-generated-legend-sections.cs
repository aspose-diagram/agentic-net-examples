using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve the page height (in inches)
                        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Collect legend shapes on the current page.
                        // For this example, we assume legend shapes have names containing "Legend".
                        var legendShapeIds = new System.Collections.Generic.List<long>();
                        foreach (Shape shape in page.Shapes)
                        {
                            if (shape.Name != null && shape.Name.IndexOf("Legend", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                legendShapeIds.Add(shape.ID);
                            }
                        }

                        // If no legend shapes are found, continue to the next page.
                        if (legendShapeIds.Count == 0)
                            continue;

                        // Determine vertical spacing based on page height.
                        // Example: distribute legends evenly within the page, leaving a margin of 0.5 inch at top and bottom.
                        double topMargin = 0.5; // inches
                        double usableHeight = pageHeight - (2 * topMargin);
                        double spacing = usableHeight / (legendShapeIds.Count - 1);

                        // Position each legend shape.
                        for (int i = 0; i < legendShapeIds.Count; i++)
                        {
                            // Retrieve the shape by its ID.
                            Shape legendShape = page.Shapes.GetShape((int)legendShapeIds[i]);

                            // Keep the original horizontal position (PinX).
                            double originalPinX = legendShape.XForm.PinX.Value;

                            // Calculate new vertical position.
                            double newPinY = topMargin + (i * spacing);

                            // Apply the new position.
                            legendShape.XForm.PinX.Value = originalPinX;
                            legendShape.XForm.PinY.Value = newPinY;
                        }
                    }

                    // Save the modified diagram.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Legend spacing adjustment completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }