using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file containing password‑protected OLE objects
            string sourceFile = "input.vsdx";

            // Path where the unlocked diagram will be saved
            string targetFile = "output_unlocked.vsdx";

            // Password (decryption key) supplied for unlocking the OLE objects
            string olePassword = "YourOlePassword";

            // Load the Visio diagram (uses the mandated load rule)
            Diagram diagram = new Diagram(sourceFile);

            // Iterate through every page and shape to locate OLE (ForeignData) objects
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // ForeignData holds embedded or linked OLE data
                    if (shape.ForeignData != null)
                    {
                        // Embedded OLE data is stored in ObjectData as a byte array
                        byte[] encryptedOle = shape.ForeignData.ObjectData;

                        // Proceed only if there is embedded OLE data present
                        if (encryptedOle != null && encryptedOle.Length > 0)
                        {
                            // Decrypt the OLE data using the supplied password
                            byte[] decryptedOle = DecryptOleData(encryptedOle, olePassword);

                            // Replace the encrypted blob with the decrypted one
                            shape.ForeignData.ObjectData = decryptedOle;
                        }
                    }
                }
            }

            // Save the modified diagram (uses the mandated save rule)
            diagram.Save(targetFile, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // -------------------------------------------------------------------------
    // Placeholder for actual OLE decryption logic.
    // Replace this stub with the real implementation that can decrypt the
    // OLE byte array using the provided password.
    // -------------------------------------------------------------------------
    static byte[] DecryptOleData(byte[] encryptedData, string password)
    {
        // Example: if the OLE data is not actually encrypted, simply return it.
        // In a real scenario, integrate the appropriate decryption library here.
        return encryptedData;
    }
}
