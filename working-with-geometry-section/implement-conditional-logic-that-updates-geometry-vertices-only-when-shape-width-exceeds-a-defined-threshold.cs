using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define the width threshold (in inches) for updating geometry vertices
        double widthThreshold = 2.0;

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the shape's width (in inches)
                        double shapeWidth = shape.XForm.Width.Value;

                        // Update geometry vertices only if the width exceeds the threshold
                        if (shapeWidth > widthThreshold)
                        {
                            // Iterate over each geometry section of the shape
                            foreach (Geom geom in shape.Geoms)
                            {
                                // Iterate over each coordinate (vertex) in the geometry collection
                                // The collection holds objects such as MoveTo, LineTo, etc.
                                for (int i = 0; i < geom.CoordinateCol.Count; i++)
                                {
                                    object coordObj = geom.CoordinateCol[i];

                                    // Handle MoveTo vertices
                                    if (coordObj is MoveTo moveTo)
                                    {
                                        // Shift the vertex by a small offset
                                        moveTo.X.Value += 0.5;
                                        moveTo.Y.Value += 0.5;
                                    }
                                    // Handle LineTo vertices
                                    else if (coordObj is LineTo lineTo)
                                    {
                                        // Shift the vertex by a small offset
                                        lineTo.X.Value += 0.5;
                                        lineTo.Y.Value += 0.5;
                                    }
                                    // Additional vertex types (e.g., ArcTo) can be processed similarly if needed
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram to the output file using the correct overload
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}