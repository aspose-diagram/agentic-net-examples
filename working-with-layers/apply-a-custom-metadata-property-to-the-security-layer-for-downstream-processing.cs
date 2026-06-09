using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                bool securityLayerFound = false;

                // Iterate through all pages and their layers to locate the "Security" layer
                foreach (Page page in diagram.Pages)
                {
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name.Value == "Security")
                        {
                            // Append custom metadata to the layer name.
                            // This approach embeds metadata directly in the layer identifier,
                            // which can be parsed later by downstream processes.
                            layer.Name.Value = "Security|CustomMeta=DownstreamProcessing";

                            securityLayerFound = true;
                            break; // Exit inner loop once the target layer is processed
                        }
                    }

                    if (securityLayerFound)
                        break; // Exit outer loop if the layer has been found
                }

                if (!securityLayerFound)
                    throw new Exception("The 'Security' layer was not found in the diagram.");

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }