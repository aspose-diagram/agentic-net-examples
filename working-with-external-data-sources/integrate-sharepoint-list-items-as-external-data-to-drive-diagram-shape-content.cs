using System;
using System.IO;
using System.Data;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Simulate SharePoint list items using an in‑memory DataTable.
                // In a real scenario this data would be retrieved via SharePoint APIs.
                // ------------------------------------------------------------
                DataTable sharePointData = new DataTable();
                sharePointData.Columns.Add("Title", typeof(string));
                sharePointData.Columns.Add("Description", typeof(string));

                // Sample rows – replace with actual SharePoint data retrieval logic.
                sharePointData.Rows.Add("Shape A", "Description for Shape A");
                sharePointData.Rows.Add("Shape B", "Description for Shape B");
                sharePointData.Rows.Add("Shape C", "Description for Shape C");

                // ------------------------------------------------------------
                // Iterate through all pages and shapes, updating shape content
                // based on matching SharePoint list items.
                // ------------------------------------------------------------
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Only process shapes that have a master (i.e., visible shapes).
                        if (shape.Master == null)
                            continue;

                        // Retrieve the current shape text (if any).
                        string shapeText = shape.Text?.Value?.Text ?? string.Empty;

                        // Attempt to find a matching SharePoint row where Title equals the shape text.
                        foreach (DataRow row in sharePointData.Rows)
                        {
                            string title = row["Title"]?.ToString() ?? string.Empty;
                            if (string.Equals(title, shapeText, StringComparison.OrdinalIgnoreCase))
                            {
                                // Update the shape's text to include the description.
                                string description = row["Description"]?.ToString() ?? string.Empty;

                                // Clear existing text and add new text runs.
                                shape.Text.Value.Clear();
                                shape.Text.Value.Add(new Txt($"{title}\n{description}"));

                                // Optionally store the description in a custom data field.
                                shape.Data1 = description;

                                // Refresh the shape to apply changes to geometry and connections.
                                shape.RefreshData();

                                // Break after the first match.
                                break;
                            }
                        }
                    }
                }

                // ------------------------------------------------------------
                // Save the modified diagram to the specified output path.
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}