using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (first argument or default)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Output compliance report file
                string reportPath = "ComplianceReport.txt";

                // Define whitelist of allowed URLs
                HashSet<string> whitelist = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    "https://example.com",
                    "http://allowed.com"
                    // Add more allowed URLs as needed
                };

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Collect report lines
                List<string> reportLines = new List<string>();
                reportLines.Add("Visio Hyperlink Compliance Report");
                reportLines.Add($"Generated on: {DateTime.Now}");
                reportLines.Add("----------------------------------------------------");

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the Hyperlinks collection exists
                        if (shape.Hyperlinks != null)
                        {
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                // Retrieve the URL from the hyperlink
                                string url = link.Address.Value ?? string.Empty;

                                // Validate against whitelist
                                bool isAllowed = whitelist.Contains(url);
                                string status = isAllowed ? "Allowed" : "Blocked";

                                // Build report entry
                                string line = $"Page: {page.Name}, Shape ID: {shape.ID}, Shape NameU: {shape.NameU}, URL: {url}, Status: {status}";
                                reportLines.Add(line);
                            }
                        }
                    }
                }

                // Write the report to a text file
                File.WriteAllLines(reportPath, reportLines);

                // Output summary to console
                Console.WriteLine($"Compliance report generated: {reportPath}");
                Console.WriteLine($"Total entries: {reportLines.Count - 3}");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }