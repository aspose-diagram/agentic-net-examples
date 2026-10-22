using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <WhitelistFilePath>");
            return;
        }

        string visioPath = args[0];
        string whitelistPath = args[1];

        // Guard: ensure the Visio file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Visio file not found: {visioPath}");
            return;
        }

        // Guard: ensure the whitelist file exists.
        if (!File.Exists(whitelistPath))
        {
            Console.Error.WriteLine($"Whitelist file not found: {whitelistPath}");
            return;
        }

        // Load whitelist URLs (one per line, ignore empty lines and whitespace).
        List<string> whitelist = new List<string>();
        try
        {
            foreach (string line in File.ReadAllLines(whitelistPath))
            {
                string trimmed = line.Trim();
                if (!string.IsNullOrEmpty(trimmed))
                {
                    whitelist.Add(trimmed);
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading whitelist file: {ex.Message}");
            return;
        }

        // Prepare a StringBuilder for the compliance report.
        StringBuilder reportBuilder = new StringBuilder();
        reportBuilder.AppendLine("Visio URL Compliance Report");
        reportBuilder.AppendLine($"Diagram: {Path.GetFileName(visioPath)}");
        reportBuilder.AppendLine($"Generated: {DateTime.Now}");
        reportBuilder.AppendLine();
        reportBuilder.AppendLine("ShapeID\tPageName\tShapeName\tURL\tStatus");

        int totalUrls = 0;
        int compliantCount = 0;
        int nonCompliantCount = 0;

        try
        {
            // Load the Visio diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Iterate through each page in the diagram.
                for (int p = 0; p < diagram.Pages.Count; p++)
                {
                    Page page = diagram.Pages[p];
                    string pageName = page.NameU;

                    // Iterate through each shape on the current page.
                    for (int s = 0; s < page.Shapes.Count; s++)
                    {
                        Shape shape = page.Shapes[s];

                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve shape name (fallback to empty string if null).
                        string shapeName = shape.NameU ?? string.Empty;

                        // Guard: ensure the Hyperlinks collection is not null.
                        if (shape.Hyperlinks == null)
                            continue;

                        // Iterate through each hyperlink attached to the shape.
                        for (int h = 0; h < shape.Hyperlinks.Count; h++)
                        {
                            Hyperlink hyperlink = shape.Hyperlinks[h];

                            // Read the address of the hyperlink (cell‑based property).
                            string url = hyperlink.Address?.Value ?? string.Empty;
                            if (string.IsNullOrWhiteSpace(url))
                                continue; // Skip empty URLs.

                            totalUrls++;

                            // Validate URL against the whitelist (case‑insensitive).
                            bool isCompliant = whitelist.Exists(w => string.Equals(w, url, StringComparison.OrdinalIgnoreCase));

                            if (isCompliant)
                                compliantCount++;
                            else
                                nonCompliantCount++;

                            string status = isCompliant ? "Compliant" : "Non‑Compliant";

                            // Append a line to the report.
                            reportBuilder.AppendLine($"{shape.ID}\t{pageName}\t{shapeName}\t{url}\t{status}");
                        }
                    }
                }
            }

            // Append summary statistics.
            reportBuilder.AppendLine();
            reportBuilder.AppendLine($"Total URLs examined: {totalUrls}");
            reportBuilder.AppendLine($"Compliant URLs: {compliantCount}");
            reportBuilder.AppendLine($"Non‑Compliant URLs: {nonCompliantCount}");

            // Write the report to a file in the same directory as the Visio file.
            string reportPath = Path.Combine(Path.GetDirectoryName(visioPath) ?? string.Empty, "ComplianceReport.txt");
            File.WriteAllText(reportPath, reportBuilder.ToString());

            Console.WriteLine($"Compliance report generated: {reportPath}");
        }
        catch (Exception ex)
        {
            // Catch any Aspose.Diagram or I/O exceptions and report them.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}