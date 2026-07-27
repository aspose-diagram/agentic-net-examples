using System;
using System.IO;
using Aspose.Diagram;

// Console application that converts all VSD files in a specified folder to CSV files.
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
                Console.Write("Enter the full path of the folder containing VSD files: ");
                folderPath = Console.ReadLine();
            }

            if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
            {
                Console.WriteLine("The specified folder does not exist.");
                return;
            }

            // Get all *.vsd files in the folder (non‑recursive).
            string[] vsdFiles = Directory.GetFiles(folderPath, "*.vsd", SearchOption.TopDirectoryOnly);
            if (vsdFiles.Length == 0)
            {
                Console.WriteLine("No VSD files found in the specified folder.");
                return;
            }

            foreach (string vsdFile in vsdFiles)
            {
                try
                {
                    string csvFile = Path.ChangeExtension(vsdFile, ".csv");
                    ExportDiagramToCsv(vsdFile, csvFile);
                    Console.WriteLine($"Converted: {Path.GetFileName(vsdFile)} -> {Path.GetFileName(csvFile)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to convert '{Path.GetFileName(vsdFile)}': {ex.Message}");
                }
            }

            Console.WriteLine("Processing completed.");
        }

        // Loads a VSD diagram and writes its shape data to a CSV file.
        private static void ExportDiagramToCsv(string vsdPath, string csvPath)
        {
            // Load the Visio diagram. VSD is a binary Visio format.
            Diagram diagram = new Diagram(vsdPath, LoadFileFormat.Vsd);

            using (StreamWriter writer = new StreamWriter(csvPath, false, System.Text.Encoding.UTF8))
            {
                // Write CSV header.
                writer.WriteLine("PageIndex,ShapeId,ShapeName,ShapeText");

                // Iterate through each page.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through each shape on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve shape identifier.
                        long shapeId = shape.ID;

                        // Shape name (NameU is the universal name).
                        string shapeName = shape.NameU ?? string.Empty;

                        // Retrieve plain text from the shape.
                        string shapeText = string.Empty;
                        if (shape.Text != null && shape.Text.Value != null)
                        {
                            shapeText = shape.Text.Value.Text ?? string.Empty;
                        }

                        // Escape double quotes by doubling them.
                        shapeName = shapeName.Replace("\"", "\"\"");
                        shapeText = shapeText.Replace("\"", "\"\"");

                        // Write a CSV line. Enclose fields that may contain commas in double quotes.
                        writer.WriteLine($"{pageIndex},\"{shapeId}\",\"{shapeName}\",\"{shapeText}\"");
                    }
                }
            }
        }
    }