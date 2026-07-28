using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Define input directories
            string diagramsFolder = @"C:\Diagrams";
            string csvFolder = @"C:\CsvData";
            string outputFolder = @"C:\ProcessedDiagrams";

            // Ensure output directory exists
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // Get all Visio files (e.g., .vsdx) in the diagrams folder
            string[] diagramFiles = Directory.GetFiles(diagramsFolder, "*.vsdx");

            foreach (string diagramPath in diagramFiles)
            {
                try
                {
                    // Determine corresponding CSV file (same base name)
                    string baseName = Path.GetFileNameWithoutExtension(diagramPath);
                    string csvPath = Path.Combine(csvFolder, baseName + ".csv");

                    if (!File.Exists(csvPath))
                    {
                        Console.WriteLine($"CSV file not found for diagram '{baseName}'. Skipping.");
                        continue;
                    }

                    // Load the diagram
                    Diagram diagram = new Diagram(diagramPath);

                    // Read CSV content (use first line as example data)
                    string[] csvLines = File.ReadAllLines(csvPath);
                    string csvData = csvLines.Length > 0 ? csvLines[0] : string.Empty;

                    // Update shape named "DataPlaceholder" on each page with CSV data
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            if (shape.NameU != null && shape.NameU.Equals("DataPlaceholder", StringComparison.OrdinalIgnoreCase))
                            {
                                // Clear existing text and add new text from CSV
                                shape.Text.Value.Clear();
                                shape.Text.Value.Add(new Txt(csvData));
                            }
                        }
                    }

                    // Save the updated diagram as PDF
                    string outputPath = Path.Combine(outputFolder, baseName + ".pdf");
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.DefaultFont = "Arial";
                    diagram.Save(outputPath, pdfOptions);

                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing diagram '{diagramPath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }