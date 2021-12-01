use std::fs::File;
use std::io::Read;

pub fn day_one(part: usize, filename: String) {
    println!("part: {}", part);

    if part == 1 {
        part_one(filename);    
    } else {
        part_two(filename);
    }
}

fn part_one(filename: String) {
    let mut file = File::open(filename).unwrap();
    let mut contents = String::new();

    file.read_to_string(&mut contents).unwrap();

    /*
    let splits: Vec<i32> = contents.split("\n")
        .filter(|s| !s.is_empty())
        .map(|s| s.parse::<i32>().unwrap())
        .collect();

    let count: i32 = splits.windows(2)
        .map(|w| (w[0] < w[1]) as i32)
        .sum();
    */

    // That solution is good but this is epic rust mode

    let count: i32 = contents.split("\n")
        .filter_map(|s| s.parse::<i32>().ok())
        .collect::<Vec<i32>>()
        .windows(2)
        .map(|w| (w[0] < w[1]) as i32)
        .sum();

    println!("The count is {}", count);
}

#[allow(unused)]
fn part_two(filename: String) {
    todo!();
}
