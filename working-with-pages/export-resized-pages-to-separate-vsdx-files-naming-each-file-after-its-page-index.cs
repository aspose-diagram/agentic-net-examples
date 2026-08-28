using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file (replace with actual path)
                string sourcePath = "input.vsdx";

                // Load the source diagram
                Diagram sourceDiagram = new Diagram(sourcePath);

                // Iterate through each page in the source diagram
                for (int i = 0; i < sourceDiagram.Pages.Count; i++)
                {
                    // Get the current page from the source diagram
                    Page srcPage = sourceDiagram.Pages[i];

                    // Create a new empty diagram for the single page export
                    Diagram singlePageDiagram = new Diagram();

                    // Copy all masters from the source diagram to the new diagram
                    foreach (Master master in sourceDiagram.Masters)
                    {
                        singlePageDiagram.Masters.Add(master);
                    }

                    // Remove the default empty page that is created by the default constructor
                    if (singlePageDiagram.Pages.Count > 0)
                    {
                        Page defaultPage = singlePageDiagram.Pages[0];
                        singlePageDiagram.Pages.Remove(defaultPage);
                    }

                    // Add the source page to the new diagram
                    // The page is added as a reference; this is sufficient for export purposes
                    singlePageDiagram.Pages.Add(srcPage);

                    // Build the output file name using the page index
                    string outputPath = $"Page_{i}.vsdx";

                    // Save the new diagram containing only the current page
                    singlePageDiagram.Save(outputPath, SaveFileFormat.Vsdx);

                    // Dispose the temporary diagram to free resources
                    singlePageDiagram.Dispose();
                }

                // Dispose the source diagram
                sourceDiagram.Dispose();

                Console.WriteLine("Export completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }