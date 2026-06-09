using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Define geometry indexes we want to process for each shape
            int[] targetGeometryIndexes = { 0, 2, 5 };

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Iterate through each Geom collection of the shape
                    for (int geomIdx = 0; geomIdx < shape.Geoms.Count; geomIdx++)
                    {
                        // Cast the item to Geom as required by the API
                        Geom geom = (Geom)shape.Geoms[geomIdx];

                        // Attempt to access each target index in the CoordinateCol collection
                        foreach (int targetIdx in targetGeometryIndexes)
                        {
                            try
                            {
                                // This will throw if the index is out of range
                                var segment = geom.CoordinateCol[targetIdx];

                                // Example operation on a valid segment: mark it as deleted
                                segment.Del = BOOL.True;
                            }
                            catch (Exception ex) when (ex is IndexOutOfRangeException || ex is ArgumentOutOfRangeException)
                            {
                                // Log detailed error information and continue with the next entry
                                Console.WriteLine($"[Warning] Shape ID {shape.ID}, Geom #{geomIdx}, attempted coordinate index {targetIdx} is out of range.");
                                Console.WriteLine($"          Exception: {ex.GetType().Name} - {ex.Message}");
                                // Skip this invalid entry
                            }
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
