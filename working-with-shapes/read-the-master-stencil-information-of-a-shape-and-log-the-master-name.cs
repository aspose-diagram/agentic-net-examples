using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string visioPath = args[0];
        // Verify that the provided file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        try
        {
            // Load the Visio document.
            Diagram diagram = new Diagram(visioPath);

            // Iterate through each page in the document.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // If the shape is based on a master (i.e., not a group or foreign shape).
                    if (shape.Master != null)
                    {
                        // Retrieve the master name (universal name) from the stencil.
                        string masterName = shape.Master.NameU;

                        // Log the shape ID together with its master name.
                        Console.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}, Master Name: {masterName}");
                    }
                    else
                    {
                        // Shape does not have a master; log that information.
                        Console.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}, No master associated.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Output any errors encountered during processing.
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }
}