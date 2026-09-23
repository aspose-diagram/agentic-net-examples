using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Determine the input file path. Use the first command‑line argument if supplied,
                // otherwise fall back to a hard‑coded example file name.
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the Visio diagram.
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    Console.WriteLine($"Page: {page.Name}");

                    // Iterate through the layers defined on the current page.
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Determine visibility status.
                        string visibility = layer.Visible.Value == BOOL.True ? "Visible" : "Hidden";

                        // Output layer name and its visibility.
                        Console.WriteLine($"  Layer: {layer.Name.Value}, Visibility: {visibility}");
                    }
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }