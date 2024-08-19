
function confirmIt()
{
    return confirm("Tem a certeza que quer apagar este registo?");
}

function confirmItImagem()
{
    return confirm("Deseja apagar a imagem?");
}

/*function LinhaActiva(elementoSelecionado)
{

    var elementoComClass = document.getElementsByClassName("linha_activa")[0];

    if (elementoComClass != null)
    {
        elementoComClass.classList.remove("linha_activa");
    }

    elementoSelecionado.classList.add("linha_activa");
}*/

function LinkActivo(elementoSelecionado,nomeClass)
{

    var elementoComClass = document.getElementsByClassName(nomeClass)[0];

    if (elementoComClass != null) {

        elementoComClass.classList.remove(nomeClass);
    }

    var elementoPai = elementoSelecionado.parentElement;

    var id = elementoPai.getAttribute("id");

    localStorage.setItem("id",id);
}

function LinkActivoArranque() {

    var id = localStorage.getItem("id");

    localStorage.removeItem("id");

    if (id!="")
    {
        var elemento = document.getElementById(id);

        if (id == "iniciar_sessao" || id == "administracao")
        {
            elemento.classList.add("iniciar_sessao_activo");
        }
        else if (id =="definicoes"|| id=="terminar_sessao")
        {
            elemento.children[0].classList.add("active");
        } 
        else if (id=="dados" || id=="contactos" || id=="enderecos")
        {
            elemento.parentElement.parentElement.children[0].classList.add("active");
        }
        else if (id == "anterior" || id == "proximo")
        {
            elemento.classList.add("lista_paginacao_activo");
        }
        else if (id == "receita" || id == "verComentario" || id == "adicionarComentario" || id == "avaliacao")
        {
            elemento.children[0].classList.add("active");
        }
        else if (id == "finalizarAdicionarReceita" || id == "adicionarReceita" || id == "alterarReceita" || id == "adicionarIngrediente")
        {
            elemento.children[0].classList.add("active");
        }
        else if (id == "verReceitasAnonimo" || id == "verReceitas" || id == "adicionarReceitas" || id == "meusFavoritos" || id== "minhasReceitas"
            || id=="gerirUtilizadores" || id == "gerirReceitas" || id == "gerirIngredientes" || id == "gerirCategorias" || id == "gerirMedidas" ||
                id =="paginaInicial")
        {
            elemento.classList.add("link_selecionado");
        }
    }
}