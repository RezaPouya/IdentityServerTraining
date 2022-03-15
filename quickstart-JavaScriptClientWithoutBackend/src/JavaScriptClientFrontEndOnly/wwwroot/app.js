function log() {
    document.getElementById("results").innerText = "";
  
    Array.prototype.forEach.call(arguments, function (msg) {
      if (typeof msg !== "undefined") {
        if (msg instanceof Error) {
          msg = "Error: " + msg.message;
        } else if (typeof msg !== "string") {
          msg = JSON.stringify(msg, null, 2);
        }
        document.getElementById("results").innerText += msg + "\r\n";
      }
    });
  }

  document.getElementById("login").addEventListener("click", login, false);
document.getElementById("api").addEventListener("click", api, false);
document.getElementById("logout").addEventListener("click", logout, false);


var config = {
    authority: "https://localhost:6001",
    client_id: "Javascript_App",
    redirect_uri: "https://localhost:10002/callback.html",
    response_type: "code",
    scope: "openid profile OrderService",
    post_logout_redirect_uri: "https://localhost:10002/index.html",
  };
  var mgr = new Oidc.UserManager(config);


  mgr.events.addUserSignedOut(function () {
    log("User signed out of IdentityServer");
  });
  
  mgr.getUser().then(function (user) {
    if (user) {
      log("User logged in", user.profile);
    } else {
      log("User not logged in");
    }
  });

  function login() {
    mgr.signinRedirect();
  }
  
  function api() {
    mgr.getUser().then(function (user) {
        var url = "https://localhost:7003/identity";
  
      var xhr = new XMLHttpRequest();
      xhr.open("GET", url);
      xhr.onload = function () {
        log(xhr.status, JSON.parse(xhr.responseText));
      };
      xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
      xhr.send();
    });
  }
  
  function logout() {
    mgr.signoutRedirect();
  }
  