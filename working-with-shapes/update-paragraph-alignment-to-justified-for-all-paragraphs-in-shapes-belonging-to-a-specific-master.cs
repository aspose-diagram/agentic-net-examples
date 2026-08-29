using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Name of the master whose shapes will be updated
            string targetMasterName = "MyMaster";

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Process only shapes that are instances of the specified master
                    if (shape.Master != null && shape.Master.Name == targetMasterName)
                    {
                        // Update each paragraph's horizontal alignment to Justify
                        for (int i = 0; i < shape.Paras.Count; i++)
                        {
                            var para = shape.Paras[i];
                            para.HorzAlign.Value = HorzAlignValue.Justify;
                        }
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
