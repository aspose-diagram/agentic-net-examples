using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Flag to indicate whether a rectangle shape was processed
                bool rectangleFound = false;

                // Iterate through all pages and shapes to locate a rectangle master shape
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify rectangle shapes by master name (common master name is "Rectangle")
                        if (shape.Master != null && shape.Master.Name == "Rectangle")
                        {
                            // Calculate corner coordinates based on XForm properties
                            double pinX = shape.XForm.PinX.Value;
                            double pinY = shape.XForm.PinY.Value;
                            double width = shape.XForm.Width.Value;
                            double height = shape.XForm.Height.Value;
                            double locPinX = shape.XForm.LocPinX.Value;
                            double locPinY = shape.XForm.LocPinY.Value;

                            // Left, Right, Top, Bottom positions
                            double left = pinX - (width / 2.0) + locPinX;
                            double right = pinX + (width / 2.0) + locPinX;
                            double top = pinY + (height / 2.0) + locPinY;
                            double bottom = pinY - (height / 2.0) + locPinY;

                            // Helper to add a connection point with absolute coordinates
                            void AddCornerConnection(double x, double y)
                            {
                                Connection conn = new Connection();
                                // Use absolute numeric formulas
                                conn.X.Ufe.F = x.ToString("G", System.Globalization.CultureInfo.InvariantCulture);
                                conn.Y.Ufe.F = y.ToString("G", System.Globalization.CultureInfo.InvariantCulture);
                                shape.Connections.Add(conn);
                            }

                            // Add connection points at the four corners
                            AddCornerConnection(left, top);     // Top‑Left
                            AddCornerConnection(right, top);    // Top‑Right
                            AddCornerConnection(right, bottom); // Bottom‑Right
                            AddCornerConnection(left, bottom);  // Bottom‑Left

                            rectangleFound = true;
                            // Assuming only one rectangle needs processing; break out of loops
                            break;
                        }
                    }

                    if (rectangleFound)
                        break;
                }

                if (!rectangleFound)
                {
                    Console.Error.WriteLine("No rectangle shape with master name \"Rectangle\" was found in the diagram.");
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}