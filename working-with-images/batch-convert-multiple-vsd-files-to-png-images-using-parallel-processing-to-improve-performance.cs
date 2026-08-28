using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Folder containing the source VSD files
            string inputFolder = @"C:\InputVsd";

            // Folder where PNG images will be saved
            string outputFolder = @"C:\OutputPng";

            // Ensure the output directory exists
            Directory.CreateDirectory(outputFolder);

            // Get all VSD files in the input folder
            string[] vsdFiles = Directory.GetFiles(inputFolder, "*.vsd");

            // Convert each VSD to PNG in parallel
            Parallel.ForEach(vsdFiles, vsdPath =>
            {
                // Load the diagram from the VSD file
                using (Diagram diagram = new Diagram(vsdPath))
                {
                    // Build the output PNG file path
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(vsdPath);
                    string pngPath = Path.Combine(outputFolder, fileNameWithoutExt + ".png");

                    // Save the diagram as a PNG image
                    diagram.Save(pngPath, SaveFileFormat.Png);
                }
            });

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
