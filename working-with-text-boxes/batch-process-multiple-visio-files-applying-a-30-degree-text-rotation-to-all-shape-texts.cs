using System;
using System.IO;
using Aspose.Diagram;

class VisioBatchTextRotate
{
    // Adjust these paths as needed
    private const string InputFolder = @"C:\Visio\Input";
    private const string OutputFolder = @"C:\Visio\Output";

    static void Main()
    {
        try
        {

            // Ensure output directory exists
            Directory.CreateDirectory(OutputFolder);

            // Process each Visio file in the input folder
            foreach (string filePath in Directory.GetFiles(InputFolder, "*.vsdx"))
            {
                // Load the diagram using the provided constructor
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Only process shapes that contain a text block
                            if (shape.Text != null && shape.Text.Value != null)
                            {
                                // Access the TextXForm object which holds text formatting
                                TextXForm textXForm = shape.TextXForm;

                                // Set the text rotation angle to 30 degrees
                                // TxtAngle is a DoubleValue; assign the numeric value directly
                                textXForm.TxtAngle.Value = 30.0;
                            }
                        }
                    }

                    // Build output file path (preserve original name)
                    string outputPath = Path.Combine(OutputFolder, Path.GetFileName(filePath));

                    // Save the modified diagram using the provided Save method
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
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
