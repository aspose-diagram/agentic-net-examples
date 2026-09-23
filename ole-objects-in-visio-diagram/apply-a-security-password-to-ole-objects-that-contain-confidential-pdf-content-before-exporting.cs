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

            // Path to the source Visio file
            string visioPath = "input.vsdx";
            // Path for the output Visio file
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Passwords to apply to confidential PDF OLE objects
            const string userPassword = "UserPass123";
            const string ownerPassword = "OwnerPass123";

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify foreign (OLE) shapes
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.ObjectData != null)
                    {
                        // Simple format detection: check file extension in ObjectSourceFullName
                        string sourceName = shape.ForeignData.ObjectSourceFullName ?? string.Empty;
                        if (sourceName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                        {
                            // Load the embedded PDF from the OLE object
                            byte[] pdfBytes = shape.ForeignData.ObjectData;
                            using (MemoryStream pdfInput = new MemoryStream(pdfBytes))
                            {
                                // Load PDF using Aspose.Pdf (fully qualified to avoid namespace conflicts)
                                Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(pdfInput);

                                // Apply password protection
                                pdfDoc.Encrypt(
                                    userPassword,
                                    ownerPassword,
                                    Aspose.Pdf.Facades.DocumentPrivilege.AllowAll,
                                    Aspose.Pdf.CryptoAlgorithm.RC4x128,
                                    false);

                                // Save the encrypted PDF back to a byte array
                                using (MemoryStream pdfOutput = new MemoryStream())
                                {
                                    pdfDoc.Save(pdfOutput);
                                    shape.ForeignData.ObjectData = pdfOutput.ToArray();
                                }
                            }
                        }
                    }
                }
            }

            // Save the modified Visio diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
