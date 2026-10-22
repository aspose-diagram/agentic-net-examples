using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputHtmlFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare an HTML report using StringBuilder.
            StringBuilder html = new StringBuilder();

            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html>");
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
            html.AppendLine("<tr><th>Page</th><th>Shape ID</th><th>OLE Type / Source</th><th>Size (bytes)</th><th>Source File Path</th></tr>");

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Use page name if available; fallback to page ID.
                string pageName = page.NameU ?? $"Page_{page.ID}";

                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Check whether the shape contains an embedded OLE object.
                    if (shape.ForeignData != null && shape.ForeignData.ForeignType == ForeignType.Object)
                    {
                        // OLE source information.
                        string oleSource = shape.ForeignData.ObjectSourceFullName ?? "Unknown";
                        // Size of the embedded OLE binary data.
                        long oleSize = shape.ForeignData.ObjectData != null ? shape.ForeignData.ObjectData.Length : 0;

                        // Append a row to the HTML table.
                        html.AppendLine("<tr>");
                        html.AppendLine($"<td>{System.Web.HttpUtility.HtmlEncode(pageName)}</td>");
                        html.AppendLine($"<td>{shape.ID}</td>");
                        html.AppendLine($"<td>{System.Web.HttpUtility.HtmlEncode(oleSource)}</td>");
                        html.AppendLine($"<td>{oleSize}</td>");
                        html.AppendLine($"<td>{System.Web.HttpUtility.HtmlEncode(oleSource)}</td>");
                        html.AppendLine("</tr>");
                    }
                }
            }

            html.AppendLine("</table>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            // Write the HTML report to the specified output file.
            File.WriteAllText(outputPath, html.ToString(), Encoding.UTF8);

            Console.WriteLine($"OLE objects report generated successfully at \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}