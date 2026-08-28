using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;
using Aspose.Diagram.Saving;

public class Program
{
    // Cached AutoSpaceOptions instance reused for all diagrams
    private static readonly AutoSpaceOptions autoSpaceOptions = CreateAutoSpaceOptions();

    private static AutoSpaceOptions CreateAutoSpaceOptions()
    {
        var options = new AutoSpaceOptions();
        // Set desired spacing between shapes (in inches)
        options.DistanceInHorizontal = 0.5;
        options.DistanceInVertical = 0.5;
        return options;
    }

    public static void Main()
    {
        try
        {

            string inputFolder = "InputDiagrams";
            string outputFolder = "OutputDiagrams";

            // Ensure the output directory exists
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Process all VSDX files in the input folder
            string[] files = Directory.GetFiles(inputFolder, "*.vsdx");
            foreach (string filePath in files)
            {
                try
                {
                    // Load the diagram from file
                    Diagram diagram = new Diagram(filePath);

                    // Apply AutoSpace to each page using the cached options
                    foreach (Page page in diagram.Pages)
                    {
                        page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);
                    }

                    // Save the updated diagram
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    string outputPath = Path.Combine(outputFolder, fileName + "_spaced.vsdx");
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
                catch (Exception ex)
                {
                    // Log any errors and continue with the next file
                    Console.WriteLine($"Error processing '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("All diagrams have been processed.");

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
