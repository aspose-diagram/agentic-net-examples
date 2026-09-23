using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be processed
            string inputPath = "input.vsdx";

            // Path where the CSV report will be saved
            string csvPath = "connectors_report.csv";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a CSV file and write the header
            using (StreamWriter writer = new StreamWriter(csvPath))
            {
                writer.WriteLine("PageName,ConnectorID,FromShapeID,FromShapeName,ToShapeID,ToShapeName");

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify connector shapes (1‑D shapes)
                        if (shape.OneD)
                        {
                            long connectorId = shape.ID;

                            // Retrieve IDs of shapes connected to this connector
                            long[] connectedIds = shape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);

                            // A valid connector should have exactly two connected shapes
                            if (connectedIds.Length >= 2)
                            {
                                Shape fromShape = page.Shapes.GetShape(connectedIds[0]);
                                Shape toShape   = page.Shapes.GetShape(connectedIds[1]);

                                // Write a CSV line with escaped values
                                writer.WriteLine(
                                    $"{Escape(page.Name)},{connectorId},{fromShape.ID},{Escape(fromShape.Name)},{toShape.ID},{Escape(toShape.Name)}");
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Connector report saved to: {csvPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to escape CSV fields containing commas, quotes, or line breaks
    static string Escape(string value)
    {
        if (value == null) return string.Empty;
        if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
        {
            value = value.Replace("\"", "\"\"");
            return $"\"{value}\"";
        }
        return value;
    }
}
