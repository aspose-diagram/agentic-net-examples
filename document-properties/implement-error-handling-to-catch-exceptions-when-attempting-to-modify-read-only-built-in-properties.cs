using System;
using System.IO;
using System.Reflection;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Path to the input Visio diagram
        string inputPath = "Drawing1.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // -------------------------------------------------------------
            // Attempt to modify a built‑in read‑only property (Version) via reflection.
            // This property is read‑only; setting it should raise an exception.
            // -------------------------------------------------------------
            try
            {
                PropertyInfo versionProp = typeof(Diagram).GetProperty("Version");
                if (versionProp != null && !versionProp.CanWrite)
                {
                    // Attempt to set the read‑only property; expect an exception
                    versionProp.SetValue(diagram, "1.0");
                }
                Console.WriteLine("Successfully modified Version (unexpected).");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error modifying read‑only property 'Version': {ex.Message}");
            }

            // -------------------------------------------------------------
            // Attempt to modify another built‑in read‑only property (BuildNumberCreated)
            // using reflection on the DocumentProps object.
            // -------------------------------------------------------------
            try
            {
                // Get the runtime type of the DocumentProps instance
                Type docPropsType = diagram.DocumentProps.GetType();
                PropertyInfo buildProp = docPropsType.GetProperty("BuildNumberCreated");
                if (buildProp != null && !buildProp.CanWrite)
                {
                    // Attempt to set the read‑only property; expect an exception
                    buildProp.SetValue(diagram.DocumentProps, 9999);
                }
                Console.WriteLine("Successfully modified BuildNumberCreated (unexpected).");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error modifying read‑only property 'BuildNumberCreated': {ex.Message}");
            }

            // -------------------------------------------------------------
            // Save the diagram (no changes expected due to read‑only restrictions)
            // -------------------------------------------------------------
            string outputPath = "ModifiedDrawing.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
        catch (Exception e)
        {
            // General error handling for any unexpected exceptions
            Console.Error.WriteLine($"An error occurred while processing the diagram: {e.Message}");
        }
    }
}