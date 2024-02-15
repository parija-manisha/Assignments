using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            userInput.Text = UserName.Text;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label3.Text = "You Clicked the Button.";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            genderId.Text = "";
            if (male.Checked)
            {
                genderId.Text = "Your gender is " + male.Text;
            }
            else
            {
                genderId.Text = "Your gender is " + female.Text;
            }
        }

        protected void SubmitButton3_Click(object sender, EventArgs e)
        {
            string selectedValue = RadioButtonList1.SelectedValue;

            ResultLabel.Text = "Selected Option: " + selectedValue;
        }

        protected void Calender_selectionChanged(object sender, EventArgs e)
        {
            ShowDate.Text = "You Selected " + Calender.SelectedDate.ToString("MMM");
        }

        protected void checkBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckboxInput.Text = "Selected Items:";
            for (int i = 0; i < checkBox.Items.Count; i++)
            {
                if (checkBox.Items[i].Selected)
                { CheckboxInput.Text += checkBox.Items[i].Text + "<br />"; }
            }
        }

        protected void LinkButtonInput_Click(object sender, EventArgs e)
        {
            LinkButtonInput.Text = "Welcome";
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if ((Request.Files.Count > 0) && (Request.Files[0].ContentLength > 0))
            {
                string fn = System.IO.Path.GetFileName(FileUpload2.PostedFile.FileName);
                string SaveLocation = Server.MapPath("upload") + "\\" + fn;
                try
                {
                    FileUpload2.PostedFile.SaveAs(SaveLocation);
                    FileUploadStatus.Text = "The file has been uploaded.";

                    // RegisterStartupScript to call openFileInNewWindow after upload
                    string script = $"<script type='text/javascript'>openFileInNewWindow('FileUploadHandler.ashx');</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "openFileScript", script);
                }
                catch (Exception ex)
                {
                    FileUploadStatus.Text = "Error: " + ex.Message;
                }
            }
            else
            {
                FileUploadStatus.Text = "Please select a file to upload.";
            }
        }


        protected void Button5_Click(object sender, EventArgs e)
        {
            //String filepath = "C:\\Users\\manishap\\Pictures\\Screenshots\\Screenshot (3).png";
            String filepath = "~/downloadItems/Screenshot (3).png";
            FileInfo file = new FileInfo(Server.MapPath(filepath));
            if (file.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-length", file.Length.ToString());
                Response.ContentType = "image/png";
                Response.Flush();
                Response.TransmitFile(file.FullName);
                Response.End();
            }
            else
                Label4.Text = "File couldnot be found";
        }

        protected void login_Click(object sender, EventArgs e)
        {
            if (password.Text == "qwe123")
            {
                // Storing email to Session variable  
                Session["email"] = email.Text;
            }
            // Checking Session variable is not empty  
            if (Session["email"] != null)
            {
                // Displaying stored email  
                Label5.Text = "This email is stored to the session.";
                Label6.Text = Session["email"].ToString();
            }
        }
    }
}