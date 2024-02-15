<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" Theme="Skin1" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            margin-left: 0px;
        }

        .auto-style3 {
            width: 121px;
        }
    </style>
</head>
<body>
    <form action="Handler1.ashx" id="form1" runat="server">
        <div>
            <h2>Heading</h2>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label1" runat="server" Text="User Name"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="auto-style2"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label2" runat="server" Text="Upload a File"></asp:Label></td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" /></td>
                </tr>
            </table>
        </div>
        <br />
        <div>
            <div>
                <asp:Label ID="labelId" runat="server" Font-Bold="true">User Name</asp:Label>
                <asp:TextBox ID="UserName" runat="server" ToolTip="Enter User Name" BackColor="#ffccff"></asp:TextBox>
            </div>
            <p>
                <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClick="SubmitButton_Click" />
            </p>
            <asp:Label class="user" ID="userInput" runat="server"></asp:Label>
        </div>
        <br />
        <div>
            <asp:TextBox ID="txtTextBox" runat="server"></asp:TextBox>
        </div>
        <br />
        <div>
            <asp:Button ID="Button1" runat="server" Text="Click herep" OnClick="Button1_Click" />
            <br />
            <asp:Label ID="Label3" runat="server">kkkkkkk</asp:Label>
        </div>
        <br />
        <div>
            <asp:HyperLink ID="hyperlink" runat="server" Text="ASP.NET" NavigateUrl="~/Default.aspx"></asp:HyperLink>
        </div>
        <br />
        <div>
            <asp:RadioButton ID="male" runat="server" Text="Male" GroupName="Gender" />
            <asp:RadioButton ID="female" runat="server" Text="Female" GroupName="Gender" />
            <br />
            <asp:Button ID="Button2" runat="server" Text="Submit" OnClick="Button2_Click" Style="width: 61px" />
            <asp:Label runat="server" ID="genderId"></asp:Label>
        </div>
        <br />
        <div>
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Option 1" Value="1" />
                <asp:ListItem Text="Option 2" Value="2" />
                <asp:ListItem Text="Option 3" Value="3" />
            </asp:RadioButtonList>

            <asp:Button ID="Button3" runat="server" Text="Submit" OnClick="SubmitButton3_Click" />

            <asp:Label ID="ResultLabel" runat="server" Text=""></asp:Label>
        </div>
        <br />
        <div>
            <asp:Calendar ID="Calender" runat="server" Caption="Calender" CaptionAlign="Bottom" OnSelectionChanged="Calender_selectionChanged"></asp:Calendar>
            <asp:Label ID="ShowDate" runat="server"></asp:Label>
        </div>
        <br />
        <div>
            <asp:CheckBoxList ID="checkBox" runat="server" AutoPostBack="true" OnSelectedIndexChanged="checkBox_SelectedIndexChanged" RepeatDirection="Horizontal">
                <asp:ListItem>Item1</asp:ListItem>
                <asp:ListItem>Item2</asp:ListItem>
                <asp:ListItem>Item3</asp:ListItem>
                <asp:ListItem>Item4</asp:ListItem>
            </asp:CheckBoxList>

            <asp:Label ID="CheckboxInput" runat="server"></asp:Label>
        </div>
        <br />
        <div>
            <asp:LinkButton ID="LinkButton" runat="server" OnClick="LinkButtonInput_Click">Helllo</asp:LinkButton>
            <asp:Label ID="LinkButtonInput" runat="server"></asp:Label>
            <asp:Label ID="Labell" runat="server" SkinID="lbltxt" Text="Label"></asp:Label>
        </div>
        <br />
        <div>
            <asp:FileUpload ID="FileUpload2" runat="server" />
            <asp:Button ID="Button4" runat="server" Text="Upload File" OnClick="Button4_Click" />
            
            <asp:Label runat="server" ID="FileUploadStatus"></asp:Label>
        </div>
        <br />
        <div>
            <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Download" />
            <asp:Label ID="Label4" runat="server"></asp:Label>
        </div>
        <br />
        <div>
            <asp:TextBox ID="email" runat="server" TextMode="Email"></asp:TextBox>
            <asp:Button ID="login" runat="server" Text="Login" OnClick="login_Click" />
            <asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox>

            <asp:Label ID="Label5" runat="server"></asp:Label>
            <br />
            <asp:Label ID="Label6" runat="server"></asp:Label>
        </div>
        <div>

            <asp:DataList runat="server" DataSourceID="SqlDataSource1" ID="ctl07" DataKeyField="StudentID">
                <ItemTemplate>
                    StudentID:
        <asp:Label Text='<%# Eval("StudentID") %>' runat="server" ID="StudentIDLabel" /><br />
                    StudentName:
        <asp:Label Text='<%# Eval("StudentName") %>' runat="server" ID="StudentNameLabel" /><br />
                    Gender:
        <asp:Label Text='<%# Eval("Gender") %>' runat="server" ID="GenderLabel" /><br />
                    DateOfBirth:
        <asp:Label Text='<%# Eval("DateOfBirth") %>' runat="server" ID="DateOfBirthLabel" /><br />
                    PhoneNumber:
        <asp:Label Text='<%# Eval("PhoneNumber") %>' runat="server" ID="PhoneNumberLabel" /><br />
                    <br />
                </ItemTemplate>
            </asp:DataList><asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString="<%$ ConnectionStrings:ENTITY_FRAMEWORK_ASSIGNMENTConnectionString %>" SelectCommand="SELECT * FROM [Student]"></asp:SqlDataSource>
        </div>
        <div>
            <asp:GridView runat="server" AutoGenerateColumns="False" DataKeyNames="StudentID" DataSourceID="SqlDataSource1" ID="ctl08" AllowSorting="True" AllowPaging="True">
                <Columns>
                    <%--<asp:CommandField ShowSelectButton="True"></asp:CommandField>--%>
                    <asp:BoundField DataField="StudentID" HeaderText="StudentID" ReadOnly="True" InsertVisible="False"></asp:BoundField>
                    <asp:BoundField DataField="StudentName" HeaderText="StudentName" SortExpression="StudentName"></asp:BoundField>
                    <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender"></asp:BoundField>
                    <asp:BoundField DataField="DateOfBirth" HeaderText="DateOfBirth" SortExpression="DateOfBirth"></asp:BoundField>
                    <asp:BoundField DataField="PhoneNumber" HeaderText="PhoneNumber" SortExpression="PhoneNumber"></asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
    <script type="text/javascript">
    function openFileInNewWindow(fileUrl) {
        window.open(fileUrl, '_blank');
    }
    </script>
</body>
</html>

