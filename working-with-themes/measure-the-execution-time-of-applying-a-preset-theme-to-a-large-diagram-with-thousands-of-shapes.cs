using System.IO;
using System;
using System.Diagnostics;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load a diagram that contains the preset theme you want to apply.
            // This diagram acts as the source for the theme.
            Diagram sourceDiagram = new Diagram("themeSource.vsdx");

            // Load the large target diagram that has thousands of shapes.
            Diagram targetDiagram = new Diagram("largeDiagram.vsdx");

            // Start measuring the time required to apply the theme.
            Stopwatch timer = Stopwatch.StartNew();

            // Apply the theme from the source diagram to the target diagram.
            targetDiagram.CopyTheme(sourceDiagram);

            // Stop the timer.
            timer.Stop();

            // Output the elapsed time in milliseconds.
            Console.WriteLine($"Theme applied in {timer.ElapsedMilliseconds} ms.");

            // Save the themed diagram (optional).
            targetDiagram.Save("largeDiagram_Themed.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
