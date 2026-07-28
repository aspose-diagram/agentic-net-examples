using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output_protected.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Look for a custom property named "Category"
                        Prop categoryProp = null;
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == "Category")
                            {
                                categoryProp = prop;
                                break;
                            }
                        }

                        // If the property exists and its value is "Critical", apply protection
                        if (categoryProp != null && categoryProp.Value.Val == "Critical")
                        {
                            // Lock movement, size, rotation, and vertex editing
                            shape.Protection.LockMoveX.Value = BOOL.True;
                            shape.Protection.LockMoveY.Value = BOOL.True;
                            shape.Protection.LockWidth.Value = BOOL.True;
                            shape.Protection.LockHeight.Value = BOOL.True;
                            shape.Protection.LockRotate.Value = BOOL.True;
                            shape.Protection.LockVtxEdit.Value = BOOL.True;

                            Console.WriteLine($"Applied protection to shape ID {shape.ID} on page '{page.Name}'.");
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }