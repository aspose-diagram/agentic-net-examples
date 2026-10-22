using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: diagrams folder and CSV folder
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <DiagramsFolder> <CsvFolder>");
            return;
        }

        string diagramsFolder = args[0];
        string csvFolder = args[1];

        // Validate diagrams folder
        if (!Directory.Exists(diagramsFolder))
        {
            Console.Error.WriteLine($"Diagrams folder does not exist: {diagramsFolder}");
            return;
        }

        // Validate CSV folder
        if (!Directory.Exists(csvFolder))
        {
            Console.Error.WriteLine($"CSV folder does not exist: {csvFolder}");
            return;
        }

        // Collect .vsdx and .vsd files
        string[] vsdxFiles = Directory.GetFiles(diagramsFolder, "*.vsdx");
        string[] vsdFiles = Directory.GetFiles(diagramsFolder, "*.vsd");
        string[] allDiagramFiles = new string[vsdxFiles.Length + vsdFiles.Length];
        vsdxFiles.CopyTo(allDiagramFiles, 0);
        vsdFiles.CopyTo(allDiagramFiles, vsdxFiles.Length);

        foreach (string diagramPath in allDiagramFiles)
        {
            // Determine matching CSV file (same base name)
            string baseName = Path.GetFileNameWithoutExtension(diagramPath);
            string csvPath = Path.Combine(csvFolder, baseName + ".csv");

            if (!File.Exists(csvPath))
            {
                Console.Error.WriteLine($"CSV file not found for diagram '{baseName}'. Expected at: {csvPath}");
                continue;
            }

            // Guard diagram file existence (redundant but required)
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
                continue;
            }

            try
            {
                // Load diagram inside a using block as required
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Read all CSV lines
                    string[] csvLines = File.ReadAllLines(csvPath);
                    foreach (string line in csvLines)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                            continue; // Skip empty lines

                        // Expected format: ShapeId,NewText (comma may appear in text)
                        int firstComma = line.IndexOf(',');
                        if (firstComma <= 0)
                            continue; // Invalid line, skip

                        string idPart = line.Substring(0, firstComma).Trim();
                        string textPart = line.Substring(firstComma + 1).Trim();

                        if (!long.TryParse(idPart, out long shapeId))
                            continue; // Invalid shape ID, skip

                        bool shapeFound = false;

                        // Search all pages for the shape ID
                        foreach (Page page in diagram.Pages)
                        {
                            // GetShape expects an int; cast safely
                            Shape shape = page.Shapes.GetShape((int)shapeId);
                            if (shape != null)
                            {
                                // Clear existing text and add new text
                                shape.Text.Value.Clear();
                                shape.Text.Value.Add(new Txt(textPart));
                                shapeFound = true;
                                break; // Assuming unique IDs, stop after update
                            }
                        }

                        if (!shapeFound)
                        {
                            Console.Error.WriteLine($"Shape with ID {shapeId} not found in diagram '{baseName}'.");
                        }
                    }

                    // Save updated diagram with "_updated" suffix
                    string outputPath = Path.Combine(diagramsFolder, baseName + "_updated.vsdx");
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Processed and saved diagram: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors from Aspose or IO operations
                Console.Error.WriteLine($"Error processing diagram '{diagramPath}': {ex.Message}");
            }
        }
    }
}