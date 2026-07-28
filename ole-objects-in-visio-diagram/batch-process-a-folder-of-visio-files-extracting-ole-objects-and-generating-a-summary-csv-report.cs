using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder to process. If not provided, use the current directory.
            string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder does not exist: {folderPath}");
                return;
            }

            // Prepare a list to hold CSV rows.
            List<string> csvLines = new List<string>();
            // Header row.
            csvLines.Add("FileName,PageName,ShapeID,ObjectSourceFullName,ObjectDataSizeBytes");

            // Supported Visio extensions.
            string[] extensions = new[] { ".vsdx", ".vsd", ".vdx", ".vssx", ".vss", ".vstx", ".vst" };

            // Get all Visio files in the folder (non‑recursive).
            string[] files = Directory.GetFiles(folderPath);
            foreach (string filePath in files)
            {
                string ext = Path.GetExtension(filePath);
                if (Array.IndexOf(extensions, ext, 0, extensions.Length) < 0)
                {
                    continue; // Skip non‑Visio files.
                }

                try
                {
                    // Load the diagram.
                    Diagram diagram = new Diagram(filePath);

                    // Iterate pages.
                    foreach (Aspose.Diagram.Page page in diagram.Pages)
                    {
                        // Iterate shapes.
                        foreach (Aspose.Diagram.Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes.
                            if (shape.Del == BOOL.True)
                                continue;

                            // Verify the shape is a foreign (OLE) shape.
                            if (shape.Type != TypeValue.Foreign)
                                continue;

                            // Ensure ForeignData exists and is an OLE object.
                            if (shape.ForeignData == null)
                                continue;

                            if (shape.ForeignData.ForeignType != ForeignType.Object)
                                continue;

                            // Ensure there is binary data.
                            if (shape.ForeignData.ObjectData == null || shape.ForeignData.ObjectData.Length == 0)
                                continue;

                            // Wrap the binary data in a MemoryStream as per the OLE handling rule.
                            long dataSize;
                            using (MemoryStream ms = new MemoryStream(shape.ForeignData.ObjectData))
                            {
                                dataSize = ms.Length;
                            }

                            // Build CSV line.
                            string fileName = Path.GetFileName(filePath);
                            string pageName = page.NameU ?? string.Empty;
                            long shapeId = shape.ID;
                            string sourceFullName = shape.ForeignData.ObjectSourceFullName ?? string.Empty;

                            // Escape commas in fields if necessary.
                            string csvLine = $"{EscapeCsv(fileName)},{EscapeCsv(pageName)},{shapeId},{EscapeCsv(sourceFullName)},{dataSize}";
                            csvLines.Add(csvLine);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            // Write the CSV report to the folder.
            string outputCsvPath = Path.Combine(folderPath, "OleSummary.csv");
            try
            {
                File.WriteAllLines(outputCsvPath, csvLines, Encoding.UTF8);
                Console.WriteLine($"OLE summary CSV generated at: {outputCsvPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write CSV file: {ex.Message}");
            }
        }

        // Helper to escape CSV fields containing commas or quotes.
        private static string EscapeCsv(string field)
        {
            if (field == null)
                return string.Empty;

            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
            {
                string escaped = field.Replace("\"", "\"\"");
                return $"\"{escaped}\"";
            }
            return field;
        }
    }