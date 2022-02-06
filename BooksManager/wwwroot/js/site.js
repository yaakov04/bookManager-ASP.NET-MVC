// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let removeRow = (row) => {
  row.remove();
};

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
              removeRow(row);
              Swal.fire(
                '!Eliminado!',
                response.message,
                'success'
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
  

  /*if (this.getAttribute('data-action') === 'Delete') {
    event.preventDefault();
    let form = $(this);
    let id = this.getAttribute('data-id');
    let url = `${this.action}/${id}`;
    console.log(url);

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
        console.log(this.parentElement.parentElement)
        $.ajax(url, {
          method: 'POST',
          data: form.serialize(),
          success: function (response) {
            console.log(response);

            if (response.result === 'ok') {
              Swal.fire(
                '!Eliminado!',
                response.message,
                'success'
              )
            }

          }
        })

      }
    })
  }*/

});

