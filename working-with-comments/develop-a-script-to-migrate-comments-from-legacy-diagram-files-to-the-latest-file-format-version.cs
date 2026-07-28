using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class CommentMigration
{
    static void Main()
    {
        try
        {

            // Path to the legacy Visio file (e.g., VSD format)
            string legacyFile = @"C:\Diagrams\legacy.vsd";

            // Path where the migrated diagram will be saved (e.g., VDX format)
            string migratedFile = @"C:\Diagrams\migrated.vdx";

            // Load the legacy diagram using the appropriate load format
            Diagram legacyDiagram = new Diagram(legacyFile, LoadFileFormat.Vsd);

            // Create a new (empty) diagram instance
            Diagram newDiagram = new Diagram();

            // Combine the legacy diagram into the new diagram.
            // This operation copies all pages, shapes, and associated comments.
            newDiagram.Combine(legacyDiagram);

            // Save the combined diagram in the latest VDX format.
            newDiagram.Save(migratedFile, SaveFileFormat.Vdx);

            // Clean up resources
            legacyDiagram.Dispose();
            newDiagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
