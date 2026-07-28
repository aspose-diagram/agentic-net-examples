using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify OLE objects (Foreign shapes with Object data)
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.ForeignType == ForeignType.Object)
                        {
                            // Set display mode to icon only
                            shape.ForeignData.ShowAsIcon = BOOL.True;

                            // Customize the icon caption by updating the shape's text
                            shape.Text.Value.Clear();
                            shape.Text.Value.Add(new Txt("My OLE Icon"));
                        }
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }