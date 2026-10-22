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
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Determine if the shape has a master (i.e., it can inherit fill from a master)
                    bool hasMaster = shape.Master != null;

                    bool inheritsFill = false;

                    if (hasMaster)
                    {
                        // Compare the current fill foreground color with the inherited fill foreground color
                        // Matching values indicate the shape is using the inherited fill from its master
                        string currentFill = shape.Fill?.FillForegnd?.Value;
                        string inheritedFill = shape.InheritFill?.FillForegnd?.Value;

                        if (!string.IsNullOrEmpty(currentFill) && !string.IsNullOrEmpty(inheritedFill))
                        {
                            inheritsFill = string.Equals(currentFill, inheritedFill, StringComparison.OrdinalIgnoreCase);
                        }
                    }

                    // Log the inheritance status
                    string masterInfo = hasMaster ? $"Master: {shape.Master.Name}" : "No Master";
                    string inheritanceInfo = inheritsFill ? "Inherits Fill from Master" : "Does NOT inherit Fill from Master";

                    Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}, Name: {shape.NameU}, {masterInfo}, {inheritanceInfo}");
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