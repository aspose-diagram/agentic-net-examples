using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file on disk
                string inputPath = "input.vsdx";

                // Read the entire file into a byte array
                byte[] fileBytes = File.ReadAllBytes(inputPath);

                // Create a memory stream from the byte array
                using (MemoryStream memoryStream = new MemoryStream(fileBytes))
                {
                    // Load the diagram from the memory stream using the Diagram(Stream) constructor
                    Diagram diagram = new Diagram(memoryStream);

                    // Path for the output file
                    string outputPath = "output.vsdx";

                    // Save the diagram to a file in VSDX format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Diagram loaded from memory and saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }