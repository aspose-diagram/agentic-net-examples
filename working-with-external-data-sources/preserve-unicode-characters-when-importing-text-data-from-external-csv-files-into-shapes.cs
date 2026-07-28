using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Prompt user for input diagram path
            Console.Write("Enter the path to the Visio diagram file (e.g., diagram.vsdx): ");
            string diagramPath = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(diagramPath) || !File.Exists(diagramPath))
                throw new Exception("Diagram file not found.");

            // Prompt user for CSV file path
            Console.Write("Enter the path to the CSV file containing shape text data: ");
            string csvPath = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(csvPath) || !File.Exists(csvPath))
                throw new Exception("CSV file not found.");

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Read CSV lines using UTF-8 encoding to preserve Unicode characters
            using (var reader = new StreamReader(csvPath, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Skip empty lines
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Simple CSV split on first comma (shape identifier, text)
                    int commaIndex = line.IndexOf(',');
                    if (commaIndex < 0)
                        continue; // Invalid line format

                    string shapeIdentifier = line.Substring(0, commaIndex).Trim();
                    string shapeText = line.Substring(commaIndex + 1).Trim();

                    // Find the shape by NameU (case‑insensitive)
                    Shape targetShape = null;
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            if (string.Equals(shape.NameU, shapeIdentifier, StringComparison.OrdinalIgnoreCase))
                            {
                                targetShape = shape;
                                break;
                            }
                        }
                        if (targetShape != null)
                            break;
                    }

                    if (targetShape == null)
                    {
                        Console.WriteLine($"Warning: Shape \"{shapeIdentifier}\" not found.");
                        continue;
                    }

                    // Replace the shape's text with the Unicode string from CSV
                    targetShape.Text.Value.Clear();
                    targetShape.Text.Value.Add(new Txt(shapeText));
                }
            }

            // Prepare save options with a Unicode‑capable default font
            var saveOptions = new DiagramSaveOptions(SaveFileFormat.Vsdx);
            saveOptions.DefaultFont = "Arial Unicode MS";

            // Prompt for output path
            Console.Write("Enter the output path for the updated diagram (e.g., updated.vsdx): ");
            string outputPath = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(outputPath))
                throw new Exception("Output path is required.");

            // Save the diagram with the specified options
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine("Diagram saved successfully with Unicode text preserved.");
        }
    }