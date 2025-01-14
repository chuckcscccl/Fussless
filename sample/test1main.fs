//module Test1
open System
open Fussless
open Test1

// create parser

let parser1 = make_parser();

// create lexer

Console.Write("Enter Expression: ");
let lexer1 = test1lexer<unit>(Console.ReadLine());

let result = parse_with(parser1,lexer1);

printfn "Result = %A" result;;

