## What is this?
This is a console app I created for splitting PDFs into one or more copies. 

I used this while in school to split my textbooks into chapter-sized chunks, significantly reducing the memory overhead to open the book and making it easier to cross-reference content across chapters.

It requires setup and configuration before it can be used:
- Download a version of [PdfTK](https://www.pdflabs.com/tools/pdftk-the-pdf-toolkit/) for your system
- Update [Constants.cs](https://github.com/ABViney/SplitMyPDF/blob/master/SplitMyPDF/Constants.cs) with:
  - the location of the PdfTK binary
  - your target PDF
  - your [instructions](https://github.com/ABViney/SplitMyPDF/blob/master/SplitMyPDF/InstructionFormattingStandards.md) for how to split the PDF
  - and the output directory

