Imports System.Net
Imports System.Web.Script.Serialization


Public Class CreateSubmissionForm
    Private stopwatchRunning As Boolean = False
    Private stopwatchTime As TimeSpan = TimeSpan.Zero

    Private Sub CreateSubmissionForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.T Then
            btnToggleStopwatch.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.S Then
            btnSubmit.PerformClick()
        End If
    End Sub

    Private Sub btnToggleStopwatch_Click(sender As Object, e As EventArgs) Handles btnToggleStopwatch.Click
        stopwatchRunning = Not stopwatchRunning
        Timer1.Enabled = stopwatchRunning
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If stopwatchRunning Then
            stopwatchTime = stopwatchTime.Add(TimeSpan.FromSeconds(1))
            lblStopwatch.Text = stopwatchTime.ToString("hh\:mm\:ss")
        End If
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim name As String = txtName.Text
        Dim email As String = txtEmail.Text
        Dim phone As String = txtPhone.Text
        Dim github As String = txtGithub.Text

        Dim submission As New Dictionary(Of String, String) From {
        {"name", name},
        {"email", email},
        {"phone", phone},
        {"github_link", github},
        {"stopwatch_time", lblStopwatch.Text}
    }

        Dim json As String = New JavaScriptSerializer().Serialize(submission)
        Dim client As New WebClient()
        client.Headers(HttpRequestHeader.ContentType) = "application/json"
        client.UploadString("http://localhost:3000/submit", "POST", json)

        MessageBox.Show("Submission successful!")
        Me.Close()
    End Sub
End Class
