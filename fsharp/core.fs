module Core

let readLines filename = Seq.toList (System.IO.File.ReadLines(filename))

