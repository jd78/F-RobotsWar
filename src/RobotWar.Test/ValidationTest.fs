module Validation

open NUnit
open NUnit.Framework
open RobotWar.App
open Should.Fluent

[<Test>]
let ValidInput() =
    let r = Validator.Input "5 3" 2
    r.[0].Should().Equal("5") |> ignore
    r.[1].Should().Equal("3") |> ignore

[<TestCase("1 1 1", 2)>]
[<TestCase("111", 2)>]
[<TestCase("1", 2)>]
[<TestCase("1", 3)>]
[<TestCase("1 1 1 1", 3)>]
[<ExpectedException(typedefof<System.ArgumentException>)>]
let InvalidInput(s, i) =
    Validator.Input s i |> ignore

[<Description("Position invalid if > than Arena Size")>]
[<TestCase([|"1"; "4"|], 2, 3)>]
[<TestCase([|"2"; "4"|], 3, 3)>]
[<TestCase([|"2"; "4"|], 2, 3)>]
[<ExpectedException(typedefof<System.ArgumentException>)>]
let InvalidGrid(position, arenaX, arenaY) =
    Validator.GridPosition position (arenaX, arenaY) |> ignore

[<Test>]
let ValidGrid() =
    let r = Validator.GridPosition [| "1"; "2"; |] (5, 5)
    r.[0].Should().Equal("1") |> ignore
    r.[1].Should().Equal("2") |> ignore

[<Description("Valid facing are n, e, s, w case insensitive")>]
[<TestCase([| "1"; "1"; "n" |])>]
[<TestCase([| "1"; "1"; "e" |])>]
[<TestCase([| "1"; "1"; "s" |])>]
[<TestCase([| "1"; "1"; "w" |])>]
[<TestCase([| "1"; "1"; "N" |])>]
[<TestCase([| "1"; "1"; "E" |])>]
[<TestCase([| "1"; "1"; "S" |])>]
[<TestCase([| "1"; "1"; "W" |])>]
let ValidFacing(f) =
    let r = Validator.Facing f
    r.[2].Should().Equal(f.[2]) |> ignore

[<Description("Invalid facing are all char different from N, E, S, W")>]
[<TestCase([| "1"; "1"; "t" |])>]
[<TestCase([| "1"; "1"; "a" |])>]
[<TestCase([| "1"; "1"; "z" |])>]
[<TestCase([| "1"; "1"; "q" |])>]
[<ExpectedException(typedefof<System.ArgumentException>)>]
let InvalidFacing(f) =
    Validator.Facing f |> ignore
