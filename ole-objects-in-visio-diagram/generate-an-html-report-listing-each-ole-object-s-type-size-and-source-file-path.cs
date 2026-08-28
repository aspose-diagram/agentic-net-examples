using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (change as needed)
                string inputPath = "input.vsdx";
                // Output HTML report path
                string outputPath = "OleReport.html";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Build HTML content
                StringBuilder html = new StringBuilder();
                html.AppendLine("<!DOCTYPE html>");
                html.AppendLine("<html lang=\"en\">");
                html.AppendLine("<head>");
                html.AppendLine("<meta charset=\"UTF-8\">");
                html.AppendLine("<title>OLE Objects Report</title>");
                html.AppendLine("<style>");
                html.AppendLine("table { border-collapse: collapse; width: 100%; }");
                html.AppendLine("th, td { border: 1px solid #ddd; padding: 8px; }");
                html.AppendLine("th { background-color: #f2f2f2; }");
                html.AppendLine("</style>");
                html.AppendLine("</head>");
                html.AppendLine("<body>");
                html.AppendLine("<h1>OLE Objects Report</h1>");
                html.AppendLine("<table>");
                html.AppendLine("<tr><th>Page</th><th>Shape ID</th><th>OLE Type</th><th>Width (inches)</th><th>Height (inches)</th><th>Source Path</th></tr>");

                // Iterate through pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape is a foreign OLE object
                        if (shape.Type == TypeValue.Foreign &&
                            shape.ForeignData != null &&
                            shape.ForeignData.ObjectType == ObjectType.EmbeddedObject)
                        {
                            // Verify ObjectData exists
                            if (shape.ForeignData.ObjectData == null || shape.ForeignData.ObjectData.Length == 0)
                                continue;

                            // Retrieve size (if available)
                            double width = shape.ForeignData.ObjectWidth;
                            double height = shape.ForeignData.ObjectHeight;

                            // Retrieve source file path (may be empty)
                            string sourcePath = shape.ForeignData.ObjectSourceFullName ?? string.Empty;

                            // Append row to HTML table
                            html.AppendLine("<tr>");
                            html.AppendLine($"<td>{page.Name}</td>");
                            html.AppendLine($"<td>{shape.ID}</td>");
                            html.AppendLine("<td>OLE Object</td>");
                            html.AppendLine($"<td>{width:F2}</td>");
                            html.AppendLine($"<td>{height:F2}</td>");
                            html.AppendLine($"<td>{System.Web.HttpUtility.HtmlEncode(sourcePath)}</td>");
                            html.AppendLine("</tr>");
                        }
                    }
                }

                html.AppendLine("</table>");
                html.AppendLine("</body>");
                html.AppendLine("</html>");

                // Write the HTML report to file
                File.WriteAllText(outputPath, html.ToString());

                Console.WriteLine($"OLE report generated: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }