using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input folder containing VSD files
        string inputFolder = @"C:\VisioFiles";
        // Output folder for CSV files
        string outputFolder = @"C:\VisioCsvOutput";
        // Path to the performance log CSV
        string logPath = Path.Combine(outputFolder, "ConversionPerformanceLog.csv");

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Prepare the log file with header
        using (var logWriter = new StreamWriter(logPath, false))
        {
            logWriter.WriteLine("FileName,DurationMs");

            // Get all .vsd files in the input folder
            string[] vsdFiles = Directory.GetFiles(inputFolder, "*.vsd", SearchOption.TopDirectoryOnly);
            foreach (string vsdPath in vsdFiles)
            {
                string fileName = Path.GetFileName(vsdPath);
                string csvOutputPath = Path.Combine(outputFolder, Path.ChangeExtension(fileName, ".csv"));

                // Measure conversion time
                Stopwatch sw = Stopwatch.StartNew();

                // Load the VSD diagram
                Diagram diagram = new Diagram(vsdPath);

                // Save as CSV
                diagram.Save(csvOutputPath, SaveFileFormat.Csv);

                sw.Stop();

                // Log the duration
                logWriter.WriteLine($"{fileName},{sw.ElapsedMilliseconds}");
                Console.WriteLine($"Converted {fileName} in {sw.ElapsedMilliseconds} ms");
            }
        }

        Console.WriteLine("All conversions completed. Performance log saved to:");
        Console.WriteLine(logPath);
    }
}
