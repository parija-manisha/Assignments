
    function enrollmentBranchTextInput() {
        var enrollmentBranchText = $('#EnrollmentBranchText');
        var enrollmentBranchNameText = $('#EnrollmentBranchNameText');
        var errorMessage = $('#ErrorMessage');

        if (parseInt(enrollmentBranchText.val(), 10)) {
            Connection.connect(function (con) {
                con.open();
                var branchId = parseInt(enrollmentBranchText.val(), 10);
                var retrieveBranchQuery = "SELECT [Branch Name] FROM Branch WHERE [Branch ID] = @branchId";

                var retrieveBranchCmd = con.prepare(retrieveBranchQuery);
                retrieveBranchCmd.bind({ "@branchId": branchId });

                var result = retrieveBranchCmd.executeScalar();

                if (result !== null) {
                    var branchName = result.toString();
                    enrollmentBranchNameText.val(branchName);
                    enrollmentBranchNameText.prop('disabled', true);
                } else {
                    errorMessage.html("Branch not found for the provided BranchID.");
                    clearForm();
                }
            });
        } else {
            errorMessage.html("Invalid Branch ID.");
            clearForm();
        }
    }

    function clearForm() {
        var enrollmentBranchText = $('#EnrollmentBranchText');
        var enrollmentBranchNameText = $('#EnrollmentBranchNameText');
        enrollmentBranchText.val('');
        enrollmentBranchNameText.val('');
        enrollmentBranchNameText.prop('disabled', false);
    }
