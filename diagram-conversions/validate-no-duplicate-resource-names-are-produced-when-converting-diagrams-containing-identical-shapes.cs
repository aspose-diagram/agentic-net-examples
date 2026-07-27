using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class DiagramValidator
{
    // Validates that there are no duplicate shape names (NameU) in the given diagram.
    // Returns true if no duplicates are found; otherwise false.
    public static bool ValidateNoDuplicateShapeNames(Diagram diagram)
    {
        // Dictionary to track occurrence count of each shape name.
        var nameCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        // Iterate through all pages.
        foreach (Page page in diagram.Pages)
        {
            // Iterate through all shapes on the page.
            foreach (Shape shape in page.Shapes)
            {
                // Use the universal name (NameU) as the identifier.
                string shapeName = shape.NameU?.Trim() ?? string.Empty;

                if (string.IsNullOrEmpty(shapeName))
                    continue; // Skip unnamed shapes.

                if (nameCounts.ContainsKey(shapeName))
                    nameCounts[shapeName]++; // Increment count for duplicate.
                else
                    nameCounts[shapeName] = 1;
            }
        }

        // Check if any name appears more than once.
        foreach (var kvp in nameCounts)
        {
            if (kvp.Value > 1)
            {
                Console.WriteLine($"Duplicate shape name detected: '{kvp.Key}' appears {kvp.Value} times.");
                return false;
            }
        }

        // No duplicates found.
        return true;
    }

    // Example usage: load two diagrams, combine them, then validate.
    static void Main()
    {
        try
        {

            // Load first diagram.
            using (Diagram diagram1 = new Diagram("Diagram1.vsdx"))
            // Load second diagram.
            using (Diagram diagram2 = new Diagram("Diagram2.vsdx"))
            {
                // Combine the second diagram into the first.
                diagram1.Combine(diagram2);

                // Validate for duplicate shape names after combination.
                bool isValid = ValidateNoDuplicateShapeNames(diagram1);

                if (isValid)
                {
                    // Save the combined diagram if validation passes.
                    diagram1.Save("CombinedDiagram.vsdx", SaveFileFormat.Vsdx);
                    Console.WriteLine("Diagram combined and saved successfully. No duplicate shape names found.");
                }
                else
                {
                    Console.WriteLine("Diagram contains duplicate shape names. Resolve duplicates before saving.");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
