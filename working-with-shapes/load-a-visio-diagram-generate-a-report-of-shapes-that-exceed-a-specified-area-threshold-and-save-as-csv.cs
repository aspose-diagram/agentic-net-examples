using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Path for the generated CSV report
            string outputCsvPath = "shape_report.csv";

            // Area threshold in square inches (adjust as needed)
            double areaThreshold = 1.0;

            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare CSV header
                StringBuilder csvBuilder = new StringBuilder();
                csvBuilder.AppendLine("ShapeID,PageName,ShapeName,Area");

                // Iterate through each page
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve width and height (in inches) from the shape's XForm
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;
                        double area = width * height;

                        // If the shape's area exceeds the threshold, add a line to the CSV
                        if (area > areaThreshold)
                        {
                            string csvLine = $"{shape.ID},{page.Name},{shape.Name},{area}";
                            csvBuilder.AppendLine(csvLine);
                        }
                    }
                }

                // Write the CSV content to the specified file
                File.WriteAllText(outputCsvPath, csvBuilder.ToString());
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
