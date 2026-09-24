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

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output compliance report file path
                string reportPath = "ComplianceReport.txt";

                // Define whitelist of allowed URLs (exact matches)
                HashSet<string> whitelist = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    "https://trusted.com",
                    "http://example.org"
                };

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Prepare the report writer
                using (StreamWriter writer = new StreamWriter(reportPath, false))
                {
                    writer.WriteLine("Visio Hyperlink Compliance Report");
                    writer.WriteLine($"Generated on: {DateTime.Now}");
                    writer.WriteLine(new string('=', 50));

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes without hyperlinks collection
                            if (shape.Hyperlinks == null)
                                continue;

                            // Iterate through each hyperlink of the shape
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                // Retrieve the URL address
                                string url = link.Address.Value ?? string.Empty;

                                // Determine compliance
                                bool isValid = whitelist.Contains(url);

                                // Build report line
                                string status = isValid ? "VALID" : "INVALID";
                                string line = $"Page: {page.NameU}, Shape ID: {shape.ID}, URL: {url} - {status}";

                                // Write to console and report file
                                Console.WriteLine(line);
                                writer.WriteLine(line);
                            }
                        }
                    }

                    writer.WriteLine(new string('=', 50));
                    writer.WriteLine("Report generation completed.");
                }

                Console.WriteLine($"Compliance report saved to: {Path.GetFullPath(reportPath)}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }