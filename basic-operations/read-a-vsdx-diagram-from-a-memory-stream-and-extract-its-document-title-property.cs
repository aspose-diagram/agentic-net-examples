using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the VSDX file into a byte array (replace with your source as needed)
                byte[] vsdxBytes = File.ReadAllBytes("input.vsdx");

                // Create a memory stream from the byte array
                using (MemoryStream memoryStream = new MemoryStream(vsdxBytes))
                {
                    // Load the diagram from the memory stream
                    Diagram diagram = new Diagram(memoryStream);

                    // Extract the document title property
                    string title = diagram.DocumentProps.Title;

                    // Output the title
                    Console.WriteLine($"Document Title: {title}");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }