using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio diagram
                string inputPath = "input.vsdx";

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (you can also retrieve by name)
                Page page = diagram.Pages[0];

                // Identify the target shape by its ID (replace with the actual ID)
                long targetShapeId = 1;
                Shape targetShape = page.Shapes.GetShape(targetShapeId);

                // Set the double‑click event formula for the shape
                // This example shows a simple CALLTHIS formula; adjust as needed
                targetShape.Event.EventDblClick.Ufe.F = "CALLTHIS(\"ThisDocument.ShowAlert\")";

                // Save the modified diagram to a new file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }