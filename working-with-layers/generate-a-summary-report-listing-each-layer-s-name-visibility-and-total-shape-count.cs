using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the Visio file path.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: <program> <visio-file-path>");
            return;
        }

        string visioPath = args[0];
        // Verify that the input file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Iterate through each page in the document.
            foreach (Page page in diagram.Pages)
            {
                // Access the collection of layers defined for the current page.
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Prepare a counter for shapes belonging to this layer.
                    int shapeCount = 0;

                    // Iterate over all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the layer membership string (e.g., "0;2;5").
                        string member = shape.LayerMem?.LayerMember?.Value;

                        // If the shape is assigned to any layers, check for the current layer index.
                        if (!string.IsNullOrEmpty(member))
                        {
                            // Split the semicolon‑separated list and compare each entry.
                            string[] indices = member.Split(';');
                            foreach (string idx in indices)
                            {
                                // Increment the counter when the layer index matches.
                                if (idx == layer.IX.ToString())
                                {
                                    shapeCount++;
                                    break; // No need to check further indices for this shape.
                                }
                            }
                        }
                    }

                    // Convert the BOOL enum to a readable string.
                    string visibility = layer.Visible.Value == BOOL.True ? "True" : "False";

                    // Output the summary line for the current layer.
                    Console.WriteLine($"Page: {page.Name} | Layer: {layer.Name.Value} | Visible: {visibility} | ShapeCount: {shapeCount}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}