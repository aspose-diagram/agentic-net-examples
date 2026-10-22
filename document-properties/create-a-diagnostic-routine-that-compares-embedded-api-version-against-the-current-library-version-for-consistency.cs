using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect a single argument: path to the Visio diagram file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: DiagnosticRoutine <diagram-file-path>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram using Aspose.Diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve the embedded API version from the document.
            // This version indicates the Aspose.Diagram version that created the file.
            string embeddedVersion = diagram.Version;

            // Retrieve the current library version from the assembly metadata.
            // This reflects the version of Aspose.Diagram.dll that the application is using.
            string libraryVersion = typeof(Diagram).Assembly.GetName().Version?.ToString() ?? "unknown";

            // Output both versions for diagnostic purposes.
            Console.WriteLine($"Embedded API version (from file): {embeddedVersion}");
            Console.WriteLine($"Current library version (runtime): {libraryVersion}");

            // Compare the versions and report consistency.
            if (string.Equals(embeddedVersion, libraryVersion, StringComparison.Ordinal))
            {
                Console.WriteLine("Diagnostic Result: Versions are consistent.");
            }
            else
            {
                Console.WriteLine("Diagnostic Result: Version mismatch detected.");
                Console.WriteLine("The diagram was created with a different Aspose.Diagram version than the one currently loaded.");
            }

            // Retrieve build numbers (they are strings) and display them.
            try
            {
                string createdBuild = diagram.DocumentProps.BuildNumberCreated;
                string editedBuild = diagram.DocumentProps.BuildNumberEdited;
                Console.WriteLine($"Build number (created): {createdBuild}");
                Console.WriteLine($"Build number (edited): {editedBuild}");
            }
            catch (Exception ex)
            {
                // Accessing build numbers may throw if the properties are unavailable.
                Console.Error.WriteLine($"Warning: Unable to retrieve build numbers - {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}