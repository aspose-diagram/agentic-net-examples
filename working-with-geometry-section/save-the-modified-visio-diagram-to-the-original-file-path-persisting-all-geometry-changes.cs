using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioSaveExample
{
    static void Main()
    {
        try
        {

            // Path to the original Visio file (could be .vsdx, .vsd, .vdx, etc.)
            string originalFilePath = @"C:\Diagrams\MyDiagram.vsdx";

            // Load the diagram from the original file
            Diagram diagram = new Diagram(originalFilePath);

            // -------------------------------------------------
            // Example geometry modification (adjust as needed)
            // -------------------------------------------------
            if (diagram.Pages.Count > 0)
            {
                // Get the first page
                Page page = diagram.Pages[0];

                if (page.Shapes.Count > 0)
                {
                    // Get the first shape on the page
                    Shape shape = page.Shapes[0];

                    // Modify the shape's position (PinX, PinY) as an example
                    shape.XForm.PinX.Value = 5.0; // new X coordinate
                    shape.XForm.PinY.Value = 5.0; // new Y coordinate
                }
            }

            // Determine the appropriate SaveFileFormat based on the file extension
            SaveFileFormat saveFormat;
            string extension = System.IO.Path.GetExtension(originalFilePath).ToLowerInvariant();

            switch (extension)
            {
                case ".vsdx":
                    saveFormat = SaveFileFormat.Vsdx;
                    break;
                case ".vsd":
                    // .vsd is a binary format; Aspose.Diagram can save it using Vdx (XML) as fallback
                    saveFormat = SaveFileFormat.Vdx;
                    break;
                case ".vdx":
                    saveFormat = SaveFileFormat.Vdx;
                    break;
                case ".vsx":
                    saveFormat = SaveFileFormat.Vsx;
                    break;
                case ".vtx":
                    saveFormat = SaveFileFormat.Vtx;
                    break;
                default:
                    // Default to Vdx if the extension is unrecognized
                    saveFormat = SaveFileFormat.Vdx;
                    break;
            }

            // Save the modified diagram back to the original file path,
            // preserving all geometry changes.
            diagram.Save(originalFilePath, saveFormat);

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
