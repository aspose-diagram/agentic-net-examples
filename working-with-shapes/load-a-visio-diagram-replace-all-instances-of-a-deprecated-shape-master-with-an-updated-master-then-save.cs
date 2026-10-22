using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths.
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Names of the masters to replace.
        string deprecatedMasterName = "OldMaster";   // TODO: set actual deprecated master name.
        string updatedMasterName = "NewMaster";      // TODO: set actual updated master name.

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                int pageIndex = 0; // Index of the current page for AddShape calls.

                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Collect shapes that use the deprecated master.
                    List<Shape> shapesToReplace = new List<Shape>();
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Master != null && shape.Master.Name == deprecatedMasterName)
                        {
                            shapesToReplace.Add(shape);
                        }
                    }

                    // Replace each collected shape with a new shape based on the updated master.
                    foreach (Shape oldShape in shapesToReplace)
                    {
                        // Preserve geometry of the old shape.
                        double pinX = oldShape.XForm.PinX.Value;
                        double pinY = oldShape.XForm.PinY.Value;
                        double width = oldShape.XForm.Width.Value;
                        double height = oldShape.XForm.Height.Value;

                        // Preserve text content of the old shape.
                        List<Txt> oldTexts = new List<Txt>();
                        foreach (object txtObj in oldShape.Text.Value)
                        {
                            if (txtObj is Txt txt)
                            {
                                oldTexts.Add(new Txt(txt.Text));
                            }
                        }

                        // Mark the old shape for deletion.
                        oldShape.Del = BOOL.True;

                        // Add a new shape using the updated master.
                        long newShapeId = diagram.AddShape(pinX, pinY, width, height, updatedMasterName, pageIndex);
                        Shape newShape = page.Shapes.GetShape(newShapeId);

                        // Copy text content to the new shape.
                        newShape.Text.Value.Clear();
                        foreach (Txt txt in oldTexts)
                        {
                            newShape.Text.Value.Add(txt);
                        }
                    }

                    pageIndex++;
                }

                // Save the modified diagram to the output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}