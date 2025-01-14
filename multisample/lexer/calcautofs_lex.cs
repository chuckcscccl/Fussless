namespace Fussless
{
//CsLex file generated from grammar calcautofs
#pragma warning disable 0414
using System;
using System.Text;
public class calcautofslexer<ET> : AbstractLexer<ET>  {
  Yylex lexer;
  public calcautofslexer(string n) { lexer = new Yylex(new System.IO.StringReader(n)); }
  public calcautofslexer(System.IO.FileStream f) { lexer=new Yylex(f); }
  public RawToken next_lt() => lexer.yylex();
}//lexer class
/* test */


internal class Yylex
{
private const int YY_BUFFER_SIZE = 512;
private const int YY_F = -1;
private const int YY_NO_STATE = -1;
private const int YY_NOT_ACCEPT = 0;
private const int YY_START = 1;
private const int YY_END = 2;
private const int YY_NO_ANCHOR = 4;
delegate RawToken AcceptMethod();
AcceptMethod[] accept_dispatch;
private const int YY_BOL = 128;
private const int YY_EOF = 129;

private static int comment_count = 0;
private static int line_char = 0;
private System.IO.TextReader yy_reader;
private int yy_buffer_index;
private int yy_buffer_read;
private int yy_buffer_start;
private int yy_buffer_end;
private char[] yy_buffer;
private int yychar;
private int yyline;
private bool yy_at_bol;
private int yy_lexical_state;

internal Yylex(System.IO.TextReader reader) : this()
  {
  if (null == reader)
    {
    throw new System.ApplicationException("Error: Bad input stream initializer.");
    }
  yy_reader = reader;
  }

internal Yylex(System.IO.FileStream instream) : this()
  {
  if (null == instream)
    {
    throw new System.ApplicationException("Error: Bad input stream initializer.");
    }
  yy_reader = new System.IO.StreamReader(instream);
  }

private Yylex()
  {
  yy_buffer = new char[YY_BUFFER_SIZE];
  yy_buffer_read = 0;
  yy_buffer_index = 0;
  yy_buffer_start = 0;
  yy_buffer_end = 0;
  yychar = 0;
  yyline = 0;
  yy_at_bol = true;
  yy_lexical_state = YYINITIAL;
accept_dispatch = new AcceptMethod[] 
 {
  null,
  null,
  new AcceptMethod(this.Accept_2),
  new AcceptMethod(this.Accept_3),
  new AcceptMethod(this.Accept_4),
  new AcceptMethod(this.Accept_5),
  new AcceptMethod(this.Accept_6),
  new AcceptMethod(this.Accept_7),
  new AcceptMethod(this.Accept_8),
  new AcceptMethod(this.Accept_9),
  new AcceptMethod(this.Accept_10),
  new AcceptMethod(this.Accept_11),
  new AcceptMethod(this.Accept_12),
  new AcceptMethod(this.Accept_13),
  new AcceptMethod(this.Accept_14),
  new AcceptMethod(this.Accept_15),
  new AcceptMethod(this.Accept_16),
  new AcceptMethod(this.Accept_17),
  new AcceptMethod(this.Accept_18),
  new AcceptMethod(this.Accept_19),
  new AcceptMethod(this.Accept_20),
  new AcceptMethod(this.Accept_21),
  new AcceptMethod(this.Accept_22),
  new AcceptMethod(this.Accept_23),
  new AcceptMethod(this.Accept_24),
  new AcceptMethod(this.Accept_25),
  new AcceptMethod(this.Accept_26),
  null,
  new AcceptMethod(this.Accept_28),
  new AcceptMethod(this.Accept_29),
  new AcceptMethod(this.Accept_30),
  new AcceptMethod(this.Accept_31),
  new AcceptMethod(this.Accept_32),
  new AcceptMethod(this.Accept_33),
  new AcceptMethod(this.Accept_34),
  new AcceptMethod(this.Accept_35),
  null,
  new AcceptMethod(this.Accept_37),
  new AcceptMethod(this.Accept_38),
  new AcceptMethod(this.Accept_39),
  new AcceptMethod(this.Accept_40),
  null,
  new AcceptMethod(this.Accept_42),
  new AcceptMethod(this.Accept_43),
  new AcceptMethod(this.Accept_44),
  null,
  null,
  null,
  null,
  new AcceptMethod(this.Accept_49),
  new AcceptMethod(this.Accept_50),
  };
  }

RawToken Accept_2()
    { // begin accept action #2
{ line_char=yychar+yytext().Length; return null; }
    } // end accept action #2

RawToken Accept_3()
    { // begin accept action #3
{ line_char = yychar+yytext().Length; return null; }
    } // end accept action #3

RawToken Accept_4()
    { // begin accept action #4
{ return null; }
    } // end accept action #4

RawToken Accept_5()
    { // begin accept action #5
{ return new RawToken("+",yytext(),yyline,yychar-line_char,yychar); }
    } // end accept action #5

RawToken Accept_6()
    { // begin accept action #6
{ return new RawToken("-",yytext(),yyline,yychar-line_char,yychar); }
    } // end accept action #6

RawToken Accept_7()
    { // begin accept action #7
{ return new RawToken("*",yytext(),yyline,yychar-line_char,yychar); }
    } // end accept action #7

RawToken Accept_8()
    { // begin accept action #8
{ return new RawToken("/",yytext(),yyline,yychar-line_char,yychar); }
    } // end accept action #8

RawToken Accept_9()
    { // begin accept action #9
{ return new RawToken("(",yytext(),yyline,yychar-line_char,yychar); }
    } // end accept action #9

RawToken Accept_10()
    { // begin accept action #10
{ return new RawToken(")",yytext(),yyline,yychar-line_char,yychar); }
    } // end accept action #10

RawToken Accept_11()
    { // begin accept action #11
{ return new RawToken("=",yytext(),yyline,yychar-line_char,yychar); }
    } // end accept action #11

RawToken Accept_12()
    { // begin accept action #12
{ return new RawToken(";",yytext(),yyline,yychar-line_char,yychar); }
    } // end accept action #12

RawToken Accept_13()
    { // begin accept action #13
{
        return new RawToken("Alphanum",yytext(),yyline,yychar-line_char,yychar);
}
    } // end accept action #13

RawToken Accept_14()
    { // begin accept action #14
{
	StringBuilder sb = new StringBuilder("Illegal character: <");
	String s = yytext();
	for (int i = 0; i < s.Length; i++)
	  if (s[i] >= 32)
	    sb.Append(s[i]);
	  else
	    {
	    sb.Append("^");
	    sb.Append(Convert.ToChar(s[i]+'A'-1));
	    }
        sb.Append(">");
	Console.WriteLine(sb.ToString());	
	Utility.error(Utility.E_UNMATCHED);
        return null;
}
    } // end accept action #14

RawToken Accept_15()
    { // begin accept action #15
{
	String str =  yytext().Substring(1,yytext().Length);
	Utility.error(Utility.E_UNCLOSEDSTR);
        return new RawToken("Unclosed String",str,yyline,yychar-line_char,yychar);
}
    } // end accept action #15

RawToken Accept_16()
    { // begin accept action #16
{ 
  return new RawToken("Num",yytext(),yyline,yychar-line_char,yychar);
}
    } // end accept action #16

RawToken Accept_17()
    { // begin accept action #17
{ yybegin(COMMENT); comment_count = comment_count + 1; return null;
}
    } // end accept action #17

RawToken Accept_18()
    { // begin accept action #18
{ return new RawToken("in",yytext(),yyline,yychar-line_char,yychar); }
    } // end accept action #18

RawToken Accept_19()
    { // begin accept action #19
{
        return new RawToken("StrLit",yytext(),yyline,yychar-line_char,yychar);
}
    } // end accept action #19

RawToken Accept_20()
    { // begin accept action #20
{ 
return new RawToken("Hexnum",yytext(),yyline,yychar-line_char,yychar);  
}
    } // end accept action #20

RawToken Accept_21()
    { // begin accept action #21
{ 
  return new RawToken("Float",yytext(),yyline,yychar-line_char,yychar);
}
    } // end accept action #21

RawToken Accept_22()
    { // begin accept action #22
{ line_char=yychar+yytext().Length; return null; }
    } // end accept action #22

RawToken Accept_23()
    { // begin accept action #23
{ return new RawToken("let",yytext(),yyline,yychar-line_char,yychar); }
    } // end accept action #23

RawToken Accept_24()
    { // begin accept action #24
{ return null; }
    } // end accept action #24

RawToken Accept_25()
    { // begin accept action #25
{ 
	comment_count = comment_count - 1;
	if (comment_count == 0) {
            yybegin(YYINITIAL);
        }
        return null;
}
    } // end accept action #25

RawToken Accept_26()
    { // begin accept action #26
{ comment_count = comment_count + 1; return null; }
    } // end accept action #26

RawToken Accept_28()
    { // begin accept action #28
{ line_char=yychar+yytext().Length; return null; }
    } // end accept action #28

RawToken Accept_29()
    { // begin accept action #29
{ line_char = yychar+yytext().Length; return null; }
    } // end accept action #29

RawToken Accept_30()
    { // begin accept action #30
{
        return new RawToken("Alphanum",yytext(),yyline,yychar-line_char,yychar);
}
    } // end accept action #30

RawToken Accept_31()
    { // begin accept action #31
{
	StringBuilder sb = new StringBuilder("Illegal character: <");
	String s = yytext();
	for (int i = 0; i < s.Length; i++)
	  if (s[i] >= 32)
	    sb.Append(s[i]);
	  else
	    {
	    sb.Append("^");
	    sb.Append(Convert.ToChar(s[i]+'A'-1));
	    }
        sb.Append(">");
	Console.WriteLine(sb.ToString());	
	Utility.error(Utility.E_UNMATCHED);
        return null;
}
    } // end accept action #31

RawToken Accept_32()
    { // begin accept action #32
{ 
  return new RawToken("Num",yytext(),yyline,yychar-line_char,yychar);
}
    } // end accept action #32

RawToken Accept_33()
    { // begin accept action #33
{
        return new RawToken("StrLit",yytext(),yyline,yychar-line_char,yychar);
}
    } // end accept action #33

RawToken Accept_34()
    { // begin accept action #34
{ 
  return new RawToken("Float",yytext(),yyline,yychar-line_char,yychar);
}
    } // end accept action #34

RawToken Accept_35()
    { // begin accept action #35
{ return null; }
    } // end accept action #35

RawToken Accept_37()
    { // begin accept action #37
{ line_char=yychar+yytext().Length; return null; }
    } // end accept action #37

RawToken Accept_38()
    { // begin accept action #38
{
        return new RawToken("Alphanum",yytext(),yyline,yychar-line_char,yychar);
}
    } // end accept action #38

RawToken Accept_39()
    { // begin accept action #39
{
	StringBuilder sb = new StringBuilder("Illegal character: <");
	String s = yytext();
	for (int i = 0; i < s.Length; i++)
	  if (s[i] >= 32)
	    sb.Append(s[i]);
	  else
	    {
	    sb.Append("^");
	    sb.Append(Convert.ToChar(s[i]+'A'-1));
	    }
        sb.Append(">");
	Console.WriteLine(sb.ToString());	
	Utility.error(Utility.E_UNMATCHED);
        return null;
}
    } // end accept action #39

RawToken Accept_40()
    { // begin accept action #40
{ return null; }
    } // end accept action #40

RawToken Accept_42()
    { // begin accept action #42
{
        return new RawToken("Alphanum",yytext(),yyline,yychar-line_char,yychar);
}
    } // end accept action #42

RawToken Accept_43()
    { // begin accept action #43
{
	StringBuilder sb = new StringBuilder("Illegal character: <");
	String s = yytext();
	for (int i = 0; i < s.Length; i++)
	  if (s[i] >= 32)
	    sb.Append(s[i]);
	  else
	    {
	    sb.Append("^");
	    sb.Append(Convert.ToChar(s[i]+'A'-1));
	    }
        sb.Append(">");
	Console.WriteLine(sb.ToString());	
	Utility.error(Utility.E_UNMATCHED);
        return null;
}
    } // end accept action #43

RawToken Accept_44()
    { // begin accept action #44
{ return null; }
    } // end accept action #44

RawToken Accept_49()
    { // begin accept action #49
{
        return new RawToken("Alphanum",yytext(),yyline,yychar-line_char,yychar);
}
    } // end accept action #49

RawToken Accept_50()
    { // begin accept action #50
{
	String str =  yytext().Substring(1,yytext().Length);
	Utility.error(Utility.E_UNCLOSEDSTR);
        return new RawToken("Unclosed String",str,yyline,yychar-line_char,yychar);
}
    } // end accept action #50

private const int YYINITIAL = 0;
private const int COMMENT = 1;
private static int[] yy_state_dtrans = new int[] 
  {   0,
  24
  };
private void yybegin (int state)
  {
  yy_lexical_state = state;
  }

private char yy_advance ()
  {
  int next_read;
  int i;
  int j;

  if (yy_buffer_index < yy_buffer_read)
    {
    return yy_buffer[yy_buffer_index++];
    }

  if (0 != yy_buffer_start)
    {
    i = yy_buffer_start;
    j = 0;
    while (i < yy_buffer_read)
      {
      yy_buffer[j] = yy_buffer[i];
      i++;
      j++;
      }
    yy_buffer_end = yy_buffer_end - yy_buffer_start;
    yy_buffer_start = 0;
    yy_buffer_read = j;
    yy_buffer_index = j;
    next_read = yy_reader.Read(yy_buffer,yy_buffer_read,
                  yy_buffer.Length - yy_buffer_read);
    if (next_read <= 0)
      {
      return (char) YY_EOF;
      }
    yy_buffer_read = yy_buffer_read + next_read;
    }
  while (yy_buffer_index >= yy_buffer_read)
    {
    if (yy_buffer_index >= yy_buffer.Length)
      {
      yy_buffer = yy_double(yy_buffer);
      }
    next_read = yy_reader.Read(yy_buffer,yy_buffer_read,
                  yy_buffer.Length - yy_buffer_read);
    if (next_read <= 0)
      {
      return (char) YY_EOF;
      }
    yy_buffer_read = yy_buffer_read + next_read;
    }
  return yy_buffer[yy_buffer_index++];
  }
private void yy_move_end ()
  {
  if (yy_buffer_end > yy_buffer_start && 
      '\n' == yy_buffer[yy_buffer_end-1])
    yy_buffer_end--;
  if (yy_buffer_end > yy_buffer_start &&
      '\r' == yy_buffer[yy_buffer_end-1])
    yy_buffer_end--;
  }
private bool yy_last_was_cr=false;
private void yy_mark_start ()
  {
  int i;
  for (i = yy_buffer_start; i < yy_buffer_index; i++)
    {
    if (yy_buffer[i] == '\n' && !yy_last_was_cr)
      {
      yyline++;
      }
    if (yy_buffer[i] == '\r')
      {
      yyline++;
      yy_last_was_cr=true;
      }
    else
      {
      yy_last_was_cr=false;
      }
    }
  yychar = yychar + yy_buffer_index - yy_buffer_start;
  yy_buffer_start = yy_buffer_index;
  }
private void yy_mark_end ()
  {
  yy_buffer_end = yy_buffer_index;
  }
private void yy_to_mark ()
  {
  yy_buffer_index = yy_buffer_end;
  yy_at_bol = (yy_buffer_end > yy_buffer_start) &&
    (yy_buffer[yy_buffer_end-1] == '\r' ||
    yy_buffer[yy_buffer_end-1] == '\n');
  }
internal string yytext()
  {
  return (new string(yy_buffer,
                yy_buffer_start,
                yy_buffer_end - yy_buffer_start)
         );
  }
private int yylength ()
  {
  return yy_buffer_end - yy_buffer_start;
  }
private char[] yy_double (char[] buf)
  {
  int i;
  char[] newbuf;
  newbuf = new char[2*buf.Length];
  for (i = 0; i < buf.Length; i++)
    {
    newbuf[i] = buf[i];
    }
  return newbuf;
  }
private const int YY_E_INTERNAL = 0;
private const int YY_E_MATCH = 1;
private static string[] yy_error_string = new string[]
  {
  "Error: Internal error.\n",
  "Error: Unmatched input.\n"
  };
private void yy_error (int code,bool fatal)
  {
  System.Console.Write(yy_error_string[code]);
  if (fatal)
    {
    throw new System.ApplicationException("Fatal Error.\n");
    }
  }
private static int[] yy_acpt = new int[]
  {
  /* 0 */   YY_NOT_ACCEPT,
  /* 1 */   YY_NO_ANCHOR,
  /* 2 */   YY_NO_ANCHOR,
  /* 3 */   YY_NO_ANCHOR,
  /* 4 */   YY_NO_ANCHOR,
  /* 5 */   YY_NO_ANCHOR,
  /* 6 */   YY_NO_ANCHOR,
  /* 7 */   YY_NO_ANCHOR,
  /* 8 */   YY_NO_ANCHOR,
  /* 9 */   YY_NO_ANCHOR,
  /* 10 */   YY_NO_ANCHOR,
  /* 11 */   YY_NO_ANCHOR,
  /* 12 */   YY_NO_ANCHOR,
  /* 13 */   YY_NO_ANCHOR,
  /* 14 */   YY_NO_ANCHOR,
  /* 15 */   YY_NO_ANCHOR,
  /* 16 */   YY_NO_ANCHOR,
  /* 17 */   YY_NO_ANCHOR,
  /* 18 */   YY_NO_ANCHOR,
  /* 19 */   YY_NO_ANCHOR,
  /* 20 */   YY_NO_ANCHOR,
  /* 21 */   YY_NO_ANCHOR,
  /* 22 */   YY_NO_ANCHOR,
  /* 23 */   YY_NO_ANCHOR,
  /* 24 */   YY_NO_ANCHOR,
  /* 25 */   YY_NO_ANCHOR,
  /* 26 */   YY_NO_ANCHOR,
  /* 27 */   YY_NOT_ACCEPT,
  /* 28 */   YY_NO_ANCHOR,
  /* 29 */   YY_NO_ANCHOR,
  /* 30 */   YY_NO_ANCHOR,
  /* 31 */   YY_NO_ANCHOR,
  /* 32 */   YY_NO_ANCHOR,
  /* 33 */   YY_NO_ANCHOR,
  /* 34 */   YY_NO_ANCHOR,
  /* 35 */   YY_NO_ANCHOR,
  /* 36 */   YY_NOT_ACCEPT,
  /* 37 */   YY_NO_ANCHOR,
  /* 38 */   YY_NO_ANCHOR,
  /* 39 */   YY_NO_ANCHOR,
  /* 40 */   YY_NO_ANCHOR,
  /* 41 */   YY_NOT_ACCEPT,
  /* 42 */   YY_NO_ANCHOR,
  /* 43 */   YY_NO_ANCHOR,
  /* 44 */   YY_NO_ANCHOR,
  /* 45 */   YY_NOT_ACCEPT,
  /* 46 */   YY_NOT_ACCEPT,
  /* 47 */   YY_NOT_ACCEPT,
  /* 48 */   YY_NOT_ACCEPT,
  /* 49 */   YY_NO_ANCHOR,
  /* 50 */   YY_NO_ANCHOR
  };
private static int[] yy_cmap = new int[]
  {
  17, 17, 17, 17, 17, 17, 17, 17,
  3, 3, 2, 17, 17, 1, 17, 17,
  17, 17, 17, 17, 17, 17, 17, 17,
  17, 17, 17, 17, 17, 17, 17, 17,
  3, 17, 19, 17, 17, 17, 17, 17,
  8, 9, 6, 4, 17, 5, 25, 7,
  22, 21, 21, 21, 21, 21, 21, 21,
  21, 21, 17, 11, 17, 10, 17, 18,
  17, 24, 24, 24, 24, 26, 24, 27,
  27, 27, 27, 27, 27, 27, 27, 27,
  27, 27, 27, 27, 27, 27, 27, 27,
  27, 27, 27, 17, 20, 17, 17, 28,
  17, 24, 24, 24, 24, 13, 24, 27,
  27, 15, 27, 27, 12, 27, 16, 27,
  27, 27, 27, 27, 14, 27, 27, 27,
  23, 27, 27, 17, 18, 17, 17, 17,
  0, 0 
  };
private static int[] yy_rmap = new int[]
  {
  0, 1, 2, 3, 4, 1, 1, 1,
  5, 1, 1, 1, 1, 6, 1, 7,
  8, 1, 9, 1, 10, 11, 1, 9,
  12, 1, 1, 2, 1, 13, 14, 15,
  16, 7, 17, 18, 19, 18, 20, 21,
  22, 15, 9, 23, 24, 25, 17, 26,
  27, 9, 28 
  };
private static int[,] yy_nxt = new int[,]
  {
  { 1, 2, 3, 4, 5, 6, 7, 8,
   9, 10, 11, 12, 13, 49, 49, 30,
   49, 14, 28, 15, 14, 16, 32, 49,
   49, 31, 49, 49, 49 },
  { -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1 },
  { -1, -1, 29, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1 },
  { -1, 27, 3, 4, -1, -1, -1, -1,
   -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1 },
  { -1, -1, 4, 4, -1, -1, -1, -1,
   -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1 },
  { -1, -1, -1, -1, -1, -1, 17, 36,
   -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1 },
  { -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, 49, 38, 49, 49,
   49, -1, -1, -1, -1, 42, 42, 49,
   49, -1, 49, 49, 42 },
  { -1, 15, 15, 15, 15, 15, 15, 15,
   15, 15, 15, 15, 15, 15, 15, 15,
   15, 15, 15, 19, 50, 15, 15, 15,
   15, 15, 15, 15, 15 },
  { -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1, 16, 16, -1,
   -1, 41, -1, -1, -1 },
  { -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, 49, 49, 49, 49,
   49, -1, -1, -1, -1, 42, 42, 49,
   49, -1, 49, 49, 42 },
  { -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1, 20, -1, -1,
   -1, -1, -1, -1, -1, 20, 20, -1,
   20, -1, 20, -1, -1 },
  { -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1, 45, -1, -1,
   -1, -1, -1, -1, -1, 21, 21, -1,
   -1, -1, 45, -1, -1 },
  { 1, 28, 28, 35, 35, 35, 39, 43,
   37, 37, 35, 35, 35, 35, 35, 35,
   35, 35, 37, 35, 35, 35, 35, 35,
   35, 35, 35, 35, 35 },
  { -1, 27, 29, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1 },
  { -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, 49, 49, 49, 49,
   18, -1, -1, -1, -1, 42, 42, 49,
   49, -1, 49, 49, 42 },
  { -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1, 21, 21, -1,
   -1, -1, -1, -1, -1 },
  { -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1, 16, 16, 20,
   -1, 41, -1, -1, -1 },
  { -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1, 34, 34, -1,
   -1, -1, -1, -1, -1 },
  { -1, -1, -1, 35, 35, 35, 47, 48,
   35, 35, 35, 35, 35, 35, 35, 35,
   35, 35, 35, 35, 35, 35, 35, 35,
   35, 35, 35, 35, 35 },
  { -1, -1, 22, 36, 36, 36, 36, 36,
   36, 36, 36, 36, 36, 36, 36, 36,
   36, 36, 36, 36, 36, 36, 36, 36,
   36, 36, 36, 36, 36 },
  { -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, 49, 49, 23, 49,
   49, -1, -1, -1, -1, 42, 42, 49,
   49, -1, 49, 49, 42 },
  { -1, -1, -1, 35, 35, 35, 40, 25,
   35, 35, 35, 35, 35, 35, 35, 35,
   35, 35, 35, 35, 35, 35, 35, 35,
   35, 35, 35, 35, 35 },
  { -1, -1, -1, 35, 35, 35, 40, 48,
   35, 35, 35, 35, 35, 35, 35, 35,
   35, 35, 35, 35, 35, 35, 35, 35,
   35, 35, 35, 35, 35 },
  { -1, -1, -1, 35, 35, 35, 26, 44,
   35, 35, 35, 35, 35, 35, 35, 35,
   35, 35, 35, 35, 35, 35, 35, 35,
   35, 35, 35, 35, 35 },
  { -1, -1, -1, 35, 35, 35, 47, 44,
   35, 35, 35, 35, 35, 35, 35, 35,
   35, 35, 35, 35, 35, 35, 35, 35,
   35, 35, 35, 35, 35 },
  { -1, -1, -1, -1, 46, 46, -1, -1,
   -1, -1, -1, -1, -1, -1, -1, -1,
   -1, -1, -1, -1, -1, 34, 34, -1,
   -1, -1, -1, -1, -1 },
  { -1, -1, -1, 35, 35, 35, 40, -1,
   35, 35, 35, 35, 35, 35, 35, 35,
   35, 35, 35, 35, 35, 35, 35, 35,
   35, 35, 35, 35, 35 },
  { -1, -1, -1, 35, 35, 35, -1, 44,
   35, 35, 35, 35, 35, 35, 35, 35,
   35, 35, 35, 35, 35, 35, 35, 35,
   35, 35, 35, 35, 35 },
  { -1, 15, 15, 15, 15, 15, 15, 15,
   15, 15, 15, 15, 15, 15, 15, 15,
   15, 15, 15, 33, 50, 15, 15, 15,
   15, 15, 15, 15, 15 }
  };
public RawToken yylex()
  {
  char yy_lookahead;
  int yy_anchor = YY_NO_ANCHOR;
  int yy_state = yy_state_dtrans[yy_lexical_state];
  int yy_next_state = YY_NO_STATE;
  int yy_last_accept_state = YY_NO_STATE;
  bool yy_initial = true;
  int yy_this_accept;

  yy_mark_start();
  yy_this_accept = yy_acpt[yy_state];
  if (YY_NOT_ACCEPT != yy_this_accept)
    {
    yy_last_accept_state = yy_state;
    yy_mark_end();
    }
  while (true)
    {
    if (yy_initial && yy_at_bol)
      yy_lookahead = (char) YY_BOL;
    else
      {
      yy_lookahead = yy_advance();
      }
    yy_next_state = yy_nxt[yy_rmap[yy_state],yy_cmap[yy_lookahead]];
    if (YY_EOF == yy_lookahead && yy_initial)
      {

  return new RawToken("EOF","EOF",yyline,yychar);
      }
    if (YY_F != yy_next_state)
      {
      yy_state = yy_next_state;
      yy_initial = false;
      yy_this_accept = yy_acpt[yy_state];
      if (YY_NOT_ACCEPT != yy_this_accept)
        {
        yy_last_accept_state = yy_state;
        yy_mark_end();
        }
      }
    else
      {
      if (YY_NO_STATE == yy_last_accept_state)
        {
        throw new System.ApplicationException("Lexical Error: Unmatched Input.");
        }
      else
        {
        yy_anchor = yy_acpt[yy_last_accept_state];
        if (0 != (YY_END & yy_anchor))
          {
          yy_move_end();
          }
        yy_to_mark();
        if (yy_last_accept_state < 0)
          {
          if (yy_last_accept_state < 51)
            yy_error(YY_E_INTERNAL, false);
          }
        else
          {
          AcceptMethod m = accept_dispatch[yy_last_accept_state];
          if (m != null)
            {
            RawToken tmp = m();
            if (tmp != null)
              return tmp;
            }
          }
        yy_initial = true;
        yy_state = yy_state_dtrans[yy_lexical_state];
        yy_next_state = YY_NO_STATE;
        yy_last_accept_state = YY_NO_STATE;
        yy_mark_start();
        yy_this_accept = yy_acpt[yy_state];
        if (YY_NOT_ACCEPT != yy_this_accept)
          {
          yy_last_accept_state = yy_state;
          yy_mark_end();
          }
        }
      }
    }
  }
}

}
