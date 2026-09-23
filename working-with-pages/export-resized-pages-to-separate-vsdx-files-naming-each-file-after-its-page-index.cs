using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Validate arguments: first argument is the source Visio file,
            // second (optional) argument is the output folder.
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: ExportPages <sourceVisioFile> [outputFolder]");
                return;
            }

            string sourcePath = args[0];
            string outputFolder = args.Length > 1 ? args[1] : Environment.CurrentDirectory;

            // Ensure the output folder exists.
            if (!System.IO.Directory.Exists(outputFolder))
            {
                System.IO.Directory.CreateDirectory(outputFolder);
            }

            // Load the source diagram.
            using (Diagram sourceDiagram = new Diagram(sourcePath))
            {
                int pageCount = sourceDiagram.Pages.Count;

                for (int i = 0; i < pageCount; i++)
                {
                    // Retrieve the page to export.
                    Page sourcePage = sourceDiagram.Pages[i];

                    // Create a new empty diagram.
                    using (Diagram singlePageDiagram = new Diagram())
                    {
                        // The new diagram contains a default page at index 0.
                        // Copy the source page content into this default page.
                        singlePageDiagram.Pages[0].Copy(sourcePage);

                        // Build the output file name using the page index.
                        string outputPath = System.IO.Path.Combine(outputFolder, $"Page_{i}.vsdx");

                        // Save the diagram containing only the copied page.
                        singlePageDiagram.Save(outputPath, SaveFileFormat.Vsdx);

                        Console.WriteLine($"Exported page {i} to '{outputPath}'.");
                    }
                }
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
