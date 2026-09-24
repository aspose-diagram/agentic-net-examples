using Aspose.Diagram;
using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (lifecycle rule)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // CSV output file
            string outputCsv = "shape_ids.csv";

            // Prepare CSV writer
            using (StreamWriter writer = new StreamWriter(outputCsv, false))
            {
                // Write CSV header
                writer.WriteLine("ShapeID");

                // Pattern to match shape names like "Node_*"
                Regex pattern = new Regex(@"^Node_.*$", RegexOptions.IgnoreCase);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Use the universal name of the shape for matching
                        string shapeName = shape.NameU;
                        if (pattern.IsMatch(shapeName))
                        {
                            // Write the shape's ID to CSV
                            writer.WriteLine(shape.ID);
                        }
                    }
                }
            }

            // No modifications made, so saving the diagram is optional
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
