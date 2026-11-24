using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    /// <summary>
    /// Applies a preset theme to the specified shape.
    /// </summary>
    /// <param name="shape">The shape to which the theme will be applied.</param>
    /// <param name="themeName">The name of the preset theme (e.g., "Bubble", "Clouds").</param>
    /// <returns>True if the theme was applied successfully; otherwise, false.</returns>
    static bool ApplyThemeToShape(Shape shape, string themeName)
    {
        // Validate the shape reference and ensure it is not deleted.
        if (shape == null)
        {
            Console.Error.WriteLine("Shape reference is null.");
            return false;
        }

        if (shape.Del == BOOL.True)
        {
            Console.Error.WriteLine("Cannot apply a theme to a deleted shape.");
            return false;
        }

        // Try to parse the theme name to the corresponding PresetThemeValue enum.
        if (Enum.TryParse<PresetThemeValue>(themeName, ignoreCase: true, out var presetTheme))
        {
            try
            {
                // Apply the preset theme. The property is write‑only.
                shape.PresetTheme = presetTheme;
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error applying theme: {ex.Message}");
                return false;
            }
        }
        else
        {
            Console.Error.WriteLine($"Invalid theme name '{themeName}'.");
            return false;
        }
    }

    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and theme name.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <themeName>");
            return;
        }

        string diagramPath = args[0];
        string themeName = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the diagram has at least one page and one shape.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            if (diagram.Pages[0].Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the first page.
            Shape shape = diagram.Pages[0].Shapes[0];

            // Apply the requested theme to the shape.
            bool success = ApplyThemeToShape(shape, themeName);
            if (!success)
            {
                Console.Error.WriteLine("Failed to apply the theme.");
                return;
            }

            // Prepare output path (original name with "_themed" suffix).
            string directory = Path.GetDirectoryName(diagramPath);
            string filenameWithoutExt = Path.GetFileNameWithoutExtension(diagramPath);
            string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_themed.vsdx");

            // Save the modified diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Theme applied successfully. Saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}