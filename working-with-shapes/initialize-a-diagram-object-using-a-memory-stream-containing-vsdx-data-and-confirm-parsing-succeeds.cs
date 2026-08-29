using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to a VSDX file (replace with an actual file path)
                string filePath = "sample.vsdx";

                // Read the file into a byte array
                byte[] vsdxData = File.ReadAllBytes(filePath);

                // Initialize a MemoryStream with the VSDX data
                using (MemoryStream memoryStream = new MemoryStream(vsdxData))
                {
                    // Load the diagram from the memory stream
                    using (Diagram diagram = new Diagram(memoryStream))
                    {
                        // Verify that the diagram was parsed successfully
                        if (diagram.Pages.Count > 0)
                        {
                            Console.WriteLine("Diagram loaded successfully. Page count: " + diagram.Pages.Count);
                        }
                        else
                        {
                            Console.WriteLine("Diagram loaded, but no pages were found.");
                            throw new Exception("Parsing failed: diagram contains no pages.");
                        }
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }