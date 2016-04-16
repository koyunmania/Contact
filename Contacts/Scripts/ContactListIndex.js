$(document).ready(function () {
    //Call search function after key press
	$("#searchBox").on('keypress', function (event) {
		if (event.which === 13) {
			deleteRows();
		    searchContacts(this.value);
		}
	});

	$(".table-header").tablesorter();
});

//Searching a search string among all columns
function searchContacts(searchStr) {
	$.ajax({
	    url: '/ContactList/updateList?searchStr=' + searchStr,
		type: 'POST',
		dataType: 'json',
		success: function (data) {
			$(data).each(function (index, item) {
				addRows(item);
			});
		}
	});
}

//Deleting all rows in the contact list
function deleteRows() {
	var table = document.getElementById("contactsBody");
	var contactItemNumb = table.getElementsByTagName("tr").length;
	for (var i = 0; i < contactItemNumb; i++) {
		table.deleteRow(0);
	}
}

//adding a row to the contact list
function addRows(item) {
	$("#contactsBody").append(
		'<tr id="contactItem">' + 
			'<td>' + item.Name + '</td>' + 
			'<td>' + item.PhoneNum + '</td>' + 
			'<td>' + item.Email + '</td>' + 
			'<td class="operation">' + 
				'<a href="/ContactList/Edit/' + item.ID + '"><p class="fa fa-pencil"></p></a>' + 
				'<a href="/Delete/' + item.ID + '"><p class="fa fa-trash"></p></a>' +
			'</td>' + 
		'</tr>'
	);
}