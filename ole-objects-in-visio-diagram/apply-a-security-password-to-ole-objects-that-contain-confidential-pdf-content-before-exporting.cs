using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file containing OLE objects
                string inputPath = "input.vsdx";
                // Output Visio file after OLE PDF encryption
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Passwords to apply to embedded PDF OLE objects
                string userPassword = "user123";
                string ownerPassword = "owner123";

                // Iterate through all pages and shapes
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Verify the shape is an OLE foreign object
                        if (shape.Type == TypeValue.Foreign &&
                            shape.ForeignData != null &&
                            shape.ForeignData.ForeignType == ForeignType.Object)
                        {
                            // Ensure there is embedded binary data
                            if (shape.ForeignData.ObjectData != null && shape.ForeignData.ObjectData.Length > 0)
                            {
                                // Simple format detection: check file extension in source name
                                string sourceName = shape.ForeignData.ObjectSourceFullName ?? string.Empty;
                                if (sourceName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                                {
                                    // Load the embedded PDF from the OLE data
                                    using (MemoryStream pdfStream = new MemoryStream(shape.ForeignData.ObjectData))
                                    {
                                        // Aspose.Pdf Document (fully qualified to avoid namespace conflict)
                                        Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(pdfStream);

                                        // Apply password protection
                                        pdfDoc.Encrypt(
                                            userPassword,
                                            ownerPassword,
                                            Aspose.Pdf.Facades.DocumentPrivilege.AllowAll,
                                            Aspose.Pdf.CryptoAlgorithm.RC4x128,
                                            false);

                                        // Save the encrypted PDF back to a memory stream
                                        using (MemoryStream encryptedStream = new MemoryStream())
                                        {
                                            pdfDoc.Save(encryptedStream);
                                            // Replace the OLE object's binary data with the encrypted version
                                            shape.ForeignData.ObjectData = encryptedStream.ToArray();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }