Imports System.Diagnostics.Eventing
Imports System.Data.Common
Imports Excel = Microsoft.Office.Interop.Excel
Imports MySql.Data.MySqlClient
Public Class ViewOrders
    Private Sub Load_Data_to_Grid(ByVal strsql As String)
        Dim myreader As MySqlDataReader
        Dim mycommand As New MySqlCommand
        Dim mydataAdapter As New MySqlDataAdapter
        Dim mydatatable As New DataTable

        Connect_to_DB()
        With Me
            Try
                mycommand.Connection = myconn
                mycommand.CommandText = strsql
                myreader = mycommand.ExecuteReader
                mydatatable = New DataTable

                myreader.Close()
                mydataAdapter.SelectCommand = mycommand

                mydataAdapter.Fill(mydatatable)
                dgreport.AutoSize = False
                .dgreport.Refresh()
                .dgreport.EndEdit()
                .dgreport.DataSource = mydatatable
                .dgreport.ReadOnly = True
                .dgreport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells

            Catch ex As MySqlException
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error on SQL query")
            End Try
            myreader = Nothing
            mycommand = Nothing
            Disconnect_to_DB()
        End With
    End Sub

    Private Sub ViewOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'strSQL = "select " & Me.sqlColumns & " from customers"
        'Me.Label1.Text = strSQL
        Call Load_Data_to_Grid("select * FROM ordering_system.order_details")

        With Me
            'panel logo background color
            plLogo.BackColor = Color.FromArgb(226, 194, 179)

            'label title color
            Label1.ForeColor = Color.White

            'panel title background color
            plTitle.BackColor = Color.FromArgb(166, 112, 91)

            'button Add order
            btnAdd.FlatStyle = FlatStyle.Flat
            btnAdd.FlatAppearance.BorderSize = 0
            btnAdd.Cursor = Cursors.Hand
            roundbutton(btnAdd)

            'button print
            btnPrint.FlatStyle = FlatStyle.Flat
            btnPrint.FlatAppearance.BorderSize = 0
            btnPrint.BackColor = Color.FromArgb(226, 194, 179)
            btnPrint.Cursor = Cursors.Hand
            roundbutton(btnPrint)

            'side-nav
            'button customer list
            btnAdmin.Cursor = Cursors.Hand
            btnAdmin.FlatAppearance.BorderSize = 0
            btnAdmin.FlatStyle = FlatStyle.Flat

            btnCustomers.Cursor = Cursors.Hand
            btnCustomers.FlatAppearance.BorderSize = 0
            btnCustomers.FlatStyle = FlatStyle.Flat

            btnProducts.Cursor = Cursors.Hand
            btnProducts.FlatAppearance.BorderSize = 0
            btnProducts.FlatStyle = FlatStyle.Flat

            btnOrders.Cursor = Cursors.Hand
            btnOrders.FlatAppearance.BorderSize = 0
            btnOrders.FlatStyle = FlatStyle.Flat

            PictureBox5.BackColor = Color.FromArgb(166, 112, 91)
            btnOrderList.FlatStyle = FlatStyle.Flat
            btnOrderList.FlatAppearance.BorderSize = 0
            btnOrderList.BackColor = Color.FromArgb(166, 112, 91)
            btnOrderList.ForeColor = Color.White
            btnOrderList.Cursor = Cursors.Hand

            btnManageOrder.Cursor = Cursors.Hand
            btnManageOrder.FlatAppearance.BorderSize = 0
            btnManageOrder.FlatStyle = FlatStyle.Flat

            btnTransaction.Cursor = Cursors.Hand
            btnTransaction.FlatAppearance.BorderSize = 0
            btnTransaction.FlatStyle = FlatStyle.Flat

            btnSales.Cursor = Cursors.Hand
            btnSales.FlatAppearance.BorderSize = 0
            btnSales.FlatStyle = FlatStyle.Flat

            'round border for Title panel
            Dim Title As Drawing2D.GraphicsPath = New Drawing2D.GraphicsPath()
            Dim cornerRadius As Integer = 10
            Dim rectTitle As Rectangle = plTitle.ClientRectangle
            Title.AddArc(rectTitle.X, rectTitle.Y, cornerRadius * 2, cornerRadius * 2, 180, 90)
            Title.AddArc(rectTitle.X + rectTitle.Width - cornerRadius * 2, rectTitle.Y, cornerRadius * 2, cornerRadius * 2, 270, 90)
            Title.AddArc(rectTitle.X + rectTitle.Width - cornerRadius * 2, rectTitle.Y + rectTitle.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90)
            Title.AddArc(rectTitle.X, rectTitle.Y + rectTitle.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90)
            Title.CloseAllFigures()

            plTitle.Region = New Region(Title)
        End With
    End Sub
    Private Sub roundbutton(btn As Button)
        Dim radius As Integer = 10 ' Set the radius of the rounded corner
        Dim add As Drawing2D.GraphicsPath = New Drawing2D.GraphicsPath()
        add.StartFigure()
        add.AddArc(New Rectangle(0, 0, radius * 2, radius * 2), 180, 90)
        add.AddLine(radius, 0, btnAdd.Width - radius, 0)
        add.AddArc(New Rectangle(btnAdd.Width - radius * 2, 0, radius * 2, radius * 2), -90, 90)
        add.AddLine(btnAdd.Width, radius, btnAdd.Width, btnAdd.Height - radius)
        add.AddArc(New Rectangle(btnAdd.Width - radius * 2, btnAdd.Height - radius * 2, radius * 2, radius * 2), 0, 90)
        add.AddLine(btnAdd.Width - radius, btnAdd.Height, radius, btnAdd.Height)
        add.AddArc(New Rectangle(0, btnAdd.Height - radius * 2, radius * 2, radius * 2), 90, 90)
        add.CloseFigure()
        btnAdd.Region = New Region(add)

        Dim print As Drawing2D.GraphicsPath = New Drawing2D.GraphicsPath()
        print.StartFigure()
        print.AddArc(New Rectangle(0, 0, radius * 2, radius * 2), 180, 90)
        print.AddLine(radius, 0, btnPrint.Width - radius, 0)
        print.AddArc(New Rectangle(btnPrint.Width - radius * 2, 0, radius * 2, radius * 2), -90, 90)
        print.AddLine(btnPrint.Width, radius, btnPrint.Width, btnPrint.Height - radius)
        print.AddArc(New Rectangle(btnPrint.Width - radius * 2, btnPrint.Height - radius * 2, radius * 2, radius * 2), 0, 90)
        print.AddLine(btnPrint.Width - radius, btnPrint.Height, radius, btnPrint.Height)
        print.AddArc(New Rectangle(0, btnPrint.Height - radius * 2, radius * 2, radius * 2), 90, 90)
        print.CloseFigure()
        btnPrint.Region = New Region(print)
    End Sub
    Private Sub btnCustomers_Click(sender As Object, e As EventArgs) Handles btnCustomers.Click
        With Me
            .Hide()
            ViewCustomers.Show()
        End With
    End Sub

    Private Sub btnProducts_Click(sender As Object, e As EventArgs) Handles btnProducts.Click
        With Me
            .Hide()
            ViewProducts.Show()
        End With
    End Sub

    Private Sub btnSales_Click(sender As Object, e As EventArgs) Handles btnSales.Click
        With Me
            .Hide()
            ViewSales.Show()
        End With
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        With Me
            .Hide()
            ManageOrders.Show()
        End With
    End Sub

    Private Sub btnManageOrder_Click(sender As Object, e As EventArgs) Handles btnManageOrder.Click
        With Me
            .Hide()
            ManageOrders.Show()
        End With
    End Sub

    Private Sub btnTransaction_Click(sender As Object, e As EventArgs) Handles btnTransaction.Click
        With Me
            .Hide()
            ViewTransaction.Show()
        End With
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        'MsgBox(currentDate.ToString)
        Call importToExcel(Me.dgreport, "order-report.xlsx")
    End Sub
End Class