using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input-visio-file>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool pentagonFound = false;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape uses the "Pentagon" master
                        if (shape.Master != null && shape.Master.Name == "Pentagon")
                        {
                            pentagonFound = true;

                            // Initialize bounding box extremes
                            double minX = double.MaxValue;
                            double minY = double.MaxValue;
                            double maxX = double.MinValue;
                            double maxY = double.MinValue;

                            // Iterate over all geometry sections of the shape
                            foreach (Geom geom in shape.Geoms)
                            {
                                // Each coordinate collection holds geometry segment objects
                                foreach (object coord in geom.CoordinateCol)
                                {
                                    double x = 0;
                                    double y = 0;

                                    // Extract X and Y values based on the concrete segment type
                                    if (coord is MoveTo moveTo)
                                    {
                                        x = moveTo.X.Value;
                                        y = moveTo.Y.Value;
                                    }
                                    else if (coord is LineTo lineTo)
                                    {
                                        x = lineTo.X.Value;
                                        y = lineTo.Y.Value;
                                    }
                                    else if (coord is ArcTo arcTo)
                                    {
                                        x = arcTo.X.Value;
                                        y = arcTo.Y.Value;
                                    }
                                    else if (coord is EllipticalArcTo ellArcTo)
                                    {
                                        x = ellArcTo.X.Value;
                                        y = ellArcTo.Y.Value;
                                    }
                                    // Other segment types (e.g., Spline, Bezier) can be added similarly if needed

                                    // Update bounding box extremes
                                    if (x < minX) minX = x;
                                    if (x > maxX) maxX = x;
                                    if (y < minY) minY = y;
                                    if (y > maxY) maxY = y;
                                }
                            }

                            // Calculate width and height of the bounding box
                            double width = maxX - minX;
                            double height = maxY - minY;

                            // Output the results
                            Console.WriteLine($"Pentagon found on page ID {page.ID} (Name: {page.Name}).");
                            Console.WriteLine("Bounding Box:");
                            Console.WriteLine($"  MinX: {minX}");
                            Console.WriteLine($"  MaxX: {maxX}");
                            Console.WriteLine($"  MinY: {minY}");
                            Console.WriteLine($"  MaxY: {maxY}");
                            Console.WriteLine($"  Width : {width}");
                            Console.WriteLine($"  Height: {height}");

                            // Assuming only one pentagon is needed; break out of loops
                            break;
                        }
                    }

                    if (pentagonFound)
                        break;
                }

                if (!pentagonFound)
                {
                    Console.WriteLine("No shape with master name 'Pentagon' was found in the diagram.");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}