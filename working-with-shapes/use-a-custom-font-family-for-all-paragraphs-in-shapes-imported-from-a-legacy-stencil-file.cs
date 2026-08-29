using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main()
    {
        // Configure global font settings before creating any Diagram instance
        // Set the folder where system fonts are located (adjust path as needed)
        FontConfigs.SetFontFolder(@"C:\Windows\Fonts", true);
        // Define the fallback font name to use when a requested font is missing
        FontConfigs.DefaultFontName = "Calibri";

        // Path to the legacy stencil file (VSS or VSSX)
        string stencilPath = "legacyStencil.vssx";
        // Guard against missing stencil file
        if (!File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"File not found: {stencilPath}");
            return;
        }

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Import all masters from the legacy stencil into the diagram
            foreach (Master master in new Diagram(stencilPath).Masters)
            {
                // Add each master by name; this creates a copy in the target diagram
                diagram.AddMaster(stencilPath, master.Name);
            }

            // Add one shape for each imported master onto the first page
            Page page = diagram.Pages[0];
            double startX = 1.0;
            double startY = 1.0;
            double offset = 2.0;

            foreach (Master master in diagram.Masters)
            {
                // Place the shape using the master name (returns a shape ID)
                long shapeId = page.AddShape(startX, startY, master.Name);
                // Retrieve the concrete Shape object for further modifications
                Shape shape = page.Shapes.GetShape(shapeId);

                // Example: add some sample text to the shape
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt($"Shape from master: {master.Name}"));

                // Move to next position for the next shape
                startX += offset;
                if (startX > 10.0)
                {
                    startX = 1.0;
                    startY += offset;
                }
            }

            // Apply the custom font family to all characters in all shapes
            foreach (Page pg in diagram.Pages)
            {
                foreach (Shape shp in pg.Shapes)
                {
                    // Ensure the shape has a character collection
                    if (shp.Chars != null && shp.Chars.Count > 0)
                    {
                        foreach (Aspose.Diagram.Char ch in shp.Chars)
                        {
                            // Set the font name for each character run
                            ch.FontName.Value = "Calibri";
                        }
                    }
                }
            }

            // Save the resulting diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}