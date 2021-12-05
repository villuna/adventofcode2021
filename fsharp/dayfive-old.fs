module DayFive

open Core

type Point = int * int
type Line = Point * Point

(*
Unoptimized, bad way of solving day 5. The better way (which uses more RAM
but like not much) would be to create an actual board of points and plot
each line on the board.

This solution checks each pair of lines on the board and calculates their
intersection points, and counts the number of unique ones.
*)

// Functions for parsing lines from file
let pointFromString (str:string) =
    let splits = str.Split(",")
    (int splits.[0], int splits.[1])

let lineFromStrings (strs:string[]) =
    (pointFromString strs.[0], pointFromString strs.[1])

let linesFromFile filename =
    readLines filename 
    |> List.map (fun s -> lineFromStrings (s.Split(" -> ")))

// This could probably go into Core soon but I wrote it for this
// Generates a list of all the unordered pairs of a list 
// (excluding pairs of an element with itself i.e. (x,x))
let rec unorderedPairs (lst: 'a list) =
    match lst with
    | [_] | [] -> []
    | x::xs -> (List.map (fun y -> (x, y)) xs) @ (unorderedPairs xs)

// Flattens a list 1 level. E.g. [[1;2];[3;4]] -> [1;2;3;4]
let flatten = List.fold (@) []

let isLineStraight (line:Line) =
    let start = fst line
    let ending = snd line
    
    (fst start = fst ending) || (snd start = snd ending)

// Generates a list of points the line covers from beginning to end.
// Somehow i got it to work regardless of direction (but it looks yucky, sorry
// about that)
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
    |> flatten

let generalSolution filename part =
    let tautology = fun x -> true

    let filter = if part = 1 then isLineStraight
                 else tautology

    linesFromFile filename
    |> List.filter filter
    |> unorderedPairs 
    |> List.map pointsOfIntersection
    |> List.filter (fun lst -> lst <> [])
    |> flatten
    |> List.countBy id
    |> List.length

let dayFive filename part =
    match part with
    | 1 | 2 -> generalSolution filename part |> printfn "%d"
    | _ -> printfn "invalid question number"

