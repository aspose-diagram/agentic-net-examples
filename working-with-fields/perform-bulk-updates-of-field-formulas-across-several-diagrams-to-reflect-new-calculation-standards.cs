using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Input folder containing Visio files
            string inputFolder = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
            // Output folder for updated files
            string outputFolder = args.Length > 1 ? args[1] : inputFolder;

            if (!Directory.Exists(inputFolder))
            {
                Console.WriteLine($"Input folder does not exist: {inputFolder}");
                return;
            }

            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Process all VSDX files in the input folder
            string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx");
            foreach (string filePath in diagramFiles)
            {
                try
                {
                    Console.WriteLine($"Processing file: {Path.GetFileName(filePath)}");
                    Diagram diagram = new Diagram(filePath);

                    UpdateFieldFormulas(diagram);

                    string outputPath = Path.Combine(outputFolder,
                        Path.GetFileNameWithoutExtension(filePath) + "_updated.vsdx");
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Saved updated diagram to: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Bulk update completed.");
        }

        // Updates field formulas according to new calculation standards
        private static void UpdateFieldFormulas(Diagram diagram)
        {
            // Define old-to-new formula mappings
            Dictionary<string, string> formulaReplacements = new Dictionary<string, string>
            {
                { "Width*Height", "Area" },
                { "OldFormula", "NewFormula" }
                // Add more mappings as needed
            };

            // Iterate through each page
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Iterate through each shape on the page
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Ensure the shape has fields to process
                    if (shape.Fields == null || shape.Fields.Count == 0)
                        continue;

                    // Iterate through each field in the shape
                    foreach (Aspose.Diagram.Field field in shape.Fields)
                    {
                        // Retrieve the current formula; skip if null or empty
                        string currentFormula = field.Value?.Ufev?.F;
                        if (string.IsNullOrWhiteSpace(currentFormula))
                            continue;

                        string updatedFormula = currentFormula;

                        // Apply all defined replacements
                        foreach (KeyValuePair<string, string> kvp in formulaReplacements)
                        {
                            if (updatedFormula.Contains(kvp.Key))
                            {
                                updatedFormula = updatedFormula.Replace(kvp.Key, kvp.Value);
                            }
                        }

                        // If the formula changed, assign the new value
                        if (!updatedFormula.Equals(currentFormula, StringComparison.Ordinal))
                        {
                            field.Value.Ufev.F = updatedFormula;
                            // Reset unit to undefined to avoid unintended unit handling
                            field.Value.Ufev.Unit = MeasureConst.Undefined;
                            Console.WriteLine($"Updated field formula on shape ID {shape.ID} on page '{page.Name}'.");
                        }
                    }
                }
            }
        }
    }