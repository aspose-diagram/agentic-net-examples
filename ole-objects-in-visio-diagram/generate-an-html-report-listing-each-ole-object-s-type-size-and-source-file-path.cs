using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Path to the Visio file to be processed
        string diagramPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(diagramPath)) { Console.Error.WriteLine($"File not found: {diagramPath}"); return; }

        // Path where the HTML report will be saved
        string reportPath = "OleReport.html";

        // Load the Visio diagram inside a try/catch to handle loading errors
        Diagram diagram;
        try
        {
            diagram = new Diagram(diagramPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Prepare HTML content
        StringBuilder html = new StringBuilder();
        html.AppendLine("<!DOCTYPE html>");
        html.AppendLine("<html lang=\"en\">");
        html.AppendLine("<head>");
        html.AppendLine("    <meta charset=\"UTF-8\">");
        html.AppendLine("    <title>OLE Objects Report</title>");
        html.AppendLine("    <style>");
        html.AppendLine("        table { border-collapse: collapse; width: 100%; }");
        html.AppendLine("        th, td { border: 1px solid #ddd; padding: 8px; }");
        html.AppendLine("        th { background-color: #f2f2f2; }");
        html.AppendLine("    </style>");
        html.AppendLine("</head>");
        html.AppendLine("<body>");
        html.AppendLine("    <h1>OLE Objects Report</h1>");
        html.AppendLine("    <table>");
        html.AppendLine("        <tr><th>Page Index</th><th>Shape ID</th><th>OLE Type</th><th>Width (inches)</th><th>Height (inches)</th><th>Source Path</th></tr>");

        // Counter to represent the page index (since Page.Index does not exist)
        int pageIndex = 0;

        // Iterate through all pages and shapes
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Identify OLE (foreign) objects
                if (shape.Type == TypeValue.Foreign &&
                    shape.ForeignData != null &&
                    shape.ForeignData.ForeignType == ForeignType.Object &&
                    shape.ForeignData.ObjectData != null)
                {
                    // OLE type (source full name, may contain file extension or application name)
                    string oleType = shape.ForeignData.ObjectSourceFullName ?? "Unknown";

                    // Size of the OLE object (width/height are in inches)
                    double width = shape.ForeignData.ObjectWidth;
                    double height = shape.ForeignData.ObjectHeight;

                    // Source file path (if available)
                    string sourcePath = shape.ForeignData.ObjectSourceFullName ?? "N/A";

                    // Append a row to the HTML table
                    html.AppendLine($"        <tr><td>{pageIndex}</td><td>{shape.ID}</td><td>{oleType}</td><td>{width:F2}</td><td>{height:F2}</td><td>{sourcePath}</td></tr>");
                }
            }
            pageIndex++; // Increment after processing each page
        }

        html.AppendLine("    </table>");
        html.AppendLine("</body>");
        html.AppendLine("</html>");

        // Write the HTML report to file inside a try/catch to capture I/O errors
        try
        {
            File.WriteAllText(reportPath, html.ToString());
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write report: {ex.Message}");
            diagram.Dispose();
            return;
        }

        // Clean up diagram resources
        diagram.Dispose();

        Console.WriteLine($"OLE report generated at: {Path.GetFullPath(reportPath)}");
    }
}