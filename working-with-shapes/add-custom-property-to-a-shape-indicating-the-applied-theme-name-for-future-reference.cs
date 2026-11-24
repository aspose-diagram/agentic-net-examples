using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, output file, theme name
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <themeName>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string themeName = args[2];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page and one shape
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page firstPage = diagram.Pages[0];
            if (firstPage.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first non-deleted shape
            Shape targetShape = null;
            foreach (Shape shp in firstPage.Shapes)
            {
                if (shp.Del != BOOL.True)
                {
                    targetShape = shp;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("No visible shape found on the first page.");
                return;
            }

            // Add a custom property to store the applied theme name
            // Property name: "AppliedTheme", Label: "Applied Theme", Value: themeName
            Prop customProp = new Prop();
            customProp.Name = "AppliedTheme";
            customProp.Label.Value = "Applied Theme";
            customProp.Value.Val = themeName;
            customProp.Type.Value = TypePropValue.String;

            // Add the property to the shape's custom properties collection
            targetShape.Props.Add(customProp);

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}