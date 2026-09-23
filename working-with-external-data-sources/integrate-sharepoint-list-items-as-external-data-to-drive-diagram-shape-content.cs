using System.IO;
using System;
using System.Data;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class Program
{
    public static void Main()
    {
        try
        {

            // Path to the source Visio template (must exist in the execution folder)
            string sourcePath = "Template.vsdx";
            // Path for the generated diagram
            string outputPath = "Result.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(sourcePath);

            // ------------------------------------------------------------
            // Simulate SharePoint list items using an in‑memory DataTable.
            // In a real scenario you would retrieve the list via SharePoint
            // client APIs and fill the DataTable accordingly.
            // ------------------------------------------------------------
            DataTable sharePointData = new DataTable();
            sharePointData.Columns.Add("Title", typeof(string));
            sharePointData.Columns.Add("Description", typeof(string));

            // Sample rows – replace with actual SharePoint data retrieval.
            sharePointData.Rows.Add("Project Alpha", "Initial phase of the project.");
            sharePointData.Rows.Add("Project Beta", "Design and development stage.");
            sharePointData.Rows.Add("Project Gamma", "Testing and QA.");

            // ------------------------------------------------------------
            // Iterate through all pages and shapes. For each shape that
            // is a regular shape (not a connector) we will populate its
            // text with the corresponding SharePoint item.
            // ------------------------------------------------------------
            int dataIndex = 0; // tracks the current row in the DataTable

            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Skip connectors and deleted shapes
                    if (shape.OneD || shape.Del == BOOL.True)
                        continue;

                    // If we have exhausted the data rows, stop updating.
                    if (dataIndex >= sharePointData.Rows.Count)
                        break;

                    DataRow row = sharePointData.Rows[dataIndex];

                    // Build the text content for the shape.
                    string title = row["Title"]?.ToString() ?? string.Empty;
                    string description = row["Description"]?.ToString() ?? string.Empty;
                    string shapeText = $"{title}\n{description}";

                    // Clear any existing text and add the new content.
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt(shapeText));

                    dataIndex++;
                }

                // If all data rows have been assigned, exit the outer loop as well.
                if (dataIndex >= sharePointData.Rows.Count)
                    break;
            }

            // ------------------------------------------------------------
            // Save the updated diagram to the desired format.
            // ------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
