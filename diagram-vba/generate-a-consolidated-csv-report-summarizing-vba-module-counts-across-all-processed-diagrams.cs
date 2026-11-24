using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputCsvPath>");
            return;
        }

        string inputFolder = args[0];
        string outputCsvPath = args[1];

        // Verify the input folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Prepare the CSV file for writing.
        try
        {
            using (StreamWriter csvWriter = new StreamWriter(outputCsvPath, false))
            {
                // Write CSV header.
                csvWriter.WriteLine("DiagramPath,ModuleCount");

                // Gather Visio files that may contain VBA projects.
                string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
                foreach (string filePath in visioFiles)
                {
                    // Filter supported Visio extensions.
                    string ext = Path.GetExtension(filePath).ToLowerInvariant();
                    if (ext != ".vsdx" && ext != ".vsdm" && ext != ".vsd" && ext != ".vssx" && ext != ".vssm" && ext != ".vstx" && ext != ".vstm")
                    {
                        continue;
                    }

                    // Guard against missing files (should not happen after GetFiles, but required by rules).
                    if (!File.Exists(filePath))
                    {
                        Console.Error.WriteLine($"File not found: {filePath}");
                        continue;
                    }

                    try
                    {
                        // Load the diagram inside a using block as required.
                        using (Diagram diagram = new Diagram(filePath))
                        {
                            int moduleCount = 0;

                            // If a VBA project exists, count its modules.
                            if (diagram.VbaProject != null)
                            {
                                foreach (VbaModule module in diagram.VbaProject.Modules)
                                {
                                    moduleCount++;
                                }
                            }

                            // Write the result line to the CSV.
                            csvWriter.WriteLine($"{filePath},{moduleCount}");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Report any errors that occur while processing an individual diagram.
                        Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report errors related to CSV creation or writing.
            Console.Error.WriteLine($"Failed to write CSV file '{outputCsvPath}': {ex.Message}");
        }
    }
}