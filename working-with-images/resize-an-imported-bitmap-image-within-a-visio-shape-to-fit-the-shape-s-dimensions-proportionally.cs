using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output_resized.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Identify imported bitmap shapes (foreign objects)
                            if (shape.Type == TypeValue.Foreign &&
                                shape.ForeignData != null &&
                                shape.ForeignData.Value != null &&
                                shape.ForeignData.Value.Length > 0)
                            {
                                // Load the bitmap from the foreign data stream
                                using (MemoryStream ms = new MemoryStream(shape.ForeignData.Value))
                                {
                                    using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(ms))
                                    {
                                        // Image aspect ratio
                                        double imgAspect = (double)img.Width / img.Height;

                                        // Current shape dimensions (in inches)
                                        double shapeWidth = shape.XForm.Width.Value;
                                        double shapeHeight = shape.XForm.Height.Value;
                                        double shapeAspect = shapeWidth / shapeHeight;

                                        double newWidth, newHeight;

                                        // Fit the image proportionally within the shape bounds
                                        if (imgAspect > shapeAspect)
                                        {
                                            // Image is wider relative to shape; limit by width
                                            newWidth = shapeWidth;
                                            newHeight = shapeWidth / imgAspect;
                                        }
                                        else
                                        {
                                            // Image is taller relative to shape; limit by height
                                            newHeight = shapeHeight;
                                            newWidth = shapeHeight * imgAspect;
                                        }

                                        // Apply the new dimensions to the shape
                                        shape.XForm.Width.Value = newWidth;
                                        shape.XForm.Height.Value = newHeight;
                                    }
                                }
                            }
                        }
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved to '{outputPath}'.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }