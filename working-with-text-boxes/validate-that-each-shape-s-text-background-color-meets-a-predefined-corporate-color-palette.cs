using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input Visio file path (first argument or default).
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Define the corporate-approved text background colors.
        // Use the same format as stored in the diagram (hex or RGB string).
        string[] corporatePalette = new string[]
        {
            "#FF0000",          // Red
            "#00FF00",          // Green
            "#0000FF",          // Blue
            "RGB(95,108,53)"    // Example corporate color
        };

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                    {
                        continue;
                    }

                    // Retrieve the text background color value.
                    // The property may be empty if no background is set.
                    string bgColor = shape.TextBlock.TextBkgnd.Value;

                    // If there is no background color defined, treat it as invalid.
                    if (string.IsNullOrWhiteSpace(bgColor))
                    {
                        Console.WriteLine($"Shape ID {shape.ID} on page \"{page.Name}\" has no text background color defined.");
                        continue;
                    }

                    // Check whether the background color matches any corporate palette entry.
                    bool isValid = false;
                    foreach (string allowedColor in corporatePalette)
                    {
                        if (bgColor.Equals(allowedColor, StringComparison.OrdinalIgnoreCase))
                        {
                            isValid = true;
                            break;
                        }
                    }

                    // Report shapes with invalid background colors.
                    if (!isValid)
                    {
                        Console.WriteLine($"Shape ID {shape.ID} on page \"{page.Name}\" has invalid text background color: {bgColor}");
                    }
                }
            }

            Console.WriteLine("Validation completed.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}