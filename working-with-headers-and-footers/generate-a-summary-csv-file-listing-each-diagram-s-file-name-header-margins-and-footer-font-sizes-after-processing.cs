using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder containing diagram files.
        string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        // Guard: ensure the folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Prepare a list of supported Visio file extensions.
        string[] extensions = new[] { ".vsdx", ".vsd", ".vdx", ".vssx", ".vstx", ".vss", ".vst" };

        // Collect all diagram files in the folder (non-recursive).
        List<string> diagramFiles = new List<string>();
        foreach (string ext in extensions)
        {
            diagramFiles.AddRange(Directory.GetFiles(folderPath, "*" + ext, SearchOption.TopDirectoryOnly));
        }

        // Define the output CSV file path.
        string csvPath = Path.Combine(folderPath, "summary.csv");

        try
        {
            using (StreamWriter writer = new StreamWriter(csvPath))
            {
                // Write CSV header.
                writer.WriteLine("FileName,HeaderMargin,FooterMargin,FooterFontSize");

                // Process each diagram file.
                foreach (string filePath in diagramFiles)
                {
                    // Guard: ensure the file still exists.
                    if (!File.Exists(filePath))
                    {
                        Console.Error.WriteLine($"File not found (skipped): {filePath}");
                        continue;
                    }

                    try
                    {
                        // Load the diagram.
                        Diagram diagram = new Diagram(filePath);

                        // Retrieve header margin (in inches).
                        double headerMargin = diagram.HeaderFooter.HeaderMargin.Value;

                        // Retrieve footer margin (in inches).
                        double footerMargin = diagram.HeaderFooter.FooterMargin.Value;

                        // Retrieve footer font size (height property, integer points).
                        int footerFontSize = diagram.HeaderFooter.HeaderFooterFont.Height;

                        // Write a CSV line for this diagram.
                        string fileName = Path.GetFileName(filePath);
                        writer.WriteLine($"{EscapeCsv(fileName)},{headerMargin},{footerMargin},{footerFontSize}");
                    }
                    catch (Exception ex)
                    {
                        // Log any errors that occur while processing this diagram.
                        Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                    }
                }
            }

            Console.WriteLine($"Summary CSV generated at: {csvPath}");
        }
        catch (Exception ex)
        {
            // Log errors related to CSV file creation/writing.
            Console.Error.WriteLine($"Failed to create summary CSV: {ex.Message}");
        }
    }

    // Helper method to escape CSV fields containing commas or quotes.
    private static string EscapeCsv(string field)
    {
        if (field.Contains(",") || field.Contains("\""))
        {
            string escaped = field.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        return field;
    }
}