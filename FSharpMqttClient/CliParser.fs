module CliParser

open System
open System.Text

// TODO: support multi-character escape sequences
let private cliEscapeChar c =
    match c with
    | 'r' -> '\r'
    | 'n' -> '\n'
    | 't' -> '\t'
    | '\\' -> '\\'
    | '\"' -> '\"'
    | _ -> failwith "Invalid character"

let private cliSplitInputInner input = seq {
    let sb = new StringBuilder()
    
    let mutable isQuote = false   // Are we in between quotes?
    let mutable isEscape = false  // Have we encountered a backslash?

    for c in input do

        if isEscape then
            cliEscapeChar c |> sb.Append |> ignore
            isEscape <- false
        elif (Char.IsWhiteSpace c) && not isQuote then
            // Produce a new word.
            if sb.Length > 0 then
                yield! [sb.ToString()]
                sb.Clear() |> ignore
        else
            match c with
            | '\"' -> 
                isQuote <- not isQuote
            | '\\' ->
                isEscape <- true
            | _ -> 
                sb.Append c |> ignore

    // Yield any remaining word.
    if sb.Length > 0 then
        yield! [sb.ToString()]
        sb.Clear() |> ignore
    }

let cliSplitInput input =
    cliSplitInputInner input
