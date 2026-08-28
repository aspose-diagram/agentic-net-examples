using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder containing the diagrams.
        string folderPath = args.Length > 0 ? args[0] : "Diagrams";

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Retrieve all Visio files in the folder.
        string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string file in files)
        {
            string ext = Path.GetExtension(file).ToLowerInvariant();
            if (ext != ".vsdx" && ext != ".vsd" && ext != ".vdx")
                continue; // Skip non‑Visio files.

            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(file);

                // Add page number placeholder to the right footer.
                diagram.HeaderFooter.FooterRight = "Page: &p";

                // Save the diagram (overwrites the original file).
                diagram.Save(file, SaveFileFormat.Vsdx);

                Console.WriteLine($"Processed: {Path.GetFileName(file)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {Path.GetFileName(file)}: {ex.Message}");
            }
        }
    }
}
