using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Path to the input Visio file.
        string inputPath = "Drawing1.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Attempt to delete a built‑in property (e.g., Title).
            // Built‑in properties are not part of the CustomProps collection and cannot be removed.
            // We simulate the deletion attempt by trying to remove a property with the same name from CustomProps.
            string builtInPropName = "Title";

            // Search for a custom property with the same name (should be null for built‑in).
            CustomProp? prop = diagram.DocumentProps.CustomProps.FirstOrDefault(p => p.Name == builtInPropName);

            try
            {
                // This call is expected to fail because 'prop' is null (built‑in properties are not removable).
                // The Remove method will throw an ArgumentNullException.
                diagram.DocumentProps.CustomProps.Remove(prop);
                // If no exception is thrown, the test has failed.
                Console.Error.WriteLine("Error: Expected exception was not thrown when attempting to delete a built‑in property.");
            }
            catch (Exception ex)
            {
                // Expected path: handle the exception and report success.
                Console.WriteLine($"Caught expected exception: {ex.GetType().Name} - {ex.Message}");
            }

            // Optionally, save the diagram to verify it remains unchanged.
            string outputPath = "Output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // General error handling for Aspose operations.
            Console.Error.WriteLine($"An error occurred: {ex.GetType().Name} - {ex.Message}");
        }
    }
}