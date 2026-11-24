using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages to find a diamond shape
                bool diamondFound = false;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify diamond shape by its master name
                        if (shape.Master != null && shape.Master.Name == "Diamond")
                        {
                            // Lock the aspect ratio of the diamond shape
                            shape.Protection.LockAspect.Value = BOOL.True;

                            // Example resize: set new width (in inches) while preserving aspect ratio
                            // Since aspect is locked, setting width will keep the height proportional.
                            // Here we set both to the same value for simplicity.
                            shape.XForm.Width.Value = 2.0;   // New width
                            shape.XForm.Height.Value = 2.0;  // New height (matches width)

                            diamondFound = true;
                            break; // Assuming only one diamond needs processing
                        }
                    }
                    if (diamondFound) break;
                }

                if (!diamondFound)
                {
                    Console.Error.WriteLine("No diamond shape found in the diagram.");
                }

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}