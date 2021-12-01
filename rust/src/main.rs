mod dayone;

use text_io::read;
use std::io::{self, Write};

const DAYS: [usize; 1] = [1];

fn main() {
    println!("Hello!");
    print!("Enter the day: ");

    io::stdout().flush().unwrap();

    let day: usize = read!("{}\n");

    if !DAYS.contains(&day) {
        println!("I haven't solved that one yet :P");
        return;
    }

    let mut question: usize;

    loop {
        print!("Enter the question (1 or 2): ");
        io::stdout().flush().unwrap();
        question = read!("{}\n");

        println!("got: {}", question);

        if question != 1 && question != 2 {
            println!("Sorry, try again");
        } else {
            break;
        }
    }

    print!("Enter the input file: ");
    io::stdout().flush().unwrap();
    let filename: String = read!("{}\n");

    // TODO: Use closures or something to make this nicer
    match day {
        1 => dayone::day_one(question, filename),
        _ => {},
    }
}
