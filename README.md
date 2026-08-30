[![](https://img.shields.io/nuget/v/soenneker.utils.strings.dicecoefficient.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.utils.strings.dicecoefficient/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.utils.strings.dicecoefficient/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.utils.strings.dicecoefficient/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.utils.strings.dicecoefficient.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.utils.strings.dicecoefficient/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.utils.strings.dicecoefficient/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.utils.strings.dicecoefficient/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Utils.Strings.DiceCoefficient
Sørensen–Dice string similarity using overlapping character bigrams.

## Installation

```bash
dotnet add package Soenneker.Utils.Strings.DiceCoefficient
```

## Usage

```csharp
using Soenneker.Utils.Strings.DiceCoefficient;

var text1 = "This is a test";
var text2 = "This is another test";

double score = DiceCoefficientStringUtil.Calculate(text1, text2);
double percentage = DiceCoefficientStringUtil.CalculatePercentage(text1, text2);

// score == 0.75
// percentage == 75
```

For each input, the utility creates overlapping two-character sequences and compares them as a multiset. Repeated bigrams therefore count up to the number present in both inputs. The coefficient is:

```text
2 × shared bigram count / total bigram count across both inputs
```

`Calculate` returns a value from `0` to `1`; `CalculatePercentage` multiplies it by 100.

## Comparison rules

- Comparison is case-sensitive.
- Bigrams consist of adjacent UTF-16 code units.
- Whitespace and punctuation participate like any other character.
- Order inside each bigram matters, while the order of the resulting bigram collection does not.
- Two empty strings return `1`; one empty input returns `0`.
- Inputs shorter than two characters return `1` only when they are exactly equal, otherwise `0`.

Call the static methods directly; no dependency-injection registration is required. Normalize casing, whitespace, punctuation, or Unicode representation before calling if your application needs those equivalences.

The implementation allocates a frequency dictionary for the shorter input and scans the longer input once, making it suitable for ordinary fuzzy matching and duplicate-candidate ranking. It is lexical comparison, not semantic similarity.
