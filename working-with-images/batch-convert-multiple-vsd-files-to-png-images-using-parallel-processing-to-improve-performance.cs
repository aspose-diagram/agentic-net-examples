using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Diagram;

class VsdToPngBatchConverter
{
    // Converts all .vsd files in the input folder to PNG images in the output folder using parallel processing.
    public static void ConvertFolder(string inputFolder, string outputFolder)
    {
        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Gather all .vsd files
        string[] vsdFiles = Directory.GetFiles(inputFolder, "*.vsd", SearchOption.TopDirectoryOnly);

        // Process files in parallel
        Parallel.ForEach(vsdFiles, vsdFile =>
        {
            // Determine output PNG file path
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(vsdFile);
            string pngPath = Path.Combine(outputFolder, fileNameWithoutExt + ".png");

            // Load the diagram from the VSD file
            using (Diagram diagram = new Diagram(vsdFile, LoadFileFormat.Vsd))
            {
                // Save the diagram as PNG using the Save method with SaveFileFormat.Png
                diagram.Save(pngPath, SaveFileFormat.Png);
            }
        });
    }

    // Example usage
    static void Main()
    {
        string inputDir = @"C:\VisioFiles";
        string outputDir = @"C:\VisioPngs";

        ConvertFolder(inputDir, outputDir);

        Console.WriteLine("Conversion completed.");
    }
}
