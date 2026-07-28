using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output Visio file path
                string outputPath = "output.vsdx";

                // Name of the master whose shapes will be processed
                string targetMasterName = "MyMaster";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape is based on the specified master
                        if (shape.Master != null && shape.Master.Name == targetMasterName)
                        {
                            // Iterate through all paragraphs of the shape
                            foreach (Para para in shape.Paras)
                            {
                                // Set paragraph horizontal alignment to Justify
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