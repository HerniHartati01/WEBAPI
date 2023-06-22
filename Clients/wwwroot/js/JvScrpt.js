/*const animals = [
    { name: "nemo", species: "fish", class: { name: "invertebrata" } },
    { name: "gary", species: "mouse", class: { name: "mamalia" } },
    { name: "dory", species: "fish", class: { name: "invertebrata" } },
    { name: "tom", species: "mouse", class: { name: "mamalia" } },
    { name: "aji", species: "wibu", class: { name: "mamalia" } }
]

console.log(animals);
animals[0].class.name

 
*//*Cek Spesies bukan "Mouse", Class diganti jadi "NonMamalia"*//*


for (let i = 0; i < animals.length; i++) {
    if (animals[i].species !== "mouse") {
        animals[i].class = { name: "non-mamalia" };
    }
}

console.log(animals);

*//*Print "mouse" species to new variable onlyMouse*//*


const onlyMouse = animals.filter(animal => animal.species === "mouse");
console.log(onlyMouse);*/


/* Print table from API */

/*$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon"
}).done((result) => {
    let temp = ""
    let number = 0
    $.each(result.results, (key, val) => {
        number += 1
        temp += `<tr> +
            <td>${number}</td> 
            <td>${val.name}</td>
            <td><button style="border-radius: 30px;" onclick="detail('${val.url}')" type="button" data-bs-toggle="modal" data-bs-target="#exampleModal">Detail</button></>
            </tr>;`
    })
    console.log(temp);
    $("#tabelSW tbody").html(temp);
}).fail((error) => {
    console.log(error);
});*/


/*$(document).ready(function () {
    $("#tabel1").DataTable({
        ajax: {
            url: "https://localhost:7148/api/Employee",
            dataSrc: "data",
            dataType: "JSON"
        },
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'copy',
                className: 'button2',
                text: 'Copy',
            },
            {
                extend: 'csv',
                className: 'button2',
                text: 'CSV',
            },
            {
                extend: 'excel',
                className: 'button2',
                text: 'Excel',
            },
            {
                extend: 'pdf',
                className: 'button2',
                text: 'PDF',
            },
            {
                extend: 'print',
                className: 'button2',
                text: 'Print',
            },
        ],
        columns: [
            {
                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { data: "guid" },
            { data: "nik" },
            { data: "firstName" },
            { data: "lastName" },
            { data: "birthDate" },
            { data: "gender" },
            { data: "hiringDate" },
            { data: "email" },
            { data: "phoneNumber" },
            {
                render: function () {
                    return `<button type="button" class="button2" data-bs-toggle="modal" data-bs-target="#employeeModal" onclick="openAddEmpModal()">Employee</button>`;
                }
            }
           *//* {
                data: "url",
                render: function (data, type, row) {
                    return `<button class="button2" type="button" data-bs-toggle="modal" data-bs-target="#employeeModal" onclick="openAddEmpModal('${data}')">Employee</button>`;
                }
            },*//*
            *//*{
                render: function (data, type, row) {
                    return `<button class="button2" data-bs-toggle="modal" data-bs-target="#employeeModal" onclick="openAddEmpModal()">Profile</button>`;
                }
            },
            {
                render: function (data, type, row) {
                    return `<button class="button2" data-bs-toggle="modal" data-bs-target="#profileModal" onclick="openAddProfileModal()">Employee</button>`;
                }
            }*//*
        ]
    });
});
*/


$(document).ready(function () {
    $('#tabel1').DataTable({
        ajax: {
            url: "https://localhost:7148/api/Employee",
            dataSrc: "data",
            dataType: "JSON"
        },
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'copy',
                className: 'button2',
                text: 'Copy',
            },
            {
                extend: 'csv',
                className: 'button2',
                text: 'CSV',
            },
            {
                extend: 'excel',
                className: 'button2',
                text: 'Excel',
            },
            {
                extend: 'pdf',
                className: 'button2',
                text: 'PDF',
            },
            {
                extend: 'print',
                className: 'button2',
                text: 'Print',
            }
        ],
        columns: [
            {
                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { data: "guid" },
            { data: "nik" },
            { data: "firstName" },
            { data: "lastName" },
            { data: "birthDate" },
            {
                data: "gender",
                render: function (data) {
                    return data === 0 ? "Female" : "Male";
                }
            },
            { data: "hiringDate" },
            { data: "email" },
            { data: "phoneNumber" },
            {
                render: function (data, type, row, meta) {
                    return `<button type="button" class="button4" data-bs-toggle="modal" data-bs-target="#employeeModalUpdate" onclick="openEditEmpModal('${row?.guid}', '${row?.nik}', '${row?.firstName}', '${row?.lastName}', '${row.birthDate}', '${row.gender}', '${row.hiringDate}', '${row.email}', '${row.phoneNumber}')">Edit</button>
                            <button type="button" class="button4" onclick="deleteEmployee('${row.guid}')" id="${row.guid}">Delete</button>`;
                }
            }
        ]
    });
});

function AddEmployee() {
    var eNik = $('#employee-nik').val();
    var eFirst = $('#employee-fname').val();
    var eLast = $('#employee-lname').val();
    var eBDate = $('#employee-bdate').val();
    var eGender = document.querySelector('input[name="employee-gender"]:checked').id.includes('m') ? 1 : 0;    var eHDate = $('#employee-hdate').val();
    var eEmail = $('#employee-email').val();
    var ePhone = $('#employee-pnumber').val();
    console.log(eNik)
    console.log(eFirst)
    console.log(eLast)
    console.log(eBDate)
    console.log(eGender)
    console.log(eHDate)
    console.log(eEmail)
    console.log(ePhone)

    $.ajax({
        async: true, // Async by default is set to “true” load the script asynchronously  
        // URL to post data into sharepoint list  
        url: "https://localhost:7148/api/Employee",
        method: "POST", //Specifies the operation to create the list item  
        data: JSON.stringify({
            '__metadata': {
                'type': 'SP.Data.EmployeeListItem' // it defines the ListEnitityTypeName  
            },
            //Pass the parameters
            'nik': eNik,
            'firstName': eFirst,
            'lastName': eLast,
            'birthDate': eBDate,
            'gender': eGender,
            'hiringDate': eHDate,
            'email': eEmail,
            'phoneNumber': ePhone
        }),
        headers: {
            "accept": "application/json;odata=verbose", //It defines the Data format   
            "content-type": "application/json;odata=verbose", //It defines the content type as JSON  
/*            "X-RequestDigest": $("#__REQUESTDIGEST").val() //It gets the digest value   
*/        },
        success: function (data) {
            console.log(data);
            alert('Employee added successfully!');
            $('#tabel1').DataTable().ajax.reload();
        },
        error: function (error) {
            console.log(JSON.stringify(error));
            alert('Error adding employee: ' + error);

        }

    })

}


//Update Data Employee
function openEditEmpModal(guid, nik, firstName, lastName, birthDate, gender, hiringDate, email, phoneNumber) {
    // Show the modal 
    document.getElementById('emp-nik').value = nik;
    document.getElementById('emp-fname').value = firstName;
    document.getElementById('emp-lname').value = lastName;
    document.getElementById('emp-bdate').value = birthDate;
    document.getElementById('emp-hdate').value = hiringDate;
    document.getElementById('emp-email').value = email;
    document.getElementById('emp-pnumber').value = phoneNumber;

    // Set the gender radio button based on the gender value
    if (gender === 1) {
        document.getElementById('emp-gender-m').checked = true;
    } else {
        document.getElementById('emp-gender-f').checked = true;
    }

    // Show the modal
    $('#employeeModalUpdate').modal('show');

    // Add an event listener to the form submit button for updating the employee
    document.getElementById('employeeModalBodyUpdate').addEventListener('submit', function (event) {
        event.preventDefault(); // Prevent form submission

        // Call the updateEmployee function with the GUID parameter
        updateEmployee(guid);
    });
}

// Update Employee Function
function updateEmployee(guid) {
    var eNik = $('#emp-nik').val();
    var eFirst = $('#emp-fname').val();
    var eLast = $('#emp-lname').val();
    var eBDate = $('#emp-bdate').val();
    var eGender = document.querySelector('input[name="emp-gender"]:checked').id.includes('m') ? 1 : 0;
    var eHDate = $('#emp-hdate').val();
    var eEmail = $('#emp-email').val();
    var ePhone = $('#emp-pnumber').val();


    $.ajax({
        async: true,
        url: `https://localhost:7148/api/Employee`,
        method: "PUT", // Use PUT method for updating
        data: JSON.stringify({
            '__metadata': {
                'type': 'SP.Data.EmployeeListItem'
            },
            'guid': guid,
            'nik': eNik,
            'firstName': eFirst,
            'lastName': eLast,
            'birthDate': eBDate,
            'gender': eGender,
            'hiringDate': eHDate,
            'email': eEmail,
            'phoneNumber': ePhone
        }),
        headers: {
            "accept": "application/json;odata=verbose",
            "content-type": "application/json;odata=verbose",
            "X-HTTP-Method": "MERGE",
            "IF-MATCH": "*"
        },
        success: function (data) {
            console.log(data);
            window.location.reload();

            // Update the row in the DataTable with the updated data
            var UpdaterowData = [
                eNik,
                eFirst,
                eLast,
                eBDate,
                eGender === 1 ? 'Male' : 'Female',
                eHDate,
                eEmail,
                ePhone
                // Add other columns as needed
            ];

            // Loop through each row in the DataTable
            dataTable.rows().every(function (rowIdx, tableLoop, rowLoop) {
                var rowData = this.data();

                // Check if the GUID in the row matches the guid parameter
                if (rowData[0] === guid) {
                    // Update the row data
                    this.data(updatedRowData);

                    // Redraw the updated row
                    this.invalidate();

                    // Exit the loop
                    return false;
                }
            });

            // Hide the modal
            $('#employeeModalUpdate').modal('hide');
        },
        error: function (error) {
            console.log(JSON.stringify(error));
        }
    });
}


function deleteEmployee(guid) {
    console.log(guid)
    $.ajax({
        url: `https://localhost:7148/api/Employee/${guid}`,
        type: 'DELETE',
        success: function (result) {    
            console.log(result);
            window.location.reload();
        }, error: function (xhr, status, error) {
            console.error('error occured: ', error)
        }
    })
}


function openAddEmpModal() {
    $("#employeeModal").modal("show");
}

/*function openAddProfileModal() {
    $("#profileModal").modal("show");
}*/

// Example starter JavaScript for disabling form submissions if there are invalid fields
(function () {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                }

                form.classList.add('was-validated')
            }, false)
        })
})()



/*function validateAndOpenAddEmpModal() {
    if (validate()) {
        openAddProfileModal();
    } else {
        // Show error message or perform other actions
        console.log('Validation failed. Please try again.');
    }
}*/


/*function detail(stringUrl) {
    $.ajax({
        url: stringUrl,
    }).done((res) => {
        console.log(res)
        $("#imgPoke").attr("src", res.sprites.other['official-artwork'].front_default)
        $("#pokemonFront").attr("src", res.sprites.front_default);
        $("#pokemonBack").attr("src", res.sprites.back_default);
        $("#modal-title").html(res.name)
        $("#height").html(res.height)
        $("#weight").html(res.weight)
        $("#baseExperience").html(res.base_experience)

        $.ajax({
            url: res.species.url
        }).done((species) => {
            const description = species.flavor_text_entries.find(entry => entry.language.name === "en");
            $("#description").html(description.flavor_text);
        }).fail((error) => {
            console.log(error);
        });

        *//*$.each(res.flavor_text_entries, (key, val) => {
            if (val.language.name == 'en') {
                console.log(val.flavor_text)
                $("#description").html(val.flavor_text)
            }
        })*//*

        *//*let hp = res.height;
        let hpProgressBar = $("#hp");
        hpProgressBar.css("width", `${ hp } %`);
        hpProgressBar.attr("aria-valuenow", hp);
        console.log(hp);

        let wght = res.weight;
        let wghtProgressBar = $("#wght");
        wghtProgressBar.css("width", `${wght} %`);
        wghtProgressBar.attr("aria-valuenow", wght);

        let bExperience = res.base_experience;
        let bExperienceProgressBar = $("#bExperience");
        bExperienceProgressBar.css("width", `${bExperience} %`);
        bExperienceProgressBar.attr("aria-valuenow", bExperience);*//*

        let temp = "";
        $.each(res.types, (key, value) => {
            console.log(value)
            const type = value.type.name;
            const badgeColor = {
                grass: "success",
                fire: "danger",
                poison: "warning",
                normal: "secondary",
                flying: "dark",
                water: "primary",
                bug: "info",
            }[type] || "light text-dark";
            const badgeSize = "badge-width-1500px";
            temp += `<span class="badge bg-${badgeColor} ${badgeSize}" style="margin:10px; align-content:center">${type}</span>`;
        });
        $("#rowBadge").html(temp);

    });
 
}*/



    
    









