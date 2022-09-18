module Java14fs
open System
open Fussless
open Java14fs

// create parser

let parser1 = make_parser();

// create lexer
let argv = Environment.GetCommandLineArgs();
if argv.Length<=1 then printfn "Need a .java source to parse, surely you can find one, but don't give me any of your csc17 junk"

try 
  let fd = new System.IO.FileStream(argv.[1] ,System.IO.FileMode.Open);
  let lexer1 = java14fslexer<unit>(fd);
  let result = parse_with(parser1,lexer1);
  printfn "Result = %A" result;
with | _ -> ()

