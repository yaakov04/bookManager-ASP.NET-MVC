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
  };
 

  const newValues = {
        name: modalInput.Name ? modalInput.Name.value : null ,
        lastName: modalInput.LastName ? modalInput.LastName.value : null,
    };

    const row = btn.parentElement.parentElement.parentElement;

    const td = {
      name: $(row).find('td.name')[0] || null,
      lastname: $(row).find('td.lastname')[0] || null,
    };

    if(td.name){td.name.innerHTML = newValues.name}
    if(td.lastname){td.lastname.innerHTML = newValues.lastName}

    const form = btn.parentElement;
    const inputs ={
      Name: $(form).find('input[name="Name"]') || null,
      LastName: $(form).find('input[name="LastName"]') || null,
    };

    if(inputs.Name){inputs.Name.value = newValues.name}
    if(inputs.LastName){inputs.LastName.value = newValues.lastName}
    
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

