using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Passwords to apply to embedded PDF files
            const string userPassword = "user123";
            const string ownerPassword = "owner123";

            // Iterate through all pages and shapes
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Verify the shape is an OLE foreign object
                    if (shape.Type == TypeValue.Foreign &&
                        shape.ForeignData != null &&
                        shape.ForeignData.ObjectType == ObjectType.EmbeddedObject)
                    {
                        // Check if the OLE object is a PDF (by file name extension)
                        string sourceName = shape.ForeignData.ObjectSourceFullName;
                        if (!string.IsNullOrEmpty(sourceName) &&
                            sourceName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                        {
                            byte[] oleData = shape.ForeignData.ObjectData;
                            if (oleData != null && oleData.Length > 0)
                            {
                                // Load the PDF from the OLE byte array
                                using (MemoryStream inputPdf = new MemoryStream(oleData))
                                {
                                    // Fully qualified Aspose.Pdf types (no using Aspose.Pdf)
                                    var pdfDoc = new Aspose.Pdf.Document(inputPdf);

                                    // Apply password protection to the PDF
                                    pdfDoc.Encrypt(
                                        userPassword,
                                        ownerPassword,
                                        Aspose.Pdf.Facades.DocumentPrivilege.AllowAll,
                                        Aspose.Pdf.CryptoAlgorithm.RC4x128,
                                        false);

                                    // Save the protected PDF back to a byte array
                                    using (MemoryStream outputPdf = new MemoryStream())
                                    {
                                        pdfDoc.Save(outputPdf);
                                        shape.ForeignData.ObjectData = outputPdf.ToArray();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
