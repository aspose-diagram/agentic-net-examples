using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the diagram file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

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

            // Regular expression to extract numeric literals (potential shape IDs) from formulas.
            Regex numberRegex = new Regex(@"\b\d+\b", RegexOptions.Compiled);

            bool hasInvalidReferences = false;

            // Iterate through each shape and validate its event formulas.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the Event object is present.
                    if (shape.Event == null)
                        continue;

                    // Validate each supported event formula.
                    ValidateEventFormula(shape, page, "EventDrop", shape.Event.EventDrop?.Ufe?.F, numberRegex, existingShapeIds, ref hasInvalidReferences);
                    ValidateEventFormula(shape, page, "EventDblClick", shape.Event.EventDblClick?.Ufe?.F, numberRegex, existingShapeIds, ref hasInvalidReferences);
                    ValidateEventFormula(shape, page, "EventXFMod", shape.Event.EventXFMod?.Ufe?.F, numberRegex, existingShapeIds, ref hasInvalidReferences);
                    ValidateEventFormula(shape, page, "EventMultiDrop", shape.Event.EventMultiDrop?.Ufe?.F, numberRegex, existingShapeIds, ref hasInvalidReferences);
                    ValidateEventFormula(shape, page, "TheText", shape.Event.TheText?.Ufe?.F, numberRegex, existingShapeIds, ref hasInvalidReferences);
                    ValidateEventFormula(shape, page, "TheData", shape.Event.TheData?.Ufe?.F, numberRegex, existingShapeIds, ref hasInvalidReferences);
                }
            }

            if (!hasInvalidReferences)
            {
                Console.WriteLine("Validation completed: no invalid shape ID references found in event formulas.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Checks a single event formula for numeric IDs that do not correspond to existing shapes.
    /// </summary>
    static void ValidateEventFormula(
        Shape shape,
        Page page,
        string eventName,
        string formula,
        Regex numberRegex,
        HashSet<long> existingShapeIds,
        ref bool hasInvalidReferences)
    {
        // If the formula is null or empty, nothing to validate.
        if (string.IsNullOrEmpty(formula))
            return;

        // Find all numeric literals in the formula.
        MatchCollection matches = numberRegex.Matches(formula);
        foreach (Match match in matches)
        {
            if (long.TryParse(match.Value, out long referencedId))
            {
                if (!existingShapeIds.Contains(referencedId))
                {
                    hasInvalidReferences = true;
                    Console.WriteLine($"Invalid reference: Shape ID {shape.ID} on page '{page.Name}' has a non‑existent shape ID {referencedId} in {eventName} formula.");
                }
            }
        }
    }
}