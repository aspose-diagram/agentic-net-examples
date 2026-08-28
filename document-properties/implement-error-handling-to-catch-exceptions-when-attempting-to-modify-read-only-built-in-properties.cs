using System;
using System.IO;
using System.Reflection;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Paths to the input and output Visio files
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }
        string outputPath = "output.vsdx";

        // Load the diagram from file
        Diagram diagram = new Diagram(inputPath);

        // Attempt to modify a read‑only built‑in property: BuildNumberCreated via reflection
        try
        {
            // Use reflection to bypass compile‑time read‑only restriction
            PropertyInfo buildProp = typeof(Diagram).GetProperty("BuildNumberCreated", BindingFlags.Instance | BindingFlags.Public);
            // Set a string value; the property is read‑only, so this will throw at runtime
            buildProp?.SetValue(diagram, "9999");
            Console.WriteLine("BuildNumberCreated was set successfully (unexpected).");
        }
        catch (Exception ex)
        {
            // Expected: property is read‑only, so an exception is caught here
            Console.WriteLine($"Error setting BuildNumberCreated: {ex.Message}");
        }

        // Attempt to modify another read‑only built‑in property: Version via reflection
        try
        {
            PropertyInfo versionProp = typeof(Diagram).GetProperty("Version", BindingFlags.Instance | BindingFlags.Public);
            // Attempt to set a new version string; will raise an exception because the property is read‑only
            versionProp?.SetValue(diagram, "15.0");
            Console.WriteLine("Version was set successfully (unexpected).");
        }
        catch (Exception ex)
        {
            // Expected: property is read‑only, so an exception is caught here
            Console.WriteLine($"Error setting Version: {ex.Message}");
        }

        // Save the diagram (even if modifications failed)
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving diagram: {ex.Message}");
        }

        // Clean up resources
        diagram.Dispose();
    }
}