const OnlyDomainURL = "https://localhost:44384/";
const APIURL = "https://localhost:44384/api/TeacherData/";

/**
 * Call API for Add teacher */

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

/**
 * Call API for Delete teacher */

function DeleteTeacher(TeacherId) {
	debugger;
	//DELETE : https://localhost:44384/api/TeacherData/DeleteTeacher/{TeacherId}

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

/**
 * Call API for Update teacher */
function UpdateTeacher(TeacherId) {
	debugger;
	//PUT : https://localhost:44384/api/TeacherData/UpdateTeacher/{TeacherId}
	
	var URL = APIURL + "UpdateTeacher/" + TeacherId;
	var OutputURL = OnlyDomainURL + "Teacher/Details/" + TeacherId;

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
		//ready state should be 4 AND status should be starts with 2 like 2XX for 200, 204
		if (rq.readyState == 4 && (rq.status.toString().startsWith('2'))) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.

			window.location.href = OutputURL;
		}

	}
	//POST information sent through the .send() method
	console.log(JSON.stringify(TeacherData));
	rq.send(JSON.stringify(TeacherData));

}