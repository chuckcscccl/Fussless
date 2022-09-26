module Calcbx
open System
open Fussless
open Calcbx
open Option

// create parser

let parser1 = make_parser();

// create lexer
Console.Write("Enter Expression: ")
let lexer1 = calcbxlexer<unit>(Console.ReadLine());
let result = parse_with(parser1,lexer1);
//printfn "Result = %A" result;;

result |> map eval |> map (printfn "Result = %A") |> ignore
