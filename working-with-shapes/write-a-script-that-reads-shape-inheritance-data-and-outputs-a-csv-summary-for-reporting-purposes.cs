using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class ShapeInheritanceCsvExporter
{
    // Entry point of the application
    static void Main(string[] args)
    {
        // Validate arguments: first is input Visio file, second is output CSV file
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: ShapeInheritanceCsvExporter <input.vsdx> <output.csv>");
            return;
        }

        string visioFilePath = args[0];
        string csvFilePath = args[1];

        // Load the Visio diagram (lifecycle load rule)
        Diagram diagram = new Diagram(visioFilePath);

        // Prepare a StringBuilder for CSV content
        StringBuilder csvBuilder = new StringBuilder();

        // Write CSV header
        csvBuilder.AppendLine("PageName,ShapeID,ShapeName,MasterName,InheritPropsCount,InheritFillExists,InheritLineExists,InheritGeomsCount");

        // Iterate through each page in the diagram
        foreach (Page page in diagram.Pages)
        {
            // Iterate through each shape on the page
            foreach (Shape shape in page.Shapes)
            {
                // Gather inheritance information
                string masterName = shape.Master != null ? shape.Master.NameU : string.Empty;
                int inheritPropsCount = shape.InheritProps != null ? shape.InheritProps.Count : 0;
                bool inheritFillExists = shape.InheritFill != null;
                bool inheritLineExists = shape.InheritLine != null;
                int inheritGeomsCount = shape.InheritGeoms != null ? shape.InheritGeoms.Count : 0;

                // Build CSV line
                string csvLine = string.Format(
                    "\"{0}\",{1},\"{2}\",\"{3}\",{4},{5},{6},{7}",
                    page.NameU,
                    shape.ID,
                    shape.NameU,
                    masterName,
                    inheritPropsCount,
                    inheritFillExists,
                    inheritLineExists,
                    inheritGeomsCount
                );

                csvBuilder.AppendLine(csvLine);
            }
        }

        // Write the CSV content to the specified file (lifecycle save rule)
        File.WriteAllText(csvFilePath, csvBuilder.ToString(), Encoding.UTF8);

        Console.WriteLine($"CSV summary has been written to: {csvFilePath}");
    }
}
