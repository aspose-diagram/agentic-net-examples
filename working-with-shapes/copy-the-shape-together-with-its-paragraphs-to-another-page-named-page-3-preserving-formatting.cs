using System;
using System.IO;
using Aspose.Diagram;

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

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the source Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page in the diagram
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Use the first page as the source page
                Page sourcePage = diagram.Pages[0];

                // Find the first shape on the source page
                Shape sourceShape = null;
                foreach (Shape s in sourcePage.Shapes)
                {
                    sourceShape = s;
                    break;
                }

                if (sourceShape == null)
                {
                    Console.Error.WriteLine("No shape found on the source page to copy.");
                    return;
                }

                // Retrieve or create the target page named "Page-3"
                Page targetPage = diagram.Pages.GetPage("Page-3");
                if (targetPage == null)
                {
                    targetPage = new Page();
                    targetPage.Name = "Page-3";
                    diagram.Pages.Add(targetPage);
                }

                // Determine the master name of the source shape (fallback to "Rectangle")
                string masterName = "Rectangle";
                if (sourceShape.Master != null && !string.IsNullOrEmpty(sourceShape.Master.Name))
                {
                    masterName = sourceShape.Master.Name;
                }

                // Add a new shape on the target page using the same master and position
                double pinX = sourceShape.XForm.PinX.Value;
                double pinY = sourceShape.XForm.PinY.Value;
                long newShapeId = targetPage.AddShape(pinX, pinY, masterName, false); // fourth argument is a bool

                // Retrieve the newly added shape instance
                Shape newShape = targetPage.Shapes.GetShape((int)newShapeId);
                if (newShape == null)
                {
                    Console.Error.WriteLine("Failed to retrieve the newly added shape.");
                    return;
                }

                // Copy formatting from source shape to the new shape
                newShape.Copy(sourceShape);

                // Preserve paragraphs (text formatting) from the source shape
                // Clear any existing paragraphs in the new shape
                newShape.Paras.Clear();

                // Copy each paragraph from source to target
                for (int i = 0; i < sourceShape.Paras.Count; i++)
                {
                    // Add the source paragraph reference to the new shape's paragraph collection
                    // This preserves paragraph formatting; Aspose.Diagram stores paragraph objects by reference
                    newShape.Paras.Add(sourceShape.Paras[i]);
                }

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
