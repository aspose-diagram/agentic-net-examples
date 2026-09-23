using System;
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

                // Access the first page and the first shape on that page
                Page page = diagram.Pages[0];
                Shape shape = page.Shapes[0];

                // -------------------------------------------------
                // Remove all existing fields from the shape
                // -------------------------------------------------
                var fieldsToRemove = new System.Collections.Generic.List<Field>();
                foreach (Field f in shape.Fields)
                {
                    fieldsToRemove.Add(f);
                }

                foreach (Field f in fieldsToRemove)
                {
                    shape.Fields.Remove(f);
                }

                // -------------------------------------------------
                // Add a fresh set of updated fields
                // -------------------------------------------------

                // Example Field 1: Current date
                Field dateField = new Field();
                dateField.Value.Val = DateTime.Now.ToString("yyyy-MM-dd");
                // Clear any formatting strings
                dateField.Format.Val = "";
                dateField.Format.Ufev.F = "";
                dateField.Format.Ufev.Unit = MeasureConst.Undefined;
                shape.Fields.Add(dateField);

                // Example Field 2: Page number (hard‑coded for demonstration)
                Field pageNumberField = new Field();
                pageNumberField.Value.Val = "1";
                pageNumberField.Format.Val = "";
                pageNumberField.Format.Ufev.F = "";
                pageNumberField.Format.Ufev.Unit = MeasureConst.Undefined;
                shape.Fields.Add(pageNumberField);

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }