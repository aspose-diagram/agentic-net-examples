using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Folder containing the source Visio files
            string inputFolder = "VisioFiles";

            // Folder where the updated files will be saved
            string outputFolder = "UpdatedVisioFiles";

            // Ensure the output directory exists
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // Get all files in the input folder (Visio supports multiple extensions)
            string[] files = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);

            foreach (string filePath in files)
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(filePath);

                // Use the document title; if missing, fall back to the file name without extension
                string title = diagram.DocumentProps.Title;
                if (string.IsNullOrWhiteSpace(title))
                    title = Path.GetFileNameWithoutExtension(filePath);

                // Set the left header text to the title
                diagram.HeaderFooter.HeaderLeft = title;

                // Build the output file path (preserve original file name)
                string fileName = Path.GetFileName(filePath);
                string outPath = Path.Combine(outputFolder, fileName);

                // Save the diagram (using Vsdx format as a common Visio format)
                diagram.Save(outPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("All Visio files have been processed and saved with updated headers.");

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
