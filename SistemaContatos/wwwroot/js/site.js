$(document).ready(function () {
    getDataTable("#contato-table");
    getDataTable("#users-table");
    $('.btn-conctats').click(function () {
        var userId = $(this).attr('user-id');
        console.log(userId)

        $.ajax({
            type: 'GET',
            url: '/User/ListarContatosPorUsuarioId/' + userId,
            success: function (result) {
                $('#userContactList').html(result);
                $('#ContactsModal').modal('show');
                getDataTable('#contatoTableUser')
            }
        });
        $('#btn-modal-close').click(function () {
            $('#ContactsModal').modal('hide');
        });


    });
});



function getDataTable(id) {

    $(id).DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "oLanguage": {
            "sEmptyTable": "Nenhum registro encontrado na tabela",
            "sInfo": "Mostrar _START_ até _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrar 0 até 0 de 0 Registros",
            "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Mostrar _MENU_ registros por pagina",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Proximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Ultimo"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });
}


setTimeout(function () {
    $(".alert").hide("hide");
}, 5000);
