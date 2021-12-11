open DayTwo
open DayFive
open DaySix
open DaySeven
open DayEleven

let functions day =
    match day with
    | 2 -> dayTwo
    | 5 -> dayFive
    | 6 -> daySix
    | 7 -> daySeven
    | 11 -> dayEleven
    | _ -> (fun a b -> printfn "Error: Invalid day \"%d\"" day)

[<EntryPoint>]
let main argv = 
    let dayFn = functions (int argv.[0])
    let part = int argv.[1]
    let filename = argv.[2]

    dayFn filename part
    0
