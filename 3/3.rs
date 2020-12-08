fn main() {
    let mut slopes: Vec<(usize, usize, usize)> =
        vec![(3, 1, 0), (1, 1, 0), (5, 1, 0), (7, 1, 0), (1, 2, 0)];
    let raw_input = std::fs::read_to_string("src/input.txt").expect("Error reading the file!");
    raw_input.lines().enumerate().for_each(|(index, line)| {
        slopes
            .iter_mut()
            .filter(|slope| index % slope.1 == 0)
            .filter(|slope| {
                line.chars()
                    .nth((slope.0 * index / slope.1) % line.len())
                    .unwrap()
                    == '#'
            })
            .for_each(|slope| slope.2 += 1);
    });
    println!("{}", slopes.iter().fold(1, |acc, slope| slope.2 * acc));
}