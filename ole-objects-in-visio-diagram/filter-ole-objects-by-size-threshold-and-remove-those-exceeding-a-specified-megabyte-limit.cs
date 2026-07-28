using System.IO;
using System;
using Aspose.Diagram;

class OleSizeFilter
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path to the output Visio file
            string outputPath = "output.vsdx";

            // Maximum allowed OLE object size in megabytes
            double maxSizeMb = 5.0;

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate backwards to allow safe removal of shapes
                for (int i = page.Shapes.Count - 1; i >= 0; i--)
                {
                    Shape shape = page.Shapes[i];

                    // Check if the shape contains foreign (OLE) data
                    if (shape.ForeignData != null && shape.ForeignData.ObjectData != null)
                    {
                        // Calculate the size of the OLE object in megabytes
                        double sizeMb = shape.ForeignData.ObjectData.Length / (1024.0 * 1024.0);

                        // Remove the shape if it exceeds the size threshold
                        if (sizeMb > maxSizeMb)
                        {
                            page.Shapes.Remove(shape);
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
