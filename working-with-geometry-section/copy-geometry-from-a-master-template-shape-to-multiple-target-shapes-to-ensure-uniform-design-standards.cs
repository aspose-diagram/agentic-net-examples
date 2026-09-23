using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source diagram and the output diagram
                string sourcePath = "source.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(sourcePath);

                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Identify the master template shape by its universal name (NameU)
                // Change "TemplateShape" to the actual NameU of your template shape
                Shape? masterShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == "TemplateShape")
                    {
                        masterShape = shape;
                        break;
                    }
                }

                if (masterShape == null)
                {
                    throw new Exception("Master template shape not found.");
                }

                // Iterate over all shapes on the page and copy geometry to target shapes
                // Here we consider shapes whose NameU starts with "Target" as the shapes to update
                foreach (Shape targetShape in page.Shapes)
                {
                    if (targetShape == masterShape) continue; // skip the master itself

                    if (targetShape.NameU != null && targetShape.NameU.StartsWith("Target"))
                    {
                        // Clear existing geometry of the target shape
                        targetShape.Geoms.Clear();

                        // Copy each geometry section from the master shape
                        foreach (Geom masterGeom in masterShape.Geoms)
                        {
                            // Clone the geometry object (deep copy)
                            Geom clonedGeom = (Geom)masterGeom.Clone();

                            // Add the cloned geometry to the target shape
                            targetShape.Geoms.Add(clonedGeom);
                        }

                        // Optionally, copy the local pin positions to keep the shape centered
                        targetShape.XForm.LocPinX.Ufe.F = "Width*0.5";
                        targetShape.XForm.LocPinY.Ufe.F = "Height*0.5";
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