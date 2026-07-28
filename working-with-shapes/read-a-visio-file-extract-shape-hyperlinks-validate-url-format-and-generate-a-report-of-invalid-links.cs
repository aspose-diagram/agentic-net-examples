using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Get the Visio file path from command line or prompt the user
            string inputPath;
            if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            {
                inputPath = args[0];
            }
            else
            {
                Console.Write("Enter the path to the Visio file: ");
                inputPath = Console.ReadLine();
            }

            if (string.IsNullOrWhiteSpace(inputPath))
            {
                Console.WriteLine("No file path provided. Exiting.");
                return;
            }

            // Load the diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // List to hold details of invalid hyperlinks
            List<string> invalidLinks = new List<string>();

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the Hyperlinks collection exists
                    if (shape.Hyperlinks == null)
                        continue;

                    foreach (Hyperlink link in shape.Hyperlinks)
                    {
                        // Retrieve the address string; it may be null
                        string address = link.Address?.Value;

                        if (string.IsNullOrWhiteSpace(address))
                            continue; // Skip empty addresses

                        // Validate URL format (absolute HTTP/HTTPS)
                        bool isValid = Uri.IsWellFormedUriString(address, UriKind.Absolute) &&
                                       (address.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                                        address.StartsWith("https://", StringComparison.OrdinalIgnoreCase));

                        if (!isValid)
                        {
                            string reportLine = $"Page: {page.NameU}, Shape ID: {shape.ID}, Shape Name: {shape.NameU}, URL: {address}";
                            invalidLinks.Add(reportLine);
                        }
                    }
                }
            }

            // Output the report
            Console.WriteLine();
            if (invalidLinks.Count == 0)
            {
                Console.WriteLine("No invalid hyperlinks found in the diagram.");
            }
            else
            {
                Console.WriteLine("Invalid hyperlinks detected:");
                foreach (string line in invalidLinks)
                {
                    Console.WriteLine(line);
                }
                Console.WriteLine($"\nTotal invalid links: {invalidLinks.Count}");
            }
        }
    }