﻿open DayTwo

let functions day =
    match day with
    | 2 -> dayTwo
    | _ -> (fun a b -> printfn "Error: Invalid day")

[<EntryPoint>]
let main argv = 
    let dayFn = functions (int argv.[0])
    let part = int argv.[1]
    let filename = argv.[2]

    dayFn filename part
    0
