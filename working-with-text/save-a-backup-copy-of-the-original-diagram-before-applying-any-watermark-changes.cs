using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the original Visio diagram
            string originalPath = "diagram.vdx";

            // Path where the backup copy will be saved
            string backupPath = "diagram_backup.vdx";

            // Load the original diagram
            Diagram diagram = new Diagram(originalPath);

            // Save a backup copy before applying any watermark changes
            diagram.Save(backupPath, SaveFileFormat.Vdx);

            // Continue with watermark modifications here...

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
