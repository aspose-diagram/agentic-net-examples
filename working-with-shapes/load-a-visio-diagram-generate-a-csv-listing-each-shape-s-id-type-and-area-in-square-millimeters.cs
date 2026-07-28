using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (modify as needed)
                string inputPath = "input.vsdx";

                // Output CSV file path
                string outputCsvPath = "shapes.csv";

                // Conversion factor from inches to millimeters
                const double InchToMillimeter = 25.4;

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Prepare to write CSV
                    using (StreamWriter writer = new StreamWriter(outputCsvPath))
                    {
                        // Write CSV header
                        writer.WriteLine("ID,Type,AreaSqMm");

                        // Iterate through all pages
                        foreach (Aspose.Diagram.Page page in diagram.Pages)
                        {
                            // Iterate through all shapes on the page
                            foreach (Aspose.Diagram.Shape shape in page.Shapes)
                            {
                                // Skip deleted shapes
                                if (shape.Del == BOOL.True)
                                    continue;

                                // Retrieve shape ID
                                long shapeId = shape.ID;

                                // Retrieve shape type as string
                                string shapeType = shape.Type.ToString();

                                // Retrieve width and height in inches
                                double widthInches = shape.XForm.Width.Value;
                                double heightInches = shape.XForm.Height.Value;

                                // Calculate area in square millimeters
                                double areaSqMm = widthInches * heightInches * InchToMillimeter * InchToMillimeter;

                                // Write CSV line
                                writer.WriteLine($"{shapeId},{shapeType},{areaSqMm:F2}");
                            }
                        }
                    }
                }

                Console.WriteLine($"CSV file generated at: {Path.GetFullPath(outputCsvPath)}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }