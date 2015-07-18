module RobotTest

open NUnit
open NUnit.Framework
open RobotWar.App
open Should.Fluent

[<Description("Valid entries are n, e, s, w case insensitive")>]
[<TestCase("N", Facing.North)>]
[<TestCase("E", Facing.East)>]
[<TestCase("S", Facing.South)>]
[<TestCase("W", Facing.West)>]
[<TestCase("n", Facing.North)>]
[<TestCase("e", Facing.East)>]
[<TestCase("s", Facing.South)>]
[<TestCase("w", Facing.West)>]
let StringToFacingTest(s, f : Facing) = 
    let r = Robot.StringToFacing s 
    r.Should().Equal(f) |> ignore

[<Test>]
[<Description("If facing != n, s, e, w -> exception")>]
[<ExpectedException(typedefof<System.ArgumentException>)>]
let WrongFacingConversion() =
    Robot.StringToFacing "c" |> ignore

[<Description("Move aborted if brings the robot outside the arena")>]
[<TestCase(0, 0, Facing.East, 3, 3)>]
[<TestCase(0, 0, Facing.South, 3, 3)>]
[<TestCase(3, 0, Facing.West, 3, 3)>]
[<TestCase(0, 3, Facing.North, 3, 3)>]
let WrongMove(x, y, facing, arenaX, arenaY) =
    let (rX, rY, _) = Robot.ExecuteCommand (x, y, facing) (arenaX, arenaY) 'M' 
    rX.Should().Equal(x) |> ignore
    rY.Should().Equal(y) |> ignore

[<Description("Robot moves in the arena")>]
[<TestCase(0, 0, Facing.North, 3, 3, 0, 1)>]
[<TestCase(0, 1, Facing.South, 3, 3, 0, 0)>]
[<TestCase(2, 0, Facing.West, 3, 3, 3, 0)>]
[<TestCase(1, 0, Facing.East, 3, 3, 0, 0)>]
let Move(x, y, facing, arenaX, arenaY, expectedX, expectedY) =
    let (rX, rY, _) = Robot.ExecuteCommand (x, y, facing) (arenaX, arenaY) 'M' 
    rX.Should().Equal(expectedX) |> ignore
    rY.Should().Equal(expectedY) |> ignore