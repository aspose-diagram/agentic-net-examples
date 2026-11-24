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
            Console.Error.WriteLine("Please provide the path to a Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        try
                        {
                            // Attempt to retrieve the InheritFill property.
                            // Older Visio files may not contain this property, so it can be null.
                            Fill inheritFill = shape.InheritFill;

                            if (inheritFill != null)
                            {
                                // Retrieve fill values safely, converting non‑string values to string.
                                string foreColor = inheritFill.FillForegnd != null && inheritFill.FillForegnd.Value != null
                                    ? inheritFill.FillForegnd.Value
                                    : "N/A";

                                string backColor = inheritFill.FillBkgnd != null && inheritFill.FillBkgnd.Value != null
                                    ? inheritFill.FillBkgnd.Value
                                    : "N/A";

                                string pattern = inheritFill.FillPattern != null
                                    ? inheritFill.FillPattern.Value.ToString()
                                    : "N/A";

                                Console.WriteLine($"Shape ID {shape.ID}: InheritFill - Foreground={foreColor}, Background={backColor}, Pattern={pattern}");
                            }
                            else
                            {
                                // InheritFill property is missing (null) – typical for older Visio versions
                                Console.WriteLine($"Shape ID {shape.ID}: InheritFill property is not available in this file version.");
                            }
                        }
                        catch (Exception ex)
                        {
                            // Log any unexpected errors while accessing InheritFill
                            Console.Error.WriteLine($"Shape ID {shape.ID}: Error accessing InheritFill. {ex.Message}");
                        }
                    }
                }

                // Save the diagram unchanged to demonstrate successful processing
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(inputPath) ?? string.Empty,
                    Path.GetFileNameWithoutExtension(inputPath) + "_processed.vsdx");

                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram processed and saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // General error handling for any Aspose.Diagram operation failures
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}