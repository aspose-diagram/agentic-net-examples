using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect exactly four arguments: input diagram path, output diagram path, shape ID, desired theme name
        if (args.Length != 4)
        {
            Console.Error.WriteLine("Usage: <inputPath> <outputPath> <shapeId> <desiredTheme>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string shapeIdArg = args[2];
        string desiredThemeName = args[3];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: parse shape ID
        if (!int.TryParse(shapeIdArg, out int shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        // Guard: parse desired theme enum (case‑insensitive)
        if (!Enum.TryParse<PresetThemeValue>(desiredThemeName, true, out var desiredTheme))
        {
            Console.Error.WriteLine($"Invalid theme name: {desiredThemeName}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page
                Page page = diagram.Pages[0];

                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on the first page.");
                    return;
                }

                // Determine whether the shape already has the desired theme recorded in a custom property
                bool themeAlreadyApplied = false;
                if (shape.Props != null)
                {
                    foreach (Prop prop in shape.Props)
                    {
                        if (prop.Name == "AppliedTheme")
                        {
                            // Compare the stored value (string) with the desired theme name
                            if (prop.Value != null && prop.Value.Val == desiredTheme.ToString())
                            {
                                themeAlreadyApplied = true;
                            }
                            break;
                        }
                    }
                }

                if (themeAlreadyApplied)
                {
                    Console.WriteLine($"Shape ID {shapeId} already has the theme '{desiredTheme}'. No changes made.");
                }
                else
                {
                    // Apply the desired preset theme to the shape (write‑only property)
                    shape.PresetTheme = desiredTheme;

                    // Update or add the custom property to record the applied theme
                    bool propFound = false;
                    if (shape.Props != null)
                    {
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == "AppliedTheme")
                            {
                                // Ensure the Value cell exists before assigning
                                if (prop.Value != null)
                                {
                                    prop.Value.Val = desiredTheme.ToString();
                                }
                                // Ensure the Label cell exists before assigning
                                if (prop.Label != null)
                                {
                                    prop.Label.Value = "Applied Theme";
                                }
                                propFound = true;
                                break;
                            }
                        }
                    }

                    if (!propFound)
                    {
                        // Create a new custom property to store the applied theme
                        Prop newProp = new Prop();
                        newProp.Name = "AppliedTheme";
                        // The constructor initializes Value and Label, so we can set their contents directly
                        newProp.Value.Val = desiredTheme.ToString();
                        newProp.Label.Value = "Applied Theme";
                        shape.Props.Add(newProp);
                    }

                    Console.WriteLine($"Applied theme '{desiredTheme}' to shape ID {shapeId}.");
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any Aspose or runtime errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
