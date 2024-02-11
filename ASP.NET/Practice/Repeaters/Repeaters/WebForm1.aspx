<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Repeaters.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Repeater ID="Repeater1" runat="server">  
            <ItemTemplate>  
                <div>  
                    <table>  
                        <tr>  
                            <th>Student<%#Eval("S_ID")%></th>  
                        </tr>  
                        <tr>  
                            <td>Student Name</td>  
                            <td><%#Eval("Student_Name")%></td>  
                        </tr>  
                        <tr>  
                            <td>Registration Number</td>  
                            <td><%#Eval("Register_No")%></td>  
                        </tr>  
                        <tr>  
                            <td>Date Of Birth</td>  
                            <td><%#Eval("D_O_B")%></td>  
                        </tr>  
                        <tr>  
                            <td>Date Of Examination</td>  
                            <td><%#Eval("D_O_E")%></td>  
                        </tr>  
                        <tr>  
                            <td>Department</td>  
                            <td><%#Eval("Department")%></td>  
                        </tr>  
                    </table>  
                </div>  
            </ItemTemplate>  
        </asp:Repeater>  
        </div>
    </form>
</body>
</html>
