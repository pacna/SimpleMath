# Simple Math

<img alt="Dotnet test passing" src="https://github.com/pacna/SimpleMath/workflows/Test%20CI/badge.svg" />

A basic interactive calculator that uses the [Shunting-yard algorithm](https://en.wikipedia.org/wiki/Shunting-yard_algorithm).

### Terminal

```
Pass in a math expression: 3 + 3
6
Press q and then ENTER to quit
Pass in a math expression: This should be invalid
Invalid input (ノ﹏ヽ)
Press q and then ENTER to quit
Pass in a math expression: q
Bye | (• ◡•)| (❍ᴥ❍ʋ)
```

## Prerequisites

-   [dotnet core](https://dotnet.microsoft.com/download)

## Running

```bash
# Go to SimpleMath
$ cd src/SimpleMath

# run
$ dotnet run
```

## Running tests

```bash
# Go to SimpleMath.UnitTests
$ cd tests/SimpleMath.UnitTests

# run
$ dotnet test
```
