using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output report file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioComplianceChecker <inputVisioPath> <outputReportPath>");
                return;
            }

            string inputPath = args[0];
            string reportPath = args[1];

            // Define whitelist of allowed URLs
            HashSet<string> whitelist = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "https://example.com",
                "http://allowed.com"
                // Add more allowed URLs as needed
            };

            // Counters for reporting
            int totalPages = 0;
            int totalShapes = 0;
            int totalHyperlinks = 0;
            int compliantCount = 0;
            int nonCompliantCount = 0;

            // List to store details of non‑compliant hyperlinks
            List<string> nonCompliantDetails = new List<string>();

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page
                foreach (Page page in diagram.Pages)
                {
                    totalPages++;

                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        totalShapes++;

                        // Ensure the Hyperlinks collection is not null
                        if (shape.Hyperlinks != null)
                        {
                            // Iterate through each hyperlink of the shape
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                totalHyperlinks++;

                                // Retrieve the address value (URL)
                                string address = link.Address?.Value ?? string.Empty;

                                // Validate against whitelist
                                if (whitelist.Contains(address))
                                {
                                    compliantCount++;
                                }
                                else
                                {
                                    nonCompliantCount++;
                                    string detail = $"Page: {page.NameU}, Shape ID: {shape.ID}, URL: {address}";
                                    nonCompliantDetails.Add(detail);
                                }
                            }
                        }
                    }
                }
            }

            // Build the compliance report
            using (StreamWriter writer = new StreamWriter(reportPath, false))
            {
                writer.WriteLine("Visio Hyperlink Compliance Report");
                writer.WriteLine("================================");
                writer.WriteLine($"Input Diagram: {inputPath}");
                writer.WriteLine($"Generated On : {DateTime.Now}");
                writer.WriteLine();
                writer.WriteLine($"Total Pages          : {totalPages}");
                writer.WriteLine($"Total Shapes         : {totalShapes}");
                writer.WriteLine($"Total Hyperlinks     : {totalHyperlinks}");
                writer.WriteLine($"Compliant Hyperlinks : {compliantCount}");
                writer.WriteLine($"Non‑Compliant Hyperlinks : {nonCompliantCount}");
                writer.WriteLine();

                if (nonCompliantDetails.Count > 0)
                {
                    writer.WriteLine("Non‑Compliant Hyperlink Details:");
                    writer.WriteLine("---------------------------------");
                    foreach (string detail in nonCompliantDetails)
                    {
                        writer.WriteLine(detail);
                    }
                }
                else
                {
                    writer.WriteLine("All hyperlinks are compliant with the whitelist.");
                }
            }

            // Output summary to console
            Console.WriteLine("Visio Hyperlink Compliance Report generated:");
            Console.WriteLine($"Report Path: {reportPath}");
            Console.WriteLine($"Total Pages          : {totalPages}");
            Console.WriteLine($"Total Shapes         : {totalShapes}");
            Console.WriteLine($"Total Hyperlinks     : {totalHyperlinks}");
            Console.WriteLine($"Compliant Hyperlinks : {compliantCount}");
            Console.WriteLine($"Non‑Compliant Hyperlinks : {nonCompliantCount}");
        }
    }