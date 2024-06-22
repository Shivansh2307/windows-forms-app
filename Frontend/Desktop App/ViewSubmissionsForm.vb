Imports System.Net
Imports System.Web.Script.Serialization

Public Class ViewSubmissionsForm

    Private submissions As New List(Of Submission)()
    Private currentSubmissionIndex As Integer = 0

    Private Sub ViewSubmissionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FetchSubmissions()
        DisplaySubmission()
    End Sub

    Private Sub FetchSubmissions()
        Try
            Dim client As New WebClient()
            Dim index As Integer = 0 ' Start fetching from the first submission
            Dim jsonResponse As String = client.DownloadString($"http://localhost:3000/read?index={index}")
            Dim serializer As New JavaScriptSerializer()
            Dim response As Dictionary(Of String, Object) = serializer.Deserialize(Of Dictionary(Of String, Object))(jsonResponse)

            If response.ContainsKey("success") AndAlso response("success") Then
                Dim submission As Dictionary(Of String, Object) = CType(response("submission"), Dictionary(Of String, Object))
                Dim submissionDetails As New Submission With {
                .Name = submission("name").ToString(),
                .Email = submission("email").ToString(),
                .Phone = submission("phone").ToString(),
                .GitHubLink = submission("github_link").ToString(),
                .StopwatchTime = submission("stopwatch_time").ToString()
            }
                submissions.Add(submissionDetails)
            Else
                MessageBox.Show("Failed to fetch submissions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show($"Error fetching submissions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub DisplaySubmission()
        If submissions.Count > 0 AndAlso currentSubmissionIndex < submissions.Count Then
            Dim submission As Submission = submissions(currentSubmissionIndex)
            lblSubmissionDetails.Text = $"Name: {submission.Name}, Email: {submission.Email}, Phone: {submission.Phone}, GitHub: {submission.GitHubLink}, Time: {submission.StopwatchTime}"
        Else
            lblSubmissionDetails.Text = "No submissions found."
        End If
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If currentSubmissionIndex > 0 Then
            currentSubmissionIndex -= 1
            DisplaySubmission()
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If currentSubmissionIndex < submissions.Count - 1 Then
            currentSubmissionIndex += 1
            DisplaySubmission()
        End If
    End Sub

End Class

Public Class Submission
    Public Property Name As String
    Public Property Email As String
    Public Property Phone As String
    Public Property GitHubLink As String
    Public Property StopwatchTime As String
End Class


