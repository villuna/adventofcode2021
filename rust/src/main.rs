mod dayone;
mod daythree;
mod dayfour;

use text_io::read;
use std::io::{self, Write};

const DAYS: [usize; 3] = [1, 3, 4];

fn main() {
    let args = std::env::args().collect::<Vec<String>>();
    let day: usize;
    let mut question: usize;

    if args.len() != 3 {
        println!("Hello!");
        print!("Enter the day: ");

        io::stdout().flush().unwrap();

        day = read!("{}\n");

        if !DAYS.contains(&day) {
            println!("I haven't solved that one yet :P");
            return;
        }

        loop {
            print!("Enter the question (1 or 2): ");
            io::stdout().flush().unwrap();
            question = read!("{}\n");

            if question != 1 && question != 2 {
                println!("Sorry, try again");
            } else {
                break;
            }
        }
    } else {
        day = args[1].parse().unwrap();
        question = args[2].parse().unwrap();
    }

    print!("Enter the input file: ");
    io::stdout().flush().unwrap();
    let filename: String = read!("{}\n");


    // TODO: Use closures or something to make this nicer
    match day {
        1 => dayone::day_one(question, filename),
        3 => daythree::day_three(question, filename),
        4 => dayfour::day_four(question, filename),
        _ => {},
    }
}
