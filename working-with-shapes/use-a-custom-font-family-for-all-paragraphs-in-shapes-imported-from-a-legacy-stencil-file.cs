using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        string stencilPath = "legacyStencil.vss";
        if (!File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"File not found: {stencilPath}");
            return;
        }

        string outputPath = "output.vsdx";

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Import a master shape from the legacy stencil.
            diagram.AddMaster(stencilPath, "Rectangle");

            // Add a shape based on the imported master to the first page
            Page page = diagram.Pages[0];
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle");
            Shape shape = page.Shapes.GetShape(shapeId);

            // Define the custom font family to apply
            string customFontFamily = "MyCustomFont";

            // Apply the custom font to all characters of the newly added shape
            foreach (Aspose.Diagram.Char ch in shape.Chars)
            {
                ch.FontName.Value = customFontFamily;
            }

            // Additionally, apply the custom font to all existing shapes in the diagram
            foreach (Page pg in diagram.Pages)
            {
                foreach (Shape shp in pg.Shapes)
                {
                    foreach (Aspose.Diagram.Char ch in shp.Chars)
                    {
                        ch.FontName.Value = customFontFamily;
                    }
                }
            }

            // Save the diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}