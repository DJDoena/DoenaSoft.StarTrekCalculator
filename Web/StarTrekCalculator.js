// UI logic: DOM manipulation and form-handling for the four calculation directions.
// Calculation itself lives in StarTrekCalculatorSolver.js.

// Shorthand for document.getElementById.
function getById(id)
{
    let node = document.getElementById(id);

    return node;
}

// Displays a result or error message in the given output element.
function showResult(outputId, text, isError)
{
    let node = getById(outputId);

    node.textContent = text;

    node.style.color = isError ? "red" : "black";
}

// Reads the year/month/day/hour/minute/second input fields for a date section (identified by
// idPrefix) and builds a UTC-based Date from them, matching the UTC-based convention used by
// the solver's stardateToNormalDate/normalDateToStardate functions.
function readDateInputs(idPrefix)
{
    let year = parseInt(getById(idPrefix + "Year").value, 10);

    let month = parseInt(getById(idPrefix + "Month").value, 10);

    let day = parseInt(getById(idPrefix + "Day").value, 10);

    let hour = parseInt(getById(idPrefix + "Hour").value, 10);

    let minute = parseInt(getById(idPrefix + "Minute").value, 10);

    let second = parseInt(getById(idPrefix + "Second").value, 10);

    // setUTCFullYear (unlike the Date constructor / Date.UTC) does not remap years 0-99 to
    // 1900+year, so it is used here to correctly support years 1-99.
    let date = new Date(0);

    date.setUTCFullYear(year, month - 1, day);

    date.setUTCHours(hour, minute, second, 0);

    // Unlike .NET's DateTime constructor (which throws ArgumentOutOfRangeException for an
    // invalid day like February 29th on a non-leap year), JS's Date silently rolls invalid
    // fields over into the following month/day. Detect that rollover here and fail loudly
    // instead, to match the .NET behavior.
    if(date.getUTCFullYear() !== year || date.getUTCMonth() !== month - 1 || date.getUTCDate() !== day)
    {
        throw new CalculationError(`${year}-${month}-${day} is not a valid date.`);
    }

    return date;
}

// Writes a UTC-based Date into the year/month/day/hour/minute/second input fields for a date
// section (identified by idPrefix).
function writeDateInputs(idPrefix, date)
{
    getById(idPrefix + "Year").value = date.getUTCFullYear();

    getById(idPrefix + "Month").value = date.getUTCMonth() + 1;

    getById(idPrefix + "Day").value = date.getUTCDate();

    getById(idPrefix + "Hour").value = date.getUTCHours();

    getById(idPrefix + "Minute").value = date.getUTCMinutes();

    getById(idPrefix + "Second").value = date.getUTCSeconds();
}

// "Warp -> Light Speed" button handler.
function calculateWarpToLightSpeed()
{
    try
    {
        let warpFactor = parseFloat(getById("warpFactorInput").value);

        let lightSpeed = warpToLightSpeed(warpFactor);

        showResult("warpToLightSpeedOutput", `${lightSpeed} times the speed of light`, false);
    }
    catch(error)
    {
        showResult("warpToLightSpeedOutput", error.message, true);
    }
}

// "Light Speed -> Warp" button handler.
function calculateLightSpeedToWarp()
{
    try
    {
        let lightSpeed = parseFloat(getById("lightSpeedInput").value);

        let warpFactor = lightSpeedToWarp(lightSpeed);

        showResult("lightSpeedToWarpOutput", `Warp ${warpFactor}`, false);
    }
    catch(error)
    {
        showResult("lightSpeedToWarpOutput", error.message, true);
    }
}

// "Warp/Light Speed + Distance -> Travel Time" button handler.
function calculateTravelTime()
{
    try
    {
        let lightYears = parseFloat(getById("travelTimeLightYearsInput").value);

        let useWarp = getById("travelTimeUseWarp").checked;

        let travelTime;

        if(useWarp)
        {
            let warpFactor = parseFloat(getById("travelTimeWarpInput").value);

            travelTime = warpToTravelTime(warpFactor, lightYears);
        }
        else
        {
            let lightSpeed = parseFloat(getById("travelTimeLightSpeedInput").value);

            travelTime = lightSpeedToTravelTime(lightSpeed, lightYears);
        }

        showResult("travelTimeOutput", travelTimeToString(travelTime), false);
    }
    catch(error)
    {
        showResult("travelTimeOutput", error.message, true);
    }
}

// "Stardate -> Date" button handler.
function calculateStardateToNormalDate()
{
    try
    {
        let stardate = parseFloat(getById("stardateInput").value);

        let normalDate = stardateToNormalDate(stardate);

        writeDateInputs("stardateResult", normalDate);

        showResult("stardateToNormalDateOutput", normalDate.toISOString().replace("T", " ").replace("Z", " UTC"), false);
    }
    catch(error)
    {
        showResult("stardateToNormalDateOutput", error.message, true);
    }
}

// "Date -> Stardate" button handler.
function calculateNormalDateToStardate()
{
    try
    {
        let normalDate = readDateInputs("normalDate");

        let stardate = normalDateToStardate(normalDate);

        showResult("normalDateToStardateOutput", stardate.toString(), false);
    }
    catch(error)
    {
        showResult("normalDateToStardateOutput", error.message, true);
    }
}

// Toggles which of the Warp / Light Speed inputs is enabled in the Travel Time section,
// based on the "useWarp" checkbox.
function toggleTravelTimeInputMode()
{
    let useWarp = getById("travelTimeUseWarp").checked;

    getById("travelTimeWarpInput").disabled = !useWarp;

    getById("travelTimeLightSpeedInput").disabled = useWarp;
}
