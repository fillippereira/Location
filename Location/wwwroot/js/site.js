// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function limpaCampos() {
    $("input[name=Endereco]").val("");
    $("input[name=Bairro]").val("");
    $("input[name=Cidade]").val("");
    $("input[name=Uf]").val("");
    $("input[name=Numero]").val("");
}

function pesquisaCep(cep) {
    limpaCampos();
    $.ajax({
        type: "GET",
        url: `https://viacep.com.br/ws/${cep}/json/`,
        success: function (response) {
            console.log(response);
            $("input[name=Endereco]").val(response.logradouro);
            $("input[name=Bairro]").val(response.bairro);
            $("input[name=Cidade]").val(response.localidade);
            $("input[name=Uf]").val(response.uf);
        }
    })
}


$(".custom-file-input").on("change", function () {
    var conteudo = "";
    
    var input = $("input[name=file]");
    for (var i = 0; i < input[0].files.length; ++i) {
        conteudo +=`"${input[0].files[i].name}</label>",`;
    }
    $(this).siblings(".custom-file-label").addClass("selected").html(conteudo);
});


function ExcluirFoto(guid,ref) {
    $.ajax({
        url: `/Fotos/Delete/`,
        type: "POST",
        data: { Id: guid },
        ajaxasync: true,
        success: function (response) {
            $(ref).parent().parent().parent().remove();
        }
    });
}