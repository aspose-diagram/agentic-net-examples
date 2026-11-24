using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input Visio file, output folder, optional DPI
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputFolder> [dpi]");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];
        int dpi = 300; // default DPI

        if (args.Length >= 3 && !int.TryParse(args[2], out dpi))
        {
            Console.Error.WriteLine("Invalid DPI value. Using default 300.");
            dpi = 300;
        }

        // Guard input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard output folder existence (create if missing)
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
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through pages with index
                for (int pageIdx = 0; pageIdx < diagram.Pages.Count; pageIdx++)
                {
                    Page page = diagram.Pages[pageIdx];

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify OLE (foreign) objects
                        if (shape.Type == TypeValue.Foreign &&
                            shape.ForeignData != null &&
                            shape.ForeignData.ObjectData != null &&
                            shape.ForeignData.ObjectData.Length > 0)
                        {
                            // Prepare image save options with custom DPI (Resolution)
                            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                            imgOptions.Resolution = (float)dpi;
                            imgOptions.ExportHiddenPage = false; // plain bool for non-BOOL property

                            // Build output file name: pageIndex_shapeId.png
                            string imageFileName = $"Page{pageIdx + 1}_Shape{shape.ID}.png";
                            string imagePath = Path.Combine(outputFolder, imageFileName);

                            // Render OLE object preview to image file
                            shape.ToImage(imagePath, imgOptions);

                            Console.WriteLine($"Rendered OLE preview: {imagePath}");
                        }
                    }
                }

                Console.WriteLine("Processing completed.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}