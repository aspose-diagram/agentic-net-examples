using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input_folder_path> [output_csv_path]");
            return;
        }

        string inputFolder = args[0];
        string outputCsv = args.Length >= 2 ? args[1] : "VbaModuleReport.csv";

        // Guard: input folder must exist.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Prepare CSV header.
        try
        {
            using (var writer = new StreamWriter(outputCsv, false))
            {
                writer.WriteLine("DiagramPath,ModuleCount");

                // Process each Visio file in the folder (non-recursive).
                string[] visioExtensions = new[] { ".vsdx", ".vsd", ".vsdm", ".vssx", ".vss", ".vssm", ".vstx", ".vst", ".vstm", ".vdx", ".vsx", ".vtx" };
                foreach (string filePath in Directory.GetFiles(inputFolder))
                {
                    // Guard: file must exist (redundant but per rules).
                    if (!File.Exists(filePath))
                    {
                        Console.Error.WriteLine($"File not found: {filePath}");
                        continue;
                    }

                    // Process only files with Visio extensions.
                    string ext = Path.GetExtension(filePath);
                    if (Array.IndexOf(visioExtensions, ext, 0, visioExtensions.Length) < 0)
                    {
                        // Skip non-Visio files.
                        continue;
                    }

                    int moduleCount = 0;

                    try
                    {
                        // Load the diagram.
                        Diagram diagram = new Diagram(filePath);

                        // If a VBA project exists, count its modules.
                        if (diagram.VbaProject != null && diagram.VbaProject.Modules != null)
                        {
                            moduleCount = diagram.VbaProject.Modules.Count;
                        }

                        // Dispose diagram to free resources.
                        diagram.Dispose();
                    }
                    catch (Exception ex)
                    {
                        // Log loading/processing errors and continue with next file.
                        Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
                        continue;
                    }

                    // Write CSV line for this diagram.
                    writer.WriteLine($"{EscapeCsv(filePath)},{moduleCount}");
                }
            }

            Console.WriteLine($"CSV report generated at: {Path.GetFullPath(outputCsv)}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write CSV report: {ex.Message}");
        }
    }

    // Helper to escape CSV fields containing commas or quotes.
    private static string EscapeCsv(string field)
    {
        if (field.Contains("\""))
        {
            field = field.Replace("\"", "\"\"");
        }
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
        {
            field = $"\"{field}\"";
        }
        return field;
    }
}