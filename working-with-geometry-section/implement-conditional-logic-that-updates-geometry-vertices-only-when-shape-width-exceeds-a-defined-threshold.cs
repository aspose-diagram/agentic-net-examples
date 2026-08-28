using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Define input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Width threshold for geometry modification (in inches)
                double widthThreshold = 2.0;

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape's width exceeds the threshold
                        if (shape.XForm.Width.Value > widthThreshold)
                        {
                            // Ensure the shape has at least one geometry section
                            if (shape.Geoms.Count > 0)
                            {
                                // Retrieve the first geometry (usually the primary one)
                                Geom geom = (Geom)shape.Geoms[0];

                                // Create a new vertex (LineTo) and set its coordinates
                                LineTo newVertex = new LineTo();
                                // Example: place the new vertex slightly to the right of the shape's current width
                                newVertex.X.Value = shape.XForm.Width.Value + 0.5; // 0.5 inches beyond current width
                                newVertex.Y.Value = shape.XForm.Height.Value;    // Align with current height

                                // Append the new vertex to the geometry's coordinate collection
                                geom.CoordinateCol.Add(newVertex);
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