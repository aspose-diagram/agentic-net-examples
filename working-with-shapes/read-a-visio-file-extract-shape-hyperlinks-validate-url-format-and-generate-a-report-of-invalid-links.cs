using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Input Visio file path (modify as needed or pass as first argument)
            string visioPath = args.Length > 0 ? args[0] : "input.vsdx";

            if (!File.Exists(visioPath))
            {
                Console.WriteLine($"File not found: {visioPath}");
                return;
            }

            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Prepare a list to collect invalid hyperlink information
            List<string> invalidLinks = new List<string>();

            // Regular expression for basic URL validation
            Regex urlRegex = new Regex(@"^(https?|ftp)://[^\s/$.?#].[^\s]*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the Hyperlinks collection exists
                    if (shape.Hyperlinks == null)
                        continue;

                    // Iterate through each hyperlink in the shape
                    foreach (Hyperlink link in shape.Hyperlinks)
                    {
                        // Retrieve the address value
                        string address = link.Address.Value;

                        // Validate the URL format
                        if (string.IsNullOrWhiteSpace(address) || !urlRegex.IsMatch(address))
                        {
                            // Record details of the invalid link
                            string info = $"Page: {page.NameU}, Shape ID: {shape.ID}, Shape Name: {shape.NameU}, Hyperlink Address: {address}";
                            invalidLinks.Add(info);
                        }
                    }
                }
            }

            // Output the report
            Console.WriteLine("Invalid Hyperlinks Report:");
            if (invalidLinks.Count == 0)
            {
                Console.WriteLine("No invalid hyperlinks found.");
            }
            else
            {
                foreach (string line in invalidLinks)
                {
                    Console.WriteLine(line);
                }

                // Optionally write the report to a text file
                string reportPath = "InvalidHyperlinksReport.txt";
                try
                {
                    File.WriteAllLines(reportPath, invalidLinks);
                    Console.WriteLine($"Report saved to: {Path.GetFullPath(reportPath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to write report file: {ex.Message}");
                }
            }
        }
    }