using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFilePath> <outputFilePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source diagram inside a using block.
            using (Diagram srcDiagram = new Diagram(inputPath))
            {
                // Assume the first page contains the layers we need.
                Page srcPage = srcDiagram.Pages[0];

                // Locate the layer named "Export".
                Layer exportLayer = null;
                foreach (Layer layer in srcPage.PageSheet.Layers)
                {
                    if (layer.Name.Value == "Export")
                    {
                        exportLayer = layer;
                        break;
                    }
                }

                if (exportLayer == null)
                {
                    Console.Error.WriteLine("Layer named 'Export' was not found in the source diagram.");
                    return;
                }

                // Create a new empty diagram.
                Diagram newDiagram = new Diagram();

                // Ensure the new diagram has at least one window.
                if (newDiagram.Windows.Count == 0)
                {
                    Window window = new Window();
                    window.WindowType = WindowTypeValue.Drawing;
                    window.WindowState = WindowStateValue.Maximized;
                    window.WindowWidth = 1100;
                    window.WindowHeight = 700;
                    newDiagram.Windows.Add(window);
                }

                // Add a new page to the target diagram.
                Page targetPage = new Page();
                newDiagram.Pages.Add(targetPage);

                // Iterate through all shapes on the source page.
                foreach (Shape shape in srcPage.Shapes)
                {
                    // Determine if the shape belongs to the Export layer.
                    string layerMember = shape.LayerMem.LayerMember.Value;
                    bool belongsToExport = false;

                    if (!string.IsNullOrEmpty(layerMember))
                    {
                        string[] members = layerMember.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string mem in members)
                        {
                            if (mem.Trim() == exportLayer.IX.ToString())
                            {
                                belongsToExport = true;
                                break;
                            }
                        }
                    }

                    // If the shape is part of the Export layer, copy it to the new page.
                    if (belongsToExport)
                    {
                        // Add the shape to the target page's shape collection.
                        targetPage.Shapes.Add(shape);
                    }
                }

                // Save the new diagram containing only the Export‑layer shapes.
                newDiagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}