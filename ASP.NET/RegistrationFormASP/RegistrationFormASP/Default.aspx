<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RegistrationFormASP._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row" aria-labelledby="student">
            <h3 id="student">STUDENTS</h3>
            <p class="lead">Click below to perform following operations.</p>
            <p>
                <a href="NewStudent.aspx" class="btn btn-primary btn-md">Add Student</a>
                <a href="UpdateStudent.aspx" class="btn btn-primary btn-md">Update Student</a>
                <a href="DeleteStudent.aspx" class="btn btn-primary btn-md">Delete Student</a>
            </p>
        </section>
        <section class="row" aria-labelledby="branch">
            <h3 id="branch">BRANCH</h3>
            <p class="lead">Click below to perform following operations.</p>
            <p>
                <a href="NewBranch.aspx" class="btn btn-primary btn-md">Add Branch</a>
                <a href="UpdateBranch.aspx" class="btn btn-primary btn-md">Update Branch</a>
                <a href="DeleteBranch.aspx" class="btn btn-primary btn-md">Delete Branch</a>
            </p>
        </section> 
        <section class="row" aria-labelledby="enrollment">
            <h3 id="enrollment">ENROLLMENTS</h3>
            <p class="lead">Click below to perform following operations.</p>
            <p>
                <a href="NewEnrollment.aspx" class="btn btn-primary btn-md">Add Enrollment</a>
                <a href="UpdateEnrollment.aspx" class="btn btn-primary btn-md">Update Enrollment</a>
                <a href="DeleteEnrollment.aspx" class="btn btn-primary btn-md">Delete Enrollment</a>
            </p>
        </section>

    </main>

</asp:Content>
