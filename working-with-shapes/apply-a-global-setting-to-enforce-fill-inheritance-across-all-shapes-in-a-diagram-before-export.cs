using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Path for the exported Visio file
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to enforce fill inheritance
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Copy foreground, background and pattern fill values from the inherited fill
                    shape.Fill.FillForegnd.Value = shape.InheritFill.FillForegnd.Value;
                    shape.Fill.FillBkgnd.Value = shape.InheritFill.FillBkgnd.Value;
                    shape.Fill.FillPattern.Value = shape.InheritFill.FillPattern.Value;

                    // Copy shadow (shade) fill values from the inherited fill
                    shape.Fill.ShdwForegnd.Value = shape.InheritFill.ShdwForegnd.Value;
                    shape.Fill.ShdwPattern.Value = shape.InheritFill.ShdwPattern.Value;

                    // Copy transparency values from the inherited fill
                    shape.Fill.FillForegndTrans.Value = shape.InheritFill.FillForegndTrans.Value;
                    shape.Fill.FillBkgndTrans.Value = shape.InheritFill.FillBkgndTrans.Value;
                    shape.Fill.ShdwForegndTrans.Value = shape.InheritFill.ShdwForegndTrans.Value;
                    shape.Fill.ShdwBkgndTrans.Value = shape.InheritFill.ShdwBkgndTrans.Value;
                }
            }

            // Save the updated diagram with the same format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}