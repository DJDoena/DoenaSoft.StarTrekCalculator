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

// All user-facing texts, keyed by language. Add a language by adding a new top-level key here.
const Translations =
{
    de:
    {
        lightSpeedResult: "${0} mal die Lichtgeschwindigkeit",
        warpResult: "Warp ${0}",
        utcSuffix: " UTC",
        invalidDate: "${0}-${1}-${2} ist kein gültiges Datum.",
        lightSpeedOutOfRange: "Lichtgeschwindigkeit ist kleiner als ${0} oder größer als ${1}",
        distanceNegative: "Entfernung ist kleiner als 0.",
        lightSpeedTooLow: "Lichtgeschwindigkeit ist kleiner als ${0}",
        warpFactorOutOfRange: "Warpfaktor kleiner als ${0} oder größer als ${1}",
        stardateOutOfRange: "Sternzeit ist kleiner als ${0} oder größer als ${1}"
    },
    en:
    {
        lightSpeedResult: "${0} times the speed of light",
        warpResult: "Warp ${0}",
        utcSuffix: " UTC",
        invalidDate: "${0}-${1}-${2} is not a valid date.",
        lightSpeedOutOfRange: "Lightspeed is smaller than ${0} or bigger than ${1}",
        distanceNegative: "Distance is lower than 0.",
        lightSpeedTooLow: "Lightspeed is lower than ${0}",
        warpFactorOutOfRange: "Warpfactor smaller than ${0} or bigger than ${1}",
        stardateOutOfRange: "Stardate is smaller than ${0} or bigger than ${1}"
    }
};

// Looks up the translation table for the given language code, falling back to English.
function getTranslations(languageCode)
{
    return Translations[languageCode] || Translations.en;
}

// Replaces "${0}", "${1}", ... placeholders in a translation template with the given values.
function formatTranslation(template, values)
{
    let text = template;

    for(let valueIndex = 0; valueIndex < values.length; valueIndex++)
    {
        text = text.replace("${" + valueIndex + "}", values[valueIndex]);
    }

    return text;
}

// Translates a CalculationError (thrown by StarTrekCalculatorSolver.js) into the given
// language, falling back to the error's original (English) message if it has no code.
function translateError(error, languageCode)
{
    if(!error.code)
    {
        return error.message;
    }

    let translations = getTranslations(languageCode);

    let template = translations[error.code];

    if(!template)
    {
        return error.message;
    }

    return formatTranslation(template, error.params);
}

// Reads the year/month/day/hour/minute/second input fields for a date section (identified by
// idPrefix) and builds a UTC-based Date from them, matching the UTC-based convention used by
// the solver's stardateToNormalDate/normalDateToStardate functions.
function readDateInputs(idPrefix, languageCode)
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
        let template = getTranslations(languageCode).invalidDate;

        throw new CalculationError(formatTranslation(template, [year, month, day]));
    }

    return date;
}

// Formats a number as a two-digit string, e.g. 5 -> "05".
function pad2(number)
{
    return String(number).padStart(2, "0");
}

// Parses a decimal number from user input, honoring the German convention of using a
// comma instead of a dot as the decimal separator.
function parseLocaleFloat(text, languageCode)
{
    let normalized = languageCode === "de"
        ? text.replace(",", ".")
        : text;

    return parseFloat(normalized);
}

// Formats a decimal number for display, honoring the German convention of using a
// comma instead of a dot as the decimal separator.
function formatLocaleNumber(number, languageCode)
{
    let text = String(number);

    return languageCode === "de"
        ? text.replace(".", ",")
        : text;
}

// Writes a UTC-based Date into the year/month/day/hour/minute/second input fields for a date
// section (identified by idPrefix). Month/day/hour/minute/second are zero-padded to two digits.
function writeDateInputs(idPrefix, date)
{
    getById(idPrefix + "Year").value = date.getUTCFullYear();

    getById(idPrefix + "Month").value = pad2(date.getUTCMonth() + 1);

    getById(idPrefix + "Day").value = pad2(date.getUTCDate());

    getById(idPrefix + "Hour").value = pad2(date.getUTCHours());

    getById(idPrefix + "Minute").value = pad2(date.getUTCMinutes());

    getById(idPrefix + "Second").value = pad2(date.getUTCSeconds());
}

// "Warp -> Light Speed" button handler.
function calculateWarpToLightSpeed(languageCode)
{
    try
    {
        let warpFactor = parseLocaleFloat(getById("warpFactorInput").value, languageCode);

        let lightSpeed = warpToLightSpeed(warpFactor);

        let text = formatTranslation(getTranslations(languageCode).lightSpeedResult, [formatLocaleNumber(lightSpeed, languageCode)]);

        showResult("warpToLightSpeedOutput", text, false);
    }
    catch(error)
    {
        showResult("warpToLightSpeedOutput", translateError(error, languageCode), true);
    }
}

// "Light Speed -> Warp" button handler.
function calculateLightSpeedToWarp(languageCode)
{
    try
    {
        let lightSpeed = parseLocaleFloat(getById("lightSpeedInput").value, languageCode);

        let warpFactor = lightSpeedToWarp(lightSpeed);

        let text = formatTranslation(getTranslations(languageCode).warpResult, [formatLocaleNumber(warpFactor, languageCode)]);

        showResult("lightSpeedToWarpOutput", text, false);
    }
    catch(error)
    {
        showResult("lightSpeedToWarpOutput", translateError(error, languageCode), true);
    }
}

// "Warp/Light Speed + Distance -> Travel Time" button handler.
function calculateTravelTime(languageCode)
{
    try
    {
        let lightYears = parseLocaleFloat(getById("travelTimeLightYearsInput").value, languageCode);

        let useWarp = getById("travelTimeUseWarp").checked;

        let travelTime;

        if(useWarp)
        {
            let warpFactor = parseLocaleFloat(getById("travelTimeWarpInput").value, languageCode);

            travelTime = warpToTravelTime(warpFactor, lightYears);
        }
        else
        {
            let lightSpeed = parseLocaleFloat(getById("travelTimeLightSpeedInput").value, languageCode);

            travelTime = lightSpeedToTravelTime(lightSpeed, lightYears);
        }

        showResult("travelTimeOutput", travelTimeToString(travelTime), false);
    }
    catch(error)
    {
        showResult("travelTimeOutput", translateError(error, languageCode), true);
    }
}

// "Stardate -> Date" button handler.
function calculateStardateToNormalDate(languageCode)
{
    try
    {
        let stardate = parseLocaleFloat(getById("stardateInput").value, languageCode);

        let normalDate = stardateToNormalDate(stardate);

        writeDateInputs("stardateResult", normalDate);

        let utcSuffix = getTranslations(languageCode).utcSuffix;

        showResult("stardateToNormalDateOutput", normalDate.toISOString().replace("T", " ").replace("Z", utcSuffix), false);
    }
    catch(error)
    {
        showResult("stardateToNormalDateOutput", translateError(error, languageCode), true);
    }
}

// "Date -> Stardate" button handler.
function calculateNormalDateToStardate(languageCode)
{
    try
    {
        let normalDate = readDateInputs("normalDate", languageCode);

        let stardate = normalDateToStardate(normalDate);

        showResult("normalDateToStardateOutput", formatLocaleNumber(stardate, languageCode), false);
    }
    catch(error)
    {
        showResult("normalDateToStardateOutput", translateError(error, languageCode), true);
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
