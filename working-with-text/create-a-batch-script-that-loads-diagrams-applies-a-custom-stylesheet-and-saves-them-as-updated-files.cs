using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Paths – adjust as needed
            string inputFolder = @"C:\Diagrams\Input";
            string outputFolder = @"C:\Diagrams\Output";
            string styleSheetPath = @"C:\Diagrams\CustomStyle.vss";

            // Ensure the output directory exists
            Directory.CreateDirectory(outputFolder);

            // Load the custom stylesheet (stored as a Visio stencil)
            Diagram styleDiagram = new Diagram(styleSheetPath, LoadFileFormat.Vss);

            // Process each Visio file in the input folder
            foreach (string filePath in Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly))
            {
                string ext = Path.GetExtension(filePath).ToLowerInvariant();
                if (ext != ".vsdx" && ext != ".vsd" && ext != ".vdx")
                    continue; // Skip unsupported files

                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Apply the custom stylesheet by copying each stylesheet from the stencil
                foreach (var styleSheet in styleDiagram.StyleSheets)
                {
                    diagram.StyleSheets.Add(styleSheet);
                }

                // Determine the save format based on the original file extension
                SaveFileFormat saveFormat = GetSaveFormatFromExtension(ext);
                DiagramSaveOptions saveOptions = new DiagramSaveOptions(saveFormat);

                // Build the output file path
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string outputPath = Path.Combine(outputFolder, fileName + ext);

                // Save the updated diagram
                diagram.Save(outputPath, saveOptions);

                // Clean up
                diagram.Dispose();
            }

            // Clean up the stylesheet diagram
            styleDiagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper to map file extensions to Aspose.Diagram.SaveFileFormat values
    static SaveFileFormat GetSaveFormatFromExtension(string ext)
    {
        switch (ext)
        {
            case ".vsdx":
                return SaveFileFormat.Vsdx;
            case ".vsd":
                return SaveFileFormat.Vsd;
            case ".vdx":
                return SaveFileFormat.Vdx;
            default:
                return SaveFileFormat.Vdx;
        }
    }
}
