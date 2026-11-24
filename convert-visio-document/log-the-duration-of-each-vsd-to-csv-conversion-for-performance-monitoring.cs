using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine source folder, output folder, and log file path from arguments or use defaults.
        string sourceFolder = args.Length > 0 ? args[0] : "InputVsd";
        string outputFolder = args.Length > 1 ? args[1] : "OutputCsv";
        string logFilePath = args.Length > 2 ? args[2] : "conversion_log.csv";

        // Guard: source folder must exist.
        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder does not exist: {sourceFolder}");
            return;
        }

        // Ensure output folder exists.
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder '{outputFolder}': {ex.Message}");
                return;
            }
        }

        // Prepare the CSV log file. Write header if the file is new.
        bool writeHeader = !File.Exists(logFilePath);
        try
        {
            using (var logWriter = new StreamWriter(logFilePath, append: true))
            {
                if (writeHeader)
                {
                    logWriter.WriteLine("InputFile,OutputFile,DurationMilliseconds,Status");
                }

                // Get all Visio files with common extensions.
                string[] visioExtensions = new[] { ".vsd", ".vsdx", ".vsx", ".vtx", ".vss", ".vst", ".vssx", ".vstx", ".vssm", ".vstm" };
                var visioFiles = Directory.GetFiles(sourceFolder, "*.*", SearchOption.TopDirectoryOnly);
                foreach (var filePath in visioFiles)
                {
                    string extension = Path.GetExtension(filePath);
                    if (Array.IndexOf(visioExtensions, extension.ToLowerInvariant()) < 0)
                    {
                        // Skip non-Visio files.
                        continue;
                    }

                    // Guard: ensure the file exists (redundant but per requirement).
                    if (!File.Exists(filePath))
                    {
                        Console.Error.WriteLine($"File not found: {filePath}");
                        continue;
                    }

                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                    string outputCsvPath = Path.Combine(outputFolder, fileNameWithoutExt + ".csv");

                    Stopwatch sw = new Stopwatch();
                    try
                    {
                        // Start timing.
                        sw.Start();

                        // Load the Visio diagram.
                        Diagram diagram = new Diagram(filePath);

                        // Save as CSV.
                        diagram.Save(outputCsvPath, SaveFileFormat.Csv);

                        // Stop timing.
                        sw.Stop();

                        // Log successful conversion.
                        logWriter.WriteLine($"{EscapeCsv(filePath)},{EscapeCsv(outputCsvPath)},{sw.ElapsedMilliseconds},Success");
                        Console.WriteLine($"Converted '{filePath}' to CSV in {sw.ElapsedMilliseconds} ms.");
                    }
                    catch (Exception ex)
                    {
                        // Stop timing on error.
                        sw.Stop();

                        // Log failure with error details.
                        logWriter.WriteLine($"{EscapeCsv(filePath)},{EscapeCsv(outputCsvPath)},{sw.ElapsedMilliseconds},Error: {EscapeCsv(ex.Message)}");
                        Console.Error.WriteLine($"Error converting '{filePath}': {ex.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write log file '{logFilePath}': {ex.Message}");
        }
    }

    // Helper to escape CSV fields that may contain commas or quotes.
    private static string EscapeCsv(string field)
    {
        if (field == null)
            return "";

        if (field.Contains("\""))
            field = field.Replace("\"", "\"\"");

        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
            return $"\"{field}\"";

        return field;
    }
}