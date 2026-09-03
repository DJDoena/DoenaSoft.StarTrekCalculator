const fs = require("fs");
const path = require("path");
const vm = require("vm");

const scriptPath = path.join(__dirname, "StarTrekCalculatorSolver.js");
const source = fs.readFileSync(scriptPath, "utf8");

// The solver file has no DOM dependency, so a plain sandbox is sufficient.
const sandbox = { console: console };
vm.createContext(sandbox);
vm.runInContext(source, sandbox, { filename: "StarTrekCalculatorSolver.js" });

// Top-level `class`/`function`/`const` declarations live in the context's lexical scope,
// not as properties of the sandbox object, so fetch references via another eval.
const warpToLightSpeed = vm.runInContext("warpToLightSpeed", sandbox);
const lightSpeedToWarp = vm.runInContext("lightSpeedToWarp", sandbox);
const lightSpeedToTravelTime = vm.runInContext("lightSpeedToTravelTime", sandbox);
const warpToTravelTime = vm.runInContext("warpToTravelTime", sandbox);
const travelTimeToString = vm.runInContext("travelTimeToString", sandbox);
const stardateToNormalDate = vm.runInContext("stardateToNormalDate", sandbox);
const normalDateToStardate = vm.runInContext("normalDateToStardate", sandbox);
const CalculationError = vm.runInContext("CalculationError", sandbox);
const MinWarpFactor = vm.runInContext("MinWarpFactor", sandbox);
const MaxWarpFactor = vm.runInContext("MaxWarpFactor", sandbox);
const MinLightSpeed = vm.runInContext("MinLightSpeed", sandbox);
const MaxLightSpeed = vm.runInContext("MaxLightSpeed", sandbox);
const MinStardate = vm.runInContext("MinStardate", sandbox);
const MaxStardate = vm.runInContext("MaxStardate", sandbox);

let allPassed = true;

// Builds a UTC-based Date without falling for the Date/Date.UTC two-digit-year (0-99) remap
// to 1900+year; setUTCFullYear does not have that quirk.
function makeUtcDate(year, month, day, hour, minute, second)
{
    let date = new Date(0);

    date.setUTCFullYear(year, month - 1, day);

    date.setUTCHours(hour, minute, second, 0);

    return date;
}

function runCase(name, actual, expected)
{
    let pass = actual === expected;

    console.log(`${pass ? "PASS" : "FAIL"} - ${name}: expected ${expected}, got ${actual}`);

    return pass;
}

function runThrowsCase(name, action)
{
    let pass = false;

    try
    {
        action();
    }
    catch(error)
    {
        pass = error instanceof CalculationError;
    }

    console.log(`${pass ? "PASS" : "FAIL"} - ${name}: expected a CalculationError to be thrown`);

    return pass;
}

// Mirrors WarpToLightSpeed.cs
allPassed = runCase("WarpToLightSpeed.MinWarp", warpToLightSpeed(MinWarpFactor), MinLightSpeed) && allPassed;
allPassed = runCase("WarpToLightSpeed.MaxWarp", warpToLightSpeed(MaxWarpFactor), 502439.251678) && allPassed;
allPassed = runThrowsCase("WarpToLightSpeed.MinWarpFail", () => warpToLightSpeed(0.9)) && allPassed;
allPassed = runThrowsCase("WarpToLightSpeed.MaxWarpFail", () => warpToLightSpeed(9.9999991)) && allPassed;
allPassed = runCase("WarpToLightSpeed.Warp1", warpToLightSpeed(1), 1) && allPassed;
allPassed = runCase("WarpToLightSpeed.Warp2", warpToLightSpeed(2), 10.079369) && allPassed;
allPassed = runCase("WarpToLightSpeed.Warp3", warpToLightSpeed(3), 38.940744) && allPassed;
allPassed = runCase("WarpToLightSpeed.Warp4", warpToLightSpeed(4), 101.593719) && allPassed;
allPassed = runCase("WarpToLightSpeed.Warp5", warpToLightSpeed(5), 213.747391) && allPassed;
allPassed = runCase("WarpToLightSpeed.Warp6", warpToLightSpeed(6), 392.501078) && allPassed;
allPassed = runCase("WarpToLightSpeed.Warp7", warpToLightSpeed(7), 656.161051) && allPassed;
allPassed = runCase("WarpToLightSpeed.Warp8", warpToLightSpeed(8), 1024.3124) && allPassed;
allPassed = runCase("WarpToLightSpeed.Warp9", warpToLightSpeed(9), 1516.425835) && allPassed;
allPassed = runCase("WarpToLightSpeed.Warp9_2", warpToLightSpeed(9.2), 1648.95694) && allPassed;
allPassed = runCase("WarpToLightSpeed.Warp9_6", warpToLightSpeed(9.6), 1909.292835) && allPassed;
allPassed = runCase("WarpToLightSpeed.Warp9_9", warpToLightSpeed(9.9), 3052.946441) && allPassed;
allPassed = runCase("WarpToLightSpeed.Warp9_99", warpToLightSpeed(9.99), 7912.353483) && allPassed;
allPassed = runCase("WarpToLightSpeed.Warp9_9999", warpToLightSpeed(9.9999), 199515.905343) && allPassed;

// Mirrors LightSpeedToWarp.cs
allPassed = runCase("LightSpeedToWarp.MinLightSpeed", lightSpeedToWarp(MinLightSpeed), MinWarpFactor) && allPassed;
allPassed = runCase("LightSpeedToWarp.MaxLightSpeed", lightSpeedToWarp(MaxLightSpeed), MaxWarpFactor) && allPassed;
allPassed = runThrowsCase("LightSpeedToWarp.MinLightSpeedFail", () => lightSpeedToWarp(0.9)) && allPassed;
allPassed = runThrowsCase("LightSpeedToWarp.MaxLightSpeedFail", () => lightSpeedToWarp(MaxLightSpeed + 1)) && allPassed;
allPassed = runCase("LightSpeedToWarp.Warp1", lightSpeedToWarp(1), 1) && allPassed;
allPassed = runCase("LightSpeedToWarp.Warp2", lightSpeedToWarp(10.079369), 2) && allPassed;
allPassed = runCase("LightSpeedToWarp.Warp3", lightSpeedToWarp(38.940744), 3) && allPassed;
allPassed = runCase("LightSpeedToWarp.Warp4", lightSpeedToWarp(101.593719), 4) && allPassed;
allPassed = runCase("LightSpeedToWarp.Warp5", lightSpeedToWarp(213.747391), 5) && allPassed;
allPassed = runCase("LightSpeedToWarp.Warp6", lightSpeedToWarp(392.501078), 6) && allPassed;
allPassed = runCase("LightSpeedToWarp.Warp7", lightSpeedToWarp(656.161051), 7) && allPassed;
allPassed = runCase("LightSpeedToWarp.Warp8", lightSpeedToWarp(1024.3124), 8) && allPassed;
allPassed = runCase("LightSpeedToWarp.Warp9", lightSpeedToWarp(1516.425835), 9) && allPassed;
allPassed = runCase("LightSpeedToWarp.Warp9_2", lightSpeedToWarp(1648.95694), 9.2) && allPassed;
allPassed = runCase("LightSpeedToWarp.Warp9_6", lightSpeedToWarp(1909.292835), 9.6) && allPassed;
allPassed = runCase("LightSpeedToWarp.Warp9_9", lightSpeedToWarp(3052.946441), 9.9) && allPassed;
allPassed = runCase("LightSpeedToWarp.Warp9_99", lightSpeedToWarp(7912.353483), 9.99) && allPassed;
allPassed = runCase("LightSpeedToWarp.Warp9_9999", lightSpeedToWarp(199515.905343), 9.9999) && allPassed;

// Mirrors TravelTime.cs (VoyagerDeltaQuadrant)
{
    let travelTime = warpToTravelTime(8, 70000);

    allPassed = runCase("TravelTime.VoyagerDeltaQuadrant.Years", travelTime.years, 68) && allPassed;
    allPassed = runCase("TravelTime.VoyagerDeltaQuadrant.Days", travelTime.days, 123) && allPassed;
    allPassed = runCase("TravelTime.VoyagerDeltaQuadrant.Hours", travelTime.hours, 15) && allPassed;
    allPassed = runCase("TravelTime.VoyagerDeltaQuadrant.Minutes", travelTime.minutes, 27) && allPassed;
    allPassed = runCase("TravelTime.VoyagerDeltaQuadrant.Seconds", travelTime.seconds, 41) && allPassed;
    allPassed = runCase("TravelTime.VoyagerDeltaQuadrant.ToString", travelTimeToString(travelTime), "68a 123d 15h 27m 41s") && allPassed;
}

// Mirrors LightSpeedToTravelTime error cases (Warp.vb)
allPassed = runThrowsCase("LightSpeedToTravelTime.LightSpeedTooLow", () => lightSpeedToTravelTime(0.9, 10)) && allPassed;
allPassed = runThrowsCase("LightSpeedToTravelTime.DistanceTooLow", () => lightSpeedToTravelTime(10, 0)) && allPassed;

// Mirrors StardateConstants.cs
allPassed = runCase("StardateConstants.MinStardate", MinStardate, -2322000) && allPassed;
allPassed = runCase("StardateConstants.MaxStardate", MaxStardate, 7676999.999999) && allPassed;

// Mirrors StardateToNormalDate.cs
allPassed = runCase("StardateToNormalDate.MinStardate", stardateToNormalDate(MinStardate).toISOString().slice(0, 19).replace("T", " "), "0001-01-01 00:00:00") && allPassed;
allPassed = runCase("StardateToNormalDate.MaxStardate", stardateToNormalDate(MaxStardate).toISOString().slice(0, 19).replace("T", " "), "9999-12-31 23:59:59") && allPassed;
allPassed = runThrowsCase("StardateToNormalDate.MinStardateFail", () => stardateToNormalDate(MinStardate - 1)) && allPassed;
allPassed = runThrowsCase("StardateToNormalDate.MaxStardateFail", () => stardateToNormalDate(MaxStardate + 1)) && allPassed;
allPassed = runCase("StardateToNormalDate.FirstStardate", stardateToNormalDate(0).toISOString().slice(0, 19).replace("T", " "), "2323-01-01 00:00:00") && allPassed;
allPassed = runCase("StardateToNormalDate.EncounterAtFarpoint", stardateToNormalDate(41153.7).toISOString().slice(0, 19).replace("T", " "), "2364-02-26 06:06:02") && allPassed;
allPassed = runCase("StardateToNormalDate.BattleOfWolf359", stardateToNormalDate(43997).toISOString().slice(0, 19).replace("T", " "), "2366-12-30 21:43:12") && allPassed;

// Mirrors NormalDateToStardate.cs
allPassed = runCase("NormalDateToStardate.MinStardate", normalDateToStardate(makeUtcDate(1, 1, 1, 0, 0, 0)), MinStardate) && allPassed;
allPassed = runCase("NormalDateToStardate.MaxStardate", normalDateToStardate(makeUtcDate(9999, 12, 31, 23, 59, 59)), 7676999.999968) && allPassed;
allPassed = runCase("NormalDateToStardate.FirstStardate", normalDateToStardate(makeUtcDate(2323, 1, 1, 0, 0, 0)), 0) && allPassed;
allPassed = runCase("NormalDateToStardate.EncounterAtFarpoint", normalDateToStardate(makeUtcDate(2364, 2, 26, 6, 6, 2)), 41153.699972) && allPassed;
allPassed = runCase("NormalDateToStardate.BattleOfWolf359", normalDateToStardate(makeUtcDate(2366, 12, 30, 21, 43, 12)), 43997) && allPassed;

// Mirrors the readDateInputs rollover-detection added to StarTrekCalculator.js: JS's Date
// silently rolls an invalid day (e.g. February 29th on a non-leap year) over into the
// following month, so the UI validates the constructed date's fields against the requested
// ones and throws a CalculationError on mismatch.
function readDateInputsInvalidCheck(year, month, day)
{
    let date = makeUtcDate(year, month, day, 0, 0, 0);

    if(date.getUTCFullYear() !== year || date.getUTCMonth() !== month - 1 || date.getUTCDate() !== day)
    {
        throw new CalculationError(`${year}-${month}-${day} is not a valid date.`);
    }

    return date;
}

allPassed = runThrowsCase("NormalDateToStardate.InvalidDateFails", () => readDateInputsInvalidCheck(2365, 2, 29)) && allPassed;

console.log(allPassed ? "\nAll cases passed." : "\nSome cases FAILED.");

process.exitCode = allPassed ? 0 : 1;
