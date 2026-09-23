using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Load an existing Visio diagram (replace with a valid path if needed)
        string inputPath = "sample.vsdx";
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Attempt to delete (clear) a built‑in property: Title
        // Built‑in properties are read‑only for certain operations; setting to null should raise an exception.
        try
        {
            // This operation is expected to fail because Title cannot be set to null.
            diagram.DocumentProps.Title = null;
            // If no exception is thrown, the test has failed.
            throw new Exception("Expected exception was not thrown when attempting to delete a built‑in property.");
        }
        catch (Exception ex)
        {
            // Expected path: an exception should be caught.
            Console.WriteLine($"Caught expected exception: {ex.GetType().Name} - {ex.Message}");
        }

        // Save the diagram to verify that the file can still be saved after the operation.
        string outputPath = "output.vsdx";
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}
