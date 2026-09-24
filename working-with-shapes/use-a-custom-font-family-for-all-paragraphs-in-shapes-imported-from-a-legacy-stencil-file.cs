using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main()
    {
        try
        {

            // Configure font folder and default fallback font
            string fontFolder = @"C:\Windows\Fonts";
            FontConfigs.SetFontFolder(fontFolder, true);
            FontConfigs.DefaultFontName = "CustomFont";

            // Create a new diagram
            Diagram diagram = new Diagram();

            // Path to the legacy stencil file (VSS or VSSX)
            string stencilPath = "legacy.vssx";

            // Import a master from the stencil (replace with actual master name as needed)
            string masterName = "Rectangle";
            diagram.AddMaster(stencilPath, masterName);

            // Add a shape based on the imported master to the first page
            Page page = diagram.Pages[0];
            double pinX = 2.0;
            double pinY = 2.0;
            double width = 1.0;
            double height = 0.5;
            long shapeId = page.AddShape(pinX, pinY, width, height, masterName, false);
            Shape shape = page.Shapes.GetShape(shapeId);

            // Add sample text to the shape
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Sample text"));

            // Apply the custom font to every character in every shape of the diagram
            foreach (Page pg in diagram.Pages)
            {
                foreach (Shape shp in pg.Shapes)
                {
                    // Skip deleted shapes
                    if (shp.Del == BOOL.True)
                        continue;

                    // Set font for each character run
                    foreach (Aspose.Diagram.Char ch in shp.Chars)
                    {
                        ch.FontName.Value = "CustomFont";
                    }
                }
            }

            // Save the diagram with the applied font settings
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
