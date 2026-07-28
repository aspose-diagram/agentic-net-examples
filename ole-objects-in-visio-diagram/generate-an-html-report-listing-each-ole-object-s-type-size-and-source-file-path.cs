using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Path to the Visio file to be processed
            string visioFilePath = "input.vsdx";

            // Path where the HTML report will be saved
            string reportPath = "OleObjectsReport.html";

            // Ensure the input file exists
            if (!File.Exists(visioFilePath))
            {
                throw new FileNotFoundException($"Visio file not found: {visioFilePath}");
            }

            // Load the diagram
            using (Diagram diagram = new Diagram(visioFilePath))
            {
                // Prepare HTML content
                StringBuilder htmlBuilder = new StringBuilder();

                htmlBuilder.AppendLine("<!DOCTYPE html>");
                htmlBuilder.AppendLine("<html lang=\"en\">");
                htmlBuilder.AppendLine("<head>");
                htmlBuilder.AppendLine("<meta charset=\"UTF-8\">");
                htmlBuilder.AppendLine("<title>OLE Objects Report</title>");
                htmlBuilder.AppendLine("<style>");
                htmlBuilder.AppendLine("table { border-collapse: collapse; width: 100%; }");
                htmlBuilder.AppendLine("th, td { border: 1px solid #ddd; padding: 8px; }");
                htmlBuilder.AppendLine("th { background-color: #f2f2f2; }");
                htmlBuilder.AppendLine("</style>");
                htmlBuilder.AppendLine("</head>");
                htmlBuilder.AppendLine("<body>");
                htmlBuilder.AppendLine("<h1>OLE Objects Report</h1>");
                htmlBuilder.AppendLine("<table>");
                htmlBuilder.AppendLine("<tr><th>Page Name</th><th>Shape ID</th><th>OLE Type</th><th>Width (inches)</th><th>Height (inches)</th><th>Source Path</th></tr>");

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Verify the shape is an OLE object
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.ObjectType == ObjectType.EmbeddedObject)
                        {
                            // Ensure ObjectData is present (optional for this report)
                            if (shape.ForeignData.ObjectData == null || shape.ForeignData.ObjectData.Length == 0)
                            {
                                continue; // Skip if no binary data
                            }

                            // Retrieve OLE details
                            string oleType = shape.ForeignData.ObjectSourceFullName ?? "Unknown";
                            double width = shape.ForeignData.ObjectWidth;
                            double height = shape.ForeignData.ObjectHeight;

                            // Build a table row
                            htmlBuilder.AppendLine("<tr>");
                            htmlBuilder.AppendLine($"<td>{page.Name}</td>");
                            htmlBuilder.AppendLine($"<td>{shape.ID}</td>");
                            htmlBuilder.AppendLine($"<td>{oleType}</td>");
                            htmlBuilder.AppendLine($"<td>{width:F2}</td>");
                            htmlBuilder.AppendLine($"<td>{height:F2}</td>");
                            htmlBuilder.AppendLine($"<td>{oleType}</td>");
                            htmlBuilder.AppendLine("</tr>");
                        }
                    }
                }

                htmlBuilder.AppendLine("</table>");
                htmlBuilder.AppendLine("</body>");
                htmlBuilder.AppendLine("</html>");

                // Write the HTML report to file
                File.WriteAllText(reportPath, htmlBuilder.ToString(), Encoding.UTF8);
            }

            Console.WriteLine($"OLE objects report generated at: {Path.GetFullPath(reportPath)}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
