using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DiagramPreview
{
    static void Main()
    {
        try
        {

            // Path to the source Visio diagram
            string sourceFile = "input.vsdx";

            // Path for the generated preview image
            string previewFile = "preview.png";

            // Font that should be applied to all text captions
            string userFont = "Arial";

            // Load the diagram from file
            using (Diagram diagram = new Diagram(sourceFile))
            {
                // Create rendering options for PNG output
                RenderingSaveOptions renderOptions = (RenderingSaveOptions)SaveOptions.CreateSaveOptions(SaveFileFormat.Png);

                // Apply the user‑specified font to all text elements
                renderOptions.DefaultFont = userFont;

                // Ensure the format is set to PNG
                renderOptions.SaveFormat = SaveFileFormat.Png;

                // Save the diagram as an image preview using the rendering options
                diagram.Save(previewFile, renderOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
