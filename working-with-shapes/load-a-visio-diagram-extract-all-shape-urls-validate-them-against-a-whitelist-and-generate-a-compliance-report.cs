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

                // Input Visio file path (modify as needed or pass as first argument)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Output compliance report file
                string reportPath = "ComplianceReport.txt";

                // Define whitelist of allowed URLs
                HashSet<string> whitelist = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    "https://trusted.com",
                    "http://example.org"
                    // Add more allowed URLs here
                };

                // Open the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    using (StreamWriter writer = new StreamWriter(reportPath, false))
                    {
                        writer.WriteLine("Visio Hyperlink Compliance Report");
                        writer.WriteLine($"Generated on: {DateTime.Now}");
                        writer.WriteLine(new string('=', 50));
                        writer.WriteLine();

                        // Iterate through all pages
                        foreach (Page page in diagram.Pages)
                        {
                            // Iterate through all shapes on the page
                            foreach (Shape shape in page.Shapes)
                            {
                                // Ensure the shape has hyperlinks
                                if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                                {
                                    // Iterate through each hyperlink
                                    foreach (Hyperlink link in shape.Hyperlinks)
                                    {
                                        string url = link.Address.Value ?? string.Empty;
                                        bool isAllowed = whitelist.Contains(url);

                                        // Prepare report line
                                        string status = isAllowed ? "ALLOWED" : "BLOCKED";
                                        string reportLine = $"Page: {page.Name} | Shape ID: {shape.ID} | URL: {url} | Status: {status}";

                                        // Write to console and report file
                                        Console.WriteLine(reportLine);
                                        writer.WriteLine(reportLine);
                                    }
                                }
                            }
                        }

                        writer.WriteLine();
                        writer.WriteLine("Report generation completed.");
                    }
                }

                Console.WriteLine($"Compliance report saved to: {Path.GetFullPath(reportPath)}");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }