const OnlyDomainURL = "https://localhost:44384/";
const APIURL = "https://localhost:44384/api/TeacherData/";

function AddTeacher() {

	//goal: send a request which looks like this:
	//POST : https://localhost:44384/api/TeacherData/AddTeacher
	//with POST data of authorname, bio, email, etc.

	var URL = APIURL + "AddTeacher/";
	var OutputURL = OnlyDomainURL + "Teacher/Details/";

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var TeacherFName = document.getElementById('TeacherFName').value;
	var TeacherLName = document.getElementById('TeacherLName').value;
	var EmployeeNumber = document.getElementById('EmployeeNumber').value;
	var Salary = document.getElementById('Salary').value;

	if (!(TeacherFName && TeacherLName && EmployeeNumber && Salary)) {
		return;
    }


	var TeacherData = {
		"TeacherFName": TeacherFName,
		"TeacherLName": TeacherLName,
		"EmployeeNumber": EmployeeNumber,
		"Salary": Salary
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
	rq.send(JSON.stringify(TeacherData));

}

function DeleteTeacher(TeacherId) {
	debugger;
	var URL = APIURL + "DeleteTeacher/" + TeacherId;
	var OutputURL = OnlyDomainURL + "Teacher/";

	var rq = new XMLHttpRequest();

	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.
			console.log(rq);
			window.location.href = OutputURL;
		}

	}
	//POST information sent through the .send() method
	rq.send();
}