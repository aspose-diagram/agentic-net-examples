using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DiagramProcessor
{
    static void Main()
    {
        try
        {

            // Path to the original Visio diagram
            string originalPath = "original.vdx";

            // Path where the backup copy will be saved
            string backupPath = "original_backup.vdx";

            // Load the original diagram
            Diagram diagram = new Diagram(originalPath);

            // Create save options for VDX format (Visio XML)
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vdx);

            // Save a backup copy of the original diagram before any modifications
            diagram.Save(backupPath, saveOptions);

            // -----------------------------------------------------------------
            // At this point you can apply watermark changes to 'diagram'.
            // The backup file remains unchanged and can be used for recovery.
            // -----------------------------------------------------------------

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
