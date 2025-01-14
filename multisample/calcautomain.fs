open System
open Fussless
open Calcautofs.Parser

// create parser

let parser1 = make_parser();

let input = "1.2e-10/3.14; 3==2+1; -5-(4-2)*5; let x = 10 in 2+x; let x = 1 in (x+ (let x=10 in x+x) + x); (let x = 2 in x+x) + x; (let x = 4 in x/2) + (let x=10 in x*(let y=100 in y/x)); -5*2; 5-3-2;";

// create lexer
let lexer1 = calcautofslexer<unit>(input);

let result = parse_with(parser1,lexer1);

printfn "Result = %A" result;;

