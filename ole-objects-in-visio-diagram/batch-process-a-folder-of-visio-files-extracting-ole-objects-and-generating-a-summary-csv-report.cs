using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Folder containing Visio files (change as needed)
            string inputFolder = @"C:\VisioFiles";
            // Output CSV file path
            string outputCsv = @"C:\VisioOleReport.csv";

            // Prepare list to hold CSV rows
            List<string> csvLines = new List<string>();
            // Header
            csvLines.Add("FileName,PageName,ShapeID,ObjectSourceFullName,DataSizeBytes");

            // Get all Visio files in the folder (supports .vsdx, .vsd, .vdx, etc.)
            string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx" && extension != ".vsdm")
                {
                    // Skip non-Visio files
                    continue;
                }

                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate pages
                foreach (Page page in diagram.Pages)
                {
                    // Page name may be empty; use index as fallback
                    string pageName = string.IsNullOrEmpty(page.Name) ? $"Page_{page.ID}" : page.Name;

                    // Iterate shapes
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if shape is a foreign (OLE) shape
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.ForeignType == ForeignType.Object)
                        {
                            // Ensure OLE binary data exists
                            byte[] oleData = shape.ForeignData.ObjectData;
                            if (oleData == null || oleData.Length == 0)
                                continue;

                            // Gather information
                            long shapeId = shape.ID;
                            string objectSource = shape.ForeignData.ObjectSourceFullName ?? string.Empty;
                            long dataSize = oleData.Length;

                            // Build CSV line (escape commas if needed)
                            string csvLine = $"{Path.GetFileName(filePath)},{pageName},{shapeId},{objectSource},{dataSize}";
                            csvLines.Add(csvLine);
                        }
                    }
                }
            }

            // Write all lines to the CSV file
            using (StreamWriter writer = new StreamWriter(outputCsv, false))
            {
                foreach (string line in csvLines)
                {
                    writer.WriteLine(line);
                }
            }

            Console.WriteLine($"OLE extraction completed. Report saved to: {outputCsv}");
        }
    }