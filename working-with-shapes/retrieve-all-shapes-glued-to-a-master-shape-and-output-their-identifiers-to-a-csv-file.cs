using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio diagram
                string diagramPath = "input.vsdx";

                // Name of the master shape whose glued shapes we want to list
                string masterShapeName = "MasterShape";

                // Output CSV file path
                string csvPath = "glued_shapes.csv";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Prepare the CSV file with a header
                using (StreamWriter writer = new StreamWriter(csvPath, false))
                {
                    writer.WriteLine("ShapeId");

                    // Iterate through all pages and shapes
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Identify shapes that are instances of the specified master
                            if (shape.Master != null && shape.Master.Name == masterShapeName)
                            {
                                // Retrieve IDs of all shapes glued to this master shape
                                long[] gluedIds = shape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);

                                // Write each glued shape ID to the CSV
                                foreach (long id in gluedIds)
                                {
                                    writer.WriteLine(id);
                                }
                            }
                        }
                    }
                }

                Console.WriteLine($"Glued shape IDs have been written to '{csvPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }