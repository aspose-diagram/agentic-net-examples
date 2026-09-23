using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output directories.
        string inputFolder = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
        string outputFolder = args.Length > 1 ? args[1] : Path.Combine(Directory.GetCurrentDirectory(), "output");

        // Ensure the output directory exists.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Path to the performance log CSV.
        string logPath = Path.Combine(Directory.GetCurrentDirectory(), "conversion_log.csv");

        // Write header if the log file is new.
        if (!File.Exists(logPath))
        {
            File.WriteAllText(logPath, "FileName,DurationMs" + Environment.NewLine);
        }

        // Process each VSD file in the input folder.
        foreach (string filePath in Directory.GetFiles(inputFolder, "*.vsd"))
        {
            string fileName = Path.GetFileName(filePath);
            try
            {
                // Start timing.
                Stopwatch sw = Stopwatch.StartNew();

                // Load the Visio diagram.
                Diagram diagram = new Diagram(filePath);

                // Define the CSV output path.
                string csvOutputPath = Path.Combine(outputFolder, Path.ChangeExtension(fileName, ".csv"));

                // Save the diagram as CSV.
                diagram.Save(csvOutputPath, SaveFileFormat.Csv);

                // Stop timing.
                sw.Stop();

                // Append the result to the log file.
                string logLine = $"{fileName},{sw.ElapsedMilliseconds}";
                File.AppendAllText(logPath, logLine + Environment.NewLine);
            }
            catch (Exception ex)
            {
                // Log the error for this file.
                string errorLine = $"{fileName},Error:{ex.Message}";
                File.AppendAllText(logPath, errorLine + Environment.NewLine);
            }
        }
    }
}
