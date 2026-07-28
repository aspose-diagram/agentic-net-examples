using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Path for the resulting Visio file
                string outputPath = "output.vsdx";

                // Load the diagram from the file
                Diagram diagram = new Diagram(inputPath);

                // Iterate over each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over each layer on the page
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Disable the layer named "Grid"
                        if (layer.Name.Value == "Grid")
                        {
                            layer.Visible.Value = BOOL.False;
                        }
                    }
                }

                // Save the modified diagram as VSDX
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }