using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        // Create an empty diagram
        Diagram diagram = new Diagram();

        // Ensure there is at least one window to work with
        Window window = new Window();
        window.WindowType = WindowTypeValue.Drawing;
        window.WindowState = WindowStateValue.Maximized;
        window.WindowWidth = 800;
        window.WindowHeight = 600;
        diagram.Windows.Add(window);

        // Randomly flip ShowGrid and ShowGuides flags multiple times
        Random rnd = new Random();
        int iterations = 20;

        for (int i = 0; i < iterations; i++)
        {
            // Randomly choose TRUE or FALSE for each flag
            BOOL gridValue = rnd.Next(2) == 0 ? BOOL.True : BOOL.False;
            BOOL guidesValue = rnd.Next(2) == 0 ? BOOL.True : BOOL.False;

            // Apply the random values
            window.ShowGrid = gridValue;
            window.ShowGuides = guidesValue;

            // Output the current state
            Console.WriteLine($"Iteration {i + 1}: ShowGrid = {window.ShowGrid}, ShowGuides = {window.ShowGuides}");
        }

        // Optional: Save the diagram to verify that changes persist (not required for the stress test)
        // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
