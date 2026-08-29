using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the VSDX file to be loaded
                string inputPath = "example.vsdx";

                // Load the Visio diagram from the specified file
                Diagram diagram = new Diagram(inputPath);

                // Simple verification: output the number of pages loaded
                Console.WriteLine($"Diagram loaded successfully. Pages count: {diagram.Pages.Count}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }