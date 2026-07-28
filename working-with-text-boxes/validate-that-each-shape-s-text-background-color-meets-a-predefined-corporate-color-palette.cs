using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be validated
            string diagramPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Predefined corporate palette (hex color strings)
            string[] corporatePalette = new[]
            {
                "#FFFFFF", // White
                "#FF0000", // Red
                "#00FF00", // Green
                "#0000FF"  // Blue
            };

            bool allValid = true;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the text background color formula (if any)
                    string bgFormula = shape.TextBlock.TextBkgnd.Ufe.F?.Trim();

                    // If no background color is set, skip validation for this shape
                    if (string.IsNullOrEmpty(bgFormula))
                        continue;

                    // Compare the retrieved value against the corporate palette (case‑insensitive)
                    bool isAllowed = false;
                    foreach (string allowedColor in corporatePalette)
                    {
                        if (string.Equals(bgFormula, allowedColor, StringComparison.OrdinalIgnoreCase))
                        {
                            isAllowed = true;
                            break;
                        }
                    }

                    // Report any violations
                    if (!isAllowed)
                    {
                        allValid = false;
                        Console.WriteLine($"Invalid text background color on Shape ID {shape.ID} (Page: {page.Name}): {bgFormula}");
                    }
                }
            }

            // Final result
            if (allValid)
            {
                Console.WriteLine("All shape text background colors conform to the corporate palette.");
            }
            else
            {
                throw new Exception("One or more shapes have text background colors outside the corporate palette.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
