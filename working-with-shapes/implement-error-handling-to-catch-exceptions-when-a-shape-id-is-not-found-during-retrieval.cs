using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load a Visio diagram (replace with actual file path)
                Diagram diagram = new Diagram("sample.vsdx");

                // Access the first page (adjust index as needed)
                Page page = diagram.Pages[0];

                // Example shape ID to retrieve
                long shapeId = 12345;

                try
                {
                    // Attempt to get the shape by its ID
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // If successful, output some basic information
                    Console.WriteLine($"Shape found: ID = {shape.ID}, Name = {shape.Name}");
                }
                catch (Exception ex)
                {
                    // Handle the case where the shape ID does not exist
                    Console.WriteLine($"Error: Shape with ID {shapeId} was not found. Details: {ex.Message}");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }