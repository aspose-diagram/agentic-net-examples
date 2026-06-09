using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string sourcePath = "input.vsdx";

                // Load the existing diagram
                Diagram sourceDiagram = new Diagram(sourcePath);

                // Create a new empty diagram
                Diagram newDiagram = new Diagram();

                // Add a blank page to the new diagram (required for saving)
                Page newPage = new Page();
                newDiagram.Pages.Add(newPage);

                // Combine the source diagram into the new diagram (copies all pages, masters, shapes)
                newDiagram.Combine(sourceDiagram);

                // Determine the index of the layer named "Export" in the source diagram
                int exportLayerIndex = -1;
                // Assuming the layer exists on the first page; adjust if needed
                if (sourceDiagram.Pages.Count > 0)
                {
                    Page sourcePage = sourceDiagram.Pages[0];
                    foreach (Layer layer in sourcePage.PageSheet.Layers)
                    {
                        if (layer.Name.Value == "Export")
                        {
                            exportLayerIndex = layer.IX;
                            break;
                        }
                    }
                }

                if (exportLayerIndex == -1)
                {
                    Console.WriteLine("Export layer not found in the source diagram.");
                    return;
                }

                // Iterate through all pages and shapes in the combined diagram
                foreach (Page page in newDiagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        bool belongsToExportLayer = false;

                        // Check layer membership string (semicolon‑separated list of layer indexes)
                        if (shape.LayerMem != null && shape.LayerMem.LayerMember != null)
                        {
                            string member = shape.LayerMem.LayerMember.Value;
                            if (!string.IsNullOrEmpty(member))
                            {
                                string[] parts = member.Split(';');
                                foreach (string part in parts)
                                {
                                    if (int.TryParse(part, out int idx) && idx == exportLayerIndex)
                                    {
                                        belongsToExportLayer = true;
                                        break;
                                    }
                                }
                            }
                        }

                        // Hide shapes that are not on the Export layer
                        if (!belongsToExportLayer)
                        {
                            shape.Del = BOOL.True;
                        }
                    }
                }

                // Save the resulting diagram containing only Export‑layer shapes
                string outputPath = "ExportOnly.vsdx";
                newDiagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved to {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }