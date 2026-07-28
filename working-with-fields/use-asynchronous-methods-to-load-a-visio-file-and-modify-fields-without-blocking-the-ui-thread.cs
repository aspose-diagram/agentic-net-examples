using System;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
    {
        // Asynchronous entry point
        static async Task Main(string[] args)
        {
            // Validate arguments
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioAsyncExample <inputVisioPath> <outputVisioPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                // Load the diagram without blocking the UI thread
                Diagram diagram = await LoadDiagramAsync(inputPath);

                // Modify a field in the first shape that contains at least one field
                ModifyFirstShapeField(diagram);

                // Save the modified diagram asynchronously
                await SaveDiagramAsync(diagram, outputPath);

                Console.WriteLine("Diagram processed and saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        // Loads a Visio diagram on a background thread
        private static Task<Diagram> LoadDiagramAsync(string path)
        {
            return Task.Run(() =>
            {
                // The Diagram constructor loads the file synchronously;
                // wrapping it in Task.Run makes the call non‑blocking.
                return new Diagram(path);
            });
        }

        // Saves a Visio diagram on a background thread
        private static Task SaveDiagramAsync(Diagram diagram, string path)
        {
            return Task.Run(() =>
            {
                // Save using the VSDX format (PascalCase enum member)
                diagram.Save(path, SaveFileFormat.Vsdx);
            });
        }

        // Finds the first shape with a field and updates that field's value
        private static void ModifyFirstShapeField(Diagram diagram)
        {
            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
                throw new InvalidOperationException("The diagram contains no pages.");

            Page page = diagram.Pages[0];

            // Iterate shapes to find one that has at least one field
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Fields.Count > 0)
                {
                    // Access the first field
                    Field field = shape.Fields[0];

                    // Update the displayed value
                    field.Value.Val = "Updated";

                    // Clear any formula or unit information
                    field.Value.Ufev.F = "";
                    field.Value.Ufev.Unit = MeasureConst.Undefined;

                    // Optionally clear formatting
                    field.Format.Val = "";
                    field.Format.Ufev.F = "";
                    field.Format.Ufev.Unit = MeasureConst.Undefined;

                    Console.WriteLine($"Modified field in shape ID {shape.ID} on page '{page.Name}'.");
                    return; // Modification done; exit method
                }
            }

            Console.WriteLine("No shape with fields was found in the diagram.");
        }
    }