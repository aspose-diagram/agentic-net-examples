using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and target Visio files
                string sourcePath = "source.vsdx";
                string targetPath = "target.vsdx";

                // Load the source diagram containing the comments to be copied
                Diagram sourceDiagram = new Diagram(sourcePath);

                // Load (or create) the target diagram where comments will be copied to
                Diagram targetDiagram = new Diagram(targetPath);

                // Combine the source diagram into the target diagram.
                // This operation merges pages, masters, shapes, and annotations (comments)
                // while preserving all metadata such as author (ReviewerID) and creation dates.
                targetDiagram.Combine(sourceDiagram);

                // Save the updated target diagram with the copied comments
                targetDiagram.Save("target_with_comments.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }