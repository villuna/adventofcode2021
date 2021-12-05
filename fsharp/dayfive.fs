module DayFive

open Core

type Point = int * int
type Line = Point * Point

let pointFromString (str:string) =
    let splits = str.Split(",")
    (int splits.[0], int splits.[1])

let lineFromStrings (strs:string[]) =
    (pointFromString strs.[0], pointFromString strs.[1])

let isLineStraight (line:Line) =
    let start = fst line
    let ending = snd line
    
    (fst start = fst ending) || (snd start = snd ending)

let rec unorderedPairs (lst: 'a list) =
    match lst with
    | [_] | [] -> []
    | x::xs -> (List.map (fun y -> (x, y)) xs) @ (unorderedPairs xs)


let lineRange line =
    let bounds = fun start ending ->
        if start <= ending then [start .. ending]
        else [start .. -1 .. ending]

    match line with
    | ((x, start), (y, ending)) when x = y -> 
        let range = bounds start ending
        [for i in range do yield (x, i)]

    | ((start, x), (ending, y)) when x = y -> 
        let range = bounds start ending
        [for i in range do yield (i, x)]

    | ((a, b), (c, d)) -> 
        let range1 = bounds a c
        let range2 = bounds b d
        List.zip range1 range2

let pointsOfIntersection (lines: Line * Line) =
    let line1 = lineRange (fst lines)
    let line2 = lineRange (snd lines)

    line1
    |> List.map (fun p1 -> List.filter (fun p2 -> p2 = p1) line2)
    |> List.filter (fun lst -> lst <> [])
    |> List.fold (@) [] // Flatten list

let generalSolution filename part =
    let tautology = fun x -> true

    let filter = if part = 1 then isLineStraight
                 else tautology

    readLines filename 
    |> List.map (fun s -> s.Split(" -> "))
    |> List.map lineFromStrings
    |> List.filter filter
    |> unorderedPairs 
    |> List.map pointsOfIntersection
    |> List.filter (fun lst -> lst <> [])
    |> List.fold (@) []
    |> List.countBy id
    |> List.length

let dayFive filename part =
    match part with
    | 1 | 2 -> generalSolution filename part |> printfn "%d"
    | _ -> printfn "invalid question number"

