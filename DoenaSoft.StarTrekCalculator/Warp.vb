''' <summary>
''' This class calculates the Star Trek: The Next Generation / Deep Space Nine / Voyager warp scale.
''' The official numbers can be seen in this chart from The Official Star Trek Fact Files: https://i.stack.imgur.com/ZBsFO.gif
''' </summary>
Public Module Warp
    ''' <summary />
    Public Const MinWarpFactor As Double = 1

    ''' <summary />
    Public Const MaxWarpFactor As Double = 9.999999

    ''' <summary />
    Public Const MinLightSpeed As Double = 1

    ''' <summary />
    Public Const MaxLightSpeed As Double = 500000

    ''' <summary />
    Friend Const OneYearInDays As Double = 365.2425

    ''' <summary>
    ''' Converts a Warp factor into a multiple of c (light speed)
    ''' </summary>
    ''' <param name="warpFactor">the Warp factor</param>
    ''' <returns>the multiple of light speed</returns>
    Public Function WarpToLightSpeed(ByVal warpFactor As Double) As Double
        Return WarpToLightspeed(warpFactor, False)
    End Function

    ''' <summary>
    ''' Converts a multiple of c (light speed) to a Warp factor
    ''' </summary>
    ''' <param name="lightSpeed">the multiple of light speed</param>
    ''' <returns>the Warp factor</returns>
    Public Function LightSpeedToWarp(ByVal lightSpeed As Double) As Double
        If lightSpeed >= MinLightSpeed AndAlso lightSpeed <= MaxLightSpeed Then
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

            Dim warp As Double = 1

            Debug.WriteLine("Starting Newton")

            For counter As Integer = 1 To 100
                Debug.WriteLine("Iteration: " & counter.ToString())
                Debug.WriteLine("Min / Max Warp: " & minWarp.ToString() & " / " & maxWarp.ToString())

                warp = (minWarp + maxWarp) / 2

                Debug.WriteLine("Warp: " & warp.ToString())

                Dim calculatedLightspeed As Double = WarpToLightspeed(warp, True)

                Debug.WriteLine("Light speed: " & calculatedLightspeed.ToString())

                If Math.Round(lightSpeed, 6) = calculatedLightspeed Then
                    Exit For
                ElseIf calculatedLightspeed < lightSpeed Then
                    minWarp = warp
                ElseIf calculatedLightspeed > lightSpeed Then
                    maxWarp = warp
                End If
            Next counter

            Return Math.Round(warp, 6)
        Else
            Throw New CalculationException("Lightspeed is smaller than " & MinLightSpeed.ToString() & " or bigger than " & MaxLightSpeed.ToString())
        End If
    End Function

    ''' <summary>
    ''' Calculates the travel time based upon the given factor of light speed and distance in light years
    ''' </summary>
    ''' <param name="lightSpeed">the multiple of light speed</param>
    ''' <param name="lightYears">the distance in light years (9,460,730,472,580,800 km)</param>
    ''' <returns>the travel time</returns>
    ''' <remarks>
    ''' An average year is calculated with 365.2425 days.
    ''' This is based on the leap year logic with results in 97 leap days in 400 years.
    ''' </remarks>
    Public Function LightSpeedToTime(ByVal lightSpeed As Double, ByVal lightYears As Double) As TravelTime
        If lightSpeed >= MinLightSpeed Then
            If lightYears > 0 Then
                Dim temp As Double = lightYears / lightSpeed

                Dim years As Long = Convert.ToInt64(Truncate(temp))

                temp -= years

                Dim days As Short = Convert.ToInt16(Truncate(temp * OneYearInDays))

                temp = (temp * OneYearInDays) - days

                Dim hours As Short = Convert.ToInt16(Truncate(temp * 24))

                temp = (temp * 24) - hours

                Dim minutes As Short = Convert.ToInt16(Truncate(temp * 60))

                temp = (temp * 60) - minutes

                Dim seconds As Short = Convert.ToInt16(Truncate(temp * 60))

                Return New TravelTime(years, days, hours, minutes, seconds)
            Else
                Throw New CalculationException("Distance is lower than 0.")
            End If
        Else
            Throw New CalculationException("Lightspeed is lower than " & MinLightSpeed.ToString())
        End If
    End Function

    ''' <summary>
    ''' Calculates the travel time based upon the given Wap factor and distance in light years
    ''' </summary>
    ''' <param name="warpFactor">the multiple of light speed</param>
    ''' <param name="lightYears">the distance in light years (9,460,730,472,580,800 km)</param>
    ''' <returns>the travel time</returns>
    ''' <remarks>An average year is calculated with 365.2425 days.
    ''' This is based on the leap year logic with results in 97 leap days in 400 years.</remarks>
    Public Function WarpToTime(ByVal warpFactor As Double, ByVal lightYears As Double) As TravelTime
        Dim lightspeed As Double = WarpToLightSpeed(warpFactor)

        Dim travelTime As TravelTime = LightSpeedToTime(lightspeed, lightYears)

        Return travelTime
    End Function

    Private Function WarpToLightspeed(ByVal warpFactor As Double, ByVal internalCall As Boolean) As Double
        If internalCall OrElse (warpFactor >= MinWarpFactor AndAlso warpFactor <= MaxWarpFactor) Then
            Dim ln10 As Double = Math.Log(10)

            Dim inverseWarp As Double = 10 - warpFactor

            Dim a As Double = 0.20467 * Math.Exp(-0.0058 * ((Math.Log(10000 * inverseWarp) / ln10) ^ 5))

            Dim b As Double = 1 + (2 * Math.Cos(10 * Math.PI * Math.Log(8 / (10 * inverseWarp)) / ln10) - 1) / 3 * Math.Exp(-49.369 * ((Math.Log(8 / (10 * inverseWarp)) / ln10) ^ 4))

            Dim c As Double = 1 + 1.88269 / Math.PI * (Math.PI / 2 - Math.Atan((10 ^ warpFactor) * Math.Log(2000 * inverseWarp) / ln10))

            Dim d As Double = warpFactor ^ (10 / 3 * (1 + (a * b * c)))

            Return Math.Round(d, 6)
        Else
            Throw New CalculationException("Warpfactor smaller than " & MinWarpFactor.ToString() & " or bigger than" & MaxWarpFactor.ToString())
        End If
    End Function
End Module