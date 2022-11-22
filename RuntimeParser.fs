module Fussless.RuntimeParser
open System
open System.Collections.Generic
open Option
//open FusslessRuntimeParser


// interface declaration:
//type HasDefault<'A> =
//  abstract Default: unit -> 'A;

type Vec<'A> = ResizeArray<'A>
type HashMap<'A,'B> = Dictionary<'A,'B>

let mutable private uid_counter:uint64 = 0UL;

///// LBox analog
type LBox<'AT> =
  {
    mutable value: 'AT;
    line : int;
    column: int;
    uid:uint64;
  }
let lbox<'AT> (v:'AT,ln:int,cn:int) =
  uid_counter <- uid_counter + 1UL;
  {
    LBox.value =v;
    line=ln;
    column=cn;
    uid=uid_counter;
  }
// Active pattern hides the lexical info:
let (|Lbox|) (b:LBox<'AT>) = Lbox(b.value)


// this is the only token type the abs parser needs to know about
type TerminalToken<'AT> =   // AT will be FLTypeDUnion
  {
    mutable sym: string;
    mutable svalue: 'AT;
    mutable line: int;
    mutable column: int;
  }
  member this.set(a,b,c,d) =
    this.sym<-a; this.svalue<-b; this.line <- c; this.column <- d;
  member this.to_string() = this.sym+"("+string(this.svalue)+")";

///////////////// runtime parser base

type StackedItem<'AT> =
  {  mutable statei: int;
     mutable svalue: 'AT;
     mutable line : int;
     mutable column : int;
  }
  member this.to_lbox() = lbox(this.svalue,this.line,this.column)
  //{LBox.value=this.svalue; line=this.line; column=this.column;}

type Stateaction = Shift of int | Reduce of int | Gotonext of int | Accept | ParseError of string;;


// runtime parser and runtime production:
type RTProduction<'AT,'ET> =
  {
    lhs : string;  // left-hand side nonterminal name
    mutable action : (RTParser<'AT,'ET>) -> 'AT; 
  }
and RTParser<'AT,'ET> =
  {
    mutable exstate: 'ET;
    mutable RSM: (Dictionary<string,Stateaction>) []; //runtime statemachine
    mutable stack: Vec<StackedItem<'AT>>;
    mutable resynch: HashSet<string>;
    mutable err_occurred: bool;
    mutable stopparsing: bool;
    mutable line: int;
    mutable column: int;
    mutable positions: Vec<int*int>;
    mutable src_id: int;
    mutable Symset: HashSet<string>;
    mutable Rules: RTProduction<'AT,'ET> [];
    mutable NextToken: unit -> TerminalToken<'AT> option;
  }
  member this.UpdateState (x:'ET) = this.exstate <- x;
  member this.State() = this.exstate;
  member this.Pop() =
    let lasti = this.stack.Count-1
    let item = this.stack.[lasti]
    this.stack.RemoveAt(lasti)
    this.positions.Add((item.line,item.column));
    item;
  member this.position(i) =
    if i<0 || i>=this.positions.Count then (0,0)
    else this.positions.[this.positions.Count-i-1]
  member this.abort (msg:string) =
    Console.Error.WriteLine(msg);
    this.err_occurred <- true;
    this.stopparsing <- true;
  member this.report_error(msg:string, showlc:bool) =
    if showlc then Console.Error.Write("Line "+string(this.line)+", Col "+string(this.column)+":");
    Console.Error.WriteLine(msg);
    this.err_occurred <- true;
    
  member this.shift(nextstate:int, lookahead:TerminalToken<'AT>) =
    this.line <- lookahead.line; this.column <- lookahead.column;
    this.stack.Add({StackedItem.statei=nextstate; svalue=lookahead.svalue; line=lookahead.line; column=lookahead.column})
    let nextsym = this.NextToken();
    if (isNone nextsym) then {TerminalToken.sym="EOF"; svalue=Unchecked.defaultof<'AT>; line=this.line; column=this.column}
    else nextsym.Value
    
  member this.reduce(ruleindex:int) =
    let rulei = this.Rules.[ruleindex]
    let semval = rulei.action(this)  // the action will pop the stack
    let sti = this.stack.Count-1;
    let newstate = this.stack.[sti].statei;
    let goton = this.RSM.[newstate].[rulei.lhs]
    this.positions.Clear()
    match goton with
      | Gotonext nsi ->
          this.stack.Add {StackedItem.statei=nsi; svalue=semval; line=this.line; column=this.column}
      | _ ->
          Console.Error.WriteLine("LR state transition table corrupted; no suitable action after reduce");
          exit(1);

    member this.get_action(lookahead:TerminalToken<'AT>) =
      if this.stack.Count=0 then None
      else
        let tos = this.stack.[this.stack.Count-1]
        try (Some(this.RSM.[tos.statei].[lookahead.sym])) with | _ -> None;

    member this.err_recover1(lookahead:TerminalToken<'AT>) =
      let lexinfo = ", line "+string(lookahead.line)+", column "+string(lookahead.column);
      this.report_error("Unexpected Token "+lookahead.sym+lexinfo,false)
      // look for resynch token, then look down stack for something that can
      // continue.  If no resynch token exists, then skip input 
      // until top state can handle the next token
      if lookahead.sym="EOF" then (this.stopparsing<-true; lookahead)
      else if this.resynch.Count>0 then
        let mutable la = lookahead
        while not(la.sym="EOF" || this.stopparsing || this.resynch.Contains(la.sym)) do
           let nextla = this.NextToken()
           if Option.isNone(nextla) then this.stopparsing <- true
           nextla |> map (fun x -> la <- x) |> ignore
        if this.resynch.Contains(la.sym) then // do one more
           //printfn "attempting error recovery: skipping past next %s ..." la.sym
           let nextla = this.NextToken()
           if Option.isNone(nextla) then this.stopparsing <- true
           nextla |> map (fun x -> la <- x) |> ignore
           //printfn "next symbol after resynch marker is %s" la.sym
        let mutable i = this.stack.Count-1
        while not(this.stopparsing) && i>=0 && Option.isNone(this.get_action(la)) do
           this.stack.RemoveAt(i)
           i<-i-1;
        if i<0 then this.stopparsing<-true
        la
      else
        let nextla = this.NextToken()
        if Option.isSome(nextla) then nextla.Value
        else (this.stopparsing<-true; lookahead)


  ////////////// core parse function
  member this.parse_core() =  // no error handlers yet
    this.stack.Clear()
    this.err_occurred <- false
    this.stopparsing <- false
    this.exstate <- Unchecked.defaultof<'ET>
    let mutable defaultresult = Unchecked.defaultof<'AT>
    let mutable result = None
    let mutable tosi = 0;
    this.stack.Add {statei=0;svalue=defaultresult;line=0;column=0;}
    let mutable action = ParseError(""); // dummy
    let eoftoken = {TerminalToken.sym="EOF";svalue=Unchecked.defaultof<'AT>; line=0; column=0;}
    let mutable lookahead = eoftoken;
    match this.NextToken() with
      | Some(tok) -> lookahead <-tok
      | None -> this.stopparsing <- true
    while not(this.stopparsing) do
      tosi <- this.stack.Count-1
      let tos = this.stack.[tosi]
      this.line <- tos.line; this.column <- tos.column;
      let currentstate = tos.statei
      let lexinfo = ", line "+string(lookahead.line)+", column "+string(lookahead.column);
      let mutable actionopt = try (Some(this.RSM.[currentstate].[lookahead.sym])) with | _ -> None;
      match actionopt with
        | Some(Shift(nextstate)) -> lookahead <- this.shift(nextstate,lookahead)
        | Some(Reduce(ri)) -> this.reduce(ri)
        | Some(Accept) ->
           this.stopparsing <- true
           if this.stack.Count<1 then this.err_occurred<-true
           else result <- Some(this.Pop().svalue)
        | Some(ParseError(msg)) -> this.abort(msg+lexinfo)
        | _ -> lookahead <- this.err_recover1(lookahead)
    // while not(stopparsing)
    result;;
    
let err_reporter1(self:RTParser<'AT,'ET>, lookahead:TerminalToken<'AT>, actionopt:Stateaction option) =
  self.report_error("Unexpected Token"+string(lookahead.sym),true);


let skeleton_production<'AT,'ET>(s) : RTProduction<'AT,'ET> =
  {
    lhs=s;
    action= fun (parser:RTParser<'AT,'ET>) -> Unchecked.defaultof<'AT>;
  };

let skeleton_parser<'AT,'ET>(e0:'ET,rlen,mlen) : RTParser<'AT,'ET> =
  let mutable rsm = Array.zeroCreate mlen
  for i in 0..mlen-1 do
    rsm.[i] <- Dictionary<string,Stateaction>()
  { exstate=e0;
    RSM= rsm;
    Rules= Array.zeroCreate rlen;
    stack= Vec<StackedItem<'AT>>();
    resynch= HashSet<string>();
    err_occurred=false;
    stopparsing=false;
    line=0;
    column=0;
    positions = Vec<int*int>();
    src_id=0;
    Symset= HashSet<string>();
    NextToken= fun () -> None;
  };


////////////// decode binary encoded stateaction

let decode_action (ncode:uint64) =
  let symi = int((ncode &&& 0x0000ffff00000000UL) >>> 32)  // symbol index
  let sti =  int((ncode &&& 0xffff000000000000UL) >>> 48)  // state index
  let satype = int(ncode &&& 0x000000000000ffffUL)  // stateaction type
  let savalue = int((ncode &&& 0x00000000ffff0000UL) >>> 16) //action value
  let action =
    match (satype,savalue) with
      | (0,si) -> Shift(si)
      | (1,si) -> Gotonext(si)
      | (2,ri) -> Reduce(ri)
      | (3,_) -> Accept
      | _ -> ParseError("parse table corrupted")
  (sti,symi,action);;


///// Tokenizer must ultimately produce terminal tokens
let convert_lt<'T> (lt:LexToken) =  // not being used
  if lt=null then None
  else Some({TerminalToken.sym = lt.token_type; svalue=(lt.token_value :?> 'T); line=lt.line; column=lt.column;})


// How to instantiate the nexttoken function of a parser?

// must take a lexer_lex.cs .dll, with class like Sample,
// call constructor with a string or a iostream,
// Then call Yylex to get a LexToken
// then call lt_convert to get a TerminalToken option
// instantiate parser.NextToken <- fun () -> lt_covert(Sample.yylex())
// make parse_with a Curried function

// But must define some kind of interface! for parse_with to take?



//fsharpc RuntimeParser.fs -a -r absLexer.dll

(*
mcs absLexer.cs /t:library
fsharpc RuntimeParser.fs -a -r absLexer.dll
./lex.exe test1.lex
mcs test1_lex.cs /t:library /r:absLexer.dll

cargo run test1.grammar -o test1parser.fs
fsharpc test1parser.fs -a -r RuntimeParser.dll

fsharpc test1main.fs -r test1parser.dll -r test1_lex.dll
*)



// This structure is for convenience: sets generated tokenizer attributes
type lexattributes =
  {
    mutable oneline_comment: string;
    mutable ml_comment_start: string;
    mutable ml_comment_end: string;
    keep_comment: bool;
    keep_newline: bool;
    keep_whitespace: bool;
    custom_defined: Vec<string*string>;
  }
  member this.set_line_comment(cm) = this.oneline_comment <- cm
  member this.set_multiline_comments(cm:string) =
    let cms = cm.Split ' '
    if cms.Length=2 then
      this.ml_comment_start <- cms.[0]
      this.ml_comment_end <- cms.[1]
  member this.add_custom(ckind,cregex) =
    this.custom_defined.Add((ckind,cregex))
//////lexattributes    
  
let default_attributes() =
  { lexattributes.oneline_comment="//";
    ml_comment_start = "/*";
    ml_comment_end = "*/";
    keep_comment = false;
    keep_newline = false;
    keep_whitespace = false;
    custom_defined= Vec<string*string>();
  }
////////////////  

