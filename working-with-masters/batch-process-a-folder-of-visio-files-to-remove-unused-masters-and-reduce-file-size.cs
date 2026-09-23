using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Folder containing Visio files (change as needed)
        string folderPath = @"C:\VisioFiles";

        // Get all Visio files in the folder (VSDX, VDX, VSD)
        string[] visioFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in visioFiles)
        {
            // Process only supported Visio extensions
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (extension != ".vsdx" && extension != ".vdx" && extension != ".vsd")
                continue;

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Remove masters that are not used by any shape
                RemoveUnusedMasters(diagram);

                // Save the diagram back, preserving original format
                if (extension == ".vsdx")
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                else if (extension == ".vdx")
                    diagram.Save(filePath, SaveFileFormat.Vdx);
                else // .vsd
                    diagram.Save(filePath, SaveFileFormat.Vsd);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }

    // Removes masters that are not referenced by any shape in the diagram
    private static void RemoveUnusedMasters(Diagram diagram)
    {
        // Collect masters that are actually used
        var usedMasterNames = new System.Collections.Generic.HashSet<string>();

        // Iterate through all pages and shapes to gather used master names
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Master != null && !string.IsNullOrEmpty(shape.Master.Name))
                {
                    usedMasterNames.Add(shape.Master.Name);
                }
            }
        }

        // Iterate through masters and remove those not in the used set
        // Note: Collect masters to remove first to avoid modifying the collection while iterating
        var mastersToRemove = new System.Collections.Generic.List<Master>();
        foreach (Master master in diagram.Masters)
        {
            if (!usedMasterNames.Contains(master.Name))
            {
                mastersToRemove.Add(master);
            }
        }

        // Remove the unused masters
        foreach (Master master in mastersToRemove)
        {
            diagram.Masters.Remove(master);
        }
    }
}
