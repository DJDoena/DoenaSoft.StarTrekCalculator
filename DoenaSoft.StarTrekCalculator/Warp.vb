''' <summary>
''' Warp scale see official chart https://i.stack.imgur.com/ZBsFO.gif
''' </summary>
Public Module Warp
    Public Const MinWarp As Double = 1

    Public Const MaxWarp As Double = 9.999999

    Public Const MinLightspeed As Double = 1

    Public Const MaxLightspeed As Double = 500000

    Public Const OneYearInDays As Double = 365.2425

    Public Function WarpToLightspeed(ByVal warp As Double) As Double
        Return WarpToLightspeed(warp, False)
    End Function

    Public Function LightspeedToWarp(ByVal lightspeed As Double) As Double
        If lightspeed >= MinLightspeed AndAlso lightspeed <= MaxLightspeed Then
            Dim minWarp As Double = 1

            Dim maxWarp As Double = 10

            If lightspeed >= 502440 Then
                minWarp = 9.999999
            ElseIf lightspeed >= 204851 Then
                minWarp = 9.99999
            ElseIf lightspeed >= 199516 Then
                minWarp = 9.9999
                maxWarp = 9.999999
            ElseIf lightspeed >= 10268 Then
                minWarp = 9.999
                maxWarp = 9.99999
            ElseIf lightspeed >= 7913 Then
                minWarp = 9.99
                maxWarp = 9.9999
            ElseIf lightspeed >= 3053 Then
                minWarp = 9.9
                maxWarp = 9.999
            ElseIf lightspeed >= 1517 Then
                minWarp = 9
                maxWarp = 9.99
            ElseIf lightspeed >= 1025 Then
                minWarp = 8
                maxWarp = 9.9
            ElseIf lightspeed >= 1025 Then
                minWarp = 7
                maxWarp = 9
            ElseIf lightspeed >= 657 Then
                minWarp = 6
                maxWarp = 8
            ElseIf lightspeed >= 393 Then
                minWarp = 5
                maxWarp = 7
            ElseIf lightspeed >= 102 Then
                minWarp = 4
                maxWarp = 6
            ElseIf lightspeed >= 39 Then
                minWarp = 3
                maxWarp = 5
            ElseIf lightspeed >= 11 Then
                minWarp = 2
                maxWarp = 4
            ElseIf lightspeed > 1 Then
                maxWarp = 3
            Else
                maxWarp = 1
            End If

            Dim warp As Double = 1

            Debug.WriteLine("Starting Newton")

            For counter As Integer = 1 To 100
                Debug.WriteLine("Iteration: " & counter)
                Debug.WriteLine("Min / Max Warp: " & minWarp & " / " & maxWarp)

                warp = (minWarp + maxWarp) / 2

                Debug.WriteLine("Warp: " & warp)

                Dim calculatedLightspeed As Double = WarpToLightspeed(warp, True)

                Debug.WriteLine("Light speed: " & calculatedLightspeed)

                If Math.Round(lightspeed, 6) = calculatedLightspeed Then
                    Exit For
                ElseIf calculatedLightspeed < lightspeed Then
                    minWarp = warp
                ElseIf calculatedLightspeed > lightspeed Then
                    maxWarp = warp
                End If
            Next counter

            Return Math.Round(warp, 6)
        Else
            Throw New CalculationException("Lightspeed is smaller than " & MinLightspeed & " or bigger than " & MaxLightspeed)
        End If
    End Function

    Public Function WarpToTime(ByVal warp As Double, ByVal distance As Double) As TravelTime
        Dim lightspeed As Double = WarpToLightspeed(warp)

        Dim travelTime As TravelTime = LightspeedToTime(lightspeed, distance)

        Return travelTime
    End Function

    Public Function LightspeedToTime(ByVal lightspeed As Double, ByVal distance As Double) As TravelTime
        If lightspeed >= MinLightspeed Then
            If distance > 0 Then
                Dim years As Long = Convert.ToInt64(Fix(distance / lightspeed))

                Dim temp As Double = (distance / lightspeed) - years

                Dim days As Short = Convert.ToInt16(Int(temp * OneYearInDays))

                temp = (temp * OneYearInDays) - days

                Dim hours As Short = Convert.ToInt16(Int(temp * 24))

                temp = (temp * 24) - hours

                Dim minutes As Short = Convert.ToInt16(Int(temp * 60))

                temp = (temp * 60) - minutes

                Dim seconds As Short = Convert.ToInt16(Int(temp * 60))

                Return New TravelTime() With {
                    .Years = years,
                    .Days = days,
                    .Hours = hours,
                    .Minutes = minutes,
                    .Seconds = seconds
                }
            Else
                Throw New CalculationException("Distance is lower than " & MinLightspeed)
            End If
        Else
            Throw New CalculationException("Lightspeed is lower than 0.")
        End If
    End Function

    Private Function WarpToLightspeed(ByVal warp As Double, ByVal internalCall As Boolean) As Double
        If internalCall OrElse warp >= MinWarp AndAlso warp <= MaxWarp Then
            Dim ln10 As Double = Math.Log(10)

            Dim inverseWarp As Double = 10 - warp

            Dim a As Double = 0.20467 * Math.Exp(-0.0058 * ((Math.Log(10000 * inverseWarp) / ln10) ^ 5))

            Dim b As Double = 1 + (2 * Math.Cos(10 * Math.PI * Math.Log(8 / (10 * inverseWarp)) / ln10) - 1) / 3 * Math.Exp(-49.369 * ((Math.Log(8 / (10 * inverseWarp)) / ln10) ^ 4))

            Dim c As Double = 1 + 1.88269 / Math.PI * (Math.PI / 2 - Math.Atan((10 ^ warp) * Math.Log(2000 * inverseWarp) / ln10))

            Dim d As Double = warp ^ (10 / 3 * (1 + (a * b * c)))

            Return Math.Round(d, 6)
        Else
            Throw New CalculationException("Warpfactor smaller than " & MinWarp & " or bigger than" & MaxWarp)
        End If
    End Function
End Module