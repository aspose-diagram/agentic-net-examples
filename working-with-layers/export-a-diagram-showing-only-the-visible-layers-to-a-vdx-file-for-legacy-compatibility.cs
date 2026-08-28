using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (replace with your actual file)
                string inputPath = "input.vsdx";

                // Output VDX file path (legacy format)
                string outputPath = "output.vdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page and its layers
                foreach (Page page in diagram.Pages)
                {
                    // Access the collection of layers on the page
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Ensure only visible layers remain visible.
                        // Hidden layers are already marked with BOOL.False.
                        // No additional action required unless you want to force hide them.
                        if (layer.Visible.Value == BOOL.False)
                        {
                            // Example: explicitly keep hidden layers hidden (optional)
                            layer.Visible.Value = BOOL.False;
                        }
                    }
                }

                // Save the diagram in VDX format (legacy compatibility)
                diagram.Save(outputPath, SaveFileFormat.Vdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }