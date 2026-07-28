using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify group shapes
                        if (shape.Type == TypeValue.Group)
                        {
                            // Initialize bounding box extremes
                            double minX = double.MaxValue;
                            double maxX = double.MinValue;
                            double minY = double.MaxValue;
                            double maxY = double.MinValue;

                            // Iterate through child shapes of the group
                            foreach (Shape child in shape.Shapes)
                            {
                                // Retrieve child's center position and size
                                double pinX = child.XForm.PinX.Value;
                                double pinY = child.XForm.PinY.Value;
                                double width = child.XForm.Width.Value;
                                double height = child.XForm.Height.Value;

                                // Calculate child's extents
                                double left = pinX - width / 2.0;
                                double right = pinX + width / 2.0;
                                double top = pinY + height / 2.0;    // Y increases upwards in Visio
                                double bottom = pinY - height / 2.0;

                                // Update bounding box extremes
                                if (left < minX) minX = left;
                                if (right > maxX) maxX = right;
                                if (bottom < minY) minY = bottom;
                                if (top > maxY) maxY = top;
                            }

                            // Compute bounding box dimensions and center
                            double bboxWidth = maxX - minX;
                            double bboxHeight = maxY - minY;
                            double bboxCenterX = (minX + maxX) / 2.0;
                            double bboxCenterY = (minY + maxY) / 2.0;

                            // Output the results
                            Console.WriteLine($"Group Shape ID: {shape.ID}");
                            Console.WriteLine($"Bounding Box Center: ({bboxCenterX}, {bboxCenterY})");
                            Console.WriteLine($"Bounding Box Width: {bboxWidth}");
                            Console.WriteLine($"Bounding Box Height: {bboxHeight}");
                            Console.WriteLine(new string('-', 40));
                        }
                    }
                }

                // Optional: keep console window open when run outside debugger
                Console.WriteLine("Processing completed. Press any key to exit.");
                Console.ReadKey();

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }