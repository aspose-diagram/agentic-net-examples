using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioToSvgBatch
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputFile = @"C:\Visio\source.vsdx";

            // Folder where individual SVG files will be stored
            string outputFolder = @"C:\Visio\SvgExport";

            // Load the Visio diagram using the Diagram(string) constructor (load rule)
            Diagram diagram = new Diagram(inputFile);

            // Ensure the output directory exists
            Directory.CreateDirectory(outputFolder);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // ---- Embed metadata into the shape ----
                    // Example: prepend a custom tag to the shape's NameU property
                    // (In a real scenario you might add a Prop row to the ShapeSheet)
                    shape.NameU = $"[Converted]{shape.NameU}";

                    // ---- Export the shape to an SVG file ----
                    // Build a unique file name for the shape SVG
                    string svgFileName = Path.Combine(
                        outputFolder,
                        $"Page{page.ID}_Shape{shape.ID}.svg");

                    // Create ImageSaveOptions specifying SVG format
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Svg);

                    // Use Shape.ToImage (image conversion rule) to save the shape as SVG
                    shape.ToImage(svgFileName, saveOptions);
                }
            }

            // Optionally, save the modified diagram (metadata embedded) back to disk
            string modifiedDiagramPath = Path.Combine(outputFolder, "source_modified.vsdx");
            diagram.Save(modifiedDiagramPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
