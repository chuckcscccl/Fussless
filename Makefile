# run with make   GRAMMAR=grammarname   such as GRAMMAR=test1

# Configuration is for mono on linux-like environments

GRAMMAR = _NO_GRAMMAR_GIVEN_
FUSSLESS = ./
TARGETDIR = ./
FSC = fsharpc
CSC = mcs
LEX = $(FUSSLESS)lex.exe
RUSTLR = rustlr
RUSTLROPTIONS = -genlex
#MAIN = $(GRAMMAR)main
AUTOAST = false

ifneq ($(AUTOAST), true)
ifdef MAIN
$(MAIN).exe : $(MAIN).fs $(GRAMMAR)_lex.dll $(GRAMMAR)parser.dll
	$(FSC) $(MAIN).fs /r:$(GRAMMAR)_lex.dll /r:$(GRAMMAR)parser.dll /r:$(FUSSLESS)RuntimeParser.dll /r:$(FUSSLESS)absLexer.dll $(ADDITIONAL)
endif

$(GRAMMAR)_lex.dll $(GRAMMAR)parser.dll : $(GRAMMAR)_lex.cs $(GRAMMAR)parser.fs
	$(CSC) /t:library $(GRAMMAR)_lex.cs /r:$(FUSSLESS)absLexer.dll $(CSADDITIONAL)
	$(FSC) -a $(GRAMMAR)parser.fs /r:$(FUSSLESS)RuntimeParser.dll /r:$(FUSSLESS)absLexer.dll $(ADDITIONAL)

$(GRAMMAR)_lex.cs : $(GRAMMAR).lex
	$(LEX) $(GRAMMAR).lex	

$(GRAMMAR).lex $(GRAMMAR)parser.fs : $(GRAMMAR).grammar
	$(RUSTLR) $(GRAMMAR).grammar -fsharp $(RUSTLROPTIONS)
else
# AUTOAST=true
ifdef MAIN
$(MAIN).exe : $(MAIN).fs $(GRAMMAR)_lex.dll $(GRAMMAR)parser.dll $(GRAMMAR)_ast.dll
	$(FSC) $(MAIN).fs /r:$(GRAMMAR)_lex.dll /r:$(GRAMMAR)parser.dll /r:$(GRAMMAR)_ast.dll /r:$(FUSSLESS)RuntimeParser.dll /r:$(FUSSLESS)absLexer.dll $(ADDITIONAL)
endif

$(GRAMMAR)parser.dll : $(GRAMMAR)parser.fs $(GRAMMAR)_ast.dll
	$(FSC) -a $(GRAMMAR)parser.fs /r:$(GRAMMAR)_ast.dll /r:$(FUSSLESS)RuntimeParser.dll /r:$(FUSSLESS)absLexer.dll $(ADDITIONAL)

$(GRAMMAR)_ast.dll : $(GRAMMAR)_ast.fs
	$(FSC) -a $(GRAMMAR)_ast.fs /r:$(FUSSLESS)RuntimeParser.dll /r:$(FUSSLESS)absLexer.dll $(ADDITIONAL)

$(GRAMMAR)_lex.dll : $(GRAMMAR)_lex.cs
	$(CSC) /t:library $(GRAMMAR)_lex.cs /r:$(FUSSLESS)absLexer.dll $(CSADDITIONAL)

$(GRAMMAR)_lex.cs : $(GRAMMAR).lex
	$(LEX) $(GRAMMAR).lex	

$(GRAMMAR).lex $(GRAMMAR)parser.fs $(GRAMMAR)_ast.fs: $(GRAMMAR).grammar
	$(RUSTLR) $(GRAMMAR).grammar -auto -fsharp $(RUSTLROPTIONS)

endif

clean:
	rm -f $(GRAMMAR).lex $(GRAMMAR)parser.fs $(GRAMMAR)_lex.cs $(GRAMMAR)*.dll
# note: clean will delete everything built
