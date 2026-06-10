using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be loaded.
                // Adjust the file name or use a command‑line argument as needed.
                string inputPath = "sample.vsdx";

                // Load the diagram inside a using block to ensure resources are released.
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // The Diagram object is now available for further processing.
                    Console.WriteLine($"Diagram loaded successfully. Page count: {diagram.Pages.Count}");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }