using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages to find the layer named "Architecture"
                foreach (Page page in diagram.Pages)
                {
                    // Access the collection of layers on the page sheet
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Compare the layer name (case‑sensitive)
                        if (layer.Name.Value == "Architecture")
                        {
                            // Lock the layer by setting its Status to TRUE
                            // (Status is used by Aspose.Diagram to indicate a locked layer)
                            layer.Status.Value = BOOL.True;

                            // Optionally keep the layer visible
                            layer.Visible.Value = BOOL.True;
                        }
                    }
                }

                // Save the modified diagram to a new file
                string outputPath = "output_locked.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }