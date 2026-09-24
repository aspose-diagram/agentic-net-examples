using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    // Predefined width for the target shape (in inches)
    private const double TargetWidth = 2.0;

    static void Main(string[] args)
    {
        // Expect two arguments: input folder and output folder
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: BatchResize <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Create output folder if it does not exist
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Process each VSDX file in the input folder
        string[] files = Directory.GetFiles(inputFolder, "*.vsdx", SearchOption.TopDirectoryOnly);
        foreach (string filePath in files)
        {
            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify the target shape by its universal name (adjust as needed)
                        if (shape.NameU == "TargetShape")
                        {
                            // Resize the shape width to the predefined value
                            shape.XForm.Width.Value = TargetWidth;
                        }
                    }
                }

                // Save the modified diagram to the output folder (overwrite if exists)
                string fileName = Path.GetFileName(filePath);
                string outputPath = Path.Combine(outputFolder, fileName);
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Processed and saved: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}
