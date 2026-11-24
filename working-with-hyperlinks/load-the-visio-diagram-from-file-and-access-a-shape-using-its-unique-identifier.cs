using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before attempting to load
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify that the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Define the unique shape identifier to retrieve
                // Adjust this ID to match an existing shape in your diagram
                long shapeId = 1;

                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Output basic shape information
                Console.WriteLine($"Shape ID   : {shape.ID}");
                Console.WriteLine($"Shape Name : {shape.Name}");

                // Safely access the master name if the shape has a master
                string masterName = shape.Master != null ? shape.Master.Name : "No master associated";
                Console.WriteLine($"Master Name: {masterName}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram related errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}