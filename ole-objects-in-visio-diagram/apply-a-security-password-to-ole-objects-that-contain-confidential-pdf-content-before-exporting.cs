using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path after OLE PDF encryption
                string outputPath = "output.vsdx";

                // Passwords to apply to embedded PDF files
                const string userPassword = "UserPass123";
                const string ownerPassword = "OwnerPass123";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape is a foreign (OLE) object
                        if (shape.Type != TypeValue.Foreign)
                            continue;

                        // Ensure ForeignData is present
                        if (shape.ForeignData == null)
                            continue;

                        // Ensure the foreign object is an embedded OLE object
                        if (shape.ForeignData.ObjectType != ObjectType.EmbeddedObject)
                            continue;

                        // Get the raw OLE binary data
                        byte[] oleData = shape.ForeignData.ObjectData;
                        if (oleData == null || oleData.Length == 0)
                            continue;

                        // Simple PDF detection by checking the header bytes for "%PDF"
                        bool isPdf = false;
                        using (MemoryStream headerStream = new MemoryStream(oleData))
                        using (StreamReader reader = new StreamReader(headerStream))
                        {
                            char[] buffer = new char[4];
                            int read = reader.ReadBlock(buffer, 0, 4);
                            if (read == 4)
                            {
                                string header = new string(buffer);
                                if (header == "%PDF")
                                    isPdf = true;
                            }
                        }

                        if (!isPdf)
                            continue; // Skip non‑PDF OLE objects

                        // Load the PDF from the OLE data using Aspose.Pdf (fully qualified)
                        using (MemoryStream pdfInput = new MemoryStream(oleData))
                        {
                            // Aspose.Pdf.Document constructor accepts a Stream
                            Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(pdfInput);

                            // Apply password protection
                            pdfDoc.Encrypt(
                                userPassword,
                                ownerPassword,
                                Aspose.Pdf.Facades.DocumentPrivilege.AllowAll,
                                Aspose.Pdf.CryptoAlgorithm.RC4x128,
                                false); // usePdf20 = false

                            // Save the encrypted PDF back to a memory stream
                            using (MemoryStream pdfOutput = new MemoryStream())
                            {
                                pdfDoc.Save(pdfOutput);
                                // Replace the OLE object's binary data with the encrypted version
                                shape.ForeignData.ObjectData = pdfOutput.ToArray();
                            }
                        }
                    }
                }

                // Save the modified diagram (using VSDX format as an example)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }