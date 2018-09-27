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
    $('#myModal').modal('hide');
};

function NovoAluno(){
    var btnSalvar = document.querySelector('#btnSalvar');
    
    var titulo = document.querySelector('#tituloModal');
    document.querySelector('#nome').value = '';
    document.querySelector('#sobrenome').value = '';
    document.querySelector('#telefone').value = '';
    document.querySelector('#ra').value = '';

    aluno = {};

    btnSalvar.textContent = 'Cadastrar';    
    
    titulo.textContent = 'Cadastrar aluno';

    $('#myModal').modal('show');
}

function Cancelar() {
    var btnSalvar = document.querySelector('#btnSalvar');
    
    var titulo = document.querySelector('#tituloModal');
    document.querySelector('#nome').value = '';
    document.querySelector('#sobrenome').value = '';
    document.querySelector('#telefone').value = '';
    document.querySelector('#ra').value = '';

    aluno = {};

    btnSalvar.textContent = 'Cadastrar';    
    
    titulo.textContent = 'Cadastrar aluno';

    $('#myModal').modal('hide');
    //console.log('limpar');
}

function editarEstudante(estudante) {
    var btnSalvar = document.querySelector('#btnSalvar');    
    var titulo = document.querySelector('#tituloModal');

    btnSalvar.textContent = 'Salvar';    
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

function excluir(estudante) {

    bootbox.confirm({
        message: `Tem certeza que deseja excluir o estudante ${estudante.nome}?`,
        buttons: {
            confirm: {
                label: 'Sim',
                className: 'btn-success'
            },
            cancel: {
                label: 'Não',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result){
                excluirEstudante(id);
                carregaEstudantes();
            }    
        }
    });

    
}

function carregaEstudantes() {
    tbody.innerHTML = '';
    var xhr = new XMLHttpRequest();
    console.log('UNSENT', xhr.readyState);

    xhr.open('GET', `http://localhost:50367/api/alunos`, true);
    console.log('OPENED', xhr.readyState);

    xhr.onprogress = function() {
        console.log('LOADING', xhr.readyState);
    }

    xhr.onerror = function() {
        console.log('ERROR', xhr.readyState);
    }

    xhr.onload = function () {
        var estudantes = JSON.parse(this.responseText);
        console.log('DONE', xhr.readyState);
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
                                <button class="btn btn-danger"  onclick='excluir(${JSON.stringify( estudante)})'>Excluir </button> 
                            </td>
                        </tr>
                       `
    tbody.innerHTML += trow;
}