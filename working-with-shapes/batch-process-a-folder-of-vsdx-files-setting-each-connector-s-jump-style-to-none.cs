using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the folder path containing VSDX files as the first argument.");
            return;
        }

        string folderPath = args[0];

        // Guard: ensure the folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Get all VSDX files in the folder (non-recursive).
        string[] files = Directory.GetFiles(folderPath, "*.vsdx");

        if (files.Length == 0)
        {
            Console.WriteLine("No VSDX files found in the specified folder.");
            return;
        }

        foreach (string filePath in files)
        {
            // Guard: ensure the file exists (should always be true here).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found (skipping): {filePath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate through each page.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes.
                            if (shape.Del == BOOL.True)
                                continue;

                            // Process only connector shapes (1‑D shapes).
                            if (shape.OneD)
                            {
                                // Set the connector's jump style to "none" (PageDefault).
                                shape.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.PageDefault;
                            }
                        }
                    }

                    // Save the modified diagram, overwriting the original file.
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine($"Processed and saved: {filePath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of this file.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}