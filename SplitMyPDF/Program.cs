using System;

using CliWrap;
using CliWrap.Buffered;
using Kevsoft.PDFtk;
using SplitMyPDF;
using SplitMyPDF.Models;
using static SplitMyPDF.Constants;

for (int i = 0; i < args.Length; i++)
{
    if (i == 0) INPUT_PDF = args[0];
    if (i == 1) OUTPUT_FOLDER = args[1];
}

/////////////////////////
// Test that files exist
/////////////////////////
int numOfMissingFiles = 0;
if (!File.Exists(PATH_TO_PDFTK))
{
    Console.WriteLine($"PDFTK binary not found. Location given: [{PATH_TO_PDFTK}]");
    numOfMissingFiles++;
}
if (!File.Exists(INPUT_PDF))
{
    Console.WriteLine($"Input PDF not found. Location given: [{INPUT_PDF}]");
    numOfMissingFiles++;
}
if (!File.Exists(OUTPUT_INSTRUCTIONS))
{
    Console.WriteLine($"Output instructions not found. Location given: [{OUTPUT_INSTRUCTIONS}]");
    numOfMissingFiles++;
}

if (numOfMissingFiles > 0)
{
    throw new ArgumentException($"Failed: {numOfMissingFiles} missing files.");
}

/////////////////////
// Load instructions
/////////////////////
IList<Instruction> instructions = Instructions.Parse(OUTPUT_INSTRUCTIONS);

//////////////////
// Process PDF
//////////////////

// Get PDF data
var pdftk = new PDFtk();
var pdfBytes = await File.ReadAllBytesAsync(INPUT_PDF);


// Debug
string debugMessageFormat = "--- {0} ---\n"
                      + "Output Directory: {1}\n"
                      + "Name: {2}\n"
                      + "Pages: {3}\n"
                      + "Path: {4}\n"
                      + "Status: {5}\n"
                      + "---\n";

// Parse instructions to generate new PDFs
int instructionNumber = 1;
foreach (var instruction in instructions)
{
    // Compile a new pdf comprised of the specified pages
    var newPdf = await pdftk.GetPagesAsync(pdfBytes, instruction.Pages);
    if (!newPdf.Success)
    {
        // Something went wrong. Possibly a page number was out-of-bounds.
        throw new Exception($"Failed to gather all pages while processing instruction #{instructionNumber}");
    }

    // Ensure destination directory exists
    Directory.CreateDirectory(Path.GetDirectoryName(instruction.Path) ?? "");
    
    // Write the file to its destination
    await File.WriteAllBytesAsync(instruction.Path, newPdf.Result);
    
    // Log information for this operation
    Console.WriteLine(
        debugMessageFormat, 
        instructionNumber,
        instruction.OutputDirectory,
        instruction.Name,
        String.Join(",", instruction.Pages),
        instruction.Path,
        File.Exists(instruction.Path) ? "Success" : "Failed");

    instructionNumber++;
}



