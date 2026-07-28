using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define file paths (adjust as needed)
        string inputPath = "protected.vsdx";
        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "exported.vdx";

        try
        {
            // Load the protected diagram from the input file
            Diagram original = new Diagram(inputPath);

            // Capture document‑level protection flags (BOOL enum comparison)
            bool protectBkgnds = original.DocumentSettings.ProtectBkgnds == BOOL.True;
            bool protectMasters = original.DocumentSettings.ProtectMasters == BOOL.True;
            bool protectShapes = original.DocumentSettings.ProtectShapes == BOOL.True;
            bool protectStyles = original.DocumentSettings.ProtectStyles == BOOL.True;

            // Export the diagram to VDX format
            original.Save(outputPath, SaveFileFormat.Vdx);

            // Reload the exported VDX file
            Diagram exported = new Diagram(outputPath);

            // Verify that protection flags are identical after export
            bool protectBkgndsExport = exported.DocumentSettings.ProtectBkgnds == BOOL.True;
            bool protectMastersExport = exported.DocumentSettings.ProtectMasters == BOOL.True;
            bool protectShapesExport = exported.DocumentSettings.ProtectShapes == BOOL.True;
            bool protectStylesExport = exported.DocumentSettings.ProtectStyles == BOOL.True;

            // If any flag differs, raise an exception
            if (protectBkgnds != protectBkgndsExport ||
                protectMasters != protectMastersExport ||
                protectShapes != protectShapesExport ||
                protectStyles != protectStylesExport)
            {
                throw new Exception("Protection settings were not retained after exporting to VDX.");
            }

            Console.WriteLine("Export to VDX succeeded and all protection settings are retained.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}