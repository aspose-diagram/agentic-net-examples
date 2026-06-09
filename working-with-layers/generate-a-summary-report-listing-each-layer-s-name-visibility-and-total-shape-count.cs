using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (replace with actual file path)
                string filePath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each layer on the current page
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        int shapeCount = 0;

                        // Iterate through each shape on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Get the layer membership string (e.g., "0;2;5")
                            string member = shape.LayerMem.LayerMember.Value;

                            if (string.IsNullOrEmpty(member))
                                continue;

                            // Split the membership string into individual layer indexes
                            string[] indexes = member.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                            // Check if the current layer's index is present in the shape's layer membership
                            foreach (string idx in indexes)
                            {
                                if (int.TryParse(idx, out int layerIndex) && layerIndex == layer.IX)
                                {
                                    shapeCount++;
                                    break;
                                }
                            }
                        }

                        // Determine visibility status
                        bool isVisible = layer.Visible.Value == BOOL.True;

                        // Output the summary for the current layer
                        Console.WriteLine($"Page: {page.Name} | Layer: {layer.Name.Value} | Visible: {isVisible} | Shape Count: {shapeCount}");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }