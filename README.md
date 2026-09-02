# DoenaSoft.StarTrekCalculator

This package calculates the Warp factors of Star Trek: The Next Generation / Deep Space Nine / Voyager into multiples of the speed of light (c).

It can also convert it the other way around.

Given a Warp factor and a distance in light years it can also calculate the time it takes to accomplish the journey.

The chart for the Warp factors can be found in [The Official Star Trek Files](https://github.com/DJDoena/DoenaSoft.StarTrekCalculator/blob/main/DoenaSoft.StarTrekCalculator/warpchart.jpg).

It can also convert the Stardate system used in these shows to convert them into actual dates and vice versa.

## Warp Calculation

`Warp.WarpToLightSpeed(warpFactor)` converts a Warp factor (between `1` and `9.999999`) into a multiple of `c` using the formula devised to reproduce the official TNG warp chart. The factor `10 - warpFactor` (the distance to the theoretical Warp 10 asymptote) is fed into three correction terms:

- `dampingFactor` dampens the correction for low warp factors, having little effect near Warp 10.
- `rippleFactor` reproduces the wave-like ripple visible in the official chart around Warp 9.99 and above.
- `asymptoteFactor` steepens the curve as the warp factor approaches Warp 10, where speed becomes theoretically infinite.

These terms are combined into an exponent that is applied to the warp factor itself, yielding the multiple of light speed, rounded to 6 decimal places.

`Warp.LightSpeedToWarp(lightSpeed)` performs the reverse conversion (between `1c` and `500000c`). Since the formula above cannot be inverted algebraically, this method uses a binary search (bisection / Newton-style narrowing): starting from a `min`/`max` warp range determined by the input light speed, it repeatedly calculates the midpoint's equivalent light speed via `WarpToLightSpeed` and narrows the range until the calculated value matches the requested light speed (rounded to 6 decimal places), or 100 iterations have passed.

## Stardate Calculation

The Stardate system implemented here assumes each in-universe year corresponds to 1000 stardates, and that the stardate system's epoch (stardate `0`) begins on January 1st, 2323 - chosen so that the first season of TNG (which starts around stardate `41000`) lines up with the show's real-world premiere year of 2364.

`Stardate.StardateToNormalDate(stardate)` converts a stardate (between `MinStardate` and `MaxStardate`) into an actual `Date`:

1. The stardate is divided by 1000 to determine the calendar year (the integer part, offset by 2323) and the fractional position within that year (the remainder).
2. The fractional part is multiplied by the number of days in that year (accounting for leap years) to get a fractional day count.
3. That fractional day count is converted into exact ticks and added to January 1st of the resulting year to produce the final date and time.

`Stardate.NormalDateToStardate(normalDate)` performs the reverse conversion:

1. The time of day is converted into a fraction of a day (`seconds + minutes * 60 + hours * 3600`, divided by the number of seconds in a day).
2. That fraction is added to the (zero-based) day of the year and divided by the total number of days in that year, then multiplied by 1000 to get the stardate's fractional/lower part.
3. The year offset from 2323 multiplied by 1000 gives the stardate's whole/upper part.
4. Both parts are summed and rounded to 6 decimal places to produce the final stardate.
