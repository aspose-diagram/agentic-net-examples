using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            string filePath = "input.vsdx";
            Diagram diagram = new Diagram(filePath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each layer on the current page
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    int connectorCount = 0;

                    // Examine each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify connector shapes (1‑D shapes)
                        if (shape.OneD)
                        {
                            // Retrieve the layer membership string (semicolon‑separated indexes)
                            string layerMember = shape.LayerMem.LayerMember.Value;

                            if (!string.IsNullOrEmpty(layerMember))
                            {
                                // Split the membership string and check for the current layer index
                                string[] members = layerMember.Split(';');
                                foreach (string member in members)
                                {
                                    if (member == layer.IX.ToString())
                                    {
                                        connectorCount++;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    Console.WriteLine($"Layer '{layer.Name.Value}' (Index {layer.IX}) contains {connectorCount} connector shape(s).");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
