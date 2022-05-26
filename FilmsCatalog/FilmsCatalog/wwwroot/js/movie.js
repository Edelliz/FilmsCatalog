$("#formCreateMovieDirector").submit(function (e) {

    let name = $("#nameDirector").val()
    let surname = $("#surnameDirector").val()
    let patronymic = $("#patronymicDirector").val()

    let fullname = "";

    if (patronymic === null || patronymic === " ") {
        fullname = name + " " + surname
    }
    else {
        fullname = name + " " + surname + " " + patronymic
    }

    $.ajax({
        method: 'post',
        url: '/Movies/CreateMovieDirector',
        data: {
            name: name,
            surname: surname,
            patronymic: patronymic
        }
    })
        .done(function (msg) {
            $("#modalMovieDirectorForm").modal('toggle');
            $(".mdb-select").append($('<option value="'+ msg +'" disable selected>' + fullname + '</option>'));
        })

    //clearFileds();
});

/*function clearFileds() {
    $("#nameDirector").val() = " ";
    $("#surnameDirector").val() = " ";
    $("#patronymicDirector").val() = " ";
}*/
