using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Specify the path to the Visio file you want to load.
                string visioFilePath = @"C:\Diagrams\sample.vsdx";

                // Load the Visio diagram using the Diagram constructor that accepts a file name.
                Diagram diagram = new Diagram(visioFilePath);

                // At this point the Diagram object is initialized and ready for further processing.
                Console.WriteLine("Diagram loaded successfully. Pages count: " + diagram.Pages.Count);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }