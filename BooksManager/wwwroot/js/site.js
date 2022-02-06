// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let deleteElement = (este) => {
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
        let row = este.parentElement.parentElement
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







$("form").on("submit", function (e) {
  
  if (this.getAttribute('data-action') != null) {
    e.preventDefault();

    switch (this.getAttribute('data-action')) {
      case 'Delete':
        deleteElement(this);
        break;
    
      default:
        break;
    }
    
  }
  

});

