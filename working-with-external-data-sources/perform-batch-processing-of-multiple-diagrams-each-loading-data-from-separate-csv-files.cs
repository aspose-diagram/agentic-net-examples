using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input folder containing diagram files (.vsdx) and CSV files.
                string inputFolder = @"C:\Diagrams\Input";
                // Output folder for processed diagrams.
                string outputFolder = @"C:\Diagrams\Output";

                // Ensure output folder exists.
                if (!Directory.Exists(outputFolder))
                    Directory.CreateDirectory(outputFolder);

                // Get all diagram files in the input folder.
                string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx");

                foreach (string diagramPath in diagramFiles)
                {
                    try
                    {
                        // Load the diagram.
                        Diagram diagram = new Diagram(diagramPath);

                        // Determine corresponding CSV file (same file name, .csv extension).
                        string csvPath = Path.ChangeExtension(diagramPath, ".csv");
                        if (!File.Exists(csvPath))
                        {
                            Console.WriteLine($"CSV file not found for diagram: {Path.GetFileName(diagramPath)}. Skipping.");
                            continue;
                        }

                        // Read CSV data.
                        List<string[]> csvRows = new List<string[]>();
                        using (var reader = new StreamReader(csvPath))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                // Simple split on commas; does not handle quoted commas.
                                string[] columns = line.Split(',');
                                csvRows.Add(columns);
                            }
                        }

                        // Example processing: update the first shape on the first page with the first CSV row (if any).
                        if (csvRows.Count > 0 && diagram.Pages.Count > 0)
                        {
                            Page firstPage = diagram.Pages[0];
                            // Retrieve the first shape (ID 1 is usually the background; find a visible shape).
                            Shape targetShape = null;
                            foreach (Shape shape in firstPage.Shapes)
                            {
                                if (shape.Del == BOOL.False && shape.OneD == false)
                                {
                                    targetShape = shape;
                                    break;
                                }
                            }

                            if (targetShape != null)
                            {
                                // Clear existing text.
                                targetShape.Text.Value.Clear();

                                // Concatenate CSV columns into a single line of text.
                                string newText = string.Join(" ", csvRows[0]);
                                targetShape.Text.Value.Add(new Txt(newText));
                            }
                        }

                        // Save the processed diagram to the output folder with the same file name.
                        string outputPath = Path.Combine(outputFolder, Path.GetFileName(diagramPath));
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);

                        Console.WriteLine($"Processed and saved diagram: {Path.GetFileName(outputPath)}");
                    }
                    catch (Exception ex)
                    {
                        // Log any errors for the current diagram.
                        Console.WriteLine($"Error processing diagram '{Path.GetFileName(diagramPath)}': {ex.Message}");
                    }
                }

                Console.WriteLine("Batch processing completed.");

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }