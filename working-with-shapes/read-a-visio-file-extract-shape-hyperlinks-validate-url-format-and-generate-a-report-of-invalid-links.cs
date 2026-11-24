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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputReportFile]");
            return;
        }

        string inputPath = args[0];
        string reportPath = args.Length >= 2 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "InvalidLinksReport.txt");

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Prepare a list to hold details of invalid hyperlinks.
        List<string> invalidLinks = new List<string>();

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Aspose.Diagram.Page page = diagram.Pages[pageIndex];

                // Iterate through all shapes on the current page.
                for (int shapeIdx = 0; shapeIdx < page.Shapes.Count; shapeIdx++)
                {
                    Shape shape = page.Shapes[shapeIdx];

                    // Ensure the shape has a hyperlink collection.
                    if (shape.Hyperlinks == null)
                        continue;

                    // Iterate through each hyperlink of the shape.
                    foreach (Hyperlink link in shape.Hyperlinks)
                    {
                        // Retrieve the address; guard against nulls.
                        string address = link.Address?.Value ?? string.Empty;

                        // Validate the URL format.
                        bool isValid = Uri.TryCreate(address, UriKind.Absolute, out Uri uriResult) &&
                                       (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                        if (!isValid && !string.IsNullOrWhiteSpace(address))
                        {
                            // Record details of the invalid hyperlink.
                            string entry = $"Page {pageIndex + 1}, Shape ID {shape.ID}, Shape Name \"{shape.NameU}\", Invalid URL: {address}";
                            invalidLinks.Add(entry);
                        }
                    }
                }
            }

            // Write the report to the console.
            Console.WriteLine("Invalid Hyperlink Report:");
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
            }

            // Write the report to the output file.
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
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}