using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load the Visio diagram file (replace with your actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Iterate through each page and count the shapes
                foreach (Page page in diagram.Pages)
                {
                    // ShapeCollection provides a Count property
                    int shapeCount = page.Shapes.Count;

                    // Output the result for the current page
                    Console.WriteLine($"Page \"{page.Name}\" (ID: {page.ID}) contains {shapeCount} shape(s).");
                }

                // Optionally, keep the console window open
                Console.WriteLine("Shape count summary completed.");
                Console.ReadKey();

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }