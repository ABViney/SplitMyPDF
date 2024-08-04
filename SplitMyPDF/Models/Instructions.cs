using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO;

namespace SplitMyPDF.Models;

/// <summary>
/// Semantically titled utility class for aggregating <see cref="Instruction"/>-related functions.
/// </summary>
public static class Instructions
{
    /// <summary>
    /// Provides a string version of the instruction formatting standards file.
    /// </summary>
    /// <returns>The text in <c>InstructionFormattingStandards.md</c>.</returns>
    public static string FileFormatStandard()
    {
        //
        string pathToInstructionFormattingStandards =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "InstructionFormattingStandards.md");
        string instructionFormattingStandards = File.ReadAllText(pathToInstructionFormattingStandards);
        return instructionFormattingStandards;
    }
    
    /// <summary>
    /// Generate a sequence of <see cref="Instruction"/> from a file.
    /// </summary>
    /// <param name="path">Path to the file being read.</param>
    /// <returns>A sequence of instructions</returns>
    /// <exception cref="MalformedLineException">An incompatible instruction was parsed.</exception>
    public static IList<Instruction> Parse(string path)
    {
        // Read instruction file
        string outputInstructions = File.ReadAllText(Constants.OUTPUT_INSTRUCTIONS);
        
        // This stores the most recent subfolder directive
        string outputDirectorySubfolder = "."; // current folder
        
        // Each instruction is on its own line and is parsed in the order they're written
        string[] rawInstructions = outputInstructions.Split("\n");
        int line = 0; // Debug
        List<Instruction> instructions = new();
        foreach (var rawInstruction in rawInstructions)
        {
            if (String.IsNullOrWhiteSpace(rawInstruction)) continue; // Skip empty line.
            
            if (rawInstruction[0] == Constants.DIRECTORY_MARKER)
            {
                // This line is a subfolder directive.
                outputDirectorySubfolder = rawInstruction.Substring(1).Trim();
            }
            else
            {
                // Instructions are formatted into two columns delimitted by a colon.
                // E.g. [instruction] = [name]:[pages]
                string[] instructionSet = rawInstruction.Trim().Split(':');
                if (instructionSet.Length != 2)
                {
                    // Incorrect instruction format
                    throw new MalformedLineException($"Invalid instruction format on line {line}. {instructionSet.Length} columns defined when only 2 are expected.");
                }

                string pdfOutputFolder = Path.Combine(Constants.OUTPUT_FOLDER, outputDirectorySubfolder);
                string name = instructionSet[0];
                int[] pages = instructionSet[1].Split(Constants.PAGE_DELIMITER)
                    .Select(rangeDefinition => Regex.Match(rangeDefinition, @"(\d+(-\d+)?)").Value)
                    .Where(rawRangeDefinition => !String.IsNullOrWhiteSpace(rawRangeDefinition))
                    .SelectMany(rawRangeDefinition =>
                    {
                        string[] pageRange = rawRangeDefinition.Split('-');
                        
                        int pageNumber = Int32.Parse(pageRange[0]);
                        int length = 1;
                        if (pageRange.Length == 2)
                        {
                            // The length is the number of pages (inclusive) between the start and end of the range.
                            // E.g. "2" would have a length of 1 — same as "2-2". "2-3" would have a length of 2.
                            length += Int32.Parse(pageRange[1]) - pageNumber;
                        }

                        return Enumerable.Range(pageNumber, length);
                    })
                    .ToArray();
                
                instructions.Add(new Instruction(pdfOutputFolder, name, pages));
                
            }
        }

        return instructions;
    }
}
