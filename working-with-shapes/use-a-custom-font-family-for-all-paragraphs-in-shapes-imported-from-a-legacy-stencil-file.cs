using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <stencilPath> <outputPath> <customFontName>
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <stencilPath> <outputPath> <customFontName>");
            return;
        }

        string stencilPath = args[0];
        string outputPath = args[1];
        string customFontName = args[2];

        // Guard: ensure the stencil file exists
        if (!File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"Stencil file not found: {stencilPath}");
            return;
        }

        // Guard: ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Verify that the requested custom font is installed on the system
            InstalledFontCollection fontCollection = new InstalledFontCollection();
            bool fontFound = false;
            foreach (var font in fontCollection.Families)
            {
                if (string.Equals(font.Name, customFontName, StringComparison.OrdinalIgnoreCase))
                {
                    fontFound = true;
                    break;
                }
            }

            if (!fontFound)
            {
                Console.Error.WriteLine($"Warning: Font \"{customFontName}\" is not installed on the system. It may be substituted during rendering.");
            }

            // Set the default font for Aspose.Diagram (fallback handling)
            FontConfigs.DefaultFontName = customFontName;

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Load the legacy stencil (master) from the provided file stream and add it to the diagram
            using (FileStream stencilStream = new FileStream(stencilPath, FileMode.Open, FileAccess.Read))
            {
                string masterName = "LegacyMaster";
                diagram.AddMaster(stencilStream, masterName);
            }

            // Ensure there is at least one page to place shapes on
            if (diagram.Pages.Count == 0)
            {
                diagram.Pages.Add(new Page());
            }

            // Use the first page
            Page page = diagram.Pages[0];

            // Add a shape based on the imported master
            // Position (1,1) with size (2,1) – adjust as needed
            double posX = 1.0;
            double posY = 1.0;
            double width = 2.0;
            double height = 1.0;
            long shapeId = page.AddShape(posX, posY, width, height, "LegacyMaster");

            // Retrieve the shape instance
            Shape shape = page.Shapes.GetShape(shapeId);

            // Apply the custom font to every character run in all shapes on the page
            foreach (Shape s in page.Shapes)
            {
                // Skip deleted shapes
                if (s.Del == BOOL.True)
                    continue;

                for (int c = 0; c < s.Chars.Count; c++)
                {
                    // Set the FontName cell value to the custom font
                    s.Chars[c].FontName.Value = customFontName;
                }
            }

            // Save the resulting diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or IO exceptions and report them
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}