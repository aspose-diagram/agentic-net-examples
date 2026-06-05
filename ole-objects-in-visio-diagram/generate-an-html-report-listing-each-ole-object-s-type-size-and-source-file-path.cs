using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class OLEReportGenerator
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Build HTML content
            StringBuilder html = new StringBuilder();
            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html><head><meta charset=\"utf-8\"><title>OLE Objects Report</title></head><body>");
            html.AppendLine("<h1>OLE Objects Report</h1>");
            html.AppendLine("<table border=\"1\" cellpadding=\"5\" cellspacing=\"0\">");
            html.AppendLine("<tr><th>Page</th><th>Shape ID</th><th>OLE Type</th><th>Width (page units)</th><th>Height (page units)</th><th>Source File</th></tr>");

            // Iterate through pages and shapes to find OLE objects
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Shapes that contain OLE data have a non‑null ForeignData object
                    if (shape.ForeignData != null)
                    {
                        ForeignData fd = shape.ForeignData;

                        string oleType = fd.ObjectType.ToString();
                        string width = fd.ObjectWidth.ToString();
                        string height = fd.ObjectHeight.ToString();
                        string source = fd.ObjectSourceFullName ?? "N/A";

                        html.AppendLine($"<tr><td>{page.ID}</td><td>{shape.ID}</td><td>{oleType}</td><td>{width}</td><td>{height}</td><td>{source}</td></tr>");
                    }
                }
            }

            html.AppendLine("</table></body></html>");

            // Save the HTML report
            File.WriteAllText("OleObjectsReport.html", html.ToString());

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
