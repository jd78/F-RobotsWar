module GameTest

open NUnit
open NUnit.Framework
open RobotWar.App
open Should.Fluent
open System

[<Test>]
let GameTest1() = 
    let mutable robot = (1, 2, Facing.North)
    for m in [| 'L';'M';'L';'M';'L';'M';'L';'M';'M' |] do
        robot <- Robot.ExecuteCommand robot (5, 5) m
    let (rx, ry, rf) = robot
    rx.Should().Equal(1) |> ignore
    ry.Should().Equal(3) |> ignore
    rf.Should().Equal(Facing.North) |> ignore

[<Test>]
let GameTest2() = 
    let mutable robot = (3, 3, Facing.East)
    for m in [| 'M';'M';'R';'M';'M';'R';'M';'R';'R'; 'M' |] do
        robot <- Robot.ExecuteCommand robot (5, 5) m
    let (rx, ry, rf) = robot
    rx.Should().Equal(5) |> ignore
    ry.Should().Equal(1) |> ignore
    rf.Should().Equal(Facing.East) |> ignore