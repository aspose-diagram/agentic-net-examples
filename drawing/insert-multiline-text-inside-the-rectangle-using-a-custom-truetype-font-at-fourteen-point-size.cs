using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define file and font paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";
        string fontFolder = @"C:\CustomFonts";
        string customFontName = "MyCustomFont";

        // Guard: check input diagram file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: check custom font folder exists
        if (!Directory.Exists(fontFolder))
        {
            Console.Error.WriteLine($"Font folder not found: {fontFolder}");
            return;
        }

        try
        {
            // Configure Aspose.Diagram to use the custom TrueType font folder
            // The second parameter indicates recursive search
            FontConfigs.SetFontFolder(fontFolder, true);
            // Optionally set a default font name (if supported)
            // FontConfigs.DefaultFontName = customFontName; // Uncomment if property exists

            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page
                // PinX and PinY define the center position; width and height are defined by the master shape
                double pinX = page.PageSheet.PageProps.PageWidth.Value / 2.0;
                double pinY = page.PageSheet.PageProps.PageHeight.Value / 2.0;
                long rectShapeId = page.AddShape(pinX, pinY, "Rectangle");

                // Retrieve the newly added rectangle shape
                Shape rectShape = page.Shapes.GetShape(rectShapeId);

                // Clear any existing text (optional)
                rectShape.Text.Value.Clear();

                // Insert multiline text using Txt objects
                rectShape.Text.Value.Add(new Txt("First line of text"));
                rectShape.Text.Value.Add(new Txt("Second line of text"));
                rectShape.Text.Value.Add(new Txt("Third line of text"));

                // Apply custom font and size to all characters in the shape
                // Font size is specified in inches (points / 72)
                double fontSizeInInches = 14.0 / 72.0;
                for (int i = 0; i < rectShape.Chars.Count; i++)
                {
                    rectShape.Chars[i].FontName.Value = customFontName;
                    rectShape.Chars[i].Size.Value = fontSizeInInches;
                }

                // Save the modified diagram to the output file in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram updated and saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}