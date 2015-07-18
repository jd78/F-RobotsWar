namespace RobotWar.App

module Validator =

    let Input (input: string) (size: int)  = 
        input.Split(' ') |> fun s -> 
            match s.Length = size with
            | true -> s
            | false -> raise(System.ArgumentException("Wrong input"))

    let GridPosition (position: string[]) (arenaX, arenaY) =
        match ((int position.[0]), (int position.[1])) with
        | (x, y) when x <= arenaX && y <= arenaY -> position
        | _ -> raise(System.ArgumentException("Wrong robot position"))

    let Facing (position: string[]) =
        match position.[2] with
        | x when x = "N" || x = "n" -> position
        | x when x = "E" || x = "e" -> position
        | x when x = "S" || x = "s" -> position
        | x when x = "W" || x = "w" -> position
        | _ -> raise(System.ArgumentException("Wrong robot facing"))