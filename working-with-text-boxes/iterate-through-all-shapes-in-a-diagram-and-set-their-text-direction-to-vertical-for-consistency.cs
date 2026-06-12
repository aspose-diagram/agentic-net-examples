using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths
        string inputPath;
        string outputPath;

        if (args.Length >= 2)
        {
            inputPath = args[0];
            outputPath = args[1];
        }
        else
        {
            Console.Write("Enter the path of the Visio file to process: ");
            inputPath = Console.ReadLine();

            Console.Write("Enter the path where the modified file should be saved: ");
            outputPath = Console.ReadLine();
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes, setting text direction to vertical
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the TextBlock exists before setting its direction
                    if (shape.TextBlock != null)
                    {
                        shape.TextBlock.TextDirection.Value = TextDirectionValue.Vertical;
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("All shape text directions set to vertical and diagram saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
