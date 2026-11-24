using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, high‑resolution images folder, output diagram path
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagram.vsdx> <highResImagesFolder> <outputDiagram.vsdx>");
            return;
        }

        string diagramPath = args[0];
        string highResFolder = args[1];
        string outputPath = args[2];

        // Guard against missing files/folders
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {diagramPath}");
            return;
        }

        if (!Directory.Exists(highResFolder))
        {
            Console.Error.WriteLine($"High‑resolution images folder not found: {highResFolder}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify foreign (image) shapes
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                        {
                            byte[] currentBytes = shape.ForeignData.Value;

                            // Load the current image to assess its resolution
                            using (MemoryStream ms = new MemoryStream(currentBytes))
                            using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(ms))
                            {
                                // Simple heuristic: treat images smaller than 500x500 as low‑resolution
                                if (img.Width < 500 || img.Height < 500)
                                {
                                    // Determine a base name for matching high‑resolution files
                                    string baseName = !string.IsNullOrEmpty(shape.NameU) ? shape.NameU : $"shape_{shape.ID}";
                                    string[] candidateFiles = Directory.GetFiles(highResFolder, baseName + ".*", SearchOption.TopDirectoryOnly);

                                    if (candidateFiles.Length > 0)
                                    {
                                        // Load the first matching high‑resolution image
                                        string highResPath = candidateFiles[0];
                                        byte[] highResBytes = File.ReadAllBytes(highResPath);

                                        // Replace the foreign data with the high‑resolution image bytes
                                        shape.ForeignData.Value = highResBytes;

                                        // Adjust shape size to match the new image dimensions
                                        using (MemoryStream hrMs = new MemoryStream(highResBytes))
                                        using (Aspose.Drawing.Image hrImg = Aspose.Drawing.Image.FromStream(hrMs))
                                        {
                                            shape.XForm.Width.Value = (double)hrImg.Width;
                                            shape.XForm.Height.Value = (double)hrImg.Height;
                                        }

                                        Console.WriteLine($"Replaced low‑resolution image in shape ID {shape.ID} with '{Path.GetFileName(highResPath)}'.");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"No high‑resolution replacement found for shape ID {shape.ID} (base name '{baseName}').");
                                    }
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}