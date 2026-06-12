using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output CSV file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioCsvExport <inputVisioPath> <outputCsvPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare CSV writer
                using (StreamWriter writer = new StreamWriter(outputPath))
                {
                    // Write CSV header
                    writer.WriteLine("ID,Type,AreaSqMm");

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted
                            if (shape.Del == BOOL.True)
                                continue;

                            // Ensure XForm is available
                            if (shape.XForm == null)
                                continue;

                            // Width and Height are in inches; convert to millimeters (1 inch = 25.4 mm)
                            double widthInches = shape.XForm.Width.Value;
                            double heightInches = shape.XForm.Height.Value;
                            double areaSqMm = widthInches * heightInches * 25.4 * 25.4;

                            // Write shape information to CSV
                            writer.WriteLine($"{shape.ID},{shape.Type},{areaSqMm:F2}");
                        }
                    }
                }
            }

            Console.WriteLine($"CSV export completed: {outputPath}");
        }
    }