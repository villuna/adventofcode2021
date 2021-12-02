[<Struct>]
type Position = {
    depth: int
    distance: int
    aim: int
}

type Command =
    | Up of int
    | Down of int
    | Forward of int
    | Invalid

let newCommand command distance =
    match command with
    | "up" -> Up distance
    | "down" -> Down distance
    | "forward" -> Forward distance
    | _ -> Invalid

let applyCommand position command =
    match command with
    | Up i -> { position with depth = position.depth - i }
    | Down i -> { position with depth = position.depth + i }
    | Forward i -> { position with distance = position.distance + i }
    | Invalid -> position // Do nothing ig

let applyCommandAim position command =
    match command with
    | Up i -> { position with aim = position.aim - i }
    | Down i -> { position with aim = position.aim + i }
    | Forward i -> { position with depth = position.depth + position.aim * i
                                   distance = position.distance + i }
    | Invalid -> position

let readLines filename = Seq.toList (System.IO.File.ReadLines(filename))

let getProduct position = position.depth * position.distance

let runCommands filename commandFn  =
    let lines = readLines filename
    let initialPosition = { depth = 0; distance = 0; aim = 0 }

    lines 
    |> List.map (fun s -> s.Split ' ')
    |> List.map (fun sl -> newCommand sl.[0] (int sl.[1]))
    |> List.fold commandFn initialPosition

let aimPosition position = 
    { position with depth = position.depth * position.distance }

let partOne filename =
    runCommands filename applyCommand |> getProduct

let partTwo filename =
    runCommands filename applyCommandAim |> getProduct

[<EntryPoint>]
let main argv = 
    let filename = "../input/day2.txt"
    partOne filename |> printfn "%d" 
    partTwo filename |> printfn "%d"
    0
