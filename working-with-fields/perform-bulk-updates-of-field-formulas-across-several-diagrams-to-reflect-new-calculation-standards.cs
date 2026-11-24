using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder>");
            return;
        }

        string inputFolder = args[0];

        // Verify the input folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Define old‑to‑new formula mappings.
        var formulaMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "OldFormula1", "NewFormulaA" },
            { "OldFormula2", "NewFormulaB" }
            // Add additional mappings as required.
        };

        // Prepare the output folder for updated diagrams.
        string outputFolder = Path.Combine(inputFolder, "Updated");
        Directory.CreateDirectory(outputFolder);

        // Retrieve all Visio diagram files (VSDX) in the input folder.
        string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx");

        foreach (string diagramPath in diagramFiles)
        {
            // Guard against missing files (should not happen after GetFiles).
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"File not found: {diagramPath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required.
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Iterate through each page in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the current page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Ensure the shape has a Fields collection.
                            if (shape.Fields == null)
                                continue;

                            // Iterate through each field within the shape.
                            foreach (Field field in shape.Fields)
                            {
                                // Retrieve the current formula string.
                                string currentFormula = field.Value.Ufev.F;

                                if (string.IsNullOrEmpty(currentFormula))
                                    continue;

                                // Check each mapping for a possible replacement.
                                foreach (KeyValuePair<string, string> kvp in formulaMap)
                                {
                                    if (currentFormula.Contains(kvp.Key))
                                    {
                                        // Update the formula with the new standard.
                                        field.Value.Ufev.F = currentFormula.Replace(kvp.Key, kvp.Value);
                                        // Set unit to Undefined to avoid unintended units.
                                        field.Value.Ufev.Unit = MeasureConst.Undefined;
                                        // Exit after the first applicable replacement.
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    // Save the modified diagram to the output folder, preserving the original name.
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(diagramPath));
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Updated diagram saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing.
                Console.Error.WriteLine($"Error processing file '{diagramPath}': {ex.Message}");
            }
        }
    }
}