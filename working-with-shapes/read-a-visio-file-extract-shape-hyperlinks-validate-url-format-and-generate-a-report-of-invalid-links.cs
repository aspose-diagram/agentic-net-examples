using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string visioPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        // List to hold details of invalid hyperlinks.
        var invalidLinks = new List<string>();

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Iterate through all pages in the diagram.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];
                string pageName = page.NameU ?? $"Page_{pageIndex + 1}";

                // Iterate through all shapes on the current page.
                for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                {
                    Shape shape = page.Shapes[shapeIndex];
                    if (shape == null) continue;

                    string shapeName = shape.NameU ?? $"Shape_{shape.ID}";

                    // Ensure the Hyperlinks collection is not null.
                    if (shape.Hyperlinks == null) continue;

                    // Iterate through each hyperlink attached to the shape.
                    foreach (Hyperlink hyperlink in shape.Hyperlinks)
                    {
                        if (hyperlink == null) continue;

                        // Retrieve the hyperlink address.
                        string address = hyperlink.Address?.Value ?? string.Empty;

                        // Validate the URL format.
                        bool isValid = Uri.TryCreate(address, UriKind.Absolute, out Uri uriResult) &&
                                       (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                        if (!isValid && !string.IsNullOrWhiteSpace(address))
                        {
                            // Record details of the invalid hyperlink.
                            string reportLine = $"Page: {pageName}, Shape: {shapeName}, Hyperlink Name: {hyperlink.Name}, URL: {address}";
                            invalidLinks.Add(reportLine);
                        }
                    }
                }
            }

            // Output the report of invalid hyperlinks.
            if (invalidLinks.Count > 0)
            {
                Console.WriteLine("Invalid Hyperlinks Report:");
                foreach (string line in invalidLinks)
                {
                    Console.WriteLine(line);
                }

                // Optionally write the report to a text file in the same directory as the input file.
                string reportPath = Path.Combine(Path.GetDirectoryName(visioPath) ?? "", "InvalidHyperlinksReport.txt");
                try
                {
                    File.WriteAllLines(reportPath, invalidLinks);
                    Console.WriteLine($"Report saved to: {reportPath}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to write report file: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("No invalid hyperlinks found.");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}