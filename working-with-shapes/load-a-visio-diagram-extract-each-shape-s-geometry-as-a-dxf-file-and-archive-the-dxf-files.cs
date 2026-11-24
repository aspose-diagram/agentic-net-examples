using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputFolder> [archivePath]");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];
        string archivePath = args.Length >= 3 ? args[2] : Path.Combine(outputFolder, "ShapesDXF.zip");

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure the output folder exists (create if necessary)
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Create a new empty diagram to hold the single shape
                        using (Diagram shapeDiagram = new Diagram())
                        {
                            // Create a new Shape instance and copy the source shape into it
                            Shape copiedShape = new Shape();
                            copiedShape.Copy(shape);

                            // Add the copied shape to the new diagram's active page
                            shapeDiagram.ActivePage.Shapes.Add(copiedShape);

                            // Build the DXF file path for this shape
                            string dxfFileName = $"Shape_{shape.ID}.dxf";
                            string dxfPath = Path.Combine(outputFolder, dxfFileName);

                            // Save the single‑shape diagram as DXF
                            shapeDiagram.Save(dxfPath, SaveFileFormat.Dxf);
                        }
                    }
                }
            }

            // Archive all generated DXF files into a zip file
            try
            {
                // Remove existing archive if it already exists
                if (File.Exists(archivePath))
                {
                    File.Delete(archivePath);
                }

                using (FileStream zipToOpen = new FileStream(archivePath, FileMode.Create))
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
                {
                    // Add each DXF file from the output folder to the archive
                    string[] dxfFiles = Directory.GetFiles(outputFolder, "*.dxf");
                    foreach (string dxfFile in dxfFiles)
                    {
                        string entryName = Path.GetFileName(dxfFile);
                        archive.CreateEntryFromFile(dxfFile, entryName);
                    }
                }

                Console.WriteLine($"DXF files have been archived to: {archivePath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error creating ZIP archive: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing Visio diagram: {ex.Message}");
        }
    }
}