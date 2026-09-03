// Pure calculation logic, ported from the DoenaSoft.StarTrekCalculator .NET library
// (Warp.vb, Stardate.vb, TravelTime.vb). No DOM access happens in this file; see
// StarTrekCalculator.js for the UI wiring that calls into these functions.

const MinWarpFactor = 1;
const MaxWarpFactor = 9.999999;
const MinLightSpeed = 1;
const MaxLightSpeed = 500000;
const OneYearInDays = 365.2425;

const MinStardate = -2322000;
const MaxStardate = 7676999.999999;

// Equivalent of DoenaSoft.StarTrekCalculator.CalculationException: thrown whenever an input
// is outside of the supported range for a calculation.
class CalculationError extends Error
{
}

// Equivalent of DoenaSoft.StarTrekCalculator.TravelTime: a plain result object describing a
// travel time broken down into years/days/hours/minutes/seconds.
function createTravelTime(years, days, hours, minutes, seconds)
{
    return {
        years: years,
        days: days,
        hours: hours,
        minutes: minutes,
        seconds: seconds
    };
}

// Equivalent of TravelTime.ToString().
function travelTimeToString(travelTime)
{
    if(travelTime.years > 0)
    {
        return `${travelTime.years}a ${travelTime.days}d ${travelTime.hours}h ${travelTime.minutes}m ${travelTime.seconds}s`;
    }
    else if(travelTime.days > 0)
    {
        return `${travelTime.days}d ${travelTime.hours}h ${travelTime.minutes}m ${travelTime.seconds}s`;
    }
    else
    {
        return `${travelTime.hours}h ${travelTime.minutes}m ${travelTime.seconds}s`;
    }
}

// Truncates the fractional part of a number, returning only its integral part.
// Equivalent of the Helper.Truncate extension method.
function truncate(number)
{
    return Math.trunc(number);
}

// Converts a Warp factor into a multiple of c (light speed).
// Equivalent of the public Warp.WarpToLightSpeed(warpFactor) overload.
function warpToLightSpeed(warpFactor)
{
    return calculateLightSpeed(warpFactor, false);
}

// Converts a multiple of c (light speed) to a Warp factor.
// Equivalent of Warp.LightSpeedToWarp(lightSpeed).
function lightSpeedToWarp(lightSpeed)
{
    if(lightSpeed >= MinLightSpeed && lightSpeed <= MaxLightSpeed)
    {
        return calculateWarp(lightSpeed);
    }
    else
    {
        throw new CalculationError(`Lightspeed is smaller than ${MinLightSpeed} or bigger than ${MaxLightSpeed}`);
    }
}

// Performs the Newton approximation search for the Warp factor that yields the given light
// speed multiple. Equivalent of the private Warp.CalculateWarp(lightSpeed) function.
function calculateWarp(lightSpeed)
{
    let bounds = getWarpSearchRange(lightSpeed);

    let minWarp = bounds.minWarp;

    let maxWarp = bounds.maxWarp;

    let warp = 1;

    for(let counter = 1; counter <= 100; counter++)
    {
        warp = (minWarp + maxWarp) / 2;

        let calculatedLightSpeed = calculateLightSpeed(warp, true);

        if(round(lightSpeed, 6) === calculatedLightSpeed)
        {
            break;
        }
        else if(calculatedLightSpeed < lightSpeed)
        {
            minWarp = warp;
        }
        else if(calculatedLightSpeed > lightSpeed)
        {
            maxWarp = warp;
        }
    }

    return round(warp, 6);
}

// Determines the lower and upper Warp factor bounds within which the given light speed
// multiple falls, used as the initial search interval for the Newton approximation in
// calculateWarp. Equivalent of the private Warp.GetWarpSearchRange(lightSpeed) function.
function getWarpSearchRange(lightSpeed)
{
    let minWarp = 1;

    let maxWarp = 10;

    if(lightSpeed >= 502440)
    {
        minWarp = 9.999999;
    }
    else if(lightSpeed >= 204851)
    {
        minWarp = 9.99999;
    }
    else if(lightSpeed >= 199516)
    {
        minWarp = 9.9999;
        maxWarp = 9.999999;
    }
    else if(lightSpeed >= 10268)
    {
        minWarp = 9.999;
        maxWarp = 9.99999;
    }
    else if(lightSpeed >= 7913)
    {
        minWarp = 9.99;
        maxWarp = 9.9999;
    }
    else if(lightSpeed >= 3053)
    {
        minWarp = 9.9;
        maxWarp = 9.999;
    }
    else if(lightSpeed >= 1517)
    {
        minWarp = 9;
        maxWarp = 9.99;
    }
    else if(lightSpeed >= 1025)
    {
        minWarp = 8;
        maxWarp = 9.9;
    }
    else if(lightSpeed >= 1025)
    {
        minWarp = 7;
        maxWarp = 9;
    }
    else if(lightSpeed >= 657)
    {
        minWarp = 6;
        maxWarp = 8;
    }
    else if(lightSpeed >= 393)
    {
        minWarp = 5;
        maxWarp = 7;
    }
    else if(lightSpeed >= 102)
    {
        minWarp = 4;
        maxWarp = 6;
    }
    else if(lightSpeed >= 39)
    {
        minWarp = 3;
        maxWarp = 5;
    }
    else if(lightSpeed >= 11)
    {
        minWarp = 2;
        maxWarp = 4;
    }
    else if(lightSpeed > 1)
    {
        maxWarp = 3;
    }
    else
    {
        maxWarp = 1;
    }

    return { minWarp: minWarp, maxWarp: maxWarp };
}

// Calculates the travel time based upon the given factor of light speed and distance in
// light years. Equivalent of Warp.LightSpeedToTravelTime(lightSpeed, lightYears).
function lightSpeedToTravelTime(lightSpeed, lightYears)
{
    if(lightSpeed >= MinLightSpeed)
    {
        if(lightYears > 0)
        {
            let temp = lightYears / lightSpeed;

            let years = truncate(temp);

            temp -= years;

            let days = truncate(temp * OneYearInDays);

            temp = (temp * OneYearInDays) - days;

            let hours = truncate(temp * 24);

            temp = (temp * 24) - hours;

            let minutes = truncate(temp * 60);

            temp = (temp * 60) - minutes;

            let seconds = truncate(temp * 60);

            return createTravelTime(years, days, hours, minutes, seconds);
        }
        else
        {
            throw new CalculationError("Distance is lower than 0.");
        }
    }
    else
    {
        throw new CalculationError(`Lightspeed is lower than ${MinLightSpeed}`);
    }
}

// Calculates the travel time based upon the given Warp factor and distance in light years.
// Equivalent of Warp.WarpToTravelTime(warpFactor, lightYears).
function warpToTravelTime(warpFactor, lightYears)
{
    let lightSpeed = warpToLightSpeed(warpFactor);

    return lightSpeedToTravelTime(lightSpeed, lightYears);
}

// Converts a Warp factor into a multiple of c (light speed), with an internalCall escape
// hatch used by the recursive Newton approximation in calculateWarp.
// Equivalent of the private Warp.WarpToLightSpeed(warpFactor, internalCall) overload.
function calculateLightSpeed(warpFactor, internalCall)
{
    if(internalCall || (warpFactor >= MinWarpFactor && warpFactor <= MaxWarpFactor))
    {
        let ln10 = Math.log(10);

        // distance (in warp factors) from the theoretical Warp 10 asymptote
        let inverseWarp = 10 - warpFactor;

        // dampens the correction curve for low warp factors (barely affects results near Warp 10)
        let dampingFactor = 0.20467 * Math.exp(-0.0058 * ((Math.log(10000 * inverseWarp) / ln10) ** 5));

        // introduces the wave-like ripple seen in the official TNG warp chart around Warp 9.99+
        let rippleFactor = 1 + (2 * Math.cos(10 * Math.PI * Math.log(8 / (10 * inverseWarp)) / ln10) - 1) / 3 * Math.exp(-49.369 * ((Math.log(8 / (10 * inverseWarp)) / ln10) ** 4));

        // steepens the curve as warpFactor approaches the Warp 10 asymptote (approaches infinite speed)
        let asymptoteFactor = 1 + 1.88269 / Math.PI * (Math.PI / 2 - Math.atan((10 ** warpFactor) * Math.log(2000 * inverseWarp) / ln10));

        // combined exponent applied to the warp factor to yield the multiple of light speed
        let exponent = warpFactor ** (10 / 3 * (1 + (dampingFactor * rippleFactor * asymptoteFactor)));

        return round(exponent, 6);
    }
    else
    {
        throw new CalculationError(`Warpfactor smaller than ${MinWarpFactor} or bigger than ${MaxWarpFactor}`);
    }
}

// Rounds a number to the given number of decimal places (round-half-away-from-zero, matching
// the behavior of VB.NET's Math.Round for the positive values used throughout this module).
function round(number, decimals)
{
    let factor = 10 ** decimals;

    return Math.round(number * factor) / factor;
}

// Determines the number of days in the given year, accounting for leap years.
// Equivalent of CultureInfo.InvariantCulture.Calendar.GetDaysInYear(year).
function daysInYear(year)
{
    let isLeapYear = (new Date(year, 1, 29)).getMonth() === 1;

    return isLeapYear ? 366 : 365;
}

// Calculates an actual date based on a given stardate.
// Equivalent of Stardate.StardateToNormalDate(stardate).
function stardateToNormalDate(stardate)
{
    if(stardate < MinStardate || stardate > MaxStardate)
    {
        throw new CalculationError(`Stardate is smaller than ${MinStardate} or bigger than ${MaxStardate}`);
    }

    stardate = round(stardate, 6);

    let temp = stardate / 1000;

    let myYear = truncate(temp) + 2323;

    temp -= truncate(temp);

    let days = daysInYear(myYear);

    temp *= days;

    // Year 1, January 1st, 00:00:00 as the calculation baseline (equivalent of VB's Date(1, 1, 1)).
    // setUTCFullYear (unlike the Date constructor / Date.UTC) does not remap years 0-99 to 1900+year,
    // so it is used here to represent year 1 correctly.
    let normalDate = new Date(0);

    normalDate.setUTCFullYear(myYear, 0, 1);

    normalDate.setUTCHours(0, 0, 0, 0);

    let millisecondsOfDay = Math.round(temp * 86400000);

    normalDate = new Date(normalDate.getTime() + millisecondsOfDay);

    return normalDate;
}

// Calculates a stardate based on an actual date.
// Equivalent of Stardate.NormalDateToStardate(normalDate).
function normalDateToStardate(normalDate)
{
    let dayOfYear = getDayOfYear(normalDate);

    let dayPart = (normalDate.getUTCSeconds() + normalDate.getUTCMinutes() * 60 + normalDate.getUTCHours() * 3600) / 86400;

    let stardateLowerPart = (dayOfYear - 1 + dayPart) / daysInYear(normalDate.getUTCFullYear()) * 1000;

    let stardateUpperPart = (normalDate.getUTCFullYear() - 2323) * 1000;

    return round(stardateUpperPart + stardateLowerPart, 6);
}

// Determines the 1-based day-of-year (equivalent of .NET's DateTime.DayOfYear) for a UTC date.
function getDayOfYear(normalDate)
{
    let startOfYear = Date.UTC(normalDate.getUTCFullYear(), 0, 1);

    let startOfDay = Date.UTC(normalDate.getUTCFullYear(), normalDate.getUTCMonth(), normalDate.getUTCDate());

    let dayOfYear = Math.round((startOfDay - startOfYear) / 86400000) + 1;

    return dayOfYear;
}

