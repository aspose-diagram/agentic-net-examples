using System.IO;
using System;
using Aspose.Diagram;

class CloneShapeProtection
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (or specify the required page index/name)
            Page page = diagram.Pages[0];

            // Identify the source shape (the shape whose protection settings will be copied)
            // Here we assume the shape is identified by its name; adjust as needed (e.g., by ID)
            Shape sourceShape = null;
            foreach (Shape shp in page.Shapes)
            {
                if (shp.Name == "SourceShapeName") // replace with actual source shape name
                {
                    sourceShape = shp;
                    break;
                }
            }

            // Identify the target shape (the shape that will receive the protection settings)
            Shape targetShape = null;
            foreach (Shape shp in page.Shapes)
            {
                if (shp.Name == "TargetShapeName") // replace with actual target shape name
                {
                    targetShape = shp;
                    break;
                }
            }

            // Ensure both shapes were found before proceeding
            if (sourceShape == null || targetShape == null)
            {
                Console.WriteLine("Source or target shape not found.");
                return;
            }

            // Clone protection settings from source to target.
            // Shape.Copy copies all shape properties, including the Protection settings.
            targetShape.Copy(sourceShape);

            // Save the modified diagram (replace with desired output path and format)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
