using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the original Visio file
                string visioFilePath = @"C:\Diagrams\SampleDiagram.vsdx";

                // Load the diagram from the file
                Diagram diagram = new Diagram(visioFilePath);

                // -------------------------------------------------
                // Perform field operations or other modifications here
                // Example: modify a shape's text (placeholder code)
                // -------------------------------------------------
                // if (diagram.Pages.Count > 0 && diagram.Pages[0].Shapes.Count > 0)
                // {
                //     var shape = diagram.Pages[0].Shapes[0];
                //     shape.Text.Value = "Updated Text";
                // }

                // Save the modified diagram back to its original location
                diagram.Save(visioFilePath, SaveFileFormat.Vsdx);

                // Release resources
                diagram.Dispose();

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }