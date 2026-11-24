using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Dictionary to cache OLE data for lazy access (shape ID -> OLE binary)
                Dictionary<long, byte[]> oleCache = new Dictionary<long, byte[]>();

                // Iterate pages and shapes to identify OLE (foreign) objects
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Detect OLE objects via ForeignData
                        if (shape.ForeignData != null && shape.ForeignData.ObjectData != null && shape.ForeignData.ObjectData.Length > 0)
                        {
                            // Store OLE data in cache for later use (lazy loading)
                            oleCache[shape.ID] = shape.ForeignData.ObjectData;

                            // Optionally clear the OLE data from the shape to reduce initial memory usage
                            // Note: ObjectData property is read‑only in many versions; if it is settable, uncomment the next line
                            // shape.ForeignData.ObjectData = null;
                        }
                    }
                }

                Console.WriteLine($"Diagram loaded. Detected {oleCache.Count} OLE object(s).");
                Console.WriteLine("OLE data will be loaded on demand using GetOleData method.");

                // Example usage: retrieve OLE data for a specific shape when needed
                // (Replace 12345 with an actual shape ID from the diagram)
                long exampleShapeId = 0;
                if (oleCache.Count > 0)
                {
                    exampleShapeId = new List<long>(oleCache.Keys)[0];
                    byte[] oleData = GetOleData(exampleShapeId, oleCache);
                    Console.WriteLine($"Retrieved OLE data for shape ID {exampleShapeId}, size: {oleData.Length} bytes.");
                }

                // Save the diagram (no modifications made in this example)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves OLE binary data for the given shape ID from the cache.
    /// This method simulates lazy loading by accessing the data only when called.
    /// </summary>
    /// <param name="shapeId">The ID of the shape containing OLE data.</param>
    /// <param name="cache">Dictionary storing OLE data keyed by shape ID.</param>
    /// <returns>Byte array of OLE data.</returns>
    private static byte[] GetOleData(long shapeId, Dictionary<long, byte[]> cache)
    {
        if (cache.TryGetValue(shapeId, out byte[] data))
        {
            return data;
        }
        else
        {
            throw new Exception($"OLE data for shape ID {shapeId} not found in cache.");
        }
    }
}