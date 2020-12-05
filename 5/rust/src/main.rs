fn main() {
    let raw_input = std::fs::read_to_string("src/input.txt").expect("Error reading the file!");

    let mut zz: Vec<isize> = raw_input
        .lines()
        .map(|line| {
            let res = line
                .replace("F", "0")
                .replace("L", "0")
                .replace("B", "1")
                .replace("R", "1");
            let s_slice: &str = &*res;
            return isize::from_str_radix(s_slice, 2).unwrap();
        })
        .collect();

    zz.sort();
    let first = zz.iter().next().unwrap();
    println!(
        "{}",
        zz.iter()
            .enumerate()
            .find(|(index, id)| *index as isize + first != **id)
            .unwrap()
            .1
            - 1
    );
}
