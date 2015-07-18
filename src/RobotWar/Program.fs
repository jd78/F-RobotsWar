open RobotWar.App

[<EntryPoint>]
let main argv = 
    printfn "Please enter the size of the grid (eg. 5 5)"

    let arena = System.Console.ReadLine() |> fun s -> Validator.Input s 2 |> fun s -> (int s.[0], int s.[1])

    let setRobot() =

        printfn "Please enter the initial robot position in the grid follwed by a direction (N,S,E,W) (eg 1 2 N)"

        let robot = 
            System.Console.ReadLine()
            |> fun s -> Validator.Input s 3 
            |> fun s -> Validator.GridPosition s arena
            |> Validator.Facing 
            |> fun r -> (int r.[0], int r.[1], Robot.StringToFacing(r.[2]))
        robot

    let startGame robot = 
        printf "R -> rotate right, L -> rotate left, M -> move, press enter to stop and get the final position\n"

        let rec recursiveCommand (x: int, y: int, facing: Facing) =
            let robot = (x, y, facing)
            System.Console.ReadKey() |> fun k ->
                match k.Key with
                | System.ConsoleKey.Enter -> (x, y, facing)
                | _ -> Robot.ExecuteCommand robot arena k.KeyChar |> recursiveCommand

        recursiveCommand robot

    let finalPosition (x, y, facing) =
        printf "%d %d %c\n\n" x y (facing.ToString().[0])

    let r1 = setRobot() |> startGame
    let r2 = setRobot() |> startGame

    printf "Game ended\n\n"
    finalPosition r1
    finalPosition r2

    System.Console.ReadLine() |> ignore
    0

