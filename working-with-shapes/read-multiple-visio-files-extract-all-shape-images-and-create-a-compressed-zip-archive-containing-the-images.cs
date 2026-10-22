using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: at least one input Visio file and an output ZIP path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisio1> [<inputVisio2> ...] <outputZipPath>");
            return;
        }

        // The last argument is the output ZIP file path
        string zipPath = args[args.Length - 1];

        // Collect input file paths (all arguments except the last one)
        string[] inputFiles = new string[args.Length - 1];
        Array.Copy(args, inputFiles, args.Length - 1);

        // Verify each input file exists
        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                return;
            }
        }

        try
        {
            // Create (or overwrite) the ZIP archive
            using (FileStream zipToOpen = new FileStream(zipPath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
            {
                // Process each Visio file
                foreach (string visioPath in inputFiles)
                {
                    try
                    {
                        // Load the Visio diagram
                        using (Diagram diagram = new Diagram(visioPath))
                        {
                            int pageIndex = 0; // Manual page counter since Page.PageNum does not exist

                            // Iterate through all pages
                            foreach (Page page in diagram.Pages)
                            {
                                pageIndex++;

                                // Iterate through all shapes on the page
                                foreach (Shape shape in page.Shapes)
                                {
                                    // Skip deleted shapes
                                    if (shape.Del == BOOL.True)
                                        continue;

                                    // Identify foreign (image) shapes
                                    if (shape.Type == TypeValue.Foreign)
                                    {
                                        // Retrieve the raw image bytes from the shape
                                        byte[] imageData = shape.ForeignData?.Value;

                                        // Ensure we have image data to write
                                        if (imageData != null && imageData.Length > 0)
                                        {
                                            // Build a unique entry name: <VisioFileName>_Page<pageIndex>_Shape<shape.ID>.png
                                            string safeVisioName = Path.GetFileNameWithoutExtension(visioPath);
                                            string entryName = $"{safeVisioName}_Page{pageIndex}_Shape{shape.ID}.png";

                                            // Create a new entry in the ZIP archive
                                            ZipArchiveEntry entry = archive.CreateEntry(entryName, CompressionLevel.Optimal);
                                            using (Stream entryStream = entry.Open())
                                            {
                                                entryStream.Write(imageData, 0, imageData.Length);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log any errors that occur while processing an individual Visio file
                        Console.Error.WriteLine($"Error processing file '{visioPath}': {ex.Message}");
                    }
                }
            }

            Console.WriteLine($"Image extraction completed. ZIP archive created at: {zipPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur while creating the ZIP archive
            Console.Error.WriteLine($"Failed to create ZIP archive '{zipPath}': {ex.Message}");
        }
    }
}