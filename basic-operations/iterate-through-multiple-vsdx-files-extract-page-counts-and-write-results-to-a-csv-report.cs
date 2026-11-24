using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect input folder and output CSV file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input_folder_path> <output_csv_path>");
            return;
        }

        string inputFolder = args[0];
        string outputCsvPath = args[1];

        // Guard: input folder must exist
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Guard: output directory must exist (create if necessary)
        string outputDir = Path.GetDirectoryName(outputCsvPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {outputDir}. Error: {ex.Message}");
                return;
            }
        }

        // Prepare CSV writer
        try
        {
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
            {
                // Write CSV header
                writer.WriteLine("FilePath,PageCount");

                // Get all VSDX files in the input folder (non-recursive)
                string[] vsdxFiles = Directory.GetFiles(inputFolder, "*.vsdx");

                foreach (string filePath in vsdxFiles)
                {
                    // Guard: ensure the file still exists
                    if (!File.Exists(filePath))
                    {
                        Console.Error.WriteLine($"File not found (skipping): {filePath}");
                        continue;
                    }

                    try
                    {
                        // Load the Visio diagram
                        Diagram diagram = new Diagram(filePath);

                        // Retrieve page count
                        int pageCount = diagram.Pages.Count;

                        // Write result to CSV
                        // Escape double quotes in path if any
                        string escapedPath = filePath.Replace("\"", "\"\"");
                        writer.WriteLine($"\"{escapedPath}\",{pageCount}");
                    }
                    catch (Exception ex)
                    {
                        // Log any Aspose.Diagram errors for this file
                        Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                    }
                }
            }

            Console.WriteLine($"Report generated successfully at: {outputCsvPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write CSV report: {ex.Message}");
        }
    }
}