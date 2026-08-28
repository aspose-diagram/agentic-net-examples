using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

namespace VisioOleExtractor
{
    // Represents a single record in the CSV summary.
    class OleRecord
    {
        public string FileName { get; set; } = string.Empty;
        public long ShapeId { get; set; }
        public string ShapeName { get; set; } = string.Empty;
        public long OleSizeBytes { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder to process.
            string folderPath;
            if (args.Length > 0)
            {
                folderPath = args[0];
            }
            else
            {
                Console.Write("Enter the full path of the folder containing Visio files: ");
                folderPath = Console.ReadLine() ?? string.Empty;
            }

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("The specified folder does not exist.");
                return;
            }

            // Prepare a list to hold all OLE extraction records.
            List<OleRecord> records = new List<OleRecord>();

            // Supported Visio extensions.
            string[] extensions = new[] { ".vsdx", ".vsd", ".vsdm", ".vssx", ".vss", ".vssm", ".vstx", ".vst", ".vstm", ".vdx", ".vtx" };

            // Enumerate all files with the supported extensions.
            foreach (string filePath in Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly))
            {
                if (Array.IndexOf(extensions, Path.GetExtension(filePath).ToLowerInvariant()) < 0)
                    continue; // Skip non‑Visio files.

                try
                {
                    // Load the Visio diagram.
                    Diagram diagram = new Diagram(filePath);

                    // Iterate through each page.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Verify the shape is a foreign (OLE) shape.
                            if (shape.Type != TypeValue.Foreign)
                                continue;

                            // Ensure ForeignData is present.
                            if (shape.ForeignData == null)
                                continue;

                            // Verify the embedded object type.
                            if (shape.ForeignData.ObjectType != ObjectType.EmbeddedObject)
                                continue;

                            // Retrieve the binary OLE data.
                            byte[] oleData = shape.ForeignData.ObjectData;
                            if (oleData == null || oleData.Length == 0)
                                continue; // No data to record.

                            // Create a record for the CSV.
                            OleRecord rec = new OleRecord
                            {
                                FileName = Path.GetFileName(filePath),
                                ShapeId = shape.ID,
                                ShapeName = shape.Name ?? string.Empty,
                                OleSizeBytes = oleData.Length
                            };
                            records.Add(rec);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log loading errors but continue processing other files.
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            // Define the output CSV path.
            string csvPath = Path.Combine(folderPath, "OleSummaryReport.csv");

            // Write the CSV file.
            try
            {
                using (StreamWriter writer = new StreamWriter(csvPath, false))
                {
                    // Write header.
                    writer.WriteLine("FileName,ShapeId,ShapeName,OleSizeBytes");

                    // Write each record.
                    foreach (OleRecord rec in records)
                    {
                        // Simple CSV escaping.
                        string fileNameEsc = EscapeCsv(rec.FileName);
                        string shapeNameEsc = EscapeCsv(rec.ShapeName);
                        writer.WriteLine($"{fileNameEsc},{rec.ShapeId},{shapeNameEsc},{rec.OleSizeBytes}");
                    }
                }

                Console.WriteLine($"OLE extraction summary written to: {csvPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write CSV report: {ex.Message}");
            }
        }

        // Escapes a CSV field by surrounding it with quotes if needed.
        private static string EscapeCsv(string field)
        {
            if (field.Contains("\""))
                field = field.Replace("\"", "\"\"");

            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
                return $"\"{field}\"";

            return field;
        }
    }
}