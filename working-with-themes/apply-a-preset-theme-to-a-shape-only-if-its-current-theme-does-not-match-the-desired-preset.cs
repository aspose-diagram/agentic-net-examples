using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <input.vsdx> <output.vsdx> <themeName>");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];
        string themeName = args[2];

        PresetThemeValue desiredTheme;
        switch (themeName.Trim().ToLower())
        {
            case "bubble":
                desiredTheme = PresetThemeValue.Bubble;
                break;
            // Add more mappings here if needed
            default:
                Console.Error.WriteLine($"Unsupported theme name: {themeName}");
                return;
        }

        try
        {
            Diagram diagram = new Diagram(inputPath);

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    bool alreadyHasTheme = false;
                    foreach (Prop prop in shape.Props)
                    {
                        if (prop.Name == "AppliedTheme" && prop.Value.Val == themeName)
                        {
                            alreadyHasTheme = true;
                            break;
                        }
                    }

                    if (alreadyHasTheme)
                        continue;

                    // Apply the preset theme
                    shape.PresetTheme = desiredTheme;
                    shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                    // Record that the theme was applied
                    bool propFound = false;
                    foreach (Prop prop in shape.Props)
                    {
                        if (prop.Name == "AppliedTheme")
                        {
                            prop.Value.Val = themeName;
                            propFound = true;
                            break;
                        }
                    }

                    if (!propFound)
                    {
                        Prop newProp = new Prop();
                        newProp.Name = "AppliedTheme";
                        newProp.Value.Val = themeName;
                        shape.Props.Add(newProp);
                    }
                }
            }

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}