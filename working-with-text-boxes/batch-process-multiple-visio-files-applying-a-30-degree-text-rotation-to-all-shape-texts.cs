using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Folder containing the Visio files to process.
        string inputFolder = args.Length > 0 ? args[0] : @"C:\VisioFiles";

        // Folder where the processed files will be saved.
        string outputFolder = args.Length > 1 ? args[1] : Path.Combine(inputFolder, "Processed");
        Directory.CreateDirectory(outputFolder);

        // Process each .vsdx file in the input folder.
        foreach (string filePath in Directory.GetFiles(inputFolder, "*.vsdx"))
        {
            // Load the diagram from the file.
            using (Diagram diagram = new Diagram(filePath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Some shapes may not have a TextXForm; skip those.
                        if (shape.TextXForm != null)
                        {
                            // Apply a 30‑degree rotation to the shape's text block.
                            shape.TextXForm.TxtAngle.Value = 30.0;
                        }
                    }
                }

                // Save the modified diagram to the output folder, preserving the original name.
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
    }
}
