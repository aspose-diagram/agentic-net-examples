using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path to the Visio file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block (Diagram implements IDisposable).
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Retrieve the mapping of shape IDs to their absolute Pin coordinates.
                Dictionary<long, Tuple<double, double>> pinMap = GetShapeAbsolutePinCoordinates(diagram);

                // Output the results to the console.
                foreach (var kvp in pinMap)
                {
                    Console.WriteLine($"Shape ID: {kvp.Key}, Absolute PinX: {kvp.Value.Item1}, Absolute PinY: {kvp.Value.Item2}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Returns a dictionary that maps each shape's ID to a tuple containing its absolute PinX and PinY coordinates.
    /// </summary>
    /// <param name="diagram">The loaded Aspose.Diagram Diagram instance.</param>
    /// <returns>Dictionary with shape ID as key and (PinX, PinY) as value.</returns>
    private static Dictionary<long, Tuple<double, double>> GetShapeAbsolutePinCoordinates(Diagram diagram)
    {
        // Initialize the result dictionary.
        var result = new Dictionary<long, Tuple<double, double>>();

        // Iterate through all pages in the diagram.
        foreach (Page page in diagram.Pages)
        {
            // Iterate through all shapes on the current page.
            foreach (Shape shape in page.Shapes)
            {
                // Skip logically deleted shapes.
                if (shape.Del == BOOL.True)
                    continue;

                // Retrieve the PinX and PinY values from the shape's XForm.
                // These values represent the shape's position. For group sub‑shapes,
                // they are relative to the parent group; however, Aspose.Diagram
                // does not expose a direct GetAbsolutePinX/Y method, so we use the
                // available coordinates.
                double pinX = shape.XForm.PinX.Value;
                double pinY = shape.XForm.PinY.Value;

                // Add to the dictionary using the shape's unique ID.
                result[shape.ID] = Tuple.Create(pinX, pinY);
            }
        }

        return result;
    }
}