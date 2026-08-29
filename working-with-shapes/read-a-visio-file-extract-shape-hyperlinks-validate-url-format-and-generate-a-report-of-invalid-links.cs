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

                // Input Visio file path (first argument) or default.
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Output report file path (second argument) or default.
                string reportPath = args.Length > 1 ? args[1] : "InvalidHyperlinksReport.txt";

                // Load the Visio diagram.
                Diagram diagram = new Diagram(inputPath);

                // Collect information about invalid hyperlinks.
                List<string> invalidLinks = new List<string>();

                // Iterate through all pages and shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a Hyperlinks collection.
                        if (shape.Hyperlinks != null)
                        {
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                // Retrieve the address string; it may be null.
                                string address = link.Address?.Value;

                                // Validate the URL format.
                                if (!IsValidUrl(address))
                                {
                                    string info = $"Page: {page.NameU}, Shape: {shape.NameU}, Hyperlink: {address ?? "(null)"}";
                                    invalidLinks.Add(info);
                                }
                            }
                        }
                    }
                }

                // Output results.
                if (invalidLinks.Count == 0)
                {
                    Console.WriteLine("No invalid hyperlinks found.");
                }
                else
                {
                    Console.WriteLine($"Found {invalidLinks.Count} invalid hyperlink(s). Report written to: {reportPath}");
                    File.WriteAllLines(reportPath, invalidLinks);
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }

        // Helper method to validate absolute HTTP/HTTPS URLs.
        private static bool IsValidUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            if (Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult))
            {
                return uriResult.Scheme == Uri.UriSchemeHttp ||
                       uriResult.Scheme == Uri.UriSchemeHttps;
            }

            return false;
        }
    }