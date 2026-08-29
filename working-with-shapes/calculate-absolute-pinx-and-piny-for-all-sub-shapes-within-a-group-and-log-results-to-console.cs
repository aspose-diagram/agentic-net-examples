using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output Visio file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramGroupPinCalculator <inputFilePath> <outputFilePath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Aspose.Diagram.Shape groupShape in page.Shapes)
                {
                    // Identify group shapes
                    if (groupShape.Type == TypeValue.Group)
                    {
                        // Retrieve group's transformation values
                        double groupPinX = groupShape.XForm.PinX.Value;
                        double groupPinY = groupShape.XForm.PinY.Value;
                        double groupLocPinX = groupShape.XForm.LocPinX.Value;
                        double groupLocPinY = groupShape.XForm.LocPinY.Value;

                        // Iterate through sub‑shapes contained in the group
                        foreach (Aspose.Diagram.Shape subShape in groupShape.Shapes)
                        {
                            // Sub‑shape's local PinX/PinY
                            double subPinX = subShape.XForm.PinX.Value;
                            double subPinY = subShape.XForm.PinY.Value;

                            // Simple absolute position calculation (ignores rotation/scaling)
                            double absolutePinX = groupPinX + (subPinX - groupLocPinX);
                            double absolutePinY = groupPinY + (subPinY - groupLocPinY);

                            // Log the result
                            Console.WriteLine(
                                $"Group Shape ID {groupShape.ID}, Sub‑Shape ID {subShape.ID}: " +
                                $"Absolute PinX = {absolutePinX}, Absolute PinY = {absolutePinY}");
                        }
                    }
                }
            }

            // Save the (potentially modified) diagram to the output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }