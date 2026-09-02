''' <summary>
''' This class calculates the Star Trek: The Next Generation / Deep Space Nine / Voyager warp scale.
''' The official numbers can be seen in this chart from <a href="https://i.stack.imgur.com/ZBsFO.gif">The Official Star Trek Fact Files</a>.
''' </summary>
Public Module Warp
    ''' <summary>
    ''' The minimum Warp factor supported by the calculations of this module.
    ''' </summary>
    Public Const MinWarpFactor As Double = 1

    ''' <summary>
    ''' The maximum Warp factor supported by the calculations of this module.
    ''' </summary>
    Public Const MaxWarpFactor As Double = 9.999_999

    ''' <summary>
    ''' The minimum multiple of light speed supported by the calculations of this module.
    ''' </summary>
    Public Const MinLightSpeed As Double = 1

    ''' <summary>
    ''' The maximum multiple of light speed supported by the calculations of this module.
    ''' </summary>
    Public Const MaxLightSpeed As Double = 500_000

    ''' <summary>
    ''' The average number of days in a year, used to convert between years and days in travel time calculations.
    ''' </summary>
    Friend Const OneYearInDays As Double = 365.2425

    ''' <summary>
    ''' Converts a Warp factor into a multiple of c (light speed)
    ''' </summary>
    ''' <param name="warpFactor">the Warp factor</param>
    ''' <returns>the multiple of light speed</returns>
    ''' <exception cref="CalculationException">the Warp factor is smaller than <see cref="MinWarpFactor"/> or bigger than <see cref="MaxWarpFactor"/></exception>
    Public Function WarpToLightSpeed(ByVal warpFactor As Double) As Double
        Return WarpToLightSpeed(warpFactor, False)
    End Function

    ''' <summary>
    ''' Converts a multiple of c (light speed) to a Warp factor
    ''' </summary>
    ''' <param name="lightSpeed">the multiple of light speed</param>
    ''' <returns>the Warp factor</returns>
    ''' <exception cref="CalculationException">the light speed is smaller than <see cref="MinLightSpeed"/> or bigger than <see cref="MaxLightSpeed"/></exception>
    Public Function LightSpeedToWarp(ByVal lightSpeed As Double) As Double
        Select Case lightSpeed
            Case MinLightSpeed To MaxLightSpeed
                Return CalculateWarp(lightSpeed)
            Case Else
                Throw New CalculationException($"Lightspeed is smaller than {MinLightSpeed} or bigger than {MaxLightSpeed}")
        End Select
    End Function

    ''' <summary>
    ''' Performs the Newton approximation search for the Warp factor that yields the given light speed multiple.
    ''' </summary>
    ''' <param name="lightSpeed">the multiple of light speed</param>
    ''' <returns>the Warp factor</returns>
    Private Function CalculateWarp(lightSpeed As Double) As Double
        Dim bounds As (minWarp As Double, maxWarp As Double) = GetWarpSearchRange(lightSpeed)

        Dim minWarp As Double = bounds.minWarp

        Dim maxWarp As Double = bounds.maxWarp

        Dim warp As Double = 1

        Debug.WriteLine("Starting Newton")

        For counter As Integer = 1 To 100
            Debug.WriteLine($"Iteration: {counter}")
            Debug.WriteLine($"Min / Max Warp: {minWarp} / {maxWarp}")

            warp = (minWarp + maxWarp) / 2

            Debug.WriteLine($"Warp: {warp}")

            Dim calculatedLightspeed As Double = WarpToLightSpeed(warp, True)

            Debug.WriteLine($"Light speed: {calculatedLightspeed}")

            If Math.Round(lightSpeed, 6) = calculatedLightspeed Then
                Exit For
            ElseIf calculatedLightspeed < lightSpeed Then
                minWarp = warp
            ElseIf calculatedLightspeed > lightSpeed Then
                maxWarp = warp
            End If
        Next counter

        Return Math.Round(warp, 6)
    End Function

    ''' <summary>
    ''' Determines the lower and upper Warp factor bounds within which the given light speed multiple falls,
    ''' used as the initial search interval for the Newton approximation in <see cref="LightSpeedToWarp"/>.
    ''' </summary>
    ''' <param name="lightSpeed">the multiple of light speed</param>
    ''' <returns>the minimum and maximum Warp factor of the search interval</returns>
    Private Function GetWarpSearchRange(ByVal lightSpeed As Double) As (minWarp As Double, maxWarp As Double)
        Dim minWarp As Double = 1

        Dim maxWarp As Double = 10

        If lightSpeed >= 502440 Then
            minWarp = 9.999999
        ElseIf lightSpeed >= 204851 Then
            minWarp = 9.99999
        ElseIf lightSpeed >= 199516 Then
            minWarp = 9.9999
            maxWarp = 9.999999
        ElseIf lightSpeed >= 10268 Then
            minWarp = 9.999
            maxWarp = 9.99999
        ElseIf lightSpeed >= 7913 Then
            minWarp = 9.99
            maxWarp = 9.9999
        ElseIf lightSpeed >= 3053 Then
            minWarp = 9.9
            maxWarp = 9.999
        ElseIf lightSpeed >= 1517 Then
            minWarp = 9
            maxWarp = 9.99
        ElseIf lightSpeed >= 1025 Then
            minWarp = 8
            maxWarp = 9.9
        ElseIf lightSpeed >= 1025 Then
            minWarp = 7
            maxWarp = 9
        ElseIf lightSpeed >= 657 Then
            minWarp = 6
            maxWarp = 8
        ElseIf lightSpeed >= 393 Then
            minWarp = 5
            maxWarp = 7
        ElseIf lightSpeed >= 102 Then
            minWarp = 4
            maxWarp = 6
        ElseIf lightSpeed >= 39 Then
            minWarp = 3
            maxWarp = 5
        ElseIf lightSpeed >= 11 Then
            minWarp = 2
            maxWarp = 4
        ElseIf lightSpeed > 1 Then
            maxWarp = 3
        Else
            maxWarp = 1
        End If

        Return (minWarp, maxWarp)
    End Function

    ''' <summary>
    ''' Calculates the travel time based upon the given factor of light speed and distance in light years
    ''' </summary>
    ''' <param name="lightSpeed">the multiple of light speed</param>
    ''' <param name="lightYears">the distance in light years (9,460,730,472,580,800 km)</param>
    ''' <returns>the travel time</returns>
    ''' <remarks>
    ''' An average year is calculated with 365.2425 days.
    ''' This is based on the leap year logic wich results in 97 leap days in 400 years.
    ''' </remarks>
    ''' <exception cref="CalculationException">the light speed is lower than <see cref="MinLightSpeed"/>, or the distance is lower than or equal to 0</exception>
    Public Function LightSpeedToTravelTime(ByVal lightSpeed As Double, ByVal lightYears As Double) As TravelTime
        If lightSpeed >= MinLightSpeed Then
            If lightYears > 0 Then
                Dim temp As Double = lightYears / lightSpeed

                Dim years As Long = Convert.ToInt64(temp.Truncate())

                temp -= years

                Dim days As Short = Convert.ToInt16((temp * OneYearInDays).Truncate())

                temp = (temp * OneYearInDays) - days

                Dim hours As Short = Convert.ToInt16((temp * 24).Truncate())

                temp = (temp * 24) - hours

                Dim minutes As Short = Convert.ToInt16((temp * 60).Truncate())

                temp = (temp * 60) - minutes

                Dim seconds As Short = Convert.ToInt16((temp * 60).Truncate())

                Return New TravelTime(years, days, hours, minutes, seconds)
            Else
                Throw New CalculationException("Distance is lower than 0.")
            End If
        Else
            Throw New CalculationException($"Lightspeed is lower than {MinLightSpeed}")
        End If
    End Function

    ''' <summary>
    ''' Calculates the travel time based upon the given Warp factor and distance in light years
    ''' </summary>
    ''' <param name="warpFactor">the Warp factor</param>
    ''' <param name="lightYears">the distance in light years (9,460,730,472,580,800 km)</param>
    ''' <returns>the travel time</returns>
    ''' <remarks>An average year is calculated with 365.2425 days.
    ''' This is based on the leap year logic with results in 97 leap days in 400 years.</remarks>
    ''' <exception cref="CalculationException">the Warp factor is smaller than <see cref="MinWarpFactor"/> or bigger than <see cref="MaxWarpFactor"/>, or the distance is lower than or equal to 0</exception>
    Public Function WarpToTravelTime(ByVal warpFactor As Double, ByVal lightYears As Double) As TravelTime
        Dim lightspeed As Double = WarpToLightSpeed(warpFactor)

        Dim travelTime As TravelTime = LightSpeedToTravelTime(lightspeed, lightYears)

        Return travelTime
    End Function

    ''' <summary>
    ''' Converts a Warp factor into a multiple of c (light speed).
    ''' </summary>
    ''' <param name="warpFactor">the Warp factor</param>
    ''' <param name="internalCall">if <see langword="True"/>, allows <paramref name="warpFactor"/> to be outside the public min/max range,
    ''' as used by the recursive Newton approximation in <see cref="LightSpeedToWarp"/></param>
    ''' <returns>the multiple of light speed</returns>
    ''' <exception cref="CalculationException"><paramref name="internalCall"/> is <see langword="False"/> and the Warp factor is smaller than <see cref="MinWarpFactor"/> or bigger than <see cref="MaxWarpFactor"/></exception>
    Private Function WarpToLightSpeed(ByVal warpFactor As Double, ByVal internalCall As Boolean) As Double
        ' internal (recursive) calls from LightSpeedToWarp's Newton approximation are allowed to
        ' pass warp factors outside the public min/max range while the search interval converges.
        If internalCall OrElse (warpFactor >= MinWarpFactor AndAlso warpFactor <= MaxWarpFactor) Then
            Dim ln10 As Double = Math.Log(10)

            ' distance (in warp factors) from the theoretical Warp 10 asymptote
            Dim inverseWarp As Double = 10 - warpFactor

            ' dampens the correction curve for low warp factors (barely affects results near Warp 10)
            Dim dampingFactor As Double = 0.20467 * Math.Exp(-0.0058 * ((Math.Log(10000 * inverseWarp) / ln10) ^ 5))

            ' introduces the wave-like ripple seen in the official TNG warp chart around Warp 9.99+
            Dim rippleFactor As Double = 1 + (2 * Math.Cos(10 * Math.PI * Math.Log(8 / (10 * inverseWarp)) / ln10) - 1) / 3 * Math.Exp(-49.369 * ((Math.Log(8 / (10 * inverseWarp)) / ln10) ^ 4))

            ' steepens the curve as warpFactor approaches the Warp 10 asymptote (approaches infinite speed)
            Dim asymptoteFactor As Double = 1 + 1.88269 / Math.PI * (Math.PI / 2 - Math.Atan((10 ^ warpFactor) * Math.Log(2000 * inverseWarp) / ln10))

            ' combined exponent applied to the warp factor to yield the multiple of light speed
            Dim exponent As Double = warpFactor ^ (10 / 3 * (1 + (dampingFactor * rippleFactor * asymptoteFactor)))

            Return Math.Round(exponent, 6)
        Else
            Throw New CalculationException($"Warpfactor smaller than {MinWarpFactor} or bigger than {MaxWarpFactor}")
        End If
    End Function
End Module