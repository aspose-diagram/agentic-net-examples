using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file (adjust the path as needed)
                string inputPath = "input.vsdx";

                // Output image file showing the diagram after routing adjustments
                string outputPath = "output.png";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Work with the first page (modify as required)
                Page page = diagram.Pages[0];

                Console.WriteLine("Connector routing information:");

                // Iterate through all connections on the page
                foreach (Connect conn in page.Connects)
                {
                    long fromId = conn.FromSheet;
                    long toId = conn.ToSheet;
                    string fromCell = conn.FromCell;
                    string toCell = conn.ToCell;

                    // Retrieve the source and target shapes
                    Shape fromShape = page.Shapes.GetShape(fromId);
                    Shape toShape = page.Shapes.GetShape(toId);

                    Console.WriteLine($"From Shape ID {fromId} (Name: {fromShape.Name}) " +
                                      $"to Shape ID {toId} (Name: {toShape.Name})");
                    Console.WriteLine($"  FromCell: {fromCell}, ToCell: {toCell}");
                }

                // Example: set all connector routing to right‑angle style
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.OneD) // 1‑D shapes are connectors
                    {
                        shape.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;
                    }
                }

                // Save the modified diagram as a PNG image
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                diagram.Save(outputPath, pngOptions);

                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }