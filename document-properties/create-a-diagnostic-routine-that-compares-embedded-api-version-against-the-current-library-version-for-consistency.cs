using System;
using System.IO;
using System.Reflection;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: DiagnosticRoutine <path-to-visio-file>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve the embedded API version from the document
            string embeddedVersion = diagram.Version;

            // Retrieve the current Aspose.Diagram library version via assembly metadata
            Version assemblyVersion = typeof(Diagram).Assembly.GetName().Version;
            string libraryVersion = assemblyVersion != null ? assemblyVersion.ToString() : "Unknown";

            // Output both versions for diagnostic purposes
            Console.WriteLine($"Embedded Diagram API Version : {embeddedVersion}");
            Console.WriteLine($"Current Aspose.Diagram Library Version : {libraryVersion}");

            // Compare versions and report consistency
            if (string.Equals(embeddedVersion, libraryVersion, StringComparison.Ordinal))
            {
                Console.WriteLine("Diagnostic Result: Versions are consistent.");
            }
            else
            {
                Console.WriteLine("Diagnostic Result: Version mismatch detected.");
                Console.WriteLine("Recommendation: Consider updating the diagram or the library to ensure compatibility.");
            }

            // Additional optional diagnostics: display build numbers from document properties
            Console.WriteLine($"Document Build Number (Created) : {diagram.DocumentProps.BuildNumberCreated}");
            Console.WriteLine($"Document Build Number (Edited)  : {diagram.DocumentProps.BuildNumberEdited}");
        }
        catch (Exception ex)
        {
            // Capture any errors from Aspose.Diagram operations
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}