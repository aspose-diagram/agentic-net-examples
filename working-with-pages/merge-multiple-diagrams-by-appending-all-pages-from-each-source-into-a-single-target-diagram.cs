using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Paths of the source Visio files to be merged.
            // Adjust these paths as needed or retrieve them from command‑line arguments.
            string[] sourceFiles = new string[]
            {
                "Diagram1.vsdx",
                "Diagram2.vsdx",
                "Diagram3.vsdx"
            };

            // Path for the merged output diagram.
            string outputFile = "MergedDiagram.vsdx";

            // Load the first diagram as the target container.
            using (Diagram target = new Diagram(sourceFiles[0]))
            {
                // Iterate over the remaining source diagrams and combine them into the target.
                for (int i = 1; i < sourceFiles.Length; i++)
                {
                    using (Diagram src = new Diagram(sourceFiles[i]))
                    {
                        // Combine merges all pages and masters from src into target.
                        target.Combine(src);
                    }
                }

                // Save the merged diagram to the specified file in VSDX format.
                target.Save(outputFile, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Merged diagram saved to: {outputFile}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
