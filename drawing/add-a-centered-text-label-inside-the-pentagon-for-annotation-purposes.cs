using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Find the first shape that uses the "Pentagon" master
                Shape pentagonShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Master != null && shape.Master.Name == "Pentagon")
                    {
                        pentagonShape = shape;
                        break;
                    }
                }

                if (pentagonShape == null)
                {
                    Console.WriteLine("Pentagon shape not found.");
                    return;
                }

                // Clear any existing text and add the annotation text
                pentagonShape.Text.Value.Clear();
                pentagonShape.Text.Value.Add(new Txt("Annotation"));

                // Center the text within the pentagon
                // TxtPinX/Y define the position of the text block; 0.5 = 50% of the shape width/height
                pentagonShape.TextXForm.TxtPinX.Value = 0.5;
                pentagonShape.TextXForm.TxtPinY.Value = 0.5;

                // TxtLocPinX/Y define the local pivot point of the text block; set to 0.5 to center it
                pentagonShape.TextXForm.TxtLocPinX.Value = 0.5;
                pentagonShape.TextXForm.TxtLocPinY.Value = 0.5;

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

                Console.WriteLine("Annotation added and diagram saved as output.vsdx.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }