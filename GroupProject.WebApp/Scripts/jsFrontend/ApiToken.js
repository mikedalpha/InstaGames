    $("#subBtn").click(function() {
        let userName = $("#UserName").val();
        let password = $("#Password").val();


        $.ajax({
            type: "POST",
            url: "https://localhost:44369/token",
            "headers": {
                "Content-Type": "application/x-www-form-urlencoded"
            },
            data: {
                "username": `${userName}`,
                "password": `${password}`,
                "grant_type": "password"
            },
            success: function(response) {
                localStorage.setItem('User-Token', response.access_token);
            }
        });
    });
