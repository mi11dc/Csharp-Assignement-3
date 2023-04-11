const OnlyDomainURL = "https://localhost:44384/";
const APIURL = "https://localhost:44384/api/TeacherData/";

function AddTeacher() {

	//POST : https://localhost:44384/api/TeacherData/AddTeacher

	var URL = APIURL + "AddTeacher/";
	var OutputURL = OnlyDomainURL + "Teacher/Details/";

	var rq = new XMLHttpRequest();

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
	//DELETE : https://localhost:44384/api/TeacherData/DeleteTeacher/{TeacherId}

	var URL = APIURL + "DeleteTeacher/" + TeacherId;
	var OutputURL = OnlyDomainURL + "Teacher/";

	var rq = new XMLHttpRequest();

	rq.open("DELETE", URL, true);
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

function UpdateTeacher(TeacherId) {
	debugger;
	//PUT : https://localhost:44384/api/TeacherData/UpdateTeacher/{TeacherId}
	
	var URL = APIURL + "UpdateTeacher/" + TeacherId;
	var OutputURL = OnlyDomainURL + "Teacher/Details/";

	var rq = new XMLHttpRequest();

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


	rq.open("PUT", URL, true);
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