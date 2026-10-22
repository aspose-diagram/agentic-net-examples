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
            Console.Error.WriteLine("Usage: Program <InputFolderPath> <OutputCsvPath> [OleOutputFolder]");
            return;
        }

        string inputFolder = args[0];
        string outputCsvPath = args[1];
        string oleOutputFolder = args.Length >= 3 ? args[2] : Path.Combine(Path.GetDirectoryName(outputCsvPath) ?? "", "ExtractedOle");

        // Guard input folder existence
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure OLE output folder exists
        try
        {
            Directory.CreateDirectory(oleOutputFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create OLE output folder '{oleOutputFolder}': {ex.Message}");
            return;
        }

        // Prepare CSV writer
        StreamWriter csvWriter;
        try
        {
            csvWriter = new StreamWriter(outputCsvPath, false);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create CSV file '{outputCsvPath}': {ex.Message}");
            return;
        }

        using (csvWriter)
        {
            // Write CSV header
            csvWriter.WriteLine("DiagramFile,PageId,ShapeId,OleSourceFullName,ExtractedFileName,ShowAsIcon");

            // Get Visio files (common extensions)
            string[] visioExtensions = new[] { ".vsdx", ".vsd", ".vsx", ".vtx", ".vdx", ".vssx", ".vss", ".vstx", ".vst", ".vstm", ".vsdm", ".vssm", ".vstm" };
            string[] files = Directory.GetFiles(inputFolder);

            foreach (string filePath in files)
            {
                // Filter by Visio extensions
                if (Array.IndexOf(visioExtensions, Path.GetExtension(filePath).ToLowerInvariant()) < 0)
                    continue;

                // Guard file existence (redundant but per rules)
                if (!File.Exists(filePath))
                {
                    Console.Error.WriteLine($"File not found: {filePath}");
                    continue;
                }

                try
                {
                    // Load diagram
                    Diagram diagram = new Diagram(filePath);

                    // Iterate pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Detect OLE objects via ForeignData
                            if (shape.ForeignData != null && shape.ForeignData.ObjectData != null && shape.ForeignData.ObjectData.Length > 0)
                            {
                                // Extract OLE binary data
                                string sourceFullName = shape.ForeignData.ObjectSourceFullName ?? string.Empty;
                                string extension = Path.GetExtension(sourceFullName);
                                if (string.IsNullOrEmpty(extension))
                                    extension = ".bin";

                                // Build unique file name for extracted OLE object
                                string oleFileName = $"{Path.GetFileNameWithoutExtension(filePath)}_Page{page.ID}_Shape{shape.ID}{extension}";
                                string oleFilePath = Path.Combine(oleOutputFolder, oleFileName);

                                // Write OLE data to file using MemoryStream as required
                                using (MemoryStream ms = new MemoryStream(shape.ForeignData.ObjectData))
                                using (FileStream fs = new FileStream(oleFilePath, FileMode.Create, FileAccess.Write))
                                {
                                    ms.CopyTo(fs);
                                }

                                // Determine ShowAsIcon flag
                                string showAsIcon = shape.ForeignData.ShowAsIcon == BOOL.True ? "True" : "False";

                                // Write summary line to CSV
                                csvWriter.WriteLine($"{Path.GetFileName(filePath)},{page.ID},{shape.ID},{sourceFullName},{oleFileName},{showAsIcon}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log any errors while processing a diagram
                    Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }
        }

        Console.WriteLine("Processing completed.");
    }
}