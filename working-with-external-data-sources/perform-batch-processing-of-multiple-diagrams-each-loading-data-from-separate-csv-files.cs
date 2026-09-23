using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Folder containing diagram files (e.g., .vsdx) and matching CSV files
            string diagramsFolder = @"C:\Diagrams";
            string outputFolder = @"C:\ProcessedDiagrams";

            // Ensure output folder exists
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // Get all diagram files in the folder
            string[] diagramFiles = Directory.GetFiles(diagramsFolder, "*.vsdx");

            foreach (string diagramPath in diagramFiles)
            {
                try
                {
                    // Derive CSV file name (same base name, .csv extension)
                    string csvPath = Path.ChangeExtension(diagramPath, ".csv");
                    if (!File.Exists(csvPath))
                    {
                        Console.WriteLine($"CSV file not found for diagram: {Path.GetFileName(diagramPath)}. Skipping.");
                        continue;
                    }

                    // Load the diagram
                    Diagram diagram = new Diagram(diagramPath);

                    // Read CSV data into a dictionary: ShapeName -> NewText
                    Dictionary<string, string> updates = LoadCsvUpdates(csvPath);

                    // Apply updates to shapes on each page
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Only process shapes that have a name and are not deleted
                            if (shape.Del == BOOL.True)
                                continue;

                            string shapeName = shape.NameU;
                            if (updates.TryGetValue(shapeName, out string newText))
                            {
                                // Clear existing text and add new text
                                shape.Text.Value.Clear();
                                shape.Text.Value.Add(new Txt(newText));
                            }
                        }
                    }

                    // Save the updated diagram to the output folder
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(diagramPath));
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{Path.GetFileName(diagramPath)}': {ex.Message}");
                }
            }
        }

        // Reads a CSV file where each line has: ShapeName,NewText
        // Returns a dictionary mapping shape names to the new text value.
        private static Dictionary<string, string> LoadCsvUpdates(string csvFilePath)
        {
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            using (var reader = new StreamReader(csvFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Skip empty lines
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Split by comma (basic CSV, no quoted commas handling)
                    string[] parts = line.Split(',');
                    if (parts.Length < 2)
                        continue; // Invalid line, ignore

                    string shapeName = parts[0].Trim();
                    string newText = parts[1].Trim();

                    if (!string.IsNullOrEmpty(shapeName))
                        dict[shapeName] = newText;
                }
            }
            return dict;
        }
    }