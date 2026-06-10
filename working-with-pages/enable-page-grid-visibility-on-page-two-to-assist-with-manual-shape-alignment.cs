using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Ensure there are at least two pages
                if (diagram.Pages.Count < 2)
                {
                    throw new Exception("The diagram does not contain a second page.");
                }

                // Access the second page (index 1) and enable its grid visibility
                Page pageTwo = diagram.Pages[1];
                pageTwo.PageSheet.PageLayout.EnableGrid.Value = BOOL.True;

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