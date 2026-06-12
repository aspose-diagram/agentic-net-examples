using System;
using Aspose.Diagram;

class Program
    {
        // Define the corporate palette as an array of allowed color strings.
        // Colors can be specified in HEX (e.g., "#FF0000") or RGB formula strings (e.g., "RGB(255,0,0)").
        static readonly string[] AllowedColors = new string[]
        {
            "#FF0000", // Red
            "#00FF00", // Green
            "#0000FF", // Blue
            "RGB(95,108,53)" // Example corporate RGB color
        };

        static void Main()
        {
            try
            {

                // Path to the Visio diagram file to be validated.
                // Replace with the actual file path as needed.
                string diagramPath = "input.vsdx";

                // Load the diagram.
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the text background color formula, if any.
                        // The TextBlock.TextBkgnd cell stores the background color as a formula string.
                        // Example values: "#FF0000" or "RGB(95,108,53)".
                        string bgColorFormula = shape.TextBlock?.TextBkgnd?.Ufe?.F;

                        // If the shape has no text background defined, skip validation.
                        if (string.IsNullOrWhiteSpace(bgColorFormula))
                        {
                            continue;
                        }

                        // Check if the background color matches one of the allowed corporate colors.
                        bool isAllowed = false;
                        foreach (string allowed in AllowedColors)
                        {
                            if (string.Equals(bgColorFormula.Trim(), allowed, StringComparison.OrdinalIgnoreCase))
                            {
                                isAllowed = true;
                                break;
                            }
                        }

                        // If the color is not allowed, report the issue.
                        if (!isAllowed)
                        {
                            string message = $"Shape ID {shape.ID} on page '{page.Name}' has an invalid text background color: {bgColorFormula}";
                            Console.WriteLine(message);
                            // Optionally, you can throw an exception to halt processing.
                            // throw new Exception(message);
                        }
                    }
                }

                Console.WriteLine("Validation completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }