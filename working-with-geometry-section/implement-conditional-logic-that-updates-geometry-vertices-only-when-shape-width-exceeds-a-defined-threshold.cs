using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Define the width threshold (in inches)
                double widthThreshold = 2.0;

                // Load an existing Visio diagram
                // Replace "input.vsdx" with the path to your diagram file
                Diagram diagram = new Diagram("input.vsdx");

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape's width exceeds the defined threshold
                        if (shape.XForm.Width.Value > widthThreshold)
                        {
                            // Ensure the shape has at least one geometry section
                            if (shape.Geoms.Count > 0)
                            {
                                // Cast the first geometry to Geom
                                Geom geom = (Geom)shape.Geoms[0];

                                // Create a new LineTo vertex
                                LineTo newVertex = new LineTo();

                                // Example: place the new vertex relative to the shape's position
                                newVertex.X.Value = shape.XForm.PinX.Value + 0.5; // 0.5 inches to the right
                                newVertex.Y.Value = shape.XForm.PinY.Value + 0.5; // 0.5 inches upward

                                // Append the new vertex to the geometry's coordinate collection
                                geom.CoordinateCol.Add(newVertex);
                            }
                        }
                    }
                }

                // Save the modified diagram
                // Replace "output.vsdx" with the desired output path
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }