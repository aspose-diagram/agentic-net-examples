using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input folder and output CSV path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <InputFolderPath> <OutputCsvPath>");
            return;
        }

        string inputFolder = args[0];
        string outputCsvPath = args[1];

        // Guard for input folder existence
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure the directory for the CSV file exists
        string csvDirectory = Path.GetDirectoryName(outputCsvPath);
        if (!string.IsNullOrEmpty(csvDirectory) && !Directory.Exists(csvDirectory))
        {
            try
            {
                Directory.CreateDirectory(csvDirectory);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create directory for CSV: {ex.Message}");
                return;
            }
        }

        // Create a subfolder for extracted OLE files next to the CSV file
        string oleExportFolder = Path.Combine(csvDirectory ?? "", "OleExports");
        try
        {
            Directory.CreateDirectory(oleExportFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create OLE export folder: {ex.Message}");
            return;
        }

        // Prepare CSV writer
        using (var csvWriter = new StreamWriter(outputCsvPath, false))
        {
            // Write CSV header
            csvWriter.WriteLine("DiagramFile,PageID,ShapeID,OleSource,ExportedFile,SizeBytes");

            // Define Visio file extensions to process
            string[] visioExtensions = new[] { ".vsdx", ".vsd", ".vsx", ".vtx", ".vdx", ".vssx", ".vstx", ".vss", ".vst", ".vsdm", ".vssm", ".vstm" };

            // Iterate over all files in the input folder (recursive)
            foreach (string filePath in Directory.GetFiles(inputFolder, "*.*", SearchOption.AllDirectories))
            {
                // Filter by supported Visio extensions (case-insensitive)
                if (Array.IndexOf(visioExtensions, Path.GetExtension(filePath).ToLowerInvariant()) < 0)
                    continue;

                // Guard for file existence (should always be true here)
                if (!File.Exists(filePath))
                {
                    Console.Error.WriteLine($"File not found (skipped): {filePath}");
                    continue;
                }

                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(filePath);

                    // Iterate through pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Detect OLE (foreign) shapes via ForeignData
                            if (shape.ForeignData != null && shape.ForeignData.ObjectData != null && shape.ForeignData.ObjectData.Length > 0)
                            {
                                byte[] oleData = shape.ForeignData.ObjectData;
                                string oleSource = shape.ForeignData.ObjectSourceFullName ?? string.Empty;

                                // Determine file extension for the OLE object (fallback to .bin)
                                string fileExtension = ".bin";
                                if (!string.IsNullOrEmpty(oleSource))
                                {
                                    string extFromSource = Path.GetExtension(oleSource);
                                    if (!string.IsNullOrEmpty(extFromSource))
                                        fileExtension = extFromSource;
                                }

                                // Build a unique file name for the extracted OLE object
                                string diagramBaseName = Path.GetFileNameWithoutExtension(filePath);
                                string exportFileName = $"{diagramBaseName}_Page{page.ID}_Shape{shape.ID}{fileExtension}";
                                string exportFilePath = Path.Combine(oleExportFolder, exportFileName);

                                // Write the OLE binary data to disk
                                try
                                {
                                    File.WriteAllBytes(exportFilePath, oleData);
                                }
                                catch (Exception writeEx)
                                {
                                    Console.Error.WriteLine($"Failed to write OLE file '{exportFilePath}': {writeEx.Message}");
                                    continue; // Skip CSV entry for this shape
                                }

                                // Write a line to the CSV report
                                string csvLine = $"{EscapeCsv(filePath)},{page.ID},{shape.ID},{EscapeCsv(oleSource)},{EscapeCsv(exportFilePath)},{oleData.Length}";
                                csvWriter.WriteLine(csvLine);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                    // Continue with next file
                }
            }
        }

        Console.WriteLine($"Processing completed. Summary CSV saved to: {outputCsvPath}");
    }

    // Helper method to escape CSV fields containing commas or quotes
    private static string EscapeCsv(string field)
    {
        if (field == null)
            return string.Empty;

        if (field.Contains("\""))
            field = field.Replace("\"", "\"\"");

        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
            return $"\"{field}\"";

        return field;
    }
}