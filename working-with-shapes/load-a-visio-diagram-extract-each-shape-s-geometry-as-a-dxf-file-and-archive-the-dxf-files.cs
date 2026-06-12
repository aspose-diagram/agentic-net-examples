using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputVisioPath = "input.vsdx";

            // Path for the resulting ZIP archive containing DXF files
            string outputZipPath = "shapes.dxf.zip";

            // Load the Visio diagram using the provided constructor
            using (Diagram diagram = new Diagram(inputVisioPath))
            {
                // Create a temporary folder to store individual DXF files
                string tempFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                Directory.CreateDirectory(tempFolder);

                int shapeCounter = 0;

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Build a unique DXF file name for each shape
                        string dxfFilePath = Path.Combine(tempFolder, $"shape_{shapeCounter}.dxf");

                        // Export the shape's geometry to a DXF file
                        ExportShapeToDxf(shape, dxfFilePath);

                        shapeCounter++;
                    }
                }

                // Create a ZIP archive that contains all generated DXF files
                if (File.Exists(outputZipPath))
                    File.Delete(outputZipPath);
                ZipFile.CreateFromDirectory(tempFolder, outputZipPath);

                // Clean up the temporary folder
                Directory.Delete(tempFolder, true);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method that writes a minimal DXF representation of a shape.
    // Aspose.Diagram does not expose a direct DXF export, so this method
    // demonstrates how one could manually write DXF content based on shape geometry.
    static void ExportShapeToDxf(Shape shape, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // DXF header
            writer.WriteLine("0");
            writer.WriteLine("SECTION");
            writer.WriteLine("2");
            writer.WriteLine("ENTITIES");

            // Example: write a simple closed polyline (rectangle) as placeholder.
            // Replace this block with actual geometry extraction from 'shape'.
            writer.WriteLine("0");
            writer.WriteLine("LWPOLYLINE");
            writer.WriteLine("8");   // Layer name
            writer.WriteLine("0");
            writer.WriteLine("90");  // Number of vertices
            writer.WriteLine("4");
            writer.WriteLine("70");  // Closed polyline flag
            writer.WriteLine("1");

            // Vertex 1 (0,0)
            writer.WriteLine("10");
            writer.WriteLine("0");
            writer.WriteLine("20");
            writer.WriteLine("0");

            // Vertex 2 (100,0)
            writer.WriteLine("10");
            writer.WriteLine("100");
            writer.WriteLine("20");
            writer.WriteLine("0");

            // Vertex 3 (100,100)
            writer.WriteLine("10");
            writer.WriteLine("100");
            writer.WriteLine("20");
            writer.WriteLine("100");

            // Vertex 4 (0,100)
            writer.WriteLine("10");
            writer.WriteLine("0");
            writer.WriteLine("20");
            writer.WriteLine("100");

            // End of entities section
            writer.WriteLine("0");
            writer.WriteLine("ENDSEC");
            writer.WriteLine("0");
            writer.WriteLine("EOF");
        }
    }
}
