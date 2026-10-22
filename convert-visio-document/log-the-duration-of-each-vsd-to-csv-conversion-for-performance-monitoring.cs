using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input folder, output folder, optional log file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputFolder> <outputFolder> [logFilePath]");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];
        string logFilePath = args.Length >= 3 ? args[2] : Path.Combine(outputFolder, "conversion_log.csv");

        // Guard for input folder existence
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        // Prepare log file: write header if file does not exist
        bool writeHeader = !File.Exists(logFilePath);
        try
        {
            using (var logWriter = new StreamWriter(logFilePath, append: true))
            {
                if (writeHeader)
                {
                    logWriter.WriteLine("FileName,StartTime,EndTime,DurationSeconds");
                }

                // Define supported Visio extensions (case‑insensitive)
                var visioExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    ".vsd", ".vsdx", ".vsx", ".vss", ".vssx", ".vst", ".vstx", ".vtx"
                };

                // Iterate over all files in the input folder
                foreach (string filePath in Directory.GetFiles(inputFolder))
                {
                    // Guard for file existence (redundant but per rule)
                    if (!File.Exists(filePath))
                    {
                        Console.Error.WriteLine($"File not found: {filePath}");
                        continue;
                    }

                    string extension = Path.GetExtension(filePath);
                    if (!visioExtensions.Contains(extension))
                    {
                        // Skip non‑Visio files
                        continue;
                    }

                    string fileName = Path.GetFileName(filePath);
                    string outputCsvPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(filePath) + ".csv");

                    DateTime startTime = DateTime.Now;
                    var sw = new Stopwatch();

                    try
                    {
                        // Start timing before loading
                        sw.Start();

                        // Load the Visio diagram
                        Diagram diagram = new Diagram(filePath);

                        // Save as CSV using the correct overload
                        diagram.Save(outputCsvPath, SaveFileFormat.Csv);

                        // Stop timing after saving
                        sw.Stop();
                    }
                    catch (Exception ex)
                    {
                        // Log failure with zero duration
                        Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
                        DateTime errorTime = DateTime.Now;
                        logWriter.WriteLine($"{fileName},{startTime:O},{errorTime:O},0");
                        continue;
                    }

                    DateTime endTime = DateTime.Now;
                    double durationSeconds = sw.Elapsed.TotalSeconds;

                    // Write log entry
                    logWriter.WriteLine($"{fileName},{startTime:O},{endTime:O},{durationSeconds}");
                    Console.WriteLine($"Converted '{fileName}' to CSV in {durationSeconds:F2} seconds.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write log file: {ex.Message}");
        }
    }
}