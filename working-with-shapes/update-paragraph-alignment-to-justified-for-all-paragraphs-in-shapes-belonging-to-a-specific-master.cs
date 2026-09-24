using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Name of the master whose shapes' paragraphs will be updated
                string targetMasterName = "MyMaster";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Process only shapes that are based on the specified master
                        if (shape.Master != null && shape.Master.Name == targetMasterName)
                        {
                            // Update each paragraph's horizontal alignment to Justify
                            foreach (Para para in shape.Paras)
                            {
                                para.HorzAlign.Value = HorzAlignValue.Justify;
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