using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio diagram
                string inputPath = "input.vsdx";

                // Path for the locked diagram output
                string outputPath = "output_locked.vsdx";

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and their layers
                foreach (Page page in diagram.Pages)
                {
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Find the layer named "Architecture"
                        if (layer.Name.Value == "Architecture")
                        {
                            // Hide the layer to effectively prevent editing
                            // (Visio does not expose a direct lock flag for layers)
                            layer.Visible.Value = BOOL.False;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }