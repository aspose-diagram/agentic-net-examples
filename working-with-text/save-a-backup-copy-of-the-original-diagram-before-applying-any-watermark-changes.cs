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
            string originalFilePath = "diagram.vdx";

            // Path for the backup copy
            string backupFilePath = "diagram_backup.vdx";

            // Load the original diagram
            using (Diagram diagram = new Diagram(originalFilePath))
            {
                // Save a backup copy before any modifications
                diagram.Save(backupFilePath, SaveFileFormat.Vdx);

                // -----------------------------------------------------------------
                // Place watermark modification logic here (e.g., adding shapes,
                // updating headers/footers, etc.). This section is omitted as the
                // task focuses on creating the backup.
                // -----------------------------------------------------------------

                // Example: after modifications, you might save the updated diagram
                // diagram.Save(originalFilePath, SaveFileFormat.Vdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
