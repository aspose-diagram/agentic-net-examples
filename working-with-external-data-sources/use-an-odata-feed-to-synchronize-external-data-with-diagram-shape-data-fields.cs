using System;
using System.IO;
using System.Data;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // ------------------------------------------------------------
            // Simulate an OData feed using an in-memory DataTable.
            // In a real scenario, replace this block with actual OData
            // retrieval logic (e.g., HttpClient request and JSON parsing).
            // ------------------------------------------------------------
            DataTable odataTable = new DataTable();
            odataTable.Columns.Add("ShapeId", typeof(long));
            odataTable.Columns.Add("Status", typeof(string));

            // Sample data rows – map shape IDs to a status string.
            // Adjust or extend rows as needed for real data.
            DataRow row1 = odataTable.NewRow();
            row1["ShapeId"] = 1L;
            row1["Status"] = "Active";
            odataTable.Rows.Add(row1);

            DataRow row2 = odataTable.NewRow();
            row2["ShapeId"] = 2L;
            row2["Status"] = "Inactive";
            odataTable.Rows.Add(row2);
            // ------------------------------------------------------------

            // Iterate through all pages and shapes in the diagram.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                {
                    Shape shape = page.Shapes[shapeIndex];

                    // Find a matching OData row for the current shape ID.
                    string statusValue = null;
                    foreach (DataRow dataRow in odataTable.Rows)
                    {
                        long shapeIdFromData = (long)dataRow["ShapeId"];
                        if (shape.ID == shapeIdFromData)
                        {
                            statusValue = dataRow["Status"]?.ToString();
                            break;
                        }
                    }

                    // If there is no matching data, skip this shape.
                    if (statusValue == null)
                        continue;

                    // --------------------------------------------------------
                    // Update or add a custom property named "Status" on the shape.
                    // --------------------------------------------------------
                    bool propertyFound = false;
                    for (int propIndex = 0; propIndex < shape.Props.Count; propIndex++)
                    {
                        Prop prop = shape.Props[propIndex];
                        if (prop.Name == "Status")
                        {
                            // Update existing property value.
                            prop.Value.Val = statusValue;
                            propertyFound = true;
                            break;
                        }
                    }

                    if (!propertyFound)
                    {
                        // Create a new custom property.
                        Prop newProp = new Prop();
                        newProp.Name = "Status";
                        newProp.Label.Value = "Status";
                        newProp.Type.Value = TypePropValue.String;
                        newProp.Value.Val = statusValue;
                        shape.Props.Add(newProp);
                    }

                    // Refresh shape data to apply changes.
                    shape.RefreshData();
                }
            }

            // Save the updated diagram to the output file.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}