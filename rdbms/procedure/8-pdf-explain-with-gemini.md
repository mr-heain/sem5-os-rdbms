Got it, bro. If you're just going to practice this directly on the college lab PCs using Microsoft Access, we can skip all the complicated Visual Basic 6.0 connection stuff. MS Access has its own forms and coding language (VBA) that looks and acts exactly like VB6, but you don't need any ADODB connection strings to make it work.

Here is the fastest, start-to-finish process to build **Program 8 (Library Management System)** entirely inside MS Access.

### Phase 1: Set Up the Database & Table

1. Open MS Access and click **Blank Database**. (You can name it `lib.mdb` or just let it use the default).
2. On the top menu, click **Create** $\rightarrow$ **Table Design**.
3. Create your fields exactly as your manual requires:


* **Field Name:** `bookid` | **Data Type:** Number (Set this as Primary Key)
* **Field Name:** `bookname` | **Data Type:** Short Text
* **Field Name:** `author` | **Data Type:** Short Text
* **Field Name:** `quan` | **Data Type:** Number


4. Save the table and name it `book`. Close the table tab.



### Phase 2: Create the Main Menu (Form 1)

1. Go to **Create** $\rightarrow$ **Form Design**.
2. From the Form Design toolkit at the top, draw 4 **Buttons** on the screen.
3. Name them: *Add Book*, *Issue Book*, *Return Book*, and *View Report*.


4. Right-click the form tab, click **Save**, and name it `Form1`.



### Phase 3: Create the Add Book Screen (Form 2)

1. Go to **Create** $\rightarrow$ **Form Design**.
2. Draw 4 **Text Boxes** on the screen. By default, Access names them `Text0`, `Text2`, etc. Click on each one, go to the **Property Sheet** (right side), and change their names to `Text1`, `Text2`, `Text3`, and `Text4` to match your manual.


3. Draw an **ADD** button and a **CLOSE** button.


4. Right-click the **ADD** button $\rightarrow$ **Build Event** $\rightarrow$ **Code Builder**. Paste this exact code (it does the same thing as your manual but uses native Access VBA):

```vb
Private Sub cmdAdd_Click()
    ' Inserts the exact textbox values into the book table
    CurrentDb.Execute "INSERT INTO book (bookid, bookname, author, quan) VALUES (" & Me.Text1 & ", '" & Me.Text2 & "', '" & Me.Text3 & "', " & Me.Text4 & ")"
    MsgBox "Successfully Inserted"
    
    ' Clear the boxes
    Me.Text1 = ""
    Me.Text2 = ""
    Me.Text3 = ""
    Me.Text4 = ""
End Sub

```

5. For the **CLOSE** button, right-click $\rightarrow$ Build Event $\rightarrow$ Code Builder:

```vb
Private Sub cmdClose_Click()
    DoCmd.Close
End Sub

```

6. Save this form as `Form2`.



### Phase 4: Create the Issue & Return Screens (Form 3 & 4)

Because Access is smart, you don't even need to build the complicated ADODB recordset logic from your manual. You just need a button that asks for the ID and runs an update query.

**For Form 3 (Issue Book):**

1. Create a blank form, draw a button named **ISSUE**.


2. Right-click $\rightarrow$ Build Event $\rightarrow$ Code Builder and paste:

```vb
Private Sub cmdIssue_Click()
    Dim bid As String
    bid = InputBox("Enter the Book ID :") 
    
    If bid <> "" Then
        ' Decrements the quantity by 1 based on the ID entered
        CurrentDb.Execute "UPDATE book SET quan = quan - 1 WHERE bookid = " & bid
        MsgBox "Book Issued"
    End If
End Sub

```

3. Save as `Form3`.



**For Form 4 (Return Book):**

1. Create a blank form, draw a button named **RETURN**.


2. Right-click $\rightarrow$ Build Event $\rightarrow$ Code Builder and paste:

```vb
Private Sub cmdReturn_Click()
    Dim bid As String
    bid = InputBox("Enter the Book ID to return book :")
    
    If bid <> "" Then
        ' Increments the quantity by 1 based on the ID entered
        CurrentDb.Execute "UPDATE book SET quan = quan + 1 WHERE bookid = " & bid
        MsgBox "Book Returned"
    End If
End Sub

```

3. Save as `Form4`.



### Phase 5: The Report

1. In the left navigation pane, click once on your `book` table to highlight it.
2. Go to the top menu and click **Create** $\rightarrow$ **Report**.
3. MS Access instantly generates a beautiful, printable report. Save it as `DataReport1`.



### Final Step: Link Form 1 to Everything

Go back to `Form1` in Design View. Right-click each of your main menu buttons, go to Code Builder, and link them up like this:

```vb
Private Sub btnAdd_Click()
    DoCmd.OpenForm "Form2"
End Sub

Private Sub btnIssue_Click()
    DoCmd.OpenForm "Form3"
End Sub

Private Sub btnReturn_Click()
    DoCmd.OpenForm "Form4"
End Sub

Private Sub btnReport_Click()
    DoCmd.OpenReport "DataReport1", acViewReport
End Sub

```

That’s it! No messy connection strings, no VisData add-in, and it fulfills all the exact logic your examiner is looking for. When is your practical exam scheduled?
