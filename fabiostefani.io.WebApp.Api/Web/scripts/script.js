var tbody = document.querySelector('table tbody');
var aluno = {};

carregaEstudantes();

function inserindo(id) {
    if (aluno.id === undefined || aluno.id === 0) {
        return true;
    }
    return false;
}

function Cadastrar() {
    aluno.nome = document.querySelector('#nome').value;
    aluno.sobrenome = document.querySelector('#sobrenome').value;
    aluno.telefone = document.querySelector('#telefone').value;
    aluno.ra = document.querySelector('#ra').value;

    //if (aluno.id === undefined || aluno.id === 0)
    if (inserindo(aluno.id)) {
        salvarEstudantes('POST', 0, aluno);
    }
    else {
        salvarEstudantes('PUT', aluno.id, aluno);
    }
    carregaEstudantes();
    //console.log(aluno);
};

function Cancelar() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var btnCancelar = document.querySelector('#btnCancelar');
    var titulo = document.querySelector('#titulo');

    aluno = {};

    btnSalvar.textContent = 'Cadastrar';
    btnCancelar.textContent = 'Limpar';
    titulo.textContent = 'Cadastrar aluno';

    document.querySelector('#nome').value = '';
    document.querySelector('#sobrenome').value = '';
    document.querySelector('#telefone').value = '';
    document.querySelector('#ra').value = '';

    console.log('limpar');
}

function editarEstudante(estudante) {
    var btnSalvar = document.querySelector('#btnSalvar');
    var btnCancelar = document.querySelector('#btnCancelar');
    var titulo = document.querySelector('#titulo');

    btnSalvar.textContent = 'Salvar';
    btnCancelar.textContent = 'Cancelar';
    titulo.textContent = `Editar aluno ${estudante.nome}`;


    document.querySelector('#nome').value = estudante.nome;
    document.querySelector('#sobrenome').value = estudante.sobrenome;
    document.querySelector('#telefone').value = estudante.telefone;
    document.querySelector('#ra').value = estudante.ra;

    aluno = estudante;
    console.log(aluno);
}

function excluirEstudante(id) {
    var xhr = new XMLHttpRequest();

    xhr.open('DELETE', `http://localhost:50367/api/alunos/${id}`, false);

    xhr.send();

}

function excluir(id) {
    excluirEstudante(id);
    carregaEstudantes();
}

function carregaEstudantes() {
    tbody.innerHTML = '';
    var xhr = new XMLHttpRequest();

    xhr.open('GET', `http://localhost:50367/api/alunos`, true);

    xhr.onload = function () {
        var estudantes = JSON.parse(this.responseText);
        for (var indice in estudantes) {
            adicionaLinha(estudantes[indice]);
        }
    }
    xhr.send();
};

function salvarEstudantes(metodo, id, corpo) {

    var xhr = new XMLHttpRequest();

    if (id === undefined || id === 0)
        id = '';

    xhr.open(metodo, `http://localhost:50367/api/alunos/${id}`, false);


    xhr.setRequestHeader('content-type', 'application/json');
    xhr.send(JSON.stringify(corpo));
};

function adicionaLinha(estudante) {

    var trow = `<tr>
                            <td>${estudante.nome}</td>
                            <td>${estudante.sobrenome}</td>
                            <td>${estudante.telefone}</td>
                            <td>${estudante.ra}</td>
                            <td>
                                <button class="btn btn-info" data-toggle="modal" data-target="#myModal" onclick='editarEstudante(${JSON.stringify(estudante)})'>Editar </button>
                                <button class="btn btn-danger"  onclick='excluir(${estudante.id})'>Excluir </button> 
                            </td>
                        </tr>
                       `
    tbody.innerHTML += trow;
}