module Fussless.Defaults
open System
open System.Collections.Generic
type Vec<'T> = ResizeArray<'T>
type HashMap<'K,'V> = Dictionary<'K,'V>

type BaseDefault =
  static member get(x:int):int = 0
  static member get(x:uint) = 0u;
  static member get(x:int64):int64 = 0L
  static member get(x:uint64):uint64 = 0UL
  static member get(x:uint16):uint16 = uint16(0);
  static member get(x:int16):int16 = int16(0);
  static member get(x:byte):byte = byte(0);
  static member get(x:sbyte):sbyte = sbyte(0);
  static member get(x:float):float = 0.0
  static member get(x:float32):float32 = x
  static member get(x:bool):bool = false
  static member get(x:char):char = char(0)
  static member get(x:string) = ""
  static member get(x:'a list):'a list = []
  static member get(x:'a []):'a [] = [||]  
  static member get(x:Vec<'T>) = Vec<'T>()
  static member get(x:HashMap<'K,'V>) = HashMap<'K,'V>()
  static member get(x:HashSet<'K>) = HashSet<'K>()  
  static member get(x:unit) = ()
  static member get(x:option<'T>):option<'T> = None
  static member get(x:Result<'A,'B>):Result<'A,'B> = Error(Unchecked.defaultof<'B>)
  static member get(x:obj) = x

(*  to be generated per grammar:
type NewDefault =
  inherit BaseDefault
  static member get(x:int*string):int*string = (0,"")

printfn "%A" (NewDefault.get(Unchecked.defaultof<string>))
let x = NewDefault.get(Unchecked.defaultof<int64>);;
*)
