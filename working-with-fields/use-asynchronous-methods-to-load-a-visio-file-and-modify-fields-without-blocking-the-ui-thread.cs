using System;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
    {
        // Entry point – async to allow awaiting I/O‑bound work.
        static async Task Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioAsyncExample <inputVisioPath> <outputVisioPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the diagram on a background thread to keep the UI thread responsive.
            Diagram diagram = await Task.Run(() => new Diagram(inputPath));

            // Iterate through all pages and shapes to update text fields.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has at least one field before accessing.
                    if (shape.Fields.Count > 0)
                    {
                        // Example: update the first field's displayed value.
                        Field field = shape.Fields[0];
                        field.Value.Val = "Updated Value";

                        // Clear any existing formatting strings.
                        field.Format.Val = "";
                        field.Format.Ufev.F = "";
                        field.Format.Ufev.Unit = MeasureConst.Undefined;

                        // Reset unit for the value part as well.
                        field.Value.Ufev.Unit = MeasureConst.Undefined;
                        field.Value.Ufev.F = "";
                    }
                }
            }

            // Save the modified diagram on a background thread.
            await Task.Run(() => diagram.Save(outputPath, SaveFileFormat.Vsdx));

            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
    }