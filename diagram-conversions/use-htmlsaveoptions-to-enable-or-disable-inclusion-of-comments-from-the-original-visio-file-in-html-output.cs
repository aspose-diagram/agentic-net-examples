using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML export options
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                // Set to true to include comments, false to exclude them
                htmlOptions.IsExportComments = true; // change to false to disable comment export

                // Export the diagram to HTML with the specified options
                string outputPath = "output.html";
                diagram.Save(outputPath, htmlOptions);

                Console.WriteLine($"Diagram exported to HTML at: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }