using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram (replace with your actual file path)
                string diagramPath = "input.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each layer defined on the page
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        int layerIndex = layer.IX;                     // Index of the layer
                        string layerName = layer.Name.Value;           // Human‑readable name of the layer
                        int connectorCount = 0;

                        // Examine every shape on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Connector shapes are 1‑D shapes (OneD == true)
                            if (shape.OneD)
                            {
                                // Layer membership is stored as a semicolon‑separated list of indexes
                                string memberString = shape.LayerMem.LayerMember.Value;

                                if (!string.IsNullOrEmpty(memberString))
                                {
                                    string[] memberIndexes = memberString.Split(';');
                                    foreach (string idx in memberIndexes)
                                    {
                                        if (idx == layerIndex.ToString())
                                        {
                                            connectorCount++;
                                            break; // Shape counted for this layer; no need to check other indexes
                                        }
                                    }
                                }
                            }
                        }

                        Console.WriteLine($"Layer '{layerName}' contains {connectorCount} connector shape(s).");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }