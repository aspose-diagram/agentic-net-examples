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
            string sourcePath = "input.vsdx";

            // Load the file into a memory stream
            using (FileStream fileStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            using (MemoryStream memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                memoryStream.Position = 0; // Reset stream position before loading

                // Use the Diagram constructor that accepts a Stream to load from memory
                using (Diagram diagram = new Diagram(memoryStream))
                {
                    // Example operation: you could modify the diagram here

                    // Save the diagram to a new file using the Save method
                    string outputPath = "output.vsdx";
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
