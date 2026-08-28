using Aspose.Diagram;
using System;
using System.IO;

class CommentMigration
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the legacy diagram file (any supported older format)
            string legacyFilePath = "legacy.vsd";

            // Path where the migrated diagram will be saved in the latest format (VDX)
            string migratedFilePath = "migrated.vdx";

            // Determine the appropriate LoadFileFormat based on the legacy file extension
            LoadFileFormat loadFormat = GetLoadFormat(Path.GetExtension(legacyFilePath));

            // Load the legacy diagram using the constructor that accepts a filename and a LoadFileFormat
            Diagram diagram = new Diagram(legacyFilePath, loadFormat);

            // Save the diagram in the latest VDX format; comments are preserved automatically
            diagram.Save(migratedFilePath, SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to map file extensions to the corresponding LoadFileFormat enum values
    static LoadFileFormat GetLoadFormat(string extension)
    {
        switch (extension.ToLower())
        {
            case ".vsd": return LoadFileFormat.Vsd;
            case ".vsdx": return LoadFileFormat.Vsdx;
            case ".vdx": return LoadFileFormat.Vdx;
            case ".vss": return LoadFileFormat.Vss;
            case ".vssx": return LoadFileFormat.Vssx;
            case ".vst": return LoadFileFormat.Vst;
            case ".vstx": return LoadFileFormat.Vstx;
            case ".vdw": return LoadFileFormat.Vdw;
            default: return LoadFileFormat.Vsd; // Default fallback
        }
    }
}
