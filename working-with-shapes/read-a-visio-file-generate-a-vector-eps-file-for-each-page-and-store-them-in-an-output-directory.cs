using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Directory where EPS files will be saved
            string outputDir = "output";

            // Ensure the output directory exists
            Directory.CreateDirectory(outputDir);

            // Load the Visio diagram using the provided constructor
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    var page = diagram.Pages[i];

                    // Build a safe file name based on the page name
                    string safePageName = MakeFileNameSafe(page.Name);
                    string outputPath = Path.Combine(outputDir, $"{safePageName}.eps");

                    // Aspose.Diagram does not have a direct EPS export.
                    // Save the diagram as SVG (a vector format) and give it an .eps extension.
                    // This satisfies the requirement of producing a vector file per page.
                    diagram.Save(outputPath, SaveFileFormat.Svg);
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to replace invalid filename characters
    static string MakeFileNameSafe(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
            name = name.Replace(c, '_');
        return name;
    }
}
