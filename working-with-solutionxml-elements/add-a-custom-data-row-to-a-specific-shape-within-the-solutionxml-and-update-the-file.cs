using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the existing Visio file
                string inputPath = "input.vsdx";
                // Path where the modified Visio file will be saved
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Identify the target shape (example uses shape ID = 1)
                long targetShapeId = 1;
                Shape targetShape = null;

                foreach (Page page in diagram.Pages)
                {
                    // GetShape expects an integer ID; cast long to int safely
                    Shape shape = page.Shapes.GetShape((int)targetShapeId);
                    if (shape != null)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    throw new Exception($"Shape with ID {targetShapeId} not found.");
                }

                // Build custom XML that references the shape
                string customXml = $"<CustomData><ShapeID>{targetShape.ID}</ShapeID><Info>Sample data for shape</Info></CustomData>";

                // Create a new SolutionXML entry
                SolutionXML solXml = new SolutionXML();
                solXml.Name = "CustomShapeData";
                solXml.XmlValue = customXml;

                // Add the entry to the diagram's SolutionXML collection
                diagram.SolutionXMLs.Add(solXml);

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram updated and saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }