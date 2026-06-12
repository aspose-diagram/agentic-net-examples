using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Name of the master whose shapes' paragraphs should be justified
                string targetMasterName = "MyMaster";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a master and it matches the target master name
                        if (shape.Master != null && shape.Master.Name == targetMasterName)
                        {
                            // Iterate through all paragraphs of the shape
                            for (int i = 0; i < shape.Paras.Count; i++)
                            {
                                // Set horizontal alignment to Justify
                                shape.Paras[i].HorzAlign.Value = HorzAlignValue.Justify;
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }