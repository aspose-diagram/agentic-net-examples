using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class ShapeInheritanceCsvExporter
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output CSV file path
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: ShapeInheritanceCsvExporter <input.vsdx> <output.csv>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the Visio diagram using Aspose.Diagram
        Diagram diagram = new Diagram(inputPath);

        // Prepare a StringBuilder for CSV content
        StringBuilder csvBuilder = new StringBuilder();

        // Write CSV header
        csvBuilder.AppendLine("PageName,ShapeID,ShapeNameU,MasterName,InheritPropsCount,HasInheritFill,HasInheritLine,HasInheritTextBlock");

        // Iterate through all pages
        foreach (Page page in diagram.Pages)
        {
            // Iterate through all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Basic shape information
                string pageName = page.NameU ?? string.Empty;
                string shapeId = shape.ID.ToString();
                string shapeNameU = shape.NameU ?? string.Empty;

                // Master information (if the shape is based on a master)
                string masterName = shape.Master != null ? shape.Master.NameU ?? string.Empty : string.Empty;

                // Inherited properties count
                int inheritPropsCount = shape.InheritProps != null ? shape.InheritProps.Count : 0;

                // Flags for other inheritance collections
                bool hasInheritFill = shape.InheritFill != null;
                bool hasInheritLine = shape.InheritLine != null;
                bool hasInheritTextBlock = shape.InheritTextBlock != null;

                // Build CSV line (escape commas if needed)
                string line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                    EscapeCsv(pageName),
                    EscapeCsv(shapeId),
                    EscapeCsv(shapeNameU),
                    EscapeCsv(masterName),
                    inheritPropsCount,
                    hasInheritFill,
                    hasInheritLine,
                    hasInheritTextBlock);

                csvBuilder.AppendLine(line);
            }
        }

        // Write CSV content to the output file
        File.WriteAllText(outputPath, csvBuilder.ToString(), Encoding.UTF8);
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
