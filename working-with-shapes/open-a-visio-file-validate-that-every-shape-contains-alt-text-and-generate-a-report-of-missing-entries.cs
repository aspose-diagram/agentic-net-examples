using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 1)
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

        // Prepare a StringBuilder to collect the report lines.
        StringBuilder reportBuilder = new StringBuilder();
        reportBuilder.AppendLine("Alt Text Validation Report");
        reportBuilder.AppendLine($"Source File: {visioPath}");
        reportBuilder.AppendLine($"Generated On: {DateTime.Now}");
        reportBuilder.AppendLine(new string('=', 50));

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve Alt text (comment) from the shape's Misc.Comment.
                    string altText = shape.Misc?.Comment?.Value ?? string.Empty;

                    // If Alt text is missing or whitespace, record the shape.
                    if (string.IsNullOrWhiteSpace(altText))
                    {
                        string shapeName = shape.NameU ?? "(Unnamed)";
                        string line = $"Page: {page.Name}, Shape ID: {shape.ID}, Shape Name: {shapeName} - Missing Alt Text";
                        reportBuilder.AppendLine(line);
                    }
                }
            }

            // Determine the report file path (same folder as source, with suffix).
            string reportFileName = Path.GetFileNameWithoutExtension(visioPath) + "_AltTextReport.txt";
            string reportPath = Path.Combine(Path.GetDirectoryName(visioPath) ?? string.Empty, reportFileName);

            // Write the report to the file.
            File.WriteAllText(reportPath, reportBuilder.ToString());

            // Output summary to console.
            Console.WriteLine("Alt text validation completed.");
            Console.WriteLine($"Report written to: {reportPath}");
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}