using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path to the Visio file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Collect all existing shape IDs across all pages.
            HashSet<long> existingShapeIds = new HashSet<long>();
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    existingShapeIds.Add(shape.ID);
                }
            }

            // Regular expression to extract numeric IDs from formulas.
            Regex idRegex = new Regex(@"\b\d+\b", RegexOptions.Compiled);

            bool anyInvalidReference = false;

            // Iterate again to validate EventCalc (or closest substitute) formulas.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Access the closest substitute for EventCalc: EventDrop formula.
                    // Guard against null references.
                    if (shape.Event == null || shape.Event.EventDrop == null || shape.Event.EventDrop.Ufe == null)
                        continue;

                    string formula = shape.Event.EventDrop.Ufe.F;

                    if (string.IsNullOrWhiteSpace(formula))
                        continue; // No formula to validate.

                    // Find all numeric tokens that could represent shape IDs.
                    MatchCollection matches = idRegex.Matches(formula);
                    foreach (Match match in matches)
                    {
                        if (long.TryParse(match.Value, out long referencedId))
                        {
                            if (!existingShapeIds.Contains(referencedId))
                            {
                                anyInvalidReference = true;
                                Console.WriteLine($"Warning: Shape ID {shape.ID} contains an EventCalc formula referencing non‑existent shape ID {referencedId}.");
                            }
                        }
                    }
                }
            }

            if (!anyInvalidReference)
            {
                Console.WriteLine("Validation completed: All EventCalc formulas reference existing shape IDs.");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}