using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Input Visio file path (can be passed as first argument)
            string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Error: File not found - {inputPath}");
                return;
            }

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // List to collect information about invalid hyperlinks
            List<string> invalidLinks = new List<string>();

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Hyperlinks collection
                    if (shape.Hyperlinks == null)
                        continue;

                    foreach (Hyperlink link in shape.Hyperlinks)
                    {
                        // Retrieve the address value (cell-based property)
                        string url = link.Address.Value;

                        // Skip empty addresses
                        if (string.IsNullOrWhiteSpace(url))
                            continue;

                        // Validate URL format (absolute HTTP/HTTPS)
                        bool isValid = Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) &&
                                       (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                        if (!isValid)
                        {
                            string reportLine = $"Page: \"{page.Name}\" (ID {page.ID}), " +
                                                $"Shape ID {shape.ID}, " +
                                                $"Hyperlink \"{url}\" is invalid.";
                            invalidLinks.Add(reportLine);
                        }
                    }
                }
            }

            // Output report to console
            if (invalidLinks.Count == 0)
            {
                Console.WriteLine("No invalid hyperlinks found.");
            }
            else
            {
                Console.WriteLine("Invalid Hyperlinks Report:");
                foreach (string line in invalidLinks)
                {
                    Console.WriteLine(line);
                }

                // Write report to a text file
                string reportPath = "InvalidLinksReport.txt";
                try
                {
                    File.WriteAllLines(reportPath, invalidLinks);
                    Console.WriteLine($"Report saved to {reportPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to write report file: {ex.Message}");
                }
            }
        }
    }