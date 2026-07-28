using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class ShapeInheritanceCsvExporter
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path (change as needed)
            string visioFilePath = "input.vsdx";

            // Output CSV file path
            string csvFilePath = "shape_inheritance_summary.csv";

            // Load the Visio diagram
            Diagram diagram = new Diagram(visioFilePath);

            // Prepare a StringBuilder for CSV content
            StringBuilder csvBuilder = new StringBuilder();

            // Write CSV header
            csvBuilder.AppendLine("PageName,ShapeID,ShapeName,MasterName,InheritPropsCount,InheritGeomsCount");

            // Iterate through all pages and their shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Gather required information
                    string pageName = page.NameU ?? string.Empty;
                    string shapeId = shape.ID.ToString();
                    string shapeName = shape.NameU ?? string.Empty;
                    string masterName = shape.Master != null ? shape.Master.NameU ?? string.Empty : string.Empty;
                    int inheritPropsCount = shape.InheritProps != null ? shape.InheritProps.Count : 0;
                    int inheritGeomsCount = shape.InheritGeoms != null ? shape.InheritGeoms.Count : 0;

                    // Build CSV line (escape commas if needed)
                    string csvLine = string.Format("{0},{1},{2},{3},{4},{5}",
                        EscapeCsv(pageName),
                        EscapeCsv(shapeId),
                        EscapeCsv(shapeName),
                        EscapeCsv(masterName),
                        inheritPropsCount,
                        inheritGeomsCount);

                    csvBuilder.AppendLine(csvLine);
                }
            }

            // Write CSV content to file
            File.WriteAllText(csvFilePath, csvBuilder.ToString(), Encoding.UTF8);

            Console.WriteLine("CSV summary generated at: " + Path.GetFullPath(csvFilePath));

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to escape CSV fields containing commas or quotes
    private static string EscapeCsv(string field)
    {
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            field = field.Replace("\"", "\"\"");
            return $"\"{field}\"";
        }
        return field;
    }
}
