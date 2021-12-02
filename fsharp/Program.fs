﻿[<Struct>]
type Position = {
    depth: int
    distance: int
    aim: int
}

let applyCommand position command =
    match command with
    | ("up", i) -> { position with aim = position.aim - i }
    | ("down", i) -> { position with aim = position.aim + i }
    | ("forward", i) -> { position with depth = position.depth + position.aim * i
                                        distance = position.distance + i }
    | _ -> position

let readLines filename = Seq.toList (System.IO.File.ReadLines(filename))

let runCommands filename =
    let lines = readLines filename
    let initialPosition = { depth = 0; distance = 0; aim = 0 }

    lines 
    |> List.map (fun s -> s.Split ' ')
    |> List.map (fun sl -> (sl.[0], (int sl.[1])))
    |> List.fold applyCommand initialPosition

let partOne filename =
    let finalPos = runCommands filename
    finalPos.aim * finalPos.distance

let partTwo filename =
    let finalPos = runCommands filename
    finalPos.depth * finalPos.distance

[<EntryPoint>]
let main argv = 
    let filename = "../input/day2.txt"
    partOne filename |> printfn "%d" 
    partTwo filename |> printfn "%d"
    0
