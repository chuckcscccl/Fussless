module Fussless.Defaults
open System
open System.Collections.Generic
type Vec<'T> = ResizeArray<'T>
type HashMap<'K,'V> = Dictionary<'K,'V>


type BaseDefault =
  static member get(x:'a list):'a list = []
  static member get(x:'a []):'a [] = [||]
  static member get(x:string) = ""  
  static member get(x:int):int = 0
  static member get(x:float):float = 0.0
  static member get(x:Vec<'T>):Vec<'T> = Vec<'T>()
  static member get(x:option<'T>):option<'T> = None


printfn "%A" (BaseDefault.get(Unchecked.defaultof<int list>))
printfn "%A" (BaseDefault.get(Unchecked.defaultof<Vec<int>>))
printfn "%A" (BaseDefault.get(Unchecked.defaultof<float>))

// extending BaseDefault?

///// type to be generated per grammar:
type NewDefault =
  inherit BaseDefault
  static member get(x:HashMap<'K,'V>) = HashMap<'K,'V>()
  static member get(x:string) = ""
  static member get(x:obj) = null

printfn "%A" (NewDefault.get(Unchecked.defaultof<string>))
printfn "%A" (NewDefault.get(Unchecked.defaultof<int>))
