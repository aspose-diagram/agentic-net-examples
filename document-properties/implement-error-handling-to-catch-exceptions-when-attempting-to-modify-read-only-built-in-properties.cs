using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "Drawing1.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output diagram path
        string outputPath = "Drawing1_Modified.vsdx";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Display current built‑in properties (read‑only examples)
            Console.WriteLine($"Diagram Version: {diagram.Version}");
            Console.WriteLine($"Build Number Created: {diagram.DocumentProps.BuildNumberCreated}");
            Console.WriteLine($"Build Number Edited: {diagram.DocumentProps.BuildNumberEdited}");

            // Attempt to modify read‑only built‑in properties
            // These operations are expected to throw exceptions because the properties are read‑only
            try
            {
                // Attempt to change the API version (read‑only)
                // This will raise an exception
                // Note: Version is a string property with only a getter
                // The following line is intentionally incorrect to demonstrate error handling
                // diagram.Version = "99.99"; // Uncommenting this line would cause a compile error
                // Instead, we simulate the attempt via reflection (which will still fail at runtime)
                var versionProp = typeof(Diagram).GetProperty("Version");
                if (versionProp != null && versionProp.CanWrite)
                {
                    versionProp.SetValue(diagram, "99.99");
                }
                else
                {
                    Console.WriteLine("Version property is read‑only; cannot modify.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error modifying Version: {ex.Message}");
            }

            try
            {
                // Attempt to change BuildNumberCreated (read‑only)
                // This property has only a getter
                // The following line would not compile, so we use reflection to simulate the attempt
                var buildCreatedProp = typeof(DocumentProperties).GetProperty("BuildNumberCreated");
                if (buildCreatedProp != null && buildCreatedProp.CanWrite)
                {
                    buildCreatedProp.SetValue(diagram.DocumentProps, 9999);
                }
                else
                {
                    Console.WriteLine("BuildNumberCreated property is read‑only; cannot modify.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error modifying BuildNumberCreated: {ex.Message}");
            }

            // Save the diagram (even though no changes were applied)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Catch any unexpected exceptions from Aspose operations
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}