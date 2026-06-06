using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Retrieve the page height (in inches)
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Collect all shapes that represent legend sections.
                // Here we identify them by master name "LegendSection".
                var legendShapes = new System.Collections.Generic.List<Shape>();
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Master != null && shape.Master.Name == "LegendSection")
                    {
                        legendShapes.Add(shape);
                    }
                }

                // If no legend sections are found, nothing to adjust.
                if (legendShapes.Count == 0)
                {
                    Console.WriteLine("No legend sections found on the page.");
                    return;
                }

                // Sort shapes to have a deterministic order (by ID)
                legendShapes.Sort((a, b) => a.ID.CompareTo(b.ID));

                // Calculate vertical spacing based on page height.
                // We leave a margin at top and bottom, so divide the usable height
                // by (count + 1) to get equal gaps.
                double usableHeight = pageHeight * 0.9; // 10% margin total
                double startOffset = pageHeight * 0.05; // 5% top margin
                double spacing = usableHeight / (legendShapes.Count + 1);

                // Position each legend section vertically.
                for (int i = 0; i < legendShapes.Count; i++)
                {
                    Shape legend = legendShapes[i];

                    // Keep the original PinX (horizontal position)
                    double pinX = legend.XForm.PinX.Value;

                    // Compute new PinY based on spacing
                    double pinY = startOffset + spacing * (i + 1);

                    // Apply the new position
                    legend.XForm.PinX.Value = pinX;
                    legend.XForm.PinY.Value = pinY;
                }

                // Save the modified diagram back to a Visio file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Legend sections repositioned based on page height.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
