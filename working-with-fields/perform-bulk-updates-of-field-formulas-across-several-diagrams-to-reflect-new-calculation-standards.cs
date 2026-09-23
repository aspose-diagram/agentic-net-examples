using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        // Example mapping of old formulas to new formulas.
        // In a real scenario this could be loaded from a config file.
        private static readonly System.Collections.Generic.Dictionary<string, string> FormulaMappings = new System.Collections.Generic.Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Width*Height", "Area()" },
            { "Length+Width", "Perimeter()" }
            // Add more mappings as needed.
        };

        static void Main(string[] args)
        {
            // Folder containing Visio files to process.
            string inputFolder = @"C:\VisioFiles";
            // Folder to save updated files.
            string outputFolder = @"C:\VisioFiles\Updated";

            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Process all VSDX files in the input folder.
            string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx", SearchOption.TopDirectoryOnly);

            foreach (string filePath in diagramFiles)
            {
                try
                {
                    Console.WriteLine($"Loading diagram: {Path.GetFileName(filePath)}");
                    Diagram diagram = new Diagram(filePath);

                    // Iterate through each page explicitly typed.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Ensure the shape has fields to update.
                            if (shape.Fields != null && shape.Fields.Count > 0)
                            {
                                // Iterate through each field.
                                foreach (Field field in shape.Fields)
                                {
                                    // The formula is stored in field.Value.Ufev.F.
                                    string currentFormula = field.Value.Ufev.F ?? string.Empty;

                                    // Check if the current formula matches any mapping.
                                    foreach (var mapping in FormulaMappings)
                                    {
                                        if (currentFormula.Equals(mapping.Key, StringComparison.OrdinalIgnoreCase))
                                        {
                                            Console.WriteLine($"Updating field formula on shape ID {shape.ID} from '{currentFormula}' to '{mapping.Value}'");
                                            field.Value.Ufev.F = mapping.Value;
                                            // Optionally clear the displayed value if needed.
                                            field.Value.Val = string.Empty;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // Save the updated diagram to the output folder.
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Saved updated diagram to: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Bulk update operation completed.");
        }
    }