using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

public class Program
{
    // Uniform paragraph margin values (in inches)
    private const double LeftMargin = 0.1;
    private const double RightMargin = 0.1;
    private const double FirstIndent = 0.0;
    private const double SpaceBefore = 0.0;
    private const double SpaceAfter = 0.0;
    private const double LineSpacing = 0.0;

    public static void Main(string[] args)
    {
        try
        {

            // Input and output folder paths can be passed as command‑line arguments.
            string inputFolder = args.Length > 0 ? args[0] : "InputDiagrams";
            string outputFolder = args.Length > 1 ? args[1] : "OutputDiagrams";

            // Ensure the output directory exists.
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Get all Visio files (VSDX) in the input folder.
            string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx");

            foreach (string filePath in diagramFiles)
            {
                try
                {
                    // Load the diagram.
                    Diagram diagram = new Diagram(filePath);

                    // Iterate through each page.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted.
                            if (shape.Del == BOOL.True)
                                continue;

                            // Apply uniform paragraph margins to all paragraphs of the shape.
                            foreach (Para para in shape.Paras)
                            {
                                para.IndLeft.Value = LeftMargin;
                                para.IndRight.Value = RightMargin;
                                para.IndFirst.Value = FirstIndent;
                                para.SpBefore.Value = SpaceBefore;
                                para.SpAfter.Value = SpaceAfter;
                                para.SpLine.Value = LineSpacing;
                            }
                        }
                    }

                    // Build the output file path.
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                    string outputPath = Path.Combine(outputFolder, fileNameWithoutExt + "_updated.vsdx");

                    // Save the modified diagram.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);

                    // Dispose the diagram to release resources.
                    diagram.Dispose();

                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
