using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram from a file (replace with your actual file path)
                string diagramPath = "input.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Access the collection of layers defined for the page
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // The layer index (zero‑based) used in the shape's LayerMember string
                        string layerIndexString = layer.IX.ToString();

                        int connectorCount = 0;

                        // Iterate through all shapes on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Check if the shape is a connector (1‑D shape)
                            if (shape.OneD)
                            {
                                // Retrieve the layer membership string; may be null or empty
                                string membership = shape.LayerMem.LayerMember.Value;

                                if (!string.IsNullOrEmpty(membership))
                                {
                                    // Split the semicolon‑separated list of layer indexes
                                    string[] memberIndexes = membership.Split(';');

                                    // If the current layer index is present, count this connector
                                    foreach (string idx in memberIndexes)
                                    {
                                        if (idx == layerIndexString)
                                        {
                                            connectorCount++;
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        // Output the result for the current layer
                        Console.WriteLine($"Page \"{page.Name}\" - Layer \"{layer.Name.Value}\": {connectorCount} connector shape(s).");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }