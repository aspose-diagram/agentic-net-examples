using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be loaded
                string inputPath = "example.vsdx";

                // Load the diagram from the file
                Diagram diagram = new Diagram(inputPath);

                // Specify the page index (0‑based) and the unique shape ID you want to retrieve
                int pageIndex = 0;          // first page
                long shapeId = 12345;       // replace with the actual shape ID

                // Get the page
                Page page = diagram.Pages[pageIndex];

                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Example processing: output some basic information about the shape
                Console.WriteLine($"Shape ID: {shape.ID}");
                Console.WriteLine($"Shape Name: {shape.Name}");
                Console.WriteLine($"Shape Master: {shape.Master?.Name ?? "None"}");
                Console.WriteLine($"Shape Text: {shape.Text.Value.ToString()}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }