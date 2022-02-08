// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function ajaxRequest(btn=null){
  $("form").on("submit", function (e) {
    
    if (this.getAttribute('data-action') != null) {
      e.preventDefault();
  
      switch (this.getAttribute('data-action')) {
        case 'Delete':
          deleteElement(this);
          break;
        case 'Edit':
          editElement(this, btn);
          break;
      
        default:
          break;
      }
      
    }
    
  
  });
}


$('.init-form-edit').click(function(){
  const container = this.parentElement;

  const card = $(container).find('.card').clone();

  card.removeClass('d-none');
  card.css('box-shadow', '0 0 0');
  card.find('.card-footer').css('background-color', 'transparent');

  $('#modal-content-edit').children().remove();
  $('#modal-content-edit').append(card);
  ajaxRequest(this);
});


function deleteElement(este) {
    const form = $(este);
    const id = este.getAttribute('data-id');
    const url = `${este.action}/${id}`;

    Swal.fire({
      title: '¿Quieres eliminar este registro?',
      text: "Esta acción no se puede revertir.",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: '!Si, Eliminalo!'
    }).then((result) => {
      if (result.isConfirmed) {
        const row = este.parentElement.parentElement;
        $.ajax(url, {
          method: 'POST',
          data: form.serialize(),
          success: function (response) {

            if (response.result === 'ok') {
              row.remove();
              Swal.fire(
                '!Eliminado!',
                response.message,
                'success'
              )
            }else{
              Swal.fire(
                '!Error!',
                response.message,
                'error'
              )
            }

          }
        })

      }
    })
};

function updateRow(modal, btn){
  const modalInput = {
      Name:  modal.find('input[name="Name"]')[0] || null,
      LastName: modal.find('input[name="LastName"]')[0] || null,
      Country: modal.find('input[name="Country"]')[0] || null,
      Title: modal.find('input[name="Title"]')[0] || null,
      PublishedYear: modal.find('input[name="PublishedYear"]')[0] || null,

      AuthorId: $(modal.find('select[name="AuthorId"]')[0]).find('option:selected')[0] || null,
      PublisherId: $(modal.find('select[name="PublisherId"]')[0]).find('option:selected')[0] || null,
      CategoryId: $(modal.find('select[name="CategoryId"]')[0]).find('option:selected')[0] || null,
  };
    
  const newValues = {
      name: modalInput.Name ? modalInput.Name.value : null ,
      lastName: modalInput.LastName ? modalInput.LastName.value : null,
      country: modalInput.Country ? modalInput.Country.value : null,
      title: modalInput.Title ? modalInput.Title.value : null,
      year: modalInput.PublishedYear ? modalInput.PublishedYear.value : null,

      author: modalInput.AuthorId || null,
      publisher: modalInput.PublisherId || null,
      category: modalInput.CategoryId || null,
    };

    const row = btn.parentElement.parentElement.parentElement;

    const td = {
        name: $(row).find('td.name')[0] || null,
        lastname: $(row).find('td.lastname')[0] || null,
        country: $(row).find('td.country')[0] || null,
        title: $(row).find('td.title')[0] || null,
        year: $(row).find('td.year')[0] || null,

        author: $(row).find('td.author')[0] || null,
        publisher: $(row).find('td.publisher')[0] || null,
        category: $(row).find('td.category')[0] || null,
    };

    if(td.name){td.name.innerHTML = newValues.name}
    if (td.lastname) { td.lastname.innerHTML = newValues.lastName }
    if (td.country) { td.country.innerHTML = newValues.country }
    if (td.title) { td.title.innerHTML = newValues.title }
    if (td.year) { td.year.innerHTML = newValues.year }


    
    

    if (td.author) {
        const id = $(btn.parentElement).find('form')[0].getAttribute('data-id');
        //td.author.innerHTML = newValues.author.innerText;
        $.ajax(`${window.location.href}/GetAuthorNameOfABook/${id}`, {
            method: "GET",
            success: function (response) {
                console.log(response)
                td.author.innerHTML = response.result === 'ok' ? response.data : '';
            }
        });
    }
    if (td.publisher) { td.publisher.innerHTML = newValues.publisher.innerText }
    if (td.category) { td.category.innerHTML = newValues.category.innerText }

    const form = btn.parentElement;
    const inputs ={
        Name: $(form).find('input[name="Name"]') || null,
        LastName: $(form).find('input[name="LastName"]') || null,
        Country: $(form).find('input[name="Country"]') || null,
        Title: $(form).find('input[name="Title"]') || null,
        PublishedYear: $(form).find('input[name="PublishedYear"]') || null,

        AuthorId: $(form).find('select[name="AuthorId"]')[0] || null,
        PublisherId: $(form).find('select[name="PublisherId"]')[0] || null,
        CategoryId: $(form).find('select[name="CategoryId"]')[0] || null,
    };

    if(inputs.Name){inputs.Name.value = newValues.name}
    if (inputs.LastName) { inputs.LastName.value = newValues.lastName }
    if (inputs.Country) { inputs.Country.value = newValues.country }
    if (inputs.Title) { inputs.Title.value = newValues.title }
    if (inputs.PublishedYear) { inputs.PublishedYear.value = newValues.year }

    if (inputs.AuthorId) {
        const opt = $(inputs.AuthorId).find('option')
        for (let i = 0; i < opt.length; i++) {
            $(opt[i]).removeAttr("selected")
        }
        $(inputs.AuthorId).find(`option[value = ${newValues.author.value}]`).attr('selected', 'selected');
    }

    if (inputs.PublisherId) {
        const opt = $(inputs.PublisherId).find('option')
        for (let i = 0; i < opt.length; i++) {
            $(opt[i]).removeAttr("selected")
        }
        $(inputs.PublisherId).find(`option[value = ${newValues.publisher.value}]`).attr('selected', 'selected');
    }

    if (inputs.CategoryId) {
        const opt = $(inputs.CategoryId).find('option')
        for (let i = 0; i < opt.length; i++) {
            $(opt[i]).removeAttr("selected")
        }
        $(inputs.CategoryId).find(`option[value = ${newValues.category.value}]`).attr('selected', 'selected');
    }

    
}

function editElement(este, btn){
    const form = $(este);
    const id = este.getAttribute('data-id');
    const url = `${este.action}/${id}`;

    $.ajax(url, {
      method: 'PUT',
      data: form.serialize(),
      success: function (response) {

        if (response.result === 'ok') {
          $('#modal-edit').modal('hide');
          updateRow($('#modal-edit'), btn);
          
          Swal.fire(
            '!Correcto!',
            response.message,
            'success'
          )
        }else{
          console.log(response);
          Swal.fire(
            '!Error!',
            response.message,
            'error'
          )
        }

      }
    })
}

ajaxRequest();

