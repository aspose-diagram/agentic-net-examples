using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Define the width threshold (in inches) above which geometry will be modified
                const double widthThreshold = 2.0;

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the shape's width
                        double shapeWidth = shape.XForm.Width.Value;

                        // Apply geometry changes only when the width exceeds the threshold
                        if (shapeWidth > widthThreshold)
                        {
                            // Ensure the shape has at least one geometry section
                            if (shape.Geoms.Count > 0)
                            {
                                // Get the first geometry (Geom) of the shape
                                Geom geom = (Geom)shape.Geoms[0];

                                // Create a new vertex (LineTo) at the shape's current PinX/PinY position
                                LineTo newVertex = new LineTo();
                                newVertex.X.Value = shape.XForm.PinX.Value;
                                newVertex.Y.Value = shape.XForm.PinY.Value;

                                // Append the new vertex to the geometry's coordinate collection
                                geom.CoordinateCol.Add(newVertex);
                            }
                        }
                    }
                }

                // Save the modified diagram to a new file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }