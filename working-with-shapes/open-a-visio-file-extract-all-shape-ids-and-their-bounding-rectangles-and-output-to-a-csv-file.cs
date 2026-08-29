using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output CSV file path
                string outputCsv = "shapes.csv";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Prepare CSV writer
                using (StreamWriter writer = new StreamWriter(outputCsv))
                {
                    // Write CSV header
                    writer.WriteLine("ShapeID,Left,Top,Right,Bottom");

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Retrieve position and size values (in inches)
                            double pinX = shape.XForm.PinX.Value;
                            double pinY = shape.XForm.PinY.Value;
                            double width = shape.XForm.Width.Value;
                            double height = shape.XForm.Height.Value;

                            // Calculate bounding rectangle edges
                            double left = pinX - width / 2.0;
                            double right = pinX + width / 2.0;
                            double top = pinY + height / 2.0;
                            double bottom = pinY - height / 2.0;

                            // Write shape information to CSV
                            writer.WriteLine($"{shape.ID},{left},{top},{right},{bottom}");
                        }
                    }
                }

                Console.WriteLine($"Shape data exported to {outputCsv}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }