using System;
using System.IO;
using Aspose.Diagram;

class HeaderComparison
{
    static void Main()
    {
        try
        {

            // Paths to the two Visio diagrams to compare
            string diagramPath1 = "Diagram1.vsdx";
            string diagramPath2 = "Diagram2.vsdx";

            // Load the diagrams using Aspose.Diagram constructors (lifecycle rule)
            Diagram diagram1 = new Diagram(diagramPath1);
            Diagram diagram2 = new Diagram(diagramPath2);

            // Retrieve the left header text from each diagram
            string headerLeft1 = diagram1.HeaderFooter.HeaderLeft;
            string headerLeft2 = diagram2.HeaderFooter.HeaderLeft;

            // Prepare the log file
            string logPath = "HeaderComparisonLog.txt";
            using (StreamWriter logWriter = new StreamWriter(logPath, false))
            {
                // Compare the header texts and write differences (if any)
                if (headerLeft1 == headerLeft2)
                {
                    logWriter.WriteLine("HeaderLeft texts are identical.");
                    logWriter.WriteLine($"HeaderLeft: \"{headerLeft1}\"");
                }
                else
                {
                    logWriter.WriteLine("HeaderLeft texts differ:");
                    logWriter.WriteLine($"Diagram 1 HeaderLeft: \"{headerLeft1}\"");
                    logWriter.WriteLine($"Diagram 2 HeaderLeft: \"{headerLeft2}\"");
                }
            }

            // Clean up resources
            diagram1.Dispose();
            diagram2.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
