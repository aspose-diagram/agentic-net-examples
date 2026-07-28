using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram (adjust the file path as needed)
                Diagram diagram = new Diagram("input.vsdx");

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a Props collection
                        if (shape.Props == null)
                            continue;

                        // Check if the shape contains a custom property named "Version"
                        bool hasVersionProp = false;
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == "Version")
                            {
                                hasVersionProp = true;
                                break;
                            }
                        }

                        // If the shape has the "Version" custom property, replace "v1.0" with "v2.0" in its text
                        if (hasVersionProp && shape.Text != null && shape.Text.Value != null)
                        {
                            foreach (var item in shape.Text.Value)
                            {
                                if (item is Txt txt && txt.Text != null && txt.Text.Contains("v1.0"))
                                {
                                    txt.Text = txt.Text.Replace("v1.0", "v2.0");
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }