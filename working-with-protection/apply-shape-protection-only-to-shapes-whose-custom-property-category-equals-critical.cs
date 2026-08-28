using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Determine if the shape has a custom property "Category" with value "Critical"
                        bool isCritical = false;
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == "Category" && prop.Value.Val == "Critical")
                            {
                                isCritical = true;
                                break;
                            }
                        }

                        // Apply protection only to critical shapes
                        if (isCritical)
                        {
                            shape.Protection.LockMoveX.Value = BOOL.True;
                            shape.Protection.LockMoveY.Value = BOOL.True;
                            shape.Protection.LockWidth.Value = BOOL.True;
                            shape.Protection.LockHeight.Value = BOOL.True;
                            shape.Protection.LockRotate.Value = BOOL.True;
                            shape.Protection.LockVtxEdit.Value = BOOL.True;
                        }
                    }
                }

                // Save the updated diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }