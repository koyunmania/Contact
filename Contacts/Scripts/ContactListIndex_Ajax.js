$(document).ready(function () {
	$("#searchBox").on('keypress', function (event) {
		if (event.which === 13) {
			//deleteRows();
			//searchContacts(this.value);
			searchContacts();
		}
	});
});

function searchContacts() {
	$.ajax({
		url: '/ContactList/updateList',
		type: 'POST',
		dataType: 'json',
		success: function (data) {
			$(data).each(function (index, item) {
				addRows(item);
			});
		}
	});
}

function deleteRows() {
	var table = document.getElementById("contactsBody");
	var contactItemNumb = table.getElementsByTagName("tr").length;
	for (var i = 0; i < contactItemNumb; i++) {
		table.deleteRow(0);
	}
}

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