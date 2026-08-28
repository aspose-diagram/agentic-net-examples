using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Folder containing the source Visio diagrams
            string diagramsFolder = "Diagrams";
            // Folder containing CSV files with the same base names as the diagrams
            string csvFolder = "CsvData";
            // Folder where the processed diagrams will be saved
            string outputFolder = "Output";

            // Ensure the output directory exists
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Get all Visio files (e.g., .vsdx) in the diagrams folder
            string[] diagramFiles = Directory.GetFiles(diagramsFolder, "*.vsdx");

            foreach (string diagramPath in diagramFiles)
            {
                // Determine the matching CSV file based on the diagram file name
                string baseName = Path.GetFileNameWithoutExtension(diagramPath);
                string csvPath = Path.Combine(csvFolder, baseName + ".csv");

                if (!File.Exists(csvPath))
                {
                    Console.WriteLine($"CSV file not found for diagram '{baseName}'. Skipping.");
                    continue;
                }

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                try
                {
                    // Read all lines from the CSV file
                    string[] csvLines = File.ReadAllLines(csvPath);
                    if (csvLines.Length == 0)
                    {
                        Console.WriteLine($"CSV file '{csvPath}' is empty. Skipping.");
                        continue;
                    }

                    // Use the first line of the CSV as the new text for the first shape
                    string newText = csvLines[0];

                    // Access the first page of the diagram
                    Page page = diagram.Pages[0];

                    // Find the first shape on the page
                    Aspose.Diagram.Shape firstShape = null;
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        firstShape = shape;
                        break;
                    }

                    if (firstShape != null)
                    {
                        // Replace the shape's text
                        firstShape.Text.Value.Clear();
                        firstShape.Text.Value.Add(new Txt(newText));
                        Console.WriteLine($"Updated shape ID {firstShape.ID} in diagram '{baseName}'.");
                    }

                    // Save the updated diagram to the output folder
                    string outputPath = Path.Combine(outputFolder, baseName + "_updated.vsdx");
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Saved updated diagram to '{outputPath}'.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing diagram '{diagramPath}': {ex.Message}");
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
