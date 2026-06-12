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
                string csvPath = "ShapesReport.csv";

                // Area threshold in square inches (adjust as needed)
                double areaThreshold = 1.0;

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Prepare CSV writer
                    using (StreamWriter writer = new StreamWriter(csvPath, false))
                    {
                        // Write CSV header
                        writer.WriteLine("ShapeID,ShapeName,Area");

                        // Iterate through all pages
                        foreach (Page page in diagram.Pages)
                        {
                            // Iterate through all shapes on the page
                            foreach (Shape shape in page.Shapes)
                            {
                                // Skip deleted shapes
                                if (shape.Del == BOOL.True)
                                    continue;

                                // Retrieve width and height (in inches)
                                double width = shape.XForm.Width.Value;
                                double height = shape.XForm.Height.Value;

                                // Calculate area
                                double area = width * height;

                                // Check against threshold
                                if (area > areaThreshold)
                                {
                                    // Shape name may be empty; use empty string if null
                                    string shapeName = shape.Name ?? string.Empty;

                                    // Write record to CSV
                                    writer.WriteLine($"{shape.ID},{shapeName},{area}");
                                }
                            }
                        }
                    }

                    // Save the diagram as CSV (optional, if you also want the diagram saved in CSV format)
                    diagram.Save(csvPath, SaveFileFormat.Csv);
                }

                Console.WriteLine($"Shape area report generated at: {Path.GetFullPath(csvPath)}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }