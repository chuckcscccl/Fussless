### Fussless: Rustlr Target for F\#

"Fussless" is a parser generation system for F\# written in Rust as part of the
**[Rustlr](https://crates.io/crates/rustlr)** parser generator. This package contains the .Net end of the project including the runtime parser and
routines to interpret the parsing tables generated by Rustlr.
Rustlr was originally designed to generate parsers for Rust and 
so far, only some of its capabilities are available for F# targets.  Notably,
there is no automatic generation of abstract syntax, nor is the -lrsd option
for using selective Marcus-Leermakers grammars currently available.  However,
the system does recognize the operators +, \*, ?, as well as the <\_*> and
<\_+> opertions and creates vectors (ResizeArray) for them.

It also contains an interface to lexical analyzers, which is written in C#,
although other tokenizers can be used by conforming to the AbstractLexer
interface.

Fussless can automatically generate a lexical analyzer in the form of
a .lex file from the grammar. The .lex file can be compiled by *CsLex*, which is
available at
[https://github.com/zbrad/CsLex](https://github.com/zbrad/CsLex)


The contents of this repository are as follows:

- **RuntimeParser.fs** : This is the main implementation of the F# side of
  Rustlr/Fussless.  Currently, there are limitations to the runtime parser
  as there is no error-recovery capability.  

- **absLexer.cs** : contains basic interface and utility classes to be used by
  the automatically generated lexical scanners. RuntimeParser and absLexer define the *Fussless* namespace.

- test1.grammar, calce.grammar, test1main.fs, calcemain.cs: these are
  sample grammars and "main" files that test the parsers and tokenizers
  generated for them.


#### Building Fussless

The system was contructed and tested on Mono and therefore should be
portable to any platform or development setting.

To build the basic Fussless system, do the following **once**:

1. git clone https://github.com/chuckcscccl/Fussless.git
   (or otherwise download the files from github).
   The repository contains pre-built dlls, but they may need to be recompiled
   if your version of .Net or F# is slightly different:
2. compile absLexer.cs:  **mcs absLexer.cs /t:library**
3. compile RuntimeParser.fs:  **fsharpc RuntimeParser.fs -a -r absLexer.dll**

This will create **absLexer.dll** and **RuntimeParser.dll** that are need by 
all parsers.  Unless you create your parsing applications within the Fussless
directory you should **export MONO_PATH = where/ever/you/put/Fussless/**
for mono to find these assemblies.  


3.  The repository contains the CsLex executable (lex.exe) along with its
MIT license.  However, if this does not work, download and build CsLex
(generate the lex.exe program).
   *Note: to build CsLex on newer Windows systems, you must manually create the directory lex/bin (or lex\bin) before running nmake in lex/src/.*  The lex.exe file
will be created in the bin directory by nmake.

4. Install Rust (if you don't have it) from rust-lang.org, then install
rustlr with **cargo install rustlr**.   Rustlr is found at [crates.io/crates/rustlr](https://crates.io/crates/rustlr), which also contains links to
a tutorial and its reference documentation.


#### Constructing and testing a parser.

The easiest way to construct a parser is to write a grammar then run
gnu make on the
[Makefile](https://github.com/chuckcscccl/Fussless/blob/main/Makefile)
included in Fussless.  It was written with mono
defaults and should be modified for other platforms.  If you move the
makefile outside of the Fussless directory then you should change the
FUSSLESS variable in the makefile accordingly.

To build a parser from a grammar such as test1.grammar, run

>      make GRAMMAR=test1

The GRAMMAR must be specified in the command.  This will run rustlr on the
grammar and build test1parser.dll and test1_lex.dll.  If there are additional
sources or libraries that are required to create the parser (e.g., the AST
definition) run make with a definition for 'ADDITIONAL', for example:

>      make GRAMMAR=test2 ADDITIONAL=/r:test2_ast.dll

Optionally, if you've written a main program for your grammar, one
that invokes the parser, then you can invoke make with

>      make GRAMMAR=test1 MAIN=test1main

This assumes that there's a file 'test1main.fs' and will build test1main.exe.



Although the makefile will call rustlr on the grammar, we recommend
rustlr be called separately so that it's clear if there's anything
wrong with the grammar.


##### Manaul Build Steps.

To facilitate the adaptation of Fussless on other developmen platforms, we
detail the steps that the makefile takes.

To build and test a specific example, do the following:

1. Run **rustlr test1.grammar -fsharp** (with optionally a -o directory/) at a
   shell prompt. This will create a file 'test1parser.fs' and a file 'test1.lex'

2. compile the generated parser: **fsharpc test1parser.fs -r RuntimeParser.dll**

3. Build the generated lexer:
   - lex.exe test1.lex   (where lex.exe is the CsLex executable)
   - mcs test1_lex.cs /t:library /r:absLexer.dll

4. Build and run the test program:
   - **fsharpc test1main.fs -r test1parser.dll -r test1_lex.dll**
   - (mono) test1main.exe  (enter 3+2*5 or similar expression at the prompt)


<p>

A [tutorial](https://cs.hofstra.edu/~cscccl/rustlr_project/chapterfs.html) on
how to write grammars for Fussless is available.
