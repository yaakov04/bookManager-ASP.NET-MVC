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
  let container = this.parentElement;

  let card = $(container).find('.card').clone();

  card.removeClass('d-none');
  card.css('box-shadow', '0 0 0');
  card.find('.card-footer').css('background-color', 'transparent');

  $('#modal-content-edit').children().remove();
  $('#modal-content-edit').append(card);
  ajaxRequest(this);
});


function deleteElement(este) {
    let form = $(este);
    let id = este.getAttribute('data-id');
    let url = `${este.action}/${id}`;

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
        let row = este.parentElement.parentElement;
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
  let newValues = {
        name: modal.find('input')[0].value,
        lastName: modal.find('input')[1].value,
    };

    let row = btn.parentElement.parentElement.parentElement;
    row.children[0].innerHTML = newValues.name;
    row.children[1].innerHTML = newValues.lastName;

    let inputs = $(btn.parentElement).find('input');
    inputs[0].value = newValues.name;
    inputs[1].value = newValues.lastName;
    
}

function editElement(este, btn){
    let form = $(este);
    let id = este.getAttribute('data-id');
    let url = `${este.action}/${id}`;

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

