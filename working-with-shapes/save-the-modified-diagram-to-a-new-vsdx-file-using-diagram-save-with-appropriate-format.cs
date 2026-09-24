using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Example modification: set the first page size to 11 x 8.5 inches
                if (diagram.Pages.Count > 0)
                {
                    Page firstPage = diagram.Pages[0];
                    firstPage.PageSheet.PageProps.PageWidth.Value = 11.0;   // width in inches
                    firstPage.PageSheet.PageProps.PageHeight.Value = 8.5;   // height in inches
                }

                // Save the modified diagram to a new VSDX file
                string outputPath = "output_modified.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }