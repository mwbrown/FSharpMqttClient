// Learn more about F# at http://fsharp.org

open System

open CliParser
open CliCommands

let rec evalLoop () =

    printfn ""
    printf "> "

    let commandResult =
        Console.ReadLine ()
        |> cliSplitInput
        |> Seq.toList
        |> cliExecCommand

    match commandResult with
    | Success ->
        evalLoop ()
    | Error i ->
        printfn "Error processing command: %u" i
        evalLoop ()
    | CommandNotFound cmd ->
        printfn "Command not found: %s" cmd
        evalLoop ()
    | ExitCode i ->
        printfn "Exiting"
        i

[<EntryPoint>]
let main argv =
    evalLoop ()
