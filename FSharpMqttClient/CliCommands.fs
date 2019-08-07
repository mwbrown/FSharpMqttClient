module CliCommands

type CommandResult =
    | Success
    | Error of uint32
    | ExitCode of uint32

let cliExecCommand parseList =

    if List.length parseList = 0 then
        Success
    elif parseList.[0] = "exit" then
        ExitCode (uint32 0)
    else
        printfn "%A" parseList |> ignore
        Success