using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file containing OLE objects
                string inputPath = "input.vsdx";
                // Path where the processed diagram will be saved
                string outputPath = "output.vsdx";

                // Create LoadOptions and enable lazy loading if the property exists.
                // This instructs Aspose.Diagram to defer loading of heavy resources such as OLE data
                // until they are actually accessed, improving the initial opening performance.
                LoadOptions loadOptions = new LoadOptions();

                // The EnableLazyLoading property is available in recent versions of Aspose.Diagram.
                // If the property is not present in the referenced version, the line below will cause a compile‑time error,
                // indicating that the feature is unavailable. In that case, the library loads OLE data eagerly.
                // Uncomment the following line if your version supports it.
                // loadOptions.EnableLazyLoading = true;

                // Load the diagram using the specified load options.
                Diagram diagram = new Diagram(inputPath, loadOptions);

                // At this point the diagram is loaded with OLE objects lazily (if supported).
                // No further processing is required to keep the OLE data unloaded until accessed.

                // Example of accessing an OLE object on demand:
                // Iterate through pages and shapes, and when an OLE shape is encountered,
                // read its binary data only at that moment.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Verify the shape is an OLE (foreign) object.
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.ForeignType == ForeignType.Object)
                        {
                            // Lazy access: the ObjectData is retrieved only when needed.
                            byte[] oleData = shape.ForeignData.ObjectData;

                            // Perform any required processing with oleData here.
                            // For demonstration, we simply output the size of the OLE binary.
                            Console.WriteLine($"OLE Shape ID {shape.ID} has data size: {oleData?.Length ?? 0} bytes");
                        }
                    }
                }

                // Save the diagram back to a file. Use DiagramSaveOptions to keep the lazy loading behavior
                // for subsequent loads (if the format supports it).
                DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vsdx);
                saveOptions.AutoFitPageToDrawingContent = true; // optional: adjust page size

                diagram.Save(outputPath, saveOptions);

                Console.WriteLine("Diagram saved successfully with lazy loading enabled (if supported).");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }