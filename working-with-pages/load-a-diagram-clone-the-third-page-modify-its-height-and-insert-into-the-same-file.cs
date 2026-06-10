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

                // Load the existing diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure the diagram has at least three pages (index 0‑based)
                if (diagram.Pages.Count < 3)
                {
                    Console.WriteLine("The diagram must contain at least three pages.");
                    return;
                }

                // Retrieve the third page (index 2)
                Page sourcePage = diagram.Pages[2];

                // Create a new page instance and copy the contents of the third page
                Page clonedPage = new Page();
                clonedPage.Copy(sourcePage);

                // Modify the height of the cloned page (value is in inches)
                clonedPage.PageSheet.PageProps.PageHeight.Value = 11.0; // example height

                // Insert the cloned page into the diagram
                diagram.Pages.Add(clonedPage);

                // Save the updated diagram back to a file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Third page cloned, height modified, and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }