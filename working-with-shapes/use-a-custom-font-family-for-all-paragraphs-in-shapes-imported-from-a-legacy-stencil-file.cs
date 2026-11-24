using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <stencilFilePath> <outputDiagramPath> [customFontName]");
            return;
        }

        string stencilPath = args[0];
        string outputPath = args[1];
        string customFont = args.Length >= 3 ? args[2] : "Arial";

        // Guard: ensure stencil file exists
        if (!File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"Stencil file not found: {stencilPath}");
            return;
        }

        // Verify that the custom font is installed on the system using Aspose.Drawing.Text
        bool fontFound = false;
        try
        {
            InstalledFontCollection fontCollection = new InstalledFontCollection();
            foreach (var fontFamily in fontCollection.Families)
            {
                if (string.Equals(fontFamily.Name, customFont, StringComparison.OrdinalIgnoreCase))
                {
                    fontFound = true;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while enumerating installed fonts: {ex.Message}");
            // Continue; fallback font will be used.
        }

        if (!fontFound)
        {
            Console.Error.WriteLine($"Warning: Font \"{customFont}\" is not installed on the system. It will be used as fallback.");
        }

        // Configure Aspose.Diagram to use the custom font as default fallback
        FontConfigs.DefaultFontName = customFont;

        try
        {
            // Load the legacy stencil file into a stream
            using (FileStream stencilStream = new FileStream(stencilPath, FileMode.Open, FileAccess.Read))
            {
                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Derive a master name from the stencil file name
                string masterName = Path.GetFileNameWithoutExtension(stencilPath);

                // Import the master (stencil) into the diagram
                diagram.AddMaster(stencilStream, masterName);

                // Add a new page to host shapes
                Page page = new Page();
                diagram.Pages.Add(page);

                // Add a shape based on the imported master
                // Position (1,1) with size (2,2) – units are inches by default
                page.AddShape(1.0, 1.0, 2.0, 2.0, masterName);
            }

            // At this point, the diagram contains shapes imported from the legacy stencil.
            // Apply the custom font to all character runs in every shape across all pages.
            Diagram finalDiagram = new Diagram(); // Placeholder to hold the diagram after using block
            // Since the diagram variable is scoped inside the using block, we need to recreate loading.
            // Instead, we will load the diagram from the saved temporary file.
            // To avoid file I/O, we will restructure: move processing inside the using block.

        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram processing: {ex.Message}");
            return;
        }

        // Revised implementation: process within the same using block to retain diagram reference
        try
        {
            // Load the stencil and create diagram, then apply font and save
            using (FileStream stencilStream = new FileStream(stencilPath, FileMode.Open, FileAccess.Read))
            {
                Diagram diagram = new Diagram();

                string masterName = Path.GetFileNameWithoutExtension(stencilPath);
                diagram.AddMaster(stencilStream, masterName);

                Page page = new Page();

                diagram.Pages.Add(page);
                page.AddShape(1.0, 1.0, 2.0, 2.0, masterName);

                // Iterate over all pages and shapes to set the custom font on each character run
                foreach (Page pg in diagram.Pages)
                {
                    foreach (Shape shp in pg.Shapes)
                    {
                        // Skip deleted shapes
                        if (shp.Del == BOOL.True)
                            continue;

                        // Apply font to each character in the shape
                        for (int i = 0; i < shp.Chars.Count; i++)
                        {
                            shp.Chars[i].FontName.Value = customFont;
                        }
                    }
                }

                // Save the resulting diagram with the custom font applied
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram creation or saving: {ex.Message}");
            return;
        }

        Console.WriteLine($"Diagram saved successfully to: {outputPath}");
    }
}