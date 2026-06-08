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
                string outputPath = "output.vsdx";

                // Load the existing Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Remove all existing fields from the shape
                        // Fields collection does not have a Clear method, so remove items one by one
                        while (shape.Fields.Count > 0)
                        {
                            // Remove the first field repeatedly until the collection is empty
                            shape.Fields.Remove(shape.Fields[0]);
                        }

                        // Add a fresh set of updated fields
                        // Example: add a single field with a custom value
                        Field newField = new Field();

                        // Set the field's displayed value
                        newField.Value.Val = "UpdatedValue";

                        // Optionally clear formatting strings (if needed)
                        newField.Format.Val = "";
                        newField.Format.Ufev.F = "";
                        newField.Format.Ufev.Unit = MeasureConst.Undefined;

                        // Add the new field to the shape
                        shape.Fields.Add(newField);
                    }
                }

                // Save the modified diagram to a new file (using VSDX format)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }