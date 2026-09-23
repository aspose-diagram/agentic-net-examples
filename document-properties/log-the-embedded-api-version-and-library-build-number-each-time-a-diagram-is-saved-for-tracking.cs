using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Save with logging
            SaveDiagram(diagram, outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Saves the diagram and logs version/build information
    static void SaveDiagram(Diagram diagram, string outputPath)
    {
        // Retrieve API version and build numbers
        string apiVersion = diagram.Version;
        string buildCreated = diagram.DocumentProps.BuildNumberCreated;
        string buildEdited = diagram.DocumentProps.BuildNumberEdited;

        // Log the information
        Console.WriteLine($"Saving diagram:");
        Console.WriteLine($"  API Version       : {apiVersion}");
        Console.WriteLine($"  Build Number (Created) : {buildCreated}");
        Console.WriteLine($"  Build Number (Edited)  : {buildEdited}");

        // Perform the save operation
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
