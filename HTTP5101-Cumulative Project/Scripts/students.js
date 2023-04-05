function AddStudent() {

	//goal: send a request which looks like this:
	//POST : https://localhost:44384/api/StudentData/AddStudent
	//with POST data of authorname, bio, email, etc.

	var URL = "https://localhost:44384/api/StudentData/AddStudent/";
	var OutputURL = "https://localhost:44384/Student/Details/";

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var StudentFName = document.getElementById('StudentFName').value;
	var StudentLName = document.getElementById('StudentLName').value;
	var StudentNumber = document.getElementById('StudentNumber').value;

	if (!(StudentFName && StudentLName && StudentNumber)) {
		return;
	}

	var StudentData = {
		"StudentFName": StudentFName,
		"StudentLName": StudentLName,
		"StudentNumber": StudentNumber
	};

	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.

			window.location.href = OutputURL + rq.responseText;
		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(StudentData));

}