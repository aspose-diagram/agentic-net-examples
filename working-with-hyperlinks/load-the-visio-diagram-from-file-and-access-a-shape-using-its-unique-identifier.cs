using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be loaded
                string inputPath = "sample.vsdx";

                // Load the diagram from the specified file
                Diagram diagram = new Diagram(inputPath);

                // Retrieve the first page (index 0) of the diagram
                Page page = diagram.Pages[0];

                // Unique identifier of the shape to access (example ID)
                long shapeId = 5; // replace with the actual shape ID you need

                // Attempt to get the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);

                if (shape != null)
                {
                    // Output some basic information about the shape
                    Console.WriteLine($"Shape ID: {shape.ID}");
                    Console.WriteLine($"Shape NameU: {shape.NameU}");
                    Console.WriteLine($"Master Name: {shape.Master?.Name ?? "No master"}");
                    Console.WriteLine($"Shape Text: {shape.Text.Value.ToString()}");
                }
                else
                {
                    Console.WriteLine($"Shape with ID {shapeId} was not found on the page.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }