using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputHtmlPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Ensure output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
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
                htmlBuilder.AppendLine("<tr><th>OLE Type / Source</th><th>Size (bytes)</th><th>Source File Path</th></tr>");

                // Iterate through pages and shapes to find OLE objects
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // OLE objects are represented by ForeignData with non‑null ObjectData
                        if (shape.ForeignData != null && shape.ForeignData.ObjectData != null && shape.ForeignData.ObjectData.Length > 0)
                        {
                            // Retrieve OLE type information (may be a file extension or application name)
                            string oleType = shape.ForeignData.ObjectSourceFullName ?? "Unknown";

                            // Size of the embedded OLE binary data
                            int size = shape.ForeignData.ObjectData.Length;

                            // Source file path or identifier (same property as type if available)
                            string sourcePath = shape.ForeignData.ObjectSourceFullName ?? "N/A";

                            // Append a row to the HTML table
                            htmlBuilder.AppendLine("<tr>");
                            htmlBuilder.AppendLine($"<td>{System.Net.WebUtility.HtmlEncode(oleType)}</td>");
                            htmlBuilder.AppendLine($"<td>{size}</td>");
                            htmlBuilder.AppendLine($"<td>{System.Net.WebUtility.HtmlEncode(sourcePath)}</td>");
                            htmlBuilder.AppendLine("</tr>");
                        }
                    }
                }

                htmlBuilder.AppendLine("</table>");
                htmlBuilder.AppendLine("</body>");
                htmlBuilder.AppendLine("</html>");

                // Write the HTML report to the specified output file
                File.WriteAllText(outputPath, htmlBuilder.ToString(), Encoding.UTF8);
                Console.WriteLine($"HTML report generated successfully at: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}