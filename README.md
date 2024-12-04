[![](https://img.shields.io/nuget/v/soenneker.utils.strings.dicecoefficient.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.utils.strings.dicecoefficient/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.utils.strings.dicecoefficient/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.utils.strings.dicecoefficient/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.utils.strings.dicecoefficient.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.utils.strings.dicecoefficient/)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Utils.Strings.DiceCoefficient
### A utility library for comparing strings via the Dice Coefficient algorithm

## Installation

```
dotnet add package Soenneker.Utils.Strings.DiceCoefficient
```

## Why?

The Dice Coefficient is a powerful way to measure similarity between strings or other sequences. It's particularly effective for comparing text fragments, identifying duplicates, and matching approximate content. Here's why it stands out:

### Pairwise Comparison:
It evaluates based on overlapping character pairs (bigrams), focusing on shared elements without considering their order.

### Balanced by Size:
It considers both the number of matches and the total size of the compared strings, ensuring a fair similarity measure.

### Suitable for Real-World Data:
Its sensitivity to shared sequences makes it effective for noisy or partially matching data.

### Fast and Scalable:
It's computationally efficient, making it applicable for large datasets or repeated comparisons.

## Usage

```csharp
var text1 = "This is a test";
var text2 = "This is another test";

double result = DiceCoefficientStringUtil.CalculatePercentage(text1, text2); // 74.07
```