using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string visioPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        // Prepare a report file path (same folder, fixed name)
        string reportPath = Path.Combine(Path.GetDirectoryName(visioPath) ?? "", "AltTextReport.txt");

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Use a StringWriter to build the report content
                using (StringWriter reportWriter = new StringWriter())
                {
                    reportWriter.WriteLine($"Alt Text Validation Report for: {visioPath}");
                    reportWriter.WriteLine($"Generated on: {DateTime.Now}");
                    reportWriter.WriteLine(new string('=', 50));

                    bool anyMissing = false;
                    int pageCounter = 0; // Manual page index because Page.Index does not exist

                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        pageCounter++;

                        // Iterate through each shape on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del != BOOL.False)
                                continue;

                            // Retrieve Alt text (comment) if present
                            string altText = shape.Misc?.Comment?.Value ?? string.Empty;

                            // If Alt text is missing or whitespace, record it
                            if (string.IsNullOrWhiteSpace(altText))
                            {
                                anyMissing = true;
                                string pageName = page.Name ?? $"Page_{pageCounter}";
                                reportWriter.WriteLine($"Missing Alt Text - Page: {pageName}, Shape ID: {shape.ID}");
                            }
                        }
                    }

                    if (!anyMissing)
                    {
                        reportWriter.WriteLine("All shapes contain Alt text. No missing entries found.");
                    }

                    // Write the report to console
                    Console.WriteLine(reportWriter.ToString());

                    // Also write the report to a file
                    File.WriteAllText(reportPath, reportWriter.ToString());
                    Console.WriteLine($"Report saved to: {reportPath}");
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}