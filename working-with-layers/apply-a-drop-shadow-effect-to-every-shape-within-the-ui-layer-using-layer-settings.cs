using System;
using System.IO;
using System.Linq; // Required for Contains on string arrays.
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path – adjust as needed.
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path.
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Find the index of the layer named "UI".
            int uiLayerIndex = -1;
            // Layers are stored in the PageSheet of each page; use the first page for lookup.
            if (diagram.Pages.Count > 0)
            {
                Page firstPage = diagram.Pages[0];
                foreach (Layer layer in firstPage.PageSheet.Layers)
                {
                    // Compare the layer name (Str2Value) with the target name.
                    if (layer.Name.Value == "UI")
                    {
                        uiLayerIndex = layer.IX; // IX is the zero‑based index of the layer.
                        break;
                    }
                }
            }

            // If the UI layer was not found, report and exit.
            if (uiLayerIndex == -1)
            {
                Console.Error.WriteLine("Layer 'UI' not found in the diagram.");
                return;
            }

            // Convert the layer index to string for comparison with shape membership strings.
            string uiLayerIndexStr = uiLayerIndex.ToString();

            // Iterate all pages and shapes to apply the drop shadow to shapes on the UI layer.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True) continue;

                    // Ensure the shape has a layer membership cell.
                    if (shape.LayerMem?.LayerMember == null) continue;

                    // Check if the shape belongs to the UI layer (semicolon‑separated list).
                    string memberValue = shape.LayerMem.LayerMember.Value;
                    // Exact match or part of a list (e.g., "0;2;5").
                    bool isInUiLayer = memberValue.Split(';', StringSplitOptions.RemoveEmptyEntries)
                                                  .Contains(uiLayerIndexStr);
                    if (!isInUiLayer) continue;

                    // Apply a simple drop shadow.
                    shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;   // Enable simple shadow.
                    shape.Fill.ShdwForegnd.Value = "#000000";                     // Shadow color: black.
                    shape.Fill.ShdwForegndTrans.Value = 0.3;                     // 30 % transparent.
                    shape.Fill.ShapeShdwOffsetX.Value = 0.1;                     // Horizontal offset.
                    shape.Fill.ShapeShdwOffsetY.Value = 0.1;                     // Vertical offset.
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with drop shadows applied: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}