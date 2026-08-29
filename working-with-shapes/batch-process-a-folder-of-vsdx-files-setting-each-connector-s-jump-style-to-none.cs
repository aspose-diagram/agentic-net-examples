using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Folder containing the VSDX files – adjust as needed.
        string folderPath = @"C:\VisioFiles";

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Process each VSDX file in the folder.
        string[] files = Directory.GetFiles(folderPath, "*.vsdx", SearchOption.TopDirectoryOnly);
        foreach (string filePath in files)
        {
            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify connector shapes (1‑D shapes).
                        if (shape.OneD)
                        {
                            // Set the connector's jump style to "none" (page default).
                            shape.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.PageDefault;
                        }
                    }
                }

                // Save the modified diagram, overwriting the original file.
                diagram.Save(filePath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{Path.GetFileName(filePath)}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
