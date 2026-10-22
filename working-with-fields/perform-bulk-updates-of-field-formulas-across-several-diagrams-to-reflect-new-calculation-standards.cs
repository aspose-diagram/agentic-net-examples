using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the directory path containing diagram files.");
            return;
        }

        string inputDirectory = args[0];

        // Verify the directory exists.
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Directory does not exist: {inputDirectory}");
            return;
        }

        // Get all Visio diagram files (VSDX) in the directory.
        string[] diagramFiles = Directory.GetFiles(inputDirectory, "*.vsdx", SearchOption.TopDirectoryOnly);

        if (diagramFiles.Length == 0)
        {
            Console.Error.WriteLine("No .vsdx files found in the specified directory.");
            return;
        }

        // Process each diagram file.
        foreach (string diagramPath in diagramFiles)
        {
            // Guard against missing file (should not happen after GetFiles, but added per rules).
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
                    // Iterate through all pages.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Ensure the shape has fields before processing.
                            if (shape.Fields == null || shape.Fields.Count == 0)
                                continue;

                            // Iterate over each field in the shape.
                            foreach (Field field in shape.Fields)
                            {
                                // Read the existing formula.
                                string existingFormula = field.Value.Ufev.F ?? string.Empty;

                                // Transform the formula according to new standards.
                                // Example transformation: replace "OldCalc" with "NewCalc".
                                // Adjust this logic to match actual calculation standards.
                                string updatedFormula = TransformFormula(existingFormula);

                                // If the formula has changed, apply the update.
                                if (!string.Equals(existingFormula, updatedFormula, StringComparison.Ordinal))
                                {
                                    // Update the formula string.
                                    field.Value.Ufev.F = updatedFormula;

                                    // Optionally update the displayed value to match the new formula.
                                    field.Value.Val = updatedFormula;

                                    // Ensure the unit is set to undefined (standard practice).
                                    field.Value.Ufev.Unit = MeasureConst.Undefined;
                                }
                            }
                        }
                    }

                    // Prepare output path by appending "_updated" before the extension.
                    string directory = Path.GetDirectoryName(diagramPath);
                    string filenameWithoutExt = Path.GetFileNameWithoutExtension(diagramPath);
                    string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_updated.vsdx");

                    // Save the modified diagram.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of the current diagram.
                Console.Error.WriteLine($"Error processing file '{diagramPath}': {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Transforms an existing formula string to the new calculation standard.
    /// Replace this placeholder logic with the actual transformation rules.
    /// </summary>
    /// <param name="formula">The original formula.</param>
    /// <returns>The updated formula.</returns>
    private static string TransformFormula(string formula)
    {
        // Example rule: replace "OldCalc" with "NewCalc".
        // Add more sophisticated parsing/replacement as needed.
        if (string.IsNullOrEmpty(formula))
            return formula;

        return formula.Replace("OldCalc", "NewCalc");
    }
}