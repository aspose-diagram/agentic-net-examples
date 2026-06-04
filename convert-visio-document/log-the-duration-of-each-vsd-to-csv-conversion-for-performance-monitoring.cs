using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;

class VsdToCsvPerformanceLogger
{
    // Path to the folder containing VSD files
    private const string InputFolder = @"C:\Diagrams\Input";

    // Path to the CSV file where performance data will be logged
    private const string LogCsvPath = @"C:\Diagrams\ConversionLog.csv";

    static void Main()
    {
        try
        {

            // Ensure the log file exists and write the header
            if (!File.Exists(LogCsvPath))
            {
                File.WriteAllText(LogCsvPath, "FileName,DurationMilliseconds\r\n");
            }

            // Get all VSD files in the input folder
            string[] vsdFiles = Directory.GetFiles(InputFolder, "*.vsd", SearchOption.TopDirectoryOnly);

            foreach (string vsdPath in vsdFiles)
            {
                // Start timing the conversion
                Stopwatch sw = Stopwatch.StartNew();

                // Load the VSD diagram (using Aspose.Diagram's load functionality)
                Diagram diagram = new Diagram(vsdPath);

                // Define the output CSV path for this diagram
                string csvOutputPath = Path.ChangeExtension(vsdPath, ".csv");

                // Perform the conversion.
                // Aspose.Diagram does not have a direct CSV export, so we export to VDX (XML) as an example.
                // Replace SaveFileFormat.Vdx with the appropriate format if CSV export becomes available.
                diagram.Save(csvOutputPath, SaveFileFormat.Vdx);

                // Stop timing
                sw.Stop();

                // Log the duration to the CSV file
                string logLine = $"{Path.GetFileName(vsdPath)},{sw.ElapsedMilliseconds}";
                File.AppendAllText(LogCsvPath, logLine + "\r\n");
            }

            Console.WriteLine("Conversion completed. Performance data logged to: " + LogCsvPath);

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
