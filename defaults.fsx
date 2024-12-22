//module Fussless.Defaults
open System
open System.Collections.Generic
type Vec<'T> = ResizeArray<'T>
type HashMap<'K,'V> = Dictionary<'K,'V>


type BaseDefault =
  static member get(x:int):int = 0
  static member get(x:uint) = 0u;
  static member get(x:int64):int64 = 0L
  static member get(x:uint64):uint64 = 0UL  
  static member get(x:float):float = 0.0
  static member get(x:float32):float32 = x
  static member get(x:string) = ""
  static member get(x:'a list):'a list = []
  static member get(x:'a []):'a [] = [||]  
  static member get(x:Vec<'T>) = Vec<'T>()
  static member get(x:HashMap<'K,'V>) = HashMap<'K,'V>()
  static member get(x:unit) = ()
  static member get(x:option<'T>):option<'T> = None    
  static member get(x:obj) = x

type DefaultProvider =
  new() = {}
  member this.get(x:int):int = 0
  member this.get(x:uint) = 0u;
  member this.get(x:int64):int64 = 0L
  member this.get(x:uint64):uint64 = 0UL  
  member this.get(x:float):float = 0.0
  member this.get(x:float32):float32 = x
  member this.get(x:string) = ""
  member this.get(x:'a list):'a list = []
  member this.get(x:'a []):'a [] = [||]  
  member this.get(x:Vec<'T>) = Vec<'T>()
  member this.get(x:HashMap<'K,'V>) = HashMap<'K,'V>()
  member this.get(x:unit) = ()
  member this.get(x:option<'T>):option<'T> = None    
  member this.get(x:obj) = x  

type Specials =
  inherit DefaultProvider
  new() = {}
  member this.get(x:int*float) = (1,3.2)

printfn "%A" (BaseDefault.get(Unchecked.defaultof<int list>))
printfn "%A" (BaseDefault.get(Unchecked.defaultof<Vec<int>>))
printfn "%A" (BaseDefault.get(Unchecked.defaultof<float>))

// must have user declaration for default in grammar
// default Expr Val(0)  # auto
// default Sequence { Vec() }
// default int 1
// default int*string (0,"")
// major undertaking in rustlr implementation
// generates clauses under NewDefault
// static member get(x:Expr) = Expr.Expr_Nothing
// static member get(x:Sequence) = {Sequence._item0_ = Vec();}

type Expr = | Val of int | Plus of Expr * Expr | Times of Expr * Expr

///// type to be generated per grammar:
type NewDefault =
  inherit BaseDefault
  static member get(x:int*string):int*string = (0,"")
  static member get(x:Expr):Expr = Val(0)


printfn "%A" (NewDefault.get(Unchecked.defaultof<string>))
printfn "%A" (NewDefault.get(Unchecked.defaultof<int64>))
let x1 = NewDefault.get(Unchecked.defaultof<int64>) // :?> int64
// but x1 :?> int64 gets compiler error.  ugh!

printfn "%A" (NewDefault.get(Unchecked.defaultof<float32>))
printfn "%A" (NewDefault.get(Unchecked.defaultof<string>))
printfn "%A" (NewDefault.get(Unchecked.defaultof<int list>))
printfn "%A" (NewDefault.get(Unchecked.defaultof<option<int list>>))
printfn "%A" (NewDefault.get(Unchecked.defaultof<int*string>))
printfn "%A" (NewDefault.get(Unchecked.defaultof<Expr>))

let Special:DefaultProvider = Specials() :> DefaultProvider;

printfn "%A" (Special.get(Unchecked.defaultof<int*float>))
printfn "%A" (Special.get(Unchecked.defaultof<string>))


