using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Example usage:
                // Load a Visio file into a memory stream (replace with your own source).
                byte[] fileBytes = File.ReadAllBytes("input.vsdx");
                using (MemoryStream inputStream = new MemoryStream(fileBytes))
                {
                    // Update all hyperlink targets to a new URL.
                    ModifyHyperlinks(inputStream, "https://newtarget.example.com");

                    // At this point inputStream contains the updated diagram.
                    // For demonstration, write it back to a file.
                    File.WriteAllBytes("output.vsdx", inputStream.ToArray());
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Loads a diagram from the provided memory stream, updates every hyperlink's address,
        /// and writes the modified diagram back into the same stream.
        /// </summary>
        /// <param name="stream">Memory stream containing the original diagram. The stream will be reset and overwritten.</param>
        /// <param name="newAddress">The new hyperlink target URL to assign.</param>
        static void ModifyHyperlinks(MemoryStream stream, string newAddress)
        {
            // Ensure the stream is positioned at the beginning before loading.
            stream.Position = 0;

            // Load the diagram from the stream.
            using (Diagram diagram = new Diagram(stream))
            {
                // Iterate through all pages and shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Hyperlinks collection may be null; check before iterating.
                        if (shape.Hyperlinks != null)
                        {
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                // Update the hyperlink address. Hyperlink properties are cell-based, so use .Value.
                                link.Address.Value = newAddress;
                            }
                        }
                    }
                }

                // Prepare the stream for writing the updated diagram.
                stream.Position = 0;
                stream.SetLength(0);

                // Save the diagram back into the stream using VSDX format.
                diagram.Save(stream, SaveFileFormat.Vsdx);
            }

            // After saving, reset position to the beginning for any further reading.
            stream.Position = 0;
        }
    }