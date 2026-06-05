using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Prefix to prepend to the EventData1 formula
                string prefix = "Prefix_";

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has an Event section
                        if (shape.Event == null)
                            continue;

                        // Use reflection to find the EventData1 cell (if it exists)
                        var eventDataProp = shape.Event.GetType().GetProperty("EventData1");
                        if (eventDataProp == null)
                            continue; // No EventData1 cell on this shape

                        var eventCell = eventDataProp.GetValue(shape.Event);
                        if (eventCell == null)
                            continue;

                        // Access the Ufe property of the cell
                        var ufeProp = eventCell.GetType().GetProperty("Ufe");
                        var ufe = ufeProp?.GetValue(eventCell);
                        if (ufe == null)
                            continue;

                        // Access the formula string (F) and prepend the prefix
                        var fProp = ufe.GetType().GetProperty("F");
                        var currentFormula = fProp?.GetValue(ufe) as string;
                        if (currentFormula == null)
                            continue;

                        fProp.SetValue(ufe, prefix + currentFormula);
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