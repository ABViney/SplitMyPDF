# What is this?
This is a document describing how to format instructions used by this application. 

Behind the UI, this program parses a series of text inputs as instructions which are 
used to generate the desired outputs from your PDF.

# How to manually write instructions?
Things to know:
1. The instructions for processing a PDF are read from a file.
2. Each instruction is confined to its own line.
3. Instructions are parsed left-to-right.
3. Instructions are processed line-by-line until reaching the end of the file.
4. These are the types of instructions available to write:
   - Create new PDF
   - Change output directory

## Instruction format
This document uses a variety of symbols to describe how to write your own 
instructions. There are two types of symbols: literal and figurative.
- Literal symbols are one character, describing a specific action or routine.
- Figurative symbols are encapsulated in brackets, describing a user-defined input.

### Page Selection
There are two formats for specifying a page selection:
- `[page number]` - Specifies a single page in the PDF file.
  - E.g. `11`  means page 11 of the PDF.
- `[page range]` - Specifies a series of pages
  - E.g. `4-8` means pages 4, 5, 6, 7, and 8.
  - Note: `4-4` means page 4 (not 4,4).

#### Page Range Modifiers
A `[page-range]` can be modified for conveniently targeting specific pages in the range:
- `o` - Specifies only the odd-numbered pages.
  - E.g. `4-8o` means pages 5 and 7.
- `e` - Specifies only the even-numbered pages.
  - E.g. `4-8e` means pages 4, 6, and 8.

### Pages
- `[pages]` - A sequence of **Page Selection**s.
  - E.g. `1,3-5,10-14e` means pages 1, 3, 4, 5, 10, 12, and 14

### Name
- A `[name]` describes the name to be used when writing the output PDF file.
  - E.g. `Book Cover:1` would create a new PDF named "Book Cover.pdf" in the **Output Directory**. 

### Path
- A `[path]` describes a location in your file system. There are two types of paths:
  - Relative paths, e.g. `path/to/folder` is a relative path _within_ the **Output Directory**. New subfolders _will_ automatically be created as needed.
  - Absolute paths, e.g.  `/absolute/path/to/folder` or something like is an absolute path _from the root of the file system_, completely ignoring the **Output Directory**. Folders will _not_ be created, so the location must already exist.
    - Note: On the Windows operating system, you can use `C:\absolute\path\to\folder\`.

## Instructions

### Change output destination
#### Format
- `# [path]` - Describes where to place the result of executing
  - E.g. `# Section 1` would write subsequent outputs to a `Section 1/` subfolder in the **Output Directory**.
  - E.g. `# /Book/Section 1` would write subsequent outputs to an existing `C:\Book\Section 1\` folder 

Note:`#` by itself (or `# .`) changes the output destination back to the **Output Directory**. 

### Split and Splice PDF
- `[name]:[pages]` - Create and name a new PDF using these pages.
  - E.g. `Chapter 1:27-46` would create a new PDF named `Chapter 1.pdf` that contains pages 27-46 from the original PDF.

## Sample document
In this code block are the instructions I used to split up my textbook into its individual chunks and chapters.
```text
# Extra
Cover:1-4,
Table of Contents:6,8-13
Preface:14-26
Glossary:598-607
Name Index:608-610
Company Index:611-614
Subject Index:615-619
Getting Personal:29,71,107,147,187,221,261,301,337,371,411,451,489,521,556
# Part 1 - Decision to Become an Entrepreneur
Chapter 1:30-69
# Part 2 - Developing Successful Business Ideas
Chapter 2:72-106
Chapter 3:108-145
Chapter 4:148-186
Chapter 5:188-220
Chapter 6:222-259
# Part 3 - Moving from an Idea to an Entrepreneurial Firm
Chapter 7:262-300
Chapter 8:302-336
Chapter 9:338-370
Chapter 10:372-409
# Part 4 - Managing and Growing an Entrepreneurial Firm
Chapter 11:412-450
Chapter 12:452-488
Chapter 13:490-520
Chapter 14:522-556
Chapter 15:558-597
```

My **Output Directory** after these instructions were executed looked like this:
```text
~/path/to/Textbook
├── Extra
│   ├── Company Index.pdf
│   ├── Cover.pdf
│   ├── Getting Personal.pdf
│   ├── Glossary.pdf
│   ├── Name Index.pdf
│   ├── Preface.pdf
│   ├── Subject Index.pdf
│   └── Table of Contents.pdf
├── Part 1 - Decision to Become an Entrepreneur
│   └── Chapter 1.pdf
├── Part 2 - Developing Successful Business Ideas
│   ├── Chapter 2.pdf
│   ├── Chapter 3.pdf
│   ├── Chapter 4.pdf
│   ├── Chapter 5.pdf
│   └── Chapter 6.pdf
├── Part 3 - Moving from an Idea to an Entrepreneurial Firm
│   ├── Chapter 10.pdf
│   ├── Chapter 7.pdf
│   ├── Chapter 8.pdf
│   └── Chapter 9.pdf
└── Part 4 - Managing and Growing an Entrepreneurial Firm
    ├── Chapter 11.pdf
    ├── Chapter 12.pdf
    ├── Chapter 13.pdf
    ├── Chapter 14.pdf
    └── Chapter 15.pdf
```
