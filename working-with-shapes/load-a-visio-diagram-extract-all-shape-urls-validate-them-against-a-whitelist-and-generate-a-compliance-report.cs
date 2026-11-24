using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string visioPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found – {visioPath}");
            return;
        }

        // Define a simple whitelist of allowed URL prefixes.
        // In a real scenario this could be read from a configuration file.
        string[] whitelist = new string[]
        {
            "https://trusted.com",
            "http://internal.local"
        };

        // Prepare a list to hold report lines.
        List<string> reportLines = new List<string>();
        reportLines.Add("Shape URL Compliance Report");
        reportLines.Add($"Source Diagram: {visioPath}");
        reportLines.Add($"Generated: {DateTime.Now}");
        reportLines.Add(string.Empty);
        reportLines.Add("PageName,ShapeID,URL,Compliant");

        try
        {
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a Hyperlinks collection.
                        if (shape.Hyperlinks == null)
                            continue;

                        // Iterate through each hyperlink attached to the shape.
                        foreach (Hyperlink link in shape.Hyperlinks)
                        {
                            // Read the hyperlink address (URL).
                            string url = link.Address.Value;

                            // Determine compliance by checking the whitelist.
                            bool isCompliant = false;
                            foreach (string allowedPrefix in whitelist)
                            {
                                if (!string.IsNullOrEmpty(url) &&
                                    url.StartsWith(allowedPrefix, StringComparison.OrdinalIgnoreCase))
                                {
                                    isCompliant = true;
                                    break;
                                }
                            }

                            // Record the result.
                            string line = $"{page.Name},{shape.ID},{url},{(isCompliant ? "Yes" : "No")}";
                            reportLines.Add(line);
                        }
                    }
                }
            }

            // Write the compliance report to a text file beside the source diagram.
            string reportPath = Path.Combine(Path.GetDirectoryName(visioPath) ?? string.Empty, "ComplianceReport.txt");
            File.WriteAllLines(reportPath, reportLines);
            Console.WriteLine($"Compliance report generated: {reportPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}