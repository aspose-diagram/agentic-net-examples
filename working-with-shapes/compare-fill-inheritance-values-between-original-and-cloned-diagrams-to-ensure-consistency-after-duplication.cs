using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the original Visio file
        string originalPath = "original.vsdx";

        // Load the original diagram
        Diagram originalDiagram = new Diagram(originalPath);

        // Clone the diagram by saving to a memory stream and loading back
        Diagram clonedDiagram;
        using (MemoryStream ms = new MemoryStream())
        {
            originalDiagram.Save(ms, SaveFileFormat.Vsdx);
            ms.Position = 0;
            clonedDiagram = new Diagram(ms);
        }

        // Compare fill inheritance values between original and cloned diagrams
        CompareFillInheritance(originalDiagram, clonedDiagram);

        Console.WriteLine("Fill inheritance values are consistent between original and cloned diagrams.");
    }

    static void CompareFillInheritance(Diagram original, Diagram clone)
    {
        // Iterate through each page in the original diagram
        foreach (Page origPage in original.Pages)
        {
            // Find the corresponding page in the cloned diagram by ID
            Page clonedPage = clone.Pages.GetPage(origPage.ID);
            if (clonedPage == null)
                throw new Exception($"Cloned diagram is missing page with ID {origPage.ID}.");

            // Iterate through each shape on the original page
            foreach (Shape origShape in origPage.Shapes)
            {
                // Find the corresponding shape in the cloned page by ID
                Shape clonedShape = clonedPage.Shapes.GetShape(origShape.ID);
                if (clonedShape == null)
                    throw new Exception($"Cloned diagram is missing shape with ID {origShape.ID} on page {origPage.ID}.");

                // Compare FillForegnd inheritance
                string origForegnd = origShape.InheritFill.FillForegnd.Value;
                string cloneForegnd = clonedShape.InheritFill.FillForegnd.Value;
                if (origForegnd != cloneForegnd)
                    throw new Exception($"FillForegnd inheritance mismatch on shape ID {origShape.ID} (Page {origPage.ID}). Original: {origForegnd}, Clone: {cloneForegnd}");

                // Compare FillBkgnd inheritance
                string origBkgnd = origShape.InheritFill.FillBkgnd.Value;
                string cloneBkgnd = clonedShape.InheritFill.FillBkgnd.Value;
                if (origBkgnd != cloneBkgnd)
                    throw new Exception($"FillBkgnd inheritance mismatch on shape ID {origShape.ID} (Page {origPage.ID}). Original: {origBkgnd}, Clone: {cloneBkgnd}");

                // Compare FillPattern inheritance
                int origPattern = origShape.InheritFill.FillPattern.Value;
                int clonePattern = clonedShape.InheritFill.FillPattern.Value;
                if (origPattern != clonePattern)
                    throw new Exception($"FillPattern inheritance mismatch on shape ID {origShape.ID} (Page {origPage.ID}). Original: {origPattern}, Clone: {clonePattern}");
            }
        }
    }
}
