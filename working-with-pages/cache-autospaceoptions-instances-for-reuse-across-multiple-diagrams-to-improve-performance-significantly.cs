using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

namespace DiagramAutoSpaceCacheExample
{
    // Static cache for AutoSpaceOptions to reuse across diagrams
    public static class AutoSpaceOptionsCache
    {
        // Preconfigured options instance
        private static readonly AutoSpaceOptions _cachedOptions = CreateOptions();

        private static AutoSpaceOptions CreateOptions()
        {
            var options = new AutoSpaceOptions();
            // Example distances; adjust as needed
            options.DistanceInHorizontal = 2.0;
            options.DistanceInVertical = 2.0;
            return options;
        }

        // Retrieve the cached instance
        public static AutoSpaceOptions Get()
        {
            return _cachedOptions;
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Paths for input and output diagrams
                string[] inputFiles = { "Diagram1.vsdx", "Diagram2.vsdx", "Diagram3.vsdx" };
                string outputFolder = "ProcessedDiagrams";

                // Ensure output folder exists
                System.IO.Directory.CreateDirectory(outputFolder);

                foreach (string inputPath in inputFiles)
                {
                    // Load diagram
                    Diagram diagram = new Diagram(inputPath);

                    // Apply cached AutoSpaceOptions to each page
                    foreach (Page page in diagram.Pages)
                    {
                        page.AutoSpaceShapes(page.Shapes, AutoSpaceOptionsCache.Get());
                    }

                    // Save the modified diagram
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(inputPath);
                    string outputPath = System.IO.Path.Combine(outputFolder, fileName + "_AutoSpaced.vsdx");
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Processing completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}