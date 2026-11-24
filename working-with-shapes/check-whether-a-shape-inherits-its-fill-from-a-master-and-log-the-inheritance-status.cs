using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Determine if the shape has a master (i.e., can inherit fill)
                    if (shape.Master != null)
                    {
                        // Compare the current fill foreground color with the inherited fill foreground color
                        string currentFill = shape.Fill.FillForegnd.Value;
                        string inheritedFill = shape.InheritFill.FillForegnd.Value;

                        bool inheritsFill = string.Equals(currentFill, inheritedFill, StringComparison.OrdinalIgnoreCase);

                        Console.WriteLine($"Page \"{page.NameU}\", Shape ID {shape.ID}, Name \"{shape.NameU}\": Fill inherited from master = {inheritsFill}");
                    }
                    else
                    {
                        // Shape does not have a master; inheritance not applicable
                        Console.WriteLine($"Page \"{page.NameU}\", Shape ID {shape.ID}, Name \"{shape.NameU}\": No master (inheritance not applicable)");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}