using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram from file
                Diagram diagram = new Diagram("input.vsdx");

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check each custom property (Prop) of the shape
                        foreach (Prop prop in shape.Props)
                        {
                            // If a property named "FixedAngle" is set to "True"
                            if (prop.Name == "FixedAngle" &&
                                string.Equals(prop.Value.Val, "True", StringComparison.OrdinalIgnoreCase))
                            {
                                // Apply rotation lock protection to the shape
                                shape.Protection.LockRotate.Value = BOOL.True;

                                // Additional protection can be added here if required
                                // e.g., shape.Protection.LockAspect.Value = BOOL.True;
                            }
                        }
                    }
                }

                // Save the modified diagram to a new file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }