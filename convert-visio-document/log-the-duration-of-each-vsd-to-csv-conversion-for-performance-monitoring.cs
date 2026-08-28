using System;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Folder containing source VSD files
                string inputFolder = @"C:\Visio\Input";
                // Folder where converted files will be saved
                string outputFolder = @"C:\Visio\Output";
                // Path to the CSV log file
                string csvLogPath = @"C:\Visio\ConversionLog.csv";

                // Ensure output folder exists
                Directory.CreateDirectory(outputFolder);

                // Process each VSD file in the input folder
                foreach (string vsdFilePath in Directory.GetFiles(inputFolder, "*.vsd"))
                {
                    string fileName = Path.GetFileName(vsdFilePath);
                    string outputFilePath = Path.Combine(outputFolder, Path.ChangeExtension(fileName, ".vdx"));

                    // Start timing the conversion
                    Stopwatch sw = Stopwatch.StartNew();

                    // Load the VSD diagram
                    Diagram diagram = new Diagram(vsdFilePath);

                    // Perform conversion (example: VSD -> VDX)
                    diagram.Save(outputFilePath, SaveFileFormat.Csv);

                    // Stop timing
                    sw.Stop();

                    // Log the result to CSV
                    LogConversion(csvLogPath, fileName, sw.Elapsed);
                }

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Appends a conversion record to the CSV log file.
        /// </summary>
        /// <param name="csvPath">Path to the CSV file.</param>
        /// <param name="fileName">Name of the processed VSD file.</param>
        /// <param name="duration">Time taken for the conversion.</param>
        private static void LogConversion(string csvPath, string fileName, TimeSpan duration)
        {
            bool fileExists = File.Exists(csvPath);
            using (StreamWriter writer = new StreamWriter(csvPath, true))
            {
                // Write header if the file is newly created
                if (!fileExists)
                {
                    writer.WriteLine("FileName,ConversionStartUtc,ConversionDurationMs");
                }

                string startUtc = DateTime.UtcNow.Subtract(duration).ToString("o", CultureInfo.InvariantCulture);
                long durationMs = (long)duration.TotalMilliseconds;

                writer.WriteLine($"{EscapeCsv(fileName)},{startUtc},{durationMs}");
            }
        }

        /// <summary>
        /// Escapes a CSV field by wrapping it in double quotes if needed.
        /// </summary>
        private static string EscapeCsv(string field)
        {
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
            {
                return $"\"{field.Replace("\"", "\"\"")}\"";
            }
            return field;
        }
    }