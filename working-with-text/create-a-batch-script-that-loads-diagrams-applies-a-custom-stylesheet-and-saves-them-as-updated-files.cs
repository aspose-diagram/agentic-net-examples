using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Folder containing the source Visio files
            string inputFolder = @"C:\Diagrams\Input";
            // Folder where the updated files will be saved
            string outputFolder = @"C:\Diagrams\Output";
            // Name of the custom stylesheet to be added
            string customStyleName = "MyCustomStyle";

            // Ensure the output directory exists
            Directory.CreateDirectory(outputFolder);

            // Process each Visio file in the input folder (adjust the pattern if needed)
            foreach (string filePath in Directory.GetFiles(inputFolder, "*.vsdx"))
            {
                // Load the diagram from file using the Diagram(string) constructor
                Diagram diagram = new Diagram(filePath);

                // ----- Apply custom stylesheet -----
                // Create a new StyleSheet instance
                StyleSheet customStyle = new StyleSheet();
                // Set the name (or any other required properties)
                customStyle.Name = customStyleName;
                // Add the stylesheet to the diagram's collection
                diagram.StyleSheets.Add(customStyle);
                // -----------------------------------

                // Build the output file path (preserving the original name)
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));

                // Save the updated diagram using the Save(string, SaveFileFormat) method
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Release resources
                diagram.Dispose();
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
