using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths from command‑line arguments or use defaults.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output_protected.vsdx";

        // Guard: ensure the source Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // -----------------------------------------------------------------
            // Apply read‑only protection to the entire document.
            // The following protection cells prevent editing of backgrounds,
            // masters, shapes, and styles. Setting them to BOOL.True makes the
            // document effectively read‑only.
            // -----------------------------------------------------------------
            diagram.DocumentSettings.ProtectBkgnds = BOOL.True;
            diagram.DocumentSettings.ProtectMasters = BOOL.True;
            diagram.DocumentSettings.ProtectShapes = BOOL.True;
            diagram.DocumentSettings.ProtectStyles = BOOL.True;

            // -----------------------------------------------------------------
            // Assign a password for editing restrictions.
            // Aspose.Diagram does not expose a password property for document
            // protection (see API limitation). Therefore, password protection
            // cannot be applied directly via the current API. The code below
            // documents this limitation.
            // -----------------------------------------------------------------
            // Note: Password protection is not supported in the current
            // Aspose.Diagram version; only protection flags can be set.

            // Save the protected diagram to the specified output file.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with read‑only protection to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}