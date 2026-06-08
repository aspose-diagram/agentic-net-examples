using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder containing the Visio files.
            string inputFolder;
            if (args.Length > 0)
            {
                inputFolder = args[0];
            }
            else
            {
                Console.Write("Enter the full path to the folder with Visio files: ");
                inputFolder = Console.ReadLine();
            }

            if (string.IsNullOrWhiteSpace(inputFolder) || !Directory.Exists(inputFolder))
            {
                Console.WriteLine("Invalid folder path. Exiting.");
                return;
            }

            // Define old‑to‑new formula mappings.
            var formulaReplacements = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                // Example mappings – adjust as needed for the new standards.
                { "Width*Height", "Width*Height*0.9" },
                { "SUM(Prop.Row1,Prop.Row2)", "SUM(Prop.Row1,Prop.Row2)*1.1" }
            };

            // Process each .vsdx file in the folder.
            string[] visioFiles = Directory.GetFiles(inputFolder, "*.vsdx", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                try
                {
                    Console.WriteLine($"Processing file: {Path.GetFileName(filePath)}");

                    // Load the diagram.
                    Diagram diagram = new Diagram(filePath);

                    // Update field formulas throughout the diagram.
                    UpdateFieldFormulas(diagram, formulaReplacements);

                    // Save the updated diagram (overwrite original).
                    diagram.Save(filePath, SaveFileFormat.Vsdx);

                    Console.WriteLine("Successfully updated and saved.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing '{Path.GetFileName(filePath)}': {ex.Message}");
                }
            }

            Console.WriteLine("Bulk update operation completed.");
        }

        /// <summary>
        /// Iterates over all pages, shapes, and fields in the diagram,
        /// replacing field formulas according to the provided mapping.
        /// </summary>
        /// <param name="diagram">The diagram to modify.</param>
        /// <param name="replacements">Dictionary where key = old formula, value = new formula.</param>
        private static void UpdateFieldFormulas(Diagram diagram, Dictionary<string, string> replacements)
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape actually contains fields.
                    if (shape.Fields == null || shape.Fields.Count == 0)
                        continue;

                    foreach (Field field in shape.Fields)
                    {
                        // The current formula string is stored in field.Value.Ufev.F.
                        string currentFormula = field.Value.Ufev.F;

                        if (string.IsNullOrWhiteSpace(currentFormula))
                            continue;

                        // Check if the current formula matches any key in the replacement map.
                        if (replacements.TryGetValue(currentFormula, out string newFormula))
                        {
                            // Update the formula.
                            field.Value.Ufev.F = newFormula;
                            Console.WriteLine($"Updated field in shape ID {shape.ID} on page '{page.Name}' from '{currentFormula}' to '{newFormula}'.");
                        }
                    }
                }
            }
        }
    }