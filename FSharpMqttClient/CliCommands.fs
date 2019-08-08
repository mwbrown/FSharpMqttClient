module CliCommands

type CommandResult =
    | Success
    | Error of uint32
    | ExitCode of int
    | CommandNotFound of string
   
let private cmdHelp (args: string list) = 
    printfn "TODO help function"
    Success

let private cmdConnect (args: string list) =
    printfn "placeholder connect function"
    Success

let private cmdDisconnect (args: string list) =
    printfn "placeholder disconnect function"
    Success

let private cmdExit (args: string list) =
    ExitCode 0

// Define a command lookup table
let private cmdMap = Map [
    ("help", cmdHelp);
    ("connect", cmdConnect);
    ("disconnect", cmdDisconnect);
    ("exit", cmdExit);
]

let cliExecCommand parseList =

    if List.length parseList > 0 then
        let cmdName = parseList.[0]
        let cmdArgs = parseList.[1..]

        let search = cmdMap.TryFind cmdName
        match search with
        | Some fn -> fn cmdArgs
        | None -> CommandNotFound cmdName
    else
        Success