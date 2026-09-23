using System.IO;
using System;
using System.Linq;
using Aspose.Diagram;
using Aspose.Drawing.Text;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio diagram file
            string diagramPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Get the collection of installed system fonts
            InstalledFontCollection installedFonts = new InstalledFontCollection();

            bool allFontsAvailable = true;

            // Iterate over fonts used in the diagram (explicit type required)
            foreach (Font font in diagram.Fonts)
            {
                // Check if the font name exists in the installed font families
                bool exists = installedFonts.Families.Any(f => 
                    string.Equals(f.Name, font.Name, StringComparison.OrdinalIgnoreCase));

                if (!exists)
                {
                    Console.WriteLine($"Missing font: {font.Name}");
                    allFontsAvailable = false;
                }
            }

            if (!allFontsAvailable)
            {
                throw new Exception("One or more fonts used in the diagram are not installed on the system.");
            }

            Console.WriteLine("All fonts required by the diagram are available on the system.");
            // Proceed with rendering or saving operations here...

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
