using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    // Predefined template dimensions in inches (example: US Letter size)
    private const double TemplatePageWidth = 8.5;
    private const double TemplatePageHeight = 11.0;

    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output log file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputLogFile>");
            return;
        }

        string inputPath = args[0];
        string logPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Ensure the directory for the log file exists
        try
        {
            string logDir = Path.GetDirectoryName(logPath);
            if (!string.IsNullOrEmpty(logDir) && !Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to prepare log directory: {ex.Message}");
            return;
        }

        // Open the diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Open a StreamWriter for the log file
                using (StreamWriter logWriter = new StreamWriter(logPath, false))
                {
                    // Iterate over each page explicitly typing the iterator
                    for (int i = 0; i < diagram.Pages.Count; i++)
                    {
                        Page page = diagram.Pages[i];

                        // Retrieve actual page dimensions (in inches)
                        double actualWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double actualHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Compare width
                        if (Math.Abs(actualWidth - TemplatePageWidth) > 0.001)
                        {
                            logWriter.WriteLine($"Page {i + 1}: Width mismatch. Actual = {actualWidth:F3} in, Expected = {TemplatePageWidth:F3} in.");
                        }

                        // Compare height
                        if (Math.Abs(actualHeight - TemplatePageHeight) > 0.001)
                        {
                            logWriter.WriteLine($"Page {i + 1}: Height mismatch. Actual = {actualHeight:F3} in, Expected = {TemplatePageHeight:F3} in.");
                        }

                        // If both dimensions match, optionally note success (can be omitted)
                        if (Math.Abs(actualWidth - TemplatePageWidth) <= 0.001 &&
                            Math.Abs(actualHeight - TemplatePageHeight) <= 0.001)
                        {
                            logWriter.WriteLine($"Page {i + 1}: Dimensions match the template.");
                        }
                    }

                    // Flush and close the writer (using statement handles this)
                }
            }

            Console.WriteLine($"Validation completed. Log written to: {logPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or IO exceptions
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}